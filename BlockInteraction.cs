using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private InteractionController interactionController;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionController = GetComponent<InteractionController>();
    }

    public void InteractWithIronBlock()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            string objectName = "";
            if (playerController.objectInSight.name.Equals("IronRamp(Clone)"))
            {
                objectName = "Iron Ramp";

            }
            else
            {
                objectName = "Iron Block";
            }

            interactionController.CollectObject(objectName);
        }
    }

    public void InteractWithSteelBlock()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            string objectName = "";
            if (playerController.objectInSight.name.Equals("SteelRamp(Clone)"))
            {
                objectName = "Steel Ramp";
            }
            else
            {
                objectName = "Steel Block";
            }

            interactionController.CollectObject(objectName);
        }
    }

    public void InteractWithGlass()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Glass Block");
        }
    }

    public void InteractWithBricks()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Brick");
        }
    }

    public void InteractWithCombinedMesh()
    {
        playerController.lookingAtCombinedMesh = true;
        if (cInput.GetKeyDown("Collect Object") && playerController.paintGunActive == false)
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (manager.working == false)
            {
                manager.meshManager.SeparateBlocks(transform.position, "all", true);
                playerController.separatedBlocks = true;
            }
            else
            {
                playerController.requestedChunkLoad = true;
            }
            if (playerController.building == false)
            {
                playerController.destroying = true;
                playerController.destroyStartPosition = transform.position;
            }
        }
        if (playerController.paintGunActive == true && playerController.paintColorSelected == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerController.paintGun.GetComponent<AudioSource>().Play();
                playerController.objectInSight.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                if (playerController.objectInSight.name.Equals("brickHolder(Clone)"))
                {
                    FileBasedPrefs.SetBool(playerController.stateManager.WorldName + "brickHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                }
                if (playerController.objectInSight.name.Equals("glassHolder(Clone)"))
                {
                    FileBasedPrefs.SetBool(playerController.stateManager.WorldName + "glassHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                }
                if (playerController.objectInSight.name.Equals("ironHolder(Clone)"))
                {
                    FileBasedPrefs.SetBool(playerController.stateManager.WorldName + "ironHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                }
                if (playerController.objectInSight.name.Equals("steelHolder(Clone)"))
                {
                    FileBasedPrefs.SetBool(playerController.stateManager.WorldName + "steelHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                }
            }
        }
    }
}
