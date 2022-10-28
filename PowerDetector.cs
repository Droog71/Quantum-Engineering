﻿using UnityEngine;

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

        if (output == null)
        {
            LogicBlock[] logicBlocks = FindObjectsOfType<LogicBlock>();
            foreach (LogicBlock lb in logicBlocks)
            {
                float distance = Vector3.Distance(transform.position,lb.transform.position);
                if (distance <= 6 && IsValidOutput(lb))
                {
                    output = lb;
                    output.input = this;
                    break;
                }
            }
        }
        else
        {
            output.logic = logic;
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

