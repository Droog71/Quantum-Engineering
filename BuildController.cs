using System.Collections;
using UnityEngine;
using System.Net;

public class BuildController : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    public BlockDictionary blockDictionary;
    private LineRenderer dirLine;
    public Material lineMat;
    public GameObject builtObjects;
    public AudioClip singleBuildClip;
    public AudioClip multiBuildClip;
    public bool autoAxis;
    private Coroutine buildBlockCoroutine;
    private Coroutine updateNetworkConduitCoroutine;

    //! Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        blockDictionary = new BlockDictionary(playerController);
        builtObjects = GameObject.Find("BuiltObjects");
    }

    //! Called once per frame by unity engine
    public void Update()
    {
        if (!playerController.stateManager.Busy())
        {
            if (playerController.building == true)
            {
                if (playerController.separatedBlocks == false)
                {
                    if (gameManager.working == false)
                    {
                        gameManager.meshManager.SeparateBlocks(transform.position, "all", true);
                        playerController.separatedBlocks = true;
                    }
                    else
                    {
                        playerController.requestedChunkLoad = true;
                    }
                    playerController.buildStartPosition = transform.position;
                }
                else
                {
                    float distance = Vector3.Distance(transform.position, playerController.buildStartPosition);
                    if (distance > gameManager.chunkSize * 0.75f)
                    {
                        if (gameManager.working == false)
                        {
                            gameManager.meshManager.SeparateBlocks(transform.position, "all", true);
                            playerController.separatedBlocks = true;
                        }
                        else
                        {
                            playerController.requestedChunkLoad = true;
                        }
                        playerController.buildStartPosition = transform.position;
                    }
                }

                if (playerController.buildObject == null)
                {
                    CreateBuildObject();
                }
                else
                {
                    if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out RaycastHit hit, gameManager.chunkSize))
                    {
                        float distance = Vector3.Distance(transform.position, playerController.buildObject.transform.position);
                        Material buildObjectMaterial = playerController.buildObject.GetComponent<MeshRenderer>().material;
                        buildObjectMaterial.color = distance > gameManager.chunkSize * 0.75f ? Color.red : Color.white;
                        if (hit.transform.gameObject.tag == "Built" || hit.transform.gameObject.tag == "Machine")
                        {
                            if (autoAxis == true)
                            {
                                AutoSelectBuildAxis(hit);
                            }
                            SetupBuildAxis(hit);
                        }
                        else
                        {
                            if (autoAxis == true)
                            {
                                playerController.cubeloc = "up";
                            }
                            SetupFreePlacement(hit);
                        }
                    }
                    if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out RaycastHit buildHit, gameManager.chunkSize * 0.75f))
                    {
                        if (buildHit.collider.gameObject.tag != "CombinedMesh")
                        {
                            playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.white;
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                if (blockDictionary.machineDictionary.ContainsKey(playerController.buildType))
                                {
                                    BuildMachine(playerController.buildType, hit);
                                }
                                else
                                {
                                    BuildBlock(playerController.buildType);
                                }
                            }
                        }
                        else
                        {
                            playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.red;
                            if (gameManager.working == false)
                            {
                                gameManager.meshManager.SeparateBlocks(buildHit.point, "all", true);
                            }
                            else
                            {
                                playerController.requestedChunkLoad = true;
                            }
                        }
                    }
                }
            }
            else if (playerController.buildObject != null)
            {
                Destroy(playerController.buildObject);
            }
        }
    }

    //! Creates the block placement cursor.
    private void CreateBuildObject()
    {
        playerController.buildObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        playerController.buildObject.transform.localScale = new Vector3(5, 5, 5);
        playerController.buildObject.transform.position = Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward * 5;
        playerController.buildObject.GetComponent<Renderer>().material = playerController.constructionMat;
        playerController.buildObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Destroy(playerController.buildObject.GetComponent<Collider>());
        dirLine = playerController.buildObject.AddComponent<LineRenderer>();
        dirLine.material = lineMat;
        dirLine.startWidth = 0.2f;
        dirLine.endWidth = 0.2f;
        dirLine.loop = true;
        dirLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        dirLine.SetPosition(0, playerController.buildObject.transform.position);
        Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
        dirLine.SetPosition(1, dirVector);
        dirLine.enabled = true;
    }

    //! Automatically selects build axis based on raycast.
    private void AutoSelectBuildAxis(RaycastHit buildHit)
    {
        Vector3 hitPoint = buildHit.point;
        Vector3 blockPos = buildHit.collider.transform.position;
        float xDif = hitPoint.x - blockPos.x;
        float yDif = hitPoint.y - blockPos.y;
        float zDif = hitPoint.z - blockPos.z;
        float xnDif = blockPos.x - hitPoint.x;
        float ynDif = blockPos.y - hitPoint.y;
        float znDif = blockPos.z - hitPoint.z;

        if (hitPoint.x > blockPos.x)
        {
            if (xDif > yDif && xDif > zDif)
                playerController.cubeloc = "left";
        }
        if (hitPoint.x < blockPos.x)
        {
            if (xnDif > ynDif && xnDif > znDif)
                playerController.cubeloc = "right";
        }
        if (hitPoint.y > blockPos.y)
        {
            if (yDif > xDif && yDif > zDif)
                playerController.cubeloc = "up";
        }
        if (hitPoint.y < blockPos.y)
        {
            if (ynDif > xnDif && ynDif > znDif)
                playerController.cubeloc = "down";
        }
        if (hitPoint.z > blockPos.z)
        {
            if (zDif > xDif && zDif > yDif)
                playerController.cubeloc = "front";
        }
        if (hitPoint.z < blockPos.z)
        {
            if (znDif > xnDif && znDif > ynDif)
                playerController.cubeloc = "back";
        }
    }

    //! Changes the axis along which blocks will be placed.
    public void ChangeBuildAxis()
    {
        if (playerController.cubeloc == "up")
        {
            playerController.cubeloc = "down";
        }
        else if (playerController.cubeloc == "down")
        {
            playerController.cubeloc = "left";
        }
        else if (playerController.cubeloc == "left")
        {
            playerController.cubeloc = "right";
        }
        else if (playerController.cubeloc == "right")
        {
            playerController.cubeloc = "front";
        }
        else if (playerController.cubeloc == "front")
        {
            playerController.cubeloc = "back";
        }
        else if (playerController.cubeloc == "back")
        {
            playerController.cubeloc = "up";
        }
    }

    //! Implements the current build axis.
    private void SetupBuildAxis(RaycastHit hit)
    {
        if (gameManager.working == false)
        {
            Vector3 placementPoint = hit.transform.position;
            Quaternion placementRotation;
            Vector3 dirVector;

            if (playerController.cubeloc == "up")
            {
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y + 5, hit.transform.position.z);
            }
            else if (playerController.cubeloc == "down")
            {
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y - 5, hit.transform.position.z);
            }
            else if (playerController.cubeloc == "left")
            {
                placementPoint = new Vector3(hit.transform.position.x + 5, hit.transform.position.y, hit.transform.position.z);
            }
            else if (playerController.cubeloc == "right")
            {
                placementPoint = new Vector3(hit.transform.position.x - 5, hit.transform.position.y, hit.transform.position.z);
            }
            else if (playerController.cubeloc == "front")
            {
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 5);
            }
            else if (playerController.cubeloc == "back")
            {
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 5);
            }

            placementRotation = playerController.buildObject.transform.rotation;
            playerController.buildObject.transform.position = placementPoint;
            playerController.buildObject.transform.rotation = placementRotation;
            dirLine.SetPosition(0, playerController.buildObject.transform.position);
            dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
            dirLine.SetPosition(1, dirVector);
        }
    }

    //! Prepares cursor for free block placement, ie: not attached to another block.
    private void SetupFreePlacement(RaycastHit hit)
    {
        Vector3 placementPoint = new Vector3(hit.point.x, (hit.point.y + 2.4f), hit.point.z);
        Quaternion placementRotation = playerController.buildObject.transform.rotation;
        playerController.buildObject.transform.position = placementPoint;
        playerController.buildObject.transform.rotation = placementRotation;
        dirLine.SetPosition(0, playerController.buildObject.transform.position);
        Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
        dirLine.SetPosition(1, dirVector);
    }

    //! Places a machine in the world.
    private void BuildMachine(string type, RaycastHit hit)
    {
        bool foundItems = false;
        gameManager.undoBlocks.Clear();
        foreach (InventorySlot slot in playerController.playerInventory.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot > 0)
                {
                    if (slot.typeInSlot.Equals(type))
                    {
                        foundItems = true;
                        bool flag = true;
                        if (type.Equals("Rail Cart"))
                        {
                            if (hit.collider.gameObject.GetComponent<RailCartHub>() != null)
                            {
                                flag = true;
                            }
                            else
                            {
                                playerController.invalidRailCartPlacement = true;
                                flag = false;
                            }
                        }
                        else if (type.Equals("Auger"))
                        {
                            if (hit.collider.gameObject.tag.Equals("Landscape"))
                            {
                                flag = true;
                            }
                            else
                            {
                                playerController.invalidAugerPlacement = true;
                                flag = false;
                            }
                        }
                        if (flag == true)
                        {
                            GameObject t = blockDictionary.machineDictionary[type];
                            Vector3 pos = playerController.buildObject.transform.position;
                            Quaternion rot = playerController.buildObject.transform.rotation;
                            GameObject obj = Instantiate(t, pos, rot);
                            if (obj.GetComponent<RailCart>() != null)
                            {
                                obj.GetComponent<RailCart>().target = hit.collider.gameObject;
                            }
                            if (obj.GetComponent<ModMachine>() != null)
                            {
                                obj.GetComponent<ModMachine>().machineName = type;
                            }
                            if (obj.GetComponent<UniversalConduit>() != null)
                            {
                                obj.GetComponent<UniversalConduit>().range = playerController.defaultRange;
                                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                                {
                                    NetworkSend net = playerController.networkController.networkSend;
                                    Vector3 location = obj.transform.position;
                                    updateNetworkConduitCoroutine = StartCoroutine(net.SendConduitData(location,playerController.defaultRange));
                                }
                            }
                            if (obj.GetComponent<PowerConduit>() != null)
                            {
                                obj.GetComponent<PowerConduit>().range = playerController.defaultRange;
                                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                                {
                                    NetworkSend net = playerController.networkController.networkSend;
                                    Vector3 location = obj.transform.position;
                                    int range = playerController.defaultRange;
                                    bool dualOutput = obj.GetComponent<PowerConduit>().dualOutput;
                                    updateNetworkConduitCoroutine = StartCoroutine(net.SendPowerData(location,range,dualOutput));
                                }
                            }
                            if (obj.GetComponent<DarkMatterConduit>() != null)
                            {
                                obj.GetComponent<DarkMatterConduit>().range = playerController.defaultRange;
                                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                                {
                                    NetworkSend net = playerController.networkController.networkSend;
                                    Vector3 location = obj.transform.position;
                                    updateNetworkConduitCoroutine = StartCoroutine(net.SendConduitData(location,playerController.defaultRange));
                                }
                            }
                            if (obj.GetComponent<RailCartHub>() != null)
                            {
                                obj.GetComponent<RailCartHub>().range = playerController.defaultRange;
                                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                                {
                                    NetworkSend net = playerController.networkController.networkSend;
                                    RailCartHub hub = obj.GetComponent<RailCartHub>();
                                    Vector3 location = obj.transform.position;
                                    int range = playerController.defaultRange;
                                    updateNetworkConduitCoroutine = StartCoroutine(net.SendHubData(location,hub.range,hub.stop,hub.stopTime));
                                }
                            }
                            gameManager.undoBlocks.Add(new GameManager.Block(type, obj));
                            slot.amountInSlot -= 1;
                            playerController.builderSound.clip = singleBuildClip;
                            playerController.builderSound.Play();
                            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                            {
                                UpdateNetwork(0,type,pos, obj.transform.rotation);
                            }
                        }
                    }
                    if (slot.amountInSlot == 0)
                    {
                        slot.typeInSlot = "nothing";
                    }
                }
            }
        }
        if (foundItems == false)
        {
            playerController.PlayMissingItemsSound();
        }
    }

    //! Sends instantiated block info to the server in multiplayer games.
    private void UpdateNetwork(int destroy, string type, Vector3 pos, Quaternion rot)
    {
        using(WebClient client = new WebClient())
        {
            System.Uri uri = new System.Uri(PlayerPrefs.GetString("serverURL") + "/blocks");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            string rotation = Mathf.Round(rot.x) + "," + Mathf.Round(rot.y) + "," + Mathf.Round(rot.z) + "," + Mathf.Round(rot.w);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + position + ":" + rotation);
        }
    }

    private void BuildBlock(string type)
    {
        gameManager.undoBlocks.Clear();
        buildBlockCoroutine = StartCoroutine(BuildBlockCoroutine(type));
    }

    //! Places standard building blocks in the world.
    private IEnumerator BuildBlockCoroutine(string type)
    {
        bool foundItems = false;
        foreach (InventorySlot slot in playerController.playerInventory.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= playerController.buildMultiplier)
                {
                    if (slot.typeInSlot.Equals(type))
                    {
                        foundItems = true;
                        int h = 0;
                        Vector3 origin = playerController.buildObject.transform.position;
                        string direction = playerController.cubeloc;
                        Quaternion rotation = playerController.buildObject.transform.rotation;
                        Vector3 multiBuildPlacement = playerController.buildObject.transform.position;
                        slot.amountInSlot -= playerController.buildMultiplier;
                        for (int i = 0; i < playerController.buildMultiplier; i++)
                        {
                            if (direction == "up")
                            {
                                multiBuildPlacement = new Vector3(origin.x, origin.y + h, origin.z);
                            }
                            if (direction == "down")
                            {
                                multiBuildPlacement = new Vector3(origin.x, origin.y - h, origin.z);
                            }
                            if (direction == "left")
                            {
                                multiBuildPlacement = new Vector3(origin.x + h, origin.y, origin.z);
                            }
                            if (direction == "right")
                            {
                                multiBuildPlacement = new Vector3(origin.x - h, origin.y, origin.z);
                            }
                            if (direction == "front")
                            {
                                multiBuildPlacement = new Vector3(origin.x, origin.y, origin.z + h);
                            }
                            if (direction == "back")
                            {
                                multiBuildPlacement = new Vector3(origin.x, origin.y, origin.z - h);
                            }
                            h += 5;
                            GameObject obj = Instantiate(blockDictionary.blockDictionary[type], multiBuildPlacement, rotation);
                            if (obj.GetComponent<ModBlock>() != null)
                            {
                                obj.GetComponent<ModBlock>().blockName = type;
                            }
                            obj.transform.parent = builtObjects.transform;
                            gameManager.undoBlocks.Add(new GameManager.Block(type, obj));
                            playerController.builderSound.clip = playerController.buildMultiplier > 1 ? multiBuildClip : singleBuildClip;
                            playerController.builderSound.Play();
                            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                            {
                                UpdateNetwork(0, type, obj.transform.position, obj.transform.rotation);
                            }
                            yield return null;
                        }
                    }
                    if (slot.amountInSlot == 0)
                    {
                        slot.typeInSlot = "nothing";
                    }
                }
            }
        }
        if (foundItems == false)
        {
            playerController.PlayMissingItemsSound();
        }
    }
}