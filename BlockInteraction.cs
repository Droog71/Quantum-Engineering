using UnityEngine;

public class BlockInteraction
{
    private PlayerController playerController;
    private InteractionController interactionController;

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
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject(playerController.objectInSight, blockName);
        }
    }

    //! Called once per frame when the player is looking at a combined mesh object.
    public void InteractWithCombinedMesh(Vector3 point)
    {
        playerController.machineGUIopen = false;
        playerController.lookingAtCombinedMesh = true;
        if (cInput.GetKey("Collect Object"))
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
                        playerController.Invoke("CreateMesh", 0.1f);
                        bh.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
