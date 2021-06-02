using UnityEngine;

public class UniversalExtractor : Machine
{
    public float amount;
    public int speed = 1;
    public int heat;
    public int cooling;
    public bool hasHeatExchanger;
    private int machineTimer;
    public int power;
    private int warmup;
    public string type;
    public GameObject outputObject;
    private GameObject inputObject;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    public string ID = "unassigned";
    public string creationMethod = "built";
    private LineRenderer connectionLine;
    private LineRenderer inputLine;
    public bool powerON;
    private bool extractingIce;
    private bool hasResource;
    public int connectionAttempts;
    public bool connectionFailed;
    private StateManager stateManager;
    private GameObject builtObjects;
    public PowerReceiver powerReceiver;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        stateManager = FindObjectOfType<StateManager>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        builtObjects = GameObject.Find("BuiltObjects");
    }

    //! Used to remove the connection line renderer when the block is destroyed.
    public void OnDestroy()
    {
        if (inputLine != null)
        {
            Destroy(inputLine);
        }
    }

    //! The object exists, is active and is a resource node.
    private bool IsValidResource(GameObject obj)
    {
        return obj != null && obj.transform.parent != builtObjects.transform && obj.activeInHierarchy && obj.GetComponent<UniversalResource>() != null;
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
        UpdatePowerReceiver();

        if (warmup < 10)
        {
            warmup++;
        }
        else if (speed > power)
        {
            speed = power > 0 ? power : 1;
        }
        if (extractingIce == false)
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
        if (outputObject != null)
        {
            connectionLine.SetPosition(0, transform.position);
            connectionLine.SetPosition(1, outputObject.transform.position);
            connectionLine.enabled = true;
        }
        else
        {
            connectionLine.enabled = false;
        }

        if (hasResource == true)
        {
            connectionAttempts = 0;
            if (powerON == true && connectionFailed == false && speed > 0)
            {
                conduitItem.active = true;
                GetComponent<Light>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
                machineTimer += 1;
                if (machineTimer > 5)
                {
                    amount += speed - heat;
                    machineTimer = 0;
                }
            }
            else
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
            }
            type = inputObject.GetComponent<UniversalResource>().type;
        }
        else
        {
            connectionAttempts += 1;
            if (connectionAttempts >= 128)
            {
                connectionAttempts = 0;
                connectionFailed = true;
            }
            if (connectionFailed == false)
            {
                UniversalResource[] allResources = FindObjectsOfType<UniversalResource>();
                foreach (UniversalResource r in allResources)
                {
                    GameObject obj = r.gameObject;
                    if (IsValidResource(obj))
                    {
                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                        if (distance < 20)
                        {
                            if (obj.GetComponent<UniversalResource>().extractor == null)
                            {
                                obj.GetComponent<UniversalResource>().extractor = gameObject;
                            }
                            if (obj.GetComponent<UniversalResource>().extractor == gameObject)
                            {
                                if (obj.GetComponent<UniversalResource>().type.Equals("Ice"))
                                {
                                    extractingIce = true;
                                }
                                else
                                {
                                    extractingIce = false;
                                }
                                if (inputLine == null && obj.GetComponent<LineRenderer>() == null)
                                {
                                    inputLine = obj.AddComponent<LineRenderer>();
                                    inputLine.startWidth = 0.2f;
                                    inputLine.endWidth = 0.2f;
                                    inputLine.material = lineMat;
                                    inputLine.SetPosition(0, transform.position);
                                    inputLine.SetPosition(1, obj.transform.position);
                                }
                                inputObject = obj;
                                hasResource = true;
                            }
                        }
                    }
                }
            }
        }
    }
}