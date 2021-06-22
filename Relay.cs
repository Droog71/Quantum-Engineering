using UnityEngine;

public class Relay : LogicBlock
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
                if (distance <= 6 && m != this && m.GetComponent<LogicBlock>() == null)
                {
                    machine = m;
                }
            }
        }
        else
        {
            machine.logic = logic;
            machine.powerON &= logic == false;
        }
    }
}

