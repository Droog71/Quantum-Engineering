using UnityEngine;
using System.Collections;

public class PowerConduit : MonoBehaviour
{
    public string ID = "unassigned";
    public Material lineMat;
    public string creationMethod;
    public GameObject outputObject1;
    public GameObject outputObject2;
    public GameObject inputObject;
    public GameObject connectionObject;
    public string outputID1 = "unassigned";
    public string outputID2 = "unassigned";
    public string inputID;
    private LineRenderer connectionLine;
    private GameObject connectionLine2;
    private float updateTick;
    public int address;
    public int powerAmount;
    public bool dualOutput;
    public int range = 6;
    public int connectionAttempts;
    public int dualConnectionAttempts;
    public bool connectionFailed;
    private GameObject builtObjects;
    public PowerReceiver powerReceiver;

    void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    void OnDestroy()
    {
        if (connectionLine2 != null)
        {
            Destroy(connectionLine2);
        }
        if (outputObject1 != null)
        {
            if (outputObject1.GetComponent<ElectricLight>() != null)
            {
                outputObject1.GetComponent<ElectricLight>().powerON = false;
            }
            if (outputObject1.GetComponent<StorageComputer>() != null)
            {
                outputObject1.GetComponent<StorageComputer>().powerON = false;
            }
            if (outputObject1.GetComponent<Retriever>() != null)
            {
                outputObject1.GetComponent<Retriever>().power = 0;
                outputObject1.GetComponent<Retriever>().speed = 1;
                outputObject1.GetComponent<Retriever>().powerON = false;
            }
            if (outputObject1.GetComponent<Auger>() != null)
            {
                outputObject1.GetComponent<Auger>().power = 0;
                outputObject1.GetComponent<Auger>().speed = 1;
                outputObject1.GetComponent<Auger>().powerON = false;
            }
            if (outputObject1.GetComponent<UniversalExtractor>() != null)
            {
                outputObject1.GetComponent<UniversalExtractor>().power = 0;
                outputObject1.GetComponent<UniversalExtractor>().speed = 1;
                outputObject1.GetComponent<UniversalExtractor>().powerON = false;
            }
            if (outputObject1.GetComponent<Smelter>() != null)
            {
                outputObject1.GetComponent<Smelter>().power = 0;
                outputObject1.GetComponent<Smelter>().speed = 1;
                outputObject1.GetComponent<Smelter>().powerON = false;
            }
            if (outputObject1.GetComponent<Press>() != null)
            {
                outputObject1.GetComponent<Press>().power = 0;
                outputObject1.GetComponent<Press>().speed = 1;
                outputObject1.GetComponent<Press>().powerON = false;
            }
            if (outputObject1.GetComponent<Extruder>() != null)
            {
                outputObject1.GetComponent<Extruder>().power = 0;
                outputObject1.GetComponent<Extruder>().speed = 0;
                outputObject1.GetComponent<Extruder>().powerON = false;
            }
            if (outputObject1.GetComponent<GearCutter>() != null)
            {
                outputObject1.GetComponent<GearCutter>().power = 0;
                outputObject1.GetComponent<GearCutter>().speed = 1;
                outputObject1.GetComponent<GearCutter>().powerON = false;
            }
            if (outputObject1.GetComponent<AlloySmelter>() != null)
            {
                outputObject1.GetComponent<AlloySmelter>().power = 0;
                outputObject1.GetComponent<AlloySmelter>().speed = 1;
                outputObject1.GetComponent<AlloySmelter>().powerON = false;
            }
            if (outputObject1.GetComponent<DarkMatterCollector>() != null)
            {
                outputObject1.GetComponent<DarkMatterCollector>().power = 0;
                outputObject1.GetComponent<DarkMatterCollector>().speed = 1;
                outputObject1.GetComponent<DarkMatterCollector>().powerON = false;
            }
            if (outputObject1.GetComponent<Turret>() != null)
            {
                outputObject1.GetComponent<Turret>().power = 0;
                outputObject1.GetComponent<Turret>().speed = 1;
                outputObject1.GetComponent<Turret>().powerON = false;
            }
            if (outputObject1.GetComponent<AutoCrafter>() != null)
            {
                outputObject1.GetComponent<AutoCrafter>().power = 0;
                outputObject1.GetComponent<AutoCrafter>().speed = 1;
                outputObject1.GetComponent<AutoCrafter>().powerON = false;
            }
            if (outputObject1.GetComponent<PowerConduit>() != null)
            {
                outputObject1.GetComponent<PowerConduit>().powerAmount = 0;
            }
        }
        if (outputObject2 != null)
        {
            if (outputObject2.GetComponent<ElectricLight>() != null)
            {
                outputObject2.GetComponent<ElectricLight>().powerON = false;
            }
            if (outputObject2.GetComponent<StorageComputer>() != null)
            {
                outputObject2.GetComponent<StorageComputer>().powerON = false;
            }
            if (outputObject2.GetComponent<Retriever>() != null)
            {
                outputObject2.GetComponent<Retriever>().power = 0;
                outputObject2.GetComponent<Retriever>().speed = 1;
                outputObject2.GetComponent<Retriever>().powerON = false;
            }
            if (outputObject2.GetComponent<Auger>() != null)
            {
                outputObject2.GetComponent<Auger>().power = 0;
                outputObject2.GetComponent<Auger>().speed = 1;
                outputObject2.GetComponent<Auger>().powerON = false;
            }
            if (outputObject2.GetComponent<UniversalExtractor>() != null)
            {
                outputObject2.GetComponent<UniversalExtractor>().power = 0;
                outputObject2.GetComponent<UniversalExtractor>().speed = 1;
                outputObject2.GetComponent<UniversalExtractor>().powerON = false;
            }
            if (outputObject2.GetComponent<Smelter>() != null)
            {
                outputObject2.GetComponent<Smelter>().power = 0;
                outputObject2.GetComponent<Smelter>().speed = 1;
                outputObject2.GetComponent<Smelter>().powerON = false;
            }
            if (outputObject2.GetComponent<Press>() != null)
            {
                outputObject2.GetComponent<Press>().power = 0;
                outputObject2.GetComponent<Press>().speed = 1;
                outputObject2.GetComponent<Press>().powerON = false;
            }
            if (outputObject2.GetComponent<Extruder>() != null)
            {
                outputObject2.GetComponent<Extruder>().power = 0;
                outputObject2.GetComponent<Extruder>().speed = 1;
                outputObject2.GetComponent<Extruder>().powerON = false;
            }
            if (outputObject2.GetComponent<GearCutter>() != null)
            {
                outputObject2.GetComponent<GearCutter>().power = 0;
                outputObject2.GetComponent<GearCutter>().speed = 1;
                outputObject2.GetComponent<GearCutter>().powerON = false;
            }
            if (outputObject2.GetComponent<AlloySmelter>() != null)
            {
                outputObject2.GetComponent<AlloySmelter>().power = 0;
                outputObject2.GetComponent<AlloySmelter>().speed = 1;
                outputObject2.GetComponent<AlloySmelter>().powerON = false;
            }
            if (outputObject2.GetComponent<DarkMatterCollector>() != null)
            {
                outputObject2.GetComponent<DarkMatterCollector>().power = 0;
                outputObject2.GetComponent<DarkMatterCollector>().speed = 1;
                outputObject2.GetComponent<DarkMatterCollector>().powerON = false;
            }
            if (outputObject2.GetComponent<Turret>() != null)
            {
                outputObject2.GetComponent<Turret>().power = 0;
                outputObject2.GetComponent<Turret>().speed = 1;
                outputObject2.GetComponent<Turret>().powerON = false;
            }
            if (outputObject2.GetComponent<AutoCrafter>() != null)
            {
                outputObject2.GetComponent<AutoCrafter>().power = 0;
                outputObject2.GetComponent<AutoCrafter>().speed = 1;
                outputObject2.GetComponent<AutoCrafter>().powerON = false;
            }
            if (outputObject2.GetComponent<PowerConduit>() != null)
            {
                outputObject2.GetComponent<PowerConduit>().powerAmount = 0;
            }
        }
    }

    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        if (powerReceiver.powerObject != null)
        {
            inputObject = powerReceiver.powerObject;
        }
        if (inputObject != null && inputObject.GetComponent<PowerSource>() != null)
        {
            powerAmount = powerReceiver.power;
        }
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

