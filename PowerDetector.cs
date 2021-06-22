using UnityEngine;

public class PowerDetector : LogicBlock
{
    private Machine machine;

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (logic == true)
        {
            GetComponent<Renderer>().material.mainTexture = onTexture;
        }
        else
        {
            GetComponent<Renderer>().material.mainTexture = offTexture;
        }

        if (machine == null)
        {
            Machine[] machines = FindObjectsOfType<Machine>();
            foreach (Machine m in machines)
            {
                float distance = Vector3.Distance(transform.position, m.transform.position);
                if (distance <= 6)
                {
                    machine = m;
                }
            }
        }
        else
        {
            logic = machine.powerON;
        }
    }
}

