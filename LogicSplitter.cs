using UnityEngine;

public class LogicSplitter : LogicBlock
{
    public LogicBlock output2;

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
        else if (output2 == null)
        {
            LogicBlock[] logicBlocks = FindObjectsOfType<LogicBlock>();
            foreach (LogicBlock lb in logicBlocks)
            {
                float distance = Vector3.Distance(transform.position,lb.transform.position);
                if (distance <= 6 && IsValidOutput(lb))
                {
                    output2 = lb;
                    output2.input = this;
                    break;
                }
            }
        }
        else
        {
            output.logic = logic;
            output2.logic = logic;
        }
    }

    //! Used to notify other blocks.
    public new void OnDestroy()
    {
        if (output != null)
        {
            output.logic = false;
        }

        if (output2 != null)
        {
            output2.logic = false;
        }
    }
}

