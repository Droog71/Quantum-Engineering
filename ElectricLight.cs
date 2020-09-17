using UnityEngine;

public class ElectricLight : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod = "built";
    private float updateTick;
    public int address;
    public bool powerON;
    public GameObject powerObject;
    public PowerReceiver powerReceiver;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 1 + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

            updateTick = 0;
            if (powerON == true)
            {
                GetComponent<Light>().enabled = true;
            }
            else
            {
                GetComponent<Light>().enabled = false;
            }
        }
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }
}