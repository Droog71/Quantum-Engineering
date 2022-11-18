using UnityEngine;

public class LogicDelayer : LogicBlock
{
    private int delay;

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
        }
        else
        {
            outputID = output.ID;
            if (!(output is LogicGATE))
            {
                if (output.logic != logic)
                {
                    delay++;
                    if (delay >= 10)
                    {
                        delay = 0;
                        output.logic = logic;
                    }
                }
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

    private bool ConnectToLogicBlock(LogicBlock lb)
    {
        float distance = Vector3.Distance(transform.position,lb.transform.position);
        if (distance <= 6 && IsValidOutput(lb))
        {
            if (lb is LogicGATE)
            {
                LogicGATE land = lb as LogicGATE;
                if (land.input == null)
                {
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(outputID))
                    {
                        output = land;
                        land.input = this;
                        creationMethod = "built";
                        return true;
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        output = land;
                        land.input = this;
                        return true;
                    }
                }
                else if (land.input2 == null)
                {
                    if (creationMethod.Equals("spawned") && lb.GetComponent<LogicBlock>().ID.Equals(outputID))
                    {
                        output = land;
                        land.input2 = this;
                        creationMethod = "built";
                        return true;
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        output = land;
                        land.input2 = this;
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
}

