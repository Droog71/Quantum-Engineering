using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private PlayerController playerController;
    private BlockDictionary blockDictionary;
    private MachineInteraction machineInteraction;
    private StorageInteraction storageInteraction;
    private BlockInteraction blockInteraction;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        blockDictionary = new BlockDictionary(playerController);
        machineInteraction = gameObject.AddComponent<MachineInteraction>();
        storageInteraction = gameObject.AddComponent<StorageInteraction>();
        blockInteraction = gameObject.AddComponent<BlockInteraction>();
    }

    // Called once per frame by unity engine
    public void Update()
    {
        //RAYCAST AND ASSOCIATED DATA FOR INTERACTING WITH MACHINES AND OTHER OBJECTS
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out playerController.playerLookHit, 40))
        {
            if (playerController.inventoryOpen == false && playerController.escapeMenuOpen == false && playerController.tabletOpen == false)
            {
                playerController.objectInSight = playerController.playerLookHit.collider.gameObject;
                if (!IsResource(playerController.objectInSight))
                {
                    if (IsStorageContainer(playerController.objectInSight))
                    {
                        storageInteraction.InteractWithStorageContainer();
                    }
                    else if (playerController.objectInSight.GetComponent<StorageComputer>() != null)
                    {
                        storageInteraction.InteractWithStorageComputer();
                    }
                    else if (playerController.objectInSight.GetComponent<PowerSource>() != null)
                    {
                        machineInteraction.InteractWithPowerSource();
                    }
                    else if (playerController.objectInSight.GetComponent<NuclearReactor>() != null)
                    {
                        machineInteraction.InteractWithNuclearReactor();
                    }
                    else if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                    {
                        machineInteraction.InteractWithPowerConduit();
                    }
                    else if (playerController.objectInSight.GetComponent<Turret>() != null)
                    {
                        machineInteraction.InteractWithTurret();
                    }
                    else if (playerController.objectInSight.GetComponent<UniversalExtractor>() != null)
                    {
                        machineInteraction.InteractWithUniversalExtractor();
                    }
                    else if (playerController.objectInSight.GetComponent<Auger>() != null)
                    {
                        machineInteraction.InteractWithAuger();
                    }
                    else if (playerController.objectInSight.GetComponent<DarkMatterCollector>() != null)
                    {
                        machineInteraction.InteractWithDarkMatterCollector();
                    }
                    else if (playerController.objectInSight.GetComponent<UniversalConduit>() != null)
                    {
                        machineInteraction.InteractWithUniversalConduit();
                    }
                    else if (playerController.objectInSight.GetComponent<DarkMatterConduit>() != null)
                    {
                        machineInteraction.InteractWithDarkMatterConduit();
                    }
                    else if (playerController.objectInSight.GetComponent<Smelter>() != null)
                    {
                        machineInteraction.InteractWithSmelter();
                    }
                    else if (playerController.objectInSight.GetComponent<AlloySmelter>() != null)
                    {
                        machineInteraction.InteractWithAlloySmelter();
                    }
                    else if (playerController.objectInSight.GetComponent<Extruder>() != null)
                    {
                        machineInteraction.InteractWithExtruder();
                    }
                    else if (playerController.objectInSight.GetComponent<RailCartHub>() != null)
                    {
                        machineInteraction.InteractWithRailCartHub();
                    }
                    else if (playerController.objectInSight.GetComponent<Retriever>() != null)
                    {
                        machineInteraction.InteractWithRetriever();
                    }
                    else if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                    {
                        machineInteraction.InteractWithAutoCrafter();
                    }
                    else if (playerController.objectInSight.GetComponent<HeatExchanger>() != null)
                    {
                        machineInteraction.InteractWithHeatExchanger();
                    }
                    else if (playerController.objectInSight.GetComponent<GearCutter>() != null)
                    {
                        machineInteraction.InteractWithGearCutter();
                    }
                    else if (playerController.objectInSight.GetComponent<Press>() != null)
                    {
                        machineInteraction.InteractWithPress();
                    }
                    else if (playerController.objectInSight.GetComponent<ElectricLight>() != null)
                    {
                        machineInteraction.InteractWithElectricLight();
                    }
                    else if (playerController.objectInSight.GetComponent<AirLock>() != null)
                    {
                        machineInteraction.InteractWithAirLock();
                    }
                    else if (playerController.objectInSight.GetComponent<IronBlock>() != null)
                    {
                        blockInteraction.InteractWithIronBlock();
                    }
                    else if (playerController.objectInSight.GetComponent<Steel>() != null)
                    {
                        blockInteraction.InteractWithSteelBlock();
                    }
                    else if (playerController.objectInSight.GetComponent<Glass>() != null)
                    {
                        blockInteraction.InteractWithGlass();
                    }
                    else if (playerController.objectInSight.GetComponent<Brick>() != null)
                    {
                        blockInteraction.InteractWithBricks();
                    }
                    else if (playerController.objectInSight.tag.Equals("CombinedMesh"))
                    {
                        blockInteraction.InteractWithCombinedMesh();
                    }
                    else
                    {
                        EndInteraction();
                    }
                }
            }
        }
        else
        {
            EndInteraction();
        }
    }

    // Returns true if the object in question is a resource node
    private Boolean IsResource(GameObject obj)
    {
        if (obj.GetComponent<DarkMatter>() != null || obj.GetComponent<UniversalResource>() != null)
        {
            return true;
        }
        return false;
    }

    // Returns true if the object in question is a storage container
    private Boolean IsStorageContainer(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null && obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null)
        {
            return true;
        }
        return false;
    }

    // Destroys an object in the world and adds it's associated inventory item to the player's inventory
    public void CollectObject(string type)
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerController.playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(type) && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            playerController.playerInventory.AddItem(type, 1);
            Destroy(playerController.objectInSight);
            playerController.PlayCraftingSound();
        }
        else
        {
            playerController.cannotCollect = true;
            playerController.PlayMissingItemsSound();
        }
    }

    // Opens the machine GUI
    public void OpenMachineGUI()
    {
        if (playerController.machineGUIopen == false)
        {
            if (playerController.building == true)
            {
                GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (manager.working == false)
                {
                    playerController.stoppingBuildCoRoutine = true;
                    manager.meshManager.CombineBlocks();
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

    // Called when the player is no longer looking at any interactive objects
    private void EndInteraction()
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

