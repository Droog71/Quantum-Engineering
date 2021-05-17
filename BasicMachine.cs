using UnityEngine;

public class BasicMachine : Machine
{
    public float amount;
    public int address;
    public int speed = 1;
    public int power;
    public int heat;
    public int cooling;
    public int connectionAttempts;
    public string inputType;
    public string outputType;
    public string ID = "unassigned";
    public string creationMethod = "built";
    public string inputID;
    public string outputID;
    public bool connectionFailed;
    public bool hasHeatExchanger;
    public bool powerON;
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    public PowerReceiver powerReceiver;
    public BasicMachineRecipe[] recipes;
    private LineRenderer connectionLine;
    private GameObject builtObjects;
    public StateManager stateManager;
    public bool hasCustomSound;
    private int machineTimer;
    private int warmup;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.Busy())
            return;

        UpdatePowerReceiver();
        GetComponent<PhysicsHandler>().UpdatePhysics();

        if (warmup < 10)
        {
            warmup++;
        }
        else if (speed > power)
        {
            speed = power > 0 ? power : 1;
        }
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
            if (connectionFailed == false && outputObject == null)
            {
                FindOutputObject();
            }
        }
        if (inputObject != null)
        {
            if (inputObject.GetComponent<UniversalConduit>() != null)
            {
                if (amount < 1 || outputType == "nothing")
                {
                    outputType = GetOutputType();
                }

                if (inputObject.GetComponent<UniversalConduit>().conduitItem.active == false)
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                }
            }
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }

        HandleOutput();

        if (inputObject != null && outputObject != null)
        {
            connectionAttempts = 0;
        }
    }

    //! The object is a potential output connection.
    private bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj != inputObject && obj != gameObject;
    }

    //! Finds and connects to a universal conduit for output.
    private void FindOutputObject()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
        foreach (GameObject obj in allObjects)
        {
            if (obj != null)
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
                                obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position);
                                connectionLine.enabled = true;
                                creationMethod = "built";
                                break;
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                outputObject = obj;
                                obj.GetComponent<UniversalConduit>().type = outputType;
                                obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position);
                                connectionLine.enabled = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    //! Sets the appropriate output item according to the input.
    private string GetOutputType()
    {
        string incoming = inputObject.GetComponent<UniversalConduit>().type;
        if (incoming != "" && incoming != "nothing")
        {
            inputType = inputObject.GetComponent<UniversalConduit>().type;
        }
        if (recipes != null)
        {
            foreach (BasicMachineRecipe recipe in recipes)
            {
                if (recipe.input == incoming)
                {
                    return recipe.output;
                }
            }
        }
        return "nothing";
    }

    //! If everything is working, items are moved to the output conduit; sounds and other effects are enabled.
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
                            conduitItem.active = true;
                            GetComponent<Light>().enabled = true;
                            machineTimer += 1;
                            if (machineTimer > 5 - (address * 0.01f))
                            {
                                outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                amount -= speed - heat;
                                machineTimer = 0;
                                if (hasCustomSound == true)
                                {
                                    GetComponent<AudioSource>().Play();
                                }
                            }
                        }
                        else
                        {
                            conduitItem.active = false;
                            GetComponent<Light>().enabled = false;
                            machineTimer = 0;
                        }
                    }
                }
            }
        }
        else // Reset the output connection, allow new connections to be made.
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

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }
}