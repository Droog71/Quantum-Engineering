using UnityEngine;

public class LaserController
{
    private GameManager gameManager;
    private PlayerController playerController;

    //! This class handles RaycastHits for the player's laser cannon.
    public LaserController(PlayerController playerController, GameManager gameManager)
    {
        this.playerController = playerController;
        this.gameManager = gameManager;
    }

    //! Returns true if a message can be sent to the player's tablet.
    private bool CanSendDestructionMessage()
    {
        return playerController.timeToDeliver == false && playerController.meteorShowerWarningActive == false && playerController.pirateAttackWarningActive == false;
    }

    //! Returns true if the game object is parented to a combined mesh object.
    private bool CombinedMeshParent(GameObject target)
    {
        if (target.transform.parent != null)
        {
            return target.transform.parent.tag.Equals("CombinedMesh");
        }

        return false;
    }

    //! Called when the player's laser cannon shoots something.
    public void HitTarget(GameObject target,RaycastHit hit)
    {
        ProtectionBlock[] protectionBlocks = Object.FindObjectsOfType<ProtectionBlock>();
        foreach (ProtectionBlock protectionBlock in protectionBlocks)
        {
            Vector3 blockPos = protectionBlock.transform.position;
            Vector3 hitNoY = new Vector3(hit.point.x, 0, hit.point.z);
            Vector3 blockPosNoY = new Vector3(blockPos.x, 0, blockPos.z);
            float distance = Vector3.Distance(hitNoY, blockPosNoY);
            if (distance <= 160)
            {
                return;
            }
        }

        string objName;

        if (target.GetComponent<Meteor>() != null)
        {
            target.GetComponent<Meteor>().Explode();
        }

        if (target.GetComponent<Pirate>() != null)
        {
            target.GetComponent<Pirate>().TakeDamage();
        }

        if (target.tag.Equals("Built") || target.tag.Equals("Machine"))
        {
            if (target.GetComponent<Block>() != null)
            {
                objName = target.GetComponent<Block>().blockName;
            }
            else
            {
                objName = hit.collider.gameObject.name.Split('(')[0];
            }

            if (target.GetComponent<Block>() != null)
            {
                target.GetComponent<Block>().Explode();
            }
            else if (target.GetComponent<Machine>() != null)
            {
                target.GetComponent<Machine>().Explode();
            }

            if (CanSendDestructionMessage())
            {
                if (playerController.destructionMessageActive == false)
                {
                    playerController.destructionMessageActive = true;
                    playerController.currentTabletMessage = "";
                }
                playerController.currentTabletMessage += "ALERT: " + objName + " destroyed by your laser cannon!\n";
                playerController.destructionMessageCount += 1;
            }
        }

        if (target.tag.Equals("CombinedMesh") || CombinedMeshParent(target))
        {
            BlockHolder blockHolder = target.GetComponent<BlockHolder>();

            if (blockHolder == null)
            {
                blockHolder = target.transform.parent.GetComponent<BlockHolder>();
            }

            if (blockHolder != null)
            {
                int toughness = 75;

                if (blockHolder.blockType.ToUpper().Contains("GLASS"))
                {
                    toughness = 25;
                }
                else if (blockHolder.blockType.ToUpper().Contains("STEEL"))
                {
                    toughness = 99;
                }

                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > toughness)
                    {
                        gameManager.meshManager.RemoveBlock(blockHolder, hit.point, true);
                        if (CanSendDestructionMessage())
                        {
                            if (blockHolder.blockType != "Dirt" && blockHolder.blockType != "Grass")
                            {
                                if (playerController.destructionMessageActive == false)
                                {
                                    playerController.destructionMessageActive = true;
                                    playerController.currentTabletMessage = "";
                                }
                                playerController.currentTabletMessage += "ALERT: " + blockHolder.blockType + " was hit by your laser cannon!\n";
                                playerController.destructionMessageCount += 1;
                            }
                        }
                    }
                }
            }
        }
    }
}