using UnityEngine;

public class Extruder : MonoBehaviour
{
    public float amount;
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public string inputType;
    public string outputType;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public bool powerON;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    public PowerReceiver powerReceiver;
    private LineRenderer connectionLine;
    private float updateTick;
    public int address;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    private GameObject builtObjects;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    // Called once per frame by unity engine
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

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

            if (inputObject == null || outputObject == null)
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
                    ConnectToOutput();
                }
            }
            if (inputObject != null)
            {
                if (inputObject.GetComponent<UniversalConduit>() != null)
                {
                    // Determine the item to be processed before it enters the inventory
                    if (amount < 1)
                    {
                        SetOutputType();
                    }

                    // Disable effects when there is no input to this machine
                    if (inputObject.GetComponent<UniversalConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
                        GetComponent<Light>().enabled = false;
                    }
                }
            }
            else
            {
                // With no input object, this machine is inactive
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
            }

            HandleOutput();

            if (inputObject != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }

    // The object exists, is active and is not a standard building block
    private bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    // The object is a potential output connection
    private bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj != inputObject && obj != gameObject;
    }

    // Finds and connects to a universal conduit for output
    private void ConnectToOutput()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject obj in allObjects)
        {
            if (IsValidObject(obj))
            {
                if (obj.GetComponent<UniversalConduit>() != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (IsValidOutputObject(obj) && distance < 20)
                    {
                        if (obj.GetComponent<UniversalConduit>().inputObject == null)
                        {
                            if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalConduit>().ID.Equals(outputID))
                            {
                                outputObject = obj;
                                obj.GetComponent<UniversalConduit>().type = outputType;
                                obj.GetComponent<UniversalConduit>().inputObject = this.gameObject;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position);
                                connectionLine.enabled = true;
                                creationMethod = "built";
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                outputObject = obj;
                                obj.GetComponent<UniversalConduit>().type = outputType;
                                obj.GetComponent<UniversalConduit>().inputObject = this.gameObject;
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

    // Sets the appropriate output item according to the input
    private void SetOutputType()
    {
        inputType = inputObject.GetComponent<UniversalConduit>().type;
        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Copper Ingot"))
        {
            outputType = "Copper Wire";
        }
        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Aluminum Ingot"))
        {
            outputType = "Aluminum Wire";
        }
        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Steel Ingot"))
        {
            outputType = "Steel Pipe";
        }
        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Iron Ingot"))
        {
            outputType = "Iron Pipe";
        }
    }

    // If everything is working, items are moved to the output conduit; sounds and other effects are enabled.
    private void HandleOutput()
    {
        if (outputObject != null)
        {
            if (outputObject.GetComponent<UniversalConduit>() != null)
            {
                outputObject.GetComponent<UniversalConduit>().inputID = ID;
                outputObject.GetComponent<UniversalConduit>().type = outputType;
                outputObject.GetComponent<UniversalConduit>().speed = speed;
                outputID = outputObject.GetComponent<UniversalConduit>().ID;
                if (amount >= speed)
                {
                    if (outputType.Equals(outputObject.GetComponent<UniversalConduit>().type))
                    {
                        if (powerON == true && connectionFailed == false && inputObject != null && speed > 0)
                        {
                            conduitItem.GetComponent<ConduitItem>().active = true;
                            GetComponent<Light>().enabled = true;
                            machineTimer += 1;
                            if (machineTimer > 5 - (address * 0.01f))
                            {
                                outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                amount -= speed - heat;
                                machineTimer = 0;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            // Reset the output connection, allow new connections to be made
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

    // Gets power values from power receiver
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
        if (powerReceiver.overClocked == true)
        {
            speed = powerReceiver.speed;
        }
        else
        {
            powerReceiver.speed = speed;
        }
    }
}