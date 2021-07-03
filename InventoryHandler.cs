using UnityEngine;
using System.Net;
using System.Collections.Generic;
using System;
using MEC;

public class InventoryHandler
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private InventorySlot slotDraggingFrom;
    private GuiCoordinates guiCoordinates;
    private bool networkContainerCoroutineBusy;
    private bool networkTransferCoroutineBusy;
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

    //! Updates the server when items are moved from a storage container to the trash or dropped on the ground.
    private void NetworkRemoveItem()
    {
        bool flag = false;
        if (playerController.storageInventory != null)
        {
            foreach (InventorySlot slot in playerController.storageInventory.inventory)
            {
                if (slot == slotDraggingFrom)
                {
                    flag = true;
                    break;
                }
            }
        }
        if (flag == true)
        {
            using (WebClient client = new WebClient())
            {
                Vector3 pos = playerController.storageInventory.gameObject.transform.position;
                float x = Mathf.Round(pos.x);
                float y = Mathf.Round(pos.y);
                float z = Mathf.Round(pos.z);
                Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + dragSlotIndex + ";" + slotDraggingFrom.typeInSlot + "=" + slotDraggingFrom.amountInSlot);
            }
        }
    }

    //! When an item is moved from one slot to another, within the same container, the contents of both slots are updated on the server.
    private IEnumerator<float> NetworkContainerCoroutine(int storageInventoryDropSlot, InventorySlot dropSlot, string originType, int originAmount)
    {
        networkContainerCoroutineBusy = true;
        using(WebClient client = new WebClient())
        {
            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/storage");
            Vector3 pos = playerController.storageInventory.gameObject.transform.position;
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z); 
            client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+dragSlotIndex+";"+originType+"="+originAmount);
            bool flag = false;
            while (flag == false)
            {
                yield return Timing.WaitForSeconds(0.5f);
                try
                {
                    client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+storageInventoryDropSlot+";"+dropSlot.typeInSlot+"="+dropSlot.amountInSlot);
                    flag = true;
                }
                catch(Exception e)
                {
                    Debug.Log(e.Message + "\nTrying again in 0.5 seconds.");
                }
            }
        }
        networkContainerCoroutineBusy = false;
    }

    //! Updates the server when items are transferred to and from containers using ctrl+click.
    private IEnumerator<float> NetworkTransferItemCoroutine(int addIndex, int removeIndex, InventorySlot addSlot, InventorySlot removeSlot, InventoryManager destination)
    {
        networkTransferCoroutineBusy = true;
        if (destination == playerController.playerInventory)
        {
            Vector3 pos = playerController.storageInventory.gameObject.transform.position;
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z); 
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + removeIndex + ";" + removeSlot.typeInSlot + "=" + removeSlot.amountInSlot);
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
                Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+addIndex+";"+addSlot.typeInSlot+"="+addSlot.amountInSlot);
            }
        }
        yield return Timing.WaitForSeconds(0.25f);
        networkTransferCoroutineBusy = false;
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
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && networkTransferCoroutineBusy == false)
                        {
                            Timing.RunCoroutine(NetworkTransferItemCoroutine(i, index, slot, dragSlot, destination));
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
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                NetworkRemoveItem();
            }
            return;
        }

        if (guiCoordinates.inventoryTrashSlotRect.Contains(mousePos))
        {
            slotDraggingFrom.amountInSlot = 0;
            slotDraggingFrom.typeInSlot = "nothing";
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                NetworkRemoveItem();
            }
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
                    bool flag = false;
                    foreach (InventorySlot slot in playerInventory.inventory)
                    {
                        if (slot == slotDraggingFrom)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        Vector3 pos = playerController.storageInventory.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        using(WebClient client = new WebClient())
                        {
                            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/storage");
                            client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+dragSlotIndex+";"+slotDraggingFrom.typeInSlot+"="+slotDraggingFrom.amountInSlot);
                        }
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
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        slotDraggingFrom.pendingNetworkUpdate = true;
                        slotDraggingFrom.networkWaitTime = 0;
                        bool flag = false;
                        foreach (InventorySlot slot in playerInventory.inventory)
                        {
                            if (slot == slotDraggingFrom)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == false && networkContainerCoroutineBusy == false)
                        {
                            string originType = slotDraggingFrom.typeInSlot;
                            int originAmount = slotDraggingFrom.amountInSlot;
                            Timing.RunCoroutine(NetworkContainerCoroutine(storageInventoryDropSlot, dropSlot, originType, originAmount));
                        }
                    }
                }
                storageInventoryDropSlot++;
            }
        }
    }
}
