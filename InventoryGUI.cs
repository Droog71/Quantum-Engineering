using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private TextureDictionary textureDictionary;
    private GuiCoordinates guiCoordinates;
    private InventorySlot slotDraggingFrom;
    private string storageComputerSearchText = "";
    private string itemToDrag;
    private int amountToDrag;


    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        textureDictionary = GetComponent<TextureDictionary>();
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
    private void SearchComputerContainers(StorageComputer computer)
    {
        int containerCount = 0;
        foreach (InventoryManager manager in computer.computerContainers)
        {
            foreach (InventorySlot slot in manager.inventory)
            {
                if (storageComputerSearchText.Length < slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.Substring(0, storageComputerSearchText.Length).ToLower().Equals(storageComputerSearchText.ToLower()))
                    {
                        playerController.storageComputerInventory = containerCount;
                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                    }
                }
                else if (storageComputerSearchText.Length > slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.Substring(0, slot.typeInSlot.Length).ToLower()))
                    {
                        playerController.storageComputerInventory = containerCount;
                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                    }
                }
                else if (storageComputerSearchText.Length == slot.typeInSlot.Length)
                {
                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.ToLower()))
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
    private void DragItemFromSlot(InventorySlot dragSlot, InventoryManager destination)
    {
        bool flag = false;
        if (playerController.storageGUIopen == true)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                flag = true;
                foreach (InventorySlot slot in destination.inventory)
                {
                    if (CanTransfer(dragSlot, slot))
                    {
                        destination.AddItem(dragSlot.typeInSlot, dragSlot.amountInSlot);
                        dragSlot.typeInSlot = "nothing";
                        dragSlot.amountInSlot = 0;
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
    private void DropItemInSlot(Vector2 mousePos, bool usingContainer)
    {
        playerController.draggingItem = false;
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
                }
                storageInventoryDropSlot++;
            }
        }
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        // STYLE
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        // ASPECT RATIO
        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;
        if (ScreenWidth / ScreenHeight < 1.7f)
        {
            ScreenHeight = (ScreenHeight * 0.75f);
        }
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        if (!playerController.stateManager.Busy() && GetComponent<MainMenu>().finishedLoading == true)
        {
            if (playerController.inventoryOpen == true)
            {
                gameObject.GetComponent<MSCameraController>().enabled = false;
                GUI.DrawTexture(guiCoordinates.inventoryBackgroundRect, textureDictionary.dictionary["Container Background"]);

                // INVENTORY ITEM DRAWING
                int inventorySlotNumber = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (!slot.typeInSlot.Equals("") && !slot.typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(guiCoordinates.inventorySlotRects[inventorySlotNumber], textureDictionary.dictionary[slot.typeInSlot]);
                    }
                    if (slot.amountInSlot != 0)
                    {
                        GUI.Label(guiCoordinates.inventorySlotLabelRects[inventorySlotNumber], slot.amountInSlot.ToString());
                    }
                    inventorySlotNumber++;
                }

                // STORAGE CONTAINER ITEM DRAWING
                if (playerController.storageGUIopen == true)
                {
                    GUI.DrawTexture(guiCoordinates.inventoryInfoRectBG, textureDictionary.dictionary["Interface Background"]);
                    GUI.Label(guiCoordinates.inventoryInfoRect, "\nLeft Shift + Click & Drag to split stack.\n\nLeft Control + Click to transfer entire stack.");
                    GUI.DrawTexture(guiCoordinates.storageInventoryBackgroundRect, textureDictionary.dictionary["Container Background"]);

                    int storageInventorySlotNumber = 0;
                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                    {
                        if (!slot.typeInSlot.Equals("") && !slot.typeInSlot.Equals("nothing"))
                        {
                            GUI.DrawTexture(guiCoordinates.storageInventorySlotRects[storageInventorySlotNumber], textureDictionary.dictionary[slot.typeInSlot]);
                        }
                        if (slot.amountInSlot != 0)
                        {
                            GUI.Label(guiCoordinates.storageInventorySlotLabelRects[storageInventorySlotNumber], slot.amountInSlot.ToString());
                        }
                        storageInventorySlotNumber++;
                    }
                }

                // // // // // DRAG AND DROP // // // // //
                Vector2 mousePos = Event.current.mousePosition;

                // DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                int inventoryDragSlot = 0;
                foreach (Rect rect in guiCoordinates.inventorySlotRects)
                {
                    if (rect.Contains(mousePos))
                    {
                        InventorySlot dragSlot = playerInventory.inventory[inventoryDragSlot];
                        if (dragSlot.typeInSlot != "" && !dragSlot.typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(guiCoordinates.inventoryMesageRect, dragSlot.typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                DragItemFromSlot(dragSlot, playerController.storageInventory);
                            }
                        }
                    }
                    inventoryDragSlot++;
                }

                // PLAYER IS ACCESSING A STORAGE CONTAINER
                if (playerController.storageGUIopen == true) 
                {
                    int storageInventoryDragSlot = 0;
                    foreach (Rect rect in guiCoordinates.storageInventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            InventorySlot dragSlot = playerController.storageInventory.inventory[storageInventoryDragSlot];
                            if (dragSlot.typeInSlot != "" && !dragSlot.typeInSlot.Equals("nothing"))
                            {
                                if (playerController.remoteStorageActive == true)
                                {
                                    GUI.Label(guiCoordinates.storageComputerMessageRect, dragSlot.typeInSlot);
                                }
                                else
                                {
                                    GUI.Label(guiCoordinates.storageInventoryMessageRect, dragSlot.typeInSlot);
                                }
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    DragItemFromSlot(dragSlot, playerInventory);
                                }
                            }
                        }
                        storageInventoryDragSlot++;
                    }
                }

                // PLAYER IS DRAGGING AN ITEM
                if (playerController.draggingItem == true)
                {
                    float orgX = Event.current.mousePosition.x - ScreenWidth * 0.0145f;
                    float orgY = Event.current.mousePosition.y - ScreenHeight * 0.03f;
                    Rect mouseRect = new Rect(orgX, orgY, ScreenWidth * 0.029f, ScreenHeight * 0.06f);
                    GUI.DrawTexture(mouseRect, textureDictionary.dictionary[itemToDrag]);
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        DropItemInSlot(mousePos, playerController.storageGUIopen);
                    }
                }

                // STORAGE COMPUTER INVENTORY SWITCHING
                if (playerController.storageGUIopen == true && playerController.remoteStorageActive == true)
                {
                    StorageComputer computer = playerController.currentStorageComputer.GetComponent<StorageComputer>();
                    GUI.Label(guiCoordinates.storageSearchLabelRect, "SEARCH");
                    storageComputerSearchText = GUI.TextField(guiCoordinates.storageComputerSearchRect, storageComputerSearchText);
                    if (Event.current.isKey && Event.current.keyCode != KeyCode.LeftShift && Event.current.keyCode != KeyCode.LeftControl)
                    {
                        SearchComputerContainers(computer);
                    }
                    if (GUI.Button(guiCoordinates.storageComputerPreviousRect, "<-"))
                    {
                        if (playerController.storageComputerInventory > 0)
                        {
                            playerController.storageComputerInventory -= 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.storageComputerNextRect, "->"))
                    {
                        if (playerController.storageComputerInventory < computer.computerContainers.Length - 1)
                        {
                            playerController.storageComputerInventory += 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.storageComputerRebootRect, "REBOOT"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        computer.Reboot();
                        playerController.PlayButtonSound();
                    }
                }

                // MESSAGE TELLING THE PLAYER THEIR INVENTORY IS FULL
                if (playerController.outOfSpace == true)
                {
                    if (playerController.outOfSpaceTimer < 3)
                    {
                        GUI.Label(guiCoordinates.inventoryMesageRect, "\nNo space in inventory.");
                        playerController.outOfSpaceTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        playerController.outOfSpace = false;
                        playerController.outOfSpaceTimer = 0;
                    }
                }

                if (playerController.marketGUIopen == false)
                {
                    if (playerController.storageGUIopen == false)
                    {
                        if (GUI.Button(guiCoordinates.craftingButtonRect, "CRAFTING"))
                        {
                            if (playerController.craftingGUIopen == false && playerController.storageGUIopen == false)
                            {
                                playerController.craftingGUIopen = true;
                            }
                            else
                            {
                                playerController.craftingGUIopen = false;
                            }
                            playerController.PlayButtonSound();
                        }
                    }

                    if (GUI.Button(guiCoordinates.closeButtonRect, "CLOSE"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.PlayButtonSound();
                    }
                }
            }
        }
    }
}