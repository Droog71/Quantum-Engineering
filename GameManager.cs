using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private int totalBlockCount;
    public bool blockLimitReached;
    public bool blockPhysics;
    private float meteorTimer;
    private float pirateTimer;
    public bool savingData;
    public bool dataSaveRequested;
    public bool blocksCombined;
    public bool working;
    public bool exiting;
    private bool waitingForDestroy;
    private float waitTime;
    public bool initGlass;
    public bool initBrick;
    public bool initIron;
    public bool initSteel;
    private float initBrickTimer;
    private float initGlassTimer;
    private float initIronTimer;
    private float initSteelTimer;
    private bool clearBrickDummies;
    private bool clearGlassDummies;
    private bool clearIronDummies;
    private bool clearSteelDummies;
    private bool ironMeshRequired;
    private bool steelMeshRequired;
    private bool glassMeshRequired;
    private bool brickMeshRequired;
    public float pirateAttackTimer;
    public float meteorShowerTimer;
    private float pirateFrequency;
    private PlayerController player;
    private Vector3 meteorShowerLocation;
    private bool loadedMeteorTimer;
    private bool loadedPirateTimer;
    private bool loadedBlockPhysics;
    private Rocket rocketScript;
    private Coroutine separateCoroutine;
    private Coroutine combineCoroutine;
    List<Vector3> meteorShowerLocationList;

    void Start()
    {
        //Set up holder objects.
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

        //Get a reference to the player.
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        //Get a reference to the rocket.
        rocketScript = rocketObject.GetComponent<Rocket>();

        //Initiate meteor shower location list.
        meteorShowerLocationList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Meteor Frequency: " + meteorFrequency);
        if (PlayerPrefsX.GetBool(GetComponent<StateManager>().WorldName + "Initialized") == false)
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
                PlayerPrefsX.SetBool(GetComponent<StateManager>().WorldName + "Initialized", true);
            }
        }
        else
        {
            if (loadedBlockPhysics == false)
            {
                //Load block physics toggle.
                blockPhysics = PlayerPrefsX.GetBool(GetComponent<StateManager>().WorldName + "blockPhysics");
                loadedBlockPhysics = true;
            }

            if (loadedMeteorTimer == false)
            {
                //Load meteor shower timer.
                meteorShowerTimer = PlayerPrefs.GetFloat(GetComponent<StateManager>().WorldName + "meteorShowerTimer");
                loadedMeteorTimer = true;
            }
            if (loadedPirateTimer == false)
            {
                //Load meteor shower timer.
                pirateAttackTimer = PlayerPrefs.GetFloat(GetComponent<StateManager>().WorldName + "pirateAttackTimer");
                loadedPirateTimer = true;
            }
        }

        if (dataSaveRequested == true)
        {
            UnityEngine.Debug.Log("Waiting for state manager to finish current save operation...");
            if (GetComponent<StateManager>().dataSaved == true && savingData == false)
            {
                UnityEngine.Debug.Log("Saving structures...");
                GetComponent<StateManager>().SaveData();
                savingData = true;
                dataSaveRequested = false;
            }
        }

        //Waiting for component destruction on combined meshes.
        if (waitingForDestroy == true)
        {
            waitTime += 1 * Time.deltaTime;
            if (waitTime > 1)
            {
                CombineMeshes();
                waitTime = 0;
                waitingForDestroy = false;
            }
        }

        //Block dummy mesh deletion on init for combined mesh.
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

        //Pirate attacks and meteor showers
        if (player.timeToDeliver == false && rocketScript.gameTime < 2000)
        {
            //Pirate attacks
            if (rocketScript.day >= 10 && GetComponent<StateManager>().worldLoaded == true)
            {
                pirateAttackTimer += 1 * Time.deltaTime;
                if (loadedPirateTimer == true)
                {
                    PlayerPrefs.SetFloat(GetComponent<StateManager>().WorldName + "pirateAttackTimer", pirateAttackTimer);
                }
                if (pirateAttackTimer >= 530 & pirateAttackTimer < 540)
                {
                    if (player != null)
                    {
                        if (player.pirateAttackWarningActive == false)
                        {
                            player.pirateAttackWarningActive = true;
                        }
                    }
                }
                else if (pirateAttackTimer >= 540 && pirateAttackTimer < 600)
                {
                    if (player != null)
                    {
                        if (player.pirateAttackWarningActive == true)
                        {
                            player.pirateAttackWarningActive = false;
                        }
                    }
                    pirateFrequency = 40 - rocketScript.day;
                    if (pirateFrequency < 2)
                    {
                        pirateFrequency = 2;
                    }
                    pirateTimer += 1 * Time.deltaTime;
                    if (pirateTimer >= pirateFrequency && GetComponent<StateManager>().worldLoaded == true)
                    {
                        float x = Random.Range(-4500, 4500);
                        float z = Random.Range(-4500, 4500);
                        int RandomSpawn = Random.Range(1, 5);
                        if (RandomSpawn == 1)
                        {
                            Instantiate(pirateObject, new Vector3(x, 400, 10000), transform.rotation);
                        }
                        if (RandomSpawn == 2)
                        {
                            GameObject pirate = Instantiate(pirateObject, new Vector3(x, 400, -10000), transform.rotation);
                        }
                        if (RandomSpawn == 3)
                        {
                            GameObject pirate = Instantiate(pirateObject, new Vector3(10000, 400, z), transform.rotation);
                        }
                        if (RandomSpawn == 4)
                        {
                            GameObject pirate = Instantiate(pirateObject, new Vector3(-10000, 400, z), transform.rotation);
                        }
                        pirateTimer = 0;
                    }
                }
                else if (pirateAttackTimer >= 900)
                {
                    pirateAttackTimer = 0;
                    player.destructionMessageActive = false;
                }
            }

            //Meteor showers
            if (GetComponent<StateManager>().worldLoaded)
            {
                meteorShowerTimer += 1 * Time.deltaTime;
            }

            if (loadedMeteorTimer == true)
            {
                PlayerPrefs.SetFloat(GetComponent<StateManager>().WorldName + "meteorShowerTimer", meteorShowerTimer);
                //Debug.Log("METEOR TIMER: " + meteorShowerTimer);
            }
            if (meteorShowerTimer >= 530 && meteorShowerTimer < 540)
            {
                if (player != null)
                {
                    if (player.meteorShowerWarningActive == false)
                    {
                        player.meteorShowerWarningActive = true;
                    }
                }
            }
            else if (meteorShowerTimer >= 540 && meteorShowerTimer < 600)
            {
                bool locationFound = false;
                if (locationFound == false)
                {
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject go in allObjects)
                    {
                        if (locationFound == false)
                        {
                            if (!meteorShowerLocationList.Contains(go.transform.position))
                            {
                                meteorShowerLocation = go.transform.position;
                                meteorShowerLocationList.Add(meteorShowerLocation);
                                locationFound = true;
                            }
                        }
                    }
                    if (locationFound == false && meteorShowerLocationList.Count < 0)
                    {
                        meteorShowerLocationList.Clear();
                    }
                }
                if (player != null)
                {
                    if (player.meteorShowerWarningActive == true)
                    {
                        player.meteorShowerWarningActive = false;
                    }
                }
                meteorTimer += 1 * Time.deltaTime;
                if (meteorTimer > 0.5f && GetComponent<StateManager>().worldLoaded == true)
                {
                    float x = Random.Range(meteorShowerLocation.x - 500, meteorShowerLocation.x + 500);
                    float z = Random.Range(meteorShowerLocation.z - 500, meteorShowerLocation.z + 500);
                    Instantiate(meteorObject, new Vector3(x, 500, z), transform.rotation);
                    meteorTimer = 0;
                }
            }
            else if (meteorShowerTimer >= 900)
            {
                meteorShowerTimer = 0;
                player.destructionMessageActive = false;
            }
        }
        else
        {
            pirateAttackTimer = 0;
            meteorShowerTimer = 120;
            player.destructionMessageActive = false;
        }
    }

    public void RequestFinalSaveOperation() //Save all blocks before exiting the game.
    {
        if (working == false && exiting == false)
        {
            UnityEngine.Debug.Log("Requesting final save operation...");
            exiting = true;
            dataSaveRequested = true;
            blocksCombined = false;
            GetComponent<StateManager>().exiting = true;
        }
    }

    public void SeparateBlocks(Vector3 target, string type, bool building)
    {
        //Debug.Log("Chunk load called : Game Manager");
        if (working == false && exiting == false)
        {
            //Debug.Log("Separating blocks : Game Manager");
            if (building == true)
            {
                CombineBlocks();
            }
            separateCoroutine = StartCoroutine(BlockSeparationCoroutine(target, type));
        }
        //StackTrace stackTrace = new StackTrace();
        //UnityEngine.Debug.Log(stackTrace.GetFrame(1).GetMethod().Name);
        Transform[] allBlocks = builtObjects.GetComponentsInChildren<Transform>(true);
        totalBlockCount = allBlocks.Length;
        //UnityEngine.Debug.Log("Total blocks: " + totalBlockCount);
        if (totalBlockCount >= 12000 && blockLimitReached == false)
        {
            blockLimitReached = true;
            //UnityEngine.Debug.Log("Block limit reached!");
        }
        else if (totalBlockCount < 12000 && blockLimitReached == true)
        {
            blockLimitReached = false;
        }
    }

    IEnumerator BlockSeparationCoroutine(Vector3 target, string type)
    {
        if (target != null)
        {
            int ironCount = 0;
            int steelCount = 0;
            int brickCount = 0;
            int glassCount = 0;
            int totalIron = 0;
            int totalBrick = 0;
            int totalGlass = 0;
            int totalSteel = 0;

            int ironSeprationInterval = 0;
            //Debug.Log("Started mesh separation for iron at: " + System.DateTime.Now);
            foreach (GameObject obj in ironBlocks)
            {
                //Separate all meshes within building range.
                Transform[] blocks = ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform i in blocks)
                {
                    if (i != null)
                    {
                        float distance = Vector3.Distance(i.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            i.gameObject.SetActive(true);
                            i.parent = builtObjects.transform;
                            totalIron++;
                        }
                        if (distance < 20 && type.Equals("iron"))
                        {
                            i.gameObject.SetActive(true);
                            i.parent = builtObjects.transform; ;
                            totalIron++;
                        }
                        if (distance < 10 && type.Equals("iron"))
                        {
                            i.gameObject.GetComponent<PhysicsHandler>().Explode();
                            totalIron++;
                        }
                    }
                }
                ironCount++;
                ironSeprationInterval++;
                if (ironSeprationInterval >= 50)
                {
                    yield return null;
                    ironSeprationInterval = 0;
                }
            }
            //Debug.Log("Finished mesh separation for iron at: " + System.DateTime.Now);

            int glassSeprationInterval = 0;
            //Debug.Log("Started mesh separation for glass at: " + System.DateTime.Now);
            foreach (GameObject obj in glass)
            {
                Transform[] glassBlocks = glass[glassCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform g in glassBlocks)
                {
                    if (g != null)
                    {
                        float distance = Vector3.Distance(g.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            g.gameObject.SetActive(true);
                            g.parent = builtObjects.transform;
                            totalGlass++;
                        }
                        if (distance < 20 && type.Equals("glass"))
                        {
                            g.gameObject.SetActive(true);
                            g.parent = builtObjects.transform; ;
                            totalGlass++;
                        }
                        if (distance < 10 && type.Equals("glass"))
                        {
                            g.gameObject.GetComponent<PhysicsHandler>().Explode();
                            totalGlass++;
                        }
                    }
                }
                glassCount++;
                glassSeprationInterval++;
                if (glassSeprationInterval >= 50)
                {
                    yield return null;
                    glassSeprationInterval = 0;
                }
            }
            //Debug.Log("Finished mesh separation for glass at: " + System.DateTime.Now);

            int steelSeprationInterval = 0;
            //Debug.Log("Started mesh separation for steel at: " + System.DateTime.Now);
            foreach (GameObject obj in steel)
            {
                Transform[] steelBlocks = steel[steelCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform s in steelBlocks)
                {
                    if (s != null)
                    {
                        float distance = Vector3.Distance(s.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            s.gameObject.SetActive(true);
                            s.parent = builtObjects.transform;
                            totalSteel++;
                        }
                        if (distance < 20 && type.Equals("steel"))
                        {
                            s.gameObject.SetActive(true);
                            s.parent = builtObjects.transform;
                            totalSteel++;
                        }
                        if (distance < 10 && type.Equals("steel"))
                        {
                            s.gameObject.GetComponent<PhysicsHandler>().Explode();
                            totalSteel++;
                        }
                    }
                }
                steelCount++;
                steelSeprationInterval++;
                if (steelSeprationInterval >= 50)
                {
                    yield return null;
                    steelSeprationInterval = 0;
                }
            }
            //Debug.Log("Finished mesh separation for steel at: " + System.DateTime.Now);

            int brickSeprationInterval = 0;
            //Debug.Log("Started mesh separation for brick at: " + System.DateTime.Now);
            foreach (GameObject obj in bricks)
            {
                Transform[] brickBlocks = bricks[brickCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform b in brickBlocks)
                {
                    if (b != null)
                    {
                        float distance = Vector3.Distance(b.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            b.gameObject.SetActive(true);
                            b.parent = builtObjects.transform;
                            totalBrick++;
                        }
                        if (distance < 20 && type.Equals("brick"))
                        {
                            b.gameObject.SetActive(true);
                            b.parent = builtObjects.transform; ;
                            totalBrick++;
                        }
                        if (distance < 10 && type.Equals("brick"))
                        {
                            b.gameObject.GetComponent<PhysicsHandler>().Explode();
                            totalBrick++;
                        }
                    }
                }
                brickCount++;
                brickSeprationInterval++;
                if (brickSeprationInterval >= 50)
                {
                    yield return null;
                    brickSeprationInterval = 0;
                }
            }
            //Debug.Log("Finished mesh separation for brick at: " + System.DateTime.Now);

            ironCount = 0;
            steelCount = 0;
            glassCount = 0;
            brickCount = 0;
            if (totalIron > 0)
            {
                //Debug.Log("Started iron dummy creation: " + System.DateTime.Now);
                foreach (GameObject obj in ironBlocks)
                {
                    ironBlocksDummy[ironCount] = Instantiate(ironHolder, transform.position, transform.rotation);
                    ironBlocksDummy[ironCount].transform.parent = builtObjects.transform;
                    ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                //Debug.Log("Finished iron dummy creation interval at: " + System.DateTime.Now);
                ironMeshRequired = true;
                waitingForDestroy = true;
            }
            if (totalGlass > 0)
            {
                //Debug.Log("Started glass dummy creation at: " + System.DateTime.Now);
                foreach (GameObject obj in glass)
                {
                    glassDummy[glassCount] = Instantiate(glassHolder, transform.position, transform.rotation);
                    glassDummy[glassCount].transform.parent = builtObjects.transform;
                    glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        glassDummy[glassCount].GetComponent<MeshFilter>().mesh = glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                //Debug.Log("Finished glass dummy creation interval at: " + System.DateTime.Now);
                glassMeshRequired = true;
                waitingForDestroy = true;
            }
            if (totalSteel > 0)
            {
                //Debug.Log("Started steel dummy creation at: " + System.DateTime.Now);
                foreach (GameObject obj in steel)
                {
                    steelDummy[steelCount] = Instantiate(steelHolder, transform.position, transform.rotation);
                    steelDummy[steelCount].transform.parent = builtObjects.transform;
                    steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        steelDummy[steelCount].GetComponent<MeshFilter>().mesh = steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                //Debug.Log("Finished steel dummy creation interval at: " + System.DateTime.Now);
                steelMeshRequired = true;
                waitingForDestroy = true;
            }
            if (totalBrick > 0)
            {
                //Debug.Log("Started brick dummy creation interval at: " + System.DateTime.Now);
                foreach (GameObject obj in bricks)
                {
                    bricksDummy[brickCount] = Instantiate(brickHolder, transform.position, transform.rotation);
                    bricksDummy[brickCount].transform.parent = builtObjects.transform;
                    bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                //Debug.Log("Finished brick dummy creation interval at: " + System.DateTime.Now);
                brickMeshRequired = true;
                waitingForDestroy = true;
            }

            if (waitingForDestroy == false)
            {
                working = false;
            }
        }
    }

    public void CombineBlocks()
    {
        if (working == false && exiting == false)
        {
            //Debug.Log("COMBINING IRON BLOCKS");
            working = true;
            int ironCount = 0;
            int steelCount = 0;
            int glassCount = 0;
            int brickCount = 0;
            int ironBlockCount = 0;
            int steelBlockCount = 0;
            int glassBlockCount = 0;
            int brickBlockCount = 0;
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetComponent<IronBlock>() != null)
                {
                    Transform[] blocks = ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        ironCount++;
                    }
                    if (obj.GetComponent<PhysicsHandler>().falling == false && obj.GetComponent<PhysicsHandler>().fallingStack == false && obj.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        obj.transform.parent = ironBlocks[ironCount].transform;
                        if (initIron == false)
                        {
                            //UnityEngine.Debug.Log("CREATING IRON BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = obj.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = obj.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "iron";
                        }
                        ironBlockCount++;
                    }
                }
            }
            //Debug.Log("COMBINING GLASS");
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetComponent<Glass>() != null)
                {
                    Transform[] blocks = glass[glassCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        glassCount++;
                    }
                    if (obj.GetComponent<PhysicsHandler>().falling == false && obj.GetComponent<PhysicsHandler>().fallingStack == false && obj.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        obj.transform.parent = glass[glassCount].transform;
                        if (initGlass == false)
                        {
                            //UnityEngine.Debug.Log("CREATING GLASS BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = obj.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = obj.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "glass";
                        }
                        glassBlockCount++;
                    }
                }
            }
            //Debug.Log("COMBINING STEEL");
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetComponent<Steel>() != null)
                {
                    Transform[] blocks = steel[steelCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        steelCount++;
                    }
                    if (obj.GetComponent<PhysicsHandler>().falling == false && obj.GetComponent<PhysicsHandler>().fallingStack == false && obj.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        obj.transform.parent = steel[steelCount].transform;
                        if (initSteel == false)
                        {
                            //UnityEngine.Debug.Log("CREATING STEEL BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = obj.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = obj.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "steel";
                        }
                        steelBlockCount++;
                    }
                }
            }
            //Debug.Log("COMBINING BRICKS");
            foreach (GameObject obj in allObjects)
            {
                if (obj.GetComponent<Brick>() != null)
                {
                    Transform[] blocks = bricks[brickCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        brickCount++;
                    }
                    if (obj.GetComponent<PhysicsHandler>().falling == false && obj.GetComponent<PhysicsHandler>().fallingStack == false && obj.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        obj.transform.parent = bricks[brickCount].transform;
                        if (initBrick == false)
                        {
                            //UnityEngine.Debug.Log("CREATING BRICK BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = obj.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = obj.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "brick";
                        }
                        brickBlockCount++;
                    }
                }
            }

            if (ironBlockCount > 0)
            {
                ironCount = 0;
                foreach (GameObject obj in ironBlocks)
                {
                    ironBlocksDummy[ironCount] = Instantiate(ironHolder, transform.position, transform.rotation);
                    ironBlocksDummy[ironCount].transform.parent = builtObjects.transform;
                    ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                ironMeshRequired = true;
                waitingForDestroy = true;
            }

            if (steelBlockCount > 0)
            {
                steelCount = 0;
                foreach (GameObject obj in steel)
                {
                    steelDummy[steelCount] = Instantiate(steelHolder, transform.position, transform.rotation);
                    steelDummy[steelCount].transform.parent = builtObjects.transform;
                    steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        steelDummy[steelCount].GetComponent<MeshFilter>().mesh = steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                steelMeshRequired = true;
                waitingForDestroy = true;
            }

            if (glassBlockCount > 0)
            {
                glassCount = 0;
                foreach (GameObject obj in glass)
                {
                    glassDummy[glassCount] = Instantiate(glassHolder, transform.position, transform.rotation);
                    glassDummy[glassCount].transform.parent = builtObjects.transform;
                    glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        glassDummy[glassCount].GetComponent<MeshFilter>().mesh = glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                glassMeshRequired = true;
                waitingForDestroy = true;
            }

            if (brickBlockCount > 0)
            {
                brickCount = 0;
                foreach (GameObject obj in bricks)
                {
                    bricksDummy[brickCount] = Instantiate(brickHolder, transform.position, transform.rotation);
                    bricksDummy[brickCount].transform.parent = builtObjects.transform;
                    bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Destroy(bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                brickMeshRequired = true;
                waitingForDestroy = true;
            }

            blocksCombined = true;
            if (waitingForDestroy == false)
            {
                working = false;
            }
        }
    }

    void CombineMeshes()
    {
        combineCoroutine = StartCoroutine(CombineMeshCoroutine());
    }

    IEnumerator CombineMeshCoroutine()
    {
        if (ironMeshRequired == true)
        {
            int ironCount = 0;
            int ironCombineInterval = 0;
            //Debug.Log("Started mesh combine for iron at: " + System.DateTime.Now);
            foreach (GameObject obj in ironBlocks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] meshFilters = ironBlocks[ironCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in meshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                meshFilters = mfList.ToArray();
                CombineInstance[] combine = new CombineInstance[meshFilters.Length];
                int i = 0;
                while (i < meshFilters.Length)
                {
                    combine[i].mesh = meshFilters[i].sharedMesh;
                    combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                    meshFilters[i].gameObject.SetActive(false);
                    i++;
                }
                if (ironBlocks[ironCount].GetComponent<MeshFilter>() == null)
                {
                    ironBlocks[ironCount].AddComponent<MeshFilter>();
                }
                ironBlocks[ironCount].GetComponent<MeshFilter>().mesh = new Mesh();
                ironBlocks[ironCount].GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                ironBlocks[ironCount].GetComponent<MeshCollider>().sharedMesh = ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                ironBlocks[ironCount].GetComponent<MeshCollider>().enabled = true;
                if (meshFilters.Length > 0)
                {
                    ironBlocks[ironCount].SetActive(true);
                }
                ironCount++;
                ironCombineInterval++;
                if (ironCombineInterval >= 50)
                {
                    yield return null;
                    ironCombineInterval = 0;
                }
            }
            ironMeshRequired = false;
            if (initIron == false)
            {
                initIron = true;
                clearIronDummies = true;
                PlayerPrefsX.SetBool(GetComponent<StateManager>().WorldName + "initIron", true);
            }
            //Debug.Log("Finished mesh combine for iron at: " + System.DateTime.Now);
        }

        if (glassMeshRequired == true)
        {
            int glassCount = 0;
            int glassCombineInterval = 0;
            //Debug.Log("Started mesh combine for glass at: " + System.DateTime.Now);
            foreach (GameObject obj in glass)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] glassMeshFilters = glass[glassCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in glassMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                glassMeshFilters = mfList.ToArray();
                CombineInstance[] glassCombine = new CombineInstance[glassMeshFilters.Length];
                int g = 0;
                while (g < glassMeshFilters.Length)
                {
                    glassCombine[g].mesh = glassMeshFilters[g].sharedMesh;
                    glassCombine[g].transform = glassMeshFilters[g].transform.localToWorldMatrix;
                    glassMeshFilters[g].gameObject.SetActive(false);
                    g++;
                }
                if (glass[glassCount].GetComponent<MeshFilter>() == null)
                {
                    glass[glassCount].AddComponent<MeshFilter>();
                }
                glass[glassCount].GetComponent<MeshFilter>().mesh = new Mesh();
                glass[glassCount].GetComponent<MeshFilter>().mesh.CombineMeshes(glassCombine);
                glass[glassCount].GetComponent<MeshCollider>().sharedMesh = glass[glassCount].GetComponent<MeshFilter>().mesh;
                glass[glassCount].GetComponent<MeshCollider>().enabled = true;
                if (glassMeshFilters.Length > 0)
                {
                    glass[glassCount].SetActive(true);
                }
                glassCount++;
                glassCombineInterval++;
                if (glassCombineInterval >= 50)
                {
                    yield return null;
                    glassCombineInterval = 0;
                }
            }
            glassMeshRequired = false;
            if (initGlass == false)
            {
                initGlass = true;
                clearGlassDummies = true;
                PlayerPrefsX.SetBool(GetComponent<StateManager>().WorldName + "initGlass", true);
            }
            //Debug.Log("Finished mesh combine for glass at: " + System.DateTime.Now);
        }

        if (steelMeshRequired == true)
        {
            int steelCount = 0;
            int steelCombineInterval = 0;
            //Debug.Log("Started mesh combine for steel at: " + System.DateTime.Now);
            foreach (GameObject obj in steel)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] steelMeshFilters = steel[steelCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in steelMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                steelMeshFilters = mfList.ToArray();
                CombineInstance[] steelCombine = new CombineInstance[steelMeshFilters.Length];
                int s = 0;
                while (s < steelMeshFilters.Length)
                {
                    steelCombine[s].mesh = steelMeshFilters[s].sharedMesh;
                    steelCombine[s].transform = steelMeshFilters[s].transform.localToWorldMatrix;
                    steelMeshFilters[s].gameObject.SetActive(false);
                    s++;
                }
                if (steel[steelCount].GetComponent<MeshFilter>() == null)
                {
                    steel[steelCount].AddComponent<MeshFilter>();
                }
                steel[steelCount].GetComponent<MeshFilter>().mesh = new Mesh();
                steel[steelCount].GetComponent<MeshFilter>().mesh.CombineMeshes(steelCombine);
                steel[steelCount].GetComponent<MeshCollider>().sharedMesh = steel[steelCount].GetComponent<MeshFilter>().mesh;
                steel[steelCount].GetComponent<MeshCollider>().enabled = true;
                if (steelMeshFilters.Length > 0)
                {
                    steel[steelCount].SetActive(true);
                }
                steelCount++;
                steelCombineInterval++;
                if (steelCombineInterval >= 50)
                {
                    yield return null;
                    steelCombineInterval = 0;
                }
            }
            steelMeshRequired = false;
            if (initSteel == false)
            {
                initSteel = true;
                clearSteelDummies = true;
                PlayerPrefsX.SetBool(GetComponent<StateManager>().WorldName + "initSteel", true);
            }
            //Debug.Log("Finished mesh combine for steel at: " + System.DateTime.Now);
        }

        if (brickMeshRequired == true)
        {
            int brickCount = 0;
            int brickCombineInterval = 0;
            //Debug.Log("Started mesh combine for bricks at: " + System.DateTime.Now);
            foreach (GameObject obj in bricks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] brickMeshFilters = bricks[brickCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in brickMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                brickMeshFilters = mfList.ToArray();
                CombineInstance[] brickCombine = new CombineInstance[brickMeshFilters.Length];
                int b = 0;
                while (b < brickMeshFilters.Length)
                {
                    brickCombine[b].mesh = brickMeshFilters[b].sharedMesh;
                    brickCombine[b].transform = brickMeshFilters[b].transform.localToWorldMatrix;
                    brickMeshFilters[b].gameObject.SetActive(false);
                    b++;
                }
                if (bricks[brickCount].GetComponent<MeshFilter>() == null)
                {
                    bricks[brickCount].AddComponent<MeshFilter>();
                }
                bricks[brickCount].GetComponent<MeshFilter>().mesh = new Mesh();
                bricks[brickCount].GetComponent<MeshFilter>().mesh.CombineMeshes(brickCombine);
                bricks[brickCount].GetComponent<MeshCollider>().sharedMesh = bricks[brickCount].GetComponent<MeshFilter>().mesh;
                bricks[brickCount].GetComponent<MeshCollider>().enabled = true;
                if (brickMeshFilters.Length > 0)
                {
                    bricks[brickCount].SetActive(true);
                }
                brickCount++;
                brickCombineInterval++;
                if (brickCombineInterval >= 50)
                {
                    yield return null;
                    brickCombineInterval = 0;
                }
            }
            brickMeshRequired = false;
            if (initBrick == false)
            {
                initBrick = true;
                clearBrickDummies = true;
                PlayerPrefsX.SetBool(GetComponent<StateManager>().WorldName + "initBrick", true);
            }
            //Debug.Log("Finished mesh combine for bricks at: " + System.DateTime.Now);
        }

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int dummyDestroyInterval = 0;
        //Debug.Log("Started destruction of dummy objects at: "+System.DateTime.Now);
        foreach (GameObject obj in allObjects)
        {
            if (obj != null)
            {
                if (obj.GetComponent<HolderDummy>() != null)
                {
                    Destroy(obj);
                    dummyDestroyInterval++;
                    if (dummyDestroyInterval >= 50)
                    {
                        yield return null;
                        dummyDestroyInterval = 0;
                    }
                }
            }
        }
        working = false;
        //Debug.Log("Finished destruction of dummy objects at: " + System.DateTime.Now);
        //UnityEngine.Debug.Log("Glass: " + initGlass + " Iron: " + initIron + " Steel: " + initSteel + " Brick: " + initBrick);
    }
}

