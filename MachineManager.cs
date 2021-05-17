using System.Collections;
using UnityEngine;

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

