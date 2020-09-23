using UnityEngine;

public class StorageInteraction
{
    private PlayerController playerController;
    private InteractionController interactionController;

    //! This class handles the player's interactions with storage containers.
    public StorageInteraction(PlayerController playerController, InteractionController interactionController)
    {
        this.playerController = playerController;
        this.interactionController = interactionController;
    }

    //! Called when the player is looking at a storage container.
    public void InteractWithStorageContainer()
    {
        InventoryManager inventory = playerController.objectInSight.GetComponent<InventoryManager>();
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.storageGUIopen == false)
            {
                if (playerController.building == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        playerController.stoppingBuildCoRoutine = true;
                        playerController.gameManager.meshManager.CombineBlocks();
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
                if (inventory.initialized == true)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    playerController.storageGUIopen = true;
                    playerController.craftingGUIopen = false;
                    playerController.machineGUIopen = false;
                    playerController.inventoryOpen = true;
                    playerController.storageInventory = inventory;
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
        if (cInput.GetKeyDown("Collect Object") && inventory.ID != "Rocket" && inventory.ID != "Lander")
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
                InventoryManager thisContainer = inventory;
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
                Object.Destroy(playerController.objectInSight);
                playerController.PlayCraftingSound();
            }
            else
            {
                playerController.cannotCollect = true;
                playerController.PlayCraftingSound();
            }
        }
    }

    //! Called when the player is looking at a storage computer.
    public void InteractWithStorageComputer()
    {
        playerController.machineInSight = playerController.objectInSight;
        StorageComputer computer = playerController.objectInSight.GetComponent<StorageComputer>();
        playerController.machineID = computer.ID;
        playerController.machineHasPower = computer.powerON;
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.storageGUIopen == false)
            {
                if (playerController.building == true)
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().working == false)
                    {
                        playerController.stoppingBuildCoRoutine = true;
                        playerController.gameManager.meshManager.CombineBlocks();
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
                if (computer.powerON == true && computer.initialized == true)
                {
                    bool foundContainer = false;
                    int containerCount = 0;
                    foreach (InventoryManager manager in computer.computerContainers)
                    {
                        if (foundContainer == false)
                        {
                            if (computer.computerContainers[containerCount] != null)
                            {
                                Cursor.visible = true;
                                Cursor.lockState = CursorLockMode.None;
                                playerController.storageGUIopen = true;
                                playerController.craftingGUIopen = false;
                                playerController.machineGUIopen = false;
                                playerController.inventoryOpen = true;
                                playerController.storageInventory = computer.computerContainers[0];
                                playerController.currentStorageComputer = playerController.objectInSight;
                                playerController.remoteStorageActive = true;
                                foundContainer = true;
                            }
                            containerCount++;
                        }
                    }
                    if (foundContainer == false)
                    {
                        computer.Reboot();
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
            interactionController.CollectObject("Storage Computer");
        }
    }
}