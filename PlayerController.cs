using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private Coroutine saveCoroutine;
    private Vector3 originalPosition;

    public Vector3 destroyStartPosition;
    public Vector3 buildStartPosition;
    public RaycastHit playerLookHit;
    public StateManager stateManager;
    public GameManager gameManager;
    public Camera mCam;
    public InventoryManager playerInventory;
    public InventoryManager storageInventory;
    public InventorySlot slotDraggingFrom;
    public AudioSource builderSound;
    public AudioSource guiSound;
    public GameObject objectInSight;
    public GameObject machineInSight;
    public LaserController laserController;
    public BlockSelector blockSelector;

    public bool outOfSpace;
    public bool cannotCollect;
    public bool building;
    public bool inventoryOpen;
    public bool storageGUIopen;
    public bool machineGUIopen;
    public bool craftingGUIopen;
    public bool marketGUIopen;
    public bool remoteStorageActive;
    public bool escapeMenuOpen;
    public bool machineHasPower;
    public bool lookingAtCombinedMesh;
    public bool tabletOpen;
    public bool optionsGUIopen;
    public bool timeToDeliverWarningRecieved;
    public bool draggingItem;
    public bool displayingBuildItem;
    public bool exiting;
    public bool requestedSave;
    public bool invalidAugerPlacement;
    public bool autoAxisMessage;
    public bool invalidRailCartPlacement;
    public bool helpMenuOpen;
    public bool requestedBuildingStop;
    public bool pirateAttackWarningActive;
    public bool meteorShowerWarningActive;
    public bool destructionMessageActive;
    public bool timeToDeliver;
    public bool paintGunActive;
    public bool paintColorSelected;
    public bool videoMenuOpen;
    public bool schematicMenuOpen;
    public bool crosshairEnabled = true;
    public bool stoppingBuildCoRoutine;
    public bool separatedBlocks;
    public bool destroying;
    public bool requestedChunkLoad;
    public bool blockLimitMessage;
    public bool requestedEscapeMenu;
    public bool laserCannonActive;
    public bool scannerActive;
    public bool firing;
    public bool scanning;

    private bool gotPosition;
    private bool gameStarted;
    private bool meteorShowerWarningReceived;
    private bool pirateAttackWarningReceived;
    private bool destructionMessageReceived;
    private bool movedPlayer;
    private bool introDisplayed;


    public string cubeloc = "up";
    public string machineID = "unassigned";
    public string machineOutputID = "unassigned";
    public string machineOutputID2 = "unassigned";
    public string machineInputID = "unassigned";
    public string machineInputID2 = "unassigned";
    public string machineType;
    public string machineType2;
    public string machineInputType;
    public string machineInputType2;
    public string machineOutputType;
    public string currentTabletMessage = "";
    public string itemToDrag;
    public string buildType = "Dark Matter Collector";
    public string nextBuildType = "Turret";
    public string previousBuildType = "Dark Matter Conduit";

    public float outOfSpaceTimer;
    public float cannotCollectTimer;
    public float collectorAmount;
    public float machineAmount;
    public float machineAmount2;
    public float machineInputAmount;
    public float machineInputAmount2;
    public float machineOutputAmount;
    public float deliveryWarningTimer;
    public float buildItemDisplayTimer;
    public float footStepTimer;
    public float footStepSoundFrquency;
    public float destroyTimer;
    public float buildTimer;
    public float buildIncrementTimer;
    public float invalidAugerPlacementTimer;
    public float autoAxisMessageTimer;
    public float invalidRailCartPlacementTimer;
    public float paintRed;
    public float paintGreen;
    public float paintBlue;
    public float requestedSaveTimer;
    public float blockLimitMessageTimer;

    public int playerMoveSpeed;
    public int machinePower;
    public int machineSpeed;
    public double machineRange;
    public int machineHeat;
    public int machineCooling;
    public int amountToDrag;
    public int buildMultiplier = 1;
    public int money;
    public int destructionMessageCount;
    public int storageComputerInventory;

    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip footStep3;
    public AudioClip footStep4;
    public AudioClip metalFootStep1;
    public AudioClip metalFootStep2;
    public AudioClip metalFootStep3;
    public AudioClip metalFootStep4;
    public AudioClip missingItemsClip;
    public AudioClip buttonClip;
    public AudioClip craftingClip;

    public GameObject ping;
    public GameObject tablet;
    public GameObject scanner;
    public GameObject laserCannon;
    public GameObject paintGun;
    public GameObject paintGunTank;
    public GameObject adjustedPaintGunTank;
    public GameObject adjustedPaintGunTank2;
    public GameObject muzzleFlash;
    public GameObject scannerFlash;
    public GameObject weaponHit;
    public GameObject darkMatterCollector;
    public GameObject darkMatter;
    public GameObject darkMatterConduit;
    public GameObject ironBlock;
    public GameObject ironRamp;
    public GameObject steel;
    public GameObject steelRamp;
    public GameObject storageContainer;
    public GameObject universalExtractor;
    public GameObject auger;
    public GameObject airlock;
    public GameObject universalConduit;
    public GameObject glass;
    public GameObject brick;
    public GameObject electricLight;
    public GameObject smelter;
    public GameObject turret;
    public GameObject solarPanel;
    public GameObject generator;
    public GameObject reactorTurbine;
    public GameObject nuclearReactor;
    public GameObject press;
    public GameObject extruder;
    public GameObject retriever;
    public GameObject railCart;
    public GameObject railCartHub;
    public GameObject storageComputer;
    public GameObject autoCrafter;
    public GameObject heatExchanger;
    public GameObject gearCutter;
    public GameObject alloySmelter;
    public GameObject powerConduit;
    public GameObject playerBody;
    public GameObject builder;
    public GameObject headlamp;
    public GameObject guiObject;
    public GameObject currentStorageComputer;
    public GameObject buildObject;

    public Material constructionMat;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        //REFERENCE TO THE GAME AND STATE MANAGER
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();


        //INITIALIZE COMPONENT CLASSES
        playerInventory = GetComponent<InventoryManager>();
        laserController = new LaserController(this, gameManager);

        //LOAD MOUSE SETTINGS
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (FileBasedPrefs.initialized == true)
        {
            if (FileBasedPrefs.GetFloat("xSensitivity") != 0)
            {
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX = PlayerPrefs.GetFloat("xSensitivity");
            }
            if (FileBasedPrefs.GetFloat("ySensitivity") != 0)
            {
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY = PlayerPrefs.GetFloat("ySensitivity");
            }
            GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = PlayerPrefsX.GetPersistentBool("mouseInverted");
        }

        // Loading volume settings.
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;

        // Audio source for placing blocks.
        builderSound = builder.GetComponent<AudioSource>();

        // Audio source for GUI related sounds.
        guiSound = guiObject.GetComponent<AudioSource>();

        // Fog and Scanner color for atmospheric worlds.
        if (SceneManager.GetActiveScene().name.Equals("QE_World_Atmo"))
        {
            scannerFlash.GetComponent<Light>().color = Color.white;
            scannerFlash.GetComponent<Light>().intensity = 1;

            float fogDensity = PlayerPrefs.GetFloat("fogDensity");
            RenderSettings.fogDensity = fogDensity > 0 ? fogDensity : 0.00025f;
            RenderSettings.fog = PlayerPrefsX.GetPersistentBool("fogEnabled");
        }

        inputManager = new InputManager(this);
        blockSelector = new BlockSelector(this);
    }

    // Called once per frame by unity engine
    public void Update()
    {
        // Get a refrence to the camera.
        if (mCam == null)
        {
            mCam = Camera.main;
            if (FileBasedPrefs.initialized == true)
            {
                if (FileBasedPrefs.GetFloat("FOV") != 0)
                {
                    mCam.fieldOfView = PlayerPrefs.GetFloat("FOV");
                }

                if (FileBasedPrefs.GetFloat("drawDistance") != 0)
                {
                    mCam.farClipPlane = PlayerPrefs.GetFloat("drawDistance");
                }
            }
        }
        else
        {
            // Disable mouse look during main menu sequence.
            gameObject.GetComponent<MSCameraController>().enabled &= gameStarted != false;

            // Get the spawn location, for respawning the player character.
            if (gotPosition == false)
            {
                originalPosition = transform.position;
                gotPosition = true;
            }

            // The state manager has finished loading the world.
            if (stateManager.worldLoaded == true)
            {
                gameStarted = true;

                if (FileBasedPrefs.GetBool(stateManager.WorldName + "oldWorld") == false)
                {
                    OpenTabletOnFirstLoad();
                }

                if (movedPlayer == false && FileBasedPrefs.GetBool(stateManager.WorldName + "oldWorld") == true)
                {
                    MovePlayerToSavedLocation();
                }

                if (ShouldShowTabletIntro())
                {
                    ShowTabletIntro();
                }

                // Destruction messages.
                if (destructionMessageActive == true)
                {
                    if (destructionMessageCount > 10)
                    {
                        currentTabletMessage = "";
                        destructionMessageCount = 0;
                    }
                    if (destructionMessageReceived == false)
                    {
                        tablet.GetComponent<AudioSource>().Play();
                        destructionMessageReceived = true;
                    }
                }
                else
                {
                    destructionMessageCount = 0;
                    destructionMessageReceived = false;
                }

                //PIRATE ATTACK INCOMING
                if (pirateAttackWarningActive == true)
                {
                    currentTabletMessage = "Warning! Warning! Warning! Warning! Warning!\n\nIncoming pirate attack!";
                    if (pirateAttackWarningReceived == false)
                    {
                        tablet.GetComponent<AudioSource>().Play();
                        pirateAttackWarningReceived = true;
                    }
                }
                else
                {
                    pirateAttackWarningReceived = false;
                }

                //METEOR SHOWER INCOMING
                if (meteorShowerWarningActive == true)
                {
                    currentTabletMessage = "Warning! Warning! Warning! Warning! Warning!\n\nIncoming meteor shower!";
                    if (meteorShowerWarningReceived == false)
                    {
                        tablet.GetComponent<AudioSource>().Play();
                        meteorShowerWarningReceived = true;
                    }
                }
                else
                {
                    meteorShowerWarningReceived = false;
                }

                if (timeToDeliver == true)
                {
                    DisplayDeliveryWarning();
                }
                else
                {
                    timeToDeliverWarningRecieved = false;
                }

                if (destroying == true)
                {
                    ModifyCombinedMeshes();
                }

                if (requestedBuildingStop == true)
                {
                    HandleBuildingStopRequest();
                }

                // The player controller is notified that the game manager finished combining meshes.
                if (stoppingBuildCoRoutine == true && gameManager.working == false)
                {
                    stoppingBuildCoRoutine = false;
                }

                if (requestedChunkLoad == true)
                {
                    HandleChunkLoadRequest();
                }

                // Locking or unlocking the mouse cursor for GUI interaction.
                if (GuiOpen())
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    gameObject.GetComponent<MSCameraController>().enabled = false;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    gameObject.GetComponent<MSCameraController>().enabled = true;
                }

                if (requestedEscapeMenu == true)
                {
                    HandleEscapeMenuRequest();
                }

                if (requestedSave == true)
                {
                    HandleSaveRequest();
                }

                if (storageInventory != null && storageGUIopen == true)
                {
                    CheckStorageDistance();
                }

                // Input manager.
                inputManager.HandleInput();
                EnforceWorldLimits();
            }
        }
    }

    // Saves the player's location and money.
    public void SavePlayerData()
    {
        PlayerPrefsX.SetVector3(stateManager.WorldName + "playerPosition", transform.position);
        PlayerPrefsX.SetQuaternion(stateManager.WorldName + "playerRotation", transform.rotation);
        FileBasedPrefs.SetInt(stateManager.WorldName + "money", money);
        FileBasedPrefs.SetBool(stateManager.WorldName + "oldWorld", true);
    }

    // Applies global settings.
    public void ApplySettings()
    {
        PlayerPrefsX.SetPersistentBool("mouseInverted", GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput);
        PlayerPrefs.SetFloat("xSensitivity", GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX);
        PlayerPrefs.SetFloat("ySensitivity", GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY);
        PlayerPrefs.SetFloat("FOV", mCam.fieldOfView);
        PlayerPrefs.SetFloat("drawDistance", mCam.farClipPlane);
        PlayerPrefs.SetFloat("volume", GetComponent<MSCameraController>().cameras[0].volume);
        PlayerPrefsX.SetPersistentBool("fogEnabled", RenderSettings.fog);
        PlayerPrefs.SetFloat("fogDensity", RenderSettings.fogDensity);
        PlayerPrefs.Save();
    }

    // Plays a sound.
    public void PlayButtonSound()
    {
        guiSound.volume = 0.15f;
        guiSound.clip = buttonClip;
        guiSound.Play();
    }

    // Plays a sound.
    public void PlayCraftingSound()
    {
        guiSound.volume = 0.3f;
        guiSound.clip = craftingClip;
        guiSound.Play();
    }

    // Plays a sound.
    public void PlayMissingItemsSound()
    {
        guiSound.volume = 0.15f;
        guiSound.clip = missingItemsClip;
        guiSound.Play();
    }

    // Returns true if any GUI is open
    public bool GuiOpen()
    {
        return cGUI.showingInputGUI == true
        || inventoryOpen == true
        || escapeMenuOpen == true
        || machineGUIopen == true
        || tabletOpen == true
        || marketGUIopen == true
        || (paintGunActive == true && paintColorSelected == false);
    }

    // Returns true at the beginning of the first in-game day when the intro should be displayed on the tablet.
    private bool ShouldShowTabletIntro()
    {
        return currentTabletMessage.Equals("")
        && GameObject.Find("Rocket").GetComponent<Rocket>().day == 1
        && (int)GameObject.Find("Rocket").GetComponent<Rocket>().gameTime < 200;
    }

    // Displays the intro message on the tablet.
    private void ShowTabletIntro()
    {
        currentTabletMessage = "To all deployed members of:\nQuantum Engineering, \nDark Matter Research, \nMoon Base Establishment Division" +
        "\n\n" + "Manufacture of Quantum Manifestation Projection Units \nis going ahead as planned. " + "Our systems show all \nengineers have " +
        "successfully arrived at their \ndesignated FOB establishment locations. " + "\nExpected return payload to planetary facilities \nat the end " +
        "of day 1 is currently 50 units \nof non-anomylous dark matter as planned. " + "\n\n" + "As always, thank you for your service." + "\n" +
        "Agrat Eirelis:" + "\n" + "Chief Officer," + "\n" + "Quantum Engineering," + "\n" + "Dark Matter Research," + "\n" + "Moon Base Establishment Division";
    }

    // Moves the player to their previous location when a game is loaded.
    private void MovePlayerToSavedLocation()
    {
        transform.position = PlayerPrefsX.GetVector3(stateManager.WorldName + "playerPosition");
        transform.rotation = PlayerPrefsX.GetQuaternion(stateManager.WorldName + "playerRotation");
        money = FileBasedPrefs.GetInt(stateManager.WorldName + "money");
        movedPlayer = true;
    }

    // Opens the tablet to display a message the first time the world is loaded.
    private void OpenTabletOnFirstLoad() 
    {
        if (GetComponent<MainMenu>().finishedLoading == true)
        {
            if (introDisplayed == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                tabletOpen = true;
                GetComponent<MSCameraController>().cameras[0].volume = 2.5f;
                introDisplayed = true;
            }
        }
    }

    // Displays a message on the player's tablet when the rocket is landing to collect dark matter.
    private void DisplayDeliveryWarning()
    {
        deliveryWarningTimer += 1 * Time.deltaTime;
        if (deliveryWarningTimer > 30)
        {
            deliveryWarningTimer = 0;
        }

        int payload = GameObject.Find("Rocket").GetComponent<Rocket>().payloadRequired;

        currentTabletMessage = "Please load: " + payload + " dark matter\ninto the rocket near the lunar lander."
        + "\nExpected return payload to planetary facilities \nat the end " +
        "of the day tomorrow is currently:" + "\n" + (payload * 2) + " units of dark matter. "
        + "\n\n" + "As always, Thank you for your service." + "\n" +
        "Agrat Eirelis:" + "\n" + "Chief Officer," + "\n" + "Quantum Engineering," + "\n"
        + "Dark Matter Research," + "\n" + "Moon Base Establishment Division";

        if (timeToDeliverWarningRecieved == false)
        {
            tablet.GetComponent<AudioSource>().Play();
            timeToDeliverWarningRecieved = true;
        }
    }

    private void HandleBuildingStopRequest()
    {
        if (gameManager.working == false)
        {
            stoppingBuildCoRoutine = true;
            gameManager.meshManager.CombineBlocks();
            separatedBlocks = false;
            destroyTimer = 0;
            buildTimer = 0;
            building = false;
            destroying = false;
            requestedBuildingStop = false;
        }
        else
        {
            requestedBuildingStop = true;
        }
    }

    // Handles the sending of chunk load requests for modifying combined meshes.
    private void ModifyCombinedMeshes()
    {
        destroyTimer += 1 * Time.deltaTime;
        if (destroyTimer >= 30)
        {
            if (gameManager.working == false)
            {
                stoppingBuildCoRoutine = true;
                gameManager.meshManager.CombineBlocks();
                destroyTimer = 0;
                buildTimer = 0;
                building = false;
                destroying = false;
                separatedBlocks = false;
            }
            else
            {
                requestedBuildingStop = true;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, destroyStartPosition);
            if (distance > 15)
            {
                if (gameManager.working == false)
                {
                    gameManager.meshManager.SeparateBlocks(transform.position, "all", true);
                    separatedBlocks = true;
                }
                else
                {
                    requestedChunkLoad = true;
                }
                destroyStartPosition = transform.position;
            }
        }
    }

    // Handles requests to load chunks of blocks from combined meshes near the player.
    private void HandleChunkLoadRequest()
    {
        if (gameManager.working == false)
        {
            if (destroying == false)
            {
                gameManager.meshManager.SeparateBlocks(transform.position, "all", true);
            }
            else
            {
                gameManager.meshManager.SeparateBlocks(transform.position, "all", false);
            }
            separatedBlocks = true;
            requestedChunkLoad = false;
        }
    }

    // Handles requests to open the escape menu, stopping appropriate coroutines.
    private void HandleEscapeMenuRequest()
    {
        if (building == true || destroying == true)
        {
            if (gameManager.working == false)
            {
                stoppingBuildCoRoutine = true;
                gameManager.meshManager.CombineBlocks();
                separatedBlocks = false;
                destroyTimer = 0;
                buildTimer = 0;
                building = false;
                destroying = false;
            }
            else
            {
                requestedBuildingStop = true;
            }
        }
        else if (gameManager.working == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<MSCameraController>().enabled = false;
            escapeMenuOpen = true;
            requestedEscapeMenu = false;
        }
    }

    // Enforces world size limitations.
    private void EnforceWorldLimits()
    {
        if (gameObject.transform.position.x > 4500)
        {
            gameObject.transform.position = new Vector3(4490, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.z > 4500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4490);
        }
        if (gameObject.transform.position.x < -4500)
        {
            gameObject.transform.position = new Vector3(-4490, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.z < -4500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4490);
        }
        if (gameObject.transform.position.y > 500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 490, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.y < -100)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 500, gameObject.transform.position.z);
        }
    }

    // Handles requests to exit the game
    private void HandleSaveRequest()
    {
        requestedSaveTimer += 1 * Time.deltaTime;
        if (requestedSaveTimer >= 5)
        {
            if (gameManager.working == false)
            {
                SavePlayerData();
                GameObject.Find("GameManager").GetComponent<GameManager>().RequestSaveOperation();
                saveCoroutine = StartCoroutine(Save());
                requestedSaveTimer = 0;
                requestedSave = false;
            }
        }
    }

    // Closes the storage GUI if the player has moved too far from the container.
    private void CheckStorageDistance()
    {
        if (remoteStorageActive == false)
        {
            float distance = Vector3.Distance(transform.position, storageInventory.gameObject.transform.position);
            if (distance > 40)
            {
                storageGUIopen = false;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, currentStorageComputer.transform.position);
            if (distance > 40)
            {
                storageGUIopen = false;
            }
        }
    }

    // Handles safely saving data and shutting down the game.
    public static IEnumerator Save()
    {
        float f = 0;
        while (f < 6000)
        {
            f++;
            if (GameObject.Find("GameManager").GetComponent<GameManager>().blocksCombined == false)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().dataSaveRequested == false)
                {
                    if (GameObject.Find("GameManager").GetComponent<StateManager>().saving == false)
                    {
                        Debug.Log("Creating backup...");
                        string fileName = GameObject.Find("GameManager").GetComponent<StateManager>().WorldName;
                        string destinationPath = Path.Combine(Application.persistentDataPath, "SaveData/" + fileName + ".bak");
                        File.Copy(FileBasedPrefs.GetSaveFilePath(), destinationPath, true);

                        if (GameObject.Find("Player").GetComponent<PlayerController>().exiting == true)
                        {
                            Debug.Log("Loading main menu...");
                            SceneManager.LoadScene(0);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}