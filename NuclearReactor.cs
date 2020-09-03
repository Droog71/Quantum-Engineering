using UnityEngine;
using System.Collections;

public class NuclearReactor : MonoBehaviour
{
    public bool hasHeatExchanger;
    public int cooling;
    public string ID = "unassigned";
    public string creationMethod = "built";
    private float updateTick;
    public int address;
    public int turbineCount;
    public bool sufficientCooling;

    // Called once per frame by unity engine
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
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
}