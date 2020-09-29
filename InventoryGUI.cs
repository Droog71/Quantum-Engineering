using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private InventoryHandler inventoryHandler;
    private TextureDictionary textureDictionary;
    private GuiCoordinates guiCoordinates;
    private string storageComputerSearchText = "";
    public bool outOfSpace;
    public bool cannotAfford;
    public bool missingItem;
    private float outOfSpaceTimer;
    private float cannotAffordTimer;
    private float missingItemTimer;


    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        inventoryHandler = new InventoryHandler(playerController, playerInventory);
        textureDictionary = GetComponent<TextureDictionary>();
        guiCoordinates = new GuiCoordinates();
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        // Style.
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        // Aspect ratio.
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

                // Inventory item drawing.
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

                // Storage container item drawing.
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

                // Dragging items from the player's inventory.
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
                                inventoryHandler.DragItemFromSlot(dragSlot, playerController.storageInventory);
                            }
                        }
                    }
                    inventoryDragSlot++;
                }

                // The player is accessing a storage container.
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
                                    inventoryHandler.DragItemFromSlot(dragSlot, playerInventory);
                                }
                            }
                        }
                        storageInventoryDragSlot++;
                    }
                }

                // The player is dragging an inventory item.
                if (playerController.draggingItem == true)
                {
                    float orgX = Event.current.mousePosition.x - ScreenWidth * 0.0145f;
                    float orgY = Event.current.mousePosition.y - ScreenHeight * 0.03f;
                    Rect mouseRect = new Rect(orgX, orgY, ScreenWidth * 0.029f, ScreenHeight * 0.06f);
                    GUI.DrawTexture(mouseRect, textureDictionary.dictionary[inventoryHandler.itemToDrag]);
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        inventoryHandler.DropItemInSlot(mousePos, playerController.storageGUIopen);
                    }
                }
                // // // // // END DRAG AND DROP // // // // //

                // Cycling between inventories connected to a storage computer.
                if (playerController.storageGUIopen == true && playerController.remoteStorageActive == true)
                {
                    StorageComputer computer = playerController.currentStorageComputer.GetComponent<StorageComputer>();
                    GUI.Label(guiCoordinates.storageSearchLabelRect, "SEARCH");
                    storageComputerSearchText = GUI.TextField(guiCoordinates.storageComputerSearchRect, storageComputerSearchText);
                    if (Event.current.isKey && Event.current.keyCode != KeyCode.LeftShift && Event.current.keyCode != KeyCode.LeftControl)
                    {
                        inventoryHandler.SearchComputerContainers(computer, storageComputerSearchText);
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

                // Message displayed when the player attempts to purchase something they cannot afford from the market.
                if (cannotAfford == true)
                {
                    if (cannotAffordTimer < 3)
                    {
                        GUI.Label(guiCoordinates.inventoryMesageRect, "Cannot afford.");
                        cannotAffordTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        cannotAfford = false;
                        cannotAffordTimer = 0;
                    }
                }

                // Message displayed when the player is missing a required item for sale or crafting.
                if (missingItem == true)
                {
                    if (missingItemTimer < 3)
                    {
                        GUI.Label(guiCoordinates.inventoryMesageRect, "Missing items.");
                        missingItemTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        missingItem = false;
                        missingItemTimer = 0;
                    }
                }

                // Message displayed when the player does not have adequate inventory space to craft or purchase an item.
                if (outOfSpace == true)
                {
                    if (outOfSpaceTimer < 3)
                    {
                        GUI.Label(guiCoordinates.inventoryMesageRect, "\nNo space in inventory.");
                        outOfSpaceTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        outOfSpace = false;
                        outOfSpaceTimer = 0;
                    }
                }

                // Buttons.
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