using System;
using MEC;
using System.Collections.Generic;
using UnityEngine;

//! This class controls machine update functions via coroutine .
//! One machine is updated per frame.
public class MachineManager : MonoBehaviour
{
    private bool busy;
    private bool paused;
    public List<Machine> machines;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (machines == null)
        {
            machines = new List<Machine>();
        }

        if (busy == false && paused == false && GetComponent<StateManager>().initMachines == true)
        {
            Timing.RunCoroutine(MachineUpdateCoroutine(), "MachineUpdateCoroutine");
        }
    }

    public void AddMachine(Machine machine)
    {
        paused = true;
        busy = false;
        Timing.KillCoroutines("MachineUpdateCoroutine");
        machines.Add(machine);
        paused = false;
    }

    //! Calls the UpdateMachine function on each machine in the world, yielding after each call.
    private IEnumerator<float> MachineUpdateCoroutine()
    {
        busy = true;
        int interval = 0;
        foreach (Machine machine in machines)
        {
            if (machine != null)
            {
                try
                {
                    if (machine.gameManager == null)
                    {
                        machine.gameManager = GetComponent<GameManager>();
                    }

                    if (machine.stateManager == null)
                    {
                        machine.stateManager = GetComponent<StateManager>();
                    }

                    machine.UpdateMachine();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                interval++;
                if (interval >= machines.Count * GetComponent<GameManager>().simulationSpeed)
                {
                    yield return Timing.WaitForOneFrame;
                    interval = 0;
                }
            }
        }
        busy = false;
    }
}