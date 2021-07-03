using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System;
using System.Net;
using MEC;

public class GameManager : MonoBehaviour
{
    public bool fileBasedPrefsInitialized;
    private HazardManager hazardManager;
    public CombinedMeshManager meshManager;
    public Dictionary<string, Material> materialDictionary;
    public TextureDictionary textureDictionary;
    public GameObject pirateObject;
    public GameObject meteorObject;
    public GameObject machineExplosion;
    public List<string> blockNames;
    public List<GameObject[]> blockHolders;
    public GameObject blockHolder;
    public GameObject lander;
    public GameObject rocketObject;
    public GameObject builtObjects;
    public Material glassMaterial;
    public Mesh rampMesh;
    public int chunkSize;
    public float simulationSpeed;
    public bool hazardsEnabled = true;
    public float meteorTimer;
    public float pirateTimer;
    public bool dataSaveRequested;
    public bool combiningBlocks;
    public bool runningUndo;
    private float userSimSpeed;
    public float pirateAttackTimer;
    public float meteorShowerTimer;
    public float pirateFrequency;
    public PlayerController player;
    public Vector3 meteorShowerLocation;
    public bool loadedMeteorTimer;
    public bool loadedPirateTimer;
    private bool loadedHazardsEnabled;
    private bool memoryCoroutineBusy;
    private bool meshCountCoroutineBusy;
    private bool resetSimSpeed;
    private bool readyToSave;
    public Rocket rocketScript;
    public List<Vector3> meteorShowerLocationList;

    //! Stores information about blocks placed for UndoBuiltObjects function.
    public class UndoBlock
    {
        public string blockType;
        public GameObject blockObject;

        public UndoBlock(string blockType, GameObject blockObject)
        {
            this.blockType = blockType;
            this.blockObject = blockObject;
        }
    }

    public List<UndoBlock> undoBlocks;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        // Get a reference to the player.
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Get a reference to the rocket.
        rocketScript = rocketObject.GetComponent<Rocket>();

        // Initiate meteor shower location list.
        meteorShowerLocationList = new List<Vector3>();

        // Create the hazard manager.
        hazardManager = new HazardManager(this);

        // Create the combined mesh manager.
        meshManager = new CombinedMeshManager(this);

        // Create the material dictionary.
        textureDictionary = GetComponent<TextureDictionary>();
        materialDictionary = new Dictionary<string, Material>();

        // Create an object list to hold the player's most recently built objects for the 'undo' function.
        undoBlocks = new List<UndoBlock>();

        // Create block lists.
        blockNames = new List<string>();
        blockHolders = new List<GameObject[]>();

        // Load chunk size setting.
        int cs = PlayerPrefs.GetInt("chunkSize");
        chunkSize = cs > 0 ? cs : 40;

