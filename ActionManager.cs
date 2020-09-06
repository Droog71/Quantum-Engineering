using UnityEngine;

public class ActionManager
{
    private PlayerController pc;

    public ActionManager(PlayerController pc)
    {
        this.pc = pc;
    }

    // Toggles the crosshair.
    public void ToggleCrosshair()
    {
        if (pc.crosshairEnabled == true)
        {
            pc.crosshairEnabled = false;
        }
        else
        {
            pc.crosshairEnabled = true;
        }
        pc.PlayButtonSound();
    }

    // Toggles the head lamp.
    public void ToggleHeadLamp()
    {
        if (pc.headlamp.GetComponent<Light>() != null)
        {
            if (pc.headlamp.GetComponent<Light>().enabled == true)
            {
                pc.headlamp.GetComponent<Light>().enabled = false;
            }
            else
            {
                pc.headlamp.GetComponent<Light>().enabled = true;
            }
        }
        pc.PlayButtonSound();
    }

    // Toggles the laser cannon.
    public void ToggleLaserCannon()
    {
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        if (!pc.laserCannon.activeSelf)
        {
            pc.paintGun.SetActive(false);
            pc.paintGunActive = false;
            pc.paintColorSelected = false;
            pc.scanner.SetActive(false);
            pc.scannerActive = false;
            pc.laserCannon.SetActive(true);
            pc.laserCannonActive = true;
        }
        else
        {
            pc.laserCannon.SetActive(false);
            pc.laserCannonActive = false;
        }
    }

    // Toggles the scanner.
    public void ToggleScanner()
    {
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        if (!pc.scanner.activeSelf)
        {
            pc.paintGun.SetActive(false);
            pc.paintGunActive = false;
            pc.paintColorSelected = false;
            pc.laserCannon.SetActive(false);
            pc.laserCannonActive = false;
            pc.scanner.SetActive(true);
            pc.scannerActive = true;
        }
        else
        {
            pc.scanner.SetActive(false);
            pc.scannerActive = false;
        }
    }

    // Toggles the paint gun.
    public void TogglePaintGun()
    {
        if (pc.paintGunActive == false)
        {
            pc.paintGunActive = true;
            pc.paintGun.SetActive(true);
            pc.laserCannon.SetActive(false);
            pc.laserCannonActive = false;
            pc.scanner.SetActive(false);
            pc.scannerActive = false;
        }
        else
        {
            pc.paintGun.SetActive(false);
            pc.paintGunActive = false;
            pc.paintColorSelected = false;
        }
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
    }

