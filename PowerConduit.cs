using UnityEngine;

public class PowerConduit : Machine
{
    public Material lineMat;
    public GameObject outputObject1;
    public GameObject outputObject2;
    private GameObject[] outputObjects;
    public GameObject inputObject;
    public GameObject connectionObject;
    public string outputID1 = "unassigned";
    public string outputID2 = "unassigned";
    public string inputID;
    private LineRenderer connectionLine;
    private GameObject connectionLine2;
    public int powerAmount;
    public bool dualOutput;
    public int range = 6;
    public int connectionAttempts;
    public int dualConnectionAttempts;
    public bool connectionFailed;
    private GameObject builtObjects;
    public PowerReceiver powerReceiver;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
        outputObjects = new GameObject[] { outputObject1, outputObject2 };
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        UpdatePowerReceiver();

        if (outputObject1 == null && inputObject != null)
        {
            UpdateOutputOne();
        }

        if (outputObject1 != null && outputObject2 == null && inputObject != null && dualOutput == true)
        {
            UpdateOutputTwo();
        }

        if (outputObject1 != null && connectionFailed == false)
        {
            DistributePowerToConnectionOne();
        }

        if (outputObject2 != null && connectionFailed == false)
        {
            DistributePowerToConnectionTwo();
        }

        if (inputObject != null)
        {
            if (inputObject.GetComponent<PowerSource>() != null)
            {
                if (inputObject.GetComponent<PowerSource>().outputObject != gameObject)
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
                if (inputObject.GetComponent<PowerConduit>().outputObject1 != gameObject && inputObject.GetComponent<PowerConduit>().outputObject2 != gameObject)
                {
                    inputObject = null;
                    powerAmount = 0;
                }
            }
        }

        connectionLine.enabled &= outputObject1 != null;

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

    //! Used to de-energize output objects when the power conduit is destroyed.
    public void OnDestroy()
    {
        if (connectionLine2 != null)
        {
            Destroy(connectionLine2);
        }
        if (outputObject1 != null)
        {
            if (outputObject1.GetComponent<PowerReceiver>() != null)
            {
                outputObject1.GetComponent<PowerReceiver>().power = 0;
                outputObject1.GetComponent<PowerReceiver>().powerON = false;
            }
            if (outputObject1.GetComponent<PowerConduit>() != null)
            {
                outputObject1.GetComponent<PowerConduit>().powerAmount = 0;
            }
        }
        if (outputObject2 != null)
        {
            if (outputObject2.GetComponent<PowerReceiver>() != null)
            {
                outputObject2.GetComponent<PowerReceiver>().power = 0;
                outputObject2.GetComponent<PowerReceiver>().powerON = false;
            }
            if (outputObject2.GetComponent<PowerConduit>() != null)
            {
                outputObject2.GetComponent<PowerConduit>().powerAmount = 0;
            }
        }
    }

    //! Gets power values from power receiver.
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