        // Load chunk size setting.
        float simSpeed = PlayerPrefs.GetFloat("simulationSpeed");
        simulationSpeed = simSpeed > 0 ? simSpeed : 0.02f;
    }

    //! Creates initial block holders for mod blocks.
    public void InitBlocks()
    {
        int index = 0;
        int count = blockNames.Count;
        foreach (string blockName in blockNames)
        {
            GameObject blockInit = Instantiate(blockHolder, transform.position, transform.rotation);
            blockInit.GetComponent<BlockHolder>().blockType = blockName;

            if (!materialDictionary.ContainsKey(blockName))
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = textureDictionary.dictionary[blockName];
                if (textureDictionary.dictionary.ContainsKey(blockName + "_Normal"))
                {
                    mat.shaderKeywords = new string[] { "_NORMALMAP" };
                    mat.SetTexture("_BumpMap", textureDictionary.dictionary[blockName + "_Normal"]);
                    mat.SetFloat("_BumpScale", 2);
                    mat.enableInstancing = true;
                }
                materialDictionary.Add(blockName, mat);
            }

            meshManager.SetMaterial(blockInit, blockName);

            blockInit.transform.parent = builtObjects.transform;
            blockInit.SetActive(false);
            blockHolders.Add(new GameObject[] { blockInit });

            index++;
        }
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (FileBasedPrefs.initialized == true)
        {
            if (FileBasedPrefs.GetBool(GetComponent<StateManager>().worldName + "Initialized") == false)
            {
                if (lander.GetComponent<InventoryManager>().initialized == true)
                {
                    lander.GetComponent<InventoryManager>().AddItem("Solar Panel", 9);
                    lander.GetComponent<InventoryManager>().AddItem("Universal Conduit", 8);
                    lander.GetComponent<InventoryManager>().AddItem("Storage Container", 4);
                    lander.GetComponent<InventoryManager>().AddItem("Smelter", 3);
                    lander.GetComponent<InventoryManager>().AddItem("Universal Extractor", 2);
                    lander.GetComponent<InventoryManager>().AddItem("Dark Matter Conduit", 1);
                    lander.GetComponent<InventoryManager>().AddItem("Dark Matter Collector", 1);
                    FileBasedPrefs.SetBool(GetComponent<StateManager>().worldName + "Initialized", true);
                }
            }
            else
            {
                if (loadedHazardsEnabled == false)
                {
                    hazardsEnabled = PlayerPrefsX.GetPersistentBool("hazardsEnabled");
                    loadedHazardsEnabled = true;
                }
                if (loadedMeteorTimer == false)
                {
                    meteorShowerTimer = FileBasedPrefs.GetFloat(GetComponent<StateManager>().worldName + "meteorShowerTimer");
                    loadedMeteorTimer = true;
                }
                if (loadedPirateTimer == false)
                {
                    pirateAttackTimer = FileBasedPrefs.GetFloat(GetComponent<StateManager>().worldName + "pirateAttackTimer");
                    loadedPirateTimer = true;
                }
            }

            if (GetComponent<StateManager>().worldLoaded == true)
            {
                hazardManager.UpdateHazards();
            }

            if (memoryCoroutineBusy == false)
            {
                Timing.RunCoroutine(ManageMemory());
            }

            if (meshCountCoroutineBusy == false)
            {
                Timing.RunCoroutine(CheckMeshCount());
            }

            // A save game request is pending.
            if (dataSaveRequested == true)
            {
                if (combiningBlocks == true)
                {
                    return;
                }

                if (readyToSave == false)
                {
                    meshManager.CombineBlocks();
                    readyToSave = true;
                }
                else
                {
                    if (GetComponent<StateManager>().saving == false && GetComponent<StateManager>().AddressManagerBusy() == false)
                    {
                        UnityEngine.Debug.Log("Saving world...");
                        GetComponent<StateManager>().SaveData();
                        dataSaveRequested = false;
                    }
                    else if (GetComponent<StateManager>().AddressManagerBusy() == true)
                    {
                        if (GetComponent<GameManager>().simulationSpeed < 0.1f)
                        {
                            userSimSpeed = GetComponent<GameManager>().simulationSpeed;
                            resetSimSpeed = true;
                        }
                        GetComponent<GameManager>().simulationSpeed = 0.1f;
                    }
                    readyToSave = false;
                }
            }
            else if (resetSimSpeed == true)
            {
                GetComponent<GameManager>().simulationSpeed = userSimSpeed;
                resetSimSpeed = false;
            }
        }
    }

    public IEnumerator<float> CheckMeshCount()
    {
        meshCountCoroutineBusy = true;
        Block[] blocks = builtObjects.GetComponentsInChildren<Block>(false);
        if (blocks.Length >= 1000 && combiningBlocks == false)
        {
            meshManager.CombineBlocks();
        }
        yield return Timing.WaitForSeconds(10);
        meshCountCoroutineBusy = false;
    }

    //! Unloads unused assets when the game is using too much memory.
    public IEnumerator<float> ManageMemory()
    {
        memoryCoroutineBusy = true;
        float availableMemory = SystemInfo.systemMemorySize;
        Process proc = Process.GetCurrentProcess();
        proc.Refresh();
        float usedMemory = Math.Abs((int)(proc.WorkingSet64 / (1024*1024)));
        proc.Dispose();
        float percentUsedMemory = usedMemory / availableMemory;
        if (percentUsedMemory >= 0.5f)
        {
            Resources.UnloadUnusedAssets();
        }
        yield return Timing.WaitForSeconds(60);
        memoryCoroutineBusy = false;
    }

    //! Starts the undo coroutine.
    public void UndoBuiltObjects()
    {
        if (runningUndo == false)
        {
            Timing.RunCoroutine(RemoveRecent());
        }
    }

    //! Removes recently placed blocks.
    private IEnumerator<float> RemoveRecent()
    {
        runningUndo = true;
        List<UndoBlock> currentUndoBlocks = undoBlocks;
        for (int i =0; i < currentUndoBlocks.Count; i++)
        {
            if (currentUndoBlocks[i] != null)
            {
                if (currentUndoBlocks[i].blockObject != null)
                { 
                    if (currentUndoBlocks[i].blockObject.activeInHierarchy)
                    {
                        Destroy(currentUndoBlocks[i].blockObject);
                        player.playerInventory.AddItem(currentUndoBlocks[i].blockType, 1);
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                        {
                            Vector3 pos = currentUndoBlocks[i].blockObject.transform.position;
                            Quaternion rot = currentUndoBlocks[i].blockObject.transform.rotation;
                            UpdateNetwork(1, currentUndoBlocks[i].blockType, pos, rot);
                        }
                        player.PlayCraftingSound();
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
        undoBlocks.Clear();
        runningUndo = false;
    }

    //! Sends instantiated block info to the server in multiplayer games.
    private void UpdateNetwork(int destroy, string type, Vector3 pos, Quaternion rot)
    {
        using(WebClient client = new WebClient())
        {
            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/blocks");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            string rotation = Mathf.Round(rot.x) + "," + Mathf.Round(rot.y) + "," + Mathf.Round(rot.z) + "," + Mathf.Round(rot.w);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + position + ":" + rotation);
        }
    }

    //! Saves the game on exit.
    public void RequestSaveOperation()
    {
        if (combiningBlocks == false && GetComponent<StateManager>().saving == false)
        {
            UnityEngine.Debug.Log("Requesting save operation...");
            dataSaveRequested = true;
        }
    }
}