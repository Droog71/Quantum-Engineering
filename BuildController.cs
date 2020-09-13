using UnityEngine;

public class BuildController : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    public BlockDictionary blockDictionary;
    private LineRenderer dirLine;
    public Material lineMat;
    public GameObject builtObjects;
    public bool autoAxis;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        blockDictionary = new BlockDictionary(playerController);
        builtObjects = GameObject.Find("Built_Objects");
    }

    // Called once per frame by unity engine
    public void Update()
    {
        if (playerController.building == true)
        {
            playerController.buildTimer += 1 * Time.deltaTime;
            if (playerController.buildTimer >= 30)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    playerController.stoppingBuildCoRoutine = true;
                    gameManager.meshManager.CombineBlocks();
                    playerController.separatedBlocks = false;
                    playerController.destroyTimer = 0;
                    playerController.buildTimer = 0;
                    playerController.building = false;
                    playerController.destroying = false;
                }
                else
                {
                    playerController.requestedBuildingStop = true;
                }
            }

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
                if (distance > 15)
                {
                    //Debug.Log("new chunk loaded");
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
                if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out RaycastHit hit, 100))
                {
                    //LIMIT THE BUILD RANGE TO 40
                    float distance = Vector3.Distance(transform.position, playerController.buildObject.transform.position);

                    if (distance > 40)
                    {
                        playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                    else
                    {
                        playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    }

                    if (hit.transform.gameObject.tag == "Built")
                    {
                        SetupBuildAxis(hit);
                    }
                    else
                    {
                        SetupFreePlacement(hit);
                    }
                }
                if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out RaycastHit buildHit, 40))
                {
                    if (buildHit.collider.gameObject.tag != "CombinedMesh")
                    {
                        playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        if (autoAxis == true)
                        {
                            AutoSelectBuildAxis(buildHit);
                        }
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
                    }
                }
            }
        }
        else if (playerController.buildObject != null)
        {
            Destroy(playerController.buildObject);
        }
    }

    // Creates the block placement cursor.
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

    // Automatically selects build axis based on raycast.
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

        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
    }

    // Changes the axis along which blocks will be placed.
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

        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
    }

    // Implements the current build axis.
    private void SetupBuildAxis(RaycastHit hit)
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

    // Prepares cursor for free block placement, ie: not attached to another block.
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

    // Places a machine in the world.
    private void BuildMachine(string type, RaycastHit hit)
    {
        bool foundItems = false;
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
                            if (t.GetComponent<ModMachine>() != null)
                            {
                                ModMachine machine = t.GetComponent<ModMachine>();
                                machine.machineName = type;
                            }
                            slot.amountInSlot -= 1;
                            playerController.builderSound.Play();
                            playerController.destroyTimer = 0;
                            playerController.buildTimer = 0;
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
            if (gameManager.working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                gameManager.meshManager.CombineBlocks();
                playerController.separatedBlocks = false;
                playerController.destroyTimer = 0;
                playerController.buildTimer = 0;
                playerController.building = false;
                playerController.destroying = false;
            }
            else
            {
                playerController.requestedBuildingStop = true;
            }
        }
    }

    // Places standard building blocks in the world.
    private void BuildBlock(string type)
    {
        bool foundItems = false;
        foreach (InventorySlot slot in playerController.playerInventory.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= playerController.buildMultiplier)
                {
                    if (slot.typeInSlot.Equals(type) && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
                    {
                        foundItems = true;
                        int h = 0;
                        Vector3 multiBuildPlacement = playerController.buildObject.transform.position;
                        for (int i = 0; i < playerController.buildMultiplier; i++)
                        {
                            if (playerController.cubeloc == "up")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x, playerController.buildObject.transform.position.y + h, playerController.buildObject.transform.position.z);
                            }
                            if (playerController.cubeloc == "down")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x, playerController.buildObject.transform.position.y - h, playerController.buildObject.transform.position.z);
                            }
                            if (playerController.cubeloc == "left")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x + h, playerController.buildObject.transform.position.y, playerController.buildObject.transform.position.z);
                            }
                            if (playerController.cubeloc == "right")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x - h, playerController.buildObject.transform.position.y, playerController.buildObject.transform.position.z);
                            }
                            if (playerController.cubeloc == "front")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x, playerController.buildObject.transform.position.y, playerController.buildObject.transform.position.z + h);
                            }
                            if (playerController.cubeloc == "back")
                            {
                                multiBuildPlacement = new Vector3(playerController.buildObject.transform.position.x, playerController.buildObject.transform.position.y, playerController.buildObject.transform.position.z - h);
                            }
                            h += 5;
                            Instantiate(blockDictionary.blockDictionary[type], multiBuildPlacement, playerController.buildObject.transform.rotation);
                            slot.amountInSlot -= 1;
                            playerController.builderSound.Play();
                            playerController.destroyTimer = 0;
                            playerController.buildTimer = 0;
                        }
                    }
                    else if (GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == true)
                    {
                        playerController.blockLimitMessage = true;
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
            if (gameManager.working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                gameManager.meshManager.CombineBlocks();
                playerController.separatedBlocks = false;
                playerController.destroyTimer = 0;
                playerController.buildTimer = 0;
                playerController.building = false;
                playerController.destroying = false;
            }
            else
            {
                playerController.requestedBuildingStop = true;
            }
        }
    }
}


