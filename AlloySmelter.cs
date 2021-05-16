using UnityEngine;

public class AlloySmelter : MonoBehaviour
{
    public int speed = 1;
    public string ID = "unassigned";
    public string creationMethod = "built";
    public int power;
    public int heat;
    public int cooling;
    public float amount;
    public float amount2;
    public float outputAmount;
    public string inputType1;
    public string inputType2;
    public string outputType;
    public string inputID1;
    public string inputID2;
    public string outputID;
    public PowerReceiver powerReceiver;
    public GameObject fireObject;
    public GameObject inputObject1;
    public GameObject inputObject2;
    public GameObject outputObject;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    public bool hasHeatExchanger;
    public bool powerON;
    public bool connectionFailed;
    public int address;
    public int connectionAttempts;
    private LineRenderer connectionLine;
    private float updateTick;
    private int machineTimer;
    private int warmup;
    private GameObject builtObjects;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        stateManager = FindObjectOfType<StateManager>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
        conduitItem = GetComponentInChildren<ConduitItem>(true);
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (ID == "unassigned")
            return;

        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            if (stateManager.Busy())
            {
                 updateTick = 0;
                return;
            }

            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

            updateTick = 0;

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

            if (GetComponent<AudioSource>().isPlaying == false)
            {
                fireObject.SetActive(false);
            }

            if (inputObject1 == null || inputObject2 == null || outputObject == null)
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
                    GetOutputConduit();
                }
            }

            if (inputObject1 != null && inputObject2 != null)
            {
                SmeltAlloy();
            }
            else
            {
                conduitItem.active = false;
            }

            OutputToConduit();

            if (inputObject1 != null && inputObject2 != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }

    //! Returns true if the object exists, is active and is not a standard building block.
    private bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    //! The object is a potential output connection.
    private bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject1 != null && inputObject2 != null && obj != inputObject1 && obj != inputObject2 && obj != gameObject;
    }

    //! Finds a universal conduit to use as an output.
    private void GetOutputConduit()
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
                                obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position);
                                connectionLine.enabled = true;
                                creationMethod = "built";
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                outputObject = obj;
                                obj.GetComponent<UniversalConduit>().type = outputType;
                                obj.GetComponent<UniversalConduit>().inputObject = gameObject;
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

    //! Consumes input items and produces an item for output.
    private void SmeltAlloy()
    {
        if (inputObject1.GetComponent<UniversalConduit>() != null && inputObject2.GetComponent<UniversalConduit>() != null)
        {
            if (amount < 1)
            {
                if (inputObject1.GetComponent<UniversalConduit>().type != "" && inputObject1.GetComponent<UniversalConduit>().type != "nothing")
                    inputType1 = inputObject1.GetComponent<UniversalConduit>().type;
            }
            if (amount2 < 1)
            {
                if (inputObject2.GetComponent<UniversalConduit>().type != "" && inputObject2.GetComponent<UniversalConduit>().type != "nothing")
                    inputType2 = inputObject2.GetComponent<UniversalConduit>().type;
            }

            if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Copper Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Tin Ingot"))
            {
                outputType = "Bronze Ingot";
                if (amount >= speed && amount2 >= speed)
                {
                    if (powerON == true && connectionFailed == false && speed > 0)
                    {
                        outputAmount += speed - heat;
                        amount -= speed - heat;
                        amount2 -= speed - heat;
                        fireObject.SetActive(true);
                        GetComponent<AudioSource>().enabled = true;
                    }
                    else
                    {
                        fireObject.SetActive(false);
                        GetComponent<AudioSource>().enabled = false;
                    }
                }
            }
            else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Tin Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Copper Ingot"))
            {
                outputType = "Bronze Ingot";
                if (amount >= speed && amount2 >= speed)
                {
                    if (powerON == true && connectionFailed == false && speed > 0)
                    {
                        outputAmount += speed - heat;
                        amount -= speed - heat;
                        amount2 -= speed - heat;
                        fireObject.SetActive(true);
                        GetComponent<AudioSource>().enabled = true;
                    }
                    else
                    {
                        fireObject.SetActive(false);
                        GetComponent<AudioSource>().enabled = false;
                    }
                }
            }
            else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Iron Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Coal"))
            {
                outputType = "Steel Ingot";
                if (amount >= speed && amount2 >= speed)
                {
                    if (powerON == true && connectionFailed == false && speed > 0)
                    {
                        outputAmount += speed - heat;
                        amount -= speed - heat;
                        amount2 -= speed - heat;
                        fireObject.SetActive(true);
                        GetComponent<AudioSource>().enabled = true;
                    }
                    else
                    {
                        fireObject.SetActive(false);
                        GetComponent<AudioSource>().enabled = false;
                    }
                }
            }
            else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Coal") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Iron Ingot"))
            {
                outputType = "Steel Ingot";
                if (amount >= speed && amount2 >= speed)
                {
                    if (powerON == true && connectionFailed == false && speed > 0)
                    {
                        outputAmount += speed - heat;
                        amount -= speed - heat;
                        amount2 -= speed - heat;
                        fireObject.SetActive(true);
                        GetComponent<AudioSource>().enabled = true;
                    }
                    else
                    {
                        fireObject.SetActive(false);
                        GetComponent<AudioSource>().enabled = false;
                    }
                }
            }
            if (inputObject1.GetComponent<UniversalConduit>().conduitItem.active == false)
            {
                conduitItem.active = false;
            }
            if (inputObject2.GetComponent<UniversalConduit>().conduitItem.active == false)
            {
                conduitItem.active = false;
            }
        }
    }

    //! Items are moved to output conduit, sounds and special effects are created.
    private void OutputToConduit()
    {
        if (outputObject != null)
        {
            if (outputObject.GetComponent<UniversalConduit>() != null)
            {
                outputObject.GetComponent<UniversalConduit>().inputID = ID;
                outputObject.GetComponent<UniversalConduit>().type = outputType;
                outputObject.GetComponent<UniversalConduit>().speed = speed;
                outputID = outputObject.GetComponent<UniversalConduit>().ID;
                if (outputAmount >= speed)
                {
                    if (outputType.Equals(outputObject.GetComponent<UniversalConduit>().type))
                    {
                        if (powerON == true && connectionFailed == false && inputObject1 != null && inputObject2 != null && speed > 0)
                        {
                            conduitItem.active = true;
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GetComponent<AudioSource>().Play();
                                fireObject.SetActive(true);
                            }
                            machineTimer += 1;
                            if (machineTimer > 5 - (address * 0.01f))
                            {
                                outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                outputAmount -= speed;
                                machineTimer = 0;
                            }
                        }
                        else
                        {
                            conduitItem.active = false;
                            machineTimer = 0;
                        }
                    }
                    else
                    {
                        conduitItem.active = false;
                    }
                }
            }
            else
            {
                conduitItem.active = false;
            }
        }
        else
        {
            conduitItem.active = false;
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