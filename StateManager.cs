using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using MEC;

//! This class handles unique ID assignment and saving & loading of worlds.
public class StateManager : MonoBehaviour
{
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded;
    public bool initMachines;
    public bool finalMachineAddress;
    public bool finalBlockAddress;
    public int machineProgress;
    public int blockProgress;
    public int totalMachines;
    public int currentMachine;
    public int[] machineIdList;
    public int[] blockIdList;
    public List<string> modTextureList;
    public List<string> modRecipeList;
    public GameObject darkMatterCollector;
    public GameObject darkMatterConduit;
    public GameObject storageContainer;
    public GameObject universalExtractor;
    public GameObject universalConduit;
    public GameObject powerConduit;
    public GameObject smelter;
    public GameObject press;
    public GameObject gearCutter;
    public GameObject alloySmelter;
    public GameObject solarPanel;
    public GameObject generator;
    public GameObject extruder;
    public GameObject turret;
    public GameObject missileTurret;
    public GameObject auger;
    public GameObject heatExchanger;
    public GameObject electricLight;
    public GameObject retriever;
    public GameObject door;
    public GameObject quantumHatchway;
    public GameObject nuclearReactor;
    public GameObject reactorTurbine;
    public GameObject storageComputer;
    public GameObject autoCrafter;
    public GameObject railCartHub;
    public GameObject railCart;
    public GameObject modMachine;
    public GameObject protectionBlock;
    public GameObject block;
    public GameObject blockHolder;
    public GameObject builtObjects;
    public string worldName = "World";
    public string partName = "";
    public SaveManager saveManager;
    public Vector3 partPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Quaternion partRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    private GameObject player;
    private PlayerController playerController;
    private MainMenu mainMenu;
    private Vector3 emptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 objectPosition;
    private Quaternion objectRotation;
    private AddressManager addressManager;
    private MachineManager machineManager;
    private string objectName = "";
    private bool loading;
    private int batchmodeLogInterval;
    public int totalBlocks;
    public int currentBlocks;

    //! Called by unity engine before the first update.
    public void Start()
    {
        saveManager = new SaveManager(this);
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        mainMenu = player.GetComponent<MainMenu>();
        machineManager = GetComponent<MachineManager>();
    }

    //! Update is called once per frame.
    public void Update()
    {
        if (mainMenu != null && playerController != null)
        {
            if (mainMenu.worldSelected == true && playerController.addedModBlocks == true && loading == false)
            {
                Timing.RunCoroutine(LoadWorld());
                loading = true;
            }

            string[] commandLineOptions = Environment.GetCommandLineArgs();
            if (commandLineOptions.Contains("-batchmode"))
            {
                if (loading == true && worldLoaded == false)
                {
                    batchmodeLogInterval++;
                    if (batchmodeLogInterval >= 60)
                    {
                        string loadingMessage = "Loading... " + blockProgress + "/" + blockIdList.Length;

                        if (blockProgress > 0 && machineProgress >= blockIdList.Length)
                        {
                            loadingMessage = "Loading machines... " + machineProgress + "/" + machineIdList.Length;
                        }

                        if (blockProgress > 0 && blockProgress >= blockIdList.Length)
                        {
                            loadingMessage = "Initializing machines... " + currentMachine + "/" + totalMachines;
                        }

                        Debug.Log(loadingMessage);
                        batchmodeLogInterval = 0;
                    }
                }
            }

            if (addressManager == null)
            {
                addressManager = new AddressManager(this);
            }

            if (saving == false && worldLoaded == true)
            {
                AssignIDs();
            }
        }
    }

