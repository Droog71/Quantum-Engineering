﻿using System.Collections;
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
        playerController.machineGUIopen = false;
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
        playerController.machineGUIopen = false;
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
        playerController.machineGUIopen = false;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Glass Block");
        }
    }

    //! Called once per frame when the player is looking at a brick block.
    public void InteractWithBricks()
    {
        playerController.machineGUIopen = false;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Brick");
        }
    }

    //! Called once per frame when the player is looking at an iron block.
    public void InteractWithModBlock(string blockName)
    {
        playerController.machineGUIopen = false;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject(blockName);
        }
    }

    //! Called once per frame when the player is looking at a combined mesh object.
    public void InteractWithCombinedMesh()
    {
        playerController.machineGUIopen = false;
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
                if (playerController.objectInSight.name.Equals("glassHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.glassHolders, "glassHolder"));
                }
                if (playerController.objectInSight.name.Equals("brickHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.brickHolders, "brickHolder"));
                }
                if (playerController.objectInSight.name.Equals("ironHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.ironHolders, "ironHolder"));
                }
                if (playerController.objectInSight.name.Equals("steelHolder(Clone)"))
                {
                    interactionController.paintingCoroutine = interactionController.StartCoroutine(PaintMesh(playerController.gameManager.steelHolders, "steelHolder"));
                }
            }
        }
    }

    private IEnumerator PaintMesh(GameObject[] holders, string name)
    {
        playerController.machineGUIopen = false;
        BlockDictionary blockDictionary = new BlockDictionary(playerController);
        Color color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
        foreach (GameObject holder in holders)
        {
            holder.GetComponent<Renderer>().material.color = color;
            FileBasedPrefs.SetBool(playerController.stateManager.worldName + name + holder.GetComponent<MeshPainter>().ID + "painted", true);
            yield return null;
        }
        int interval = 0;
        if (name == "glassHolder")
        {
            Glass[] allGlassBlocks = Object.FindObjectsOfType<Glass>();
            foreach (Glass block in allGlassBlocks)
            {
                block.GetComponent<Renderer>().material.color = color;
                interval++;
                if (interval >= 500)
                {
                    interval = 0;
                    yield return null;
                }
            }
        }
        if (name == "brickHolder")
        {
            Transform[] allBuiltObjects = playerController.gameManager.builtObjects.GetComponentsInChildren<Transform>(true);
            foreach (Transform block in allBuiltObjects)
            {
                if (block.GetComponent<Brick>() != null)
                {
                    block.GetComponent<Renderer>().material.color = color;
                    interval++;
                    if (interval >= 500)
                    {
                        interval = 0;
                        yield return null;
                    }
                }
            }
        }
        if (name == "ironHolder")
        {
            Transform[] allBuiltObjects = playerController.gameManager.builtObjects.GetComponentsInChildren<Transform>(true);
            foreach (Transform block in allBuiltObjects)
            {
                if (block.GetComponent<IronBlock>() != null)
                {
                    block.GetComponent<Renderer>().material.color = color;
                    interval++;
                    if (interval >= 500)
                    {
                        interval = 0;
                        yield return null;
                    }
                }
            }
        }
        if (name == "steelHolder")
        {
            Transform[] allBuiltObjects = playerController.gameManager.builtObjects.GetComponentsInChildren<Transform>(true);
            foreach (Transform block in allBuiltObjects)
            {
                if (block.GetComponent<Steel>() != null)
                {
                    block.GetComponent<Renderer>().material.color = color;
                    interval++;
                    if (interval >= 500)
                    {
                        interval = 0;
                        yield return null;
                    }
                }
            }
        }
    }
}
