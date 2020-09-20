using System.Collections;
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

    //! Called once per frame when the player is looking at a steel block.
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

    //! Called once per frame when the player is looking at a glass block.
    public void InteractWithGlass()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Glass Block");
        }
    }

    //! Called once per frame when the player is looking at a brick block.
    public void InteractWithBricks()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Brick");
        }
    }

    //! Called once per frame when the player is looking at a combined mesh object.
    public void InteractWithCombinedMesh()
    {
        playerController.lookingAtCombinedMesh = true;
        if (cInput.GetKeyDown("Collect Object") && playerController.paintGunActive == false)
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (manager.working == false)
            {
                manager.meshManager.SeparateBlocks(playerController.transform.position, "all", true);
                playerController.separatedBlocks = true;
            }
            else
            {
                playerController.requestedChunkLoad = true;
            }
            if (playerController.building == false)
            {
                playerController.destroying = true;
                playerController.destroyStartPosition = playerController.transform.position;
            }
        }
        if (playerController.paintGunActive == true && playerController.paintColorSelected == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerController.paintGun.GetComponent<AudioSource>().Play();
                if (playerController.objectInSight.name.Equals("brickHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.bricks, "brickHolder"));
                }
                if (playerController.objectInSight.name.Equals("glassHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.glass, "glassHolder"));
                }
                if (playerController.objectInSight.name.Equals("ironHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.ironBlocks, "ironHolder"));
                }
                if (playerController.objectInSight.name.Equals("steelHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.steel, "steelHolder"));
                }
            }
        }
    }

    private IEnumerator PaintMesh(GameObject[] holders, string name)
    {
        foreach (GameObject holder in holders)
        {
            Color color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
            holder.GetComponent<Renderer>().material.color = color;
            Transform[] blocks = holder.GetComponentsInChildren<Transform>(true);
            foreach (Transform T in blocks)
            {
                T.gameObject.GetComponent<Renderer>().material.color = color;
            }
            yield return null;
            FileBasedPrefs.SetBool(playerController.stateManager.WorldName + name + holder.GetComponent<MeshPainter>().ID + "painted", true);
        }
    }
}
