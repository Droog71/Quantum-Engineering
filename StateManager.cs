using UnityEngine;

public class StateManager : MonoBehaviour
{
    //BUILDING
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded;
    public GameObject DarkMatterCollector;
    public GameObject DarkMatterConduit;
    public GameObject IronBlock;
    public GameObject IronRamp;
    public GameObject Steel;
    public GameObject SteelRamp;
    public GameObject StorageContainer;
    public GameObject Glass;
    public GameObject UniversalExtractor;
    public GameObject UniversalConduit;
    public GameObject PowerConduit;
    public GameObject Smelter;
    public GameObject Press;
    public GameObject GearCutter;
    public GameObject AlloySmelter;
    public GameObject SolarPanel;
    public GameObject Generator;
    public GameObject Extruder;
    public GameObject Turret;
    public GameObject Auger;
    public GameObject HeatExchanger;
    public GameObject ElectricLight;
    public GameObject Retriever;
    public GameObject AirLock;
    public GameObject NuclearReactor;
    public GameObject ReactorTurbine;
    public GameObject StorageComputer;
    public GameObject AutoCrafter;
    public GameObject RailCartHub;
    public GameObject RailCart;
    public GameObject Brick;
    public GameObject modMachine;
    public GameObject BuiltObjects;
    public string WorldName = "World";
    private float updateTick;
    private string ObjectName = "";
    public string PartName = "";
    public Vector3 PartPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Quaternion PartRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    public bool Loaded = false;
    public bool assigningIDs;
    private Vector3 EmptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 ObjectPosition;
    private Quaternion ObjectRotation;
    private AddressManager addressManager;
    private SaveManager saveManager;
    private Coroutine addressingCoroutine;
    private Coroutine saveCoroutine;

    // Called by unity engine before the first update.
    public void Start()
    {
        saveManager = new SaveManager(this);
    }

    // Update is called once per frame
    public void Update()
    {
        MainMenu mainMenu = GameObject.Find("Player").GetComponent<MainMenu>();
        if (mainMenu.worldSelected == true && worldLoaded == false)
        {
            LoadWorld();
            worldLoaded = true;
        }
        if (worldLoaded == true)
        {
            if (addressManager == null)
            {
                addressManager = new AddressManager(this);
            }

            updateTick += 1 * Time.deltaTime;
            if (updateTick > 1)
            {
                if (saving == false)
                {
                    AssignIDs();
                }
                updateTick = 0;
            }
        }
    }

