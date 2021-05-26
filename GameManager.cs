using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System;

public class GameManager : MonoBehaviour
{
    public bool fileBasedPrefsInitialized;
    private HazardManager hazardManager;
    public CombinedMeshManager meshManager;
    public GameObject pirateObject;
    public GameObject meteorObject;
    public GameObject[] ironHolders;
    public GameObject[] glassHolders;
    public GameObject[] steelHolders;
    public GameObject[] brickHolders;
    public List<string> modBlockNames;
    public List<GameObject[]> modBlockHolders;
    public GameObject ironHolder;
    public GameObject glassHolder;
    public GameObject steelHolder;
    public GameObject brickHolder;
    public GameObject modBlockHolder;
    public GameObject lander;
    public GameObject rocketObject;
    public GameObject builtObjects;
    public Material glassMaterial;
    public int chunkSize;
    public float simulationSpeed;
    public bool blockPhysics;
    public bool hazardsEnabled = true;
    public float meteorTimer;
    public float pirateTimer;
    public bool dataSaveRequested;
    public bool blocksCombined;
    public bool working;
    public bool replacingMeshFilters;
    public bool runningUndo;
    private float mfDelay;
    private float userSimSpeed;
    public bool clearBrickDummies;
    public bool clearGlassDummies;
    public bool clearIronDummies;
    public bool clearSteelDummies;
    public float pirateAttackTimer;
    public float meteorShowerTimer;
    public float pirateFrequency;
    public PlayerController player;
    public Vector3 meteorShowerLocation;
    public bool loadedMeteorTimer;
    public bool loadedPirateTimer;
    private bool loadedBlockPhysics;
    private bool loadedHazardsEnabled;
    private bool memoryCoroutineBusy;
    private bool resetSimSpeed;
    public Rocket rocketScript;
    public Coroutine separateCoroutine;
    public Coroutine meshCombineCoroutine;
    public Coroutine blockCombineCoroutine;
    public Coroutine hazardRemovalCoroutine;
    private Coroutine memoryCoroutine;
    private Coroutine undoCoroutine;
    public List<Vector3> meteorShowerLocationList;

    public class Block
    {
        public string blockType;
        public GameObject blockObject;

        public Block(string blockType, GameObject blockObject)
        {
            this.blockType = blockType;
            this.blockObject = blockObject;
        }
    }

    public List<Block> undoBlocks;

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

        // Create an object list to hold the player's most recently built objects for the 'undo' function.
        undoBlocks = new List<Block>();

        // Create mod block name list.
        modBlockNames = new List<string>();
        modBlockHolders = new List<GameObject[]>();

        // Load chunk size setting.
        int cs = PlayerPrefs.GetInt("chunkSize");
        chunkSize = cs > 0 ? cs : 40;

        // Load chunk size setting.
        float simSpeed = PlayerPrefs.GetFloat("simulationSpeed");
        simulationSpeed = simSpeed > 0 ? simSpeed : 0.02f;

        // Create initial iron block holder for mesh manager.
        GameObject ironInit = Instantiate(ironHolder, transform.position, transform.rotation);
        ironInit.transform.parent = builtObjects.transform;
        ironInit.GetComponent<MeshPainter>().ID = 0;
        ironInit.SetActive(false);
        ironHolders = new GameObject[] { ironInit };

        // Create initial iron block holder for mesh manager.
        GameObject glassInit = Instantiate(glassHolder, transform.position, transform.rotation);
        glassInit.transform.parent = builtObjects.transform;
        glassInit.GetComponent<MeshPainter>().ID = 0;
        glassInit.SetActive(false);
        glassHolders = new GameObject[] { glassInit };

        // Create initial iron block holder for mesh manager.
        GameObject steelInit = Instantiate(steelHolder, transform.position, transform.rotation);
        steelInit.transform.parent = builtObjects.transform;
        steelInit.GetComponent<MeshPainter>().ID = 0;
        steelInit.SetActive(false);
        steelHolders = new GameObject[] { steelInit };

        // Create initial iron block holder for mesh manager.
        GameObject brickInit = Instantiate(brickHolder, transform.position, transform.rotation);
        brickInit.transform.parent = builtObjects.transform;
        brickInit.GetComponent<MeshPainter>().ID = 0;
        brickInit.SetActive(false);
        brickHolders = new GameObject[] { brickInit };
    }

    //! Creates initial block holders for mod blocks.
    public void InitModBlocks()
    {
        int index = 0;
        int count = modBlockNames.Count;
        foreach (string blockName in modBlockNames)
        {
            GameObject modBlockInit = Instantiate(modBlockHolder, transform.position, transform.rotation);
            meshManager.SetMaterial(modBlockInit, blockName);
            modBlockInit.transform.parent = builtObjects.transform;
            modBlockInit.SetActive(false);
            modBlockHolders.Add(new GameObject[] { modBlockInit });
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
                if (loadedBlockPhysics == false)
                {
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == false)
                    {
                        blockPhysics = PlayerPrefsX.GetPersistentBool("blockPhysics");
                    }
                    else
                    {
                        blockPhysics = false;
                    }
                    loadedBlockPhysics = true;
                }
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

            // A save game request is pending.
            if (dataSaveRequested == true)
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
            }
            else if (resetSimSpeed == true)
            {
                GetComponent<GameManager>().simulationSpeed = userSimSpeed;
                resetSimSpeed = false;
            }

            // Used to ensure components are removed before combining meshes.
            if (replacingMeshFilters == true)
            {
                mfDelay += 1 * Time.deltaTime;
                if (mfDelay > 1)
                {
                    meshManager.CombineMeshes();
                    mfDelay = 0;
                    replacingMeshFilters = false;
                }
            }

            if (GetComponent<StateManager>().worldLoaded == true)
            {
                hazardManager.UpdateHazards();
            }

            if (memoryCoroutineBusy == false)
            {
                memoryCoroutine = StartCoroutine(ManageMemory());
            }
        }
    }

    //! Unloads unused assets when the game is using too much memory.
    public IEnumerator ManageMemory()
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
        yield return new WaitForSeconds(60);
        memoryCoroutineBusy = false;
    }

    //! Starts the undo coroutine.
    public void UndoBuiltObjects()
    {
        if (runningUndo == false)
        {
            undoCoroutine = StartCoroutine(RemoveRecent());
        }
    }

    //! Removes recently placed blocks.
    private IEnumerator RemoveRecent()
    {
        runningUndo = true;
        foreach (Block block in undoBlocks)
        {
            if (block.blockObject != null)
            { 
                if (block.blockObject.activeInHierarchy)
                {
                    Destroy(block.blockObject);
                    player.playerInventory.AddItem(block.blockType, 1);
                    player.PlayCraftingSound();
                }
            }
            yield return null;
        }
        undoBlocks.Clear();
        runningUndo = false;
    }

    //! Saves the game on exit.
    public void RequestSaveOperation()
    {
        if (working == false && GetComponent<StateManager>().saving == false)
        {
            UnityEngine.Debug.Log("Requesting save operation...");
            dataSaveRequested = true;
            blocksCombined = false;
        }
    }
}