    //! Loads a saved world.
    private IEnumerator<float> LoadWorld()
    {
        if (worldLoaded == false)
        {
            blockIdList = PlayerPrefsX.GetIntArray(worldName + "blockIdList");
            machineIdList = PlayerPrefsX.GetIntArray(worldName + "machineIdList");

            if (blockIdList.Length > 0)
            {
                blockProgress = 0;
                foreach (int objectID in blockIdList)
                {
                    objectPosition = PlayerPrefsX.GetVector3(worldName + "block" + objectID + "Position");
                    objectRotation = PlayerPrefsX.GetQuaternion(worldName + "block" + objectID + "Rotation");
                    objectName = FileBasedPrefs.GetString(worldName + "block" + objectID + "Name");
                    string ID = objectName + objectID;
                    if (objectName == worldName + "BlockHolder")
                    {
                        GameObject spawnedObject = Instantiate(blockHolder, objectPosition, objectRotation);
                        BlockHolder bh = spawnedObject.GetComponent<BlockHolder>();
                        spawnedObject.transform.parent = builtObjects.transform;
                        bh.blockData = new List<BlockHolder.BlockInfo>();
                        bh.ID = ID;
                        string blockType = FileBasedPrefs.GetString(ID + "blockType");
                        bh.blockType = blockType;
                        if (blockType == "Grass" || blockType == "Dirt")
                        {
                            Vector3 worldLoc = PlayerPrefsX.GetVector3(ID + "worldLoc");
                            GetComponent<TerrainGenerator>().chunkLocations.Add(worldLoc);
                        }
                        Vector3[] blockPositions = PlayerPrefsX.GetVector3Array(ID + "blockPositions");
                        if (blockPositions.Length > 0)
                        {
                            totalBlocks = blockPositions.Length;
                            currentBlocks = 0;
                            Quaternion[] blockRotations = PlayerPrefsX.GetQuaternionArray(ID + "blockRotations");
                            for (int i = 0; i < blockPositions.Length; i++)
                            {
                                BlockHolder.BlockInfo blockInfo = new BlockHolder.BlockInfo(blockPositions[i], blockRotations[i]);
                                bh.blockData.Add(blockInfo);
                                currentBlocks++;
                            }
                        }
                        bh.unloaded = true;
                        string saveDataPath = Path.Combine(Application.persistentDataPath, "SaveData" + "/" + worldName);
                        Directory.CreateDirectory(saveDataPath);
                        string saveFileLocation = Path.Combine(saveDataPath + "/" + ID + ".obj");
                        GetComponent<GameManager>().meshManager.SetMaterial(spawnedObject, blockType);
                        bh.Load();
                        while (bh.chunkLoadCoroutineBusy == true || bh.chunkUnloadCoroutineBusy == true)
                        {
                            yield return Timing.WaitForOneFrame;
                        }
                    }
                    blockProgress++;
                }
            }

            if (machineIdList.Length > 0)
            {
                int loadInterval = 0;
                machineProgress = 0;
                foreach (int objectID in machineIdList)
                {
                    objectPosition = PlayerPrefsX.GetVector3(worldName + "machine" + objectID + "Position");
                    objectRotation = PlayerPrefsX.GetQuaternion(worldName + "machine" + objectID + "Rotation");
                    objectName = FileBasedPrefs.GetString(worldName + "machine" + objectID + "Name");
                    string ID = objectName + objectID;
                    if (objectName != "" && objectPosition != emptyVector)
                    {
                        if (objectName == worldName + "LogicBlock")
                        {
                            string blockType = FileBasedPrefs.GetString(ID + "blockType");
                            BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
                            Debug.Log("LOGIC BLOCK: " + blockType);
                            GameObject spawnedObject = Instantiate(blockDictionary.machineDictionary[blockType], objectPosition, objectRotation);
                            spawnedObject.GetComponent<LogicBlock>().logic = PlayerPrefsX.GetBool(ID + "logic");
                            machineManager.machines.Add(spawnedObject.GetComponent<LogicBlock>());
                        }
                        if (objectName == worldName + "Door")
                        {
                            GameObject spawnedObject = Instantiate(door, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Door>().ID = ID;
                            spawnedObject.GetComponent<Door>().audioClip = FileBasedPrefs.GetInt(ID+ "audioClip");
                            spawnedObject.GetComponent<Door>().textureIndex = FileBasedPrefs.GetInt(ID+ "textureIndex");
                            spawnedObject.GetComponent<Door>().material = FileBasedPrefs.GetString(ID+ "material");
                            spawnedObject.GetComponent<Door>().edited = FileBasedPrefs.GetBool(ID+ "edited");
                            machineManager.machines.Add(spawnedObject.GetComponent<Door>());
                        }
                        if (objectName == worldName + "QuantumHatchway")
                        {
                            GameObject spawnedObject = Instantiate(quantumHatchway, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Door>().ID = ID;
                            machineManager.machines.Add(spawnedObject.GetComponent<Door>());
                        }
                        if (objectName == worldName + "DarkMatterCollector")
                        {
                            GameObject spawnedObject = Instantiate(darkMatterCollector, objectPosition, objectRotation);
                            spawnedObject.GetComponent<DarkMatterCollector>().ID = ID;
                            spawnedObject.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(ID+ "darkMatterAmount");
                            spawnedObject.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<DarkMatterCollector>());
                        }
                        if (objectName == worldName + "DarkMatterConduit")
                        {
                            GameObject spawnedObject = Instantiate(darkMatterConduit, objectPosition, objectRotation);
                            spawnedObject.GetComponent<DarkMatterConduit>().ID = ID;
                            spawnedObject.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            spawnedObject.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(ID+ "darkMatterAmount");
                            spawnedObject.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<DarkMatterConduit>());
                        }
                        if (objectName == worldName + "RailCartHub")
                        {
                            GameObject spawnedObject = Instantiate(railCartHub, objectPosition, objectRotation);
                            spawnedObject.GetComponent<RailCartHub>().ID = ID;
                            spawnedObject.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(ID+ "range");
                            spawnedObject.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(ID+ "stop");
                            spawnedObject.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(ID+ "circuit");
                            spawnedObject.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(ID+ "stopTime");
                            spawnedObject.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(ID+ "centralHub");
                            spawnedObject.GetComponent<RailCartHub>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<RailCartHub>());
                        }
                        if (objectName == worldName + "RailCart")
                        {
                            GameObject spawnedObject = Instantiate(railCart, objectPosition, objectRotation);
                            spawnedObject.GetComponent<RailCart>().ID = ID;
                            spawnedObject.GetComponent<RailCart>().creationMethod = "spawned";
                            spawnedObject.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(ID+ "targetID");
                            spawnedObject.GetComponent<RailCart>().startPosition = PlayerPrefsX.GetVector3(ID+ "startPosition");
                        }
                        if (objectName == worldName + "UniversalConduit")
                        {
                            GameObject spawnedObject = Instantiate(universalConduit, objectPosition, objectRotation);
                            spawnedObject.GetComponent<UniversalConduit>().ID = ID;
                            spawnedObject.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(ID+ "type");
                            spawnedObject.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            spawnedObject.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<UniversalConduit>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<UniversalConduit>());
                        }
                        if (objectName == worldName + "Retriever")
                        {
                            GameObject spawnedObject = Instantiate(retriever, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Retriever>().ID = ID;
                            spawnedObject.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<Retriever>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Retriever>());
                        }
                        if (objectName == worldName + "AutoCrafter")
                        {
                            GameObject spawnedObject = Instantiate(autoCrafter, objectPosition, objectRotation);
                            spawnedObject.GetComponent<AutoCrafter>().ID = ID;
                            spawnedObject.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<AutoCrafter>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<AutoCrafter>());
                        }
                        if (objectName == worldName + "Smelter")
                        {
                            GameObject spawnedObject = Instantiate(smelter, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Smelter>().ID = ID;
                            spawnedObject.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<Smelter>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Smelter>());
                        }
                        if (objectName == worldName + "HeatExchanger")
                        {
                            GameObject spawnedObject = Instantiate(heatExchanger, objectPosition, objectRotation);
                            spawnedObject.GetComponent<HeatExchanger>().ID = ID;
                            spawnedObject.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<HeatExchanger>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<HeatExchanger>());
                        }
                        if (objectName == worldName + "SolarPanel")
                        {
                            GameObject spawnedObject = Instantiate(solarPanel, objectPosition, objectRotation);
                            spawnedObject.GetComponent<PowerSource>().ID = ID;
                            spawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<PowerSource>());
                        }
                        if (objectName == worldName + "Generator")
                        {
                            GameObject spawnedObject = Instantiate(generator, objectPosition, objectRotation);
                            spawnedObject.GetComponent<PowerSource>().ID = ID;
                            spawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            spawnedObject.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(ID+ "fuelType");
                            spawnedObject.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(ID+ "fuelAmount");
                            machineManager.machines.Add(spawnedObject.GetComponent<PowerSource>());
                        }
                        if (objectName == worldName + "NuclearReactor")
                        {
                            GameObject spawnedObject = Instantiate(nuclearReactor, objectPosition, objectRotation);
                            spawnedObject.GetComponent<NuclearReactor>().ID = ID;
                            spawnedObject.GetComponent<NuclearReactor>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<NuclearReactor>());
                        }
                        if (objectName == worldName + "ReactorTurbine")
                        {
                            GameObject spawnedObject = Instantiate(reactorTurbine, objectPosition, objectRotation);
                            spawnedObject.GetComponent<PowerSource>().ID = ID;
                            spawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<PowerSource>());
                        }
                        if (objectName == worldName + "PowerConduit")
                        {
                            GameObject spawnedObject = Instantiate(powerConduit, objectPosition, objectRotation);
                            spawnedObject.GetComponent<PowerConduit>().ID = ID;
                            spawnedObject.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(ID+ "outputID1");
                            spawnedObject.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(ID+ "outputID2");
                            spawnedObject.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(ID+ "dualOutput");
                            spawnedObject.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            spawnedObject.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(ID+ "powerAmount");
                            spawnedObject.GetComponent<PowerConduit>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<PowerConduit>());
                        }
                        if (objectName == worldName + "Auger")
                        {
                            GameObject spawnedObject = Instantiate(auger, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Auger>().ID = ID;
                            spawnedObject.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<Auger>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Auger>());
                        }
                        if (objectName == worldName + "ElectricLight")
                        {
                            GameObject spawnedObject = Instantiate(electricLight, objectPosition, objectRotation);
                            spawnedObject.GetComponent<ElectricLight>().ID = ID;
                            spawnedObject.GetComponent<ElectricLight>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<ElectricLight>());
                        }
                        if (objectName == worldName + "Turret")
                        {
                            GameObject spawnedObject = Instantiate(turret, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Turret>().ID = ID;
                            spawnedObject.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Turret>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Turret>());
                        }
                        if (objectName == worldName + "MissileTurret")
                        {
                            GameObject spawnedObject = Instantiate(missileTurret, objectPosition, objectRotation);
                            spawnedObject.GetComponent<MissileTurret>().ID = ID;
                            spawnedObject.GetComponent<MissileTurret>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<MissileTurret>().ammoType = FileBasedPrefs.GetString(ID+ "ammoType");
                            spawnedObject.GetComponent<MissileTurret>().ammoAmount = FileBasedPrefs.GetInt(ID+ "ammoAmount");
                            spawnedObject.GetComponent<MissileTurret>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<MissileTurret>());
                        }
                        if (objectName == worldName + "AlloySmelter")
                        {
                            GameObject spawnedObject = Instantiate(alloySmelter, objectPosition, objectRotation);
                            spawnedObject.GetComponent<AlloySmelter>().ID = ID;
                            spawnedObject.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(ID+ "inputID1");
                            spawnedObject.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(ID+ "inputID2");
                            spawnedObject.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(ID+ "inputType1");
                            spawnedObject.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(ID+ "inputType2");
                            spawnedObject.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(ID+ "amount2");
                            spawnedObject.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(ID+ "outputAmount");
                            spawnedObject.GetComponent<AlloySmelter>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<AlloySmelter>());
                        }
                        if (objectName == worldName + "Press")
                        {
                            GameObject spawnedObject = Instantiate(press, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Press>().ID = ID;
                            spawnedObject.GetComponent<Press>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<Press>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<Press>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<Press>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<Press>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<Press>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Press>());
                        }
                        if (objectName == worldName + "Extruder")
                        {
                            GameObject spawnedObject = Instantiate(extruder, objectPosition, objectRotation);
                            spawnedObject.GetComponent<Extruder>().ID = ID;
                            spawnedObject.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<Extruder>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<Extruder>());
                        }
                        if (objectName == worldName + "GearCutter")
                        {
                            GameObject spawnedObject = Instantiate(gearCutter, objectPosition, objectRotation);
                            spawnedObject.GetComponent<GearCutter>().ID = ID;
                            spawnedObject.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<GearCutter>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<GearCutter>());
                        }
                        if (objectName == worldName + "UniversalExtractor")
                        {
                            GameObject spawnedObject = Instantiate(universalExtractor, objectPosition, objectRotation);
                            spawnedObject.GetComponent<UniversalExtractor>().ID = ID;
                            spawnedObject.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(ID+ "type");
                            spawnedObject.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<UniversalExtractor>());
                        }
                        if (objectName == worldName + "StorageContainer")
                        {
                            GameObject spawnedObject = Instantiate(storageContainer, objectPosition, objectRotation);
                            spawnedObject.GetComponent<InventoryManager>().ID = ID;
                        }
                        if (objectName == worldName + "StorageComputer")
                        {
                            GameObject spawnedObject = Instantiate(storageComputer, objectPosition, objectRotation);
                            spawnedObject.GetComponent<StorageComputer>().ID = ID;
                            machineManager.machines.Add(spawnedObject.GetComponent<StorageComputer>());
                        }
                        if (objectName == worldName + "ModMachine")
                        {
                            GameObject spawnedObject = Instantiate(modMachine, objectPosition, objectRotation);
                            spawnedObject.GetComponent<ModMachine>().ID = ID;
                            spawnedObject.GetComponent<ModMachine>().machineName = FileBasedPrefs.GetString(ID+ "machineName");
                            spawnedObject.GetComponent<ModMachine>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            spawnedObject.GetComponent<ModMachine>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            spawnedObject.GetComponent<ModMachine>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            spawnedObject.GetComponent<ModMachine>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            spawnedObject.GetComponent<ModMachine>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            spawnedObject.GetComponent<ModMachine>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            spawnedObject.GetComponent<ModMachine>().creationMethod = "spawned";
                            machineManager.machines.Add(spawnedObject.GetComponent<ModMachine>());
                        }
                        if (objectName == worldName + "ProtectionBlock")
                        {
                            GameObject spawnedObject = Instantiate(protectionBlock, objectPosition, objectRotation);
                            spawnedObject.GetComponent<ProtectionBlock>().ID = ID;
                            spawnedObject.GetComponent<ProtectionBlock>().creationMethod = "spawned";
                            spawnedObject.GetComponent<ProtectionBlock>().SetUserNames(PlayerPrefsX.GetStringArray(ID+ "userNames").ToList());
                            machineManager.machines.Add(spawnedObject.GetComponent<ProtectionBlock>());
                        }
                    }
                    machineProgress++;
                    loadInterval++;
                    if (loadInterval >= machineIdList.Length * 0.1f)
                    {
                        loadInterval = 0;
                        yield return Timing.WaitForOneFrame;
                    }
                }
            }
        }

        totalMachines = machineIdList.Length;
        float simSpeed = GetComponent<GameManager>().simulationSpeed;
        GetComponent<GameManager>().simulationSpeed = 0.1f;
        initMachines = true;
        for (currentMachine = 0; currentMachine < totalMachines; currentMachine++)
        {
            yield return Timing.WaitForSeconds(0.06f);
        }
        GetComponent<GameManager>().simulationSpeed = simSpeed;

        worldLoaded = true;
        string[] commandLineOptions = Environment.GetCommandLineArgs();
        if (commandLineOptions.Contains("-batchmode"))
        {
            Debug.Log("Server running scene " + SceneManager.GetActiveScene().buildIndex + " @ " + PlayerPrefs.GetString("serverURL"));
        }
    }

    //! Assigns ID to objects in the world.
    private void AssignIDs()
    {
        if (GetComponent<GameManager>().dataSaveRequested == true)
        {
            if (finalMachineAddress == false || finalBlockAddress == false)
            {
                if (initMachines == true && addressManager.machineIdCoroutineActive == false)
                {
                    Timing.RunCoroutine(addressManager.MachineIdCoroutine());
                }
                if (worldLoaded == true && addressManager.blockIdCoroutineActive == false)
                {
                    Timing.RunCoroutine(addressManager.BlockIdCoroutine());
                }
            }
        }
        else
        {
            finalMachineAddress = false;
            finalBlockAddress = false;
            if (initMachines == true && addressManager.machineIdCoroutineActive == false)
            {
                Timing.RunCoroutine(addressManager.MachineIdCoroutine());
            }
            if (worldLoaded == true && addressManager.blockIdCoroutineActive == false)
            {
                Timing.RunCoroutine(addressManager.BlockIdCoroutine());
            }
        }
    }

    //! Returns true if the AddressManager class is actively assigning ids.
    public bool AddressManagerBusy()
    {
        return addressManager.machineIdCoroutineActive || addressManager.blockIdCoroutineActive;
    }

    //! Saves the game.
    public void SaveData()
    {
        if (AddressManagerBusy() == false)
            Timing.RunCoroutine(saveManager.SaveDataCoroutine());
    }

    //! Returns true if the object in question is a storage container.
    public bool IsStorageContainer(GameObject go)
    {
        return go.GetComponent<InventoryManager>() != null
        && go.GetComponent<RailCart>() == null
        && go.GetComponent<PlayerController>() == null
        && go.GetComponent<Retriever>() == null
        && go.GetComponent<AutoCrafter>() == null;
    }

    public bool Busy()
    {
        return worldLoaded == false || saving == true;
    }
}