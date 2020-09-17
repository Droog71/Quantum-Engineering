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

    //! Called when the player's laser cannon shoots something.
    public void HitTarget(GameObject target,RaycastHit hit)
    {
        if (target.GetComponent<Meteor>() != null)
        {
            target.GetComponent<Meteor>().Explode();
        }

        if (target.GetComponent<Pirate>() != null)
        {
            target.GetComponent<Pirate>().TakeDamage();
        }

        if (target.tag.Equals("Built"))
        {
            if (target.GetComponent<PhysicsHandler>() != null)
            {
                target.GetComponent<PhysicsHandler>().Explode();
            }
            if (CanSendDestructionMessage())
            {
                if (playerController.destructionMessageActive == false)
                {
                    playerController.destructionMessageActive = true;
                    playerController.currentTabletMessage = "";
                }
                string objName = hit.collider.gameObject.name.Split('(')[0];
                playerController.currentTabletMessage += "ALERT: " + objName + " destroyed by your laser cannon!\n";
                playerController.destructionMessageCount += 1;
            }
        }

        if (target.tag.Equals("CombinedMesh"))
        {
            if (target.name.Equals("glassHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 25)
                    {
                        gameManager.meshManager.SeparateBlocks(hit.point, "glass",false);
                        if (CanSendDestructionMessage())
                        {
                            if (playerController.destructionMessageActive == false)
                            {
                                playerController.destructionMessageActive = true;
                                playerController.currentTabletMessage = "";
                            }
                            playerController.currentTabletMessage += "ALERT: Some glass blocks were hit by your laser cannon!\n";
                            playerController.destructionMessageCount += 1;
                        }
                    }
                }
            }
            else if (target.name.Equals("brickHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 50)
                    {
                        gameManager.meshManager.SeparateBlocks(hit.point, "brick",false);
                        if (CanSendDestructionMessage())
                        {
                            if (playerController.destructionMessageActive == false)
                            {
                                playerController.destructionMessageActive = true;
                                playerController.currentTabletMessage = "";
                            }
                            playerController.currentTabletMessage += "ALERT: Some bricks were hit by your laser cannon!\n";
                            playerController.destructionMessageCount += 1;
                        }
                    }
                }
            }

            if (target.name.Equals("ironHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 75)
                    {
                        gameManager.meshManager.SeparateBlocks(hit.point, "iron",false);
                        if (CanSendDestructionMessage())
                        {
                            if (playerController.destructionMessageActive == false)
                            {
                                playerController.destructionMessageActive = true;
                                playerController.currentTabletMessage = "";
                            }
                            playerController.currentTabletMessage += "ALERT: Some iron blocks were hit by your laser cannon!\n";
                            playerController.destructionMessageCount += 1;
                        }
                    }
                }
            }
            else if (target.name.Equals("steelHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 99)
                    {
                        gameManager.meshManager.SeparateBlocks(hit.point, "steel",false);
                        if (CanSendDestructionMessage())
                        {
                            if (playerController.destructionMessageActive == false)
                            {
                                playerController.destructionMessageActive = true;
                                playerController.currentTabletMessage = "";
                            }
                            playerController.currentTabletMessage += "ALERT: Some steel blocks were hit by your laser cannon!\n";
                            playerController.destructionMessageCount += 1;
                        }
                    }
                }
            }
        }
    }
}