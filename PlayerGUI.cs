using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    private StateManager stateManager;
    private InventoryManager playerInventory;
    private TextureDictionary textureDictionary;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        textureDictionary = GetComponent<TextureDictionary>();
        guiCoordinates = new GuiCoordinates();
    }

    // Returns true if the escape menu should be displayed.
    private bool MenuAvailable()
    {
        return playerController.helpMenuOpen == false
        && playerController.optionsGUIopen == false
        && cGUI.showingInputGUI == false
        && playerController.exiting == false
        && playerController.requestedSave == false
        && gameManager.dataSaveRequested == false
        && stateManager.saving == false;
    }

    private bool IsStandardBlock(string type)
    {
        return type == "Brick"
        || type == "Glass Block"
        || type == "Iron Block"
        || type == "Iron Ramp"
        || type == "Steel Block"
        || type == "Steel Ramp";
    }

    private bool SavingMessageRequired()
    {
        return playerController.exiting == true
        || playerController.requestedSave == true
        || gameManager.dataSaveRequested == true
        || stateManager.saving == true;
    }

    private bool TabletNotificationRequired()
    {
        return playerController.meteorShowerWarningActive == true
        || playerController.timeToDeliverWarningRecieved == true
        || playerController.pirateAttackWarningActive == true
        || playerController.destructionMessageActive == true;
    }

    private void ClearSchematic()
    {
        schematic1 = false;
        schematic2 = false;
        schematic3 = false;
        schematic4 = false;
        schematic5 = false;
        schematic6 = false;
        schematic7 = false;
    }

    private bool SchematicActive()
    {
        return schematic1 == true
        || schematic2 == true
        || schematic3 == true
        || schematic4 == true
        || schematic5 == true
        || schematic6 == true
        || schematic7 == true;
    }

    // Called by unity engine for rendering and handling GUI events
    public void OnGUI()
    {
        //STYLE
        GUI.skin = thisGUIskin;

        //ASPECT RATIO
        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;
        if (ScreenWidth / ScreenHeight < 1.7f)
        {
            ScreenHeight = (ScreenHeight * 0.75f);
        }
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
                GUI.DrawTexture(guiCoordinates.previousBuildItemTextureRect, textureDictionary.dictionary[playerController.previousBuildType]);
                GUI.DrawTexture(guiCoordinates.buildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                GUI.DrawTexture(guiCoordinates.buildItemTextureRect, textureDictionary.dictionary["Selection Box"]);
                GUI.DrawTexture(guiCoordinates.nextBuildItemTextureRect, textureDictionary.dictionary[playerController.nextBuildType]);
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
            if (TabletNotificationRequired())
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
                GUI.DrawTexture(guiCoordinates.tabletBackgroundRect, textureDictionary.dictionary["Tablet"]);
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
                    GUI.DrawTexture(guiCoordinates.escapeMenuRect, textureDictionary.dictionary["Menu Background"]);
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
                        GUI.DrawTexture(guiCoordinates.lowMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.lowMessageRect, "Saving world...");
                    }
                }
            }

            //HELP MENU
            if (playerController.helpMenuOpen == true)
            {
                if (playerController.videoMenuOpen == false && playerController.schematicMenuOpen == false)
                {
                    GUI.DrawTexture(guiCoordinates.escapeMenuRect, textureDictionary.dictionary["Menu Background"]);
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
                        GUI.DrawTexture(guiCoordinates.videoMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                    }
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        if (GUI.Button(guiCoordinates.helpButton1Rect, "Intro"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Guide.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton2Rect, "Dark Matter"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("DarkMatter.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton3Rect, "Universal Extractor"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Extractor.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton4Rect, "Heat Exchanger"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("HeatExchanger.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton5Rect, "Alloy Smelter"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("AlloySmelter.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton6Rect, "Hazards"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Hazards.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton7Rect, "Rail Carts"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("RailCarts.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton8Rect, "Storage Computers"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("StorageComputers.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton9Rect, "Nuclear Reactors"))
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("NuclearReactors.webm", false, 0.5f);
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton10Rect, "BACK"))
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
                    if (!SchematicActive())
                    {
                        GUI.DrawTexture(guiCoordinates.schematicsMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                        if (GUI.Button(guiCoordinates.helpButton1Rect, "Dark Matter"))
                        {
                            if (schematic1 == false)
                            {
                                schematic1 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton2Rect, "Plates"))
                        {
                            if (schematic2 == false)
                            {
                                schematic2 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton3Rect, "Wires"))
                        {
                            if (schematic3 == false)
                            {
                                schematic3 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton4Rect, "Gears"))
                        {
                            if (schematic4 == false)
                            {
                                schematic4 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.helpButton5Rect, "Steel"))
                        {
                            if (schematic5 == false)
                            {
                                schematic5 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.helpButton6Rect, "Bronze"))
                        {
                            if (schematic6 == false)
                            {
                                schematic6 = true;
                            }
                            playerController.PlayButtonSound();

                        }
                        if (GUI.Button(guiCoordinates.helpButton7Rect, "Heat Exchangers"))
                        {
                            if (schematic7 == false)
                            {
                                schematic7 = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.helpButton8Rect, "BACK"))
                        {
                            playerController.schematicMenuOpen = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (schematic1 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Dark Matter Schematic"]);
                    }
                    if (schematic2 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Plate Schematic"]);
                    }
                    if (schematic3 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Wire Schematic"]);
                    }
                    if (schematic4 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Gear Schematic"]);
                    }
                    if (schematic5 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Steel Schematic"]);
                    }
                    if (schematic6 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Bronze Schematic"]);
                    }
                    if (schematic7 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), textureDictionary.dictionary["Heat Exchanger Schematic"]);
                    }
                    if (SchematicActive())
                    {
                        if (GUI.Button(guiCoordinates.schematicCloseRect,"CLOSE") || Input.anyKey)
                        {
                            ClearSchematic();
                            playerController.PlayButtonSound();
                        }
                    }
                }
                else
                {
                    ClearSchematic();
                }
            }

            //OPTIONS MENU
            if (playerController.optionsGUIopen == true && cGUI.showingInputGUI == false)
            {
                GUI.DrawTexture(guiCoordinates.optionsMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                if (GUI.Button(guiCoordinates.optionsButton1Rect, "Bindings"))
                {
                    cGUI.ToggleGUI();
                    playerController.PlayButtonSound();
                }

                MSACC_SettingsCameraFirstPerson csInverted = GetComponent<MSCameraController>().CameraSettings.firstPerson;
                string invertYInput = csInverted.invertYInput == true ? "ON" : "OFF";
                if (GUI.Button(guiCoordinates.optionsButton2Rect, "Invert Y Axis: " + invertYInput))
                {
                    csInverted.invertYInput = !csInverted.invertYInput;
                    playerController.PlayButtonSound();
                }

                GUI.Label(guiCoordinates.sliderLabel1Rect, "X sensitivity");
                GUI.Label(guiCoordinates.sliderLabel2Rect, "Y sensitivity");
                GUI.Label(guiCoordinates.sliderLabel3Rect, "Volume");
                GUI.Label(guiCoordinates.sliderLabel4Rect, "FOV");
                GUI.Label(guiCoordinates.sliderLabel5Rect, "Draw Distance");
                GUI.Label(guiCoordinates.sliderLabel6Rect, "Fog Density");

                MSACC_SettingsCameraFirstPerson csSensitivity = GetComponent<MSCameraController>().CameraSettings.firstPerson;
                csSensitivity.sensibilityX = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, csSensitivity.sensibilityX, 0, 10);
                csSensitivity.sensibilityY = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, csSensitivity.sensibilityY, 0, 10);

                AudioListener.volume = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, AudioListener.volume, 0, 5);
                GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;

                playerController.mCam.fieldOfView = GUI.HorizontalSlider(guiCoordinates.optionsButton7Rect, playerController.mCam.fieldOfView, 60, 80);
                playerController.mCam.farClipPlane = GUI.HorizontalSlider(guiCoordinates.optionsButton8Rect, playerController.mCam.farClipPlane, 1000, 100000);

                RenderSettings.fogDensity = GUI.HorizontalSlider(guiCoordinates.optionsButton9Rect, RenderSettings.fogDensity, 0.00025f, 0.025f);

                string fogDisplay = RenderSettings.fog == true ? "ON" : "OFF";
                if (GUI.Button(guiCoordinates.optionsButton10Rect, "Fog: " + fogDisplay))
                {
                    RenderSettings.fog = !RenderSettings.fog;
                    PlayerPrefsX.SetPersistentBool("blockPhysics", gameManager.blockPhysics);
                    playerController.PlayButtonSound();
                }

                string blockPhysicsDisplay = gameManager.blockPhysics == true ? "ON" : "OFF";
                if (GUI.Button(guiCoordinates.optionsButton11Rect, "Block Physics: "+ blockPhysicsDisplay))
                {
                    gameManager.blockPhysics = !gameManager.blockPhysics;
                    PlayerPrefsX.SetPersistentBool("blockPhysics", gameManager.blockPhysics);
                    playerController.PlayButtonSound();
                }

                string hazardsEnabledDisplay = gameManager.hazardsEnabled == true ? "ON" : "OFF";
                if (GUI.Button(guiCoordinates.optionsButton12Rect, "Hazards: " + hazardsEnabledDisplay))
                {
                    gameManager.hazardsEnabled = !gameManager.hazardsEnabled;
                    PlayerPrefsX.SetPersistentBool("hazardsEnabled", gameManager.hazardsEnabled);
                    playerController.PlayButtonSound();
                }
                if (GUI.Button(guiCoordinates.optionsButton13Rect, "BACK"))
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
                    GUI.DrawTexture(guiCoordinates.midMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.midMessageRect, "No space in inventory.");
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
                    GUI.DrawTexture(guiCoordinates.lowMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.lowMessageRect, "Invalid location.");
                    playerController.invalidAugerPlacementTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.invalidAugerPlacement = false;
                    playerController.invalidAugerPlacementTimer = 0;
                }
            }

            if (playerController.autoAxisMessage == true)
            {
                if (playerController.autoAxisMessageTimer < 3)
                {
                    if (GetComponent<BuildController>().autoAxis == true)
                    {
                        GUI.DrawTexture(guiCoordinates.lowMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.lowMessageRect, "Auto Axis Snap");
                    }
                    else
                    {
                        GUI.DrawTexture(guiCoordinates.lowMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.lowMessageRect, "Manual Axis Snap");
                    }
                    playerController.autoAxisMessageTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.autoAxisMessage = false;
                    playerController.autoAxisMessageTimer = 0;
                }
            }

            if (playerController.invalidRailCartPlacement == true)
            {
                if (playerController.invalidRailCartPlacementTimer < 3)
                {
                    GUI.DrawTexture(guiCoordinates.lowMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.lowMessageRect, "Invalid location.");
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
                GUI.DrawTexture(guiCoordinates.highMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.longHighMessageRect, "Stopping Build System...");
            }

            if (playerController.blockLimitMessage == true)
            {
                if (playerController.blockLimitMessageTimer < 3)
                {
                    GUI.DrawTexture(guiCoordinates.secondLineHighMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.secondLineHighMessageRect, "World limit exceeded!");
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
                GUI.DrawTexture(guiCoordinates.buildInfoRectBG, textureDictionary.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.buildInfoRect, "Right click to place block.\nPress F to collect.\nPress R to rotate.\nPress Q to stop building.");
                GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                int buildItemCount = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.buildType))
                    {
                        buildItemCount += slot.amountInSlot;
                    }
                }
                if (IsStandardBlock(playerController.buildType))
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
                    GUI.DrawTexture(guiCoordinates.optionsMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                    GUI.Label(guiCoordinates.optionsButton1Rect, "        Paint  Gun");
                    GUI.Label(guiCoordinates.optionsButton2Rect, "       Select Color");
                    GUI.Label(guiCoordinates.sliderLabel1Rect, "Red");
                    GUI.Label(guiCoordinates.sliderLabel2Rect, "Green");
                    GUI.Label(guiCoordinates.sliderLabel3Rect, "Blue");
                    playerController.paintRed = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, playerController.paintRed, 0, 1);
                    playerController.paintGreen = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, playerController.paintGreen, 0, 1);
                    playerController.paintBlue = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, playerController.paintBlue, 0, 1);
                    Color paintcolor = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    Material tankMat = playerController.paintGunTank.GetComponent<Renderer>().material;
                    Material adjTankMat = playerController.adjustedPaintGunTank.GetComponent<Renderer>().material;
                    Material adjTank2Mat = playerController.adjustedPaintGunTank2.GetComponent<Renderer>().material;
                    tankMat.color = paintcolor;
                    adjTankMat.color = paintcolor;
                    adjTank2Mat.color = paintcolor;
                    GUI.color = paintcolor;
                    GUI.DrawTexture(guiCoordinates.optionsButton3Rect, textureDictionary.dictionary["Iron Block"]);
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
                    GUI.DrawTexture(guiCoordinates.twoLineHighMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.highMessageRect, "Left click to paint.\nRight click to stop.");
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.longHighMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.longHighMessageRect, "Only structures can be painted...");
                }
            }

            //CROSSHAIR
            if (!playerController.inventoryOpen && !playerController.machineGUIopen && !playerController.marketGUIopen)
            {
                if (!playerController.escapeMenuOpen && !playerController.tabletOpen && !playerController.paintGunActive)
                {
                    if (playerController.crosshairEnabled)
                    {
                        GUIContent content = new GUIContent(Resources.Load("Crosshair") as Texture2D);
                        GUIStyle style = GUI.skin.box;
                        style.alignment = TextAnchor.MiddleCenter;
                        Vector2 size = style.CalcSize(content);
                        size.x = size.x / 3.5f;
                        size.y = size.y / 4;
                        Rect crosshairRect = new Rect((Screen.width / 2) - (size.x / 2), (Screen.height / 2) - (size.y / 2), size.x, size.y);
                        GUI.DrawTexture(crosshairRect, textureDictionary.dictionary["Crosshair"]);
                    }
                }
            }
        }
    }
}
