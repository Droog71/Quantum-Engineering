using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        //RAYCAST AND ASSOCIATED DATA FOR INTERACTING WITH MACHINES AND OTHER OBJECTS
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out playerController.playerLookHit, 40) && playerController.inventoryOpen == false && playerController.escapeMenuOpen == false && playerController.tabletOpen == false)
        {
            playerController.objectInSight = playerController.playerLookHit.collider.gameObject;
            if (playerController.objectInSight.GetComponent<DarkMatter>() != null)
            {
                //Display info.
            }
            else if (playerController.objectInSight.GetComponent<Iron>() != null)
            {
                //Display info.
            }
            else if (playerController.objectInSight.GetComponent<UniversalResource>() != null)
            {
                //Display info.
            }
            else if (playerController.objectInSight.GetComponent<InventoryManager>() != null && playerController.objectInSight.GetComponent<Retriever>() == null && playerController.objectInSight.GetComponent<AutoCrafter>() == null)
            {
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.storageGUIopen == false)
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
                        if (playerController.objectInSight.GetComponent<InventoryManager>().initialized == true)
                        {
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;
                            playerController.storageGUIopen = true;
                            playerController.craftingGUIopen = false;
                            playerController.machineGUIopen = false;
                            playerController.inventoryOpen = true;
                            playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
                            playerController.remoteStorageActive = false;
                        }
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.machineGUIopen = false;
                        playerController.storageGUIopen = false;
                    }
                }
                if (cInput.GetKeyDown("Collect Object") && playerController.objectInSight.GetComponent<InventoryManager>().ID != "Rocket" && playerController.objectInSight.GetComponent<InventoryManager>().ID != "Lander")
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Storage Container") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        InventoryManager thisContainer = playerController.objectInSight.GetComponent<InventoryManager>();
                        foreach (InventorySlot slot in thisContainer.inventory)
                        {
                            slot.typeInSlot = "nothing";
                            slot.amountInSlot = 0;
                        }
                        thisContainer.SaveData();
                        if (playerController.objectInSight.GetComponent<RailCart>() != null)
                        {
                            playerController.playerInventory.AddItem("Rail Cart", 1);
                        }
                        else
                        {
                            playerController.playerInventory.AddItem("Storage Container", 1);
                        }
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<StorageComputer>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<StorageComputer>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<StorageComputer>().powerON;
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.storageGUIopen == false)
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
                        if (playerController.objectInSight.GetComponent<StorageComputer>().powerON == true && playerController.objectInSight.GetComponent<StorageComputer>().initialized == true)
                        {
                            bool foundContainer = false;
                            int containerCount = 0;
                            foreach (InventoryManager manager in playerController.objectInSight.GetComponent<StorageComputer>().computerContainers)
                            {
                                if (foundContainer == false)
                                {
                                    if (playerController.objectInSight.GetComponent<StorageComputer>().computerContainers[containerCount] != null)
                                    {
                                        Cursor.visible = true;
                                        Cursor.lockState = CursorLockMode.None;
                                        playerController.storageGUIopen = true;
                                        playerController.craftingGUIopen = false;
                                        playerController.machineGUIopen = false;
                                        playerController.inventoryOpen = true;
                                        playerController.storageInventory = playerController.objectInSight.GetComponent<StorageComputer>().computerContainers[0];
                                        playerController.currentStorageComputer = playerController.objectInSight;
                                        playerController.remoteStorageActive = true;
                                        foundContainer = true;
                                    }
                                    containerCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.machineGUIopen = false;
                        playerController.storageGUIopen = false;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Storage Computer") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Storage Computer", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<PowerSource>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<PowerSource>().ID;
                playerController.machineOutputID = playerController.objectInSight.GetComponent<PowerSource>().outputID;
                if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Solar Panel"))
                {
                    if (playerController.objectInSight.GetComponent<PowerSource>().blocked == false)
                    {
                        playerController.machinePower = 1;
                    }
                    else
                    {
                        playerController.machinePower = 0;
                    }
                }
                else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Generator"))
                {
                    playerController.machineAmount = playerController.objectInSight.GetComponent<PowerSource>().fuelAmount;
                    playerController.machineType = playerController.objectInSight.GetComponent<PowerSource>().fuelType;
                    if (playerController.objectInSight.GetComponent<PowerSource>().outOfFuel == false)
                    {
                        playerController.machinePower = 10;
                    }
                    else
                    {
                        playerController.machinePower = 0;
                    }
                }
                else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                {
                    if (playerController.objectInSight.GetComponent<PowerSource>().noReactor == false)
                    {
                        playerController.machinePower = 100;
                    }
                    else
                    {
                        playerController.machinePower = 0;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.objectInSight.GetComponent<PowerSource>().type) && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Solar Panel"))
                        {
                            playerController.playerInventory.AddItem("Solar Panel", 1);
                        }
                        else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Generator"))
                        {
                            playerController.playerInventory.AddItem("Generator", 1);
                        }
                        else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
                        {
                            playerController.playerInventory.AddItem("Reactor Turbine", 1);
                        }
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<NuclearReactor>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<NuclearReactor>().ID;
                playerController.machineCooling = playerController.objectInSight.GetComponent<NuclearReactor>().cooling;
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Nuclear Reactor") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Nuclear Reactor", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<PowerConduit>().ID;
                playerController.machineOutputID = playerController.objectInSight.GetComponent<PowerConduit>().outputID1;
                playerController.machineOutputID2 = playerController.objectInSight.GetComponent<PowerConduit>().outputID2;
                playerController.machinePower = playerController.objectInSight.GetComponent<PowerConduit>().powerAmount;
                playerController.machineRange = playerController.objectInSight.GetComponent<PowerConduit>().range;
                if (playerController.machineRange < 10)
                {
                    playerController.machineRange = 10;
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Power Conduit") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Power Conduit", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Turret>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<Turret>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Turret>().powerON;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Turret>().speed;
                playerController.machinePower = playerController.objectInSight.GetComponent<Turret>().power;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Turret>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Turret>().cooling;
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Turret") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Turret", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<UniversalExtractor>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<UniversalExtractor>().ID;
                playerController.collectorAmount = playerController.objectInSight.GetComponent<UniversalExtractor>().amount;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<UniversalExtractor>().powerON;
                playerController.machinePower = playerController.objectInSight.GetComponent<UniversalExtractor>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<UniversalExtractor>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<UniversalExtractor>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<UniversalExtractor>().cooling;
                playerController.machineType = playerController.objectInSight.GetComponent<UniversalExtractor>().type;
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Universal Extractor") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Universal Extractor", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Auger>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<Auger>().ID;
                playerController.collectorAmount = playerController.objectInSight.GetComponent<Auger>().amount;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Auger>().powerON;
                playerController.machinePower = playerController.objectInSight.GetComponent<Auger>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Auger>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Auger>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Auger>().cooling;
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Auger") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Auger", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<DarkMatterCollector>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<DarkMatterCollector>().ID;
                playerController.collectorAmount = playerController.objectInSight.GetComponent<DarkMatterCollector>().darkMatterAmount;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<DarkMatterCollector>().powerON;
                playerController.machinePower = playerController.objectInSight.GetComponent<DarkMatterCollector>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<DarkMatterCollector>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<DarkMatterCollector>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<DarkMatterCollector>().cooling;
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Dark Matter Collector") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Dark Matter Collector", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<UniversalConduit>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<UniversalConduit>().ID;
                playerController.machineType = playerController.objectInSight.GetComponent<UniversalConduit>().type;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<UniversalConduit>().speed;
                playerController.machineRange = playerController.objectInSight.GetComponent<UniversalConduit>().range;
                if (playerController.machineRange < 10)
                {
                    playerController.machineRange = 10;
                }
                if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().type;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>().amount;
                        playerController.machineInputType = "Regolith";
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().outputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().outputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().outputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().outputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>().ID;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>().currentType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().type;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().outputType;
                    }
                }
                if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().inputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().inputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().inputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().inputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().inputType;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().amount;
                        if (playerController.objectInSight.GetComponent<UniversalConduit>().type.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType1))
                        {
                            playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType1;
                        }
                        else if (playerController.objectInSight.GetComponent<UniversalConduit>().type.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType2))
                        {
                            playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType2;
                        }
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>().ID;
                        int storageTotal = 0;
                        foreach (InventorySlot slot in playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>().inventory)
                        {
                            if (slot.typeInSlot.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().type))
                            {
                                storageTotal += slot.amountInSlot;
                                playerController.machineOutputType = slot.typeInSlot;
                            }
                        }
                        playerController.machineOutputAmount = storageTotal;
                    }
                    if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>().ID;
                        int storageTotal = 0;
                        foreach (InventoryManager manager in playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>().computerContainers)
                        {
                            foreach (InventorySlot slot in manager.inventory)
                            {
                                if (slot.typeInSlot.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().type))
                                {
                                    storageTotal += slot.amountInSlot;
                                    playerController.machineOutputType = slot.typeInSlot;
                                }
                            }
                        }
                        playerController.machineOutputAmount = storageTotal;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Universal Conduit") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Universal Conduit", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<DarkMatterConduit>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().darkMatterAmount;
                playerController.machineID = playerController.objectInSight.GetComponent<DarkMatterConduit>().ID;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<DarkMatterConduit>().speed;
                playerController.machineRange = playerController.objectInSight.GetComponent<DarkMatterConduit>().range;
                if (playerController.machineRange < 10)
                {
                    playerController.machineRange = 10;
                }
                if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                        playerController.machineInputType = "Dark Matter";
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>().darkMatterAmount;
                        playerController.machineInputType = "Dark Matter";
                    }
                }
                if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                        playerController.machineOutputType = "Dark Matter";
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>().ID;
                        int storageTotal = 0;
                        foreach (InventoryManager manager in playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>().computerContainers)
                        {
                            foreach (InventorySlot slot in manager.inventory)
                            {
                                if (slot.typeInSlot.Equals("Dark Matter"))
                                {
                                    storageTotal += slot.amountInSlot;
                                    playerController.machineOutputType = "Dark Matter";
                                }
                            }
                        }
                        playerController.machineOutputAmount = storageTotal;
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>().ID;
                        int storageTotal = 0;
                        foreach (InventorySlot slot in playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>().inventory)
                        {
                            if (slot.typeInSlot.Equals("Dark Matter"))
                            {
                                storageTotal += slot.amountInSlot;
                                playerController.machineOutputType = "Dark Matter";
                            }
                        }
                        playerController.machineOutputAmount = storageTotal;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Dark Matter Conduit") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Dark Matter Conduit", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Smelter>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<Smelter>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<Smelter>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Smelter>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<Smelter>().inputType;
                playerController.machinePower = playerController.objectInSight.GetComponent<Smelter>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Smelter>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Smelter>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Smelter>().cooling;
                if (playerController.objectInSight.GetComponent<Smelter>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<Smelter>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Smelter") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Smelter", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<AlloySmelter>().amount;
                playerController.machineAmount2 = playerController.objectInSight.GetComponent<AlloySmelter>().amount2;
                playerController.machineID = playerController.objectInSight.GetComponent<AlloySmelter>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<AlloySmelter>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<AlloySmelter>().inputType1;
                playerController.machineType2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputType2;
                playerController.machinePower = playerController.objectInSight.GetComponent<AlloySmelter>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<AlloySmelter>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<AlloySmelter>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<AlloySmelter>().cooling;
                if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1 != null)
                {
                    if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2 != null)
                {
                    if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<AlloySmelter>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Alloy Smelter") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Alloy Smelter", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Extruder>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<Extruder>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<Extruder>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Extruder>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<Extruder>().inputType;
                playerController.machinePower = playerController.objectInSight.GetComponent<Extruder>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Extruder>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Extruder>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Extruder>().cooling;
                if (playerController.objectInSight.GetComponent<Extruder>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<Extruder>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Extruder") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Extruder", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<RailCartHub>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<RailCartHub>().ID;
                playerController.machineRange = playerController.objectInSight.GetComponent<RailCartHub>().range;
                if (playerController.machineRange < 10)
                {
                    playerController.machineRange = 10;
                }
                if (playerController.objectInSight.GetComponent<RailCartHub>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<RailCartHub>().inputObject.GetComponent<RailCartHub>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<RailCartHub>().inputObject.GetComponent<RailCartHub>().ID;
                    }
                }
                if (playerController.objectInSight.GetComponent<RailCartHub>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<RailCartHub>().outputObject.GetComponent<RailCartHub>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<RailCartHub>().outputObject.GetComponent<RailCartHub>().ID;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Rail Cart Hub") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Rail Cart Hub", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
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
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        playerController.storageGUIopen = false;
                        playerController.craftingGUIopen = false;
                        playerController.inventoryOpen = false;
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Retriever>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<Retriever>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Retriever>().powerON;
                if (playerController.objectInSight.GetComponent<Retriever>().type.Count > 1)
                {
                    playerController.machineType = "multiple items";
                }
                else if (playerController.objectInSight.GetComponent<Retriever>().type.Count > 0)
                {
                    playerController.machineType = playerController.objectInSight.GetComponent<Retriever>().type[0];
                }
                else
                {
                    playerController.machineType = "nothing";
                }
                playerController.machinePower = playerController.objectInSight.GetComponent<Retriever>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Retriever>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Retriever>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Retriever>().cooling;
                playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
                if (playerController.objectInSight.GetComponent<Retriever>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<InventoryManager>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<InventoryManager>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<StorageComputer>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<StorageComputer>().ID;
                    }
                }
                if (playerController.objectInSight.GetComponent<Retriever>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Retriever") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Retriever", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                        playerController.remoteStorageActive = false;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineID = playerController.objectInSight.GetComponent<AutoCrafter>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<AutoCrafter>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<AutoCrafter>().type;
                playerController.machinePower = playerController.objectInSight.GetComponent<AutoCrafter>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<AutoCrafter>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<AutoCrafter>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<AutoCrafter>().cooling;
                playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
                if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<InventoryManager>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<InventoryManager>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<StorageComputer>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<StorageComputer>().ID;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Auto Crafter") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Auto Crafter", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                        playerController.remoteStorageActive = false;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<HeatExchanger>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<HeatExchanger>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<HeatExchanger>().ID;
                playerController.machineType = playerController.objectInSight.GetComponent<HeatExchanger>().inputType;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<HeatExchanger>().speed;
                if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<UniversalExtractor>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<UniversalExtractor>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<DarkMatterCollector>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<DarkMatterCollector>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Auger>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Auger>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Smelter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Smelter>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Extruder>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Extruder>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Retriever>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Retriever>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AutoCrafter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AutoCrafter>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Press>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Press>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AlloySmelter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AlloySmelter>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<GearCutter>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<GearCutter>().ID;
                    }
                    if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Turret>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Turret>().ID;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Heat Exchanger") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Heat Exchanger", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<GearCutter>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<GearCutter>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<GearCutter>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<GearCutter>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<GearCutter>().inputType;
                playerController.machinePower = playerController.objectInSight.GetComponent<GearCutter>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<GearCutter>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<GearCutter>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<GearCutter>().cooling;
                if (playerController.objectInSight.GetComponent<GearCutter>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<GearCutter>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Gear Cutter") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Gear Cutter", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Press>() != null)
            {
                playerController.machineInSight = playerController.objectInSight;
                playerController.machineAmount = playerController.objectInSight.GetComponent<Press>().amount;
                playerController.machineID = playerController.objectInSight.GetComponent<Press>().ID;
                playerController.machineHasPower = playerController.objectInSight.GetComponent<Press>().powerON;
                playerController.machineType = playerController.objectInSight.GetComponent<Press>().inputType;
                playerController.machinePower = playerController.objectInSight.GetComponent<Press>().power;
                playerController.machineSpeed = playerController.objectInSight.GetComponent<Press>().speed;
                playerController.machineHeat = playerController.objectInSight.GetComponent<Press>().heat;
                playerController.machineCooling = playerController.objectInSight.GetComponent<Press>().cooling;
                if (playerController.objectInSight.GetComponent<Press>().inputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineInputID = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineInputAmount = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineInputType = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (playerController.objectInSight.GetComponent<Press>().outputObject != null)
                {
                    if (playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>() != null)
                    {
                        playerController.machineOutputID = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().ID;
                        playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().amount;
                        playerController.machineOutputType = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().type;
                    }
                }
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Press") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Press", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    if (playerController.machineGUIopen == false)
                    {
                        playerController.machineGUIopen = true;
                    }
                    else
                    {
                        playerController.machineGUIopen = false;
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<IronBlock>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    string objectName = "";
                    if (playerController.objectInSight.name.Equals("IronRamp(Clone)"))
                    {
                        objectName = "Iron Ramp";

                    }
                    else
                    {
                        objectName = "Iron Block";
                    }

                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(objectName) && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }

                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem(objectName, 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                        playerController.destroyTimer = 0;
                        playerController.buildTimer = 0;
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Steel>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    string objectName = "";
                    if (playerController.objectInSight.name.Equals("SteelRamp(Clone)"))
                    {
                        objectName = "Steel Ramp";
                    }
                    else
                    {
                        objectName = "Steel Block";
                    }

                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(objectName) && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }

                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem(objectName, 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                        playerController.destroyTimer = 0;
                        playerController.buildTimer = 0;
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Glass>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Glass Block") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Glass Block", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                        playerController.destroyTimer = 0;
                        playerController.buildTimer = 0;
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<Brick>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Brick") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Brick", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                        playerController.destroyTimer = 0;
                        playerController.buildTimer = 0;
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<ElectricLight>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Electric Light") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Electric Light", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.objectInSight.tag.Equals("CombinedMesh"))
            {
                playerController.lookingAtCombinedMesh = true;
                if (cInput.GetKeyDown("Collect Object") && playerController.paintGunActive == false)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all",true);
                        playerController.separatedBlocks = true;
                    }
                    else
                    {
                        playerController.requestedChunkLoad = true;
                    }
                    if (playerController.building == false)
                    {
                        playerController.destroying = true;
                        playerController.destroyStartPosition = transform.position;
                    }
                }
                if (playerController.paintGunActive == true && playerController.paintColorSelected == true)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        playerController.paintGun.GetComponent<AudioSource>().Play();
                        playerController.objectInSight.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                        if (playerController.objectInSight.name.Equals("brickHolder(Clone)"))
                        {
                            PlayerPrefsX.SetBool(playerController.stateManager.WorldName + "brickHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                        }
                        if (playerController.objectInSight.name.Equals("glassHolder(Clone)"))
                        {
                            PlayerPrefsX.SetBool(playerController.stateManager.WorldName + "glassHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                        }
                        if (playerController.objectInSight.name.Equals("ironHolder(Clone)"))
                        {
                            PlayerPrefsX.SetBool(playerController.stateManager.WorldName + "ironHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                        }
                        if (playerController.objectInSight.name.Equals("steelHolder(Clone)"))
                        {
                            PlayerPrefsX.SetBool(playerController.stateManager.WorldName + "steelHolder" + playerController.objectInSight.GetComponent<MeshPainter>().ID + "painted", true);
                        }
                    }
                }
            }
            else if (playerController.objectInSight.GetComponent<AirLock>() != null)
            {
                if (cInput.GetKeyDown("Collect Object"))
                {
                    bool spaceAvailable = false;
                    foreach (InventorySlot slot in playerController.playerInventory.inventory)
                    {
                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Quantum Hatchway") && slot.amountInSlot < 1000)
                        {
                            spaceAvailable = true;
                        }
                    }
                    if (spaceAvailable == true)
                    {
                        playerController.playerInventory.AddItem("Quantum Hatchway", 1);
                        Destroy(playerController.objectInSight);
                        playerController.guiSound.volume = 0.3f;
                        playerController.guiSound.clip = playerController.craftingClip;
                        playerController.guiSound.Play();
                    }
                    else
                    {
                        playerController.cannotCollect = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.missingItemsClip;
                        playerController.guiSound.Play();
                    }
                }
                if (cInput.GetKeyDown("Interact"))
                {
                    AirLock[] airLocks = FindObjectsOfType<AirLock>();
                    foreach (AirLock a in airLocks)
                    {
                        if (Vector3.Distance(transform.position, a.transform.position) < 40)
                        {
                            a.ToggleOpen();
                        }
                    }
                    if (playerController.objectInSight.GetComponent<AirLock>().open == true)
                    {
                        playerController.objectInSight.GetComponent<AirLock>().openObject.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        playerController.objectInSight.GetComponent<AirLock>().closedObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                if (playerController.machineGUIopen == true)
                {
                    playerController.machineGUIopen = false;
                }
                playerController.lookingAtCombinedMesh = false;
                playerController.objectInSight = null;
                playerController.machineInSight = null;
                playerController.machineInputID = "none";
                playerController.machineOutputID = "none";
                playerController.machineType = "none";
                playerController.machineAmount = 0;
                playerController.machineInputAmount = 0;
                playerController.machineOutputAmount = 0;
            }
        }
        else
        {
            if (playerController.machineGUIopen == true)
            {
                playerController.machineGUIopen = false;
            }
            playerController.lookingAtCombinedMesh = false;
            playerController.objectInSight = null;
            playerController.machineInSight = null;
            playerController.machineInputID = "none";
            playerController.machineOutputID = "none";
            playerController.machineType = "none";
            playerController.machineAmount = 0;
            playerController.machineInputAmount = 0;
            playerController.machineOutputAmount = 0;
        }
    }
}

