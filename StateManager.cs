using UnityEngine;

public class StateManager : MonoBehaviour
{
    //BUILDING
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded = false;
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
    public GameObject BuiltObjects;
    public string WorldName = "World";
    private float updateTick;
    private string ObjectName = "";
    public string PartName = "";
    public Vector3 PartPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Quaternion PartRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    public string PartNumber = "";
    public bool Loaded = false;
    public bool assigningIDs;
    public int ConstructionCount = 0;
    private Vector3 EmptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 ObjectPrintingLocation;
    private Quaternion ObjectPrintingRotation;
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
        if (GameObject.Find("Player").GetComponent<MainMenu>().worldSelected == true && worldLoaded == false)
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
        if (Loaded == false && FileBasedPrefs.GetInt(WorldName + "ConstructionTotal") != 0)
        {
            ConstructionCount = 0;
            do
            {
                PartNumber = ConstructionCount.ToString();
                ObjectPrintingLocation = PlayerPrefsX.GetVector3(WorldName + PartNumber + "Position");
                ObjectPrintingRotation = PlayerPrefsX.GetQuaternion(WorldName + PartNumber + "Rotation");
                ObjectName = FileBasedPrefs.GetString(WorldName + PartNumber + "Name");
                if (ObjectName != "" && ObjectPrintingLocation != EmptyVector)
                {
                    if (ObjectName == WorldName + "AirLock")
                    {
                        GameObject PrintedPart = Instantiate(AirLock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterCollector")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterCollector, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "darkMatterAmount");
                        PrintedPart.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterConduit")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "darkMatterAmount");
                        PrintedPart.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCartHub")
                    {
                        GameObject PrintedPart = Instantiate(RailCartHub, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(ObjectName + PartNumber + "stop");
                        PrintedPart.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(ObjectName + PartNumber + "circuit");
                        PrintedPart.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "stopTime");
                        PrintedPart.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(ObjectName + PartNumber + "centralHub");
                        PrintedPart.GetComponent<RailCartHub>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCart")
                    {
                        GameObject PrintedPart = Instantiate(RailCart, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCart>().creationMethod = "spawned";
                        PrintedPart.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(ObjectName + PartNumber + "targetID");
                    }
                    if (ObjectName == WorldName + "UniversalConduit")
                    {
                        GameObject PrintedPart = Instantiate(UniversalConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(ObjectName + PartNumber + "type");
                        PrintedPart.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<UniversalConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Retriever")
                    {
                        GameObject PrintedPart = Instantiate(Retriever, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Retriever>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AutoCrafter")
                    {
                        GameObject PrintedPart = Instantiate(AutoCrafter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AutoCrafter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Smelter")
                    {
                        GameObject PrintedPart = Instantiate(Smelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Smelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "HeatExchanger")
                    {
                        GameObject PrintedPart = Instantiate(HeatExchanger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<HeatExchanger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SolarPanel")
                    {
                        GameObject PrintedPart = Instantiate(SolarPanel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Generator")
                    {
                        GameObject PrintedPart = Instantiate(Generator, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(ObjectName + PartNumber + "fuelType");
                        PrintedPart.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(ObjectName + PartNumber + "fuelAmount");
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "NuclearReactor")
                    {
                        GameObject PrintedPart = Instantiate(NuclearReactor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<NuclearReactor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ReactorTurbine")
                    {
                        GameObject PrintedPart = Instantiate(ReactorTurbine, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "PowerConduit")
                    {
                        GameObject PrintedPart = Instantiate(PowerConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID1");
                        PrintedPart.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID2");
                        PrintedPart.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(ObjectName + PartNumber + "dualOutput");
                        PrintedPart.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(ObjectName + PartNumber + "powerAmount");
                        PrintedPart.GetComponent<PowerConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Auger")
                    {
                        GameObject PrintedPart = Instantiate(Auger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Auger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ElectricLight")
                    {
                        GameObject PrintedPart = Instantiate(ElectricLight, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<ElectricLight>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Turret")
                    {
                        GameObject PrintedPart = Instantiate(Turret, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Turret>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AlloySmelter")
                    {
                        GameObject PrintedPart = Instantiate(AlloySmelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID1");
                        PrintedPart.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID2");
                        PrintedPart.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType1");
                        PrintedPart.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType2");
                        PrintedPart.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount2");
                        PrintedPart.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "outputAmount");
                        PrintedPart.GetComponent<AlloySmelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Press")
                    {
                        GameObject PrintedPart = Instantiate(Press, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Press>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Press>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Press>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Press>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Press>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Press>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Extruder")
                    {
                        GameObject PrintedPart = Instantiate(Extruder, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Extruder>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "GearCutter")
                    {
                        GameObject PrintedPart = Instantiate(GearCutter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<GearCutter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "UniversalExtractor")
                    {
                        GameObject PrintedPart = Instantiate(UniversalExtractor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(ObjectName + PartNumber + "type");
                        PrintedPart.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageContainer")
                    {
                        GameObject PrintedPart = Instantiate(StorageContainer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageComputer")
                    {
                        GameObject PrintedPart = Instantiate(StorageComputer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronBlock")
                    {
                        GameObject PrintedPart = Instantiate(IronBlock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronRamp")
                    {
                        GameObject PrintedPart = Instantiate(IronRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Steel")
                    {
                        GameObject PrintedPart = Instantiate(Steel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SteelRamp")
                    {
                        GameObject PrintedPart = Instantiate(SteelRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Brick")
                    {
                        GameObject PrintedPart = Instantiate(Brick, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Brick>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Glass")
                    {
                        GameObject PrintedPart = Instantiate(Glass, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Glass>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    ConstructionCount++;
                }
                else
                {
                    ConstructionCount++;
                }
            }
            while (ConstructionCount <= FileBasedPrefs.GetInt(WorldName + "ConstructionTotal"));
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
