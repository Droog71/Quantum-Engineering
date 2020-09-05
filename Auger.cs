using UnityEngine;

public class Auger : MonoBehaviour
{
    public float amount;
    public int speed = 1;
    public int power;
    public bool hasHeatExchanger;
    public int heat;
    public int cooling;
    public GameObject outputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    public string ID = "unassigned";
    public string creationMethod;
    private LineRenderer connectionLine;
    public PowerReceiver powerReceiver;
    private float updateTick;
    public int address;
    public bool powerON;
    private int machineTimer;

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
            if (speed > power)
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

            if (powerON == true && speed > 0)
            {
                conduitItem.GetComponent<ConduitItem>().active = true;
                GetComponent<Light>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
                machineTimer += 1;
                if (machineTimer > 5 - (address * 0.01f))
                {
                    amount += speed - heat;
                    machineTimer = 0;
                }
            }
            else
            {
                machineTimer = 0;
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
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