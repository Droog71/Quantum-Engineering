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

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;

            if (outputObject == null)
            {
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
                        if (IsValidObject(obj))
                        {
                            ConnectToOutput(obj);
                        }
                    }
                }
            }

            if (outputObject != null && connectionFailed == false)
            {
                connectionAttempts = 0;
                DistributePower();
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

    bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    private void ConnectToOutput(GameObject obj)
    {
        float distance = Vector3.Distance(transform.position, obj.transform.position);
        if (distance < 40)
        {
            if (obj.GetComponent<ElectricLight>() != null && outputObject == null)
            {
                if (obj.GetComponent<ElectricLight>().powerObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<ElectricLight>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<ElectricLight>().powerObject = gameObject;
                        outputObject.GetComponent<ElectricLight>().powerON = true;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<ElectricLight>().powerObject = gameObject;
                        outputObject.GetComponent<ElectricLight>().powerON = true;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<StorageComputer>() != null && outputObject == null)
            {
                if (obj.GetComponent<StorageComputer>().powerObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<StorageComputer>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<StorageComputer>().powerObject = gameObject;
                        outputObject.GetComponent<StorageComputer>().powerON = true;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<StorageComputer>().powerObject = gameObject;
                        outputObject.GetComponent<StorageComputer>().powerON = true;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Retriever>() != null && outputObject == null)
            {
                if (obj.GetComponent<Retriever>().powerObject != null)
                {
                    if (obj.GetComponent<Retriever>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Retriever>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Retriever>().powerObject = gameObject;
                            outputObject.GetComponent<Retriever>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Retriever>().powerObject = gameObject;
                            outputObject.GetComponent<Retriever>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Retriever>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Retriever>().powerObject = gameObject;
                        outputObject.GetComponent<Retriever>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Retriever>().powerObject = gameObject;
                        outputObject.GetComponent<Retriever>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Turret>() != null && outputObject == null)
            {
                if (obj.GetComponent<Turret>().powerObject != null)
                {
                    if (obj.GetComponent<Turret>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Turret>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Turret>().powerObject = gameObject;
                            outputObject.GetComponent<Turret>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Turret>().powerObject = gameObject;
                            outputObject.GetComponent<Turret>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Turret>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Turret>().powerObject = gameObject;
                        outputObject.GetComponent<Turret>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Turret>().powerObject = gameObject;
                        outputObject.GetComponent<Turret>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Smelter>() != null && outputObject == null)
            {
                if (obj.GetComponent<Smelter>().powerObject != null)
                {
                    if (obj.GetComponent<Smelter>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Smelter>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Smelter>().powerObject = gameObject;
                            outputObject.GetComponent<Smelter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Smelter>().powerObject = gameObject;
                            outputObject.GetComponent<Smelter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Smelter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Smelter>().powerObject = gameObject;
                        outputObject.GetComponent<Smelter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Smelter>().powerObject = gameObject;
                        outputObject.GetComponent<Smelter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Extruder>() != null && outputObject == null)
            {
                if (obj.GetComponent<Extruder>().powerObject != null)
                {
                    if (obj.GetComponent<Extruder>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Extruder>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Extruder>().powerObject = gameObject;
                            outputObject.GetComponent<Extruder>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Extruder>().powerObject = gameObject;
                            outputObject.GetComponent<Extruder>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Extruder>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Extruder>().powerObject = gameObject;
                        outputObject.GetComponent<Extruder>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Extruder>().powerObject = gameObject;
                        outputObject.GetComponent<Extruder>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<GearCutter>() != null && outputObject == null)
            {
                if (obj.GetComponent<GearCutter>().powerObject != null)
                {
                    if (obj.GetComponent<GearCutter>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<GearCutter>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<GearCutter>().powerObject = gameObject;
                            outputObject.GetComponent<GearCutter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<GearCutter>().powerObject = gameObject;
                            outputObject.GetComponent<GearCutter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<GearCutter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<GearCutter>().powerObject = gameObject;
                        outputObject.GetComponent<GearCutter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<GearCutter>().powerObject = gameObject;
                        outputObject.GetComponent<GearCutter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<AlloySmelter>() != null && outputObject == null)
            {
                if (obj.GetComponent<AlloySmelter>().powerObject != null)
                {
                    if (obj.GetComponent<AlloySmelter>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<AlloySmelter>().powerObject = gameObject;
                            outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<AlloySmelter>().powerObject = gameObject;
                            outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<AlloySmelter>().powerObject = gameObject;
                        outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<AlloySmelter>().powerObject = gameObject;
                        outputObject.GetComponent<AlloySmelter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<UniversalExtractor>() != null && outputObject == null)
            {
                if (obj.GetComponent<UniversalExtractor>().powerObject != null)
                {
                    if (obj.GetComponent<UniversalExtractor>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalExtractor>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<UniversalExtractor>().powerObject = gameObject;
                            outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<UniversalExtractor>().powerObject = gameObject;
                            outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalExtractor>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<UniversalExtractor>().powerObject = gameObject;
                        outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<UniversalExtractor>().powerObject = gameObject;
                        outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<DarkMatterCollector>() != null && outputObject == null)
            {
                if (obj.GetComponent<DarkMatterCollector>().powerObject != null)
                {
                    if (obj.GetComponent<DarkMatterCollector>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<DarkMatterCollector>().powerObject = gameObject;
                            outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<DarkMatterCollector>().powerObject = gameObject;
                            outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<DarkMatterCollector>().powerObject = gameObject;
                        outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<DarkMatterCollector>().powerObject = gameObject;
                        outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Auger>() != null && outputObject == null)
            {
                if (obj.GetComponent<Auger>().powerObject != null)
                {
                    if (obj.GetComponent<Auger>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Auger>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Auger>().powerObject = gameObject;
                            outputObject.GetComponent<Auger>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Auger>().powerObject = gameObject;
                            outputObject.GetComponent<Auger>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Auger>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Auger>().powerObject = gameObject;
                        outputObject.GetComponent<Auger>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Auger>().powerObject = gameObject;
                        outputObject.GetComponent<Auger>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<Press>() != null && outputObject == null)
            {
                if (obj.GetComponent<Press>().powerObject != null)
                {
                    if (obj.GetComponent<Press>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<Press>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Press>().powerObject = gameObject;
                            outputObject.GetComponent<Press>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<Press>().powerObject = gameObject;
                            outputObject.GetComponent<Press>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Press>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Press>().powerObject = gameObject;
                        outputObject.GetComponent<Press>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<Press>().powerObject = gameObject;
                        outputObject.GetComponent<Press>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<AutoCrafter>() != null && outputObject == null)
            {
                if (obj.GetComponent<AutoCrafter>().powerObject != null)
                {
                    if (obj.GetComponent<AutoCrafter>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<AutoCrafter>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<AutoCrafter>().powerObject = gameObject;
                            outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<AutoCrafter>().powerObject = gameObject;
                            outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<AutoCrafter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<AutoCrafter>().powerObject = gameObject;
                        outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<AutoCrafter>().powerObject = gameObject;
                        outputObject.GetComponent<AutoCrafter>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
            if (obj.GetComponent<PowerConduit>() != null && outputObject == null)
            {
                if (obj.GetComponent<PowerConduit>().inputObject != null)
                {
                    if (obj.GetComponent<PowerConduit>().inputObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerConduit>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<PowerConduit>().inputID = ID;
                            outputObject.GetComponent<PowerConduit>().inputObject = gameObject;
                            outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<PowerConduit>().inputID = ID;
                            outputObject.GetComponent<PowerConduit>().inputObject = gameObject;
                            outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<PowerConduit>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<PowerConduit>().inputID = ID;
                        outputObject.GetComponent<PowerConduit>().inputObject = gameObject;
                        outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<PowerConduit>().inputID = ID;
                        outputObject.GetComponent<PowerConduit>().inputObject = gameObject;
                        outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
    }

    private void DistributePower()
    {
        bool addPowerFlag = false;
        bool removePowerFlag = false;
        bool powerOnFlag = false;
        bool powerOffFlag = false;

        if (type.Equals("Solar Panel"))
        {
            Vector3 sunPosition = new Vector3(7000, 15000, -10000);
            if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (blocked == true)
                    {
                        addPowerFlag = true;
                    }
                    powerOnFlag = true;
                    blocked = false;
                }
                else
                {
                    if (blocked == false)
                    {
                        removePowerFlag = true;
                        if (outputObject.GetComponent<Retriever>().power < 1)
                        {
                            powerOffFlag = true;
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
                    addPowerFlag = true;
                }
                GetComponent<Light>().enabled = true;
                GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
                powerOnFlag = true;
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
                    removePowerFlag = true;
                    if (outputObject.GetComponent<Retriever>().power < 1)
                    {
                        powerOffFlag = true;
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
            Vector3[] allDir = { transform.up, -transform.up, transform.right, -transform.right, transform.forward, -transform.forward };
            foreach (Vector3 dir in allDir)
            {
                if (Physics.Raycast(transform.position, dir, out RaycastHit dirHit, 3))
                {
                    if (dirHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                    {
                        reactorFound = dirHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling;
                    }
                }
            }
            if (reactorFound == true)
            {
                if (noReactor == true)
                {
                    addPowerFlag = true;
                }
                powerOnFlag = true;
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
                    removePowerFlag = true;
                    if (outputObject.GetComponent<Retriever>().power < 1)
                    {
                        powerOffFlag = true;
                    }
                }
                noReactor = true;
                GetComponent<AudioSource>().Stop();
            }
        }

        if (outputObject.GetComponent<Retriever>() != null)
        {
            outputObject.GetComponent<Retriever>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Retriever>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Retriever>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Retriever>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Retriever>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Retriever>().powerON = false;
            }
        }
        if (outputObject.GetComponent<Smelter>() != null)
        {
            outputObject.GetComponent<Smelter>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Smelter>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Smelter>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Smelter>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Smelter>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Smelter>().powerON = false;
            }
        }
        if (outputObject.GetComponent<Turret>() != null)
        {
            outputObject.GetComponent<Turret>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Turret>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Turret>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Turret>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Turret>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Turret>().powerON = false;
            }
        }
        if (outputObject.GetComponent<AutoCrafter>() != null)
        {
            outputObject.GetComponent<AutoCrafter>().powerObject = gameObject;
            outputID = outputObject.GetComponent<AutoCrafter>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<AutoCrafter>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<AutoCrafter>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<AutoCrafter>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<AutoCrafter>().powerON = false;
            }
        }
        if (outputObject.GetComponent<Press>() != null)
        {
            outputObject.GetComponent<Press>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Press>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Press>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Press>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Press>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Press>().powerON = false;
            }
        }
        if (outputObject.GetComponent<AlloySmelter>() != null)
        {
            outputObject.GetComponent<AlloySmelter>().powerObject = gameObject;
            outputID = outputObject.GetComponent<AlloySmelter>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<AlloySmelter>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<AlloySmelter>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<AlloySmelter>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<AlloySmelter>().powerON = false;
            }
        }
        if (outputObject.GetComponent<Extruder>() != null)
        {
            outputObject.GetComponent<Extruder>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Extruder>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Extruder>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Extruder>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Extruder>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Extruder>().powerON = false;
            }
        }
        if (outputObject.GetComponent<GearCutter>() != null)
        {
            outputObject.GetComponent<GearCutter>().powerObject = gameObject;
            outputID = outputObject.GetComponent<GearCutter>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<GearCutter>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<GearCutter>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<GearCutter>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<GearCutter>().powerON = false;
            }
        }
        if (outputObject.GetComponent<UniversalExtractor>() != null)
        {
            outputObject.GetComponent<UniversalExtractor>().powerObject = gameObject;
            outputID = outputObject.GetComponent<UniversalExtractor>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<UniversalExtractor>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<UniversalExtractor>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<UniversalExtractor>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<UniversalExtractor>().powerON = false;
            }
        }
        if (outputObject.GetComponent<DarkMatterCollector>() != null)
        {
            outputObject.GetComponent<DarkMatterCollector>().powerObject = gameObject;
            outputID = outputObject.GetComponent<DarkMatterCollector>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<DarkMatterCollector>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<DarkMatterCollector>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<DarkMatterCollector>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<DarkMatterCollector>().powerON = false;
            }
        }
        if (outputObject.GetComponent<Auger>() != null)
        {
            outputObject.GetComponent<Auger>().powerObject = gameObject;
            outputID = outputObject.GetComponent<Auger>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<Auger>().power += powerAmount;
            }
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<Auger>().powerON = true;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<Auger>().power -= powerAmount;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<Auger>().powerON = false;
            }
        }
        if (outputObject.GetComponent<ElectricLight>() != null)
        {
            outputObject.GetComponent<ElectricLight>().powerObject = gameObject;
            outputID = outputObject.GetComponent<ElectricLight>().ID;
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<ElectricLight>().powerON = true;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<ElectricLight>().powerON = false;
            }
        }
        if (outputObject.GetComponent<StorageComputer>() != null)
        {
            outputObject.GetComponent<StorageComputer>().powerObject = gameObject;
            outputID = outputObject.GetComponent<StorageComputer>().ID;
            if (powerOnFlag == true)
            {
                outputObject.GetComponent<StorageComputer>().powerON = true;
            }
            if (powerOffFlag == true)
            {
                outputObject.GetComponent<StorageComputer>().powerON = false;
            }
        }
        if (outputObject.GetComponent<PowerConduit>() != null)
        {
            outputObject.GetComponent<PowerConduit>().inputID = ID;
            outputObject.GetComponent<PowerConduit>().inputObject = gameObject;
            outputID = outputObject.GetComponent<PowerConduit>().ID;
            if (addPowerFlag == true)
            {
                outputObject.GetComponent<PowerConduit>().powerAmount += powerAmount;
            }
            if (removePowerFlag == true)
            {
                outputObject.GetComponent<PowerConduit>().powerAmount -= powerAmount;
            }
        }
    }
}
