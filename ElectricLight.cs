using UnityEngine;

public class ElectricLight : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    private float updateTick;
    public int address;
    public bool powerON;
    public GameObject powerObject;
    public PowerReceiver powerReceiver;

    void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
    }

    void OnDestroy()
    {

    }

    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }

    void Update()
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
}