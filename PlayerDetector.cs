using UnityEngine;

public class PlayerDetector : LogicBlock
{
    private GameObject player;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        bool detected = false;

        NetworkPlayer[] networkPlayers = FindObjectsOfType<NetworkPlayer>();
        foreach (NetworkPlayer netWorkPlayer in networkPlayers)
        {
            float networkPlayerDistance = Vector3.Distance(netWorkPlayer.transform.position, transform.position);
            if (networkPlayerDistance < 20)
            {
                detected = true;
                break;
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            detected = true;
        }

        logic = detected;

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
                if (distance <= 6 && lb != this && lb.input == null && lb.output != this && lb.blockType != "Player Detector")
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
}