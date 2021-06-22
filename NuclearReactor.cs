using UnityEngine;

public class NuclearReactor : Machine
{
    public bool hasHeatExchanger;
    public int cooling;
    public int turbineCount;
    public bool sufficientCooling;
    private StateManager stateManager;

    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        int currentTurbineCount = 0;

        if (Physics.Raycast(transform.position, transform.up, out RaycastHit reactorUpHit, 3))
        {
            if (reactorUpHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorUpHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit reactorDownHit, 3))
        {
            if (reactorDownHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorDownHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out RaycastHit reactorRightHit, 3))
        {
            if (reactorRightHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorRightHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit reactorLeftHit, 3))
        {
            if (reactorLeftHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorLeftHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit reactorFrontHit, 3))
        {
            if (reactorFrontHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorFrontHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit reactorBackHit, 3))
        {
            if (reactorBackHit.collider.gameObject.GetComponent<PowerSource>() != null)
            {
                if (reactorBackHit.collider.gameObject.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    currentTurbineCount++;
                }
            }
        }

        turbineCount = currentTurbineCount;
        if (cooling >= turbineCount * 5)
        {
            sufficientCooling = true;
        }
        else
        {
            sufficientCooling = false;
        }
    }
}