            updateTick = 0;
            if (outputObject1 == null && powerAmount > 0)
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
                                    if (obj.GetComponent<ElectricLight>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<ElectricLight>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<ElectricLight>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<ElectricLight>().powerON = true;
                                                        outputObject1.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<ElectricLight>().powerON = true;
                                                    outputObject1.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<StorageComputer>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<StorageComputer>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<StorageComputer>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<StorageComputer>().powerON = true;
                                                        outputObject1.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<StorageComputer>().powerON = true;
                                                    outputObject1.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Retriever>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Retriever>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Retriever>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Retriever>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Retriever>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Retriever>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Retriever>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<Retriever>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<Retriever>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Turret>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Turret>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Turret>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Turret>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Turret>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Turret>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Turret>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<Turret>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<Turret>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AutoCrafter>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<AutoCrafter>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AutoCrafter>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<AutoCrafter>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<AutoCrafter>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<AutoCrafter>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<AutoCrafter>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Smelter>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Smelter>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Smelter>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Smelter>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Smelter>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Smelter>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Smelter>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<Smelter>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<Smelter>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Extruder>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Extruder>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Extruder>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Extruder>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Extruder>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Extruder>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Extruder>().powerObject = this.gameObject;
                                                    outputObject1.GetComponent<Extruder>().power = powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<GearCutter>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<GearCutter>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<GearCutter>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<GearCutter>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<GearCutter>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<GearCutter>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<GearCutter>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AlloySmelter>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<AlloySmelter>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<AlloySmelter>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<AlloySmelter>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<AlloySmelter>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<AlloySmelter>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<UniversalExtractor>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<UniversalExtractor>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<UniversalExtractor>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<UniversalExtractor>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<UniversalExtractor>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<DarkMatterCollector>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<DarkMatterCollector>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Auger>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Auger>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Auger>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Auger>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Auger>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Auger>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Auger>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<Auger>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<Auger>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Press>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj.GetComponent<Press>().powerON == false)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Press>().ID.Equals(outputID1))
                                                    {
                                                        outputObject1 = obj;
                                                        outputObject1.GetComponent<Press>().powerObject = this.gameObject;
                                                        if (outputObject2 != null)
                                                        {
                                                            outputObject1.GetComponent<Press>().power = powerAmount / 2;
                                                        }
                                                        else
                                                        {
                                                            outputObject1.GetComponent<Press>().power = powerAmount;
                                                        }
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject1 = obj;
                                                    outputObject1.GetComponent<Press>().powerObject = this.gameObject;
                                                    if (outputObject2 != null)
                                                    {
                                                        outputObject1.GetComponent<Press>().power = powerAmount / 2;
                                                    }
                                                    else
                                                    {
                                                        outputObject1.GetComponent<Press>().power = powerAmount;
                                                    }
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<PowerConduit>() != null && outputObject1 == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().outputObject1 != this.gameObject)
                                                    {
                                                        if (obj.GetComponent<PowerConduit>().outputObject2 != null)
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().outputObject2 != this.gameObject)
                                                            {
                                                                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                                {
                                                                    if (creationMethod.Equals("spawned"))
                                                                    {
                                                                        if (obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
                                                                        {
                                                                            outputObject1 = obj;
                                                                            outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                            outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                            outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                            if (outputObject2 != null)
                                                                            {
                                                                                outputObject1.GetComponent<Press>().power = powerAmount / 2;
                                                                            }
                                                                            else
                                                                            {
                                                                                outputObject1.GetComponent<Press>().power = powerAmount;
                                                                            }
                                                                            connectionLine.SetPosition(0, transform.position);
                                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                                            connectionLine.enabled = true;
                                                                            creationMethod = "built";
                                                                        }
                                                                    }
                                                                    else if (creationMethod.Equals("built"))
                                                                    {
                                                                        outputObject1 = obj;
                                                                        outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                        if (outputObject2 != null)
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                        }
                                                                        connectionLine.SetPosition(0, transform.position);
                                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                                        connectionLine.enabled = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                            {
                                                                if (creationMethod.Equals("spawned"))
                                                                {
                                                                    if (obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
                                                                    {
                                                                        outputObject1 = obj;
                                                                        outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                        if (outputObject2 != null)
                                                                        {
                                                                            outputObject1.GetComponent<Press>().power = powerAmount / 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            outputObject1.GetComponent<Press>().power = powerAmount;
                                                                        }
                                                                        connectionLine.SetPosition(0, transform.position);
                                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                                        connectionLine.enabled = true;
                                                                        creationMethod = "built";
                                                                    }
                                                                }
                                                                else if (creationMethod.Equals("built"))
                                                                {
                                                                    outputObject1 = obj;
                                                                    outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                    outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                    outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                    if (outputObject2 != null)
                                                                    {
                                                                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                    }
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 != null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().outputObject2 != this.gameObject)
                                                    {
                                                        if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().outputObject1 != this.gameObject)
                                                            {
                                                                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                                {
                                                                    if (creationMethod.Equals("spawned"))
                                                                    {
                                                                        if (obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
                                                                        {
                                                                            outputObject1 = obj;
                                                                            outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                            outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                            outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                            if (outputObject2 != null)
                                                                            {
                                                                                outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                            }
                                                                            else
                                                                            {
                                                                                outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                            }
                                                                            connectionLine.SetPosition(0, transform.position);
                                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                                            connectionLine.enabled = true;
                                                                            creationMethod = "built";
                                                                        }
                                                                    }
                                                                    else if (creationMethod.Equals("built"))
                                                                    {
                                                                        outputObject1 = obj;
                                                                        outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                        if (outputObject2 != null)
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                        }
                                                                        connectionLine.SetPosition(0, transform.position);
                                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                                        connectionLine.enabled = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                            {
                                                                if (creationMethod.Equals("spawned"))
                                                                {
                                                                    if (obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
                                                                    {
                                                                        outputObject1 = obj;
                                                                        outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                        outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                        if (outputObject2 != null)
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                        }
                                                                        connectionLine.SetPosition(0, transform.position);
                                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                                        connectionLine.enabled = true;
                                                                        creationMethod = "built";
                                                                    }
                                                                }
                                                                else if (creationMethod.Equals("built"))
                                                                {
                                                                    outputObject1 = obj;
                                                                    outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                    outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                    outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                    if (outputObject2 != null)
                                                                    {
                                                                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                    }
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 == null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                    {
                                                        if (creationMethod.Equals("spawned"))
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
                                                            {
                                                                outputObject1 = obj;
                                                                outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                                outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                                outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                if (outputObject2 != null)
                                                                {
                                                                    outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                }
                                                                else
                                                                {
                                                                    outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                                }
                                                                connectionLine.SetPosition(0, transform.position);
                                                                connectionLine.SetPosition(1, obj.transform.position);
                                                                connectionLine.enabled = true;
                                                                creationMethod = "built";
                                                            }
                                                        }
                                                        else if (creationMethod.Equals("built"))
                                                        {
                                                            outputObject1 = obj;
                                                            outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                                                            outputObject1.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            if (outputObject2 != null)
                                                            {
                                                                outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                            }
                                                            else
                                                            {
                                                                outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                                                            }
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
                }
            }
            if (outputObject1 != null && outputObject2 == null && powerAmount > 0 && dualOutput == true)
            {
                dualConnectionAttempts += 1;
                if (creationMethod.Equals("spawned"))
                {
                    if (dualConnectionAttempts >= 15)
                    {
                        dualConnectionAttempts = 0;
                        dualOutput = false;
                    }
                }
                else
                {
                    if (dualConnectionAttempts >= 60)
                    {
                        dualConnectionAttempts = 0;
                        dualOutput = false;
                    }
                }
                if (dualOutput == true)
                {
                    GameObject[] allObjects = FindObjectsOfType<GameObject>();
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj.GetComponent<ElectricLight>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<ElectricLight>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<ElectricLight>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<ElectricLight>().powerON = true;
                                            outputObject2.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<ElectricLight>().powerON = true;
                                        outputObject2.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<StorageComputer>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<StorageComputer>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<StorageComputer>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<StorageComputer>().powerON = true;
                                            outputObject2.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<StorageComputer>().powerON = true;
                                        outputObject2.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Retriever>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Retriever>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Retriever>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Retriever>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Retriever>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Retriever>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Retriever>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Turret>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Turret>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Turret>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Turret>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Turret>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Turret>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Turret>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<AutoCrafter>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<AutoCrafter>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<AutoCrafter>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<AutoCrafter>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<AutoCrafter>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Smelter>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Smelter>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Smelter>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Smelter>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Smelter>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Smelter>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Smelter>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Extruder>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Extruder>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Extruder>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Extruder>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Extruder>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Extruder>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Extruder>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<GearCutter>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<GearCutter>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<GearCutter>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<GearCutter>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<GearCutter>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<GearCutter>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<GearCutter>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<AlloySmelter>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<AlloySmelter>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<AlloySmelter>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<AlloySmelter>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<UniversalExtractor>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<UniversalExtractor>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<UniversalExtractor>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<DarkMatterCollector>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<DarkMatterCollector>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Auger>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Auger>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Auger>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Auger>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Auger>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Auger>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Auger>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<Press>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<Press>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<Press>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<Press>().powerObject = this.gameObject;
                                            outputObject2.GetComponent<Press>().power = powerAmount / 2;
                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                            connectionLine2.SetActive(true);
                                            connectionLine2.transform.parent = outputObject2.transform;
                                            connectionLine2.AddComponent<LineRenderer>();
                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            creationMethod = "built";
                                        }
                                    }
                                    else if (creationMethod.Equals("built"))
                                    {
                                        outputObject2 = obj;
                                        outputObject2.GetComponent<Press>().powerObject = this.gameObject;
                                        outputObject2.GetComponent<Press>().power = powerAmount / 2;
                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                        connectionLine2.SetActive(true);
                                        connectionLine2.transform.parent = outputObject2.transform;
                                        connectionLine2.AddComponent<LineRenderer>();
                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                    }
                                }
                            }
                        }
                        if (obj.GetComponent<PowerConduit>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj != this.gameObject)
                                {
                                    if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                    {
                                        if (obj.GetComponent<PowerConduit>().outputObject1 != this.gameObject)
                                        {
                                            if (obj.GetComponent<PowerConduit>().outputObject2 != null)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject2 != this.gameObject)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                    {
                                                        if (creationMethod.Equals("spawned"))
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
                                                            {
                                                                outputObject2 = obj;
                                                                outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                                outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                                outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                                connectionLine2.SetActive(true);
                                                                connectionLine2.transform.parent = outputObject2.transform;
                                                                connectionLine2.AddComponent<LineRenderer>();
                                                                connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                                connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                                connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                                creationMethod = "built";
                                                            }
                                                        }
                                                        else if (creationMethod.Equals("built"))
                                                        {
                                                            outputObject2 = obj;
                                                            outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                            connectionLine2.SetActive(true);
                                                            connectionLine2.transform.parent = outputObject2.transform;
                                                            connectionLine2.AddComponent<LineRenderer>();
                                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
                                                        {
                                                            outputObject2 = obj;
                                                            outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                            connectionLine2.SetActive(true);
                                                            connectionLine2.transform.parent = outputObject2.transform;
                                                            connectionLine2.AddComponent<LineRenderer>();
                                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject2 = obj;
                                                        outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                        outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                        outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                        outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                        connectionLine2.SetActive(true);
                                                        connectionLine2.transform.parent = outputObject2.transform;
                                                        connectionLine2.AddComponent<LineRenderer>();
                                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (outputObject2 == null && obj.GetComponent<PowerConduit>().outputObject2 != null)
                                    {
                                        if (obj.GetComponent<PowerConduit>().outputObject2 != this.gameObject)
                                        {
                                            if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject1 != this.gameObject)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                    {
                                                        if (creationMethod.Equals("spawned"))
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
                                                            {
                                                                outputObject2 = obj;
                                                                outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                                outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                                outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                                connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                                connectionLine2.SetActive(true);
                                                                connectionLine2.transform.parent = outputObject2.transform;
                                                                connectionLine2.AddComponent<LineRenderer>();
                                                                connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                                connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                                connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                                creationMethod = "built";
                                                            }
                                                        }
                                                        else if (creationMethod.Equals("built"))
                                                        {
                                                            outputObject2 = obj;
                                                            outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                            connectionLine2.SetActive(true);
                                                            connectionLine2.transform.parent = outputObject2.transform;
                                                            connectionLine2.AddComponent<LineRenderer>();
                                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
                                                        {
                                                            outputObject2 = obj;
                                                            outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                            connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                            connectionLine2.SetActive(true);
                                                            connectionLine2.transform.parent = outputObject2.transform;
                                                            connectionLine2.AddComponent<LineRenderer>();
                                                            connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                            connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                            connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject2 = obj;
                                                        outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                        outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                        outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                        outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                        connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                        connectionLine2.SetActive(true);
                                                        connectionLine2.transform.parent = outputObject2.transform;
                                                        connectionLine2.AddComponent<LineRenderer>();
                                                        connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                        connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                        connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                        connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (outputObject2 == null && obj.GetComponent<PowerConduit>().outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 == null)
                                    {
                                        if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                                        {
                                            if (creationMethod.Equals("spawned"))
                                            {
                                                if (obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
                                                {
                                                    outputObject2 = obj;
                                                    outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                    outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                    outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                    outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                    connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                    connectionLine2.SetActive(true);
                                                    connectionLine2.transform.parent = outputObject2.transform;
                                                    connectionLine2.AddComponent<LineRenderer>();
                                                    connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                    connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                    connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                    connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                    connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                                    creationMethod = "built";
                                                }
                                            }
                                            else if (creationMethod.Equals("built"))
                                            {
                                                outputObject2 = obj;
                                                outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                                                outputObject2.GetComponent<PowerConduit>().inputID = ID;
                                                outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                                                connectionLine2 = Instantiate(connectionObject, outputObject2.transform.position, outputObject2.transform.rotation);
                                                connectionLine2.SetActive(true);
                                                connectionLine2.transform.parent = outputObject2.transform;
                                                connectionLine2.AddComponent<LineRenderer>();
                                                connectionLine2.GetComponent<LineRenderer>().startWidth = 0.2f;
                                                connectionLine2.GetComponent<LineRenderer>().endWidth = 0.2f;
                                                connectionLine2.GetComponent<LineRenderer>().material = lineMat;
                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                                                connectionLine2.GetComponent<LineRenderer>().SetPosition(1, outputObject2.transform.position);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (outputObject1 != null && connectionFailed == false)
            {
                if (outputObject1.GetComponent<Retriever>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Retriever>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Retriever>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Retriever>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Retriever>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Retriever>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Retriever>().ID;
                }
                if (outputObject1.GetComponent<Smelter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Smelter>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Smelter>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Smelter>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Smelter>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Smelter>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Smelter>().ID;
                }
                if (outputObject1.GetComponent<Turret>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Turret>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Turret>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Turret>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Turret>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Turret>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Turret>().ID;
                }
                if (outputObject1.GetComponent<AutoCrafter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<AutoCrafter>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<AutoCrafter>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<AutoCrafter>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<AutoCrafter>().power = powerAmount;
                    }
                    outputObject1.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<AutoCrafter>().ID;
                }
                if (outputObject1.GetComponent<Press>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Press>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Press>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Press>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Press>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Press>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Press>().ID;
                }
                if (outputObject1.GetComponent<AlloySmelter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<AlloySmelter>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<AlloySmelter>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<AlloySmelter>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<AlloySmelter>().power = powerAmount;
                    }
                    outputObject1.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<AlloySmelter>().ID;
                }
                if (outputObject1.GetComponent<Extruder>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Extruder>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Extruder>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Extruder>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Extruder>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Extruder>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Extruder>().ID;
                }
                if (outputObject1.GetComponent<GearCutter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<GearCutter>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<GearCutter>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<GearCutter>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<GearCutter>().power = powerAmount;
                    }
                    outputObject1.GetComponent<GearCutter>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<GearCutter>().ID;
                }
                if (outputObject1.GetComponent<UniversalExtractor>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<UniversalExtractor>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<UniversalExtractor>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<UniversalExtractor>().power = powerAmount;
                    }
                    outputObject1.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<UniversalExtractor>().ID;
                }
                if (outputObject1.GetComponent<DarkMatterCollector>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<DarkMatterCollector>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<DarkMatterCollector>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<DarkMatterCollector>().power = powerAmount;
                    }
                    outputObject1.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<DarkMatterCollector>().ID;
                }
                if (outputObject1.GetComponent<Auger>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<Auger>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<Auger>().powerON = false;
                    }
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<Auger>().power = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<Auger>().power = powerAmount;
                    }
                    outputObject1.GetComponent<Auger>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<Auger>().ID;
                }
                if (outputObject1.GetComponent<ElectricLight>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<ElectricLight>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<ElectricLight>().powerON = false;
                    }
                    outputObject1.GetComponent<ElectricLight>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<ElectricLight>().ID;
                }
                if (outputObject1.GetComponent<StorageComputer>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject1.GetComponent<StorageComputer>().powerON = true;
                    }
                    else
                    {
                        outputObject1.GetComponent<StorageComputer>().powerON = false;
                    }
                    outputObject1.GetComponent<StorageComputer>().powerObject = this.gameObject;
                    outputID1 = outputObject1.GetComponent<StorageComputer>().ID;
                }
                if (outputObject1.GetComponent<PowerConduit>() != null)
                {
                    outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
                    outputObject1.GetComponent<PowerConduit>().inputID = ID;
                    outputObject1.GetComponent<PowerConduit>().inputObject = this.gameObject;
                    if (outputObject2 != null)
                    {
                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                    }
                    else
                    {
                        outputObject1.GetComponent<PowerConduit>().powerAmount = powerAmount;
                    }
                }
            }
            if (outputObject2 != null && connectionFailed == false)
            {
                if (outputObject2.GetComponent<Retriever>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Retriever>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Retriever>().powerON = false;
                    }
                    outputObject2.GetComponent<Retriever>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Retriever>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Retriever>().ID;
                }
                if (outputObject2.GetComponent<Smelter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Smelter>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Smelter>().powerON = false;
                    }
                    outputObject2.GetComponent<Smelter>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Smelter>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Smelter>().ID;
                }
                if (outputObject2.GetComponent<Turret>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Turret>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Turret>().powerON = false;
                    }
                    outputObject2.GetComponent<Turret>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Turret>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Turret>().ID;
                }
                if (outputObject2.GetComponent<AutoCrafter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<AutoCrafter>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<AutoCrafter>().powerON = false;
                    }
                    outputObject2.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                    outputObject2.GetComponent<AutoCrafter>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<AutoCrafter>().ID;
                }
                if (outputObject2.GetComponent<Press>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Press>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Press>().powerON = false;
                    }
                    outputObject2.GetComponent<Press>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Press>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Press>().ID;
                }
                if (outputObject2.GetComponent<AlloySmelter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<AlloySmelter>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<AlloySmelter>().powerON = false;
                    }
                    outputObject2.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                    outputObject2.GetComponent<AlloySmelter>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<AlloySmelter>().ID;
                }
                if (outputObject2.GetComponent<Extruder>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Extruder>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Extruder>().powerON = false;
                    }
                    outputObject2.GetComponent<Extruder>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Extruder>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Extruder>().ID;
                }
                if (outputObject2.GetComponent<GearCutter>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<GearCutter>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<GearCutter>().powerON = false;
                    }
                    outputObject2.GetComponent<GearCutter>().powerObject = this.gameObject;
                    outputObject2.GetComponent<GearCutter>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<GearCutter>().ID;
                }
                if (outputObject2.GetComponent<UniversalExtractor>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<UniversalExtractor>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<UniversalExtractor>().powerON = false;
                    }
                    outputObject2.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                    outputObject2.GetComponent<UniversalExtractor>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<UniversalExtractor>().ID;
                }
                if (outputObject2.GetComponent<DarkMatterCollector>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<DarkMatterCollector>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<DarkMatterCollector>().powerON = false;
                    }
                    outputObject2.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                    outputObject2.GetComponent<DarkMatterCollector>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<DarkMatterCollector>().ID;
                }
                if (outputObject2.GetComponent<Auger>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<Auger>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<Auger>().powerON = false;
                    }
                    outputObject2.GetComponent<Auger>().powerObject = this.gameObject;
                    outputObject2.GetComponent<Auger>().power = powerAmount / 2;
                    outputID2 = outputObject2.GetComponent<Auger>().ID;
                }
                if (outputObject2.GetComponent<ElectricLight>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<ElectricLight>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<ElectricLight>().powerON = false;
                    }
                    outputObject2.GetComponent<ElectricLight>().powerObject = this.gameObject;
                    outputID2 = outputObject2.GetComponent<ElectricLight>().ID;
                }
                if (outputObject2.GetComponent<StorageComputer>() != null)
                {
                    if (powerAmount > 0)
                    {
                        outputObject2.GetComponent<StorageComputer>().powerON = true;
                    }
                    else
                    {
                        outputObject2.GetComponent<StorageComputer>().powerON = false;
                    }
                    outputObject2.GetComponent<StorageComputer>().powerObject = this.gameObject;
                    outputID2 = outputObject2.GetComponent<StorageComputer>().ID;
                }
                if (outputObject2.GetComponent<PowerConduit>() != null)
                {
                    outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
                    outputObject2.GetComponent<PowerConduit>().inputID = ID;
                    outputObject2.GetComponent<PowerConduit>().inputObject = this.gameObject;
                    outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
                }
            }
            if (inputObject != null)
            {
                if (inputObject.GetComponent<PowerSource>() != null)
                {
                    if (inputObject.GetComponent<PowerSource>().outputObject != this.gameObject)
                    {
                        inputObject = null;
                        powerAmount = 0;
                    }
                }
            }
            if (inputObject != null)
            {
                if (inputObject.GetComponent<PowerConduit>() != null)
                {
                    if (inputObject.GetComponent<PowerConduit>().outputObject1 != this.gameObject && inputObject.GetComponent<PowerConduit>().outputObject2 != this.gameObject)
                    {
                        inputObject = null;
                        powerAmount = 0;
                    }
                }
            }
            if (outputObject1 == null)
            {
                connectionLine.enabled = false;
            }
            if (outputObject1 == null || outputObject2 == null)
            {
                if (connectionFailed == true)
                {
                    if (creationMethod.Equals("spawned"))
                    {
                        creationMethod = "built";
                    }
                }
            }
            if (powerAmount < 1)
            {
                GetComponent<AudioSource>().Stop();
            }
            else
            {
                if (GetComponent<AudioSource>().isPlaying == false)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}