    // Loads a saved world.
    private void LoadWorld()
    {
        if (Loaded == false && FileBasedPrefs.GetInt(WorldName + "ObjectTotal") != 0)
        {
            int totalobjectCount = FileBasedPrefs.GetInt(WorldName + "ObjectTotal");
            for (int objectCount = 0; objectCount <= totalobjectCount; objectCount++)
            {
                ObjectPosition = PlayerPrefsX.GetVector3(WorldName + objectCount + "Position");
                ObjectRotation = PlayerPrefsX.GetQuaternion(WorldName + objectCount + "Rotation");
                ObjectName = FileBasedPrefs.GetString(WorldName + objectCount + "Name");
                if (ObjectName != "" && ObjectPosition != EmptyVector)
                {
                    if (ObjectName == WorldName + "AirLock")
                    {
                        GameObject SpawnedObject = Instantiate(AirLock, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterCollector")
                    {
                        GameObject SpawnedObject = Instantiate(DarkMatterCollector, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "darkMatterAmount");
                        SpawnedObject.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterConduit")
                    {
                        GameObject SpawnedObject = Instantiate(DarkMatterConduit, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(ObjectName + objectCount + "range");
                        SpawnedObject.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "darkMatterAmount");
                        SpawnedObject.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCartHub")
                    {
                        GameObject SpawnedObject = Instantiate(RailCartHub, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(ObjectName + objectCount + "range");
                        SpawnedObject.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(ObjectName + objectCount + "stop");
                        SpawnedObject.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(ObjectName + objectCount + "circuit");
                        SpawnedObject.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(ObjectName + objectCount + "stopTime");
                        SpawnedObject.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(ObjectName + objectCount + "centralHub");
                        SpawnedObject.GetComponent<RailCartHub>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCart")
                    {
                        GameObject SpawnedObject = Instantiate(RailCart, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<RailCart>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(ObjectName + objectCount + "targetID");
                    }
                    if (ObjectName == WorldName + "UniversalConduit")
                    {
                        GameObject SpawnedObject = Instantiate(UniversalConduit, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(ObjectName + objectCount + "type");
                        SpawnedObject.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(ObjectName + objectCount + "range");
                        SpawnedObject.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<UniversalConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Retriever")
                    {
                        GameObject SpawnedObject = Instantiate(Retriever, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<Retriever>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AutoCrafter")
                    {
                        GameObject SpawnedObject = Instantiate(AutoCrafter, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<AutoCrafter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Smelter")
                    {
                        GameObject SpawnedObject = Instantiate(Smelter, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<Smelter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "HeatExchanger")
                    {
                        GameObject SpawnedObject = Instantiate(HeatExchanger, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<HeatExchanger>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SolarPanel")
                    {
                        GameObject SpawnedObject = Instantiate(SolarPanel, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Generator")
                    {
                        GameObject SpawnedObject = Instantiate(Generator, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(ObjectName + objectCount + "fuelType");
                        SpawnedObject.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(ObjectName + objectCount + "fuelAmount");
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "NuclearReactor")
                    {
                        GameObject SpawnedObject = Instantiate(NuclearReactor, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<NuclearReactor>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ReactorTurbine")
                    {
                        GameObject SpawnedObject = Instantiate(ReactorTurbine, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "PowerConduit")
                    {
                        GameObject SpawnedObject = Instantiate(PowerConduit, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID1");
                        SpawnedObject.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID2");
                        SpawnedObject.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(ObjectName + objectCount + "dualOutput");
                        SpawnedObject.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(ObjectName + objectCount + "range");
                        SpawnedObject.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(ObjectName + objectCount + "powerAmount");
                        SpawnedObject.GetComponent<PowerConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Auger")
                    {
                        GameObject SpawnedObject = Instantiate(Auger, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<Auger>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ElectricLight")
                    {
                        GameObject SpawnedObject = Instantiate(ElectricLight, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<ElectricLight>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Turret")
                    {
                        GameObject SpawnedObject = Instantiate(Turret, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Turret>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AlloySmelter")
                    {
                        GameObject SpawnedObject = Instantiate(AlloySmelter, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID1");
                        SpawnedObject.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID2");
                        SpawnedObject.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType1");
                        SpawnedObject.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType2");
                        SpawnedObject.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount2");
                        SpawnedObject.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "outputAmount");
                        SpawnedObject.GetComponent<AlloySmelter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Press")
                    {
                        GameObject SpawnedObject = Instantiate(Press, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Press>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<Press>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<Press>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<Press>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<Press>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<Press>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Extruder")
                    {
                        GameObject SpawnedObject = Instantiate(Extruder, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<Extruder>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "GearCutter")
                    {
                        GameObject SpawnedObject = Instantiate(GearCutter, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<GearCutter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "UniversalExtractor")
                    {
                        GameObject SpawnedObject = Instantiate(UniversalExtractor, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(ObjectName + objectCount + "type");
                        SpawnedObject.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageContainer")
                    {
                        GameObject SpawnedObject = Instantiate(StorageContainer, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageComputer")
                    {
                        GameObject SpawnedObject = Instantiate(StorageComputer, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronBlock")
                    {
                        GameObject SpawnedObject = Instantiate(IronBlock, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronRamp")
                    {
                        GameObject SpawnedObject = Instantiate(IronRamp, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Steel")
                    {
                        GameObject SpawnedObject = Instantiate(Steel, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SteelRamp")
                    {
                        GameObject SpawnedObject = Instantiate(SteelRamp, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Brick")
                    {
                        GameObject SpawnedObject = Instantiate(Brick, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Brick>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Glass")
                    {
                        GameObject SpawnedObject = Instantiate(Glass, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<Glass>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ModMachine")
                    {
                        GameObject SpawnedObject = Instantiate(modMachine, ObjectPosition, ObjectRotation);
                        SpawnedObject.GetComponent<ModMachine>().machineName = FileBasedPrefs.GetString(ObjectName + objectCount + "machineName");
                        SpawnedObject.GetComponent<ModMachine>().inputID = FileBasedPrefs.GetString(ObjectName + objectCount + "inputID");
                        SpawnedObject.GetComponent<ModMachine>().outputID = FileBasedPrefs.GetString(ObjectName + objectCount + "outputID");
                        SpawnedObject.GetComponent<ModMachine>().inputType = FileBasedPrefs.GetString(ObjectName + objectCount + "inputType");
                        SpawnedObject.GetComponent<ModMachine>().outputType = FileBasedPrefs.GetString(ObjectName + objectCount + "outputType");
                        SpawnedObject.GetComponent<ModMachine>().speed = FileBasedPrefs.GetInt(ObjectName + objectCount + "speed");
                        SpawnedObject.GetComponent<ModMachine>().amount = FileBasedPrefs.GetFloat(ObjectName + objectCount + "amount");
                        SpawnedObject.GetComponent<ModMachine>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + objectCount + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + objectCount + "fallingStack");
                    }
                }
            }
        }
        Loaded = true;
        GetComponent<GameManager>().initGlass = FileBasedPrefs.GetBool(WorldName + "initGlass");
        GetComponent<GameManager>().initBrick = FileBasedPrefs.GetBool(WorldName + "initBrick");
        GetComponent<GameManager>().initIron = FileBasedPrefs.GetBool(WorldName + "initIron");
        GetComponent<GameManager>().initSteel = FileBasedPrefs.GetBool(WorldName + "initSteel");
        GameObject.Find("GameManager").GetComponent<GameManager>().meshManager.CombineBlocks();
    }

    // Assigns ID to objects in the world.
    private void AssignIDs()
    {
        addressingCoroutine = StartCoroutine(addressManager.AddressingCoroutine());
    }

    // Saves the game.
    public void SaveData()
    {
        if (assigningIDs == false)
            saveCoroutine = StartCoroutine(saveManager.SaveDataCoroutine());
    }

    // Returns true if the object in question is a storage container.
    public bool IsStorageContainer(GameObject go)
    {
        return go.GetComponent<InventoryManager>() != null
        && go.GetComponent<RailCart>() == null
        && go.GetComponent<PlayerController>() == null
        && go.GetComponent<Retriever>() == null
        && go.GetComponent<AutoCrafter>() == null;
    }
}
