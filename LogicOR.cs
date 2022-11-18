using UnityEngine;

public class LogicOR : LogicGATE
{
    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (output == null)
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
                LogicBlock[] logicBlocks = FindObjectsOfType<LogicBlock>();
                foreach (LogicBlock lb in logicBlocks)
                {
                    if (ConnectToLogicBlock(lb))
                    {
                        break;
                    }
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if (input != null && input2 != null)
        {
            outputID = output.ID;
            output.logic = input.logic || input2.logic;
            GetComponent<Renderer>().material.color = Color.white;
            if (output.logic == true)
            {
                GetComponent<Renderer>().material.mainTexture = onTexture;
            }
            else
            {
                GetComponent<Renderer>().material.mainTexture = offTexture;
            }
        }
    }

    private bool ConnectToLogicBlock(LogicBlock lb)
    {
        float distance = Vector3.Distance(transform.position,lb.transform.position);
        if (distance <= 6 && IsValidOutput(lb))
        {
            if (lb is LogicGATE)
            {
                LogicGATE lgate = lb as LogicGATE;
                if (lgate.input == null)
                {
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(outputID))
                    {
                        output = lgate;
                        lgate.input = this;
                        creationMethod = "built";
                        return true;
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        output = lgate;
                        lgate.input = this;
                        return true;
                    }
                }
                else if (lgate.input2 == null)
                {
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(outputID))
                    {
                        output = lgate;
                        lgate.input2 = this;
                        creationMethod = "built";
                        return true;
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        output = lgate;
                        lgate.input2 = this;
                        return true;
                    }
                }
            }
            else if (lb.input == null)
            {
                if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(outputID))
                {
                    output = lb;
                    lb.input = this;
                    creationMethod = "built";
                    return true;
                }
                else if (creationMethod.Equals("built"))
                {
                    output = lb;
                    lb.input = this;
                    return true;
                }
            }
        }
        return false;
    }

    //! Used to notify other blocks.
    public new void OnDestroy()
    {
        if (output != null)
        {
            output.logic = false;
        }
    }
}

