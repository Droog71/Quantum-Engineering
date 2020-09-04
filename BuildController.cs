using UnityEngine;
using System;

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
        blockDictionary = GetComponent<BlockDictionary>();
        builtObjects = GameObject.Find("Built_Objects");
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
                            ChangeBuildAxis();
                        }

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

    private void ChangeBuildAxis()
    {
        switch (playerController.cubeloc)
        {
            case "up":
                playerController.cubeloc = "down";
                break;
            case "down":
                playerController.cubeloc = "left";
                break;
            case "left":
                playerController.cubeloc = "right";
                break;
            case "right":
                playerController.cubeloc = "front";
                break;
            case "front":
                playerController.cubeloc = "back";
                break;
            case "back":
                playerController.cubeloc = "up";
                break;
        }
        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
    }

    private void SetupBuildAxis(RaycastHit hit)
    {
        Vector3 placementPoint = hit.transform.position;
        Quaternion placementRotation;
        Vector3 dirVector;

        switch (playerController.cubeloc)
        {
            case "up":
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y + 5, hit.transform.position.z);
                break;
            case "down":
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y - 5, hit.transform.position.z);
                break;
            case "left":
                placementPoint = new Vector3(hit.transform.position.x + 5, hit.transform.position.y, hit.transform.position.z);
                break;
            case "right":
                placementPoint = new Vector3(hit.transform.position.x - 5, hit.transform.position.y, hit.transform.position.z);
                break;
            case "front":
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 5);
                break;
            case "back":
                placementPoint = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 5);
                break;
        }

        placementRotation = playerController.buildObject.transform.rotation;
        playerController.buildObject.transform.position = placementPoint;
        playerController.buildObject.transform.rotation = placementRotation;
        dirLine.SetPosition(0, playerController.buildObject.transform.position);
        dirVector = playerController.buildObject.transform.position + playerController.buildObject.transform.forward * 4;
        dirLine.SetPosition(1, dirVector);
    }

    private void SetupFreePlacement(RaycastHit hit)
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