    //! Creates a line renderer to the second output object
    private void CreateOutput2ConnectionLine()
    {
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

    //! Connects to a another power conduit.
    private void ConnectOutputOneToConduit(GameObject obj)
    {
        if (obj != gameObject)
        {
            if (obj.GetComponent<PowerConduit>().outputObject1 != null)
            {
                if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject && obj.GetComponent<PowerConduit>().outputObject2 != null)
                {
                    if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
                    {
                        if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                        {
                            AttemptConduitOneConnection(obj);
                        }
                    }
                }
                else if (obj.GetComponent<PowerConduit>().outputObject2 == null)
                {
                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                    {
                        AttemptConduitOneConnection(obj);
                    }
                }
            }

            if (outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 != null)
            {
                if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject && obj.GetComponent<PowerConduit>().outputObject1 != null)
                {
                    if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
                    {
                        if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                        {
                            AttemptConduitOneConnection(obj);
                        }
                    }
                }
                else if (obj.GetComponent<PowerConduit>().outputObject1 == null)
                {
                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                    {
                        AttemptConduitOneConnection(obj);
                    }
                }
            }

            if (outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 == null)
            {
                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                {
                    AttemptConduitOneConnection(obj);
                }
            }
        }
    }

    //! Connects to a another power conduit.
    private void ConnectOutputTwoToConduit(GameObject obj)
    {
        if (obj != gameObject)
        {
            if (obj.GetComponent<PowerConduit>().outputObject1 != null)
            {
                if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject && obj.GetComponent<PowerConduit>().outputObject2 != null)
                {
                    if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
                    {
                        if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                        {
                            AttemptConduitTwoConnection(obj);
                        }
                    }
                }
                else if (obj.GetComponent<PowerConduit>().outputObject2 == null)
                {
                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                    {
                        AttemptConduitTwoConnection(obj);
                    }
                }
            }

            if (outputObject2 == null && obj.GetComponent<PowerConduit>().outputObject2 != null)
            {
                if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject && obj.GetComponent<PowerConduit>().outputObject1 != null)
                {
                    if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
                    {
                        if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                        {
                            AttemptConduitTwoConnection(obj);
                        }
                    }
                }
                else if (obj.GetComponent<PowerConduit>().outputObject1 == null)
                {
                    if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                    {
                        AttemptConduitTwoConnection(obj);
                    }
                }
            }

            if (outputObject2 == null && obj.GetComponent<PowerConduit>().outputObject1 == null && obj.GetComponent<PowerConduit>().outputObject2 == null)
            {
                if (obj.GetComponent<PowerConduit>().inputObject == null && inputObject != null)
                {
                    AttemptConduitTwoConnection(obj);
                }
            }
        }
    }

    //! Designates conduit as an output with assigned ID.
    private void InitializeOutputConduitOne(GameObject obj)
    {
        outputObject1 = obj;
        outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
        outputObject1.GetComponent<PowerConduit>().inputID = ID;
        outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
        outputObject1.GetComponent<PowerConduit>().powerAmount = outputObject2 != null ? powerAmount / 2 : powerAmount;
        connectionLine.SetPosition(0, transform.position);
        connectionLine.SetPosition(1, obj.transform.position);
        connectionLine.enabled = true;
    }

    //! Designates conduit as an output with assigned ID.
    private void InitializeOutputConduitTwo(GameObject obj)
    {
        outputObject2 = obj;
        outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
        outputObject2.GetComponent<PowerConduit>().inputID = ID;
        outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
        outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
        CreateOutput2ConnectionLine();
    }

    //! Attempts to connect to a another power conduit.
    private void AttemptConduitOneConnection(GameObject obj)
    {
        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerConduit>().ID.Equals(outputID1))
        {
            InitializeOutputConduitOne(obj);
            creationMethod = "built";
        }
        else if (creationMethod.Equals("built"))
        {
            InitializeOutputConduitOne(obj);
        }
    }

    //! Attempts to connect to a another power conduit.
    private void AttemptConduitTwoConnection(GameObject obj)
    {
        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerConduit>().ID.Equals(outputID2))
        {
            InitializeOutputConduitTwo(obj);
            creationMethod = "built";
        }
        else if (creationMethod.Equals("built"))
        {
            InitializeOutputConduitTwo(obj);
        }
    }

    //! Designates machine as an output with assigned ID.
    private void InitializeMachineOneConnection(GameObject obj)
    {
        outputObject1 = obj;
        outputObject1.GetComponent<PowerReceiver>().powerObject = gameObject;
        outputObject1.GetComponent<PowerReceiver>().power = outputObject2 != null ? powerAmount / 2 : powerAmount;
        connectionLine.SetPosition(0, transform.position);
        connectionLine.SetPosition(1, obj.transform.position);
        connectionLine.enabled = true;
    }

    //! Attempts to connect to a machine.
    private void AttemptMachineOneConnection(GameObject obj)
    {
        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerReceiver>().ID.Equals(outputID1))
        {
            InitializeMachineOneConnection(obj);
            creationMethod = "built";
        }
        else if (creationMethod.Equals("built"))
        {
            InitializeMachineOneConnection(obj);
        }
    }

    //! Designates machine as an output with assigned ID.
    private void InitializeMachineTwoConnection(GameObject obj)
    {
        outputObject2 = obj;
        outputObject2.GetComponent<PowerReceiver>().powerObject = gameObject;
        outputObject2.GetComponent<PowerReceiver>().power = powerAmount / 2;
        CreateOutput2ConnectionLine();
    }

    //! Attempts to connect to a machine.
    private void AttemptMachineTwoConnection(GameObject obj)
    {
        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerReceiver>().ID.Equals(outputID2))
        {
            InitializeMachineTwoConnection(obj);
            creationMethod = "built";
        }
        else if (creationMethod.Equals("built"))
        {
            InitializeMachineTwoConnection(obj);
        }
    }

    //! Makes connections from this conduit to another conduti or a machine.
    private void UpdateOutputOne()
    {
        connectionAttempts += 1;
        if (creationMethod.Equals("spawned"))
        {
            if (connectionAttempts >= 128)
            {
                connectionAttempts = 0;
                connectionFailed = true;
            }
        }
        else
        {
            outputID1 = "unassigned";
            if (connectionAttempts >= 512)
            {
                connectionAttempts = 0;
                connectionFailed = true;
            }
        }
        if (connectionFailed == false)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
            foreach (GameObject obj in allObjects)
            {
                if (obj != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance < range)
                    {
                        if (obj.GetComponent<PowerReceiver>() != null && obj.GetComponent<PowerConduit>() == null && outputObject1 == null)
                        {
                            if (obj.GetComponent<PowerReceiver>().powerON == false)
                            {
                                AttemptMachineOneConnection(obj);
                            }
                        }
                        if (obj.GetComponent<PowerConduit>() != null && outputObject1 == null)
                        {
                            ConnectOutputOneToConduit(obj);
                        }
                    }
                }
                if (outputObject1 != null)
                {
                    break;
                }
            }
        }
    }

    //! Makes connections from this conduit to another conduit or a machine.
    private void UpdateOutputTwo()
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
            outputID2 = "unassigned";
            if (dualConnectionAttempts >= 60)
            {
                dualConnectionAttempts = 0;
                dualOutput = false;
            }
        }
        if (dualOutput == true)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
            foreach (GameObject obj in allObjects)
            {
                if (obj != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance < range)
                    {
                        if (obj.GetComponent<PowerReceiver>() != null && obj.GetComponent<PowerConduit>() == null && obj != outputObject1 && outputObject2 == null)
                        {
                            if (obj.GetComponent<PowerReceiver>().powerON == false)
                            {
                                AttemptMachineTwoConnection(obj);
                            }
                        }
                        if (obj.GetComponent<PowerConduit>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            ConnectOutputTwoToConduit(obj);
                        }
                    }
                }
                if (outputObject2 != null)
                {
                    break;
                }
            }
        }
    }

    //! Distributes power to output 1.
    private void DistributePowerToConnectionOne()
    {
        if (outputObject1.GetComponent<PowerReceiver>() != null)
        {
            if (powerAmount > 0)
            {
                outputObject1.GetComponent<PowerReceiver>().powerON = true;
            }
            else
            {
                outputObject1.GetComponent<PowerReceiver>().powerON = false;
            }

            if (outputObject2 != null)
            {
                outputObject1.GetComponent<PowerReceiver>().power = powerAmount / 2;
            }
            else
            {
                outputObject1.GetComponent<PowerReceiver>().power = powerAmount;
            }

            outputObject1.GetComponent<PowerReceiver>().powerObject = gameObject;
            outputID1 = outputObject1.GetComponent<PowerReceiver>().ID;
        }
        if (outputObject1.GetComponent<PowerConduit>() != null)
        {
            outputID1 = outputObject1.GetComponent<PowerConduit>().ID;
            outputObject1.GetComponent<PowerConduit>().inputID = ID;
            outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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

    //! Distributes power to output 2.
    private void DistributePowerToConnectionTwo()
    {
        if (outputObject2.GetComponent<PowerReceiver>() != null)
        {
            if (powerAmount > 0)
            {
                outputObject2.GetComponent<PowerReceiver>().powerON = true;
            }
            else
            {
                outputObject2.GetComponent<PowerReceiver>().powerON = false;
            }
            outputObject2.GetComponent<PowerReceiver>().powerObject = gameObject;
            outputObject2.GetComponent<PowerReceiver>().power = powerAmount / 2;
            outputID2 = outputObject2.GetComponent<PowerReceiver>().ID;
        }
        if (outputObject2.GetComponent<PowerConduit>() != null)
        {
            outputID2 = outputObject2.GetComponent<PowerConduit>().ID;
            outputObject2.GetComponent<PowerConduit>().inputID = ID;
            outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
            outputObject2.GetComponent<PowerConduit>().powerAmount = powerAmount / 2;
        }
    }
}

