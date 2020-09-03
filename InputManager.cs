using UnityEngine;

public class InputManager
{
    private PlayerController pc;

    public InputManager(PlayerController pc)
    {
        this.pc = pc;
    }

    public void HandleInput()
    {
        //CROSSHAIR
        if (cInput.GetKeyDown("Crosshair") && pc.exiting == false)
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

        //MOVEMENT INPUT
        if (pc.exiting == false)
        {
            if (cInput.GetKey("Walk Forward"))
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
            if (cInput.GetKey("Walk Backward"))
            {
                if (!Physics.Raycast(pc.mCam.gameObject.transform.position, -pc.mCam.gameObject.transform.forward, out RaycastHit hit, 5))
                {
                    Vector3 moveDir = Vector3.Normalize(new Vector3(pc.mCam.gameObject.transform.forward.x, 0, pc.mCam.gameObject.transform.forward.z));
                    pc.gameObject.transform.position -= moveDir * pc.playerMoveSpeed * Time.deltaTime;
                }
            }
            if (cInput.GetKey("Strafe Left"))
            {
                if (!Physics.Raycast(pc.mCam.gameObject.transform.position, -pc.mCam.gameObject.transform.right, out RaycastHit hit, 5))
                {
                    pc.gameObject.transform.position -= pc.mCam.gameObject.transform.right * pc.playerMoveSpeed * Time.deltaTime;
                }
            }
            if (cInput.GetKey("Strafe Right"))
            {
                if (!Physics.Raycast(pc.mCam.gameObject.transform.position, pc.mCam.gameObject.transform.right, out RaycastHit hit, 5))
                {
                    pc.gameObject.transform.position += pc.mCam.gameObject.transform.right * pc.playerMoveSpeed * Time.deltaTime;
                }
            }
        }

        if (!cInput.GetKey("Jetpack") && cInput.GetKey("Walk Forward") || cInput.GetKey("Walk Backward") || cInput.GetKey("Strafe Left") || cInput.GetKey("Strafe Right"))
        {
            if (pc.exiting == false && Physics.Raycast(pc.gameObject.transform.position, -pc.gameObject.transform.up, out RaycastHit hit, 10))
            {
                //HEAD BOB
                pc.mCam.GetComponent<HeadBob>().active = true;

                //HELD OBJECT MOVEMENT
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

                //FOOTSTEP SOUNDS
                pc.footStepTimer += 1 * Time.deltaTime;
                if (pc.footStepTimer >= pc.footStepSoundFrquency)
                {
                    pc.footStepTimer = 0;
                    if (hit.collider.gameObject.GetComponent<IronBlock>() != null || hit.collider.gameObject.GetComponent<Steel>() != null || hit.collider.gameObject.name.Equals("ironHolder(Clone)") || hit.collider.gameObject.name.Equals("steelHolder(Clone)"))
                    {
                        if (pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep1 || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep2 || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep3 || pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep4 || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep1)
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
                        if (pc.playerBody.GetComponent<AudioSource>().clip == pc.footStep1 || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep1 || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep2 || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep3 || pc.playerBody.GetComponent<AudioSource>().clip == pc.metalFootStep4)
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
            else if (pc.exiting == false && pc.gameObject.GetComponent<AudioSource>().isPlaying == false)
            {
                pc.gameObject.GetComponent<AudioSource>().Play();
                pc.mCam.GetComponent<HeadBob>().active = false;
                pc.scanner.GetComponent<HeldItemSway>().active = false;
                pc.laserCannon.GetComponent<HeldItemSway>().active = false;
                pc.paintGun.GetComponent<HeldItemSway>().active = false;
            }
        }
        else if (!cInput.GetKey("Jetpack") && !cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
        {
            pc.gameObject.GetComponent<AudioSource>().Stop();
            pc.mCam.GetComponent<HeadBob>().active = false;
            pc.scanner.GetComponent<HeldItemSway>().active = false;
            pc.laserCannon.GetComponent<HeldItemSway>().active = false;
            pc.paintGun.GetComponent<HeldItemSway>().active = false;
        }
        else
        {
            pc.mCam.GetComponent<HeadBob>().active = false;
            pc.scanner.GetComponent<HeldItemSway>().active = false;
            pc.laserCannon.GetComponent<HeldItemSway>().active = false;
            pc.paintGun.GetComponent<HeldItemSway>().active = false;
        }

        //WEAPON SELECTION
        if (pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false && pc.building == false)
        {
            if (cInput.GetKeyDown("Paint Gun"))
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
            }
            if (pc.paintGunActive == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    pc.paintGun.SetActive(false);
                    pc.paintGunActive = false;
                    pc.paintColorSelected = false;
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
            if (cInput.GetKeyDown("Laser Cannon"))
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
            if (pc.laserCannonActive == true && pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false)
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

            if (pc.scannerActive == true && pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false)
            {
                if (pc.scanning == false)
                {
                    pc.scanning = true;
                    pc.scanner.GetComponent<AudioSource>().Play();
                    pc.scannerFlash.SetActive(true);
                    GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
                    foreach (GameObject obj in allObjects)
                    {
                        float distance = Vector3.Distance(pc.gameObject.transform.position, obj.transform.position);
                        if (distance < 2000)
                        {
                            if (obj.GetComponent<UniversalResource>() != null)
                            {
                                GameObject newPing = Object.Instantiate(pc.ping, new Vector3(obj.transform.position.x, obj.transform.position.y + 15, obj.transform.position.z), pc.gameObject.transform.rotation);
                                if (obj.GetComponent<UniversalResource>().type.Equals("Iron Ore"))
                                {
                                    newPing.GetComponent<Ping>().type = "iron";
                                }
                                if (obj.GetComponent<UniversalResource>().type.Equals("Tin Ore"))
                                {
                                    newPing.GetComponent<Ping>().type = "tin";
                                }
                                if (obj.GetComponent<UniversalResource>().type.Equals("Copper Ore"))
                                {
                                    newPing.GetComponent<Ping>().type = "copper";
                                }
                                if (obj.GetComponent<UniversalResource>().type.Equals("Aluminum Ore"))
                                {
                                    newPing.GetComponent<Ping>().type = "aluminum";
                                }
                                if (obj.GetComponent<UniversalResource>().type.Equals("Ice"))
                                {
                                    newPing.GetComponent<Ping>().type = "ice";
                                }
                                if (obj.GetComponent<UniversalResource>().type.Equals("Coal"))
                                {
                                    newPing.GetComponent<Ping>().type = "coal";
                                }
                            }
                            else if (obj.GetComponent<DarkMatter>() != null)
                            {
                                GameObject newPing = Object.Instantiate(pc.ping, new Vector3(obj.transform.position.x, obj.transform.position.y + 15, obj.transform.position.z), pc.gameObject.transform.rotation);
                                newPing.GetComponent<Ping>().type = "darkMatter";
                            }
                        }
                    }
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

        //HEADLAMP
        if (cInput.GetKeyDown("Headlamp") && pc.exiting == false)
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

        //JETPACK
        if (cInput.GetKey("Jetpack") && pc.exiting == false)
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
        else if (!cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
        {
            if (pc.gameObject.GetComponent<AudioSource>().isPlaying == true)
            {
                pc.gameObject.GetComponent<AudioSource>().Stop();
            }
        }

        //BUILD MULTIPLIER
        if (pc.building == true)
        {
            if (pc.buildType.Equals("Glass Block") || pc.buildType.Equals("Brick") || pc.buildType.Equals("Iron Block") || pc.buildType.Equals("Steel Block") || pc.buildType.Equals("Steel Ramp") || pc.buildType.Equals("Iron Ramp"))
            {
                if (cInput.GetKey("Build Amount +"))
                {
                    if (pc.buildMultiplier < 100)
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
                }
                if (cInput.GetKey("Build Amount  -"))
                {
                    if (pc.buildMultiplier > 1)
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
                }
            }
        }

        //SELECTING CURRENT BLOCK TO BUILD WITH
        if (pc.escapeMenuOpen == false && pc.inventoryOpen == false && pc.optionsGUIopen == false && pc.machineGUIopen == false)
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
            if (pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false && pc.paintGunActive == false)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in pc.playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot > 0)
                        {
                            if (slot.typeInSlot.Equals(pc.buildType))
                            {
                                foundItems = true;
                            }
                        }
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

        //ACTIVATE INVENTORY GUI ON KEY PRESS
        if (cInput.GetKeyDown("Inventory"))
        {
            if (pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false)
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
                pc.craftingGUIopen = false;
                pc.storageGUIopen = false;
                pc.machineGUIopen = false;
            }
        }

        //ACTIVATE CRAFTING GUI ON KEY PRESS
        if (cInput.GetKeyDown("Crafting"))
        {
            if (pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false && pc.tabletOpen == false)
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
                pc.craftingGUIopen = false;
                pc.storageGUIopen = false;
                pc.machineGUIopen = false;
            }
        }

        //ACTIVATE TABLET GUI ON KEY PRESS
        if (cInput.GetKeyDown("Tablet"))
        {
            if (pc.tabletOpen == false && pc.inventoryOpen == false && pc.escapeMenuOpen == false && pc.machineGUIopen == false)
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

        //OPEN OPTIONS/EXIT MENU WHEN ESCAPE KEY IS PRESSED
        if (Input.GetKeyDown(KeyCode.Escape) && pc.exiting == false)
        {
            if (pc.inventoryOpen == true)
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
            }
            else if (pc.machineGUIopen == true)
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
            else if (pc.tabletOpen == true)
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
            else if (pc.paintGunActive == true && pc.paintColorSelected == false)
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
                pc.paintGun.SetActive(false);
                pc.paintGunActive = false;
                pc.paintColorSelected = false;
            }
            else if (pc.paintGunActive == true && pc.paintColorSelected == true)
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
    }
}

