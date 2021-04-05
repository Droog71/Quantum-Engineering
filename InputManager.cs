using UnityEngine;

public class InputManager
{
    private PlayerController playerController;
    private BuildController buildController;
    private ActionManager actionManager;

    //! This class handles all input from the player.
    public InputManager(PlayerController playerController)
    {
        this.playerController = playerController;
        actionManager = new ActionManager(playerController);
        buildController = playerController.GetComponent<BuildController>();
    }

    //! Returns true if the player's current build type is a standard block.
    private bool PlacingStandardBlock()
    {
        return playerController.buildType.Equals("Glass Block")
        || playerController.buildType.Equals("Brick")
        || playerController.buildType.Equals("Iron Block")
        || playerController.buildType.Equals("Steel Block")
        || playerController.buildType.Equals("Steel Ramp")
        || playerController.buildType.Equals("Iron Ramp");
    }

    //! Recieves all actions from cInput.
    public void HandleInput()
    {
        // CROSSHAIR
        if (cInput.GetKeyDown("Crosshair") && playerController.exiting == false)
        {
            actionManager.ToggleCrosshair();
        }

        // SPRINTING
        if (cInput.GetKey("Sprint/Boost") && playerController.exiting == false)
        {
            playerController.playerMoveSpeed = 30;
            playerController.footStepSoundFrquency = 0.25f;
        }
        else
        {
            if (!Physics.Raycast(playerController.gameObject.transform.position, -playerController.gameObject.transform.up, out RaycastHit hit, 10))
            {
                playerController.playerMoveSpeed = 30;
                playerController.footStepSoundFrquency = 0.25f;
            }
            else
            {
                playerController.playerMoveSpeed = 15;
                playerController.footStepSoundFrquency = 0.5f;
            }
        }

        if (cInput.GetKeyDown("Sprint/Boost") || cInput.GetKeyUp("Sprint/Boost"))
        {
            actionManager.ResetHeldItemSway();
        }

        // MOVEMENT INPUT
        if (playerController.exiting == false)
        {
            playerController.moveForward = cInput.GetKey("Walk Forward");
            playerController.moveBackward = cInput.GetKey("Walk Backward");
            playerController.moveLeft = cInput.GetKey("Strafe Left");
            playerController.moveRight = cInput.GetKey("Strafe Right");
        }

        if (!cInput.GetKey("Jetpack") && (cInput.GetKey("Walk Forward") || cInput.GetKey("Walk Backward") || cInput.GetKey("Strafe Left") || cInput.GetKey("Strafe Right")))
        {
            if (playerController.exiting == false && Physics.Raycast(playerController.gameObject.transform.position, -playerController.gameObject.transform.up, out RaycastHit hit, 10))
            {
                actionManager.DoGroundEffects(hit);
            }
            else if (playerController.exiting == false && playerController.gameObject.GetComponent<AudioSource>().isPlaying == false)
            {
                playerController.gameObject.GetComponent<AudioSource>().Play();
                actionManager.StopGroundEffects();
            }
        }
        else if (!cInput.GetKey("Jetpack") && !cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
        {
            if (Physics.Raycast(playerController.gameObject.transform.position, -playerController.gameObject.transform.up, out RaycastHit hit, 10))
            {
                playerController.stopMovement = true;
            }
            playerController.gameObject.GetComponent<AudioSource>().Stop();
            actionManager.StopGroundEffects();
        }
        else
        {
            actionManager.StopGroundEffects();
        }

        // WEAPON SELECTION
        if (!playerController.GuiOpen())
        {
            if (cInput.GetKeyDown("Paint Gun"))
            {
                actionManager.TogglePaintGun();
            }
            if (playerController.paintGunActive == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    actionManager.TogglePaintGun();
                }
            }
            if (cInput.GetKeyDown("Paint Color"))
            {
                if (playerController.paintGunActive == true && playerController.paintColorSelected == true)
                {
                    playerController.paintColorSelected = false;
                }
            }
            if (cInput.GetKeyDown("Scanner"))
            {
                actionManager.ToggleScanner();
            }
            if (cInput.GetKeyDown("Laser Cannon"))
            {
                actionManager.ToggleLaserCannon();
            }
        }
        else
        {
            playerController.laserCannon.SetActive(false);
            playerController.laserCannonActive = false;
            playerController.scanner.SetActive(false);
            playerController.scannerActive = false;
        }

        // FIRING THE LASER CANNON OR THE SCANNER
        if (cInput.GetKeyDown("Fire") && playerController.exiting == false)
        {
            if (!playerController.GuiOpen())
            {
                if (playerController.laserCannonActive)
                {
                    actionManager.FireLaserCannon();
                }
                if (playerController.scannerActive)
                {
                    actionManager.ScannerPing();
                }
            }
        }

        if (playerController.firing == true)
        {
            if (!playerController.laserCannon.GetComponent<AudioSource>().isPlaying)
            {
                playerController.muzzleFlash.SetActive(false);
                playerController.firing = false;
            }
        }

        if (playerController.scanning == true)
        {
            if (!playerController.scanner.GetComponent<AudioSource>().isPlaying)
            {
                playerController.scannerFlash.SetActive(false);
                playerController.scanning = false;
            }
        }

        if (cInput.GetKeyDown("Headlamp") && playerController.exiting == false)
        {
            actionManager.ToggleHeadLamp();
        }

        // JETPACK
        if (cInput.GetKey("Jetpack") && playerController.exiting == false)
        {
            actionManager.JetPackThrust();
        }
        else 
        {
            if (!cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
            {
                if (playerController.gameObject.GetComponent<AudioSource>().isPlaying == true)
                {
                    playerController.gameObject.GetComponent<AudioSource>().Stop();
                }
            }
            playerController.moveUp = false;
        }

        // BUILD MULTIPLIER
        if (playerController.building == true && PlacingStandardBlock())
        {
            if (cInput.GetKey("Build Amount +") && playerController.buildMultiplier < 100)
            {
                actionManager.IncreaseBuildAmount();
            }
            if (cInput.GetKey("Build Amount  -") && playerController.buildMultiplier > 1)
            {
                actionManager.DecreaseBuildAmount();
            }
        }

        // SELECTING CURRENT BLOCK TO BUILD WITH
        if (!playerController.GuiOpen())
        {
            if (cInput.GetKeyDown("Next Item"))
            {
                playerController.blockSelector.NextBlock();
            }

            if (cInput.GetKeyDown("Previous Item"))
            {
                playerController.blockSelector.PreviousBlock();
            }
        }

        if (playerController.displayingBuildItem == true)
        {
            playerController.buildItemDisplayTimer += 1 * Time.deltaTime;
            if (playerController.buildItemDisplayTimer > 3)
            {
                playerController.displayingBuildItem = false;
                playerController.buildItemDisplayTimer = 0;
            }
        }

        // BLOCK ROTATION
        if (cInput.GetKeyDown("Rotate Block"))
        {
            if (playerController.building == true)
            {
                playerController.buildObject.transform.Rotate(playerController.buildObject.transform.up * 90);
                playerController.destroyTimer = 0;
                playerController.buildTimer = 0;
                playerController.PlayButtonSound();
            }
        }

        // BUILD AXIS
        if (cInput.GetKeyDown("Build Axis"))
        {
            playerController.PlayButtonSound();
            if (buildController.autoAxis == false)
                buildController.ChangeBuildAxis();
        }
        if (cInput.GetKeyDown("Auto Axis"))
        {
            playerController.PlayButtonSound();
            buildController.autoAxis = !buildController.autoAxis;
            playerController.autoAxisMessage = true;
        }

        // IF THE PLAYER HAS SELECTED AN ITEM AND HAS THAT ITEM IN THE INVENTORY, BEGIN BUILDING ON KEY PRESS
        if (cInput.GetKeyDown("Build"))
        {
            actionManager.StartBuildMode();
        }

        // CANCEL BUILDING ON KEY PRESS
        if (cInput.GetKeyDown("Stop Building"))
        {
            actionManager.StopBuilding();
        }

        // ACTIVATE INVENTORY GUI ON KEY PRESS
        if (cInput.GetKeyDown("Inventory"))
        {
            actionManager.ToggleInventory();
        }

        // ACTIVATE CRAFTING GUI ON KEY PRESS
        if (cInput.GetKeyDown("Crafting"))
        {
            actionManager.ToggleCraftingGUI();
        }

        // ACTIVATE MARKET GUI ON KEY PRESS
        if (cInput.GetKeyDown("Market"))
        {
            actionManager.ToggleMarketGUI();
        }

        // ACTIVATE TABLET GUI ON KEY PRESS
        if (cInput.GetKeyDown("Tablet"))
        {
            actionManager.ToggleTablet();
        }

        // OPEN OPTIONS/EXIT MENU WHEN ESCAPE KEY IS PRESSED
        if (Input.GetKeyDown(KeyCode.Escape) && playerController.exiting == false)
        {
            if (playerController.inventoryOpen == true)
            {
                actionManager.ToggleInventory();
            }
            else if (playerController.machineGUIopen == true)
            {
                actionManager.CloseMachineGUI();
            }
            else if (playerController.marketGUIopen == true)
            {
                actionManager.ToggleMarketGUI();
            }
            else if (playerController.tabletOpen == true)
            {
                actionManager.CloseTablet();
            }
            else if (playerController.paintGunActive == true)
            {
                playerController.paintGun.SetActive(false);
                playerController.paintGunActive = false;
                playerController.paintColorSelected = false;
            }
            else if (playerController.escapeMenuOpen == false)
            {
                playerController.requestedEscapeMenu = true;
            }
            else
            {
                actionManager.CloseMenus();
            }
        }
    }
}