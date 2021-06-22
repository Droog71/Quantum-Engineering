using System;
using System.Collections;
using UnityEngine;

//! This class controls machine update functions via coroutine .
//! One machine is updated per frame.
public class MachineManager : MonoBehaviour
{
    private bool busy;
    private Coroutine machineUpdateCoroutine;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (busy == false && GetComponent<StateManager>().initMachines == true)
        {
            machineUpdateCoroutine = StartCoroutine(MachineUpdateCoroutine());
        }
    }

    //! Calls the UpdateMachine function on each machine in the world, yielding after each call.
    private IEnumerator MachineUpdateCoroutine()
    {
        busy = true;
        int interval = 0;
        Machine[] machines = FindObjectsOfType<Machine>();
        foreach (Machine machine in machines)
        {
            if (machine != null)
            {
                try
                {
                    if (machine.gameManager == null)
                    {
                        machine.gameManager = FindObjectOfType<GameManager>();
                    }
                    machine.UpdateMachine();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                interval++;
                if (interval >= machines.Length * GetComponent<GameManager>().simulationSpeed)
                {
                    yield return null;
                    interval = 0;
                }
            }
        }
        busy = false;
    }
}