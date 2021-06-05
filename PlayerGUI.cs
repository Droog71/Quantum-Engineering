using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGUI : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    private StateManager stateManager;
    private InventoryManager playerInventory;
    private TextureDictionary textureDictionary;
    private Texture2D paintSelectionTexture;
    private GuiCoordinates guiCoordinates;
    public GUISkin thisGUIskin;
    public GameObject videoPlayer;
    private Descriptions descriptions;
    private bool controlsMenuOpen;
    private bool graphicsMenuOpen;
    private bool schematic1;
    private bool schematic2;
    private bool schematic3;
    private bool schematic4;
    private bool schematic5;
    private bool schematic6;
    private bool schematic7;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textureDictionary = gameManager.GetComponent<TextureDictionary>();
        descriptions = new Descriptions();
        guiCoordinates = new GuiCoordinates();
        paintSelectionTexture = new Texture2D(512, 128);
    }

    //! Returns true if the escape menu should be displayed.
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

    //! Returns true if the block is a building block, used with combined meshes.
    private bool IsStandardBlock(string type)
    {
        return playerController.GetComponent<BuildController>().blockDictionary.blockDictionary.ContainsKey(type);
    }

    //! Returns true if the saving world message should be displayed.
    private bool SavingMessageRequired()
    {
        return playerController.exiting == true
        || playerController.requestedSave == true
        || gameManager.dataSaveRequested == true
        || stateManager.saving == true;
    }

    //! Returns true if a tablet notification should be displayed.
    private bool TabletNotificationRequired()
    {
        return playerController.meteorShowerWarningActive == true
        || playerController.timeToDeliverWarningRecieved == true
        || playerController.pirateAttackWarningActive == true
        || playerController.destructionMessageActive == true;
    }

    //! Stops displaying schematics when the menu is closed.
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

    //! Returns true if any schematic is being displayed.
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

    //! Returns true if the crosshair should be displayed.
    private bool ShowCrosshair()
    {
        return playerController.crosshairEnabled &&
        !playerController.GuiOpen() &&
        !playerController.paintGunActive;
    }

    //! Gets the size of in pixels of text so it can be positioned on the screen accordingly.
    private Vector2 GetStringSize(string str)
    {
        GUIContent content = new GUIContent(str);
        GUIStyle style = GUI.skin.box;
        style.alignment = TextAnchor.MiddleCenter;
        return style.CalcSize(content);
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        // STYLE
        GUI.skin = thisGUIskin;

        // ASPECT RATIO
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
            // BUILD ITEM HUD AT TOP RIGHT OF SCREEN
            if (playerController.displayingBuildItem == true)
            {
                GUI.Label(guiCoordinates.topRightInfoRect, "\n\nBuild item set to " + playerController.buildType);

                if (textureDictionary.dictionary.ContainsKey(playerController.previousBuildType + "_Icon"))
                {
                    GUI.DrawTexture(guiCoordinates.previousBuildItemTextureRect, textureDictionary.dictionary[playerController.previousBuildType + "_Icon"]);
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.previousBuildItemTextureRect, textureDictionary.dictionary[playerController.previousBuildType]);
                }

                if (textureDictionary.dictionary.ContainsKey(playerController.buildType + "_Icon"))
                {
                    GUI.DrawTexture(guiCoordinates.buildItemTextureRect, textureDictionary.dictionary[playerController.buildType + "_Icon"]);
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.buildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                }

                if (textureDictionary.dictionary.ContainsKey(playerController.buildType + "_Icon"))
                {
                    GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType + "_Icon"]);
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                }

                GUI.DrawTexture(guiCoordinates.buildItemTextureRect, textureDictionary.dictionary["Selection Box"]);

                if (textureDictionary.dictionary.ContainsKey(playerController.nextBuildType + "_Icon"))
                {
                    GUI.DrawTexture(guiCoordinates.nextBuildItemTextureRect, textureDictionary.dictionary[playerController.nextBuildType + "_Icon"]);
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.nextBuildItemTextureRect, textureDictionary.dictionary[playerController.nextBuildType]);
                }

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

            // METEOR SHOWER WARNINGS
            if (TabletNotificationRequired())
            {
                GUI.Label(guiCoordinates.topLeftInfoRect, "Urgent message received! Check your tablet for more information.");
            }

            // TABLET
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

            // OPTIONS/EXIT MENU
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
                        int current = stateManager.saveManager.currentObject;
                        int total = stateManager.saveManager.totalObjects;
                        GUI.DrawTexture(guiCoordinates.saveMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        if (total > 0)
                        {
                            GUI.Label(guiCoordinates.saveMessageRect, "Saving world... "+current+"/"+total);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.saveMessageRect, "Saving world... " + "preparing");
                        }
                    }
                }
            }

            // HELP MENU
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
                        if (GUI.Button(guiCoordinates.schematicCloseRect,"CLOSE") || Input.GetKeyDown(KeyCode.Escape))
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

            // OPTIONS MENU
            if (playerController.optionsGUIopen == true)
            {
                Vector2 mousePos = Event.current.mousePosition;              
                if (controlsMenuOpen == false && graphicsMenuOpen == false)
                {
                    GUI.DrawTexture(guiCoordinates.optionsMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                    if (GUI.Button(guiCoordinates.optionsButton1Rect, "Graphics"))
                    {
                        graphicsMenuOpen = true;
                        playerController.PlayButtonSound();
                    }

                    if (GUI.Button(guiCoordinates.optionsButton2Rect, "Controls"))
                    {
                        controlsMenuOpen = true;
                        playerController.PlayButtonSound();
                    }

                    GUI.Label(guiCoordinates.sliderLabel1Rect, "Audio Volume");

                    GUI.Label(guiCoordinates.sliderLabel2Rect, "Chunk Size " + "(" + gameManager.chunkSize + ")");

                    int simSpeed = (int)(gameManager.simulationSpeed * 5000);
                    GUI.Label(guiCoordinates.sliderLabel3Rect, "Simulation Speed " + "(" + simSpeed + "%)");

                    AudioListener.volume = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, AudioListener.volume, 0, 5);
                    GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;

                    gameManager.chunkSize = (int)GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, gameManager.chunkSize, 20, 100);

                    if (guiCoordinates.optionsButton5Rect.Contains(mousePos))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.chunkSize);
                    }

                    gameManager.simulationSpeed = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, gameManager.simulationSpeed, 0.0051f, 0.1f);

                    if (guiCoordinates.optionsButton6Rect.Contains(mousePos))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.simulationSpeed);
                    }

                    string blockPhysicsDisplay = gameManager.blockPhysics == true ? "ON" : "OFF";
                    if (GUI.Button(guiCoordinates.optionsButton7Rect, "Block Physics: "+ blockPhysicsDisplay))
                    {
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == false)
                        {
                            gameManager.blockPhysics = !gameManager.blockPhysics;
                        }
                        playerController.PlayButtonSound();
                    }  

                    if (guiCoordinates.optionsButton7Rect.Contains(mousePos))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.blockPhysics);
                    }

                    string hazardsEnabledDisplay = gameManager.hazardsEnabled == true ? "ON" : "OFF";
                    if (GUI.Button(guiCoordinates.optionsButton8Rect, "Hazards: " + hazardsEnabledDisplay))
                    {
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == false)
                        {
                            gameManager.hazardsEnabled = !gameManager.hazardsEnabled;
                        }
                        else if (PlayerPrefsX.GetPersistentBool("hosting") == true)
                        {
                            gameManager.hazardsEnabled = !gameManager.hazardsEnabled;
                            playerController.networkController.networkSend.SendHazardData(gameManager.hazardsEnabled);
                        }
                        playerController.PlayButtonSound();
                    }

                    if (guiCoordinates.optionsButton8Rect.Contains(mousePos))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.hazards);
                    }

                    if (GUI.Button(guiCoordinates.optionsButton9Rect, "BACK"))
                    {
                        playerController.ApplySettings();
                        playerController.optionsGUIopen = false;
                        playerController.PlayButtonSound();
                    }
                }

                if (controlsMenuOpen == true && cGUI.showingInputGUI == false)
                {    
                    GUI.DrawTexture(guiCoordinates.optionsSubMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
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

                    MSACC_SettingsCameraFirstPerson csSensitivity = GetComponent<MSCameraController>().CameraSettings.firstPerson;
                    csSensitivity.sensibilityX = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, csSensitivity.sensibilityX, 0, 10);
                    csSensitivity.sensibilityY = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, csSensitivity.sensibilityY, 0, 10);

                    if (GUI.Button(guiCoordinates.optionsButton8Rect, "BACK"))
                    {
                        controlsMenuOpen = false;
                        playerController.PlayButtonSound();
                        playerController.PlayButtonSound();
                    }
                }

                if (graphicsMenuOpen == true)
                {
                    GUI.DrawTexture(guiCoordinates.optionsSubMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);
                    string fogDisplay = RenderSettings.fog == true ? "ON" : "OFF";
                    if (GUI.Button(guiCoordinates.optionsButton1Rect, "Fog: " + fogDisplay))
                    {
                        if (!SceneManager.GetActiveScene().name.Equals("QE_World"))
                        {
                            RenderSettings.fog = !RenderSettings.fog;
                        }
                        else
                        {
                            RenderSettings.fog = false;
                        }
                        playerController.PlayButtonSound();
                    }

                    GUI.Label(guiCoordinates.sliderLabel0Rect, "Fog Density");
                    GUI.Label(guiCoordinates.sliderLabel1Rect, "FOV");
                    GUI.Label(guiCoordinates.sliderLabel2Rect, "Draw Distance");
                    GUI.Label(guiCoordinates.sliderLabel3Rect, "Graphics Quality " + "(" + QualitySettings.names[(int)playerController.graphicsQuality] + ")");

                    RenderSettings.fogDensity = GUI.HorizontalSlider(guiCoordinates.optionsButton3Rect, RenderSettings.fogDensity, 0.00025f, 0.025f);
                    playerController.mCam.fieldOfView = GUI.HorizontalSlider(guiCoordinates.optionsButton4Rect, playerController.mCam.fieldOfView, 60, 80);
                    playerController.mCam.farClipPlane = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, playerController.mCam.farClipPlane, 1000, 10000);
                    playerController.graphicsQuality = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, playerController.graphicsQuality, 0, QualitySettings.names.Length - 1);

                    string vsyncDisplay = QualitySettings.vSyncCount == 1 ? "ON" : "OFF";
                    if (GUI.Button(guiCoordinates.optionsButton7Rect, "Vsync: " + vsyncDisplay))
                    {
                        QualitySettings.vSyncCount = QualitySettings.vSyncCount == 0 ? 1 : 0;
                        playerController.PlayButtonSound();
                    }

                    if (GUI.Button(guiCoordinates.optionsButton8Rect, "BACK"))
                    {
                        graphicsMenuOpen = false;
                        playerController.PlayButtonSound();
                    }
                }
            }
            else
            {
                graphicsMenuOpen = false;
                controlsMenuOpen = false;
            }
            //END OPTIONS MENU

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
                GUI.DrawTexture(guiCoordinates.buildingMessageBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.buildingMessageRect, "Stopping Build System...");
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

            // BUILDING INSTRUCTIONS
            if (playerController.building == true && !playerController.GuiOpen())
            {
                if (textureDictionary.dictionary.ContainsKey(playerController.buildType + "_Icon"))
                {
                    GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType + "_Icon"]);
                }
                else
                {
                    GUI.DrawTexture(guiCoordinates.currentBuildItemTextureRect, textureDictionary.dictionary[playerController.buildType]);
                }

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

                if (playerController.machineInSight == null)
                {
                    GUI.DrawTexture(guiCoordinates.buildInfoRectBG, textureDictionary.dictionary["Interface Background"]);
                    int f = GUI.skin.label.fontSize;
                    GUI.skin.label.fontSize = 16;
                    GUI.Label(guiCoordinates.buildInfoRect, "Right click to place block.\nPress F to collect.\nPress R or Ctrl+R to rotate.\nPress B to stop building.");
                    GUI.skin.label.fontSize = f;
                }
            }

            // PAINT COLOR SELECTION WINDOW
            if (playerController.paintGunActive == true)
            {
                if (playerController.paintColorSelected == false)
                {
                    GUI.DrawTexture(guiCoordinates.paintGunMenuBackgroundRect, textureDictionary.dictionary["Menu Background"]);

                    int f = GUI.skin.label.fontSize;
                    GUI.skin.label.fontSize = 14;

                    GUIStyle style = GUI.skin.box;
                    style.alignment = TextAnchor.MiddleCenter;
                    GUIContent content = new GUIContent("Paint Gun");
                    Vector2 size = style.CalcSize(content);
                    Rect titleRect = new Rect((Screen.width / 2) - (size.x / 2.5f), ScreenHeight * 0.05f, size.x, size.y);
                    GUI.Label(titleRect, "Paint Gun");

                    GUIStyle style2 = GUI.skin.box;
                    style2.alignment = TextAnchor.MiddleCenter;
                    GUIContent content2 = new GUIContent("Select Color");
                    Vector2 size2 = style2.CalcSize(content2);
                    Rect titleRect2 = new Rect((Screen.width / 2) - (size2.x / 2.5f), ScreenHeight * 0.11f, size2.x, size2.y);
                    GUI.Label(titleRect2, "Select Color");

                    GUI.skin.label.fontSize = f;

                    GUI.Label(guiCoordinates.sliderLabel2Rect, "Red");
                    GUI.Label(guiCoordinates.sliderLabel3Rect, "Green");
                    GUI.Label(guiCoordinates.sliderLabel4Rect, "Blue");

                    playerController.paintRed = GUI.HorizontalSlider(guiCoordinates.optionsButton5Rect, playerController.paintRed, 0, 1);
                    playerController.paintGreen = GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, playerController.paintGreen, 0, 1);
                    playerController.paintBlue = GUI.HorizontalSlider(guiCoordinates.optionsButton7Rect, playerController.paintBlue, 0, 1);

                    Color paintcolor = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);

                    Material tankMat = playerController.paintGunTank.GetComponent<Renderer>().material;
                    Material adjTankMat = playerController.adjustedPaintGunTank.GetComponent<Renderer>().material;
                    Material adjTank2Mat = playerController.adjustedPaintGunTank2.GetComponent<Renderer>().material;

                    tankMat.color = paintcolor;
                    adjTankMat.color = paintcolor;
                    adjTank2Mat.color = paintcolor;

                    GUI.color = paintcolor;
                    GUI.DrawTexture(guiCoordinates.optionsButton3Rect, paintSelectionTexture);
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

            // BUILD SETTINGS
            if (playerController.buildSettingsGuiOpen)
            {
                GUI.DrawTexture(guiCoordinates.buildSettingsRect, textureDictionary.dictionary["Menu Background"]);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Build Settings");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.14f, size.x, size.y);
                GUI.Label(messagePos, "Build Settings");
                GUI.skin.label.fontSize = f;

                GUI.Label(guiCoordinates.optionsButton3Rect, "Build Multiplier");

                string amountString = GUI.TextField(guiCoordinates.buildAmountTextFieldRect, playerController.buildMultiplier.ToString(), 3);
                try
                {
                    playerController.buildMultiplier = int.Parse(amountString);
                }
                catch
                {
                    // NOOP
                }

                int i = playerController.buildMultiplier;
                i = i > 100 ? 100 : i;
                playerController.buildMultiplier = i;

                GUI.Label(guiCoordinates.sliderLabel3Rect, "Machine Range " + "(" + (double)playerController.defaultRange / 10 + " meters"  + ")");
                playerController.defaultRange = (int)GUI.HorizontalSlider(guiCoordinates.optionsButton6Rect, playerController.defaultRange, 10, 120);

                if (GUI.Button(guiCoordinates.optionsButton7Rect, "OK"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.buildSettingsGuiOpen = false;
                    playerController.PlayButtonSound();
                }
            }

            // DOOR SETTINGS
            if (playerController.doorGUIopen)
            {
                GUI.DrawTexture(guiCoordinates.doorSettingsRect, textureDictionary.dictionary["Menu Background"]);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 12;
                GUI.Label(guiCoordinates.doorTitleRect, "Door Settings");
                GUI.skin.label.fontSize = f;

                if (GUI.Button(guiCoordinates.doorTextureRect,"Material"))
                {
                    Door door = playerController.doorToEdit;

                    if (door.textureIndex < door.textures.Length - 1)
                        door.textureIndex++;
                    else
                        door.textureIndex = 0;

                    door.material = door.textures[door.textureIndex];
                    gameManager.meshManager.SetMaterial(door.closedObject, door.material);
                    door.edited = true;
                    playerController.PlayButtonSound();
                }

                if (GUI.Button(guiCoordinates.doorSoundRect,"Sound"))
                {
                    Door door = playerController.doorToEdit;

                    if (door.audioClip < door.audioClips.Length - 1)
                        door.audioClip++;
                    else
                        door.audioClip = 0;

                    door.GetComponent<AudioSource>().clip = door.audioClips[door.audioClip];
                    door.GetComponent<AudioSource>().Play();
                }

                if (GUI.Button(guiCoordinates.doorCloseRect, "OK"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.doorGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }

            // CROSSHAIR
            if (ShowCrosshair() == true)
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