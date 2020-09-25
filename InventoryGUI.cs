﻿using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class InventoryGUI : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    private CraftingManager craftingManager;
    private CraftingDictionary craftingDictionary;
    private TextureDictionary textureDictionary;
    private GuiCoordinates guiCoordinates;
    private string storageComputerSearchText = "";
    private float missingItemTimer;
    private int craftingPage;
    private int modCraftingIndex;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
        craftingManager = GetComponent<CraftingManager>();
        textureDictionary = GetComponent<TextureDictionary>();
        craftingDictionary = new CraftingDictionary();
        guiCoordinates = new GuiCoordinates();
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

                // STORAGE COMPUTER INVENTORY SWITCHING
                if (playerController.storageGUIopen == true && playerController.remoteStorageActive == true)
                {
                    StorageComputer computer = playerController.currentStorageComputer.GetComponent<StorageComputer>();
                    GUI.Label(guiCoordinates.storageSearchLabelRect, "SEARCH");
                    storageComputerSearchText = GUI.TextField(guiCoordinates.storageComputerSearchRect, storageComputerSearchText);
                    if (Event.current.isKey && Event.current.keyCode != KeyCode.LeftShift && Event.current.keyCode != KeyCode.LeftControl)
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

                // // // // // DRAG AND DROP // // // // //
                Vector2 mousePos = Event.current.mousePosition; // MOUSE POSITION

                if (playerController.storageGUIopen == true) // PLAYER IS ACCESSING A STORAGE CONTAINER
                {
                    // PLAYER IS DRAGGING AN ITEM
                    if (playerController.draggingItem == true)
                    {
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.029f), (ScreenHeight * 0.06f)), textureDictionary.dictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            // DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                            playerController.draggingItem = false;
                            int inventoryDropSlot = 0;
                            foreach (Rect rect in guiCoordinates.inventorySlotRects)
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

                            // DROPPING ITEMS INTO THE STORAGE CONTAINER
                            int storageInventoryDropSlot = 0;
                            foreach (Rect rect in guiCoordinates.storageInventorySlotRects)
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

                    // DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    int inventoryDragSlot = 0;
                    foreach (Rect rect in guiCoordinates.inventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerInventory.inventory[inventoryDragSlot].typeInSlot != "" && !playerInventory.inventory[inventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                GUI.Label(guiCoordinates.inventoryMesageRect, playerInventory.inventory[inventoryDragSlot].typeInSlot);
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

                    // DRAGGING ITEMS FROM THE STORAGE CONTAINER
                    int storageInventoryDragSlot = 0;
                    foreach (Rect rect in guiCoordinates.storageInventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot != "" && !playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                if (playerController.remoteStorageActive == true)
                                {
                                    GUI.Label(guiCoordinates.storageComputerMessageRect, playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot);
                                }
                                else
                                {
                                    GUI.Label(guiCoordinates.storageInventoryMessageRect, playerController.storageInventory.inventory[storageInventoryDragSlot].typeInSlot);
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
                    // NO STORAGE CONTAINER IS OPEN. THE PLAYER IS DRAGGING AND DROPPING ITEMS WITHIN THEIR OWN INVENTORY
                    if (playerController.draggingItem == true)
                    {
                        // DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.025f), (ScreenHeight * 0.05f)), textureDictionary.dictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            playerController.draggingItem = false;
                            int inventoryDropSlot = 0;
                            foreach (Rect rect in guiCoordinates.inventorySlotRects)
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
                        }
                    }

                    // DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    int inventoryDragSlot = 0;
                    foreach (Rect rect in guiCoordinates.inventorySlotRects)
                    {
                        if (rect.Contains(mousePos))
                        {
                            if (playerInventory.inventory[inventoryDragSlot].typeInSlot != "" && !playerInventory.inventory[inventoryDragSlot].typeInSlot.Equals("nothing"))
                            {
                                GUI.Label(guiCoordinates.inventoryMesageRect, playerInventory.inventory[inventoryDragSlot].typeInSlot);
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

                // MESSAGE TELLING THE PLAYER THEY ARE MISSING THE ITEMS REQUIRED TO CRAFT AN OBJECT
                if (craftingManager.missingItem == true)
                {
                    if (missingItemTimer < 3)
                    {
                        GUI.Label(guiCoordinates.inventoryMesageRect, "Missing items.");
                        missingItemTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        craftingManager.missingItem = false;
                        missingItemTimer = 0;
                    }
                }

                //! MESSAGE TELLING THE PLAYER THEIR INVENTORY IS FULL
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
                    // BUTTON WHICH OPENS THE CRAFTING GUI
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

                    // BUTTON THAT CLOSES THE INVENTORY GUI
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

                // CRAFTING GUI
                if (playerController.craftingGUIopen == true)
                {
                    if (craftingPage == 0)
                    {
                        if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Storage Container"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Storage container for objects and items. Can be used to manually store items or connected to machines for automation. Universal conduits, dark matter conduits, retrievers and auto crafters can all connect to storage containers." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Auger"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Extracts regolith from the lunar surface which can be pressed into bricks or smelted to create glass. Glass blocks have a 100% chance of being destroyed by meteors and other hazards. Bricks have a 75% chance of being destroyed by meteors and other hazards. Augers must be placed directly on the lunar surface and require power from a solar panel, nuclear reactor or power conduit." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Extruder"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Creates wire from copper and aluminum ingots. Creates pipes from iron and steel ingots. Ingots must be supplied to the extruder using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. The extruder requires power from a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Press"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Presses iron, copper, aluminum or tin ingots into plates. Ingots must be supplied to the press using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. Must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Gear Cutter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Cuts plates into gears. Plates must be supplied to the gear cutter using universal conduits. Place another conduit within 2 meters of the machine for the output. The gear cutter must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The gear cutter has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Universal Extractor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Extracts ore, coal and ice from deposits found on the lunar surface. Place within 2 meters of the desired resource and use a universal conduit to handle the harvested materials. When extracting ice, the extractor will not need a heat exchanger for cooling. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Universal Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Transfers items from a machine to another universal conduit, another machine or a storage container. Universal conduits have an adjustable input/output range and do not require power to operate." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Retriever"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Retrieves items from a storage container and transfers them to a universal conduit. Place an item of each desired type into the retrievers inventory to designate that item for retrieval. Place within 2 meters of a storage container and a universal conduit. This machine requires power and it's output is adjustable. If the retriever is moving ice, it will not require cooling. The retriever's output is measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Solar Panel"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Provides 2 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple solar panels can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Generator"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Provides 20 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple generators can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate. Generators must be connected to a universal conduit supplying coal for fuel." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Nuclear Reactor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Nuclear reactors are used to drive reactor turbines. Turbines must be directly attached to the reactor. The reactor will require a heat exchanger providing 5 KBTU cooling per turbine." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Reactor Turbine"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Provides 200 MW of power to a single machine or power conduit. Reactor turbines must be directly attached to a properly functioning, adequately cooled nuclear reactor. Must be placed within 4 meters of the machine. Multiple reactor turbines can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Power Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Transfers power from a power source to a machine or to another power conduit. When used with two outputs, power will be distributed evenly. This machine has an adjustable range setting." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button15Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Heat Exchanger"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Cools down a machine to allow overclocking. Requires a supply of ice from a universal conduit. Increasing the output of the heat exchanger increases the amount of ice required. This can be compensated for by overclocking the extractor that is supplying the ice. Machines cannot be connected to more than one heat exchanger. The heat exchanger's output is measured in KBTU and will consume 1 ice per 1 KBTU of cooling each cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button17Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Smelter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Smelts ore into ingots. Can also be used to make glass when supplied with regolith. Ore must be supplied to the smelter using universal conduits. Place another conduit within 2 meters of the machine for the output. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The output of a smelter is measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button18Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Alloy Smelter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Combines tin and copper ingots to make bronze ingots. Combines coal and iron ingots to make steel ingots. Requres 3 conduits. 1 for each input and 1 for the output. Requires a power source such as a solar panel, nuclear reactor or power conduit. The alloy smelter has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button19Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Dark Matter Collector"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Harvests dark matter which is then transferred to a dark matter conduit. Requires a power source such as a solar panel, nuclear reactor or power conduit. The dark matter collector has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button20Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Dark Matter Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Transfers dark matter from a collector to a storage container or another conduit. Dark matter conduits have an adjustable input/output range and do not require power to operate." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button21Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Auto Crafter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Automatically crafts objects using items from an attached storage container. Place within 2 meters of the storage container. Then, place an item of the desired type into the auto crafter's inventory. This will designate that item as the item to be crafted. Crafted items will be deposited into the attached storage container. This machine requires power and has an adjustable output measured in items per cycle." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button22Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Rail Cart Hub"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Provides a waypoint for rail carts. Has an adjustable range at which the next hub will be located and rails deployed to it's location. Rail cart hubs can be configured to stop the rail cart so it can be loaded and unloaded." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button23Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Rail Cart"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "A mobile storage container that rides on rails from one rail cart hub to the next. Configure the hubs to stop the cart near a conduit or retriever so it can be loaded or unloaded. Must be placed on a rail cart hub." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        if (GUI.Button(guiCoordinates.button1Rect, "Storage Container"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Storage Container"]);
                        }
                        if (GUI.Button(guiCoordinates.button2Rect, "Auger"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Auger"]);
                        }
                        if (GUI.Button(guiCoordinates.button3Rect, "Extruder"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Extruder"]);
                        }
                        if (GUI.Button(guiCoordinates.button4Rect, "Press"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Press"]);
                        }
                        if (GUI.Button(guiCoordinates.button5Rect, "Gear Cutter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Gear Cutter"]);
                        }
                        if (GUI.Button(guiCoordinates.button6Rect, "Universal Extractor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Universal Extractor"]);
                        }
                        if (GUI.Button(guiCoordinates.button7Rect, "Universal Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Universal Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button9Rect, "Retriever"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Retriever"]);
                        }
                        if (GUI.Button(guiCoordinates.button10Rect, "Solar Panel"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Solar Panel"]);
                        }
                        if (GUI.Button(guiCoordinates.button11Rect, "Generator"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Generator"]);
                        }
                        if (GUI.Button(guiCoordinates.button12Rect, "Nuclear Reactor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Nuclear Reactor"]);
                        }
                        if (GUI.Button(guiCoordinates.button13Rect, "Reactor Turbine"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Reactor Turbine"]);
                        }
                        if (GUI.Button(guiCoordinates.button14Rect, "Power Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Power Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button15Rect, "Heat Exchanger"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Heat Exchanger"]);
                        }
                        if (GUI.Button(guiCoordinates.button17Rect, "Smelter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Smelter"]);
                        }
                        if (GUI.Button(guiCoordinates.button18Rect, "Alloy Smelter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Alloy Smelter"]);
                        }
                        if (GUI.Button(guiCoordinates.button19Rect, "DM Collector"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Dark Matter Collector"]);
                        }
                        if (GUI.Button(guiCoordinates.button20Rect, "DM Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Dark Matter Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button21Rect, "Auto Crafter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Auto Crafter"]);
                        }
                        if (GUI.Button(guiCoordinates.button22Rect, "Rail Cart Hub"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Rail Cart Hub"]);
                        }
                        if (GUI.Button(guiCoordinates.button23Rect, "Rail Cart"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Rail Cart"]);
                        }
                    }
                    if (craftingPage == 1)
                    {
                        if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Iron Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Iron block for building structures. Iron blocks have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Iron Ramp"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Iron ramp for building structures. Iron ramps have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Steel Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Steel block for building structures. Steel blocks have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Steel Ramp"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Steel ramp for building structures. Steel ramps have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Quantum Hatchway"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Hatchway used for entering structures." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Electric Light"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "For interior lighting. Requires power from a solar panel, nuclear reactor or power conduit." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Circuit Board"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "A combination of conductive, semi-conductive and insulating materials combined to create a logic processing circuit." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Electric Motor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "A device that converts electrical energy to mechanical torque." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Storage Computer"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Provides access to all stationary storage containers within 4 meters. Can be accessed manually or connected to retrievers, auto crafters and conduits. When a conduit is connectextureDictionary.dictionary to the computer, the computer will store items starting with the first container found to have space available. When a retriever is connected to the computer, the computer will search all of the managed containers for desired items." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Turret"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, "Protects your equipment from meteor showers and other hazards. Requires a power source such as a solar panel, nuclear reactor or power conduit. Turrets have an adjustable output measured in rounds per minute." + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        if (GUI.Button(guiCoordinates.button1Rect, "Iron Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Iron Block"]);
                        }
                        if (GUI.Button(guiCoordinates.button2Rect, "Iron Ramp"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Iron Ramp"]);
                        }
                        if (GUI.Button(guiCoordinates.button3Rect, "Steel Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Steel Block"]);
                        }
                        if (GUI.Button(guiCoordinates.button4Rect, "Steel Ramp"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Steel Ramp"]);
                        }
                        if (GUI.Button(guiCoordinates.button5Rect, "Quantum Hatchway"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Quantum Hatchway"]);
                        }
                        if (GUI.Button(guiCoordinates.button6Rect, "Electric Light"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Electric Light"]);
                        }
                        if (GUI.Button(guiCoordinates.button7Rect, "Circuit Board"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Circuit Board"]);
                        }
                        if (GUI.Button(guiCoordinates.button9Rect, "Electric Motor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Electric Motor"]);
                        }
                        if (GUI.Button(guiCoordinates.button10Rect, "Storage Computer"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Storage Computer"]);
                        }
                        if (GUI.Button(guiCoordinates.button11Rect, "Turret"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Turret"]);
                        }
                    }
                    if (craftingPage == 2)
                    {
                        int index = 0;
                        KeyValuePair<string, CraftingRecipe>[] recipes = new KeyValuePair<string, CraftingRecipe>[craftingDictionary.modDictionary.Count];
                        foreach (KeyValuePair<string,CraftingRecipe> kvp in craftingDictionary.modDictionary)
                        {
                            recipes[index] = kvp;
                            index++;
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        if (GUI.Button(guiCoordinates.button3Rect, "<-"))
                        {
                            if (modCraftingIndex > 0)
                            {
                                modCraftingIndex--;
                            }
                            playerController.PlayButtonSound();
                        }

                        if (GUI.Button(guiCoordinates.button19Rect, "->"))
                        {
                            if (modCraftingIndex < recipes.Length - 1)
                            {
                                modCraftingIndex++;
                            }
                            playerController.PlayButtonSound();
                        }

                        if (GUI.Button(guiCoordinates.button11Rect, recipes[modCraftingIndex].Value.output))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.modDictionary[recipes[modCraftingIndex].Value.output]);
                        }

                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            string desc = recipes[modCraftingIndex].Value.output;
                            if (GetComponent<BuildController>().blockDictionary.machineDictionary.ContainsKey(recipes[modCraftingIndex].Value.output))
                            {
                                desc = GetComponent<BuildController>().blockDictionary.GetMachineDescription(recipes[modCraftingIndex].Value.output);
                            }
                            string[] crafting = new string[recipes[modCraftingIndex].Value.ingredients.Length];
                            for (int i = 0; i < recipes[modCraftingIndex].Value.ingredients.Length; i++)
                            {
                                crafting[i] = recipes[modCraftingIndex].Value.amounts[i] + "x " + recipes[modCraftingIndex].Value.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, desc + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                    }
                    if (GUI.Button(guiCoordinates.craftingPreviousRect, "<-"))
                    {
                        if (craftingPage > 0)
                        {
                            craftingPage -= 1;
                        }
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.craftingNextRect, "->"))
                    {
                        if (craftingPage < 1)
                        {
                            craftingPage += 1;
                        }
                        else if (craftingPage < 2 && craftingDictionary.modDictionary.Count > 0)
                        {
                            craftingPage += 1;
                        }
                        playerController.PlayButtonSound();
                    }
                }
            }
        }
    }
}