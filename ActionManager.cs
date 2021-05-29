using UnityEngine;

public class ActionManager
{
    private PlayerController playerController;
    private CombinedMeshManager meshManager;

    //! This class contains all functions called by the input manager.
    public ActionManager(PlayerController playerController)
    {
        this.playerController = playerController;
        meshManager = playerController.gameManager.meshManager;
    }

    //! Toggles the crosshair.
    public void ToggleCrosshair()
    {
        if (playerController.crosshairEnabled == true)
        {
            playerController.crosshairEnabled = false;
        }
        else
        {
            playerController.crosshairEnabled = true;
        }
        playerController.PlayButtonSound();
    }

    //! Toggles the head lamp.
    public void ToggleHeadLamp()
    {
        if (playerController.headlamp.GetComponent<Light>() != null)
        {
            if (playerController.headlamp.GetComponent<Light>().enabled == true)
            {
                playerController.headlamp.GetComponent<Light>().enabled = false;
            }
            else
            {
                playerController.headlamp.GetComponent<Light>().enabled = true;
            }
        }
        playerController.PlayButtonSound();
    }

    //! Toggles the laser cannon.
    public void ToggleLaserCannon()
    {
        if (playerController.building == true || playerController.destroying == true)
        {
            if (playerController.gameManager.working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                meshManager.CombineBlocks();
                playerController.separatedBlocks = false;
                playerController.building = false;
                playerController.destroying = false;
            }
            else
            {
                playerController.requestedBuildingStop = true;
            }
        }
        if (!playerController.laserCannon.activeSelf)
        {
            playerController.paintGun.SetActive(false);
            playerController.paintGunActive = false;
            playerController.paintColorSelected = false;
            playerController.scanner.SetActive(false);
            playerController.scannerActive = false;
            playerController.laserCannon.SetActive(true);
            playerController.laserCannonActive = true;
        }
        else
        {
            playerController.laserCannon.SetActive(false);
            playerController.laserCannonActive = false;
        }
    }

    //! Toggles the scanner.
    public void ToggleScanner()
    {
        if (playerController.building == true || playerController.destroying == true)
        {
            if (playerController.gameManager.working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                meshManager.CombineBlocks();
                playerController.separatedBlocks = false;
                playerController.building = false;
                playerController.destroying = false;
            }
            else
            {
                playerController.requestedBuildingStop = true;
            }
        }
        if (!playerController.scanner.activeSelf)
        {
            playerController.paintGun.SetActive(false);
            playerController.paintGunActive = false;
            playerController.paintColorSelected = false;
            playerController.laserCannon.SetActive(false);
            playerController.laserCannonActive = false;
            playerController.scanner.SetActive(true);
            playerController.scannerActive = true;
        }
        else
        {
            playerController.scanner.SetActive(false);
            playerController.scannerActive = false;
        }
    }

