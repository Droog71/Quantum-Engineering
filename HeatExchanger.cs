using UnityEngine;
using System.Collections;

public class HeatExchanger : MonoBehaviour
{
    public string ID = "unassigned";
    public string inputID;
    public string inputType;
    public int speed = 1;
    public float amount;
    public bool providingCooling;
    public GameObject inputObject;
    public Material lineMat;
    public string creationMethod;
    public GameObject outputObject;
    public string outputID;
    LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public string type;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
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
            if (outputObject.GetComponent<UniversalExtractor>() != null)
            {
                outputObject.GetComponent<UniversalExtractor>().cooling = 0;
                outputObject.GetComponent<UniversalExtractor>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Retriever>() != null)
            {
                outputObject.GetComponent<Retriever>().cooling = 0;
                outputObject.GetComponent<Retriever>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<AutoCrafter>() != null)
            {
                outputObject.GetComponent<AutoCrafter>().cooling = 0;
                outputObject.GetComponent<AutoCrafter>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<DarkMatterCollector>() != null)
            {
                outputObject.GetComponent<DarkMatterCollector>().cooling = 0;
                outputObject.GetComponent<DarkMatterCollector>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Auger>() != null)
            {
                outputObject.GetComponent<Auger>().cooling = 0;
                outputObject.GetComponent<Auger>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<NuclearReactor>() != null)
            {
                outputObject.GetComponent<NuclearReactor>().cooling = 0;
                outputObject.GetComponent<NuclearReactor>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Smelter>() != null)
            {
                outputObject.GetComponent<Smelter>().cooling = 0;
                outputObject.GetComponent<Smelter>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Extruder>() != null)
            {
                outputObject.GetComponent<Extruder>().cooling = 0;
                outputObject.GetComponent<Extruder>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Press>() != null)
            {
                outputObject.GetComponent<Press>().cooling = 0;
                outputObject.GetComponent<Press>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<AlloySmelter>() != null)
            {
                outputObject.GetComponent<AlloySmelter>().cooling = 0;
                outputObject.GetComponent<AlloySmelter>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<GearCutter>() != null)
            {
                outputObject.GetComponent<GearCutter>().cooling = 0;
                outputObject.GetComponent<GearCutter>().hasHeatExchanger = false;
            }
            if (outputObject.GetComponent<Turret>() != null)
            {
                outputObject.GetComponent<Turret>().cooling = 0;
                outputObject.GetComponent<Turret>().hasHeatExchanger = false;
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
                                    if (obj.GetComponent<UniversalExtractor>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<UniversalExtractor>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<UniversalExtractor>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<UniversalExtractor>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<UniversalExtractor>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<UniversalExtractor>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<DarkMatterCollector>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<DarkMatterCollector>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<DarkMatterCollector>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<DarkMatterCollector>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<DarkMatterCollector>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<DarkMatterCollector>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Retriever>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Retriever>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Retriever>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Retriever>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Retriever>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Retriever>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AutoCrafter>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AutoCrafter>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<AutoCrafter>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<AutoCrafter>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<AutoCrafter>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AutoCrafter>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Auger>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Auger>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Auger>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Auger>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Auger>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Auger>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<NuclearReactor>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<NuclearReactor>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<NuclearReactor>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<NuclearReactor>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<NuclearReactor>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<NuclearReactor>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Smelter>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Smelter>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Smelter>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Smelter>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Smelter>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Smelter>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Extruder>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Extruder>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Extruder>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Extruder>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Extruder>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Extruder>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Press>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Press>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Press>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Press>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Press>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Press>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<AlloySmelter>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<AlloySmelter>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<AlloySmelter>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<AlloySmelter>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<AlloySmelter>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<GearCutter>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<GearCutter>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<GearCutter>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<GearCutter>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<GearCutter>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<GearCutter>().hasHeatExchanger = true;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                        connectionLine.enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<Turret>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj != this.gameObject)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Turret>().ID.Equals(outputID))
                                                    {
                                                        if (obj.GetComponent<Turret>().hasHeatExchanger == false)
                                                        {
                                                            outputObject = obj;
                                                            outputObject.GetComponent<Turret>().hasHeatExchanger = true;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                            creationMethod = "built";
                                                        }
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    if (obj.GetComponent<Turret>().hasHeatExchanger == false)
                                                    {
                                                        outputObject = obj;
                                                        outputObject.GetComponent<Turret>().hasHeatExchanger = true;
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
            if (inputObject != null)
            {
                if (inputObject.GetComponent<UniversalConduit>() != null)
                {
                    inputType = inputObject.GetComponent<UniversalConduit>().type;
                    if (inputObject.GetComponent<UniversalConduit>().type.Equals("Ice"))
                    {
                        if (speed > inputObject.GetComponent<UniversalConduit>().speed)
                        {
                            speed = inputObject.GetComponent<UniversalConduit>().speed;
                        }
                        if (amount >= speed && speed > 0)
                        {
                            providingCooling = true;
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger does not have enough ice.");
                            providingCooling = false;
                        }
                    }
                }
            }
            if (outputObject != null)
            {
                if (outputObject.GetComponent<NuclearReactor>() != null)
                {
                    outputID = outputObject.GetComponent<NuclearReactor>().ID;
                }
                if (outputObject.GetComponent<UniversalExtractor>() != null)
                {
                    outputID = outputObject.GetComponent<UniversalExtractor>().ID;
                }
                if (outputObject.GetComponent<Retriever>() != null)
                {
                    outputID = outputObject.GetComponent<Retriever>().ID;
                }
                if (outputObject.GetComponent<AutoCrafter>() != null)
                {
                    outputID = outputObject.GetComponent<AutoCrafter>().ID;
                }
                if (outputObject.GetComponent<DarkMatterCollector>() != null)
                {
                    outputID = outputObject.GetComponent<DarkMatterCollector>().ID;
                }
                if (outputObject.GetComponent<Auger>() != null)
                {
                    outputID = outputObject.GetComponent<Auger>().ID;
                }
                if (outputObject.GetComponent<NuclearReactor>() != null)
                {
                    outputID = outputObject.GetComponent<NuclearReactor>().ID;
                }
                if (outputObject.GetComponent<Smelter>() != null)
                {
                    outputID = outputObject.GetComponent<Smelter>().ID;
                }
                if (outputObject.GetComponent<Extruder>() != null)
                {
                    outputID = outputObject.GetComponent<Extruder>().ID;
                }
                if (outputObject.GetComponent<Press>() != null)
                {
                    outputID = outputObject.GetComponent<Press>().ID;
                }
                if (outputObject.GetComponent<AlloySmelter>() != null)
                {
                    outputID = outputObject.GetComponent<AlloySmelter>().ID;
                }
                if (outputObject.GetComponent<GearCutter>() != null)
                {
                    outputID = outputObject.GetComponent<GearCutter>().ID;
                }
                if (outputObject.GetComponent<Turret>() != null)
                {
                    outputID = outputObject.GetComponent<Turret>().ID;
                }
                machineTimer += 1;
                if (machineTimer > 5 - (address * 0.01f))
                {
                    if (outputObject.GetComponent<UniversalExtractor>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<UniversalExtractor>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<UniversalExtractor>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Retriever>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Retriever>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Retriever>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<AutoCrafter>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<AutoCrafter>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<AutoCrafter>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<DarkMatterCollector>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<DarkMatterCollector>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<DarkMatterCollector>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Auger>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Auger>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Auger>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<NuclearReactor>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<NuclearReactor>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<NuclearReactor>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Smelter>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Smelter>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Smelter>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Extruder>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Extruder>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Extruder>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Press>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Press>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Press>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<AlloySmelter>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<AlloySmelter>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<AlloySmelter>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<GearCutter>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<GearCutter>().cooling = speed;
                                amount -= speed;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<GearCutter>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<Turret>() != null)
                    {
                        if (providingCooling == true && connectionFailed == false && inputObject != null)
                        {
                            outputObject.GetComponent<Turret>().cooling = speed;
                            amount -= speed;
                            GetComponent<Light>().enabled = true;
                            GetComponent<AudioSource>().enabled = true;
                        }
                        else
                        {
                            //Debug.Log("Heat exchanger ran out of ice.");
                            outputObject.GetComponent<Turret>().cooling = 0;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    machineTimer = 0;
                }
            }
            else
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
