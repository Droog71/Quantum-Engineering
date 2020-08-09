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
            if (outputObject1.GetComponent<PowerReceiver>() != null)
            {
                outputObject1.GetComponent<PowerReceiver>().power = 0;
                outputObject1.GetComponent<PowerReceiver>().speed = 1;
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
                outputObject2.GetComponent<PowerReceiver>().speed = 1;
                outputObject2.GetComponent<PowerReceiver>().powerON = false;
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

    bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
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
                        if (IsValidObject(obj))
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<PowerReceiver>() != null && obj.GetComponent<PowerConduit>() == null && outputObject1 == null)
                                {
                                    if (obj.GetComponent<PowerReceiver>().powerON == false)
                                    {
                                        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerReceiver>().ID.Equals(outputID1))
                                        {
                                            outputObject1 = obj;
                                            outputObject1.GetComponent<PowerReceiver>().powerObject = gameObject;
                                            if (outputObject2 != null)
                                            {
                                                outputObject1.GetComponent<PowerReceiver>().power = powerAmount / 2;
                                            }
                                            else
                                            {
                                                outputObject1.GetComponent<PowerReceiver>().power = powerAmount;
                                            }
                                            connectionLine.SetPosition(0, transform.position);
                                            connectionLine.SetPosition(1, obj.transform.position);
                                            connectionLine.enabled = true;
                                            creationMethod = "built";
                                        }
                                        else if (creationMethod.Equals("built"))
                                        {
                                            outputObject1 = obj;
                                            outputObject1.GetComponent<PowerReceiver>().powerObject = gameObject;
                                            if (outputObject2 != null)
                                            {
                                                outputObject1.GetComponent<PowerReceiver>().power = powerAmount / 2;
                                            }
                                            else
                                            {
                                                outputObject1.GetComponent<PowerReceiver>().power = powerAmount;
                                            }
                                            connectionLine.SetPosition(0, transform.position);
                                            connectionLine.SetPosition(1, obj.transform.position);
                                            connectionLine.enabled = true;
                                        }
                                    }
                                }
                                if (obj.GetComponent<PowerConduit>() != null && outputObject1 == null)
                                {
                                    if (obj != gameObject)
                                    {
                                        if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                        {
                                            if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject2 != null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
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
                                                                    outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                                outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                                outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                            if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                                {
                                                    if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
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
                                                                    outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                                outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                                outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                        outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                    outputObject1.GetComponent<PowerConduit>().inputObject = gameObject;
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
                        if (obj.GetComponent<PowerReceiver>() != null && obj != outputObject1 && outputObject2 == null)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < range)
                            {
                                if (obj.GetComponent<PowerReceiver>().powerON == false)
                                {
                                    if (creationMethod.Equals("spawned"))
                                    {
                                        if (obj.GetComponent<PowerReceiver>().ID.Equals(outputID2))
                                        {
                                            outputObject2 = obj;
                                            outputObject2.GetComponent<PowerReceiver>().powerObject = gameObject;
                                            outputObject2.GetComponent<PowerReceiver>().power = powerAmount / 2;
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
                                        outputObject2.GetComponent<PowerReceiver>().powerObject = gameObject;
                                        outputObject2.GetComponent<PowerReceiver>().power = powerAmount / 2;
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
                                if (obj != gameObject)
                                {
                                    if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                    {
                                        if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
                                        {
                                            if (obj.GetComponent<PowerConduit>().outputObject2 != null)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
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
                                                                outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                        outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                        if (obj.GetComponent<PowerConduit>().outputObject2 != gameObject)
                                        {
                                            if (obj.GetComponent<PowerConduit>().outputObject1 != null)
                                            {
                                                if (obj.GetComponent<PowerConduit>().outputObject1 != gameObject)
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
                                                                outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                            outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                        outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                    outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
                                                outputObject2.GetComponent<PowerConduit>().inputObject = gameObject;
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
            if (outputObject2 != null && connectionFailed == false)
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

