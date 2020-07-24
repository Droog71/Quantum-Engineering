using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private PlayerCrafting playerCrafting;
    private string storageComputerSearchText = "";
    private float missingItemTimer;
    private int craftingPage;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        playerCrafting = GetComponent<PlayerCrafting>();
    }

    void OnGUI()
    {
        //STYLE
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        if (playerController.stateManager.worldLoaded == true && GetComponent<MainMenu>().finishedLoading == true)
        {
            if (playerController.inventoryOpen == true)
            {
                gameObject.GetComponent<MSCameraController>().enabled = false;
                GUI.DrawTexture(GetComponent<GuiCoordinates>().inventoryBackgroundRect, GetComponent<TextureDictionary>().dictionary["Container Background"]);

                int inventorySlotNumber = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (!slot.typeInSlot.Equals("") && !slot.typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().inventorySlotRects[inventorySlotNumber], GetComponent<TextureDictionary>().dictionary[slot.typeInSlot]);
                    }
                    if (slot.amountInSlot != 0)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().inventorySlotLabelRects[inventorySlotNumber], slot.amountInSlot.ToString());
                    }
                    inventorySlotNumber++;
                }

                //STORAGE CONTAINER ITEM DRAWING
                if (playerController.storageGUIopen == true)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().inventoryInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().inventoryInfoRect, "\nLeft Shift + Click & Drag to split stack.\n\nLeft Control + Click to transfer entire stack.");
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().storageInventoryBackgroundRect, GetComponent<TextureDictionary>().dictionary["Container Background"]);

                    int storageInventorySlotNumber = 0;
                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                    {
                        if (!slot.typeInSlot.Equals("") && !slot.typeInSlot.Equals("nothing"))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().storageInventorySlotRects[storageInventorySlotNumber], GetComponent<TextureDictionary>().dictionary[slot.typeInSlot]);
                        }
                        if (slot.amountInSlot != 0)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().storageInventorySlotLabelRects[storageInventorySlotNumber], slot.amountInSlot.ToString());
                        }
                        storageInventorySlotNumber++;
                    }                
                }

                //STORAGE COMPUTER INVENTORY SWITCHING
                if (playerController.storageGUIopen == true && playerController.remoteStorageActive == true)
                {
                    StorageComputer computer = playerController.currentStorageComputer.GetComponent<StorageComputer>();
                    GUI.Label(GetComponent<GuiCoordinates>().storageSearchLabelRect, "SEARCH");
                    storageComputerSearchText = GUI.TextField(GetComponent<GuiCoordinates>().storageComputerSearchRect, storageComputerSearchText);
                    if (Event.current.isKey && Event.current.keyCode != KeyCode.LeftShift && Event.current.keyCode != KeyCode.LeftControl)
                    {
                        int containerCount = 0;
                        foreach (InventoryManager manager in computer.computerContainers)
                        {
                            foreach (InventorySlot slot in manager.inventory)
                            {
                                if (storageComputerSearchText.Length < slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Search term shorter than item type string: "+slot.typeInSlot.Substring(storageComputerSearchText.Length) + " VS " + storageComputerSearchText);
                                    if (slot.typeInSlot.Substring(0, storageComputerSearchText.Length).ToLower().Equals(storageComputerSearchText.ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                                else if (storageComputerSearchText.Length > slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Search term longer than item type string: " + slot.typeInSlot + " VS " + storageComputerSearchText.Substring(slot.typeInSlot.Length));
                                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.Substring(0, slot.typeInSlot.Length).ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                                else if (storageComputerSearchText.Length == slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Searching for exact match");
                                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                            }
                            //Debug.Log("Current Inventory ID: " + playerController.storageComputerInventory);
                            containerCount++;
                        }
                    }
                    if (GUI.Button(GetComponent<GuiCoordinates>().storageComputerPreviousRect, "<-"))
                    {
                        if (playerController.storageComputerInventory > 0)
                        {
                            playerController.storageComputerInventory -= 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.playButtonSound();
                    }
                    if (GUI.Button(GetComponent<GuiCoordinates>().storageComputerNextRect, "->"))
                    {
                        if (playerController.storageComputerInventory < computer.computerContainers.Length - 1)
                        {
                            playerController.storageComputerInventory += 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.playButtonSound();
                    }
                    if (GUI.Button(GetComponent<GuiCoordinates>().storageComputerRebootRect, "REBOOT"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        computer.Reboot();
                        playerController.playButtonSound();
                    }
                }

                //////////DRAG AND DROP//////////
                Vector2 mousePos = Event.current.mousePosition; //MOUSE POSITION

                if (playerController.storageGUIopen == true) //PLAYER IS ACCESSING A STORAGE CONTAINER
                {
                    //PLAYER IS DRAGGING AN ITEM
                    if (playerController.draggingItem == true)
                    {
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.029f), (ScreenHeight * 0.06f)), GetComponent<TextureDictionary>().dictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            //DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                            playerController.draggingItem = false;
                            int inventoryDropSlot = 0;
                            foreach (Rect rect in GetComponent<GuiCoordinates>().inventorySlotRects)
                            {
                                if (rect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[inventoryDropSlot])
                                {
                                    if (playerInventory.inventory[inventoryDropSlot].typeInSlot == "nothing" || playerInventory.inventory[inventoryDropSlot].typeInSlot == "" || playerInventory.inventory[inventoryDropSlot].typeInSlot == playerController.itemToDrag)
                                    {
                                        if (playerInventory.inventory[inventoryDropSlot].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                        {
                                            playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, inventoryDropSlot);
                                            playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                            if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                            {
                                                playerController.slotDraggingFrom.typeInSlot = "nothing";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[inventoryDropSlot].typeInSlot;
                                        playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[inventoryDropSlot].amountInSlot;
                                        playerInventory.inventory[inventoryDropSlot].typeInSlot = playerController.itemToDrag;
                                        playerInventory.inventory[inventoryDropSlot].amountInSlot = playerController.amountToDrag;
                                    }
                                }
                                inventoryDropSlot++;
                            }

                            //DROPPING ITEMS INTO THE STORAGE CONTAINER
                            int storageInventoryDropSlot = 0;
                            foreach (Rect rect in GetComponent<GuiCoordinates>().storageInventorySlotRects)
                            {
                                if (rect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[storageInventoryDropSlot])
                                {
                                    if (playerController.storageInventory.inventory[storageInventoryDropSlot].typeInSlot == "nothing" || playerController.storageInventory.inventory[storageInventoryDropSlot].typeInSlot == "" || playerController.storageInventory.inventory[storageInventoryDropSlot].typeInSlot == playerController.itemToDrag)
                                    {
                                        if (playerController.storageInventory.inventory[storageInventoryDropSlot].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                        {
                                            playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, storageInventoryDropSlot);
                                            playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                            if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                            {
                                                playerController.slotDraggingFrom.typeInSlot = "nothing";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[storageInventoryDropSlot].typeInSlot;
                                        playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[storageInventoryDropSlot].amountInSlot;
                                        playerController.storageInventory.inventory[storageInventoryDropSlot].typeInSlot = playerController.itemToDrag;
                                        playerController.storageInventory.inventory[storageInventoryDropSlot].amountInSlot = playerController.amountToDrag;
                                    }
                                }
                                storageInventoryDropSlot++;
                            }
                        }
                    }

                    //DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    int inventoryDragSlot = 0;
                    foreach (Rect rect in GetComponent<GuiCoordinates>().inventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerInventory.inventory[inventoryDragSlot].typeInSlot != "" && !playerInventory.inventory[inventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().inventoryMesageRect, playerInventory.inventory[inventoryDragSlot].typeInSlot);
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    if (Input.GetKey(KeyCode.LeftControl))
                                    {
                                        foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                        {
                                            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[inventoryDragSlot].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[inventoryDragSlot].amountInSlot)
                                            {
                                                playerController.storageInventory.AddItem(playerInventory.inventory[inventoryDragSlot].typeInSlot, playerInventory.inventory[inventoryDragSlot].amountInSlot);
                                                playerInventory.inventory[inventoryDragSlot].typeInSlot = "nothing";
                                                playerInventory.inventory[inventoryDragSlot].amountInSlot = 0;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        playerController.draggingItem = true;
                                        playerController.itemToDrag = playerInventory.inventory[inventoryDragSlot].typeInSlot;
                                        playerController.slotDraggingFrom = playerInventory.inventory[inventoryDragSlot];
                                        if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 1)
                                            {
                                                playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot / 2;
                                            }
                                            else if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 0)
                                            {
                                                playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot;
                                            }
                                        }
                                        else if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot;
                                        }
                                    }
                                }
                            }
                        }
                        inventoryDragSlot++;
                    }

                    //DRAGGING ITEMS FROM THE STORAGE CONTAINER
                    int storageInventoryDragSlot = 0;
                    foreach (Rect rect in GetComponent<GuiCoordinates>().storageInventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot != "" && !playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                if (playerController.remoteStorageActive == true)
                                {
                                    GUI.Label(GetComponent<GuiCoordinates>().storageComputerMessageRect, playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot);
                                }
                                else
                                {
                                    GUI.Label(GetComponent<GuiCoordinates>().storageInventoryMessageRect, playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot);
                                }
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    if (Input.GetKey(KeyCode.LeftControl))
                                    {
                                        foreach (InventorySlot slot in playerInventory.inventory)
                                        {
                                            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot)
                                            {
                                                playerInventory.AddItem(playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot, playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot);
                                                playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot = "nothing";
                                                playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot = 0;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        playerController.draggingItem = true;
                                        playerController.itemToDrag = playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot;
                                        playerController.slotDraggingFrom = playerController.storageInventory.inventory[storageInventoryDragSlot];
                                        if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            if (playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot > 1)
                                            {
                                                playerController.amountToDrag = playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot / 2;
                                            }
                                            else if (playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot > 0)
                                            {
                                                playerController.amountToDrag = playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot;
                                            }
                                        }
                                        else if (playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[storageInventoryDragSlot].amountInSlot;
                                        }
                                    }
                                }
                            }
                        }
                        storageInventoryDragSlot++;
                    }
                }
                else
                {
                    //NO STORAGE CONTAINER IS OPEN. THE PLAYER IS DRAGGING AND DROPPING ITEMS WITHIN THEIR OWN INVENTORY
                    if (playerController.draggingItem == true)
                    {
                        //DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.025f), (ScreenHeight * 0.05f)), GetComponent<TextureDictionary>().dictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            playerController.draggingItem = false;
                            int inventoryDropSlot = 0;
                            foreach (Rect rect in GetComponent<GuiCoordinates>().inventorySlotRects)
                            {
                                if (rect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[inventoryDropSlot])
                                {
                                    if (playerInventory.inventory[inventoryDropSlot].typeInSlot == "nothing" || playerInventory.inventory[inventoryDropSlot].typeInSlot == "" || playerInventory.inventory[inventoryDropSlot].typeInSlot == playerController.itemToDrag)
                                    {
                                        if (playerInventory.inventory[inventoryDropSlot].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                        {
                                            playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 0);
                                            playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                            if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                            {
                                                playerController.slotDraggingFrom.typeInSlot = "nothing";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[inventoryDropSlot].typeInSlot;
                                        playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[inventoryDropSlot].amountInSlot;
                                        playerInventory.inventory[inventoryDropSlot].typeInSlot = playerController.itemToDrag;
                                        playerInventory.inventory[inventoryDropSlot].amountInSlot = playerController.amountToDrag;
                                    }
                                }
                                inventoryDropSlot++;
                            }
                        }
                    }

                    //DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    int inventoryDragSlot = 0;
                    foreach (Rect rect in GetComponent<GuiCoordinates>().inventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerInventory.inventory[inventoryDragSlot].typeInSlot != "" && !playerInventory.inventory[inventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().inventoryMesageRect, playerInventory.inventory[inventoryDragSlot].typeInSlot);
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[inventoryDragSlot].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[inventoryDragSlot];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[inventoryDragSlot].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[inventoryDragSlot].amountInSlot;
                                    }
                                }
                            }
                        }
                        inventoryDragSlot++;
                    }
                }

                //MESSAGE TELLING THE PLAYER THEY ARE MISSING THE ITEMS REQUIRED TO CRAFT AN OBJECT
                if (playerCrafting.missingItem == true)
                {
                    if (missingItemTimer < 3)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().inventoryMesageRect, "Missing items.");
                        missingItemTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        playerCrafting.missingItem = false;
                        missingItemTimer = 0;
                    }
                }

                //MESSAGE TELLING THE PLAYER THEIR INVENTORY IS FULL
                if (playerController.outOfSpace == true)
                {
                    if (playerController.outOfSpaceTimer < 3)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().inventoryMesageRect, "\nNo space in inventory.");
                        playerController.outOfSpaceTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        playerController.outOfSpace = false;
                        playerController.outOfSpaceTimer = 0;
                    }
                }

                //BUTTON WHICH OPENS THE CRAFTING GUI
                if (playerController.storageGUIopen == false)
                {
                    if (GUI.Button(GetComponent<GuiCoordinates>().craftingButtonRect, "CRAFTING"))
                    {
                        if (playerController.craftingGUIopen == false && playerController.storageGUIopen == false)
                        {
                            playerController.craftingGUIopen = true;
                        }
                        else
                        {
                            playerController.craftingGUIopen = false;
                        }
                        playerController.playButtonSound();
                    }
                }

                //BUTTON THAT CLOSES THE INVENTORY GUI
                if (GUI.Button(GetComponent<GuiCoordinates>().closeButtonRect, "CLOSE"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.inventoryOpen = false;
                    playerController.craftingGUIopen = false;
                    playerController.storageGUIopen = false;
                    playerController.playButtonSound();
                }

                //CRAFTING GUI
                if (playerController.craftingGUIopen == true)
                {
                    if (craftingPage == 0)
                    {
                        if (GetComponent<GuiCoordinates>().button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Storage container for objects and items. Can be used to manually store items or connected to machines for automation. Universal conduits, dark matter conduits, retrievers and auto crafters can all connect to storage containers.\n\n[CRAFTING]\n6x Iron Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Extracts regolith from the lunar surface which can be pressed into bricks or smelted to create glass. Glass blocks have a 100% chance of being destroyed by meteors and other hazards. Bricks have a 75% chance of being destroyed by meteors and other hazards. Augers must be placed directly on the lunar surface and require power from a solar panel, nuclear reactor or power conduit.\n\n[CRAFTING]\n10x Iron Ingot\n10x Copper Ingot");
                        }
                        if (GetComponent<GuiCoordinates>().button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Creates wire from copper and aluminum ingots. Creates pipes from iron and steel ingots. Ingots must be supplied to the extruder using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. The extruder requires power from a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Ingot\n10x Copper Ingot");
                        }
                        if (GetComponent<GuiCoordinates>().button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Presses iron, copper, aluminum or tin ingots into plates. Ingots must be supplied to the press using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. Must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Ingot\n10x Iron Pipe\n10x Copper Wire");
                        }
                        if (GetComponent<GuiCoordinates>().button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Cuts plates into gears. Plates must be supplied to the gear cutter using universal conduits. Place another conduit within 2 meters of the machine for the output. The gear cutter must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The gear cutter has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Aluminum Wire\n10x Copper Wire\n5x Iron Plate\n5x Tin Plate\n5x Iron Pipe");
                        }
                        if (GetComponent<GuiCoordinates>().button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Extracts ore, coal and ice from deposits found on the lunar surface. Place within 2 meters of the desired resource and use a universal conduit to handle the harvested materials. When extracting ice, the extractor will not need a heat exchanger for cooling. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Plate\n10x Iron Pipe\n10x Copper Wire\n10x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Transfers items from a machine to another universal conduit, another machine or a storage container. Universal conduits have an adjustable input/output range and do not require power to operate.\n\n[CRAFTING]\n5x Iron Pipe\n5x Iron Plate\n5x Copper Wire\n5x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Retrieves items from a storage container and transfers them to a universal conduit. Place an item of each desired type into the retrievers inventory to designate that item for retrieval. Place within 2 meters of a storage container and a universal conduit. This machine requires power and it's output is adjustable. If the retriever is moving ice, it will not require cooling. The retriever's output is measured in items per cycle.\n\n[CRAFTING]\n4x Iron Plate\n4x Copper Wire\n2x Iron Pipe\n2x Electric Motor\n2x Circuit Board");
                        }
                        if (GetComponent<GuiCoordinates>().button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Provides 1 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple solar panels can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[CRAFTING]\n4x Iron Pipe\n4x Iron Plate\n4x Copper Wire\n4x Copper Plate\n4x Glass Block");
                        }
                        if (GetComponent<GuiCoordinates>().button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Provides 10 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple generators can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate. Generators must be connected to a universal conduit supplying coal for fuel.\n\n[CRAFTING]\n4x Iron Plate\n4x Copper Wire\n2x Iron Gear\n2x Iron Pipe\n1x Smelter\n1x Electric Motor");
                        }
                        if (GetComponent<GuiCoordinates>().button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Nuclear reactors are used to drive reactor turbines. Turbines must be directly attached to the reactor. The reactor will require a heat exchanger providing 5 KBTU cooling per turbine.\n\n[CRAFTING]\n10x Steel Pipe\n10x Steel Plate\n10x Copper Wire\n10x Copper Plate\n10x Glass Block\n10x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Provides 100 MW of power to a single machine or power conduit. Reactor turbines must be directly attached to a properly functioning, adequately cooled nuclear reactor. Must be placed within 4 meters of the machine. Multiple reactor turbines can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[CRAFTING]\n4x Steel Plate\n4x Copper Wire\n2x Steel Gear\n2x Steel Pipe\n1x Generator\n1x Glass Block");
                        }
                        if (GetComponent<GuiCoordinates>().button14Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Transfers power from a power source to a machine or to another power conduit. When used with two outputs, power will be distributed evenly. This machine has an adjustable range setting.\n\n[CRAFTING]\n4x Aluminum Plate\n4x Copper Wire\n4x Glass Block");
                        }
                        if (GetComponent<GuiCoordinates>().button15Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Cools down a machine to allow overclocking. Requires a supply of ice from a universal conduit. Increasing the output of the heat exchanger increases the amount of ice required. This can be compensated for by overclocking the extractor that is supplying the ice. Machines cannot be connected to more than one heat exchanger. The heat exchanger's output is measured in KBTU and will consume 1 ice per 1 KBTU of cooling each cycle.\n\n[CRAFTING]\n10x Steel Plate\n10x Steel Pipe");
                        }
                        if (GetComponent<GuiCoordinates>().button17Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Smelts ore into ingots. Can also be used to make glass when supplied with regolith. Ore must be supplied to the smelter using universal conduits. Place another conduit within 2 meters of the machine for the output. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The output of a smelter is measured in items per cycle.\n\n[CRAFTING]\n10x Iron Plate\n10x Copper Wire\n5x Iron Pipe");
                        }
                        if (GetComponent<GuiCoordinates>().button18Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Combines tin and copper ingots to make bronze ingots. Combines coal and iron ingots to make steel ingots. Requres 3 conduits. 1 for each input and 1 for the output. Requires a power source such as a solar panel, nuclear reactor or power conduit. The alloy smelter has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n40x Copper Wire\n40x Aluminum Wire\n20x Iron Plate\n20x Tin Plate\n20x Iron Pipe\n20x Iron Gear");
                        }
                        if (GetComponent<GuiCoordinates>().button19Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Harvests dark matter which is then transferred to a dark matter conduit. Requires a power source such as a solar panel, nuclear reactor or power conduit. The dark matter collector has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n100x Dark Matter\n100x Copper Wire\n100x Aluminum Wire\n50x Steel Plate\n50x Steel Pipe\n50x Steel Gear\n50x Tin Gear\n50x Bronze Gear");
                        }
                        if (GetComponent<GuiCoordinates>().button20Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Transfers dark matter from a collector to a storage container or another conduit. Dark matter conduits have an adjustable input/output range and do not require power to operate.\n\n[CRAFTING]\n50x Dark Matter\n50x Copper Wire\n50x Aluminum Wire\n25x Steel Plate\n25x Steel Pipe\n25x Steel Gear\n25x Tin Gear\n25x Bronze Gear");
                        }
                        if (GetComponent<GuiCoordinates>().button21Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Automatically crafts objects using items from an attached storage container. Place within 2 meters of the storage container. Then, place an item of the desired type into the auto crafter's inventory. This will designate that item as the item to be crafted. Crafted items will be deposited into the attached storage container. This machine requires power and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n4x Bronze Gear\n4x Steel Plate\n4x Electric Motor\n4x Circuit Board\n4x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button22Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Provides a waypoint for rail carts. Has an adjustable range at which the next hub will be located and rails deployed to it's location. Rail cart hubs can be configured to stop the rail cart so it can be loaded and unloaded.\n\n[CRAFTING]\n10x Iron Pipe\n6x Iron Plate\n1x Circuit Board");
                        }
                        if (GetComponent<GuiCoordinates>().button23Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "A mobile storage container that rides on rails from one rail cart hub to the next. Configure the hubs to stop the cart near a conduit or retriever so it can be loaded or unloaded. Must be placed on a rail cart hub.\n\n[CRAFTING]\n10x Copper Wire\n8x Aluminum Gear\n4x Tin Plate\n2x Electric Motor\n1x Solar Panel\n1x Storage Container");
                        }

                        GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        if (GUI.Button(GetComponent<GuiCoordinates>().button1Rect, "Storage Container"))
                        {
                            playerCrafting.CraftStorageContainer();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button2Rect, "Auger"))
                        {
                            playerCrafting.CraftAuger();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button3Rect, "Extruder"))
                        {
                            playerCrafting.CraftExtruder();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button4Rect, "Press"))
                        {
                            playerCrafting.CraftPress();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button5Rect, "Gear Cutter"))
                        {
                            playerCrafting.CraftGearCutter();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button6Rect, "Universal Extractor"))
                        {
                            playerCrafting.CraftUniversalExtractor();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button7Rect, "Universal Conduit"))
                        {
                            playerCrafting.CraftUniversalConduit();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button9Rect, "Retriever"))
                        {
                            playerCrafting.CraftRetriever();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button10Rect, "Solar Panel"))
                        {
                            playerCrafting.CraftSolarPanel();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button11Rect, "Generator"))
                        {
                            playerCrafting.CraftGenerator();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button12Rect, "Nuclear Reactor"))
                        {
                            playerCrafting.CraftNuclearReactor();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button13Rect, "Reactor Turbine"))
                        {
                            playerCrafting.CraftReactorTurbine();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button14Rect, "Power Conduit"))
                        {
                            playerCrafting.CraftPowerConduit();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button15Rect, "Heat Exchanger"))
                        {
                            playerCrafting.CraftHeatExchanger();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button17Rect, "Smelter"))
                        {
                            playerCrafting.CraftSmelter();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button18Rect, "Alloy Smelter"))
                        {
                            playerCrafting.CraftAlloySmelter();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button19Rect, "DM Collector"))
                        {
                            playerCrafting.CraftDarkMatterCollector();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button20Rect, "DM Conduit"))
                        {
                            playerCrafting.CraftDarkMatterConduit();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button21Rect, "Auto Crafter"))
                        {
                            playerCrafting.CraftAutoCrafter();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button22Rect, "Rail Cart Hub"))
                        {
                            playerCrafting.CraftRailCartHub();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button23Rect, "Rail Cart"))
                        {
                            playerCrafting.CraftRailCart();
                        }
                    }
                    if (craftingPage == 1)
                    {
                        if (GetComponent<GuiCoordinates>().button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Iron block for building structures. Iron blocks have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Iron Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Iron ramp for building structures. Iron ramps have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Iron Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Steel block for building structures. Steel blocks have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Steel Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Steel ramp for building structures. Steel ramps have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Steel Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Hatchway used for entering structures.\n\n[CRAFTING]\n1x Tin Plate\n1x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "For interior lighting. Requires power from a solar panel, nuclear reactor or power conduit.\n\n[CRAFTING]\n2x Copper Wire\n1x Glass Block\n1x Tin Plate");
                        }
                        if (GetComponent<GuiCoordinates>().button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "A combination of conductive, semi-conductive and insulating materials combined to create a logic processing circuit.\n\n[CRAFTING]\n2x Copper Wire\n1x Glass Block\n1x Tin Plate\n1x Dark Matter");
                        }
                        if (GetComponent<GuiCoordinates>().button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "A device that converts electrical energy to mechanical torque.\n\n[CRAFTING]\n10x Copper Wire\n2x Iron Plate\n1x Iron Pipe\n2x Iron Gear");
                        }
                        if (GetComponent<GuiCoordinates>().button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Provides access to all stationary storage containers within 4 meters. Can be accessed manually or connected to retrievers, auto crafters and conduits. When a conduit is connectd to the computer, the computer will store items starting with the first container found to have space available. When a retriever is connected to the computer, the computer will search all of the managed containers for desired items.\n\n[CRAFTING]\n10x Copper Wire\n10x Tin Gear\n5x Retriever\n5x Universal Conduit\n5x Aluminum Plate\n1x Dark Matter Conduit\n1x Glass Block");
                        }
                        if (GetComponent<GuiCoordinates>().button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingInfoBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().craftingInfoRect, "Protects your equipment from meteor showers and other hazards. Requires a power source such as a solar panel, nuclear reactor or power conduit. Turrets have an adjustable output measured in rounds per minute.\n\n[CRAFTING]\n10x Copper Wire\n10x Aluminum Wire\n5x Steel Plate\n5x Steel Pipe\n5x Steel Gear\n5x Bronze Plate\n4x Electric Motor\n4x Circuit Board");
                        }

                        GUI.DrawTexture(GetComponent<GuiCoordinates>().craftingBackgroundRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        if (GUI.Button(GetComponent<GuiCoordinates>().button1Rect, "Iron Block"))
                        {
                            playerCrafting.CraftIronBlock();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button2Rect, "Iron Ramp"))
                        {
                            playerCrafting.CraftIronRamp();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button3Rect, "Steel Block"))
                        {
                            playerCrafting.CraftSteelBlock();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button4Rect, "Steel Ramp"))
                        {
                            playerCrafting.CraftSteelRamp();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button5Rect, "Quantum Hatchway"))
                        {
                            playerCrafting.CraftQuantumHatchway();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button6Rect, "Electric Light"))
                        {
                            playerCrafting.CraftElectricLight();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button7Rect, "Circuit Board"))
                        {
                            playerCrafting.CraftCircuitBoard();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button9Rect, "Electric Motor"))
                        {
                            playerCrafting.CraftMotor();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button10Rect, "Storage Computer"))
                        {
                            playerCrafting.CraftStorageComputer();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().button11Rect, "Turret"))
                        {
                            playerCrafting.CraftTurret();
                        }

                    }
                    if (GUI.Button(GetComponent<GuiCoordinates>().craftingPreviousRect, "<-"))
                    {
                        if (craftingPage > 0)
                        {
                            craftingPage -= 1;
                        }
                        playerController.playButtonSound();
                    }
                    if (GUI.Button(GetComponent<GuiCoordinates>().craftingNextRect, "->"))
                    {
                        if (craftingPage < 1)
                        {
                            craftingPage += 1;
                        }
                        playerController.playButtonSound();
                    }
                }
            }
        }
    }
}