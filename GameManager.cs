using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HazardManager hazardManager;
    public CombinedMeshManager meshManager;
    public GameObject pirateObject;
    public GameObject meteorObject;
    public GameObject[] ironBlocks;
    public GameObject[] glass;
    public GameObject[] steel;
    public GameObject[] bricks;
    public GameObject[] ironBlocksDummy;
    public GameObject[] glassDummy;
    public GameObject[] steelDummy;
    public GameObject[] bricksDummy;
    public GameObject ironHolder;
    public GameObject glassHolder;
    public GameObject steelHolder;
    public GameObject brickHolder;
    public GameObject lander;
    public GameObject rocketObject;
    public GameObject builtObjects;
    public int totalBlockCount;
    public bool blockLimitReached;
    public bool blockPhysics;
    public bool hazardsEnabled = true;
    public float meteorTimer;
    public float pirateTimer;
    public bool dataSaveRequested;
    public bool blocksCombined;
    public bool working;
    public bool waitingForDestroy;
    private float waitTime;
    public bool initGlass;
    public bool initBrick;
    public bool initIron;
    public bool initSteel;
    private float initBrickTimer;
    private float initGlassTimer;
    private float initIronTimer;
    private float initSteelTimer;
    public bool clearBrickDummies;
    public bool clearGlassDummies;
    public bool clearIronDummies;
    public bool clearSteelDummies;
    public bool ironMeshRequired;
    public bool steelMeshRequired;
    public bool glassMeshRequired;
    public bool brickMeshRequired;
    public float pirateAttackTimer;
    public float meteorShowerTimer;
    public float pirateFrequency;
    public PlayerController player;
    public Vector3 meteorShowerLocation;
    public bool loadedMeteorTimer;
    public bool loadedPirateTimer;
    private bool loadedBlockPhysics;
    private bool loadedHazardsEnabled;
    public Rocket rocketScript;
    public Coroutine separateCoroutine;
    public Coroutine combineCoroutine;
    public Coroutine hazardRemovalCoroutine;
    public List<Vector3> meteorShowerLocationList;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        ironBlocks = new GameObject[50];
        glass = new GameObject[50];
        steel = new GameObject[50];
        bricks = new GameObject[50];
        ironBlocksDummy = new GameObject[50];
        glassDummy = new GameObject[50];
        steelDummy = new GameObject[50];
        bricksDummy = new GameObject[50];
        int ironCount = 0;
        int glassCount = 0;
        int steelCount = 0;
        int brickCount = 0;
        foreach (GameObject obj in ironBlocks)
        {
            ironBlocks[ironCount] = Instantiate(ironHolder, transform.position, transform.rotation);
            ironBlocks[ironCount].transform.parent = builtObjects.transform;
            ironBlocks[ironCount].GetComponent<MeshPainter>().ID = ironCount;
            ironBlocks[ironCount].SetActive(false);
            ironCount++;
        }
        foreach (GameObject obj in glass)
        {
            glass[glassCount] = Instantiate(glassHolder, transform.position, transform.rotation);
            glass[glassCount].transform.parent = builtObjects.transform;
            glass[glassCount].GetComponent<MeshPainter>().ID = glassCount;
            glass[glassCount].SetActive(false);
            glassCount++;
        }
        foreach (GameObject obj in steel)
        {
            steel[steelCount] = Instantiate(steelHolder, transform.position, transform.rotation);
            steel[steelCount].transform.parent = builtObjects.transform;
            steel[steelCount].GetComponent<MeshPainter>().ID = steelCount;
            steel[steelCount].SetActive(false);
            steelCount++;
        }
        foreach (GameObject obj in bricks)
        {
            bricks[brickCount] = Instantiate(brickHolder, transform.position, transform.rotation);
            bricks[brickCount].transform.parent = builtObjects.transform;
            bricks[brickCount].GetComponent<MeshPainter>().ID = brickCount;
            bricks[brickCount].SetActive(false);
            brickCount++;
        }

        // Get a reference to the player
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Get a reference to the rocket
        rocketScript = rocketObject.GetComponent<Rocket>();

        // Initiate meteor shower location list
        meteorShowerLocationList = new List<Vector3>();

        // Create the hazard manager.
        hazardManager = new HazardManager(this);

        // Create the combined mesh manager.
        meshManager = new CombinedMeshManager(this);
    }

    // Called once per frame by unity engine
    public void Update()
    {
        if (FileBasedPrefs.initialized == true)
        {
            if (FileBasedPrefs.GetBool(GetComponent<StateManager>().WorldName + "Initialized") == false)
            {
                if (lander.GetComponent<InventoryManager>().initialized == true)
                {
                    if (GetComponent<StateManager>().WorldName.Equals("devMachineTest"))
                    {
                        lander.GetComponent<InventoryManager>().AddItem("Universal Extractor", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Universal Conduit", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Storage Container", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Solar Panel", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Reactor Turbine", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Nuclear Reactor", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Smelter", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Press", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Extruder", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Auger", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Alloy Smelter", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Heat Exchanger", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Turret", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Dark Matter Collector", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Dark Matter Conduit", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Storage Computer", 1000);
                    }
                    else if (GetComponent<StateManager>().WorldName.Equals("devBuildTest"))
                    {
                        lander.GetComponent<InventoryManager>().AddItem("Brick", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Brick", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Brick", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Brick", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Iron Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Iron Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Iron Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Iron Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Steel Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Steel Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Steel Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Steel Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Glass Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Glass Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Glass Block", 1000);
                        lander.GetComponent<InventoryManager>().AddItem("Glass Block", 1000);
                    }
                    else
                    {
                        lander.GetComponent<InventoryManager>().AddItem("Solar Panel", 9);
                        lander.GetComponent<InventoryManager>().AddItem("Universal Conduit", 8);
                        lander.GetComponent<InventoryManager>().AddItem("Storage Container", 4);
                        lander.GetComponent<InventoryManager>().AddItem("Smelter", 3);
                        lander.GetComponent<InventoryManager>().AddItem("Universal Extractor", 2);
                        lander.GetComponent<InventoryManager>().AddItem("Dark Matter Conduit", 1);
                        lander.GetComponent<InventoryManager>().AddItem("Dark Matter Collector", 1);
                    }
                    FileBasedPrefs.SetBool(GetComponent<StateManager>().WorldName + "Initialized", true);
                }
            }
            else
            {
                if (loadedBlockPhysics == false)
                {
                    blockPhysics = PlayerPrefsX.GetPersistentBool("blockPhysics");
                    loadedBlockPhysics = true;
                }
                if (loadedHazardsEnabled == false)
                {
                    hazardsEnabled = PlayerPrefsX.GetPersistentBool("hazardsEnabled");
                    loadedHazardsEnabled = true;
                }
                if (loadedMeteorTimer == false)
                {
                    meteorShowerTimer = FileBasedPrefs.GetFloat(GetComponent<StateManager>().WorldName + "meteorShowerTimer");
                    loadedMeteorTimer = true;
                }
                if (loadedPirateTimer == false)
                {
                    pirateAttackTimer = FileBasedPrefs.GetFloat(GetComponent<StateManager>().WorldName + "pirateAttackTimer");
                    loadedPirateTimer = true;
                }
            }

            // A save game request is pending
            if (dataSaveRequested == true)
            {
                if (GetComponent<StateManager>().saving == false && GetComponent<StateManager>().assigningIDs == false)
                {
                    Debug.Log("Saving world...");
                    GetComponent<StateManager>().SaveData();
                    dataSaveRequested = false;
                }
            }

            // Used to ensure components are removed before combining meshes
            if (waitingForDestroy == true)
            {
                waitTime += 1 * Time.deltaTime;
                if (waitTime > 1)
                {
                    meshManager.CombineMeshes();
                    waitTime = 0;
                    waitingForDestroy = false;
                }
            }

            // Clear out dummy objects used for smooth transitions while combining meshes
            if (clearBrickDummies == true)
            {
                if (initBrickTimer < 3)
                {
                    initBrickTimer += 1 * Time.deltaTime;
                }
                else
                {
                    BlockDummy[] dummies = FindObjectsOfType<BlockDummy>();
                    foreach (BlockDummy dummy in dummies)
                    {
                        if (dummy.type.Equals("brick"))
                        {
                            Destroy(dummy.gameObject);
                        }
                    }
                    initBrickTimer = 0;
                    clearBrickDummies = false;
                }
            }

            // Clear out dummy objects used for smooth transitions while combining meshes
            if (clearGlassDummies == true)
            {
                if (initGlassTimer < 3)
                {
                    initGlassTimer += 1 * Time.deltaTime;
                }
                else
                {
                    BlockDummy[] dummies = FindObjectsOfType<BlockDummy>();
                    foreach (BlockDummy dummy in dummies)
                    {
                        if (dummy.type.Equals("glass"))
                        {
                            Destroy(dummy.gameObject);
                        }
                    }
                    initGlassTimer = 0;
                    clearGlassDummies = false;
                }
            }

            // Clear out dummy objects used for smooth transitions while combining meshes
            if (clearIronDummies == true)
            {
                if (initIronTimer < 3)
                {
                    initIronTimer += 1 * Time.deltaTime;
                }
                else
                {
                    BlockDummy[] dummies = FindObjectsOfType<BlockDummy>();
                    foreach (BlockDummy dummy in dummies)
                    {
                        if (dummy.type.Equals("iron"))
                        {
                            Destroy(dummy.gameObject);
                        }
                    }
                    initIronTimer = 0;
                    clearIronDummies = false;
                }
            }

            // Clear out dummy objects used for smooth transitions while combining meshes
            if (clearSteelDummies == true)
            {
                if (initSteelTimer < 3)
                {
                    initSteelTimer += 1 * Time.deltaTime;
                }
                else
                {
                    BlockDummy[] dummies = FindObjectsOfType<BlockDummy>();
                    foreach (BlockDummy dummy in dummies)
                    {
                        if (dummy.type.Equals("steel"))
                        {
                            Destroy(dummy.gameObject);
                        }
                    }
                    initSteelTimer = 0;
                    clearSteelDummies = false;
                }
            }

            hazardManager.UpdateHazards();
        }
    }

    // Saves the game on exit
    public void RequestSaveOperation()
    {
        if (working == false && GetComponent<StateManager>().saving == false)
        {
            Debug.Log("Requesting save operation...");
            dataSaveRequested = true;
            blocksCombined = false;
        }
    }
}
