using UnityEngine;

public class LogicSplitter : LogicBlock
{
    public LogicBlock output2;
    public string output2ID;

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (output == null || output2 == null)
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
                if (output == null)
                {
                    LogicBlock[] logicBlocks = FindObjectsOfType<LogicBlock>();
                    foreach (LogicBlock lb in logicBlocks)
                    {
                        if (ConnectToLogicBlock(lb, 1, outputID))
                        {
                            break;
                        }
                    }
                }
                else if (output2 == null)
                {
                    LogicBlock[] logicBlocks = FindObjectsOfType<LogicBlock>();
                    foreach (LogicBlock lb in logicBlocks)
                    {
                        if (ConnectToLogicBlock(lb, 2, output2ID))
                        {
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            creationMethod = "built";
            outputID = output.ID;
            output2ID = output2.ID;
            if (!(output is LogicGATE))
            {
                output.logic = logic;
            }
            if (!(output2 is LogicGATE))
            {
                output2.logic = logic;
            }
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

    private bool ConnectToLogicBlock(LogicBlock lb, int port, string portID)
    {
        float distance = Vector3.Distance(transform.position,lb.transform.position);
        if (distance <= 6 && IsValidOutput(lb))
        {
            if (lb is LogicGATE)
            {
                LogicGATE lgate = lb as LogicGATE;
                if (lgate.input == null)
                {
                    if (creationMethod.Equals("built"))
                    {
                        if (port == 1)
                        {
                            output = lgate;
                        }
                        else
                        {
                            output2 = lgate;
                        }
                        lgate.input = this;
                        return true;
                    }
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(portID))
                    {
                        if (port == 1)
                        {
                            output = lgate;
                        }
                        else
                        {
                            output2 = lgate;
                        }
                        lgate.input = this;
                        return true;
                    }
                }
                else if (lgate.input2 == null)
                {
                    if (creationMethod.Equals("built"))
                    {
                        if (port == 1)
                        {
                            output = lgate;
                        }
                        else
                        {
                            output2 = lgate;
                        }
                        lgate.input2 = this;
                        return true;
                    }
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(portID))
                    {
                        if (port == 1)
                        {
                            output = lgate;
                        }
                        else
                        {
                            output2 = lgate;
                        }
                        lgate.input2 = this;
                        return true;
                    }
                }
            }
            else if (lb.input == null)
            {
                if (creationMethod.Equals("built"))
                {
                    if (port == 1)
                    {
                        output = lb;
                    }
                    else
                    {
                        output2 = lb;
                    }
                    lb.input = this;
                    return true;
                }
                if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(portID))
                {
                    if (port == 1)
                    {
                        output = lb;
                    }
                    else
                    {
                        output2 = lb;
                    }
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

        if (output2 != null)
        {
            output2.logic = false;
        }
    }
}

