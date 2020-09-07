using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private InteractionController interactionController;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionController = GetComponent<InteractionController>();
    }

    // Called once per frame when the player is looking at an iron block.
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

    // Called once per frame when the player is looking at a steel block.
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

    // Called once per frame when the player is looking at a glass block.
    public void InteractWithGlass()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Glass Block");
        }
    }

    // Called once per frame when the player is looking at a brick block.
    public void InteractWithBricks()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Brick");
        }
    }

    // Called once per frame when the player is looking at a combined mesh object.
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