    // Toggles the inventory GUI.
    public void ToggleInventory()
    {
        if (!pc.GuiOpen())
        {
            if (pc.building == true || pc.destroying == true)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    pc.stoppingBuildCoRoutine = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                    pc.separatedBlocks = false;
                    pc.destroyTimer = 0;
                    pc.buildTimer = 0;
                    pc.building = false;
                    pc.destroying = false;
                }
                else
                {
                    pc.requestedBuildingStop = true;
                }
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pc.machineGUIopen = false;
            pc.inventoryOpen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pc.inventoryOpen = false;
            pc.marketGUIopen = false;
            pc.craftingGUIopen = false;
            pc.storageGUIopen = false;
            pc.machineGUIopen = false;
        }
    }

    // Toggles the crafting GUI.
    public void ToggleCraftingGUI()
    {
        if (!pc.GuiOpen())
        {
            if (pc.building == true || pc.destroying == true)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    pc.stoppingBuildCoRoutine = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                    pc.separatedBlocks = false;
                    pc.destroyTimer = 0;
                    pc.buildTimer = 0;
                    pc.building = false;
                    pc.destroying = false;
                }
                else
                {
                    pc.requestedBuildingStop = true;
                }
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pc.machineGUIopen = false;
            pc.inventoryOpen = true;
            pc.craftingGUIopen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pc.inventoryOpen = false;
            pc.marketGUIopen = false;
            pc.craftingGUIopen = false;
            pc.storageGUIopen = false;
            pc.machineGUIopen = false;
        }
    }

    // Toggles the crafting GUI.
    public void ToggleMarketGUI()
    {
        if (!pc.GuiOpen())
        {
            if (pc.building == true || pc.destroying == true)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    pc.stoppingBuildCoRoutine = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                    pc.separatedBlocks = false;
                    pc.destroyTimer = 0;
                    pc.buildTimer = 0;
                    pc.building = false;
                    pc.destroying = false;
                }
                else
                {
                    pc.requestedBuildingStop = true;
                }
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pc.machineGUIopen = false;
            pc.craftingGUIopen = false;
            pc.marketGUIopen = true;
            float distance = Vector3.Distance(pc.gameObject.transform.position, GameObject.Find("Rocket").transform.position);
            pc.inventoryOpen = distance <= 40;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pc.inventoryOpen = false;
            pc.marketGUIopen = false;
            pc.craftingGUIopen = false;
            pc.storageGUIopen = false;
            pc.machineGUIopen = false;
        }
    }

    // Toggles the tablet GUI.
    public void ToggleTablet()
    {
        if (!pc.GuiOpen())
        {
            if (pc.building == true || pc.destroying == true)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    pc.stoppingBuildCoRoutine = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                    pc.separatedBlocks = false;
                    pc.destroyTimer = 0;
                    pc.buildTimer = 0;
                    pc.building = false;
                    pc.destroying = false;
                }
                else
                {
                    pc.requestedBuildingStop = true;
                }
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pc.tabletOpen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pc.tabletOpen = false;
        }
    }

    // Moves the player forward.
    public void WalkForward()
    {
        if (!Physics.Raycast(pc.mCam.gameObject.transform.position, pc.mCam.gameObject.transform.forward, out RaycastHit hit, 5))
        {
            Vector3 moveDir = Vector3.Normalize(new Vector3(pc.mCam.gameObject.transform.forward.x, 0, pc.mCam.gameObject.transform.forward.z));
            pc.gameObject.transform.position += moveDir * pc.playerMoveSpeed * Time.deltaTime;
        }
        else if (hit.collider.gameObject.GetComponent<AirLock>() != null)
        {
            if (hit.collider.gameObject.GetComponent<Collider>().isTrigger == true)
            {
                Vector3 moveDir = Vector3.Normalize(new Vector3(pc.mCam.gameObject.transform.forward.x, 0, pc.mCam.gameObject.transform.forward.z));
                pc.gameObject.transform.position += moveDir * pc.playerMoveSpeed * Time.deltaTime;
            }
        }
    }

    // Moves the player backward.
    public void WalkBackward()
    {
        if (!Physics.Raycast(pc.mCam.gameObject.transform.position, -pc.mCam.gameObject.transform.forward, out RaycastHit hit, 5))
        {
            Vector3 moveDir = Vector3.Normalize(new Vector3(pc.mCam.gameObject.transform.forward.x, 0, pc.mCam.gameObject.transform.forward.z));
            pc.gameObject.transform.position -= moveDir * pc.playerMoveSpeed * Time.deltaTime;
        }
    }

    // Moves the player left.
    public void StrafeLeft()
    {
        if (!Physics.Raycast(pc.mCam.gameObject.transform.position, -pc.mCam.gameObject.transform.right, out RaycastHit hit, 5))
        {
            pc.gameObject.transform.position -= pc.mCam.gameObject.transform.right * pc.playerMoveSpeed * Time.deltaTime;
        }
    }

    // Moves the player right.
    public void StrafeRight()
    {
        if (!Physics.Raycast(pc.mCam.gameObject.transform.position, pc.mCam.gameObject.transform.right, out RaycastHit hit, 5))
        {
            pc.gameObject.transform.position += pc.mCam.gameObject.transform.right * pc.playerMoveSpeed * Time.deltaTime;
        }
    }

    // Returns true when the player is walking on metal.
    private bool OnMetal(RaycastHit hit)
    {
        return hit.collider.gameObject.GetComponent<IronBlock>() != null
        || hit.collider.gameObject.GetComponent<Steel>() != null
        || hit.collider.gameObject.name.Equals("ironHolder(Clone)")
        || hit.collider.gameObject.name.Equals("steelHolder(Clone)");
    }

    // Returns true when varied footstep sounds should loop back to the first sound.
    private bool InitMetalStepSounds()
    {
        return pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep1
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep2
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep3
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep4
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep1;
    }

    // Returns true when varied footstep sounds should loop back to the first sound.
    private bool InitGroundStepSounds()
    {
        return pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep1
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep1
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep2
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep3
        || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep4;
    }

    // Handles head bob, held item movement and footstep sound effects.
    public void DoGroundEffects(RaycastHit hit)
    {
        // HEAD BOB
        pc.mCam.GetComponent<HeadBob>().active = true;

        // HELD OBJECT MOVEMENT
        if (pc.gameObject.GetComponent<AudioSource>().isPlaying == true)
        {
            pc.gameObject.GetComponent<AudioSource>().Stop();
        }
        if (pc.scannerActive == true)
        {
            pc.scanner.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            pc.scanner.GetComponent<HeldItemSway>().active = false;
        }
        if (pc.laserCannonActive == true)
        {
            pc.laserCannon.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            pc.laserCannon.GetComponent<HeldItemSway>().active = false;
        }
        if (pc.paintGunActive == true)
        {
            pc.paintGun.GetComponent<HeldItemSway>().active = true;
        }
        else
        {
            pc.paintGun.GetComponent<HeldItemSway>().active = false;
        }

        // FOOTSTEP SOUNDS
        pc.footStepTimer += 1 * Time.deltaTime;
        if (pc.footStepTimer >= pc.footStepSoundFrquency)
        {
            pc.footStepTimer = 0;
            if (OnMetal(hit))
            {
                if (InitMetalStepSounds())
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.metalFootStep2;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep2)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.metalFootStep3;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep3)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.metalFootStep4;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep4)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.metalFootStep1;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                if (InitGroundStepSounds())
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.footStep2;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep2)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.footStep3;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep3)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.footStep4;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
                else if (pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep4)
                {
                    pc.playerBody.GetComponent<AudioSource>().clip = pc.footStep1;
                    pc.playerBody.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    // Stops head bob and held item movement.
    public void StopGroundEffects()
    {
        pc.mCam.GetComponent<HeadBob>().active = false;
        pc.scanner.GetComponent<HeldItemSway>().active = false;
        pc.laserCannon.GetComponent<HeldItemSway>().active = false;
        pc.paintGun.GetComponent<HeldItemSway>().active = false;
    }

    // Resets the held item's position.
    public void ResetHeldItemSway()
    {
        if (pc.scannerActive == true)
        {
            pc.scanner.GetComponent<HeldItemSway>().Reset();
        }
        if (pc.laserCannonActive == true)
        {
            pc.laserCannon.GetComponent<HeldItemSway>().Reset();
        }
        if (pc.paintGunActive == true)
        {
            pc.paintGun.GetComponent<HeldItemSway>().Reset();
        }
    }

    // Fires the laser cannon.
    public void FireLaserCannon()
    {
        if (pc.firing == false)
        {
            pc.firing = true;
            pc.laserCannon.GetComponent<AudioSource>().Play();
            pc.muzzleFlash.SetActive(true);
            if (Physics.Raycast(pc.mCam.gameObject.transform.position, pc.mCam.gameObject.transform.forward, out RaycastHit hit, 1000))
            {
                Object.Instantiate(pc.weaponHit, hit.point, pc.gameObject.transform.rotation);
                pc.laserController.HitTarget(hit.collider.gameObject, hit);
            }
        }
    }

    // Sends out a ping with the scanner.
    public void ScannerPing()
    {
        if (pc.scanning == false)
        {
            pc.scanning = true;
            pc.scanner.GetComponent<AudioSource>().Play();
            pc.scannerFlash.SetActive(true);
            UniversalResource[] allResources = Object.FindObjectsOfType<UniversalResource>();
            foreach (UniversalResource resource in allResources)
            {
                float distance = Vector3.Distance(pc.gameObject.transform.position, resource.gameObject.transform.position);
                if (distance < 2000)
                {
                    float x = resource.gameObject.transform.position.x;
                    float y = resource.gameObject.transform.position.y + 15;
                    float z = resource.gameObject.transform.position.z;
                    Vector3 pos = new Vector3(x, y, z);
                    Quaternion rot = pc.gameObject.transform.rotation;
                    GameObject newPing = Object.Instantiate(pc.ping, pos, rot);
                    if (resource.type.Equals("Iron Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "iron";
                    }
                    if (resource.type.Equals("Tin Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "tin";
                    }
                    if (resource.type.Equals("Copper Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "copper";
                    }
                    if (resource.type.Equals("Aluminum Ore"))
                    {
                        newPing.GetComponent<Ping>().type = "aluminum";
                    }
                    if (resource.type.Equals("Ice"))
                    {
                        newPing.GetComponent<Ping>().type = "ice";
                    }
                    if (resource.type.Equals("Coal"))
                    {
                        newPing.GetComponent<Ping>().type = "coal";
                    }
                }
            }
            DarkMatter[] allDarkMatter = Object.FindObjectsOfType<DarkMatter>();
            foreach (DarkMatter dm in allDarkMatter)
            {
                float distance = Vector3.Distance(pc.gameObject.transform.position, dm.gameObject.transform.position);
                if (distance < 2000)
                {
                    float x = dm.gameObject.transform.position.x;
                    float y = dm.gameObject.transform.position.y + 15;
                    float z = dm.gameObject.transform.position.z;
                    Vector3 pos = new Vector3(x, y, z);
                    Quaternion rot = pc.gameObject.transform.rotation;
                    GameObject newPing = Object.Instantiate(pc.ping, pos, rot);
                    newPing.GetComponent<Ping>().type = "darkMatter";
                }
            }
        }
    }

    // Applies jetpack thrust.
    public void JetPackThrust()
    {
        if (pc.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            pc.gameObject.GetComponent<AudioSource>().Play();
        }
        if (pc.gameObject.transform.position.y < 500 && !Physics.Raycast(pc.gameObject.transform.position, pc.gameObject.transform.up, out RaycastHit upHit, 5))
        {
            pc.gameObject.transform.position += Vector3.up * 25 * Time.deltaTime;
        }
        pc.mCam.GetComponent<HeadBob>().active = false;
        pc.scanner.GetComponent<HeldItemSway>().active = false;
        pc.laserCannon.GetComponent<HeldItemSway>().active = false;
        pc.paintGun.GetComponent<HeldItemSway>().active = false;
    }

    // Increases the number of blocks to be built along the build axis.
    public void IncreaseBuildAmount()
    {
        pc.buildIncrementTimer += 1 * Time.deltaTime;
        if (pc.buildIncrementTimer >= 0.1f)
        {
            pc.buildMultiplier += 1;
            pc.destroyTimer = 0;
            pc.buildTimer = 0;
            pc.buildIncrementTimer = 0;
            pc.PlayButtonSound();
        }
    }

    // Reduces the number of blocks to be built along the build axis.
    public void DecreaseBuildAmount()
    {
        pc.buildIncrementTimer += 1 * Time.deltaTime;
        if (pc.buildIncrementTimer >= 0.1f)
        {
            pc.buildMultiplier -= 1;
            pc.destroyTimer = 0;
            pc.buildTimer = 0;
            pc.buildIncrementTimer = 0;
            pc.PlayButtonSound();
        }
    }

    // Starts build mode, for placing blocks.
    public void StartBuildMode()
    {
        if (!pc.GuiOpen())
        {
            bool foundItems = false;
            foreach (InventorySlot slot in pc.playerInventory.inventory)
            {
                if (foundItems == false && slot.amountInSlot > 0)
                {
                    foundItems |= slot.typeInSlot.Equals(pc.buildType);
                }
            }
            if (foundItems == true)
            {
                pc.building = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pc.inventoryOpen = false;
                pc.craftingGUIopen = false;
                pc.storageGUIopen = false;
                if (pc.scannerActive == true)
                {
                    pc.scanner.SetActive(false);
                    pc.scannerActive = false;
                }
                if (pc.laserCannonActive == true)
                {
                    pc.laserCannon.SetActive(false);
                    pc.laserCannonActive = false;
                }
            }
        }
    }

    // Stops building mode.
    public void StopBuilding()
    {
        if (pc.building == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        if (pc.paintGunActive == true)
        {
            TogglePaintGun();
        }
    }

    // Closes inventory GUI.
    public void CloseInventory()
    {
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pc.gameObject.GetComponent<MSCameraController>().enabled = false;
        pc.inventoryOpen = false;
        pc.craftingGUIopen = false;
        pc.storageGUIopen = false;
        pc.marketGUIopen = false;
    }

    // Closes machine GUI.
    public void CloseMachineGUI()
    {
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pc.gameObject.GetComponent<MSCameraController>().enabled = false;
        pc.machineGUIopen = false;
    }

    // Closes tablet GUI.
    public void CloseTablet()
    {
        if (pc.building == true || pc.destroying == true)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                pc.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
                pc.separatedBlocks = false;
                pc.destroyTimer = 0;
                pc.buildTimer = 0;
                pc.building = false;
                pc.destroying = false;
            }
            else
            {
                pc.requestedBuildingStop = true;
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pc.gameObject.GetComponent<MSCameraController>().enabled = false;
        pc.tabletOpen = false;
    }

    // Closes all GUI windows.
    public void CloseMenus()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pc.gameObject.GetComponent<MSCameraController>().enabled = true;
        if (cGUI.showingInputGUI == true)
        {
            cGUI.ToggleGUI();
        }
        pc.escapeMenuOpen = false;
        pc.optionsGUIopen = false;
        pc.helpMenuOpen = false;
        pc.videoMenuOpen = false;
        pc.schematicMenuOpen = false;
    }
}

