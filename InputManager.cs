using UnityEngine;

public class InputManager
{
    private PlayerController pc;
    private ActionManager am;

    public InputManager(PlayerController pc)
    {
        this.pc = pc;
        am = new ActionManager(pc);
    }

    // Returns true if the player's current build type is a standard block.
    private bool PlacingStandardBlock()
    {
        return pc.buildType.Equals("Glass Block")
        || pc.buildType.Equals("Brick")
        || pc.buildType.Equals("Iron Block")
        || pc.buildType.Equals("Steel Block")
        || pc.buildType.Equals("Steel Ramp")
        || pc.buildType.Equals("Iron Ramp");
    }

    // Recieves all actions from cInput.
    public void HandleInput()
    {
        //CROSSHAIR
        if (cInput.GetKeyDown("Crosshair") && pc.exiting == false)
        {
            am.ToggleCrosshair();
        }

        //SPRINTING
        if (cInput.GetKey("Sprint") && pc.exiting == false)
        {
            pc.playerMoveSpeed = 25;
            pc.footStepSoundFrquency = 0.25f;
        }
        else
        {
            if (!Physics.Raycast(pc.gameObject.transform.position, -pc.gameObject.transform.up, out RaycastHit hit, 10))
            {
                pc.playerMoveSpeed = 25;
                pc.footStepSoundFrquency = 0.25f;
            }
            else
            {
                pc.playerMoveSpeed = 15;
                pc.footStepSoundFrquency = 0.5f;
            }
        }

        if (cInput.GetKeyDown("Sprint") || cInput.GetKeyUp("Sprint"))
        {
            am.ResetHeldItemSway();
        }

        //MOVEMENT INPUT
        if (pc.exiting == false)
        {
            if (cInput.GetKey("Walk Forward"))
            {
                am.WalkForward();
            }
            if (cInput.GetKey("Walk Backward"))
            {
                am.WalkBackward();
            }
            if (cInput.GetKey("Strafe Left"))
            {
                am.StrafeLeft();
            }
            if (cInput.GetKey("Strafe Right"))
            {
                am.StrafeRight();
            }
        }

        if (!cInput.GetKey("Jetpack") && (cInput.GetKey("Walk Forward") || cInput.GetKey("Walk Backward") || cInput.GetKey("Strafe Left") || cInput.GetKey("Strafe Right")))
        {
            if (pc.exiting == false && Physics.Raycast(pc.gameObject.transform.position, -pc.gameObject.transform.up, out RaycastHit hit, 10))
            {
                am.DoGroundEffects(hit);
            }
            else if (pc.exiting == false && pc.gameObject.GetComponent<AudioSource>().isPlaying == false)
            {
                pc.gameObject.GetComponent<AudioSource>().Play();
                am.StopGroundEffects();
            }
        }
        else if (!cInput.GetKey("Jetpack") && !cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
        {
            pc.gameObject.GetComponent<AudioSource>().Stop();
            am.StopGroundEffects();
        }
        else
        {
            am.StopGroundEffects();
        }

        //WEAPON SELECTION
        if (!pc.GuiOpen())
        {
            if (cInput.GetKeyDown("Paint Gun"))
            {
                am.TogglePaintGun();
            }
            if (pc.paintGunActive == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    am.TogglePaintGun();
                }
            }
            if (cInput.GetKeyDown("Paint Color"))
            {
                if (pc.paintGunActive == true && pc.paintColorSelected == true)
                {
                    pc.paintColorSelected = false;
                }
            }
            if (cInput.GetKeyDown("Scanner"))
            {
                am.ToggleScanner();
            }
            if (cInput.GetKeyDown("Laser Cannon"))
            {
                am.ToggleLaserCannon();
            }
        }
        else
        {
            pc.laserCannon.SetActive(false);
            pc.laserCannonActive = false;
            pc.scanner.SetActive(false);
            pc.scannerActive = false;
        }

        //FIRING THE LASER CANNON OR THE SCANNER
        if (cInput.GetKeyDown("Fire") && pc.exiting == false)
        {
            if (!pc.GuiOpen())
            {
                if (pc.laserCannonActive)
                {
                    am.FireLaserCannon();
                }
                if (pc.scannerActive)
                {
                    am.ScannerPing();
                }
            }
        }

        if (pc.firing == true)
        {
            if (!pc.laserCannon.GetComponent<AudioSource>().isPlaying)
            {
                pc.muzzleFlash.SetActive(false);
                pc.firing = false;
            }
        }

        if (pc.scanning == true)
        {
            if (!pc.scanner.GetComponent<AudioSource>().isPlaying)
            {
                pc.scannerFlash.SetActive(false);
                pc.scanning = false;
            }
        }

        if (cInput.GetKeyDown("Headlamp") && pc.exiting == false)
        {
            am.ToggleHeadLamp();
        }

        //JETPACK
        if (cInput.GetKey("Jetpack") && pc.exiting == false)
        {
            am.JetPackThrust();
        }
        else if (!cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
        {
            if (pc.gameObject.GetComponent<AudioSource>().isPlaying == true)
            {
                pc.gameObject.GetComponent<AudioSource>().Stop();
            }
        }

        //BUILD MULTIPLIER
        if (pc.building == true && PlacingStandardBlock())
        {
            if (cInput.GetKey("Build Amount +") && pc.buildMultiplier < 100)
            {
                am.IncreaseBuildAmount();
            }
            if (cInput.GetKey("Build Amount  -") && pc.buildMultiplier > 1)
            {
                am.DecreaseBuildAmount();
            }
        }

        //SELECTING CURRENT BLOCK TO BUILD WITH
        if (!pc.GuiOpen())
        {
            if (cInput.GetKeyDown("Next Item"))
            {
                pc.blockSelector.nextBlock();
            }

            if (cInput.GetKeyDown("Previous Item"))
            {
                pc.blockSelector.previousBlock();
            }
        }

        if (pc.displayingBuildItem == true)
        {
            pc.buildItemDisplayTimer += 1 * Time.deltaTime;
            if (pc.buildItemDisplayTimer > 3)
            {
                pc.displayingBuildItem = false;
                pc.buildItemDisplayTimer = 0;
            }
        }

        //IF THE PLAYER HAS SELECTED AN ITEM AND HAS THAT ITEM IN THE INVENTORY, BEGIN BUILDING ON KEY PRESS
        if (cInput.GetKeyDown("Build"))
        {
            am.StartBuildMode();
        }

        //CANCEL BUILDING ON KEY PRESS
        if (cInput.GetKeyDown("Stop Building"))
        {
            am.StopBuilding();
        }

        //ACTIVATE INVENTORY GUI ON KEY PRESS
        if (cInput.GetKeyDown("Inventory"))
        {
            am.ToggleInventory();
        }

        //ACTIVATE CRAFTING GUI ON KEY PRESS
        if (cInput.GetKeyDown("Crafting"))
        {
            am.ToggleCraftingGUI();
        }

        //ACTIVATE MARKET GUI ON KEY PRESS
        if (cInput.GetKeyDown("Market"))
        {
            am.ToggleMarketGUI();
        }

        //ACTIVATE TABLET GUI ON KEY PRESS
        if (cInput.GetKeyDown("Tablet"))
        {
            am.ToggleTablet();
        }

        //OPEN OPTIONS/EXIT MENU WHEN ESCAPE KEY IS PRESSED
        if (Input.GetKeyDown(KeyCode.Escape) && pc.exiting == false)
        {
            if (pc.inventoryOpen == true)
            {
                am.CloseInventory();
            }
            else if (pc.machineGUIopen == true)
            {
                am.CloseMachineGUI();
            }
            else if (pc.marketGUIopen == true)
            {
                am.ToggleMarketGUI();
            }
            else if (pc.tabletOpen == true)
            {
                am.CloseTablet();
            }
            else if (pc.paintGunActive == true)
            {
                pc.paintGun.SetActive(false);
                pc.paintGunActive = false;
                pc.paintColorSelected = false;
            }
            else if (pc.escapeMenuOpen == false)
            {
                pc.requestedEscapeMenu = true;
            }
            else
            {
                am.CloseMenus();
            }
        }
    }
}

