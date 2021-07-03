using UnityEngine;

public class DarkMatterCollector : Machine
{
    public float darkMatterAmount;
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public GameObject outputObject;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    public PowerReceiver powerReceiver;
    private LineRenderer connectionLine;
    private LineRenderer inputLine;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    public bool foundDarkMatter;

    //! Called by unity engine on start up to initialize variables.
    private void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        UpdatePowerReceiver();

        if (speed > power && power != 0)
        {
            speed = power;
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

        if (foundDarkMatter == true)
        {
            if (powerON == true && connectionFailed == false && speed > 0)
            {
                conduitItem.active = connectionLine.enabled;
                GetComponent<Light>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
                machineTimer += 1;
                if (machineTimer > 5)
                {
                    darkMatterAmount += speed - heat;
                    machineTimer = 0;
                }
            }
            else
            {
                machineTimer = 0;
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
            }
        }
        if (foundDarkMatter == false)
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
                if (connectionAttempts >= 512)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
            }

            if (connectionFailed == false)
            {
                FindDarkMatter();
            }
        }
    }

    //! Used to remove the connection line renderer when the block is destroyed.
    public void OnDestroy()
    {
        if (inputLine != null)
        {
            Destroy(inputLine);
        }
    }

    //! Finds a dark matter source node to harvest.
    private void FindDarkMatter()
    {
        DarkMatter[] allDarkMatter = FindObjectsOfType<DarkMatter>();
        foreach (DarkMatter d in allDarkMatter)
        {
            GameObject obj = d.gameObject;
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < 20)
            {
                if (obj.GetComponent<DarkMatter>().collector == null)
                {
                    obj.GetComponent<DarkMatter>().collector = gameObject;
                }
                if (obj.GetComponent<DarkMatter>().collector == gameObject)
                {
                    if (inputLine == null && obj.GetComponent<LineRenderer>() == null)
                    {
                        inputLine = obj.AddComponent<LineRenderer>();
                        inputLine.startWidth = 0.2f;
                        inputLine.endWidth = 0.2f;
                        inputLine.material = lineMat;
                        inputLine.SetPosition(0, transform.position);
                        inputLine.SetPosition(1, obj.transform.position);
                    }
                    foundDarkMatter = true;
                }
            }
        }
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        if (logic == false)
        {
            powerReceiver.ID = ID;
            power = powerReceiver.power;
            powerON = powerReceiver.powerON;
            powerObject = powerReceiver.powerObject;
        }
    }
}