    //! Toggles the paint gun.
    public void TogglePaintGun()
    {
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == false)
        {
            if (playerController.building == true || playerController.destroying == true)
            {
                if (playerController.gameManager.working == false)
                {
                    playerController.stoppingBuildCoRoutine = true;
                    meshManager.CombineBlocks();
                    playerController.separatedBlocks = false;
                    playerController.building = false;
                    playerController.destroying = false;
                }
                else
                {
                    playerController.requestedBuildingStop = true;
                }
            }
            if (playerController.paintGunActive == false)
            {
                playerController.paintGunActive = true;
                playerController.paintGun.SetActive(true);
                playerController.laserCannon.SetActive(false);
                playerController.laserCannonActive = false;
                playerController.scanner.SetActive(false);
                playerController.scannerActive = false;
            }
            else
            {
                playerController.paintGun.SetActive(false);
                playerController.paintGunActive = false;
                playerController.paintColorSelected = false;
            }
        }
    }

    //! Toggles the inventory GUI.
    public void ToggleInventory()
    {
        if (!playerController.GuiOpen())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.machineGUIopen = false;
            playerController.inventoryOpen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.inventoryOpen = false;
            playerController.marketGUIopen = false;
            playerController.craftingGUIopen = false;
            playerController.storageGUIopen = false;
            playerController.machineGUIopen = false;
        }
    }

    //! Toggles the crafting GUI.
    public void ToggleCraftingGUI()
    {
        if (!playerController.GuiOpen())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.machineGUIopen = false;
            playerController.inventoryOpen = true;
            playerController.craftingGUIopen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.inventoryOpen = false;
            playerController.marketGUIopen = false;
            playerController.craftingGUIopen = false;
            playerController.storageGUIopen = false;
            playerController.machineGUIopen = false;
        }
    }

    //! Toggles the crafting GUI.
    public void ToggleMarketGUI()
    {
        if (!playerController.GuiOpen())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.machineGUIopen = false;
            playerController.craftingGUIopen = false;
            playerController.marketGUIopen = true;
            float distance = Vector3.Distance(playerController.gameObject.transform.position, GameObject.Find("Rocket").transform.position);
            playerController.inventoryOpen = distance <= 40;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.inventoryOpen = false;
            playerController.marketGUIopen = false;
            playerController.craftingGUIopen = false;
            playerController.storageGUIopen = false;
            playerController.machineGUIopen = false;
        }
    }

    //! Toggles the tablet GUI.
    public void ToggleTablet()
    {
        if (!playerController.GuiOpen())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.tabletOpen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.tabletOpen = false;
        }
    }

    //! Returns true when the player is walking on metal.
    private bool OnMetal(RaycastHit hit)
    {
        return hit.collider.gameObject.GetComponent<IronBlock>() != null
        || hit.collider.gameObject.GetComponent<Steel>() != null
        || hit.collider.gameObject.name.Equals("ironHolder(Clone)")
        || hit.collider.gameObject.name.Equals("steelHolder(Clone)");
    }

    //! Returns true when varied footstep sounds should loop back to the first sound.
    private bool InitMetalStepSounds()
    {
        return playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep1
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep2
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep3
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep4
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep1;
    }

    //! Returns true when varied footstep sounds should loop back to the first sound.
    private bool InitGroundStepSounds()
    {
        return playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep1
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep1
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep2
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep3
        || playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep4;
    }

    //! Handles head bob, held item movement and footstep sound effects.
    public void DoGroundEffects(RaycastHit hit)
    {
        //HEAD BOB
        playerController.mCam.GetComponent<HeadBob>().active = true;

        //HELD OBJECT MOVEMENT
        if (playerController.gameObject.GetComponent<AudioSource>().isPlaying == true)
        {
            playerController.gameObject.GetComponent<AudioSource>().Stop();
        }
        if (playerController.scannerActive == true)
        {
            playerController.scanner.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            playerController.scanner.GetComponent<HeldItemSway>().active = false;
        }
        if (playerController.laserCannonActive == true)
        {
            playerController.laserCannon.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            playerController.laserCannon.GetComponent<HeldItemSway>().active = false;
        }
        if (playerController.paintGunActive == true)
        {
            playerController.paintGun.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            playerController.paintGun.GetComponent<HeldItemSway>().active = false;
        }

        //FOOTSTEP SOUNDS
        playerController.footStepTimer += 1 * Time.deltaTime;
        if (playerController.footStepTimer >= playerController.footStepSoundFrquency)
        {
            playerController.footStepTimer = 0;
            if (OnMetal(hit))
            {
                if (InitMetalStepSounds())
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.metalFootStep2;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep2)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.metalFootStep3;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep3)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.metalFootStep4;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.metalFootStep4)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.metalFootStep1;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                if (InitGroundStepSounds())
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.footStep2;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep2)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.footStep3;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep3)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.footStep4;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (playerController.playerBody.GetComponent<AudioSource>().clip == playerController.footStep4)
                {
                    playerController.playerBody.GetComponent<AudioSource>().clip = playerController.footStep1;
                    playerController.playerBody.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    //! Stops head bob and held item movement.
    public void StopGroundEffects()
    {
        playerController.mCam.GetComponent<HeadBob>().active = false;
        playerController.scanner.GetComponent<HeldItemSway>().active = false;
        playerController.laserCannon.GetComponent<HeldItemSway>().active = false;
        playerController.paintGun.GetComponent<HeldItemSway>().active = false;
    }

    //! Resets the held item's position.
    public void ResetHeldItemSway()
    {
        if (playerController.scannerActive == true)
        {
            playerController.scanner.GetComponent<HeldItemSway>().Reset();
        }
        if (playerController.laserCannonActive == true)
        {
            playerController.laserCannon.GetComponent<HeldItemSway>().Reset();
        }
        if (playerController.paintGunActive == true)
        {
            playerController.paintGun.GetComponent<HeldItemSway>().Reset();
        }
    }

    //! Fires the laser cannon.
    public void FireLaserCannon()
    {
        if (playerController.firing == false)
        {
            if (Physics.Raycast(playerController.mCam.gameObject.transform.position, playerController.mCam.gameObject.transform.forward, out RaycastHit hit, 1000))
            {
                if (hit.collider.gameObject.GetComponent<Deer>() == null)
                {
                    playerController.firing = true;
                    playerController.laserCannon.GetComponent<AudioSource>().Play();
                    playerController.muzzleFlash.SetActive(true);
                    playerController.laserController.HitTarget(hit.collider.gameObject, hit);
                    Object.Instantiate(playerController.weaponHit, hit.point, playerController.gameObject.transform.rotation);
                }
                else
                {
                    playerController.PlayMissingItemsSound();
                }
            }
            else
            {
                playerController.firing = true;
                playerController.laserCannon.GetComponent<AudioSource>().Play();
                playerController.muzzleFlash.SetActive(true);
            }
        }
    }

    //! Sends out a ping with the scanner.
    public void ScannerPing()
    {
        if (playerController.scanning == false)
        {
            playerController.scanning = true;
            playerController.scanner.GetComponent<AudioSource>().Play();
            playerController.scannerFlash.SetActive(true);
            UniversalResource[] allResources = Object.FindObjectsOfType<UniversalResource>();
            foreach (UniversalResource resource in allResources)
            {
                float distance = Vector3.Distance(playerController.gameObject.transform.position, resource.gameObject.transform.position);
                if (distance < 2000)
                {
                    float x = resource.gameObject.transform.position.x;
                    float y = resource.gameObject.transform.position.y + 15;
                    float z = resource.gameObject.transform.position.z;
                    Vector3 pos = new Vector3(x, y, z);
                    Quaternion rot = playerController.gameObject.transform.rotation;
                    GameObject newPing = Object.Instantiate(playerController.ping, pos, rot);
                    if (resource.type.Equals("Iron Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "iron";
                    }
                    else if (resource.type.Equals("Tin Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "tin";
                    }
                    else if (resource.type.Equals("Copper Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "copper";
                    }
                    else if (resource.type.Equals("Aluminum Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "aluminum";
                    }
                    else if (resource.type.Equals("Ice"))
                    {
                        newPing.GetComponent<Ping>().type = "ice";
                    }
                    else if (resource.type.Equals("Coal"))
                    {
                        newPing.GetComponent<Ping>().type = "coal";
                    }
                    else
                    {
                        newPing.GetComponent<Ping>().type = "unknown";
                    }
                }
            }
            DarkMatter[] allDarkMatter = Object.FindObjectsOfType<DarkMatter>();
            foreach (DarkMatter dm in allDarkMatter)
            {
                float distance = Vector3.Distance(playerController.gameObject.transform.position, dm.gameObject.transform.position);
                if (distance < 2000)
                {
                    float x = dm.gameObject.transform.position.x;
                    float y = dm.gameObject.transform.position.y + 15;
                    float z = dm.gameObject.transform.position.z;
                    Vector3 pos = new Vector3(x, y, z);
                    Quaternion rot = playerController.gameObject.transform.rotation;
                    GameObject newPing = Object.Instantiate(playerController.ping, pos, rot);
                    newPing.GetComponent<Ping>().type = "darkMatter";
                }
            }
        }
    }

    //! Applies jetpack thrust.
    public void JetPackThrust()
    {
        if (playerController.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            playerController.gameObject.GetComponent<AudioSource>().Play();
        }
        if (playerController.gameObject.transform.position.y < 500 && !Physics.Raycast(playerController.gameObject.transform.position, playerController.gameObject.transform.up, out RaycastHit upHit, 5))
        {
            playerController.moveUp = true;
        }
        playerController.mCam.GetComponent<HeadBob>().active = false;
        playerController.scanner.GetComponent<HeldItemSway>().active = false;
        playerController.laserCannon.GetComponent<HeldItemSway>().active = false;
        playerController.paintGun.GetComponent<HeldItemSway>().active = false;
    }

    //! Increases the number of blocks to be built along the build axis.
    public void IncreaseBuildAmount()
    {
        playerController.buildIncrementTimer += 1 * Time.deltaTime;
        if (playerController.buildIncrementTimer >= 0.1f)
        {
            playerController.buildMultiplier += 1;
            playerController.buildIncrementTimer = 0;
            playerController.PlayButtonSound();
        }
    }

    //! Reduces the number of blocks to be built along the build axis.
    public void DecreaseBuildAmount()
    {
        playerController.buildIncrementTimer += 1 * Time.deltaTime;
        if (playerController.buildIncrementTimer >= 0.1f)
        {
            playerController.buildMultiplier -= 1;
            playerController.buildIncrementTimer = 0;
            playerController.PlayButtonSound();
        }
    }

    //! Toggles building mode.
    public void ToggleBuilding()
    {
        if (playerController.building == true)
        {
            StopBuilding();
        }
        else
        {
            StartBuildMode();
        }
    }

    //! Starts build mode, for placing blocks.
    public void StartBuildMode()
    {
        if (!playerController.GuiOpen())
        {
            if (playerController.paintGunActive == true)
            {
                playerController.paintGun.SetActive(false);
                playerController.paintGunActive = false;
                playerController.paintColorSelected = false;
            }
            if (playerController.scannerActive == true)
            {
                playerController.scanner.SetActive(false);
                playerController.scannerActive = false;
            }
            if (playerController.laserCannonActive == true)
            {
                playerController.laserCannon.SetActive(false);
                playerController.laserCannonActive = false;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.building = true;
        }
    }

    //! Stops building mode.
    public void StopBuilding()
    {
        if (playerController.building == true)
        {
            if (playerController.gameManager.working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                meshManager.CombineBlocks();
                playerController.separatedBlocks = false;
                playerController.building = false;
                playerController.destroying = false;
            }
            else
            {
                playerController.requestedBuildingStop = true;
            }
        }
    }

    //! Removes duplicate blocks.
    public void Undo()
    {
        playerController.gameManager.UndoBuiltObjects();
    }

    //! Closes machine GUI.
    public void CloseMachineGUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerController.gameObject.GetComponent<MSCameraController>().enabled = false;
        playerController.machineGUIopen = false;
    }

    //! Closes tablet GUI.
    public void CloseTablet()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerController.gameObject.GetComponent<MSCameraController>().enabled = false;
        playerController.tabletOpen = false;
    }

    //! Closes all GUI windows.
    public void CloseMenus()
    {
        playerController.ApplySettings();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.gameObject.GetComponent<MSCameraController>().enabled = true;
        if (cGUI.showingInputGUI == true)
        {
            cGUI.ToggleGUI();
        }
        playerController.escapeMenuOpen = false;
        playerController.optionsGUIopen = false;
        playerController.helpMenuOpen = false;
        playerController.videoMenuOpen = false;
        playerController.schematicMenuOpen = false;
    }
}