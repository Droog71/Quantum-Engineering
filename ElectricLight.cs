using UnityEngine;

public class ElectricLight : Machine
{
    public string ID = "unassigned";
    public string creationMethod = "built";
    public int address;
    public bool powerON;
    public GameObject powerObject;
    public PowerReceiver powerReceiver;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
        UpdatePowerReceiver();

        if (powerON == true)
        {
            GetComponent<Light>().enabled = true;
        }
        else
        {
            GetComponent<Light>().enabled = false;
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