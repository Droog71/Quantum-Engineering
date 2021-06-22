using UnityEngine;

public class LogicBlock : Machine
{
    public string blockType;
    public Texture2D onTexture;
    public Texture2D offTexture;
    public LogicBlock output;
    public LogicBlock input;

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
    }

    public bool IsValidOutput(LogicBlock lb)
    {
        return lb != output && lb != this && lb.input == null && 
        lb.output != this && lb.blockType != "Player Detector" && 
        lb.blockType != "Power Detector" && lb.blockType != "Item Detector";
    }

    //! Used to notify other blocks.
    public void OnDestroy()
    {
        if (output != null)
        {
            output.logic = false;
        }
    }
}

