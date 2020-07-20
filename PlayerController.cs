using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Coroutine quitCoroutine;
    private Vector3 originalPosition;
    private LaserController laserController;

    public Vector3 destroyStartPosition;
    public Vector3 buildStartPosition;
    public RaycastHit playerLookHit;
    public StateManager stateManager;
    public Camera mCam;
    public InventoryManager playerInventory;
    public InventoryManager storageInventory;
    public InventorySlot slotDraggingFrom;
    public AudioSource builderSound;
    public AudioSource guiSound;
    public GameObject objectInSight;
    public GameObject machineInSight;

    public bool outOfSpace;
    public bool cannotCollect;
    public bool building;
    public bool inventoryOpen;
    public bool storageGUIopen;
    public bool machineGUIopen;
    public bool craftingGUIopen;
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
    public bool requestedExit;
    public bool invalidAugerPlacement;
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
    public bool backupCreated;
    public bool crosshairEnabled = true;
    public bool stoppingBuildCoRoutine;
    public bool separatedBlocks;
    public bool destroying;
    public bool requestedChunkLoad;
    public bool blockLimitMessage;

    private bool gotPosition;
    private bool gameStarted;
    private bool laserCannonActive;
    private bool scannerActive;
    private bool firing;
    private bool scanning;
    private bool meteorShowerWarningReceived;
    private bool pirateAttackWarningReceived;
    private bool destructionMessageReceived;
    private bool movedPlayer;
    private bool introDisplayed;
    private bool requestedEscapeMenu;

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
    public float invalidRailCartPlacementTimer;
    public float paintRed;
    public float paintGreen;
    public float paintBlue;
    public float requestedExitTimer;
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

    void Start()
    {
        //REFERENCE TO THE STATE MANAGER
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();

        //INITIALIZE COMPONENT CLASSES
        playerInventory = GetComponent<InventoryManager>();
        laserController = GetComponent<LaserController>();

        //LOAD MOUSE SETTINGS
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.GetFloat("xSensitivity") != 0)
        {
            GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX = PlayerPrefs.GetFloat("xSensitivity");
        }
        if (PlayerPrefs.GetFloat("ySensitivity") != 0)
        {
            GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY = PlayerPrefs.GetFloat("ySensitivity");
        }
        GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = PlayerPrefsX.GetBool("mouseInverted");

        //LOAD VOLUME SETTING
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;

        //AUDIO SOURCE FOR PLACING BLOCKS
        builderSound = builder.GetComponent<AudioSource>();

        //AUDIO SOURCE FOR GUI RELATED SOUNDS
        guiSound = guiObject.GetComponent<AudioSource>();

        //SCANNER LIGHT COLOR DIFFERS DEPENDING ON THE SCENE
        if (SceneManager.GetActiveScene().name.Equals("QE_World_Atmo"))
        {
            scannerFlash.GetComponent<Light>().color = Color.white;
            scannerFlash.GetComponent<Light>().intensity = 1;
        }
    }

    void Update()
    {
        //DISABLE MOUSE LOOK DURING MAIN MENU SEQUENCE
        if (gameStarted == false)
        {
            gameObject.GetComponent<MSCameraController>().enabled = false;
        }

        //GET A REFERENCE TO THE MAIN CAMERA
        if (mCam == null)
        {
            mCam = Camera.main;
            if (PlayerPrefs.GetFloat("FOV") != 0)
            {
                mCam.fieldOfView = PlayerPrefs.GetFloat("FOV");
            }
            if (PlayerPrefs.GetFloat("DrawDistance") != 0)
            {
                mCam.farClipPlane = PlayerPrefs.GetFloat("DrawDistance");
            }
        }

        //GET THE SPAWN LOCATION, FOR RESPAWNING THE PLAYER
        if (gotPosition == false)
        {
            originalPosition = transform.position;
            gotPosition = true;
        }

        //THE WORLD STATE MANAGER HAS FINISHED LOADING
        if (stateManager.worldLoaded == true)
        {
            gameStarted = true;

            //IF THIS IS A NEW WORLD, OPEN THE TABLET TO DISPLAY THE INTRO MESSAGE
            if (PlayerPrefsX.GetBool(stateManager.WorldName + "oldWorld") == false)
            {
                if (GetComponent<Main_Menu>().finishedLoading == true)
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

            if (movedPlayer == false && PlayerPrefsX.GetBool(stateManager.WorldName+"oldWorld") == true)
            {
                transform.position = PlayerPrefsX.GetVector3(stateManager.WorldName+"playerPosition");
                transform.rotation = PlayerPrefsX.GetQuaternion(stateManager.WorldName + "playerRotation");
                money = PlayerPrefs.GetInt(stateManager.WorldName + "money");
                movedPlayer = true;
            }

            //MAIN CAMERA WAS FOUND IN THE SCENE
            if (mCam != null)
            {
                //IF NOTHING ELSE HAS HAPPENED YET, DISPLAY THE INTRO MESSAGE WHEN THE TABLET IS OPENED
                if (currentTabletMessage.Equals("") && GameObject.Find("Rocket").GetComponent<Rocket>().day == 1 && (int)GameObject.Find("Rocket").GetComponent<Rocket>().gameTime < 200)
                {
                    currentTabletMessage = "To all deployed members of:\nQuantum Engineering, \nDark Matter Research, \nMoon Base Establishment Division" +
                        "\n\n" + "Manufacture of Quantum Manifestation Projection Units \nis going ahead as planned. " + "Our systems show all \nengineers have " +
                        "successfully arrived at their \ndesignated FOB establishment locations. " + "\nExpected return payload to planetary facilities \nat the end " +
                        "of day 1 is currently 50 units \nof non-anomylous dark matter as planned. " + "\n\n" + "As always, thank you for your service." + "\n" +
                        "Agrat Eirelis:" + "\n" + "Chief Officer," + "\n" + "Quantum Engineering," + "\n" + "Dark Matter Research," + "\n" + "Moon Base Establishment Division";
                }

                //DESTRUCTION MESSAGES
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

                //ROCKET IS COMING TO COLLECT MATERIAL FROM THE PLAYER
                if (timeToDeliver == true)
                {
                    deliveryWarningTimer += 1 * Time.deltaTime;
                    if (deliveryWarningTimer > 30)
                    {
                        deliveryWarningTimer = 0;
                    }
                    int payload = GameObject.Find("Rocket").GetComponent<Rocket>().payloadRequired;
                    currentTabletMessage = "Please load: "+payload+" dark matter\ninto the rocket near the lunar lander." 
                        + "\nExpected return payload to planetary facilities \nat the end " +
                        "of the day tomorrow is currently:" + "\n" + (payload*2) + " units of dark matter. "
                         + "\n\n" + "As always, Thank you for your service." + "\n" +
                        "Agrat Eirelis:" + "\n" + "Chief Officer," + "\n" + "Quantum Engineering," + "\n" 
                         + "Dark Matter Research," + "\n" + "Moon Base Establishment Division";
                    if (timeToDeliverWarningRecieved == false)
                    {
                        tablet.GetComponent<AudioSource>().Play();
                        timeToDeliverWarningRecieved = true;
                    }
                }
                else
                {
                    timeToDeliverWarningRecieved = false;
                }

                //CROSSHAIR
                if (cInput.GetKeyDown("Crosshair") && exiting == false)
                {
                    if (crosshairEnabled == true)
                    {
                        crosshairEnabled = false;
                    }
                    else
                    {
                        crosshairEnabled = true;
                    }
                    guiSound.volume = 0.15f;
                    guiSound.clip = buttonClip;
                    guiSound.Play();
                }

                //SPRINTING
                if (cInput.GetKey("Sprint") && exiting == false)
                {
                    playerMoveSpeed = 25;
                    footStepSoundFrquency = 0.25f;
                }
                else
                {
                    if (!Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10))
                    {
                        playerMoveSpeed = 25;
                        footStepSoundFrquency = 0.25f;
                    }
                    else
                    {
                        playerMoveSpeed = 15;
                        footStepSoundFrquency = 0.5f;
                    }
                }

                if (cInput.GetKeyDown("Sprint") || cInput.GetKeyUp("Sprint"))
                {
                    if (scannerActive == true)
                    {
                        scanner.GetComponent<HeldItemSway>().Reset();
                    }
                    if (laserCannonActive == true)
                    {
                        laserCannon.GetComponent<HeldItemSway>().Reset();
                    }
                    if (paintGunActive == true)
                    {
                        paintGun.GetComponent<HeldItemSway>().Reset();
                    }
                }

                //MOVEMENT INPUT
                if (exiting == false)
                {
                    if (cInput.GetKey("Walk Forward"))
                    {
                        if (!Physics.Raycast(mCam.gameObject.transform.position, mCam.gameObject.transform.forward, out RaycastHit hit, 5))
                        {
                            Vector3 moveDir = Vector3.Normalize(new Vector3(mCam.gameObject.transform.forward.x, 0, mCam.gameObject.transform.forward.z));
                            gameObject.transform.position += moveDir * playerMoveSpeed * Time.deltaTime;
                        }
                        else if (hit.collider.gameObject.GetComponent<AirLock>() != null)
                        {
                            if (hit.collider.gameObject.GetComponent<Collider>().isTrigger == true)
                            {
                                Vector3 moveDir = Vector3.Normalize(new Vector3(mCam.gameObject.transform.forward.x, 0, mCam.gameObject.transform.forward.z));
                                gameObject.transform.position += moveDir * playerMoveSpeed * Time.deltaTime;
                            }
                        }
                    }
                    if (cInput.GetKey("Walk Backward"))
                    {
                        if (!Physics.Raycast(mCam.gameObject.transform.position, -mCam.gameObject.transform.forward, out RaycastHit hit, 5))
                        {
                            Vector3 moveDir = Vector3.Normalize(new Vector3(mCam.gameObject.transform.forward.x, 0, mCam.gameObject.transform.forward.z));
                            gameObject.transform.position -= moveDir * playerMoveSpeed * Time.deltaTime;
                        }
                    }
                    if (cInput.GetKey("Strafe Left"))
                    {
                        if (!Physics.Raycast(mCam.gameObject.transform.position, -mCam.gameObject.transform.right, out RaycastHit hit, 5))
                        {
                            gameObject.transform.position -= mCam.gameObject.transform.right * playerMoveSpeed * Time.deltaTime;
                        }
                    }
                    if (cInput.GetKey("Strafe Right"))
                    {
                        if (!Physics.Raycast(mCam.gameObject.transform.position, mCam.gameObject.transform.right, out RaycastHit hit, 5))
                        {
                            gameObject.transform.position += mCam.gameObject.transform.right * playerMoveSpeed * Time.deltaTime;
                        }
                    }
                }

                if (!cInput.GetKey("Jetpack") && cInput.GetKey("Walk Forward") || cInput.GetKey("Walk Backward") || cInput.GetKey("Strafe Left") || cInput.GetKey("Strafe Right"))
                {
                    if (exiting == false && Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10))
                    {
                        //HEAD BOB
                        mCam.GetComponent<HeadBob>().active = true;

                        //HELD OBJECT MOVEMENT
                        if (GetComponent<AudioSource>().isPlaying == true)
                        {
                            GetComponent<AudioSource>().Stop();
                        }
                        if (scannerActive == true)
                        {
                            scanner.GetComponent<HeldItemSway>().active = true;
                        }
                        else
                        {
                            scanner.GetComponent<HeldItemSway>().active = false;
                        }
                        if (laserCannonActive == true)
                        {
                            laserCannon.GetComponent<HeldItemSway>().active = true;
                        }
                        else
                        {
                            laserCannon.GetComponent<HeldItemSway>().active = false;
                        }
                        if (paintGunActive == true)
                        {
                            paintGun.GetComponent<HeldItemSway>().active = true;
                        }
                        else
                        {
                            paintGun.GetComponent<HeldItemSway>().active = false;
                        }

                        //FOOTSTEP SOUNDS
                        footStepTimer += 1 * Time.deltaTime;
                        if (footStepTimer >= footStepSoundFrquency)
                        {
                            footStepTimer = 0;
                            if (hit.collider.gameObject.GetComponent<IronBlock>() != null || hit.collider.gameObject.GetComponent<Steel>() != null || hit.collider.gameObject.name.Equals("ironHolder(Clone)") || hit.collider.gameObject.name.Equals("steelHolder(Clone)"))
                            {
                                if (playerBody.GetComponent<AudioSource>().clip == footStep1 || playerBody.GetComponent<AudioSource>().clip == footStep2 || playerBody.GetComponent<AudioSource>().clip == footStep3 || playerBody.GetComponent<AudioSource>().clip == footStep4 || playerBody.GetComponent<AudioSource>().clip == metalFootStep1)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = metalFootStep2;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == metalFootStep2)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = metalFootStep3;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == metalFootStep3)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = metalFootStep4;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == metalFootStep4)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = metalFootStep1;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                            }
                            else
                            {
                                if (playerBody.GetComponent<AudioSource>().clip == footStep1 || playerBody.GetComponent<AudioSource>().clip == metalFootStep1 || playerBody.GetComponent<AudioSource>().clip == metalFootStep2 || playerBody.GetComponent<AudioSource>().clip == metalFootStep3 || playerBody.GetComponent<AudioSource>().clip == metalFootStep4)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = footStep2;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == footStep2)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = footStep3;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == footStep3)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = footStep4;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                                else if (playerBody.GetComponent<AudioSource>().clip == footStep4)
                                {
                                    playerBody.GetComponent<AudioSource>().clip = footStep1;
                                    playerBody.GetComponent<AudioSource>().Play();
                                }
                            }
                        }
                    }
                    else if (exiting == false && GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().Play();
                        mCam.GetComponent<HeadBob>().active = false;
                        scanner.GetComponent<HeldItemSway>().active = false;
                        laserCannon.GetComponent<HeldItemSway>().active = false;
                        paintGun.GetComponent<HeldItemSway>().active = false;
                    }
                }
                else if (!cInput.GetKey("Jetpack") && !cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
                {
                    GetComponent<AudioSource>().Stop();
                    mCam.GetComponent<HeadBob>().active = false;
                    scanner.GetComponent<HeldItemSway>().active = false;
                    laserCannon.GetComponent<HeldItemSway>().active = false;
                    paintGun.GetComponent<HeldItemSway>().active = false;
                }
                else
                {
                    mCam.GetComponent<HeadBob>().active = false;
                    scanner.GetComponent<HeldItemSway>().active = false;
                    laserCannon.GetComponent<HeldItemSway>().active = false;
                    paintGun.GetComponent<HeldItemSway>().active = false;
                }

                //WEAPON SELECTION
                if (inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false && building == false)
                {
                    if (cInput.GetKeyDown("Paint Gun"))
                    {
                        if (paintGunActive == false)
                        {
                            paintGunActive = true;
                            paintGun.SetActive(true);
                            laserCannon.SetActive(false);
                            laserCannonActive = false;
                            scanner.SetActive(false);
                            scannerActive = false;
                        }
                        else
                        {
                            paintGun.SetActive(false);
                            paintGunActive = false;
                            paintColorSelected = false;
                        }
                    }
                    if (paintGunActive == true)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            paintGun.SetActive(false);
                            paintGunActive = false;
                            paintColorSelected = false;
                        }
                    }
                    if (cInput.GetKeyDown("Paint Color"))
                    {
                        if (paintGunActive == true && paintColorSelected == true)
                        {
                            paintColorSelected = false;
                        }
                    }
                    if (cInput.GetKeyDown("Scanner"))
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        if (!scanner.activeSelf)
                        {
                            paintGun.SetActive(false);
                            paintGunActive = false;
                            paintColorSelected = false;
                            laserCannon.SetActive(false);
                            laserCannonActive = false;
                            scanner.SetActive(true);
                            scannerActive = true;
                        }
                        else
                        {
                            scanner.SetActive(false);
                            scannerActive = false;
                        }
                    }
                    if (cInput.GetKeyDown("Laser Cannon"))
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        if (!laserCannon.activeSelf)
                        {
                            paintGun.SetActive(false);
                            paintGunActive = false;
                            paintColorSelected = false;
                            scanner.SetActive(false);
                            scannerActive = false;
                            laserCannon.SetActive(true);
                            laserCannonActive = true;
                        }
                        else
                        {
                            laserCannon.SetActive(false);
                            laserCannonActive = false;
                        }
                    }
                }
                else
                {
                    laserCannon.SetActive(false);
                    laserCannonActive = false;
                    scanner.SetActive(false);
                    scannerActive = false;
                }

                //FIRING THE LASER CANNON OR THE SCANNER
                if (cInput.GetKeyDown("Fire") && exiting == false)
                {
                    if (laserCannonActive == true && inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false)
                    {
                        if (firing == false)
                        {
                            firing = true;
                            laserCannon.GetComponent<AudioSource>().Play();
                            muzzleFlash.SetActive(true);
                            if (Physics.Raycast(mCam.gameObject.transform.position, mCam.gameObject.transform.forward, out RaycastHit hit, 1000))
                            {
                                Instantiate(weaponHit, hit.point, transform.rotation);
                                laserController.HitTarget(hit.collider.gameObject,hit);
                            }
                        }
                    }

                    if (scannerActive == true && inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false)
                    {
                        if (scanning == false)
                        {
                            scanning = true;
                            scanner.GetComponent<AudioSource>().Play();
                            scannerFlash.SetActive(true);
                            GameObject[] allObjects = FindObjectsOfType<GameObject>();
                            foreach (GameObject obj in allObjects)
                            {
                                float distance = Vector3.Distance(transform.position, obj.transform.position);
                                if (distance < 2000)
                                {
                                    if (obj.GetComponent<UniversalResource>() != null)
                                    {
                                        GameObject newPing = Instantiate(ping, new Vector3(obj.transform.position.x, obj.transform.position.y + 15, obj.transform.position.z), transform.rotation);
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
                                        GameObject newPing = Instantiate(ping, new Vector3(obj.transform.position.x, obj.transform.position.y + 15, obj.transform.position.z), transform.rotation);
                                        newPing.GetComponent<Ping>().type = "darkMatter";
                                    }
                                }
                            }
                        }
                    }
                }
                if (firing == true)
                {
                    if (!laserCannon.GetComponent<AudioSource>().isPlaying)
                    {
                        muzzleFlash.SetActive(false);
                        firing = false;
                    }
                }
                if (scanning == true)
                {
                    if (!scanner.GetComponent<AudioSource>().isPlaying)
                    {
                        scannerFlash.SetActive(false);
                        scanning = false;
                    }
                }

                //HEADLAMP
                if (cInput.GetKeyDown("Headlamp") && exiting == false)
                {
                    if (headlamp.GetComponent<Light>()!= null)
                    {
                        if (headlamp.GetComponent<Light>().enabled == true)
                        {
                            headlamp.GetComponent<Light>().enabled = false;
                        }
                        else
                        {
                            headlamp.GetComponent<Light>().enabled = true;
                        }
                    }
                    guiSound.volume = 0.15f;
                    guiSound.clip = buttonClip;
                    guiSound.Play();
                }

                //JETPACK
                if (cInput.GetKey("Jetpack") && exiting == false)
                {
                    if (GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().Play();
                    }
                    if (gameObject.transform.position.y < 500 && !Physics.Raycast(transform.position, transform.up, out RaycastHit upHit, 5))
                    {
                        gameObject.transform.position += Vector3.up * 25 * Time.deltaTime;
                    }
                    mCam.GetComponent<HeadBob>().active = false;
                    scanner.GetComponent<HeldItemSway>().active = false;
                    laserCannon.GetComponent<HeldItemSway>().active = false;
                    paintGun.GetComponent<HeldItemSway>().active = false;
                }
                else if (!cInput.GetKey("Walk Forward") && !cInput.GetKey("Walk Backward") && !cInput.GetKey("Strafe Left") && !cInput.GetKey("Strafe Right"))
                {
                    if (GetComponent<AudioSource>().isPlaying == true)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                }

                //REMOVING BLOCKS
                if (destroying == true)
                {
                    destroyTimer += 1 * Time.deltaTime;
                    if (destroyTimer >= 30)
                    {
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                        {
                            stoppingBuildCoRoutine = true;
                            GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                            //Debug.Log("new chunk loaded");
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all",true);
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

                //STOPPING CONSTRUCTION COROUTINES
                if (requestedBuildingStop == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        stoppingBuildCoRoutine = true;
                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                if (stoppingBuildCoRoutine == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        stoppingBuildCoRoutine = false;
                    }
                }

                //CHUNK LOADING
                if (requestedChunkLoad == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        if (destroying == false)
                        {
                            GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all", true);
                        }
                        else
                        {
                            GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all", false);
                        }
                        separatedBlocks = true;
                        requestedChunkLoad = false;
                    }
                }

                //BUILD MULTIPLIER
                if (building == true)
                {
                    if (buildType.Equals("Glass Block") || buildType.Equals("Brick") || buildType.Equals("Iron Block") || buildType.Equals("Steel Block") || buildType.Equals("Steel Ramp") || buildType.Equals("Iron Ramp"))
                    {
                        if (cInput.GetKey("Build Amount +"))
                        {
                            if (buildMultiplier < 100)
                            {
                                buildIncrementTimer += 1 * Time.deltaTime;
                                if (buildIncrementTimer >= 0.1f)
                                {
                                    buildMultiplier += 1;
                                    guiSound.volume = 0.15f;
                                    guiSound.clip = buttonClip;
                                    guiSound.Play();
                                    destroyTimer = 0;
                                    buildTimer = 0;
                                    buildIncrementTimer = 0;
                                }
                            }
                        }
                        if (cInput.GetKey("Build Amount  -"))
                        {
                            if (buildMultiplier > 1)
                            {
                                buildIncrementTimer += 1 * Time.deltaTime;
                                if (buildIncrementTimer >= 0.1f)
                                {
                                    buildMultiplier -= 1;
                                    guiSound.volume = 0.15f;
                                    guiSound.clip = buttonClip;
                                    guiSound.Play();
                                    destroyTimer = 0;
                                    buildTimer = 0;
                                    buildIncrementTimer = 0;
                                }
                            }
                        }
                    }
                }

                //SELECTING CURRENT BLOCK TO BUILD WITH
                if (escapeMenuOpen == false && inventoryOpen == false && optionsGUIopen == false && machineGUIopen == false)
                {
                    if (cInput.GetKeyDown("Next Item"))
                    {
                        //Debug.Log("Build type: " + buildType);
                        if (buildType.Equals("Glass Block"))
                        {
                            buildType = "Brick";
                            previousBuildType = "Glass Block";
                            nextBuildType = "Iron Block";
                        }
                        else if (buildType.Equals("Brick"))
                        {
                            buildType = "Iron Block";
                            previousBuildType = "Brick";
                            nextBuildType = "Iron Ramp";
                        }
                        else if (buildType.Equals("Iron Block"))
                        {
                            buildType = "Iron Ramp";
                            previousBuildType = "Iron Block";
                            nextBuildType = "Steel Block";
                        }
                        else if (buildType.Equals("Iron Ramp"))
                        {
                            buildType = "Steel Block";
                            previousBuildType = "Iron Ramp";
                            nextBuildType = "Steel Ramp";
                        }
                        else if (buildType.Equals("Steel Block"))
                        {
                            buildType = "Steel Ramp";
                            previousBuildType = "Steel Block";
                            nextBuildType = "Quantum Hatchway";
                        }
                        else if (buildType.Equals("Steel Ramp"))
                        {
                            buildType = "Quantum Hatchway";
                            previousBuildType = "Steel Ramp";
                            nextBuildType = "Storage Container";
                        }
                        else if (buildType.Equals("Quantum Hatchway"))
                        {
                            buildType = "Storage Container";
                            previousBuildType = "Quantum Hatchway";
                            nextBuildType = "Storage Computer";
                        }
                        else if (buildType.Equals("Storage Container"))
                        {
                            buildType = "Storage Computer";
                            previousBuildType = "Storage Container";
                            nextBuildType = "Electric Light";
                        }
                        else if (buildType.Equals("Storage Computer"))
                        {
                            buildType = "Electric Light";
                            previousBuildType = "Storage Computer";
                            nextBuildType = "Auger";
                        }
                        else if (buildType.Equals("Electric Light"))
                        {
                            buildType = "Auger";
                            previousBuildType = "Electric Light";
                            nextBuildType = "Extruder";
                        }
                        else if (buildType.Equals("Auger"))
                        {
                            buildType = "Extruder";
                            previousBuildType = "Auger";
                            nextBuildType = "Press";
                        }
                        else if (buildType.Equals("Extruder"))
                        {
                            buildType = "Press";
                            previousBuildType = "Extruder";
                            nextBuildType = "Smelter";
                        }
                        else if (buildType.Equals("Press"))
                        {
                            buildType = "Smelter";
                            previousBuildType = "Press";
                            nextBuildType = "Universal Conduit";
                        }
                        else if (buildType.Equals("Smelter"))
                        {
                            buildType = "Universal Conduit";
                            previousBuildType = "Smelter";
                            nextBuildType = "Retriever";
                        }
                        else if (buildType.Equals("Universal Conduit"))
                        {
                            buildType = "Retriever";
                            previousBuildType = "Universal Conduit";
                            nextBuildType = "Rail Cart Hub";
                        }
                        else if (buildType.Equals("Retriever"))
                        {
                            buildType = "Rail Cart Hub";
                            previousBuildType = "Retriever";
                            nextBuildType = "Rail Cart";
                        }
                        else if (buildType.Equals("Rail Cart Hub"))
                        {
                            buildType = "Rail Cart";
                            previousBuildType = "Rail Cart Hub";
                            nextBuildType = "Universal Extractor";
                        }
                        else if (buildType.Equals("Rail Cart"))
                        {
                            buildType = "Universal Extractor";
                            previousBuildType = "Rail Cart";
                            nextBuildType = "Solar Panel";
                        }
                        else if (buildType.Equals("Universal Extractor"))
                        {
                            buildType = "Solar Panel";
                            previousBuildType = "Universal Extractor";
                            nextBuildType = "Generator";
                        }
                        else if (buildType.Equals("Solar Panel"))
                        {
                            buildType = "Generator";
                            previousBuildType = "Solar Panel";
                            nextBuildType = "Nuclear Reactor";
                        }
                        else if (buildType.Equals("Generator"))
                        {
                            buildType = "Nuclear Reactor";
                            previousBuildType = "Generator";
                            nextBuildType = "Reactor Turbine";
                        }
                        else if (buildType.Equals("Nuclear Reactor"))
                        {
                            buildType = "Reactor Turbine";
                            previousBuildType = "Nuclear Reactor";
                            nextBuildType = "Power Conduit";
                        }
                        else if (buildType.Equals("Reactor Turbine"))
                        {
                            buildType = "Power Conduit";
                            previousBuildType = "Reactor Turbine";
                            nextBuildType = "Heat Exchanger";
                        }
                        else if (buildType.Equals("Power Conduit"))
                        {
                            buildType = "Heat Exchanger";
                            previousBuildType = "Power Conduit";
                            nextBuildType = "Alloy Smelter";
                        }
                        else if (buildType.Equals("Heat Exchanger"))
                        {
                            buildType = "Alloy Smelter";
                            previousBuildType = "Heat Exchanger";
                            nextBuildType = "Gear Cutter";
                        }
                        else if (buildType.Equals("Alloy Smelter"))
                        {
                            buildType = "Gear Cutter";
                            previousBuildType = "Alloy Smelter";
                            nextBuildType = "Auto Crafter";
                        }
                        else if (buildType.Equals("Gear Cutter"))
                        {
                            buildType = "Auto Crafter";
                            previousBuildType = "Gear Cutter";
                            nextBuildType = "Dark Matter Conduit";
                        }
                        else if (buildType.Equals("Auto Crafter"))
                        {
                            buildType = "Dark Matter Conduit";
                            previousBuildType = "Auto Crafter";
                            nextBuildType = "Dark Matter Collector";
                        }
                        else if (buildType.Equals("Dark Matter Conduit"))
                        {
                            buildType = "Dark Matter Collector";
                            previousBuildType = "Dark Matter Conduit";
                            nextBuildType = "Turret";
                        }
                        else if (buildType.Equals("Dark Matter Collector"))
                        {
                            buildType = "Turret";
                            previousBuildType = "Dark Matter Collector";
                            nextBuildType = "Glass Block";
                        }
                        else if (buildType.Equals("Turret"))
                        {
                            buildType = "Glass Block";
                            previousBuildType = "Turret";
                            nextBuildType = "Iron Block";
                        }
                        displayingBuildItem = true;
                        buildItemDisplayTimer = 0;
                        guiSound.volume = 0.15f;
                        guiSound.clip = buttonClip;
                        guiSound.Play();
                        destroyTimer = 0;
                        buildTimer = 0;
                    }

                    if (cInput.GetKeyDown("Previous Item"))
                    {
                        //Debug.Log("Build type: " + buildType);
                        if (buildType.Equals("Turret"))
                        {
                            buildType = "Dark Matter Collector";
                            previousBuildType = "Dark Matter Conduit";
                            nextBuildType = "Turret";
                        }
                        else if (buildType.Equals("Dark Matter Collector"))
                        {
                            buildType = "Dark Matter Conduit";
                            previousBuildType = "Auto Crafter";
                            nextBuildType = "Dark Matter Collector";
                        }
                        else if (buildType.Equals("Dark Matter Conduit"))
                        {
                            buildType = "Auto Crafter";
                            previousBuildType = "Gear Cutter";
                            nextBuildType = "Dark Matter Conduit";
                        }
                        else if (buildType.Equals("Auto Crafter"))
                        {
                            buildType = "Gear Cutter";
                            previousBuildType = "Alloy Smelter";
                            nextBuildType = "Auto Crafter";
                        }
                        else if (buildType.Equals("Gear Cutter"))
                        {
                            buildType = "Alloy Smelter";
                            previousBuildType = "Heat Exchanger";
                            nextBuildType = "Gear Cutter";
                        }
                        else if (buildType.Equals("Alloy Smelter"))
                        {
                            buildType = "Heat Exchanger";
                            previousBuildType = "Power Conduit";
                            nextBuildType = "Alloy Smelter";
                        }
                        else if (buildType.Equals("Heat Exchanger"))
                        {
                            buildType = "Power Conduit";
                            previousBuildType = "Reactor Turbine";
                            nextBuildType = "Heat Exchanger";
                        }
                        else if (buildType.Equals("Power Conduit"))
                        {
                            buildType = "Reactor Turbine";
                            previousBuildType = "Nuclear Reactor";
                            nextBuildType = "Power Conduit";
                        }
                        else if (buildType.Equals("Reactor Turbine"))
                        {
                            buildType = "Nuclear Reactor";
                            previousBuildType = "Generator";
                            nextBuildType = "Reactor Turbine";
                        }
                        else if (buildType.Equals("Nuclear Reactor"))
                        {
                            buildType = "Generator";
                            previousBuildType = "Solar Panel";
                            nextBuildType = "Nuclear Reactor";
                        }
                        else if (buildType.Equals("Generator"))
                        {
                            buildType = "Solar Panel";
                            previousBuildType = "Universal Extractor";
                            nextBuildType = "Generator";
                        }
                        else if (buildType.Equals("Solar Panel"))
                        {
                            buildType = "Universal Extractor";
                            previousBuildType = "Rail Cart";
                            nextBuildType = "Solar Panel";
                        }
                        else if (buildType.Equals("Universal Extractor"))
                        {
                            buildType = "Rail Cart";
                            previousBuildType = "Rail Cart Hub";
                            nextBuildType = "Universal Extractor";
                        }
                        else if (buildType.Equals("Rail Cart"))
                        {
                            buildType = "Rail Cart Hub";
                            previousBuildType = "Retriever";
                            nextBuildType = "Rail Cart";
                        }
                        else if (buildType.Equals("Rail Cart Hub"))
                        {
                            buildType = "Retriever";
                            previousBuildType = "Universal Conduit";
                            nextBuildType = "Rail Cart Hub";
                        }
                        else if (buildType.Equals("Retriever"))
                        {
                            buildType = "Universal Conduit";
                            previousBuildType = "Smelter";
                            nextBuildType = "Retriever";
                        }
                        else if (buildType.Equals("Universal Conduit"))
                        {
                            buildType = "Smelter";
                            previousBuildType = "Press";
                            nextBuildType = "Universal Conduit";
                        }
                        else if (buildType.Equals("Smelter"))
                        {
                            buildType = "Press";
                            previousBuildType = "Extruder";
                            nextBuildType = "Smelter";
                        }
                        else if (buildType.Equals("Press"))
                        {
                            buildType = "Extruder";
                            previousBuildType = "Auger";
                            nextBuildType = "Press";
                        }
                        else if (buildType.Equals("Extruder"))
                        {
                            buildType = "Auger";
                            previousBuildType = "Electric Light";
                            nextBuildType = "Extruder";
                        }
                        else if (buildType.Equals("Auger"))
                        {
                            buildType = "Electric Light";
                            previousBuildType = "Storage Computer";
                            nextBuildType = "Auger";
                        }
                        else if (buildType.Equals("Electric Light"))
                        {
                            buildType = "Storage Computer";
                            previousBuildType = "Storage Container";
                            nextBuildType = "Electric Light";
                        }
                        else if (buildType.Equals("Storage Computer"))
                        {
                            buildType = "Storage Container";
                            previousBuildType = "Quantum Hatchway";
                            nextBuildType = "Storage Computer";
                        }
                        else if (buildType.Equals("Storage Container"))
                        {
                            buildType = "Quantum Hatchway";
                            previousBuildType = "Steel Ramp";
                            nextBuildType = "Storage Container";
                        }
                        else if (buildType.Equals("Quantum Hatchway"))
                        {
                            buildType = "Steel Ramp";
                            previousBuildType = "Steel Block";
                            nextBuildType = "Quantum Hatchway";
                        }
                        else if (buildType.Equals("Steel Ramp"))
                        {
                            buildType = "Steel Block";
                            previousBuildType = "Iron Ramp";
                            nextBuildType = "Steel Block";
                        }
                        else if (buildType.Equals("Steel Block"))
                        {
                            buildType = "Iron Ramp";
                            previousBuildType = "Iron Block";
                            nextBuildType = "Steel Block";
                        }
                        else if (buildType.Equals("Iron Ramp"))
                        {
                            buildType = "Iron Block";
                            previousBuildType = "Brick";
                            nextBuildType = "Iron Ramp";
                        }
                        else if (buildType.Equals("Iron Block"))
                        {
                            buildType = "Brick";
                            previousBuildType = "Glass Block";
                            nextBuildType = "Iron Block";
                        }
                        else if (buildType.Equals("Brick"))
                        {
                            buildType = "Glass Block";
                            previousBuildType = "Turret";
                            nextBuildType = "Iron Block";
                        }
                        else if (buildType.Equals("Glass Block"))
                        {
                            buildType = "Turret";
                            previousBuildType = "Dark Matter Collector";
                            nextBuildType = "Glass Block";
                        }
                        displayingBuildItem = true;
                        buildItemDisplayTimer = 0;
                        guiSound.volume = 0.15f;
                        guiSound.clip = buttonClip;
                        guiSound.Play();
                        destroyTimer = 0;
                        buildTimer = 0;
                    }
                }

                if (displayingBuildItem == true)
                {
                    buildItemDisplayTimer += 1 * Time.deltaTime;
                    if (buildItemDisplayTimer > 3)
                    {
                        displayingBuildItem = false;
                        buildItemDisplayTimer = 0;
                    }
                }

                //IF THE PLAYER HAS SELECTED AN ITEM AND HAS THAT ITEM IN THE INVENTORY, BEGIN BUILDING ON KEY PRESS
                if (cInput.GetKeyDown("Build"))
                {
                    if (inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false && paintGunActive == false)
                    {
                        bool foundItems = false;
                        foreach (InventorySlot slot in playerInventory.inventory)
                        {
                            if (foundItems == false)
                            {
                                if (slot.amountInSlot > 0)
                                {
                                    if (slot.typeInSlot.Equals(buildType))
                                    {
                                        foundItems = true;
                                    }
                                }
                            }
                        }
                        if (foundItems == true)
                        {
                            building = true;
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            inventoryOpen = false;
                            craftingGUIopen = false;
                            storageGUIopen = false;
                            if (scannerActive == true)
                            {
                                scanner.SetActive(false);
                                scannerActive = false;
                            }
                            if (laserCannonActive == true)
                            {
                                laserCannon.SetActive(false);
                                laserCannonActive = false;
                            }
                        }
                    }
                }

                //ACTIVATE INVENTORY GUI ON KEY PRESS
                if (cInput.GetKeyDown("Inventory"))
                {
                    if (inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        machineGUIopen = false;
                        inventoryOpen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        inventoryOpen = false;
                        craftingGUIopen = false;
                        storageGUIopen = false;
                        machineGUIopen = false;
                    }
                }

                //ACTIVATE CRAFTING GUI ON KEY PRESS
                if (cInput.GetKeyDown("Crafting"))
                {
                    if (inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        machineGUIopen = false;
                        inventoryOpen = true;
                        craftingGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        inventoryOpen = false;
                        craftingGUIopen = false;
                        storageGUIopen = false;
                        machineGUIopen = false;
                    }
                }

                //ACTIVATE TABLET GUI ON KEY PRESS
                if (cInput.GetKeyDown("Tablet"))
                {
                    if (tabletOpen == false && inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        tabletOpen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        tabletOpen = false;
                    }
                }

                //OPEN OPTIONS/EXIT MENU WHEN ESCAPE KEY IS PRESSED
                if (Input.GetKeyDown(KeyCode.Escape) && exiting == false)
                {
                    if (inventoryOpen == true)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        gameObject.GetComponent<MSCameraController>().enabled = false;
                        inventoryOpen = false;
                        craftingGUIopen = false;
                        storageGUIopen = false;
                    }
                    else if (machineGUIopen == true)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        gameObject.GetComponent<MSCameraController>().enabled = false;
                        machineGUIopen = false;
                    }
                    else if (tabletOpen == true)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        gameObject.GetComponent<MSCameraController>().enabled = false;
                        tabletOpen = false;
                    }
                    else if (paintGunActive == true && paintColorSelected == false)
                    {
                        if (building == true || destroying == true)
                        {
                            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                            {
                                stoppingBuildCoRoutine = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        gameObject.GetComponent<MSCameraController>().enabled = false;
                        paintGun.SetActive(false);
                        paintGunActive = false;
                        paintColorSelected = false;
                    }
                    else if (paintGunActive == true && paintColorSelected == true)
                    {
                        paintGun.SetActive(false);
                        paintGunActive = false;
                        paintColorSelected = false;
                    }
                    else if (escapeMenuOpen == false)
                    {
                        requestedEscapeMenu = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        gameObject.GetComponent<MSCameraController>().enabled = true;
                        if (cGUI.showingInputGUI == true)
                        {
                            cGUI.ToggleGUI();
                        }
                        escapeMenuOpen = false;
                        optionsGUIopen = false;
                        helpMenuOpen = false;
                        videoMenuOpen = false;
                        schematicMenuOpen = false;
                    }
                }

                //LOCKING AND UNLOCKING MOUSE CURSOR FOR GUI
                if (cGUI.showingInputGUI == true || inventoryOpen == true || escapeMenuOpen == true || machineGUIopen == true || tabletOpen == true || paintGunActive == true && paintColorSelected == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    gameObject.GetComponent<MSCameraController>().enabled = false;
                }
                if (cGUI.showingInputGUI == false && inventoryOpen == false && escapeMenuOpen == false && machineGUIopen == false && tabletOpen == false && paintGunActive == false)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    gameObject.GetComponent<MSCameraController>().enabled = true;
                }

                //OPENING ESCAPE MENU
                if (requestedEscapeMenu == true)
                {
                    if (building == true || destroying == true)
                    {
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                        {
                            stoppingBuildCoRoutine = true;
                            GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                    else if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        gameObject.GetComponent<MSCameraController>().enabled = false;
                        escapeMenuOpen = true;
                        requestedEscapeMenu = false;
                    }
                }

                //EXITING THE GAME
                if (requestedExit == true)
                {
                    requestedExitTimer += 1 * Time.deltaTime;
                    if (requestedExitTimer >= 5)
                    {
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                        {
                            GameObject.Find("GameManager").GetComponent<GameManager>().RequestFinalSaveOperation();
                            quitCoroutine = StartCoroutine(Quit());
                            requestedExitTimer = 0;
                            requestedExit = false;
                        }
                    }
                }

                //WORLD SIZE LIMITATIONS
                //Debug.Log(gameObject.transform.position);
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

            //CLOSE STORAGE CONTAINERS WHEN THE PLAYER MOVES AWAY FROM THEM
            if (storageInventory != null && storageGUIopen == true)
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
        }
    }

    //THIS FUNCTION HANDLES SAVING OF DATA AND EXITING THE GAME
    public static IEnumerator Quit()
    {
        float f = 0;
        while (f < 6000)
        {
            f++;
            if (GameObject.Find("GameManager").GetComponent<GameManager>().blocksCombined == false)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().savingData == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<StateManager>().dataSaved == true)
                    {
                        if (GameObject.Find("Player").GetComponent<PlayerController>().backupCreated == false)
                        {
                            Debug.Log("Creating backup...");
                            string osType = System.Environment.OSVersion.ToString().Split(' ')[0];
                            if (osType.Equals("Unix"))
                            {
                                System.IO.File.Copy("/home/" + System.Environment.UserName + "/.config/unity3d/Droog71/Quantum Engineering/prefs", "/home/" + System.Environment.UserName + "/.config/unity3d/Droog71/Quantum Engineering/" + GameObject.Find("GameManager").GetComponent<StateManager>().WorldName, true);
                            }
                            GameObject.Find("Player").GetComponent<PlayerController>().backupCreated = true;
                        }
                        if (!Application.isEditor)
                        {
                            Debug.Log("Shutting down...");
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}