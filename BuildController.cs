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
    private Coroutine updateNetworkCoroutine;

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
                        if (hit.collider.gameObject.tag == "Built" || hit.collider.gameObject.tag == "Machine")
                        {
                            if (autoAxis == true)
                            {
                                AutoSelectBuildAxis(hit);
                            }
                            SetupBuildAxis(hit.collider.gameObject.transform.position);
                        }
                        else if (hit.collider.gameObject.tag == "CombinedMesh")
                        {
                            BlockHolder bh = hit.collider.gameObject.GetComponent<BlockHolder>();
                            if (bh.blockData != null)
                            {
                                foreach (BlockHolder.BlockInfo info in bh.blockData)
                                {
                                    float infoDistance = Vector3.Distance(info.position, hit.point);
                                    if (infoDistance <= 5)
                                    {
                                        if (autoAxis == true)
                                        {
                                            AutoSelectBuildAxis(hit);
                                        }
                                        SetupBuildAxis(info.position);
                                    }
                                }
                            }
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
    private void SetupBuildAxis(Vector3 loc)
    {
        Vector3 placementPoint = loc;
        Quaternion placementRotation;
        Vector3 dirVector;

        if (playerController.cubeloc == "up")
        {
            placementPoint = new Vector3(loc.x, loc.y + 5, loc.z);
        }
        else if (playerController.cubeloc == "down")
        {
            placementPoint = new Vector3(loc.x, loc.y - 5, loc.z);
        }
        else if (playerController.cubeloc == "left")
        {
            placementPoint = new Vector3(loc.x + 5, loc.y, loc.z);
        }
        else if (playerController.cubeloc == "right")
        {
            placementPoint = new Vector3(loc.x - 5, loc.y, loc.z);
        }
        else if (playerController.cubeloc == "front")
        {
            placementPoint = new Vector3(loc.x, loc.y, loc.z + 5);
        }
        else if (playerController.cubeloc == "back")
        {
            placementPoint = new Vector3(loc.x, loc.y, loc.z - 5);
        }

        placementRotation = playerController.buildObject.transform.rotation;
        playerController.buildObject.transform.position = placementPoint;
        playerController.buildObject.transform.rotation = placementRotation;
        dirLine.SetPosition(0, playerController.buildObject.transform.position);
        dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
        dirLine.SetPosition(1, dirVector);
    }

    //! Prepares cursor for free block placement, ie: not attached to another block.
    private void SetupFreePlacement(RaycastHit hit)
    {
        Vector3 placementPoint = new Vector3(hit.point.x, (hit.point.y + 2.4f), hit.point.z);
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            placementPoint = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 2.4f), Mathf.Round(hit.point.z));
        }
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
        bool canBuild = true;

        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            canBuild = CanBuild();
        }
        else
        {
            canBuild = playerController.buildType != "Protection Block";
        }

        if (canBuild == true)
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
                                BlockHolder blockHolder = hit.collider.gameObject.GetComponent<BlockHolder>();
                                if (blockHolder != null)
                                {
                                    if (blockHolder.blockType == "Dirt")
                                    {
                                        flag = true;
                                    }
                                }
                                else if (hit.collider.gameObject.tag.Equals("Landscape"))
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
                                    obj.GetComponent<RailCart>().startPosition = pos;
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
                                        updateNetworkCoroutine = StartCoroutine(net.SendConduitData(location,playerController.defaultRange));
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
                                        updateNetworkCoroutine = StartCoroutine(net.SendPowerData(location,range,dualOutput));
                                    }
                                }
                                if (obj.GetComponent<DarkMatterConduit>() != null)
                                {
                                    obj.GetComponent<DarkMatterConduit>().range = playerController.defaultRange;
                                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                                    {
                                        NetworkSend net = playerController.networkController.networkSend;
                                        Vector3 location = obj.transform.position;
                                        updateNetworkCoroutine = StartCoroutine(net.SendConduitData(location,playerController.defaultRange));
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
                                        updateNetworkCoroutine = StartCoroutine(net.SendHubData(location, hub.circuit, hub.range, hub.stop, hub.stopTime));
                                    }
                                }
                                gameManager.undoBlocks.Add(new GameManager.UndoBlock(type, obj));
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
        else
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

    //! Returns true if the player is allowed to place the object.
    private bool CanBuild()
    {
        bool closeToProtectionBlock = false;
        ProtectionBlock[] protectionBlocks = FindObjectsOfType<ProtectionBlock>();
        if (playerController.buildType == "Protection Block")
        {
            int totalProtectionBlocks = 0;
            float distance = 1000;
            protectionBlocks = FindObjectsOfType<ProtectionBlock>();
            foreach (ProtectionBlock protectionBlock in protectionBlocks)
            {
                Vector3 protectionBlockPosition = protectionBlock.gameObject.transform.position;
                distance = Vector3.Distance(playerController.buildObject.transform.position, protectionBlockPosition);
                if (distance <= 160)
                {
                    return false;
                }
                if (protectionBlock.IsAuthorizedUser(PlayerPrefs.GetString("UserName")))
                {
                    totalProtectionBlocks++;
                }
            }
            Debug.Log("total protection blocks: " + totalProtectionBlocks);
            if (totalProtectionBlocks >= 16)
            {
                return false;
            }
        }

        foreach (ProtectionBlock protectionBlock in protectionBlocks)
        {
            Vector3 playerPos = playerController.gameObject.transform.position;
            Vector3 blockPos = protectionBlock.transform.position;
            Vector3 playerPosNoY = new Vector3(playerPos.x, 0, playerPos.z);
            Vector3 blockPosNoY = new Vector3(blockPos.x, 0, blockPos.z);
            float distance = Vector3.Distance(playerPosNoY, blockPosNoY);
            if (distance <= 160)
            {
                closeToProtectionBlock = true;
                if (protectionBlock.IsAuthorizedUser(PlayerPrefs.GetString("UserName")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        if (closeToProtectionBlock == false)
        {
            return true;
        }

        return false;
    }

    //! Starts the building coroutine.
    private void BuildBlock(string type)
    {
        gameManager.undoBlocks.Clear();
        buildBlockCoroutine = StartCoroutine(BuildBlockCoroutine(type));
    }

    //! Places standard building blocks in the world.
    private IEnumerator BuildBlockCoroutine(string type)
    {
        bool canBuild = true;

        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            canBuild = CanBuild();
        }

        if (canBuild == true)
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
                                if (obj.GetComponent<Block>() != null)
                                {
                                    obj.GetComponent<Block>().blockName = type;
                                }
                                obj.transform.parent = builtObjects.transform;
                                gameManager.undoBlocks.Add(new GameManager.UndoBlock(type, obj));
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
        else
        {
            playerController.PlayMissingItemsSound();
        }
    }
}