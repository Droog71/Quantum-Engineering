using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//! This class handles unique ID assignment and saving & loading of worlds.
public class StateManager : MonoBehaviour
{
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded;
    public bool initMachines;
    public bool finalMachineAddress;
    public bool finalBlockAddress;
    public int progress;
    public int totalMachines;
    public int currentMachine;
    public int[] machineIdList;
    public int[] blockIdList;
    public List<string> modTextureList;
    public List<string> modRecipeList;
    public GameObject darkMatterCollector;
    public GameObject darkMatterConduit;
    public GameObject ironBlock;
    public GameObject ironRamp;
    public GameObject steel;
    public GameObject steelRamp;
    public GameObject storageContainer;
    public GameObject glass;
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
    public GameObject brick;
    public GameObject modMachine;
    public GameObject protectionBlock;
    public GameObject modBlock;
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
    private Coroutine machineIdCoroutine;
    private Coroutine blockIdCoroutine;
    private Coroutine loadCoroutine;
    private Coroutine saveCoroutine;
    private string objectName = "";
    private bool loading;
    private int batchmodeLogInterval;

    //! Called by unity engine before the first update.
    public void Start()
    {
        saveManager = new SaveManager(this);
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        mainMenu = player.GetComponent<MainMenu>();
    }

    //! Update is called once per frame.
    public void Update()
    {
        if (mainMenu != null && playerController != null)
        {
            if (mainMenu.worldSelected == true && playerController.addedModBlocks == true && loading == false)
            {
                loadCoroutine = StartCoroutine(LoadWorld());
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
                        int idTotal = machineIdList.Length + blockIdList.Length;
                        string loadingMessage = "Loading... " + progress + "/" + idTotal;
                        if (progress > 0 && progress >= idTotal)
                        {
                            loadingMessage = "Initializing... " + currentMachine + "/" + totalMachines;
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
    private IEnumerator LoadWorld()
    {
        if (worldLoaded == false)
        {
            machineIdList = PlayerPrefsX.GetIntArray(worldName + "machineIdList");
            blockIdList = PlayerPrefsX.GetIntArray(worldName + "blockIdList");

            if (machineIdList.Length > 0)
            {
                int loadInterval = 0;
                progress = 0;
                foreach (int objectID in machineIdList)
                {
                    objectPosition = PlayerPrefsX.GetVector3(worldName + "machine" + objectID + "Position");
                    objectRotation = PlayerPrefsX.GetQuaternion(worldName + "machine" + objectID + "Rotation");
                    objectName = FileBasedPrefs.GetString(worldName + "machine" + objectID + "Name");
                    string ID = objectName + objectID;
                    if (objectName != "" && objectPosition != emptyVector)
                    {
                        if (objectName == worldName + "Door")
                        {
                            GameObject SpawnedObject = Instantiate(door, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Door>().ID = ID;
                            SpawnedObject.GetComponent<Door>().audioClip = FileBasedPrefs.GetInt(ID+ "audioClip");
                            SpawnedObject.GetComponent<Door>().textureIndex = FileBasedPrefs.GetInt(ID+ "textureIndex");
                            SpawnedObject.GetComponent<Door>().material = FileBasedPrefs.GetString(ID+ "material");
                            SpawnedObject.GetComponent<Door>().edited = FileBasedPrefs.GetBool(ID+ "edited");
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "QuantumHatchway")
                        {
                            GameObject SpawnedObject = Instantiate(quantumHatchway, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Door>().ID = ID;
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "DarkMatterCollector")
                        {
                            GameObject SpawnedObject = Instantiate(darkMatterCollector, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<DarkMatterCollector>().ID = ID;
                            SpawnedObject.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(ID+ "darkMatterAmount");
                            SpawnedObject.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "DarkMatterConduit")
                        {
                            GameObject SpawnedObject = Instantiate(darkMatterConduit, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<DarkMatterConduit>().ID = ID;
                            SpawnedObject.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            SpawnedObject.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(ID+ "darkMatterAmount");
                            SpawnedObject.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "RailCartHub")
                        {
                            GameObject SpawnedObject = Instantiate(railCartHub, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<RailCartHub>().ID = ID;
                            SpawnedObject.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(ID+ "range");
                            SpawnedObject.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(ID+ "stop");
                            SpawnedObject.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(ID+ "circuit");
                            SpawnedObject.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(ID+ "stopTime");
                            SpawnedObject.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(ID+ "centralHub");
                            SpawnedObject.GetComponent<RailCartHub>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "RailCart")
                        {
                            GameObject SpawnedObject = Instantiate(railCart, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<RailCart>().ID = ID;
                            SpawnedObject.GetComponent<RailCart>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(ID+ "targetID");
                            SpawnedObject.GetComponent<RailCart>().startPosition = PlayerPrefsX.GetVector3(ID+ "startPosition");
                        }
                        if (objectName == worldName + "UniversalConduit")
                        {
                            GameObject SpawnedObject = Instantiate(universalConduit, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<UniversalConduit>().ID = ID;
                            SpawnedObject.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(ID+ "type");
                            SpawnedObject.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            SpawnedObject.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<UniversalConduit>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Retriever")
                        {
                            GameObject SpawnedObject = Instantiate(retriever, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Retriever>().ID = ID;
                            SpawnedObject.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<Retriever>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "AutoCrafter")
                        {
                            GameObject SpawnedObject = Instantiate(autoCrafter, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<AutoCrafter>().ID = ID;
                            SpawnedObject.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<AutoCrafter>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Smelter")
                        {
                            GameObject SpawnedObject = Instantiate(smelter, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Smelter>().ID = ID;
                            SpawnedObject.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<Smelter>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "HeatExchanger")
                        {
                            GameObject SpawnedObject = Instantiate(heatExchanger, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<HeatExchanger>().ID = ID;
                            SpawnedObject.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<HeatExchanger>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "SolarPanel")
                        {
                            GameObject SpawnedObject = Instantiate(solarPanel, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<PowerSource>().ID = ID;
                            SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Generator")
                        {
                            GameObject SpawnedObject = Instantiate(generator, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<PowerSource>().ID = ID;
                            SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(ID+ "fuelType");
                            SpawnedObject.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(ID+ "fuelAmount");
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "NuclearReactor")
                        {
                            GameObject SpawnedObject = Instantiate(nuclearReactor, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<NuclearReactor>().ID = ID;
                            SpawnedObject.GetComponent<NuclearReactor>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "ReactorTurbine")
                        {
                            GameObject SpawnedObject = Instantiate(reactorTurbine, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<PowerSource>().ID = ID;
                            SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "PowerConduit")
                        {
                            GameObject SpawnedObject = Instantiate(powerConduit, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<PowerConduit>().ID = ID;
                            SpawnedObject.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(ID+ "outputID1");
                            SpawnedObject.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(ID+ "outputID2");
                            SpawnedObject.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(ID+ "dualOutput");
                            SpawnedObject.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(ID+ "range");
                            SpawnedObject.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(ID+ "powerAmount");
                            SpawnedObject.GetComponent<PowerConduit>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Auger")
                        {
                            GameObject SpawnedObject = Instantiate(auger, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Auger>().ID = ID;
                            SpawnedObject.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<Auger>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "ElectricLight")
                        {
                            GameObject SpawnedObject = Instantiate(electricLight, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<ElectricLight>().ID = ID;
                            SpawnedObject.GetComponent<ElectricLight>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Turret")
                        {
                            GameObject SpawnedObject = Instantiate(turret, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Turret>().ID = ID;
                            SpawnedObject.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Turret>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "MissileTurret")
                        {
                            GameObject SpawnedObject = Instantiate(missileTurret, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<MissileTurret>().ID = ID;
                            SpawnedObject.GetComponent<MissileTurret>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<MissileTurret>().ammoType = FileBasedPrefs.GetString(ID+ "ammoType");
                            SpawnedObject.GetComponent<MissileTurret>().ammoAmount = FileBasedPrefs.GetInt(ID+ "ammoAmount");
                            SpawnedObject.GetComponent<MissileTurret>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "AlloySmelter")
                        {
                            GameObject SpawnedObject = Instantiate(alloySmelter, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<AlloySmelter>().ID = ID;
                            SpawnedObject.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(ID+ "inputID1");
                            SpawnedObject.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(ID+ "inputID2");
                            SpawnedObject.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(ID+ "inputType1");
                            SpawnedObject.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(ID+ "inputType2");
                            SpawnedObject.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(ID+ "amount2");
                            SpawnedObject.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(ID+ "outputAmount");
                            SpawnedObject.GetComponent<AlloySmelter>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Press")
                        {
                            GameObject SpawnedObject = Instantiate(press, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Press>().ID = ID;
                            SpawnedObject.GetComponent<Press>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<Press>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<Press>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<Press>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<Press>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<Press>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Extruder")
                        {
                            GameObject SpawnedObject = Instantiate(extruder, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Extruder>().ID = ID;
                            SpawnedObject.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<Extruder>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "GearCutter")
                        {
                            GameObject SpawnedObject = Instantiate(gearCutter, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<GearCutter>().ID = ID;
                            SpawnedObject.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<GearCutter>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "UniversalExtractor")
                        {
                            GameObject SpawnedObject = Instantiate(universalExtractor, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<UniversalExtractor>().ID = ID;
                            SpawnedObject.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(ID+ "type");
                            SpawnedObject.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "StorageContainer")
                        {
                            GameObject SpawnedObject = Instantiate(storageContainer, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<InventoryManager>().ID = ID;
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "StorageComputer")
                        {
                            GameObject SpawnedObject = Instantiate(storageComputer, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<StorageComputer>().ID = ID;
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "ModMachine")
                        {
                            GameObject SpawnedObject = Instantiate(modMachine, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<ModMachine>().ID = ID;
                            SpawnedObject.GetComponent<ModMachine>().machineName = FileBasedPrefs.GetString(ID+ "machineName");
                            SpawnedObject.GetComponent<ModMachine>().inputID = FileBasedPrefs.GetString(ID+ "inputID");
                            SpawnedObject.GetComponent<ModMachine>().outputID = FileBasedPrefs.GetString(ID+ "outputID");
                            SpawnedObject.GetComponent<ModMachine>().inputType = FileBasedPrefs.GetString(ID+ "inputType");
                            SpawnedObject.GetComponent<ModMachine>().outputType = FileBasedPrefs.GetString(ID+ "outputType");
                            SpawnedObject.GetComponent<ModMachine>().speed = FileBasedPrefs.GetInt(ID+ "speed");
                            SpawnedObject.GetComponent<ModMachine>().amount = FileBasedPrefs.GetFloat(ID+ "amount");
                            SpawnedObject.GetComponent<ModMachine>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                        }
                        if (objectName == worldName + "ProtectionBlock")
                        {
                            GameObject SpawnedObject = Instantiate(protectionBlock, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<ProtectionBlock>().ID = ID;
                            SpawnedObject.GetComponent<ProtectionBlock>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<ProtectionBlock>().SetUserNames(PlayerPrefsX.GetStringArray(ID+ "userNames").ToList());
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                        }
                    }
                    progress++;
                    loadInterval++;
                    if (loadInterval >= machineIdList.Length * 0.025f)
                    {
                        loadInterval = 0;
                        yield return null;
                    }
                }
            }

            if (blockIdList.Length > 0)
            {
                int loadInterval = 0;
                foreach (int objectID in blockIdList)
                {
                    while (GetComponent<GameManager>().working == true)
                    {
                        yield return null;
                    }
                    objectPosition = PlayerPrefsX.GetVector3(worldName + "block" + objectID + "Position");
                    objectRotation = PlayerPrefsX.GetQuaternion(worldName + "block" + objectID + "Rotation");
                    objectName = FileBasedPrefs.GetString(worldName + "block" + objectID + "Name");
                    string ID = objectName + objectID;
                    if (objectName != "" && objectPosition != emptyVector)
                    {
                        if (objectName == worldName + "IronBlock")
                        {
                            GameObject SpawnedObject = Instantiate(ironBlock, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<IronBlock>().ID = ID;
                            SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "IronRamp")
                        {
                            GameObject SpawnedObject = Instantiate(ironRamp, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<IronBlock>().ID = ID;
                            SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Steel")
                        {
                            GameObject SpawnedObject = Instantiate(steel, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Steel>().ID = ID;
                            SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "SteelRamp")
                        {
                            GameObject SpawnedObject = Instantiate(steelRamp, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Steel>().ID = ID;
                            SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Brick")
                        {
                            GameObject SpawnedObject = Instantiate(brick, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Brick>().ID = ID;
                            SpawnedObject.GetComponent<Brick>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "Glass")
                        {
                            GameObject SpawnedObject = Instantiate(glass, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<Glass>().ID = ID;
                            SpawnedObject.GetComponent<Glass>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        }
                        if (objectName == worldName + "ModBlock")
                        {
                            GameObject SpawnedObject = Instantiate(modBlock, objectPosition, objectRotation);
                            SpawnedObject.GetComponent<ModBlock>().ID = ID;
                            string blockName = FileBasedPrefs.GetString(ID+ "blockName");
                            SpawnedObject.GetComponent<ModBlock>().blockName = blockName;
                            BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
                            if (blockDictionary.meshDictionary.ContainsKey(blockName))
                            {
                                SpawnedObject.GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[blockName];
                            }
                            GetComponent<GameManager>().meshManager.SetMaterial(SpawnedObject, blockName);
                            SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                            SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ID+ "falling");
                            SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ID+ "fallingStack");
                        }
                    }
                    progress++;
                    loadInterval++;
                    if (loadInterval >= blockIdList.Length * 0.025f)
                    {
                        loadInterval = 0;
                        GetComponent<GameManager>().meshManager.CombineBlocks();
                        yield return null;
                    }
                }
            }
        }
        GetComponent<GameManager>().meshManager.CombineBlocks();
        totalMachines = machineIdList.Length;
        float simSpeed = GetComponent<GameManager>().simulationSpeed;
        GetComponent<GameManager>().simulationSpeed = 0.1f;
        initMachines = true;
        for (currentMachine = 0; currentMachine < totalMachines; currentMachine++)
        {
            yield return new WaitForSeconds(0.06f);
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
                    machineIdCoroutine = StartCoroutine(addressManager.MachineIdCoroutine());
                }
                if (worldLoaded == true && addressManager.blockIdCoroutineActive == false)
                {
                    blockIdCoroutine = StartCoroutine(addressManager.BlockIdCoroutine());
                }
            }
        }
        else
        {
            finalMachineAddress = false;
            finalBlockAddress = false;
            if (initMachines == true && addressManager.machineIdCoroutineActive == false)
            {
                machineIdCoroutine = StartCoroutine(addressManager.MachineIdCoroutine());
            }
            if (worldLoaded == true && addressManager.blockIdCoroutineActive == false)
            {
                blockIdCoroutine = StartCoroutine(addressManager.BlockIdCoroutine());
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
            saveCoroutine = StartCoroutine(saveManager.SaveDataCoroutine());
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