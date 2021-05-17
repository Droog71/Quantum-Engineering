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
        if (busy == false && GetComponent<StateManager>().worldLoaded == true)
        {
            machineUpdateCoroutine = StartCoroutine(MachineUpdateCoroutine());
        }
    }

    //! Calls the UpdateMachine function on each machine in the world, yielding after each call.
    private IEnumerator MachineUpdateCoroutine()
    {
        busy = true;
        Machine[] machines = FindObjectsOfType<Machine>();
        foreach (Machine machine in machines)
        {
            machine.UpdateMachine();
            yield return null;
        }
        busy = false;
    }
}