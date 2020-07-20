using UnityEngine;
using System.Collections;

public class AutoCrafter : MonoBehaviour
{
    public int speed = 1;
    public int power;
    public int heat;
    public int cooling;
    public bool powerON;
    public string type;
    public string ID = "unassigned";
    public string inputID;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public bool hasHeatExchanger;
    private MachineCrafting machineCrafting;
    private ComputerCrafting computerCrafting;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    private GameObject builtObjects;

    void Start()
    {
        machineCrafting = GetComponent<MachineCrafting>();
        computerCrafting = GetComponent<ComputerCrafting>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
            if (speed > 1)
            {
                heat = speed - 1 - cooling;
            }
            else
            {
                heat = 0;
            }
            if (heat < 0)
            {
                heat = 0;
            }
            GetComponent<InventoryManager>().ID = ID;
            bool foundType = false;
            foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
            {
                if (foundType == false)
                {
                    if (slot.amountInSlot > 0)
                    {
                        foundType = true;
                        type = slot.typeInSlot;
                    }
                }
            }

            if (inputObject == null)
            {
                connectionAttempts += 1;
                if (creationMethod.Equals("spawned"))
                {
                    if (connectionAttempts >= 30)
                    {
                        connectionAttempts = 0;
                        connectionFailed = true;
                    }
                }
                else
                {
                    if (connectionAttempts >= 120)
                    {
                        connectionAttempts = 0;
                        connectionFailed = true;
                    }
                }
                if (connectionFailed == false)
                {
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj != null)
                        {
                            if (obj.transform.parent != builtObjects.transform)
                            {
                                if (obj.activeInHierarchy)
                                {
                                    if (obj.GetComponent<InventoryManager>() != null && !obj.GetComponent<InventoryManager>().ID.Equals("player") && obj.GetComponent<Retriever>() == null && obj.GetComponent<Rocket>() == null && obj.GetComponent<AutoCrafter>() == null && obj != this.gameObject)
                                    {
                                        if (inputObject == null)
                                        {
                                            if (creationMethod.Equals("spawned"))
                                            {
                                                //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + outputID);
                                                if (obj.GetComponent<InventoryManager>().ID.Equals(inputID))
                                                {
                                                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                    if (distance < 20 || obj.GetComponent<RailCart>() != null)
                                                    {
                                                        inputObject = obj;
                                                        if (obj.GetComponent<RailCart>() != null)
                                                        {
                                                            inputID = obj.GetComponent<RailCart>().ID;
                                                        }
                                                        else
                                                        {
                                                            inputID = obj.GetComponent<InventoryManager>().ID;
                                                        }
                                                        machineCrafting.inventoryManager = obj.GetComponent<InventoryManager>();
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                            }
                                            else if (creationMethod.Equals("built"))
                                            {
                                                float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                if (distance < 20)
                                                {
                                                    inputObject = obj;
                                                    inputID = obj.GetComponent<InventoryManager>().ID;
                                                    machineCrafting.inventoryManager = obj.GetComponent<InventoryManager>();
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<StorageComputer>() != null)
                                    {
                                        if (inputObject == null)
                                        {
                                            if (creationMethod.Equals("spawned"))
                                            {
                                                //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + outputID);
                                                if (obj.GetComponent<StorageComputer>().ID.Equals(inputID))
                                                {
                                                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                    if (distance < 20)
                                                    {
                                                        inputObject = obj;
                                                        inputID = obj.GetComponent<StorageComputer>().ID;
                                                        computerCrafting.computerManager = obj.GetComponent<StorageComputer>().computerContainers;
                                                        computerCrafting.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                            }
                                            else if (creationMethod.Equals("built"))
                                            {
                                                float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                if (distance < 20)
                                                {
                                                    inputObject = obj;
                                                    inputID = obj.GetComponent<StorageComputer>().ID;
                                                    computerCrafting.computerManager = obj.GetComponent<StorageComputer>().computerContainers;
                                                    computerCrafting.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (connectionFailed == false)
            {
                if (inputObject != null)
                {
                    float distance = Vector3.Distance(transform.position, inputObject.transform.position);
                    if (distance < 20)
                    {
                        if (powerON == true && speed > 0 && type != "" && type != "nothing")
                        {
                            if (inputObject.GetComponent<InventoryManager>() != null)
                            {
                                if (inputObject.GetComponent<RailCart>() != null)
                                {
                                    inputID = inputObject.GetComponent<RailCart>().ID;
                                }
                                else
                                {
                                    inputID = inputObject.GetComponent<InventoryManager>().ID;
                                }
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                if (GetComponent<AudioSource>().isPlaying == false)
                                {
                                    GetComponent<AudioSource>().Play();
                                }
                                connectionLine.enabled = true;
                                connectionLine.SetPosition(1, inputObject.transform.position);
                                machineTimer += 1;
                                if (machineTimer > 5 - (address * 0.01f))
                                {
                                    for (int count = 0; count < speed; count++)
                                    {
                                        if (type.Equals("Iron Block"))
                                        {
                                            machineCrafting.CraftIronBlock();
                                        }
                                        if (type.Equals("Iron Ramp"))
                                        {
                                            machineCrafting.CraftIronRamp();
                                        }
                                        if (type.Equals("Steel Block"))
                                        {
                                            machineCrafting.CraftSteelBlock();
                                        }
                                        if (type.Equals("Steel Ramp"))
                                        {
                                            machineCrafting.CraftSteelRamp();
                                        }
                                        if (type.Equals("Circuit Board"))
                                        {
                                            machineCrafting.CraftCircuitBoard();
                                        }
                                        if (type.Equals("Electric Motor"))
                                        {
                                            machineCrafting.CraftMotor();
                                        }
                                        if (type.Equals("Quantum Hatchway"))
                                        {
                                            machineCrafting.CraftQuantumHatchway();
                                        }
                                        if (type.Equals("Electric Light"))
                                        {
                                            machineCrafting.CraftElectricLight();
                                        }
                                        if (type.Equals("Auger"))
                                        {
                                            machineCrafting.CraftAuger();
                                        }
                                        if (type.Equals("Storage Container"))
                                        {
                                            machineCrafting.CraftStorageContainer();
                                        }
                                        if (type.Equals("Storage Computer"))
                                        {
                                            machineCrafting.CraftStorageComputer();
                                        }
                                        if (type.Equals("Extruder"))
                                        {
                                            machineCrafting.CraftExtruder();
                                        }
                                        if (type.Equals("Press"))
                                        {
                                            machineCrafting.CraftPress();
                                        }
                                        if (type.Equals("Universal Extractor"))
                                        {
                                            machineCrafting.CraftUniversalExtractor();
                                        }
                                        if (type.Equals("Universal Conduit"))
                                        {
                                            machineCrafting.CraftUniversalConduit();
                                        }
                                        if (type.Equals("Retriever"))
                                        {
                                            machineCrafting.CraftRetriever();
                                        }
                                        if (type.Equals("Solar Panel"))
                                        {
                                            machineCrafting.CraftSolarPanel();
                                        }
                                        if (type.Equals("Generator"))
                                        {
                                            machineCrafting.CraftGenerator();
                                        }
                                        if (type.Equals("Power Conduit"))
                                        {
                                            machineCrafting.CraftPowerConduit();
                                        }
                                        if (type.Equals("Nuclear Reactor"))
                                        {
                                            machineCrafting.CraftNuclearReactor();
                                        }
                                        if (type.Equals("Reactor Turbine"))
                                        {
                                            machineCrafting.CraftReactorTurbine();
                                        }
                                        if (type.Equals("Heat Exchanger"))
                                        {
                                            machineCrafting.CraftHeatExchanger();
                                        }
                                        if (type.Equals("Smelter"))
                                        {
                                            machineCrafting.CraftSmelter();
                                        }
                                        if (type.Equals("Gear Cutter"))
                                        {
                                            machineCrafting.CraftGearCutter();
                                        }
                                        if (type.Equals("Alloy Smelter"))
                                        {
                                            machineCrafting.CraftAlloySmelter();
                                        }
                                        if (type.Equals("Turret"))
                                        {
                                            machineCrafting.CraftTurret();
                                        }
                                        if (type.Equals("DM Collector"))
                                        {
                                            machineCrafting.CraftDarkMatterCollector();
                                        }
                                        if (type.Equals("DM Conduit"))
                                        {
                                            machineCrafting.CraftDarkMatterConduit();
                                        }
                                        if (type.Equals("Auto Crafter"))
                                        {
                                            machineCrafting.CraftAutoCrafter();
                                        }
                                        if (type.Equals("Rail Cart Hub"))
                                        {
                                            machineCrafting.CraftRailCartHub();
                                        }
                                        if (type.Equals("Rail Cart"))
                                        {
                                            machineCrafting.CraftRailCart();
                                        }
                                    }
                                    machineTimer = 0;
                                }
                            }
                            if (inputObject.GetComponent<StorageComputer>() != null)
                            {
                                inputID = inputObject.GetComponent<StorageComputer>().ID;
                                if (inputObject.GetComponent<StorageComputer>().initialized == true)
                                {
                                    computerCrafting.computerManager = inputObject.GetComponent<StorageComputer>().computerContainers;
                                    if (computerCrafting.conduitItem == null)
                                    {
                                        GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, inputObject.transform.position, inputObject.transform.rotation);
                                        storageComputerItemObject.transform.parent = inputObject.transform;
                                        computerCrafting.conduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
                                    }
                                    conduitItem.GetComponent<ConduitItem>().active = true;
                                    GetComponent<Light>().enabled = true;
                                    if (GetComponent<AudioSource>().isPlaying == false)
                                    {
                                        GetComponent<AudioSource>().Play();
                                    }
                                    connectionLine.enabled = true;
                                    connectionLine.SetPosition(1, inputObject.transform.position);
                                    machineTimer += 1;
                                    if (machineTimer > 5 - (address * 0.01f))
                                    {
                                        for (int count = 0; count < speed; count++)
                                        {
                                            if (type.Equals("Iron Block"))
                                            {
                                                computerCrafting.CraftIronBlock();
                                            }
                                            if (type.Equals("Iron Ramp"))
                                            {
                                                computerCrafting.CraftIronRamp();
                                            }
                                            if (type.Equals("Steel Block"))
                                            {
                                                computerCrafting.CraftSteelBlock();
                                            }
                                            if (type.Equals("Steel Ramp"))
                                            {
                                                computerCrafting.CraftSteelRamp();
                                            }
                                            if (type.Equals("Circuit Board"))
                                            {
                                                computerCrafting.CraftCircuitBoard();
                                            }
                                            if (type.Equals("Electric Motor"))
                                            {
                                                computerCrafting.CraftMotor();
                                            }
                                            if (type.Equals("Quantum Hatchway"))
                                            {
                                                computerCrafting.CraftQuantumHatchway();
                                            }
                                            if (type.Equals("Electric Light"))
                                            {
                                                computerCrafting.CraftElectricLight();
                                            }
                                            if (type.Equals("Auger"))
                                            {
                                                computerCrafting.CraftAuger();
                                            }
                                            if (type.Equals("Storage Container"))
                                            {
                                                computerCrafting.CraftStorageContainer();
                                            }
                                            if (type.Equals("Storage Computer"))
                                            {
                                                computerCrafting.CraftStorageComputer();
                                            }
                                            if (type.Equals("Extruder"))
                                            {
                                                computerCrafting.CraftExtruder();
                                            }
                                            if (type.Equals("Press"))
                                            {
                                                computerCrafting.CraftPress();
                                            }
                                            if (type.Equals("Universal Extractor"))
                                            {
                                                computerCrafting.CraftUniversalExtractor();
                                            }
                                            if (type.Equals("Universal Conduit"))
                                            {
                                                computerCrafting.CraftUniversalConduit();
                                            }
                                            if (type.Equals("Retriever"))
                                            {
                                                computerCrafting.CraftRetriever();
                                            }
                                            if (type.Equals("Solar Panel"))
                                            {
                                                computerCrafting.CraftSolarPanel();
                                            }
                                            if (type.Equals("Generator"))
                                            {
                                                computerCrafting.CraftGenerator();
                                            }
                                            if (type.Equals("Power Conduit"))
                                            {
                                                computerCrafting.CraftPowerConduit();
                                            }
                                            if (type.Equals("Nuclear Reactor"))
                                            {
                                                computerCrafting.CraftNuclearReactor();
                                            }
                                            if (type.Equals("Reactor Turbine"))
                                            {
                                                computerCrafting.CraftReactorTurbine();
                                            }
                                            if (type.Equals("Heat Exchanger"))
                                            {
                                                computerCrafting.CraftHeatExchanger();
                                            }
                                            if (type.Equals("Smelter"))
                                            {
                                                computerCrafting.CraftSmelter();
                                            }
                                            if (type.Equals("Gear Cutter"))
                                            {
                                                computerCrafting.CraftGearCutter();
                                            }
                                            if (type.Equals("Alloy Smelter"))
                                            {
                                                computerCrafting.CraftAlloySmelter();
                                            }
                                            if (type.Equals("Turret"))
                                            {
                                                computerCrafting.CraftTurret();
                                            }
                                            if (type.Equals("DM Collector"))
                                            {
                                                computerCrafting.CraftDarkMatterCollector();
                                            }
                                            if (type.Equals("DM Conduit"))
                                            {
                                                computerCrafting.CraftDarkMatterConduit();
                                            }
                                            if (type.Equals("Auto Crafter"))
                                            {
                                                computerCrafting.CraftAutoCrafter();
                                            }
                                            if (type.Equals("Rail Cart Hub"))
                                            {
                                                computerCrafting.CraftRailCartHub();
                                            }
                                            if (type.Equals("Rail Cart"))
                                            {
                                                computerCrafting.CraftRailCart();
                                            }
                                        }
                                        machineTimer = 0;
                                    }
                                }
                                else
                                {
                                    if (computerCrafting.conduitItem != null)
                                    {
                                        computerCrafting.conduitItem.active = false;
                                    }
                                    conduitItem.GetComponent<ConduitItem>().active = false;
                                    GetComponent<Light>().enabled = false;
                                    GetComponent<AudioSource>().Stop();
                                }
                            }
                        }
                        else
                        {
                            if (computerCrafting.conduitItem != null)
                            {
                                computerCrafting.conduitItem.active = false;
                            }
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                    else
                    {
                        if (computerCrafting.conduitItem != null)
                        {
                            computerCrafting.conduitItem.active = false;
                        }
                        connectionLine.enabled = false;
                        conduitItem.GetComponent<ConduitItem>().active = false;
                        GetComponent<Light>().enabled = false;
                        GetComponent<AudioSource>().Stop();
                    }
                }
                else
                {
                    if (computerCrafting.conduitItem != null)
                    {
                        computerCrafting.conduitItem.active = false;
                    }
                    connectionLine.enabled = false;
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().Stop();
                }
            }
            else
            {
                if (computerCrafting.conduitItem != null)
                {
                    computerCrafting.conduitItem.active = false;
                }
                connectionLine.enabled = false;
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().Stop();
            }
            if (inputObject == null)
            {
                connectionLine.enabled = false;
                if (connectionFailed == true)
                {
                    if (creationMethod.Equals("spawned"))
                    {
                        creationMethod = "built";
                    }
                }
            }
        }
    }
}