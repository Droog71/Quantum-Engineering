using UnityEngine;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private TextureDictionary td;
    private GuiCoordinates guiCoordinates;
    public GUISkin thisGUIskin;
    public GameObject videoPlayer;
    private bool schematic1;
    private bool schematic2;
    private bool schematic3;
    private bool schematic4;
    private bool schematic5;
    private bool schematic6;
    private bool schematic7;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        guiCoordinates = GetComponent<GuiCoordinates>();
        td = GetComponent<TextureDictionary>();
    }

    // Returns true if the escape menu should be displayed.
    private bool MenuAvailable()
    {
        return playerController.helpMenuOpen == false
        && playerController.optionsGUIopen == false
        && cGUI.showingInputGUI == false
        && playerController.exiting == false
        && playerController.requestedSave == false
        && GameObject.Find("GameManager").GetComponent<GameManager>().dataSaveRequested == false
        && GameObject.Find("GameManager").GetComponent<StateManager>().saving == false;

    }

    private bool SavingMessageRequired()
    {
        return playerController.exiting == true
        || playerController.requestedSave == true
        || GameObject.Find("GameManager").GetComponent<GameManager>().dataSaveRequested == true
        || GameObject.Find("GameManager").GetComponent<StateManager>().saving == true;

    }

    // Called by unity engine for rendering and handling GUI events
    public void OnGUI()
    {
        //STYLE
        GUI.skin = thisGUIskin;

        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        if (playerController.stateManager.worldLoaded == true && GetComponent<MainMenu>().finishedLoading == true)
        {
            //BUILD ITEM HUD AT TOP RIGHT OF SCREEN
            if (playerController.displayingBuildItem == true)
            {
                GUI.Label(guiCoordinates.topRightInfoRect, "\n\nBuild item set to " + playerController.buildType);
                GUI.DrawTexture(guiCoordinates.previousBuildItemTextureRect, td.dictionary[playerController.previousBuildType]);
                GUI.DrawTexture(guiCoordinates.buildItemTextureRect, td.dictionary[playerController.buildType]);
                GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, td.dictionary[playerController.buildType]);
                GUI.DrawTexture(guiCoordinates.buildItemTextureRect, td.dictionary["Selection Box"]);
                GUI.DrawTexture(guiCoordinates.nextBuildItemTextureRect, td.dictionary[playerController.nextBuildType]);
                int buildItemCount = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.buildType))
                    {
                        buildItemCount += slot.amountInSlot;
                    }
                }
                GUI.Label(guiCoordinates.buildItemCountRect, "" + buildItemCount);
            }

            //METEOR SHOWER WARNINGS
            if (playerController.meteorShowerWarningActive == true || playerController.timeToDeliverWarningRecieved == true || playerController.pirateAttackWarningActive == true || playerController.destructionMessageActive == true)
            {
                GUI.Label(guiCoordinates.topLeftInfoRect, "Urgent message received! Check your tablet for more information.");
            }

            //TABLET
            if (playerController.tabletOpen == true)
            {
                int day = GameObject.Find("Rocket").GetComponent<Rocket>().day;
                int hour = (int)GameObject.Find("Rocket").GetComponent<Rocket>().gameTime;
                string hourString = "";
                if (hour < 10)
                {
                    hourString = "000" + hour;
                }
                else if (hour >= 10 && hour < 100)
                {
                    hourString = "00" + hour;
                }
                else if (hour >= 100 && hour < 1000)
                {
                    hourString = "0" + hour;
                }
                else if (hour >= 1000)
                {
                    hourString = "" + hour;
                }
                GUI.DrawTexture(guiCoordinates.tabletBackgroundRect, td.dictionary["Tablet"]);
                GUI.Label(guiCoordinates.tabletMessageRect, playerController.currentTabletMessage);
                GUI.Label(guiCoordinates.tabletTimeRect, "\nDay: " + day + " Hour: " + hourString + ", Income: $" + playerController.money.ToString("N0"));
                if (GUI.Button(guiCoordinates.tabletButtonRect, "CLOSE"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.tabletOpen = false;
                    playerController.PlayButtonSound();
                }
            }

            //OPTIONS/EXIT MENU
            if (playerController.escapeMenuOpen == true)
            {
                if (MenuAvailable())
                {
                    GUI.DrawTexture(guiCoordinates.escapeMenuRect, td.dictionary["Menu Background"]);
                    if (GUI.Button(guiCoordinates.escapeButton1Rect, "Resume"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        gameObject.GetComponent<MSCameraController>().enabled = true;
                        playerController.escapeMenuOpen = false;
                        playerController.optionsGUIopen = false;
                        playerController.helpMenuOpen = false;
                        playerController.videoMenuOpen = false;
                        playerController.schematicMenuOpen = false;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton2Rect, "Save"))
                    {
                        playerController.requestedSave = true;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton3Rect, "Options"))
                    {
                        playerController.optionsGUIopen = true;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton4Rect, "Help"))
                    {
                        playerController.helpMenuOpen = true;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton5Rect, "Exit"))
                    {
                        playerController.exiting = true;
                        playerController.requestedSave = true;
                        playerController.PlayButtonSound();
                    }
                }
                else
                {
                    if (SavingMessageRequired())
                    {
                        GUI.DrawTexture(guiCoordinates.savingBackgroundRect, td.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.messageRect, "\n\n\n\n\nSaving world...");
                    }
                }
            }

            //HELP MENU
            if (playerController.helpMenuOpen == true)
            {
                if (playerController.videoMenuOpen == false && playerController.schematicMenuOpen == false)
                {
                    GUI.DrawTexture(guiCoordinates.escapeMenuRect, td.dictionary["Menu Background"]);
                    if (GUI.Button(guiCoordinates.escapeButton1Rect, "Videos"))
                    {
                        playerController.videoMenuOpen = true;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton2Rect, "Schematics"))
                    {
                        playerController.schematicMenuOpen = true;
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.escapeButton4Rect, "BACK"))
                    {
                        playerController.helpMenuOpen = false;
                        playerController.PlayButtonSound();
                    }
                }
                if (playerController.videoMenuOpen == true)
                {
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        GUI.DrawTexture(guiCoordinates.videoMenuBackgroundRect, td.dictionary["Menu Background"]);
                    }
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        if (GUI.Button(guiCoordinates.optionsButton1Rect, "Intro"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Guide.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton2Rect, "Dark Matter"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("DarkMatter.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton3Rect, "Universal Extractor"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Extractor.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton4Rect, "Heat Exchanger"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("HeatExchanger.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton5Rect, "Alloy Smelter"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("AlloySmelter.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton6Rect, "Hazards"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Hazards.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton7Rect, "Rail Carts"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("RailCarts.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton8Rect, "Storage Computers"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("StorageComputers.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton9Rect, "Nuclear Reactors"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("NuclearReactors.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton10Rect, "BACK"))
                        {
                            playerController.videoMenuOpen = false;
                            playerController.PlayButtonSound();
                        }
                    }

                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        videoPlayer.GetComponent<VP>().StopVideo();
                    }
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == true && Input.anyKey)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        videoPlayer.GetComponent<VP>().StopVideo();
                    }
                }
                if (playerController.schematicMenuOpen == true)
                {
                    if (schematic1 == false || schematic2 == false || schematic3 == false || schematic4 == false || schematic5 == false || schematic6 == false || schematic7 == false)
                    {
                        GUI.DrawTexture(guiCoordinates.schematicsMenuBackgroundRect, td.dictionary["Menu Background"]);
                        if (GUI.Button(guiCoordinates.optionsButton1Rect, "Dark Matter"))
                        {
                            if (schematic1 == false)
                            {
                                schematic1 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton2Rect, "Plates"))
                        {
                            if (schematic2 == false)
                            {
                                schematic2 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton3Rect, "Wires"))
                        {
                            if (schematic3 == false)
                            {
                                schematic3 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton4Rect, "Gears"))
                        {
                            if (schematic4 == false)
                            {
                                schematic4 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.optionsButton5Rect, "Steel"))
                        {
                            if (schematic5 == false)
                            {
                                schematic5 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.optionsButton6Rect, "Bronze"))
                        {
                            if (schematic6 == false)
                            {
                                schematic6 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.optionsButton7Rect, "Heat Exchangers"))
                        {
                            if (schematic7 == false)
                            {
                                schematic7 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.optionsButton8Rect, "BACK"))
                        {
                            playerController.schematicMenuOpen = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (schematic1 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Dark Matter Schematic"]);
                    }
                    if (schematic2 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Plate Schematic"]);
                    }
                    if (schematic3 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Wire Schematic"]);
                    }
                    if (schematic4 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Gear Schematic"]);
                    }
                    if (schematic5 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Steel Schematic"]);
                    }
                    if (schematic6 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Bronze Schematic"]);
                    }
                    if (schematic7 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), td.dictionary["Heat Exchanger Schematic"]);
                    }
                    if (schematic1 == true || schematic2 == true || schematic3 == true || schematic4 == true || schematic5 == true || schematic6 == true || schematic7 == true)
                    {
                        if (GUI.Button(guiCoordinates.schematicCloseRect,"CLOSE") || Input.anyKey)
                        {
                            schematic1 = false;
                            schematic2 = false;
                            schematic3 = false;
                            schematic4 = false;
                            schematic5 = false;
                            schematic6 = false;
                            schematic7 = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }
                else
                {
                    schematic1 = false;
                    schematic2 = false;
                    schematic3 = false;
                    schematic4 = false;
                    schematic5 = false;
                    schematic6 = false;
                    schematic7 = false;
                }
            }

            //OPTIONS MENU
            if (playerController.optionsGUIopen == true && cGUI.showingInputGUI == false)
            {
                GUI.DrawTexture(guiCoordinates.optionsMenuBackgroundRect, td.dictionary["Menu Background"]);
                if (GUI.Button(guiCoordinates.optionsButton1Rect, "Bindings"))
                {
                    cGUI.ToggleGUI();
                    playerController.PlayButtonSound();
                }
                string invertYInput = "";
                if (GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput == true)
                {
                    invertYInput = "ON";
                }
                else
                {
                    invertYInput = "OFF";
                }
                if (GUI.Button(guiCoordinates.optionsButton2Rect, "Invert Y Axis: " + invertYInput))
                {
                    if (GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput)
                    {
                        GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = false;
                    }
                    else
                    {
                        GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = true;
                    }
                    playerController.PlayButtonSound();
                }
                GUI.Label(guiCoordinates.sliderLabel1Rect, "X sensitivity");
                GUI.Label(guiCoordinates.sliderLabel2Rect, "Y sensitivity");
                GUI.Label(guiCoordinates.sliderLabel3Rect, "Volume");
                GUI.Label(guiCoordinates.sliderLabel4Rect, "FOV");
                GUI.Label(guiCoordinates.sliderLabel5Rect, "Draw Distance");
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX, 0, 10);
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY, 0, 10);
                AudioListener.volume = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, AudioListener.volume, 0, 5);
                GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;
                playerController.mCam.fieldOfView = GUI.HorizontalSlider(guiCoordinates.optionsButton7Rect, playerController.mCam.fieldOfView, 60, 80);
                playerController.mCam.farClipPlane = GUI.HorizontalSlider(guiCoordinates.optionsButton8Rect, playerController.mCam.farClipPlane, 1000, 100000);
                string blockPhysicsDisplay = "";
                if (GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics == true)
                {
                    blockPhysicsDisplay = "ON";
                }
                else
                {
                    blockPhysicsDisplay = "OFF";
                }
                if (GUI.Button(guiCoordinates.optionsButton9Rect, "Block Physics: "+ blockPhysicsDisplay))
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics == false)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics = true;
                    }
                    else
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics = false;
                    }
                    FileBasedPrefs.SetBool(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "blockPhysics", GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics);
                    playerController.PlayButtonSound();
                }
                string hazardsEnabledDisplay = "";
                if (GameObject.Find("GameManager").GetComponent<GameManager>().hazardsEnabled == true)
                {
                    hazardsEnabledDisplay = "ON";
                }
                else
                {
                    hazardsEnabledDisplay = "OFF";
                }
                if (GUI.Button(guiCoordinates.optionsButton10Rect, "Hazards: " + hazardsEnabledDisplay))
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().hazardsEnabled == false)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().hazardsEnabled = true;
                    }
                    else
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().hazardsEnabled = false;
                    }
                    FileBasedPrefs.SetBool(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "hazardsEnabled", GameObject.Find("GameManager").GetComponent<GameManager>().hazardsEnabled);
                    playerController.PlayButtonSound();
                }
                if (GUI.Button(guiCoordinates.optionsButton11Rect, "BACK"))
                {
                    playerController.ApplySettings();
                    playerController.optionsGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }

            if (playerController.cannotCollect == true)
            {
                if (playerController.cannotCollectTimer < 3)
                {
                    GUI.Label(guiCoordinates.messageRect, "\n\nNo space in inventory.");
                    playerController.cannotCollectTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.cannotCollect = false;
                    playerController.cannotCollectTimer = 0;
                }
            }

            if (playerController.invalidAugerPlacement == true)
            {
                if (playerController.invalidAugerPlacementTimer < 3)
                {
                    GUI.Label(guiCoordinates.messageRect, "\n\n\n\nInvalid location.");
                    playerController.invalidAugerPlacementTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.invalidAugerPlacement = false;
                    playerController.invalidAugerPlacementTimer = 0;
                }
            }

            if (playerController.invalidRailCartPlacement == true)
            {
                if (playerController.invalidRailCartPlacementTimer < 3)
                {
                    GUI.Label(guiCoordinates.messageRect, "\n\n\n\nInvalid location.");
                    playerController.invalidRailCartPlacementTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.invalidRailCartPlacement = false;
                    playerController.invalidRailCartPlacementTimer = 0;
                }
            }

            if (playerController.stoppingBuildCoRoutine == true || playerController.requestedBuildingStop == true)
            {
                GUI.Label(guiCoordinates.longHighMessageRect, "Stopping Build System...");
            }

            if (playerController.blockLimitMessage == true)
            {
                if (playerController.blockLimitMessageTimer < 3)
                {
                    GUI.Label(guiCoordinates.longHighMessageRect, "\nWorld limit exceeded!");
                    playerController.blockLimitMessageTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.blockLimitMessage = false;
                    playerController.blockLimitMessageTimer = 0;
                }
            }

            //BUILDING INSTRUCTIONS
            if (playerController.building == true && playerController.tabletOpen == false)
            {
                GUI.DrawTexture(guiCoordinates.buildInfoRectBG, td.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.buildInfoRect, "Right click to place block.\nPress F to collect.\nPress R to rotate.\nPress Q to stop building.");
                GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, td.dictionary[playerController.buildType]);
                int buildItemCount = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.buildType))
                    {
                        buildItemCount += slot.amountInSlot;
                    }
                }
                if (playerController.buildType == "Brick" || playerController.buildType == "Glass Block" || playerController.buildType == "Iron Block" || playerController.buildType == "Iron Ramp" || playerController.buildType == "Steel Block" || playerController.buildType == "Steel Ramp")
                {
                    GUI.Label(guiCoordinates.buildItemCountRect, "" + buildItemCount + "\nx" + playerController.buildMultiplier);
                }
                else
                {
                    GUI.Label(guiCoordinates.buildItemCountRect, "" + buildItemCount);
                }
            }

            //PAINT COLOR SELECTION WINDOW
            if (playerController.paintGunActive == true)
            {
                if (playerController.paintColorSelected == false)
                {
                    GUI.DrawTexture(guiCoordinates.optionsMenuBackgroundRect, td.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.optionsButton1Rect, "      Paint  Gun");
                    GUI.Label(guiCoordinates.optionsButton2Rect, "     Select Color");
                    GUI.Label(guiCoordinates.sliderLabel1Rect, "Red");
                    GUI.Label(guiCoordinates.sliderLabel2Rect, "Green");
                    GUI.Label(guiCoordinates.sliderLabel3Rect, "Blue");
                    playerController.paintRed = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, playerController.paintRed, 0, 1);
                    playerController.paintGreen = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, playerController.paintGreen, 0, 1);
                    playerController.paintBlue = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, playerController.paintBlue, 0, 1);
                    GUI.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.paintGunTank.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.adjustedPaintGunTank.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.adjustedPaintGunTank2.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    GUI.DrawTexture(guiCoordinates.optionsButton3Rect, td.dictionary["Iron Block"]);
                    GUI.color = Color.white;
                    if (GUI.Button(guiCoordinates.optionsButton8Rect, "DONE"))
                    {
                        playerController.paintColorSelected = true;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        gameObject.GetComponent<MSCameraController>().enabled = true;
                        playerController.PlayButtonSound();
                    }
                }
                else if (playerController.lookingAtCombinedMesh == true)
                {
                    GUI.Label(guiCoordinates.highMessageRect, "Left click to paint.\nRight click to stop.");
                }
                else
                {
                    GUI.Label(guiCoordinates.longHighMessageRect, "Only structures can be painted...");
                }
            }

            //CROSSHAIR
            if (!playerController.inventoryOpen && !playerController.machineGUIopen && !playerController.marketGUIopen)
            {
                if (!playerController.escapeMenuOpen && !playerController.tabletOpen && !playerController.paintGunActive)
                {
                    if (!playerController.objectInSight || playerController.building)
                    {
                        if (playerController.crosshairEnabled)
                        {
                            GUI.DrawTexture(guiCoordinates.crosshairRect, td.dictionary["Crosshair"]);
                        }
                    }
                }
            }
        }
    }
}
