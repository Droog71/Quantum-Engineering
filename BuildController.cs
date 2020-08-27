using UnityEngine;

public class BuildController : MonoBehaviour
{
    private PlayerController playerController;
    private BlockDictionary blockDictionary;
    LineRenderer dirLine;
    public Material lineMat;
    public GameObject builtObjects;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        blockDictionary = gameObject.AddComponent<BlockDictionary>();
        builtObjects = GameObject.Find("Built_Objects");
    }

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
                        if (type.Equals("Auger"))
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
                            Instantiate(blockDictionary.dictionary[type], playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
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
            if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
            {
                playerController.stoppingBuildCoRoutine = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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

    public void Update()
    {
        //PLACING BLOCKS
        if (playerController.building == true)
        {
            playerController.buildTimer += 1 * Time.deltaTime;
            if (playerController.buildTimer >= 30)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    playerController.stoppingBuildCoRoutine = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all", true);
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
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all", true);
                        playerController.separatedBlocks = true;
                    }
                    else
                    {
                        playerController.requestedChunkLoad = true;
                    }
                    playerController.buildStartPosition = transform.position;
                }
            }

            //CANCEL BUILDING ON KEY PRESS
            if (cInput.GetKeyDown("Stop Building"))
            {
                if (playerController.building == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        playerController.stoppingBuildCoRoutine = true;
                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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

            if (playerController.buildObject == null)
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

                    //SNAP BLOCKS TO ADJACENT BLOCKS DEPENDING ON AXIS SELECTED BY THE PLAYER
                    if (hit.transform.gameObject.tag == "Built")
                    {
                        //BLOCK ROTATION
                        if (cInput.GetKeyDown("Rotate Block"))
                        {
                            playerController.buildObject.transform.Rotate(transform.up * 90);
                            playerController.destroyTimer = 0;
                            playerController.buildTimer = 0;
                        }
                        //BUILD AXIS
                        if (cInput.GetKeyDown("Build Axis"))
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

                        if (playerController.cubeloc == "up")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y + 5, hit.transform.position.z);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                        if (playerController.cubeloc == "down")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y - 5, hit.transform.position.z);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                        if (playerController.cubeloc == "left")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x + 5, hit.transform.position.y, hit.transform.position.z);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                        if (playerController.cubeloc == "right")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x - 5, hit.transform.position.y, hit.transform.position.z);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                        if (playerController.cubeloc == "front")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 5);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                        if (playerController.cubeloc == "back")
                        {
                            Vector3 placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 5);
                            Quaternion placementRotation = playerController.buildObject.transform.rotation;
                            playerController.buildObject.transform.position = placementPoint;
                            playerController.buildObject.transform.rotation = placementRotation;
                            dirLine.SetPosition(0, playerController.buildObject.transform.position);
                            Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                            dirLine.SetPosition(1, dirVector);
                        }
                    }
                    else
                    {
                        Vector3 placementPoint = new Vector3(hit.point.x, (hit.point.y + 2.4f), hit.point.z);
                        //BLOCK ROTATION
                        if (cInput.GetKeyDown("Rotate Block"))
                        {
                            playerController.buildObject.transform.Rotate(transform.up * 90);
                            playerController.destroyTimer = 0;
                            playerController.buildTimer = 0;
                        }
                        Quaternion placementRotation = playerController.buildObject.transform.rotation;
                        playerController.buildObject.transform.position = placementPoint;
                        playerController.buildObject.transform.rotation = placementRotation;
                        dirLine.SetPosition(0, playerController.buildObject.transform.position);
                        Vector3 dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
                        dirLine.SetPosition(1, dirVector);
                    }
                }
                if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out RaycastHit buildHit, 40))
                {
                    if (buildHit.collider.gameObject.tag != "CombinedMesh")
                    {
                        playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        if (playerController.buildType == "Steel Block")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Steel Block") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.steel, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<Steel>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else if (playerController.buildType == "Steel Ramp")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Steel Ramp") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.steelRamp, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<Steel>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else if (playerController.buildType == "Iron Block")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Iron Block") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.ironBlock, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<IronBlock>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else if (playerController.buildType == "Iron Ramp")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Iron Ramp") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.ironRamp, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<IronBlock>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else if (playerController.buildType == "Glass Block")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Glass Block") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.glass, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<Glass>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else if (playerController.buildType == "Brick")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot >= playerController.buildMultiplier)
                                        {
                                            if (slot.typeInSlot.Equals("Brick") && GameObject.Find("GameManager").GetComponent<GameManager>().blockLimitReached == false)
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
                                                    GameObject builtObject = Instantiate(playerController.brick, multiBuildPlacement, playerController.buildObject.transform.rotation);
                                                    builtObject.transform.parent = builtObjects.transform;
                                                    builtObject.GetComponent<Brick>().creationMethod = "built";
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
                                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                                    {
                                        playerController.stoppingBuildCoRoutine = true;
                                        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
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
                        else
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                BuildMachine(playerController.buildType, hit);
                            }
                        }
                    }
                    else
                    {
                        playerController.buildObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                        {
                            GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(buildHit.point, "all", true);
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


