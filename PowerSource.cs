using UnityEngine;
using System.Collections;

public class PowerSource : MonoBehaviour
{
    public string ID = "unassigned";
    public Material lineMat;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    public string inputID;
    public string outputID;
    LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public int powerAmount;
    public string type;
    public string fuelType;
    public int fuelAmount;
    public int connectionAttempts;
    public bool connectionFailed;
    public bool blocked;
    public bool outOfFuel;
    public bool noReactor;
    private int fuelConsumptionTimer;
    public Texture2D generatorOffTexture;
    public Texture2D generatorOnTexture;
    private GameObject builtObjects;

    void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    void OnDestroy()
    {
        if (outputObject != null)
        {
            if (outputObject.GetComponent<ElectricLight>() != null)
            {
                outputObject.GetComponent<ElectricLight>().powerON = false;
            }
            if (outputObject.GetComponent<StorageComputer>() != null)
            {
                outputObject.GetComponent<StorageComputer>().powerON = false;
            }
            if (outputObject.GetComponent<Retriever>() != null)
            {
                if (outputObject.GetComponent<Retriever>().power >= powerAmount)
                {
                    outputObject.GetComponent<Retriever>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Retriever>().power < 1)
                {
                    outputObject.GetComponent<Retriever>().powerON = false;
                }
                if (outputObject.GetComponent<Retriever>().power < outputObject.GetComponent<Retriever>().speed && outputObject.GetComponent<Retriever>().speed > 1)
                {
                    outputObject.GetComponent<Retriever>().speed = outputObject.GetComponent<Retriever>().power;
                }
            }
            if (outputObject.GetComponent<Auger>() != null)
            {
                if (outputObject.GetComponent<Auger>().power >= powerAmount)
                {
                    outputObject.GetComponent<Auger>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Auger>().power < 1)
                {
                    outputObject.GetComponent<Auger>().powerON = false;
                }
                if (outputObject.GetComponent<Auger>().power < outputObject.GetComponent<Auger>().speed && outputObject.GetComponent<Auger>().speed > 1)
                {
                    outputObject.GetComponent<Auger>().speed = outputObject.GetComponent<Auger>().power;
                }
            }
            if (outputObject.GetComponent<UniversalExtractor>() != null)
            {
                if (outputObject.GetComponent<UniversalExtractor>().power >= powerAmount)
                {
                    outputObject.GetComponent<UniversalExtractor>().power -= powerAmount;
                }
                if (outputObject.GetComponent<UniversalExtractor>().power < 1)
                {
                    outputObject.GetComponent<UniversalExtractor>().powerON = false;
                }
                if (outputObject.GetComponent<UniversalExtractor>().power < outputObject.GetComponent<UniversalExtractor>().speed && outputObject.GetComponent<UniversalExtractor>().speed > 1)
                {
                    outputObject.GetComponent<UniversalExtractor>().speed = outputObject.GetComponent<UniversalExtractor>().power;
                }
            }
            if (outputObject.GetComponent<Smelter>() != null)
            {
                if (outputObject.GetComponent<Smelter>().power >= powerAmount)
                {
                    outputObject.GetComponent<Smelter>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Smelter>().power < 1)
                {
                    outputObject.GetComponent<Smelter>().powerON = false;
                }
                if (outputObject.GetComponent<Smelter>().power < outputObject.GetComponent<Smelter>().speed && outputObject.GetComponent<Smelter>().speed > 1)
                {
                    outputObject.GetComponent<Smelter>().speed = outputObject.GetComponent<Smelter>().power;
                }
            }
            if (outputObject.GetComponent<Press>() != null)
            {
                if (outputObject.GetComponent<Press>().power >= powerAmount)
                {
                    outputObject.GetComponent<Press>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Press>().power < 1)
                {
                    outputObject.GetComponent<Press>().powerON = false;
                }
                if (outputObject.GetComponent<Press>().power < outputObject.GetComponent<Press>().speed && outputObject.GetComponent<Press>().speed > 1)
                {
                    outputObject.GetComponent<Press>().speed = outputObject.GetComponent<Press>().power;
                }
            }
            if (outputObject.GetComponent<Extruder>() != null)
            {
                if (outputObject.GetComponent<Extruder>().power >= powerAmount)
                {
                    outputObject.GetComponent<Extruder>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Extruder>().power < 1)
                {
                    outputObject.GetComponent<Extruder>().powerON = false;
                }
                if (outputObject.GetComponent<Extruder>().power < outputObject.GetComponent<Extruder>().speed && outputObject.GetComponent<Extruder>().speed > 1)
                {
                    outputObject.GetComponent<Extruder>().speed = outputObject.GetComponent<Extruder>().power;
                }
            }
            if (outputObject.GetComponent<GearCutter>() != null)
            {
                if (outputObject.GetComponent<GearCutter>().power >= powerAmount)
                {
                    outputObject.GetComponent<GearCutter>().power -= powerAmount;
                }
                if (outputObject.GetComponent<GearCutter>().power < 1)
                {
                    outputObject.GetComponent<GearCutter>().powerON = false;
                }
                if (outputObject.GetComponent<GearCutter>().power < outputObject.GetComponent<GearCutter>().speed && outputObject.GetComponent<GearCutter>().speed > 1)
                {
                    outputObject.GetComponent<GearCutter>().speed = outputObject.GetComponent<GearCutter>().power;
                }
            }
            if (outputObject.GetComponent<AlloySmelter>() != null)
            {
                if (outputObject.GetComponent<AlloySmelter>().power >= powerAmount)
                {
                    outputObject.GetComponent<AlloySmelter>().power -= powerAmount;
                }
                if (outputObject.GetComponent<AlloySmelter>().power < 1)
                {
                    outputObject.GetComponent<AlloySmelter>().powerON = false;
                }
                if (outputObject.GetComponent<AlloySmelter>().power < outputObject.GetComponent<AlloySmelter>().speed && outputObject.GetComponent<AlloySmelter>().speed > 1)
                {
                    outputObject.GetComponent<AlloySmelter>().speed = outputObject.GetComponent<AlloySmelter>().power;
                }
            }
            if (outputObject.GetComponent<DarkMatterCollector>() != null)
            {
                if (outputObject.GetComponent<DarkMatterCollector>().power >= powerAmount)
                {
                    outputObject.GetComponent<DarkMatterCollector>().power -= powerAmount;
                }
                if (outputObject.GetComponent<DarkMatterCollector>().power < 1)
                {
                    outputObject.GetComponent<DarkMatterCollector>().powerON = false;
                }
                if (outputObject.GetComponent<DarkMatterCollector>().power < outputObject.GetComponent<DarkMatterCollector>().speed && outputObject.GetComponent<DarkMatterCollector>().speed > 1)
                {
                    outputObject.GetComponent<DarkMatterCollector>().speed = outputObject.GetComponent<DarkMatterCollector>().power;
                }
            }
            if (outputObject.GetComponent<Turret>() != null)
            {
                if (outputObject.GetComponent<Turret>().power >= powerAmount)
                {
                    outputObject.GetComponent<Turret>().power -= powerAmount;
                }
                if (outputObject.GetComponent<Turret>().power < 1)
                {
                    outputObject.GetComponent<Turret>().powerON = false;
                }
                if (outputObject.GetComponent<Turret>().power < outputObject.GetComponent<Turret>().speed && outputObject.GetComponent<Turret>().speed > 1)
                {
                    outputObject.GetComponent<Turret>().speed = outputObject.GetComponent<Turret>().power;
                }
            }
            if (outputObject.GetComponent<AutoCrafter>() != null)
            {
                if (outputObject.GetComponent<AutoCrafter>().power >= powerAmount)
                {
                    outputObject.GetComponent<AutoCrafter>().power -= powerAmount;
                }
                if (outputObject.GetComponent<AutoCrafter>().power < 1)
                {
                    outputObject.GetComponent<AutoCrafter>().powerON = false;
                }
                if (outputObject.GetComponent<AutoCrafter>().power < outputObject.GetComponent<AutoCrafter>().speed && outputObject.GetComponent<AutoCrafter>().speed > 1)
                {
                    outputObject.GetComponent<AutoCrafter>().speed = outputObject.GetComponent<AutoCrafter>().power;
                }
            }
            if (outputObject.GetComponent<PowerConduit>() != null)
            {
                if (outputObject.GetComponent<PowerConduit>().powerAmount >= powerAmount)
                {
                    outputObject.GetComponent<PowerConduit>().powerAmount -= powerAmount;
                }
            }
        }
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
            if (outputObject == null)
            {
                //Debug.Log(ID + " Searching as "+creationMethod);
                connectionAttempts += 1;
                if (connectionAttempts >= 120)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
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
                                    if (obj.GetComponent<ElectricLight>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<ElectricLight>().powerObject == null)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<ElectricLight>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<ElectricLight>().powerON = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<ElectricLight>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<ElectricLight>().powerON = true;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<StorageComputer>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<StorageComputer>().powerObject == null)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<StorageComputer>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<StorageComputer>().powerON = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<StorageComputer>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<StorageComputer>().powerON = true;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Retriever>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Retriever>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Retriever>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Retriever>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Retriever>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Retriever>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Retriever>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Retriever>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Retriever>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Retriever>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Retriever>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Retriever>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Retriever>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Turret>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Turret>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Turret>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Turret>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Turret>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Turret>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Turret>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Turret>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Turret>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Turret>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Turret>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Turret>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Turret>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Smelter>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Smelter>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Smelter>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Smelter>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Smelter>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Smelter>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Smelter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Smelter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Smelter>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Smelter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Smelter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Smelter>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Smelter>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Extruder>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Extruder>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Extruder>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Extruder>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Extruder>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Extruder>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Extruder>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Extruder>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Extruder>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Extruder>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Extruder>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Extruder>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Extruder>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<GearCutter>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<GearCutter>().powerObject != null)
                                            {
                                                if (obj.GetComponent<GearCutter>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<GearCutter>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<GearCutter>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<GearCutter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<GearCutter>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<GearCutter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<GearCutter>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<GearCutter>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AlloySmelter>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<AlloySmelter>().powerObject != null)
                                            {
                                                if (obj.GetComponent<AlloySmelter>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<UniversalExtractor>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<UniversalExtractor>().powerObject != null)
                                            {
                                                if (obj.GetComponent<UniversalExtractor>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<UniversalExtractor>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<UniversalExtractor>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<DarkMatterCollector>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<DarkMatterCollector>().powerObject != null)
                                            {
                                                if (obj.GetComponent<DarkMatterCollector>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }

                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Auger>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Auger>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Auger>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Auger>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Auger>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Auger>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Auger>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Auger>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Auger>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Auger>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Auger>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Auger>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Auger>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Press>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<Press>().powerObject != null)
                                            {
                                                if (obj.GetComponent<Press>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<Press>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Press>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<Press>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Press>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Press>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Press>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Press>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<Press>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<Press>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<Press>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AutoCrafter>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj.GetComponent<AutoCrafter>().powerObject != null)
                                            {
                                                if (obj.GetComponent<AutoCrafter>().powerObject.GetComponent<PowerConduit>() == null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<AutoCrafter>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                            outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AutoCrafter>().ID.Equals(outputID))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                        outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = obj;
                                                    outputObject.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                                                    outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                                                    connectionLine.SetPosition(0, transform.position);
                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                    connectionLine.enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<PowerConduit>() != null && outputObject == null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 40)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (obj.GetComponent<PowerConduit>().inputObject != null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().inputObject.GetComponent<PowerConduit>() == null)
                                                    {
                                                        if (creationMethod.Equals("spawned"))
                                                        {
                                                            if (obj.GetComponent<PowerConduit>().ID.Equals(outputID))
                                                            {
                                                                outputObject = obj;
                                                                outputObject.GetComponent<PowerConduit>().inputID = ID;
                                                                outputObject.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                                outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                                                                connectionLine.SetPosition(0, transform.position);
                                                                connectionLine.SetPosition(1, obj.transform.position);
                                                                connectionLine.enabled = true;
                                                                creationMethod = "built";
                                                            }
                                                        }
                                                        else if (creationMethod.Equals("built"))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        if (obj.GetComponent<PowerConduit>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<PowerConduit>().inputID = ID;
                                                            outputObject.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                            outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                    else if (creationMethod.Equals("built"))
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<PowerConduit>().inputID = ID;
                                                        outputObject.GetComponent<PowerConduit>().inputObject = this.gameObject;
                                                        outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
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
            if (outputObject != null && connectionFailed == false)
            {
                connectionAttempts = 0;
                //Debug.Log(ID+" connected to "+outputObject.name);
                if (outputObject.GetComponent<Retriever>() != null)
                {
                    outputObject.GetComponent<Retriever>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Retriever>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Retriever>().power += powerAmount;
                                }
                                outputObject.GetComponent<Retriever>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Retriever>().power -= powerAmount;
                                    if (outputObject.GetComponent<Retriever>().power < 1)
                                    {
                                        outputObject.GetComponent<Retriever>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Retriever>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Retriever>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Retriever>().power -= powerAmount;
                                if (outputObject.GetComponent<Retriever>().power < 1)
                                {
                                    outputObject.GetComponent<Retriever>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position,transform.up,out RaycastHit reactorUpHit,3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Retriever>().power += powerAmount;
                            }
                            outputObject.GetComponent<Retriever>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Retriever>().power -= powerAmount;
                                if (outputObject.GetComponent<Retriever>().power < 1)
                                {
                                    outputObject.GetComponent<Retriever>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<Smelter>() != null)
                {
                    outputObject.GetComponent<Smelter>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Smelter>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Smelter>().power += powerAmount;
                                }
                                outputObject.GetComponent<Smelter>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Smelter>().power -= powerAmount;
                                    if (outputObject.GetComponent<Smelter>().power < 1)
                                    {
                                        outputObject.GetComponent<Smelter>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Smelter>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Smelter>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Smelter>().power -= powerAmount;
                                if (outputObject.GetComponent<Smelter>().power < 1)
                                {
                                    outputObject.GetComponent<Smelter>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Smelter>().power += powerAmount;
                            }
                            outputObject.GetComponent<Smelter>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Smelter>().power -= powerAmount;
                                if (outputObject.GetComponent<Smelter>().power < 1)
                                {
                                    outputObject.GetComponent<Smelter>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<Turret>() != null)
                {
                    outputObject.GetComponent<Turret>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Turret>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Turret>().power += powerAmount;
                                }
                                outputObject.GetComponent<Turret>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Turret>().power -= powerAmount;
                                    if (outputObject.GetComponent<Turret>().power < 1)
                                    {
                                        outputObject.GetComponent<Turret>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Turret>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Turret>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Turret>().power -= powerAmount;
                                if (outputObject.GetComponent<Turret>().power < 1)
                                {
                                    outputObject.GetComponent<Turret>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Turret>().power += powerAmount;
                            }
                            outputObject.GetComponent<Turret>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Turret>().power -= powerAmount;
                                if (outputObject.GetComponent<Turret>().power < 1)
                                {
                                    outputObject.GetComponent<Turret>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<AutoCrafter>() != null)
                {
                    outputObject.GetComponent<AutoCrafter>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<AutoCrafter>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                                }
                                outputObject.GetComponent<AutoCrafter>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<AutoCrafter>().power -= powerAmount;
                                    if (outputObject.GetComponent<AutoCrafter>().power < 1)
                                    {
                                        outputObject.GetComponent<AutoCrafter>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<AutoCrafter>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<AutoCrafter>().power -= powerAmount;
                                if (outputObject.GetComponent<AutoCrafter>().power < 1)
                                {
                                    outputObject.GetComponent<AutoCrafter>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                            }
                            outputObject.GetComponent<AutoCrafter>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<AutoCrafter>().power -= powerAmount;
                                if (outputObject.GetComponent<AutoCrafter>().power < 1)
                                {
                                    outputObject.GetComponent<AutoCrafter>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<Press>() != null)
                {
                    outputObject.GetComponent<Press>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Press>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Press>().power += powerAmount;
                                }
                                outputObject.GetComponent<Press>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Press>().power -= powerAmount;
                                    if (outputObject.GetComponent<Press>().power < 1)
                                    {
                                        outputObject.GetComponent<Press>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Press>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Press>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Press>().power -= powerAmount;
                                if (outputObject.GetComponent<Press>().power < 1)
                                {
                                    outputObject.GetComponent<Press>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Press>().power += powerAmount;
                            }
                            outputObject.GetComponent<Press>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Press>().power -= powerAmount;
                                if (outputObject.GetComponent<Press>().power < 1)
                                {
                                    outputObject.GetComponent<Press>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<AlloySmelter>() != null)
                {
                    outputObject.GetComponent<AlloySmelter>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<AlloySmelter>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                                }
                                outputObject.GetComponent<AlloySmelter>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<AlloySmelter>().power -= powerAmount;
                                    if (outputObject.GetComponent<AlloySmelter>().power < 1)
                                    {
                                        outputObject.GetComponent<AlloySmelter>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<AlloySmelter>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<AlloySmelter>().power -= powerAmount;
                                if (outputObject.GetComponent<AlloySmelter>().power < 1)
                                {
                                    outputObject.GetComponent<AlloySmelter>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                            }
                            outputObject.GetComponent<AlloySmelter>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<AlloySmelter>().power -= powerAmount;
                                if (outputObject.GetComponent<AlloySmelter>().power < 1)
                                {
                                    outputObject.GetComponent<AlloySmelter>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<Extruder>() != null)
                {
                    outputObject.GetComponent<Extruder>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Extruder>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Extruder>().power += powerAmount;
                                }
                                outputObject.GetComponent<Extruder>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Extruder>().power -= powerAmount;
                                    if (outputObject.GetComponent<Extruder>().power < 1)
                                    {
                                        outputObject.GetComponent<Extruder>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Extruder>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Extruder>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Extruder>().power -= powerAmount;
                                if (outputObject.GetComponent<Extruder>().power < 1)
                                {
                                    outputObject.GetComponent<Extruder>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Extruder>().power += powerAmount;
                            }
                            outputObject.GetComponent<Extruder>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Extruder>().power -= powerAmount;
                                if (outputObject.GetComponent<Extruder>().power < 1)
                                {
                                    outputObject.GetComponent<Extruder>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<GearCutter>() != null)
                {
                    outputObject.GetComponent<GearCutter>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<GearCutter>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<GearCutter>().power += powerAmount;
                                }
                                outputObject.GetComponent<GearCutter>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<GearCutter>().power -= powerAmount;
                                    if (outputObject.GetComponent<GearCutter>().power < 1)
                                    {
                                        outputObject.GetComponent<GearCutter>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<GearCutter>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<GearCutter>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<GearCutter>().power -= powerAmount;
                                if (outputObject.GetComponent<GearCutter>().power < 1)
                                {
                                    outputObject.GetComponent<GearCutter>().powerON = false;
                                }
                            }
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            GetComponent<Light>().enabled = false;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<GearCutter>().power += powerAmount;
                            }
                            outputObject.GetComponent<GearCutter>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<GearCutter>().power -= powerAmount;
                                if (outputObject.GetComponent<GearCutter>().power < 1)
                                {
                                    outputObject.GetComponent<GearCutter>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<UniversalExtractor>() != null)
                {
                    outputObject.GetComponent<UniversalExtractor>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<UniversalExtractor>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                                }
                                outputObject.GetComponent<UniversalExtractor>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<UniversalExtractor>().power -= powerAmount;
                                    if (outputObject.GetComponent<UniversalExtractor>().power < 1)
                                    {
                                        outputObject.GetComponent<UniversalExtractor>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<UniversalExtractor>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<UniversalExtractor>().power -= powerAmount;
                                if (outputObject.GetComponent<UniversalExtractor>().power < 1)
                                {
                                    outputObject.GetComponent<UniversalExtractor>().powerON = false;
                                }
                            }
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                            }
                            outputObject.GetComponent<UniversalExtractor>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<UniversalExtractor>().power -= powerAmount;
                                if (outputObject.GetComponent<UniversalExtractor>().power < 1)
                                {
                                    outputObject.GetComponent<UniversalExtractor>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<DarkMatterCollector>() != null)
                {
                    outputObject.GetComponent<DarkMatterCollector>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<DarkMatterCollector>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                                }
                                outputObject.GetComponent<DarkMatterCollector>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<DarkMatterCollector>().power -= powerAmount;
                                    if (outputObject.GetComponent<DarkMatterCollector>().power < 1)
                                    {
                                        outputObject.GetComponent<DarkMatterCollector>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<DarkMatterCollector>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<DarkMatterCollector>().power -= powerAmount;
                                if (outputObject.GetComponent<DarkMatterCollector>().power < 1)
                                {
                                    outputObject.GetComponent<DarkMatterCollector>().powerON = false;
                                }
                            }
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                            }
                            outputObject.GetComponent<DarkMatterCollector>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<DarkMatterCollector>().power -= powerAmount;
                                if (outputObject.GetComponent<DarkMatterCollector>().power < 1)
                                {
                                    outputObject.GetComponent<DarkMatterCollector>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<Auger>() != null)
                {
                    outputObject.GetComponent<Auger>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<Auger>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<Auger>().power += powerAmount;
                                }
                                outputObject.GetComponent<Auger>().powerON = true;
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<Auger>().power -= powerAmount;
                                    if (outputObject.GetComponent<Auger>().power < 1)
                                    {
                                        outputObject.GetComponent<Auger>().powerON = false;
                                    }
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<Auger>().power += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outputObject.GetComponent<Auger>().powerON = true;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<Auger>().power -= powerAmount;
                                if (outputObject.GetComponent<Auger>().power < 1)
                                {
                                    outputObject.GetComponent<Auger>().powerON = false;
                                }
                            }
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<Auger>().power += powerAmount;
                            }
                            outputObject.GetComponent<Auger>().powerON = true;
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<Auger>().power -= powerAmount;
                                if (outputObject.GetComponent<Auger>().power < 1)
                                {
                                    outputObject.GetComponent<Auger>().powerON = false;
                                }
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                if (outputObject.GetComponent<ElectricLight>() != null)
                {
                    outputObject.GetComponent<ElectricLight>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<ElectricLight>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                outputObject.GetComponent<ElectricLight>().powerON = true;
                            }
                            else
                            {
                                outputObject.GetComponent<ElectricLight>().powerON = false;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outOfFuel = false;
                            outputObject.GetComponent<ElectricLight>().powerON = true;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            outOfFuel = true;
                            GetComponent<AudioSource>().Stop();
                            outputObject.GetComponent<ElectricLight>().powerON = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            outputObject.GetComponent<ElectricLight>().powerON = true;
                        }
                        else
                        {
                            outputObject.GetComponent<ElectricLight>().powerON = false;
                        }
                    }
                }
                if (outputObject.GetComponent<StorageComputer>() != null)
                {
                    outputObject.GetComponent<StorageComputer>().powerObject = this.gameObject;
                    outputID = outputObject.GetComponent<StorageComputer>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                outputObject.GetComponent<StorageComputer>().powerON = true;
                            }
                            else
                            {
                                outputObject.GetComponent<StorageComputer>().powerON = false;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outOfFuel = false;
                            outputObject.GetComponent<StorageComputer>().powerON = true;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            outOfFuel = true;
                            GetComponent<AudioSource>().Stop();
                            outputObject.GetComponent<StorageComputer>().powerON = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            outputObject.GetComponent<StorageComputer>().powerON = true;
                        }
                        else
                        {
                            outputObject.GetComponent<StorageComputer>().powerON = false;
                        }
                    }
                }
                if (outputObject.GetComponent<PowerConduit>() != null)
                {
                    outputObject.GetComponent<PowerConduit>().inputID = ID;
                    outputObject.GetComponent<PowerConduit>().inputObject = this.gameObject;
                    outputID = outputObject.GetComponent<PowerConduit>().ID;
                    if (type.Equals("Solar Panel"))
                    {
                        Vector3 sunPosition = new Vector3(7000, 15000, -10000);
                        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                if (blocked == true)
                                {
                                    outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                                }
                                blocked = false;
                            }
                            else
                            {
                                if (blocked == false)
                                {
                                    outputObject.GetComponent<PowerConduit>().powerAmount -= powerAmount;
                                }
                                blocked = true;
                            }
                        }
                    }
                    else if (type.Equals("Generator"))
                    {
                        if (fuelType.Equals("Coal") && fuelAmount >= 1)
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                            if (outOfFuel == true)
                            {
                                outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                            }
                            GetComponent<Light>().enabled = true;
                            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                            outOfFuel = false;
                            fuelConsumptionTimer += 1;
                            if (fuelConsumptionTimer > 10 - (address * 0.01f))
                            {
                                fuelAmount -= 1;
                                fuelConsumptionTimer = 0;
                            }
                        }
                        else
                        {
                            GetComponent<AudioSource>().Stop();
                            if (outOfFuel == false)
                            {
                                outputObject.GetComponent<PowerConduit>().powerAmount -= powerAmount;
                            }
                            GetComponent<Light>().enabled = false;
                            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
                            outOfFuel = true;
                        }
                    }
                    else if (type.Equals("Reactor Turbine"))
                    {
                        bool reactorFound = false;
                        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
                        {
                            if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorUpHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
                        {
                            if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorDownHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
                        {
                            if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorRightHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
                        {
                            if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorLeftHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
                        {
                            if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorFrontHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
                        {
                            if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                            {
                                if (reactorBackHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling == true)
                                {
                                    reactorFound = true;
                                }
                            }
                        }
                        if (reactorFound == true)
                        {
                            if (noReactor == true)
                            {
                                outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                            }
                            noReactor = false;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else
                        {
                            if (noReactor == false)
                            {
                                outputObject.GetComponent<PowerConduit>().powerAmount -= powerAmount;
                            }
                            noReactor = true;
                            GetComponent<AudioSource>().Stop();
                        }
                    }
                }
            }
            else
            {
                //Debug.Log(ID + " output object is null.");
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
