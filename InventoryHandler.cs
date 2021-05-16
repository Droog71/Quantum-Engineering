using UnityEngine;
using System.Net;

public class InventoryHandler
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private InventorySlot slotDraggingFrom;
    private GuiCoordinates guiCoordinates;
    public string itemToDrag;
    private int amountToDrag;
    private int dragSlotIndex;

    //! This class handles inventory related functions that don't directly require the OnGUI method.
    public InventoryHandler(PlayerController playerController, InventoryManager playerInventory)
    {
        this.playerController = playerController;
        this.playerInventory = playerInventory;
        guiCoordinates = new GuiCoordinates();
    }

    //! Returns true if the item can be transferred from dragSlot to dropSlot.
    private bool CanTransfer(InventorySlot dragSlot, InventorySlot dropSlot)
    {
        return dropSlot.typeInSlot.Equals("nothing")
        || (dropSlot.typeInSlot.Equals(dragSlot.typeInSlot)
        && dropSlot.amountInSlot <= 1000 - dragSlot.amountInSlot);
    }

    //! Searches for items in all containers connected to the computer.
    public void SearchComputerContainers(StorageComputer computer, string term)
    {
        int containerCount = 0;
        foreach (InventoryManager manager in computer.computerContainers)
        {
            foreach (InventorySlot slot in manager.inventory)
            {
                if (term.Length < slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.Substring(0, term.Length).ToLower().Equals(term.ToLower()))
                    {
                        playerController.storageComputerInventory = containerCount;
                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                    }
                }
                else if (term.Length > slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.ToLower().Equals(term.Substring(0, slot.typeInSlot.Length).ToLower()))
                    {
                        playerController.storageComputerInventory = containerCount;
                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                    }
                }
                else if (term.Length == slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.ToLower().Equals(term.ToLower()))
                    {
                        playerController.storageComputerInventory = containerCount;
                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                    }
                }
            }
            containerCount++;
        }
    }

    //! Begins dragging an item from an inventory slot.
    public void DragItemFromSlot(int index, InventorySlot dragSlot, InventoryManager destination)
    {
        bool flag = false;
        dragSlotIndex = index;
        if (playerController.storageGUIopen == true)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                flag = true;
                for (int i = 0; i < destination.inventory.Length; i++)
                {
                    InventorySlot slot = destination.inventory[i];
                    if (CanTransfer(dragSlot, slot))
                    {
                        destination.AddItem(dragSlot.typeInSlot, dragSlot.amountInSlot);
                        dragSlot.typeInSlot = "nothing";
                        dragSlot.amountInSlot = 0;
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                        {
                            if (destination == playerController.playerInventory)
                            {
                                Vector3 pos = playerController.storageInventory.gameObject.transform.position;
                                float x = Mathf.Round(pos.x);
                                float y = Mathf.Round(pos.y); 
                                float z = Mathf.Round(pos.z); 
                                using (WebClient client = new WebClient())
                                {
                                    System.Uri uri = new System.Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                                    client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + index + ";" + dragSlot.typeInSlot + "=" + dragSlot.amountInSlot);
                                }
                            }
                            else
                            {
                                Vector3 pos = destination.gameObject.transform.position;
                                float x = Mathf.Round(pos.x);
                                float y = Mathf.Round(pos.y); 
                                float z = Mathf.Round(pos.z); 
                                using(WebClient client = new WebClient())
                                {
                                    System.Uri uri = new System.Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                                    client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+i+";"+slot.typeInSlot+"="+slot.amountInSlot);
                                }
                            }
                        }
                    }
                }
            }
        }
        if (flag == false)
        {
            playerController.draggingItem = true;
            itemToDrag = dragSlot.typeInSlot;
            slotDraggingFrom = dragSlot;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (dragSlot.amountInSlot > 1)
                {
                    amountToDrag = dragSlot.amountInSlot / 2;
                }
                else if (dragSlot.amountInSlot > 0)
                {
                    amountToDrag = dragSlot.amountInSlot;
                }
            }
            else if (dragSlot.amountInSlot > 0)
            {
                amountToDrag = dragSlot.amountInSlot;
            }
        }
    }

    //! Drops an item into an inventory slot.
    public void DropItemInSlot(Vector2 mousePos, bool usingContainer)
    {
        playerController.draggingItem = false;

        if (guiCoordinates.inventoryDropSlotRect.Contains(mousePos))
        {
            playerController.DropItem(slotDraggingFrom);
            return;
        }

        if (guiCoordinates.inventoryTrashSlotRect.Contains(mousePos))
        {
            slotDraggingFrom.amountInSlot = 0;
            slotDraggingFrom.typeInSlot = "nothing";
            playerController.PlayCraftingSound();
            return;
        }

        int inventoryDropSlot = 0;
        foreach (Rect rect in guiCoordinates.inventorySlotRects)
        {
            InventorySlot dropSlot = playerInventory.inventory[inventoryDropSlot];
            if (rect.Contains(mousePos) && slotDraggingFrom != dropSlot)
            {
                if (dropSlot.typeInSlot == "nothing" || dropSlot.typeInSlot == "" || dropSlot.typeInSlot == itemToDrag)
                {
                    if (dropSlot.amountInSlot <= playerInventory.maxStackSize - amountToDrag)
                    {
                        playerInventory.AddItemToSlot(itemToDrag, amountToDrag, inventoryDropSlot);
                        slotDraggingFrom.amountInSlot -= amountToDrag;
                        if (slotDraggingFrom.amountInSlot <= 0)
                        {
                            slotDraggingFrom.typeInSlot = "nothing";
                        }
                    }
                }
                else
                {
                    slotDraggingFrom.typeInSlot = dropSlot.typeInSlot;
                    slotDraggingFrom.amountInSlot = dropSlot.amountInSlot;
                    dropSlot.typeInSlot = itemToDrag;
                    dropSlot.amountInSlot = amountToDrag;
                }
                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                {
                    Vector3 pos = playerController.storageInventory.gameObject.transform.position;
                    float x = Mathf.Round(pos.x);
                    float y = Mathf.Round(pos.y); 
                    float z = Mathf.Round(pos.z); 
                    using(WebClient client = new WebClient())
                    {
                        System.Uri uri = new System.Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                        client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+dragSlotIndex+";"+slotDraggingFrom.typeInSlot+"="+slotDraggingFrom.amountInSlot);
                    }
                }
            }
            inventoryDropSlot++;
        }

        if (usingContainer == true)
        {
            int storageInventoryDropSlot = 0;
            foreach (Rect rect in guiCoordinates.storageInventorySlotRects)
            {
                InventorySlot dropSlot = playerController.storageInventory.inventory[storageInventoryDropSlot];
                if (rect.Contains(mousePos) && slotDraggingFrom != dropSlot)
                {
                    if (dropSlot.typeInSlot == "nothing" || dropSlot.typeInSlot == "" || dropSlot.typeInSlot == itemToDrag)
                    {
                        if (dropSlot.amountInSlot <= playerController.storageInventory.maxStackSize - amountToDrag)
                        {
                            playerController.storageInventory.AddItemToSlot(itemToDrag, amountToDrag, storageInventoryDropSlot);
                            slotDraggingFrom.amountInSlot -= amountToDrag;
                            if (slotDraggingFrom.amountInSlot <= 0)
                            {
                                slotDraggingFrom.typeInSlot = "nothing";
                            }
                        }
                    }
                    else
                    {
                        slotDraggingFrom.typeInSlot = dropSlot.typeInSlot;
                        slotDraggingFrom.amountInSlot = dropSlot.amountInSlot;
                        dropSlot.typeInSlot = itemToDrag;
                        dropSlot.amountInSlot = amountToDrag;
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && PlayerPrefsX.GetPersistentBool("hosting") == false)
                    {
                        using(WebClient client = new WebClient())
                        {
                            System.Uri uri = new System.Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                            Vector3 pos = playerController.storageInventory.gameObject.transform.position;
                            float x = Mathf.Round(pos.x);
                            float y = Mathf.Round(pos.y); 
                            float z = Mathf.Round(pos.z); 
                            client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+storageInventoryDropSlot+";"+dropSlot.typeInSlot+"="+dropSlot.amountInSlot);
                        }
                    }
                }
                storageInventoryDropSlot++;
            }
        }
    }
}
