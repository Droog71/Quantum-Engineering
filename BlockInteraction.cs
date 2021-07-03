using UnityEngine;
using System.Collections.Generic;
using MEC;

public class BlockInteraction
{
    private PlayerController playerController;
    private InteractionController interactionController;
    private bool modifyMeshCoroutineBusy;

    //! This class handles the player's interactions with standard building blocks.
    public BlockInteraction(PlayerController playerController, InteractionController interactionController)
    {
        this.playerController = playerController;
        this.interactionController = interactionController;
    }

    //! Called once per frame when the player is looking at an iron block.
    public void InteractWithBlock(string blockName)
    {
        playerController.machineGUIopen = false;
        if (cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, blockName);
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }
    }

    //! Called once per frame when the player is looking at a combined mesh object.
    public void InteractWithCombinedMesh(Vector3 point)
    {
        playerController.machineGUIopen = false;
        playerController.lookingAtCombinedMesh = true;
        if (cInput.GetKey("Collect Object"))
        {
            if (modifyMeshCoroutineBusy == false)
            {
                BlockHolder bh = playerController.objectInSight.GetComponent<BlockHolder>();
                if (bh == null)
                {
                    bh = playerController.objectInSight.transform.parent.GetComponent<BlockHolder>();
                }

                if (bh != null)
                {
                    if (bh.unloaded == true)
                    {
                        bh.Load();
                    }
                    else if (bh.blockData != null)
                    {
                        playerController.digTime += 1 * Time.deltaTime;
                        if (playerController.digTime > 0.15f)
                        {
                            Dig(bh, point);
                            playerController.digTime = 0;
                        }
                    }
                }
            }
        }
        else
        {
            playerController.digTime = 0;
        }
    }

    //! Removes a block.
    private void Dig(BlockHolder bh, Vector3 point)
    {
        Block blockToRemove = null;
        int indexToRemove = 0;
        for (int index = 0; index < bh.blockData.Count; index++)
        {
            BlockHolder.BlockInfo info = bh.blockData[index];
            float distance = Vector3.Distance(info.position, point);
            if (distance < 5)
            {
                Block[] blocks = playerController.objectInSight.GetComponentsInChildren<Block>(true);
                foreach (Block block in blocks)
                {
                    if (block != null)
                    {
                        Vector3 pos = block.gameObject.transform.position;
                        Vector3 roundPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
                        Vector3 roundInfoPos = new Vector3(Mathf.Round(info.position.x), Mathf.Round(info.position.y), Mathf.Round(info.position.z));
                        if (roundPos == roundInfoPos)
                        {
                            blockToRemove = block;
                            indexToRemove = index;
                            break;
                        }
                    }
                }
            }
        }
        if (blockToRemove != null)
        {
            interactionController.CollectObject(blockToRemove.gameObject, bh.blockType);
            bh.blockData.RemoveAt(indexToRemove);
            Timing.RunCoroutine(ModifyMesh(bh.gameObject));
            bh.gameObject.SetActive(true);
        }
    }

    //! Modifies a combined mesh with a slight delay to allow the BlockHolder class to load data.
    private IEnumerator<float> ModifyMesh(GameObject obj)
    {
        modifyMeshCoroutineBusy = true;
        yield return Timing.WaitForSeconds(0.1f);
        if (obj != null)
        {
            BlockHolder bh = obj.GetComponent<BlockHolder>();
            if (bh != null)
            {
                if (bh.chunkLoadCoroutineBusy == false && bh.chunkUnloadCoroutineBusy == false && bh.unloaded == false)
                {
                    playerController.gameManager.meshManager.CreateCombinedMesh(obj);
                }
            }
        }
        modifyMeshCoroutineBusy = false;
    }
}
