using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StateManager : MonoBehaviour
{
    //BUILDING
    float updateTick;
    private bool saving;
    public bool dataSaved;
    public bool worldLoaded = false;
    public bool exiting;
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
    private int ConstructionCount = 0;
    private string PartNumber = "";
    private string PartName = "";
    private string ObjectName = "";
    private Vector3 EmptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 PartPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 ObjectPrintingLocation;
    private Quaternion ObjectPrintingRotation;
    private Quaternion PartRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    private Coroutine saveCoroutine;

    //Whether or not initialization has completed.
    public bool Loaded = false;

    // Use this for initialization
    void Start()
    {

    }

    void LoadWorld()
    {
        //LOADING BUILT OBJECTS
        //Debug.Log("Game Starting");
        if (Loaded == false && PlayerPrefs.GetInt(WorldName + "ConstructionTotal") != 0)
        {
            //Debug.Log("Getting Objects");
            ConstructionCount = 0;
            do
            {
                PartNumber = ConstructionCount.ToString();
                ObjectPrintingLocation = PlayerPrefsX.GetVector3(WorldName + PartNumber + "Position");
                ObjectPrintingRotation = PlayerPrefsX.GetQuaternion(WorldName + PartNumber + "Rotation");
                ObjectName = PlayerPrefs.GetString(WorldName + PartNumber + "Name");
                //Debug.Log("Spawning " + ObjectName + " " + PartNumber + " at " + ObjectPrintingLocation);
                if (ObjectName != "" && ObjectPrintingLocation != EmptyVector)
                {
                    if (ObjectName == WorldName + "AirLock")
                    {
                        GameObject PrintedPart = Instantiate(AirLock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterCollector")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterCollector, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterCollector>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterConduit")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterConduit>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterConduit>().range = PlayerPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCartHub")
                    {
                        GameObject PrintedPart = Instantiate(RailCartHub, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCartHub>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<RailCartHub>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<RailCartHub>().range = PlayerPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<RailCartHub>().stop = PlayerPrefsX.GetBool(ObjectName + PartNumber + "stop");
                        PrintedPart.GetComponent<RailCartHub>().stopTime = PlayerPrefs.GetFloat(ObjectName + PartNumber + "stopTime");
                        PrintedPart.GetComponent<RailCartHub>().centralHub = PlayerPrefsX.GetBool(ObjectName + PartNumber + "centralHub");
                        PrintedPart.GetComponent<RailCartHub>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCart")
                    {
                        GameObject PrintedPart = Instantiate(RailCart, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCart>().creationMethod = "spawned";
                        PrintedPart.GetComponent<RailCart>().targetID = PlayerPrefs.GetString(ObjectName + PartNumber + "targetID");
                    }
                    if (ObjectName == WorldName + "UniversalConduit")
                    {
                        GameObject PrintedPart = Instantiate(UniversalConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalConduit>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<UniversalConduit>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<UniversalConduit>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalConduit>().range = PlayerPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<UniversalConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Retriever")
                    {
                        GameObject PrintedPart = Instantiate(Retriever, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Retriever>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Retriever>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Retriever>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Retriever>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        //Debug.Log("Spawning " + ObjectName + " with input ID of " + PlayerPrefs.GetString(ObjectName + PartNumber + "inputID"));
                    }
                    if (ObjectName == WorldName + "AutoCrafter")
                    {
                        GameObject PrintedPart = Instantiate(AutoCrafter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AutoCrafter>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<AutoCrafter>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AutoCrafter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Smelter")
                    {
                        GameObject PrintedPart = Instantiate(Smelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Smelter>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Smelter>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Smelter>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Smelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "HeatExchanger")
                    {
                        GameObject PrintedPart = Instantiate(HeatExchanger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<HeatExchanger>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<HeatExchanger>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<HeatExchanger>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<HeatExchanger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SolarPanel")
                    {
                        GameObject PrintedPart = Instantiate(SolarPanel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Generator")
                    {
                        GameObject PrintedPart = Instantiate(Generator, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PowerSource>().fuelType = PlayerPrefs.GetString(ObjectName + PartNumber + "fuelType");
                        PrintedPart.GetComponent<PowerSource>().fuelAmount = PlayerPrefs.GetInt(ObjectName + PartNumber + "fuelAmount");
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "NuclearReactor")
                    {
                        GameObject PrintedPart = Instantiate(NuclearReactor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<NuclearReactor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ReactorTurbine")
                    {
                        GameObject PrintedPart = Instantiate(ReactorTurbine, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "PowerConduit")
                    {
                        GameObject PrintedPart = Instantiate(PowerConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerConduit>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<PowerConduit>().outputID1 = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID1");
                        PrintedPart.GetComponent<PowerConduit>().outputID2 = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID2");
                        PrintedPart.GetComponent<PowerConduit>().dualOutput = PlayerPrefsX.GetBool(ObjectName + PartNumber + "dualOutput");
                        PrintedPart.GetComponent<PowerConduit>().range = PlayerPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<PowerConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Auger")
                    {
                        GameObject PrintedPart = Instantiate(Auger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Auger>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Auger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ElectricLight")
                    {
                        GameObject PrintedPart = Instantiate(ElectricLight, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<ElectricLight>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Turret")
                    {
                        GameObject PrintedPart = Instantiate(Turret, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Turret>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Turret>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AlloySmelter")
                    {
                        GameObject PrintedPart = Instantiate(AlloySmelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AlloySmelter>().inputID1 = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID1");
                        PrintedPart.GetComponent<AlloySmelter>().inputID2 = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID2");
                        PrintedPart.GetComponent<AlloySmelter>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<AlloySmelter>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AlloySmelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Press")
                    {
                        GameObject PrintedPart = Instantiate(Press, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Press>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Press>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Press>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Press>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Extruder")
                    {
                        GameObject PrintedPart = Instantiate(Extruder, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Extruder>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Extruder>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Extruder>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Extruder>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "GearCutter")
                    {
                        GameObject PrintedPart = Instantiate(GearCutter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<GearCutter>().inputID = PlayerPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<GearCutter>().outputID = PlayerPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<GearCutter>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<GearCutter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "UniversalExtractor")
                    {
                        GameObject PrintedPart = Instantiate(UniversalExtractor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalExtractor>().speed = PlayerPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageContainer")
                    {
                        GameObject PrintedPart = Instantiate(StorageContainer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageComputer")
                    {
                        GameObject PrintedPart = Instantiate(StorageComputer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronBlock")
                    {
                        GameObject PrintedPart = Instantiate(IronBlock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronRamp")
                    {
                        GameObject PrintedPart = Instantiate(IronRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Steel")
                    {
                        GameObject PrintedPart = Instantiate(Steel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SteelRamp")
                    {
                        GameObject PrintedPart = Instantiate(SteelRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Brick")
                    {
                        GameObject PrintedPart = Instantiate(Brick, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Brick>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Glass")
                    {
                        GameObject PrintedPart = Instantiate(Glass, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Glass>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = PlayerPrefsX.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = PlayerPrefsX.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    ConstructionCount++;
                }
                else
                {
                    ConstructionCount++;
                }
            }
            while (ConstructionCount <= PlayerPrefs.GetInt(WorldName + "ConstructionTotal"));
            //Debug.Log("Spawned "+ConstructionCount+" objects.");
        }
        Loaded = true;
        GetComponent<GameManager>().initGlass = PlayerPrefsX.GetBool(WorldName + "initGlass");
        GetComponent<GameManager>().initBrick = PlayerPrefsX.GetBool(WorldName + "initBrick");
        GetComponent<GameManager>().initIron = PlayerPrefsX.GetBool(WorldName + "initIron");
        GetComponent<GameManager>().initSteel = PlayerPrefsX.GetBool(WorldName + "initSteel");
        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<MainMenu>().worldSelected == true && worldLoaded == false)
        {
            //Debug.Log("Loading world: " + WorldName);
            LoadWorld();
            worldLoaded = true;
        }
        if (worldLoaded == true)
        {
            updateTick += 1 * Time.deltaTime;
            if (updateTick > 1)
            {
                //SAVING WORLD STATE
                if (saving == false && exiting == false)
                {
                    SaveData();
                }
                updateTick = 0;
            }
        }
    }

    //SAVING BUILT OBJECTS
    public void SaveData()
    {
        saveCoroutine = StartCoroutine(SaveDataCoroutine());
    }

    IEnumerator SaveDataCoroutine()
    {
        dataSaved = false;
        saving = true;
        ConstructionCount = 0;
        int saveInterval = 0;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != BuiltObjects.transform)
                {
                    PartPosition = go.transform.position;
                    PartRotation = go.transform.rotation;
                    PartNumber = ConstructionCount.ToString();
                    if (go.GetComponent<Auger>() != null)
                    {
                        PartName = WorldName + "Auger";
                        go.GetComponent<Auger>().ID = (PartName + PartNumber);
                        go.GetComponent<Auger>().address = ConstructionCount;
                        int speed = go.GetComponent<Auger>().speed;
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        PartName = WorldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = (PartName + PartNumber);
                        go.GetComponent<ElectricLight>().address = ConstructionCount;
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        PartName = WorldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = (PartName + PartNumber);
                        go.GetComponent<DarkMatterCollector>().address = ConstructionCount;
                        int speed = go.GetComponent<DarkMatterCollector>().speed;
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        PartName = WorldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().address = ConstructionCount;
                        go.GetComponent<DarkMatterConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<DarkMatterConduit>().inputID;
                        string outputID = go.GetComponent<DarkMatterConduit>().outputID;
                        int speed = go.GetComponent<DarkMatterConduit>().speed;
                        int range = go.GetComponent<DarkMatterConduit>().range;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefs.SetInt(PartName + PartNumber + "range", range);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        PartName = WorldName + "RailCart";
                        go.GetComponent<RailCart>().address = ConstructionCount;
                        go.GetComponent<RailCart>().ID = (PartName + PartNumber);
                        string targetID = go.GetComponent<RailCart>().targetID;
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        PlayerPrefs.SetString(PartName + PartNumber + "targetID", targetID);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        PartName = WorldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().address = ConstructionCount;
                        go.GetComponent<RailCartHub>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<RailCartHub>().inputID;
                        string outputID = go.GetComponent<RailCartHub>().outputID;
                        int range = go.GetComponent<RailCartHub>().range;
                        bool centralHub = go.GetComponent<RailCartHub>().centralHub;
                        bool stop = go.GetComponent<RailCartHub>().stop;
                        float stopTime = go.GetComponent<RailCartHub>().stopTime;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "range", range);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "centralHub", centralHub);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "stop", stop);
                        PlayerPrefs.SetFloat(PartName + PartNumber + "stopTime", stopTime);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        PartName = WorldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().address = ConstructionCount;
                        go.GetComponent<UniversalConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<UniversalConduit>().inputID;
                        string outputID = go.GetComponent<UniversalConduit>().outputID;
                        int speed = go.GetComponent<UniversalConduit>().speed;
                        int range = go.GetComponent<UniversalConduit>().range;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefs.SetInt(PartName + PartNumber + "range", range);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        PartName = WorldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().address = ConstructionCount;
                        go.GetComponent<HeatExchanger>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<HeatExchanger>().inputID;
                        string outputID = go.GetComponent<HeatExchanger>().outputID;
                        int speed = go.GetComponent<HeatExchanger>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        PartName = WorldName + "Retriever";
                        go.GetComponent<Retriever>().address = ConstructionCount;
                        go.GetComponent<Retriever>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Retriever>().inputID;
                        string outputID = go.GetComponent<Retriever>().outputID;
                        int speed = go.GetComponent<Retriever>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        //Debug.Log("Saving " + PartName + PartNumber + " with inputID of " + inputID);
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        PartName = WorldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().address = ConstructionCount;
                        go.GetComponent<AutoCrafter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<AutoCrafter>().inputID;
                        int speed = go.GetComponent<AutoCrafter>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        PartName = WorldName + "Smelter";
                        go.GetComponent<Smelter>().address = ConstructionCount;
                        go.GetComponent<Smelter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Smelter>().inputID;
                        string outputID = go.GetComponent<Smelter>().outputID;
                        int speed = go.GetComponent<Smelter>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        PartName = WorldName + "Turret";
                        go.GetComponent<Turret>().address = ConstructionCount;
                        go.GetComponent<Turret>().ID = (PartName + PartNumber);
                        int speed = go.GetComponent<Turret>().speed;
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<PowerSource>() != null)
                    {
                        if (go.GetComponent<PowerSource>().type == "Solar Panel")
                        {
                            PartName = WorldName + "SolarPanel";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Generator")
                        {
                            PartName = WorldName + "Generator";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Reactor Turbine")
                        {
                            PartName = WorldName + "ReactorTurbine";
                        }
                        go.GetComponent<PowerSource>().address = ConstructionCount;
                        go.GetComponent<PowerSource>().ID = (PartName + PartNumber);
                        string outputID = go.GetComponent<PowerSource>().outputID;
                        string fuelType = go.GetComponent<PowerSource>().fuelType;
                        int fuelAmount = go.GetComponent<PowerSource>().fuelAmount;
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "fuelType", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "fuelAmount", fuelAmount);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        PartName = WorldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().address = ConstructionCount;
                        go.GetComponent<NuclearReactor>().ID = (PartName + PartNumber);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        PartName = WorldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().address = ConstructionCount;
                        go.GetComponent<PowerConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<PowerConduit>().inputID;
                        string outputID1 = go.GetComponent<PowerConduit>().outputID1;
                        string outputID2 = go.GetComponent<PowerConduit>().outputID2;
                        bool dualOutput = go.GetComponent<PowerConduit>().dualOutput;
                        int range = go.GetComponent<PowerConduit>().range;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID1", outputID1);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID2", outputID2);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "dualOutput", dualOutput);
                        PlayerPrefs.SetInt(PartName + PartNumber + "range", range);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        PartName = WorldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().address = ConstructionCount;
                        go.GetComponent<AlloySmelter>().ID = (PartName + PartNumber);

                        string inputID1 = go.GetComponent<AlloySmelter>().inputID1;
                        string inputID2 = go.GetComponent<AlloySmelter>().inputID2;
                        string outputID = go.GetComponent<AlloySmelter>().outputID;
                        int speed = go.GetComponent<AlloySmelter>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID1", inputID1);
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID2", inputID2);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        PartName = WorldName + "Press";
                        go.GetComponent<Press>().address = ConstructionCount;
                        go.GetComponent<Press>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Press>().inputID;
                        string outputID = go.GetComponent<Press>().outputID;
                        int speed = go.GetComponent<Press>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        PartName = WorldName + "Extruder";
                        go.GetComponent<Extruder>().address = ConstructionCount;
                        go.GetComponent<Extruder>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Extruder>().inputID;
                        string outputID = go.GetComponent<Extruder>().outputID;
                        int speed = go.GetComponent<Extruder>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        PartName = WorldName + "GearCutter";
                        go.GetComponent<GearCutter>().address = ConstructionCount;
                        go.GetComponent<GearCutter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<GearCutter>().inputID;
                        string outputID = go.GetComponent<GearCutter>().outputID;
                        int speed = go.GetComponent<GearCutter>().speed;
                        PlayerPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        PlayerPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        //Debug.Log("Saving " + PartName + PartNumber + " with outputID of " + outputID);
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        PartName = WorldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().address = ConstructionCount;
                        go.GetComponent<UniversalExtractor>().ID = (PartName + PartNumber);
                        int speed = go.GetComponent<UniversalExtractor>().speed;
                        PlayerPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<InventoryManager>() != null && go.GetComponent<RailCart>() == null && go.GetComponent<PlayerController>() == null && go.GetComponent<Retriever>() == null && go.GetComponent<AutoCrafter>() == null)
                    {
                        PartName = WorldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = (PartName + PartNumber);
                        go.GetComponent<InventoryManager>().address = ConstructionCount;
                        go.GetComponent<InventoryManager>().SaveData();
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        PartName = WorldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().address = ConstructionCount;
                        go.GetComponent<StorageComputer>().ID = (PartName + PartNumber);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        PartName = WorldName + "AirLock";
                        go.GetComponent<AirLock>().address = ConstructionCount;
                        go.GetComponent<AirLock>().ID = (PartName + PartNumber);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }

                    PlayerPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);

                    ConstructionCount++;

                    if (exiting == false)
                    {
                        saveInterval++;
                        if (saveInterval >= 10)
                        {
                            yield return null;
                            saveInterval = 0;
                        }
                    }
                    //Debug.Log("saving data...");
                }
            }
        }

        Transform[] allTransforms = BuiltObjects.GetComponentsInChildren<Transform>(true);
        foreach (Transform T in allTransforms)
        {
            if (T != null)
            {
                PartPosition = T.position;
                PartRotation = T.rotation;
                PartNumber = ConstructionCount.ToString();
                if (T.gameObject.GetComponent<IronBlock>() != null)
                {
                    if (T.gameObject.name.Equals("IronRamp(Clone)"))
                    {
                        PartName = WorldName + "IronRamp";
                    }
                    else
                    {
                        PartName = WorldName + "IronBlock";
                    }
                    T.gameObject.GetComponent<IronBlock>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<IronBlock>().address = ConstructionCount;
                    PlayerPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Steel>() != null)
                {
                    if (T.gameObject.name.Equals("SteelRamp(Clone)"))
                    {
                        PartName = WorldName + "SteelRamp";
                    }
                    else
                    {
                        PartName = WorldName + "Steel";
                    }
                    T.gameObject.GetComponent<Steel>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Steel>().address = ConstructionCount;
                    PlayerPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    PartName = WorldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Brick>().address = ConstructionCount;
                    PlayerPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    PartName = WorldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Glass>().address = ConstructionCount;
                    PlayerPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    PlayerPrefsX.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }

                if (exiting == false)
                {
                    saveInterval++;
                    if (saveInterval >= 10)
                    {
                        yield return null;
                        saveInterval = 0;
                    }
                }
                //Debug.Log("saving data...");
            }
        }

        if (ConstructionCount != 0)
        {
            PlayerPrefs.SetInt(WorldName + "ConstructionTotal", ConstructionCount);
        }
        //Debug.Log("Total Objects = "+PlayerPrefs.GetInt(WorldName + "ConstructionTotal"));
        dataSaved = true;
        saving = false;
        //Debug.Log("Data Saved: " + dataSaved);
    }
}
