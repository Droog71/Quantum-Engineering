using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildController : MonoBehaviour
{
    private PlayerController playerController;
    LineRenderer dirLine;
    public Material lineMat;
    public GameObject builtObjects;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        builtObjects = GameObject.Find("Built_Objects");
    }

    void Update()
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
                        if (playerController.buildType == "Steel Ramp")
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
                        if (playerController.buildType == "Iron Block")
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
                        if (playerController.buildType == "Iron Ramp")
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
                        if (playerController.buildType == "Glass Block")
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
                        if (playerController.buildType == "Brick")
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
                        if (playerController.buildType == "Electric Light")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Electric Light"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.electricLight, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<ElectricLight>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Storage Container")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Storage Container"))
                                            {
                                                foundItems = true;
                                                Instantiate(playerController.storageContainer, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Storage Computer")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Storage Computer"))
                                            {
                                                foundItems = true;
                                                Instantiate(playerController.storageComputer, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Dark Matter Collector")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Dark Matter Collector"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.darkMatterCollector, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<DarkMatterCollector>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Dark Matter Conduit")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Dark Matter Conduit"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.darkMatterConduit, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<DarkMatterConduit>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Universal Extractor")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Universal Extractor"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.universalExtractor, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<UniversalExtractor>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Quantum Hatchway")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Quantum Hatchway"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.airlock, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Auger")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Auger"))
                                            {
                                                foundItems = true;
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
                                                if (hit.collider.gameObject.tag.Equals("Landscape"))
                                                {
                                                    GameObject builtObject = Instantiate(playerController.auger, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                    builtObject.GetComponent<Auger>().creationMethod = "built";
                                                    slot.amountInSlot -= 1;
                                                    playerController.builderSound.Play();
                                                }
                                                else
                                                {
                                                    playerController.invalidAugerPlacement = true;
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
                        }
                        if (playerController.buildType == "Universal Conduit")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Universal Conduit"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.universalConduit, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<UniversalConduit>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Smelter")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Smelter"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.smelter, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<Smelter>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Solar Panel")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Solar Panel"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.solarPanel, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<PowerSource>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Generator")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Generator"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.generator, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<PowerSource>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Reactor Turbine")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Reactor Turbine"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.reactorTurbine, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<PowerSource>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Power Conduit")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Power Conduit"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.powerConduit, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<PowerConduit>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Nuclear Reactor")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Nuclear Reactor"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.nuclearReactor, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<NuclearReactor>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Alloy Smelter")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Alloy Smelter"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.alloySmelter, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<AlloySmelter>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Extruder")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Extruder"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.extruder, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<Extruder>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Retriever")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Retriever"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.retriever, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<Retriever>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Rail Cart")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Rail Cart"))
                                            {
                                                foundItems = true;
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
                                                if (hit.collider.gameObject.GetComponent<RailCartHub>() != null)
                                                {
                                                    GameObject builtObject = Instantiate(playerController.railCart, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                    builtObject.GetComponent<RailCart>().creationMethod = "built";
                                                    builtObject.GetComponent<RailCart>().target = hit.collider.gameObject;
                                                    slot.amountInSlot -= 1;
                                                    playerController.builderSound.Play();
                                                }
                                                else
                                                {
                                                    playerController.invalidRailCartPlacement = true;
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
                        }
                        if (playerController.buildType == "Rail Cart Hub")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Rail Cart Hub"))
                                            {
                                                foundItems = true;
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
                                                GameObject builtObject = Instantiate(playerController.railCartHub, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<RailCartHub>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
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
                        if (playerController.buildType == "Auto Crafter")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Auto Crafter"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.autoCrafter, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<AutoCrafter>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Heat Exchanger")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Heat Exchanger"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.heatExchanger, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<HeatExchanger>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Gear Cutter")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Gear Cutter"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.gearCutter, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<GearCutter>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Press")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Press"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.press, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<Press>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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
                        if (playerController.buildType == "Turret")
                        {
                            if (cInput.GetKeyDown("Place Object"))
                            {
                                bool foundItems = false;
                                foreach (InventorySlot slot in playerController.playerInventory.inventory)
                                {
                                    if (foundItems == false)
                                    {
                                        if (slot.amountInSlot > 0)
                                        {
                                            if (slot.typeInSlot.Equals("Turret"))
                                            {
                                                foundItems = true;
                                                GameObject builtObject = Instantiate(playerController.turret, playerController.buildObject.transform.position, playerController.buildObject.transform.rotation);
                                                builtObject.GetComponent<Turret>().creationMethod = "built";
                                                slot.amountInSlot -= 1;
                                                playerController.builderSound.Play();
                                                playerController.destroyTimer = 0;
                                                playerController.buildTimer = 0;
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


