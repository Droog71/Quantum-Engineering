using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private InteractionController interactionController;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionController = GetComponent<InteractionController>();
    }

    public void InteractWithStorageContainer()
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
                playerController.PlayCraftingSound();
            }
            else
            {
                playerController.cannotCollect = true;
                playerController.PlayCraftingSound();
            }
        }
    }

    public void InteractWithStorageComputer()
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
            interactionController.CollectObject("Storage Computer");
        }
    }
}
