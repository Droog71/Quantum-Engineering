using UnityEngine;

public class Relay : LogicBlock
{
    private Machine machine;

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (machine == null)
        {
            connectionAttempts += 1;
            if (creationMethod.Equals("spawned"))
            {
                if (connectionAttempts >= 1024)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
            }
            else
            {
                if (connectionAttempts >= 2048)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
            }
            if (connectionFailed == false)
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
        }
        else
        {
            outputID = machine.ID;
            machine.logic = logic;
            machine.powerON &= logic == false;
        }

        if (connectionFailed == false)
        {
            GetComponent<Renderer>().material.color = Color.white;
            if (logic == true)
            {
                GetComponent<Renderer>().material.mainTexture = onTexture;
            }
            else
            {
                GetComponent<Renderer>().material.mainTexture = offTexture;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}

