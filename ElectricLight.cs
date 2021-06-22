using UnityEngine;

public class ElectricLight : Machine
{
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
        if (logic == false)
        {
            powerReceiver.ID = ID;
            powerON = powerReceiver.powerON;
            powerObject = powerReceiver.powerObject;
        }
    }
}