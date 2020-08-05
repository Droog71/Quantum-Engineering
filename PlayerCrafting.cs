using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCrafting : MonoBehaviour
{
    private PlayerController playerController;
    private InventoryManager playerInventory;
    public bool missingItem;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<InventoryManager>();
    }

    public void CraftItem(string[] ingredients, int[] amounts, string output, int outputAmount)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (int i = 0; i < amounts.Length; i++)
            {
                amounts[i] *= 10;
            }
            outputAmount *= 10;
        }

        int[] slots = new int[ingredients.Length];
        bool[] found = new bool[ingredients.Length];

        for (int i = 0; i < ingredients.Length; i++)
        {
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= amounts[i])
                {
                    if (slot.typeInSlot.Equals(ingredients[i]))
                    {
                        found[i] = true;
                        slots[i] = currentSlot;
                    }
                }
                currentSlot++;
            }
        }

        foreach (bool b in found)
        {
            if (!missingItem)
            {
                missingItem |= !b;
            }
        }

        if (!missingItem)
        {
            playerInventory.AddItem(output, outputAmount);
            if (playerInventory.itemAdded)
            {
                for (int i = 0; i < ingredients.Length; i++)
                {
                    playerInventory.inventory[slots[i]].amountInSlot -= amounts[i];
                    if (playerInventory.inventory[slots[i]].amountInSlot <= 0)
                    {
                        playerInventory.inventory[slots[i]].typeInSlot = "nothing";
                    }
                }
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.playMissingItemsSound();
        }
    }

    public void CraftIronBlock()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Iron Block") && slot.amountInSlot <= 900)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 10)
                        {
                            if (slot.typeInSlot.Equals("Iron Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 10;
                                playerInventory.AddItem("Iron Block", 100);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
        else
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Iron Block") && slot.amountInSlot <= 990)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 1)
                        {
                            if (slot.typeInSlot.Equals("Iron Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 1;
                                playerInventory.AddItem("Iron Block", 10);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
    }

    public void CraftIronRamp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Iron Ramp") && slot.amountInSlot <= 900)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 10)
                        {
                            if (slot.typeInSlot.Equals("Iron Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 10;
                                playerInventory.AddItem("Iron Ramp", 100);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
        else
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Iron Ramp") && slot.amountInSlot <= 990)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 1)
                        {
                            if (slot.typeInSlot.Equals("Iron Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 1;
                                playerInventory.AddItem("Iron Ramp", 10);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
    }

    public void CraftSteelBlock()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Steel Block") && slot.amountInSlot <= 900)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 10)
                        {
                            if (slot.typeInSlot.Equals("Steel Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 10;
                                playerInventory.AddItem("Steel Block", 100);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
        else
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Steel Block") && slot.amountInSlot <= 990)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 1)
                        {
                            if (slot.typeInSlot.Equals("Steel Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 1;
                                playerInventory.AddItem("Steel Block", 10);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
    }

    public void CraftSteelRamp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Steel Ramp") && slot.amountInSlot <= 900)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 10)
                        {
                            if (slot.typeInSlot.Equals("Steel Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 10;
                                playerInventory.AddItem("Steel Ramp", 100);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
        else
        {
            bool spaceAvailable = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Steel Ramp") && slot.amountInSlot <= 990)
                {
                    spaceAvailable = true;
                }
            }
            if (spaceAvailable == true)
            {
                bool foundItems = false;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (foundItems == false)
                    {
                        if (slot.amountInSlot >= 1)
                        {
                            if (slot.typeInSlot.Equals("Steel Plate"))
                            {
                                foundItems = true;
                                slot.amountInSlot -= 1;
                                playerInventory.AddItem("Steel Ramp", 10);
                                playerController.playCraftingSound();
                            }
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
                if (foundItems == false)
                {
                    missingItem = true;
                    playerController.playMissingItemsSound();
                }
            }
            else
            {
                playerController.outOfSpace = true;
                playerController.playMissingItemsSound();
            }
        }
    }

    public void CraftQuantumHatchway()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Quantum Hatchway") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundTin = false;
            bool foundDarkMatter = false;
            int tinSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTin = true;
                        tinSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundTin == false || foundDarkMatter == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[tinSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                if (playerInventory.inventory[tinSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Quantum Hatchway", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftElectricLight()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Electric Light") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundGlass = false;
            bool foundCopperWire = false;
            bool foundTinPlates = false;
            int glassSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int tinPlateSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlates = true;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundGlass == false || foundCopperWire == false || foundTinPlates == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[glassSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 2;
                if (playerInventory.inventory[glassSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Electric Light", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftAuger()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Auger") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIron = false;
            bool foundCopper = false;
            int ironSlotNumber = 0;
            int copperSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIron = true;
                        ironSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Ingot"))
                    {
                        foundCopper = true;
                        copperSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIron == false || foundCopper == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Auger", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftStorageContainer()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Storage Container") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundItems = false;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (foundItems == false)
                {
                    if (slot.amountInSlot >= 6)
                    {
                        if (slot.typeInSlot.Equals("Iron Plate"))
                        {
                            foundItems = true;
                            slot.amountInSlot -= 6;
                            playerInventory.AddItem("Storage Container", 1);
                            playerController.playCraftingSound();
                        }
                        if (slot.amountInSlot <= 0)
                        {
                            slot.typeInSlot = "nothing";
                        }
                    }
                }
            }
            if (foundItems == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftStorageComputer()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Storage Computer") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundRetriever = false;
            bool foundUniversalConduit = false;
            bool foundAluminumPlate = false;
            bool foundCopperWire = false;
            bool foundTinGear = false;
            bool foundDarkMatterConduit = false;
            bool foundGlassBlock = false;
            int retrieverSlotNumber = 0;
            int universalConduitSlotNubmer = 0;
            int aluminumPlateSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int tinGearSlotNumber = 0;
            int darkMatterConduitSlotNumber = 0;
            int glassBlockSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Retriever"))
                    {
                        foundRetriever = true;
                        retrieverSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Universal Conduit"))
                    {
                        foundUniversalConduit = true;
                        universalConduitSlotNubmer = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Aluminum Plate"))
                    {
                        foundAluminumPlate = true;
                        aluminumPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter Conduit"))
                    {
                        foundDarkMatterConduit = true;
                        darkMatterConduitSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundRetriever == false || foundUniversalConduit == false || foundAluminumPlate == false || foundCopperWire == false || foundTinGear == false || foundDarkMatterConduit == false || foundGlassBlock == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[retrieverSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[universalConduitSlotNubmer].amountInSlot -= 5;
                playerInventory.inventory[aluminumPlateSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[tinGearSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[darkMatterConduitSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                if (playerInventory.inventory[retrieverSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[retrieverSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[universalConduitSlotNubmer].amountInSlot <= 0)
                {
                    playerInventory.inventory[universalConduitSlotNubmer].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterConduitSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterConduitSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Storage Computer", 1);
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftExtruder()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Extruder") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIron = false;
            bool foundCopper = false;
            int ironSlotNumber = 0;
            int copperSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIron = true;
                        ironSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Ingot"))
                    {
                        foundCopper = true;
                        copperSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIron == false || foundCopper == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Extruder", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftPress()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Press") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronIngot = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            int ironPipeSlotNumber = 0;
            int ironIngotSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIronIngot = true;
                        ironIngotSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronIngot == false || foundIronPipe == false || foundCopperWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironIngotSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironIngotSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironIngotSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Press", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftUniversalExtractor()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Universal Extractor") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            bool foundDarkMatter = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundDarkMatter == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Universal Extractor", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftUniversalConduit()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Universal Conduit") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            bool foundDarkMatter = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundDarkMatter == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 5;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Universal Conduit", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftRetriever()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Retriever") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            bool foundCircuitBoard = false;
            bool foundElectricMotor = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int circuitBoardSlotNumber = 0;
            int electricMotorSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundElectricMotor == false || foundCircuitBoard == false || foundIronPlate == false || foundIronPipe == false || foundCopperWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[circuitBoardSlotNumber].amountInSlot -= 2;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Retriever", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftGenerator()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Generator") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundSmelter = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            bool foundIronPlate = false;
            bool foundElectricMotor = false;
            bool foundIronGear = false;
            int smelterSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int ironPlateSlotNumber = 0;
            int electricMotorSlotNumber = 0;
            int ironGearSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Smelter"))
                    {
                        foundSmelter = true;
                        smelterSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundSmelter == false || foundIronPipe == false || foundCopperWire == false || foundElectricMotor == false || foundIronGear == false || foundIronPlate == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[smelterSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[electricMotorSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[ironGearSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[smelterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[smelterSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Generator", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftReactorTurbine()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Reactor Turbine") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundGenerator = false;
            bool foundCopperWire = false;
            bool foundSteelPipe = false;
            bool foundSteelPlate = false;
            bool foundSteelGear = false;
            bool foundGlassBlock = false;
            int generatorSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int steelPipeSlotNumber = 0;
            int steelPlateSlotNumber = 0;
            int steelGearSlotNumber = 0;
            int glassBlockSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Generator"))
                    {
                        foundGenerator = true;
                        generatorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundGenerator == false || foundSteelPipe == false || foundCopperWire == false || foundSteelGear == false || foundSteelPlate == false || foundGlassBlock == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[generatorSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[steelPipeSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[steelGearSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[generatorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[generatorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Reactor Turbine", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftCircuitBoard()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Circuit Board") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundTinPlate = false;
            bool foundGlassBlock = false;
            bool foundCopperWire = false;
            bool foundDarkMatter = false;
            int tinPlateSlotNumber = 0;
            int glassBlockSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundTinPlate == false || foundGlassBlock == false || foundCopperWire == false || foundDarkMatter == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                if (playerInventory.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Circuit Board", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftMotor()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Electric Motor") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundIronGear = false;
            bool foundCopperWire = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int ironGearSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundIronGear == false || foundCopperWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[ironGearSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Electric Motor", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftAutoCrafter()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Auto Crafter") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundBronzeGear = false;
            bool foundSteelPlate = false;
            bool foundElectricMotor = false;
            bool foundCircuitBoard = false;
            bool foundDarkMatter = false;
            int bronzeGearSlotNumber = 0;
            int steelPlateSlotNumber = 0;
            int electricMotorSlotNumber = 0;
            int circuitBoardSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundBronzeGear == false || foundSteelPlate == false || foundCircuitBoard == false || foundElectricMotor == false || foundDarkMatter == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[bronzeGearSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Auto Crafter", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftSolarPanel()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Solar Panel") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            bool foundCopperPlate = false;
            bool foundGlass = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int copperPlateSlotNumber = 0;
            int glassSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Plate"))
                    {
                        foundCopperPlate = true;
                        copperPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundGlass == false || foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundCopperPlate == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[copperPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[glassSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[glassSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Solar Panel", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftPowerConduit()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Power Conduit") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundAluminumPlate = false;
            bool foundGlassBlock = false;
            bool foundCopperWire = false;
            int aluminumPlateSlotNumber = 0;
            int glassBlockSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Aluminum Plate"))
                    {
                        foundAluminumPlate = true;
                        aluminumPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundGlassBlock == false || foundCopperWire == false || foundAluminumPlate == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[aluminumPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[glassBlockSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Power Conduit", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftNuclearReactor()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Nuclear Reactor") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundSteelPlate = false;
            bool foundSteelPipe = false;
            bool foundCopperWire = false;
            bool foundCopperPlate = false;
            bool foundDarkMatter = false;
            bool foundGlass = false;
            int steelPlateSlotNumber = 0;
            int steelPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int copperPlateSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int glassSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Plate"))
                    {
                        foundCopperPlate = true;
                        copperPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundDarkMatter == false || foundGlass == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundCopperPlate == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[steelPipeSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperPlateSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[glassSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Nuclear Reactor", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftHeatExchanger()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Heat Exchanger") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundPlates = false;
            bool foundPipe = false;
            int plateSlotNumber = 0;
            int pipeSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundPlates = true;
                        plateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundPipe = true;
                        pipeSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundPlates == false || foundPipe == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[plateSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[pipeSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[plateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[plateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[pipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[pipeSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Heat Exchanger", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftSmelter()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Smelter") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCopperWire = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Smelter", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftRailCartHub()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Rail Cart Hub") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundCircuitBoard = false;
            int circuitBoardSlotNumber = 0;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 6)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundCircuitBoard == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 6;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[circuitBoardSlotNumber].amountInSlot -= 1;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Rail Cart Hub", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftRailCart()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Rail Cart") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundElectricMotor = false;
            bool foundCopperWire = false;
            bool foundTinPlate = false;
            bool foundAluminumGear = false;
            bool foundStorageContainer = false;
            bool foundSolarPanel = false;
            int electricMotorSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int tinPlateSlotNumber = 0;
            int aluminumGearSlotNumber = 0;
            int storageContainerSlotNumber = 0;
            int solarPanelSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 8)
                {
                    if (slot.typeInSlot.Equals("Aluminum Gear"))
                    {
                        foundAluminumGear = true;
                        aluminumGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Solar Panel"))
                    {
                        foundSolarPanel = true;
                        solarPanelSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Storage Container"))
                    {
                        foundStorageContainer = true;
                        storageContainerSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundSolarPanel == false || foundElectricMotor == false || foundCopperWire == false || foundTinPlate == false || foundAluminumGear == false || foundStorageContainer == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[tinPlateSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[aluminumGearSlotNumber].amountInSlot -= 8;
                playerInventory.inventory[storageContainerSlotNumber].amountInSlot -= 1;
                playerInventory.inventory[solarPanelSlotNumber].amountInSlot -= 1;
                if (playerInventory.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[storageContainerSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[storageContainerSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[solarPanelSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[solarPanelSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Rail Cart", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftGearCutter()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Gear Cutter") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundTinPlate = false;
            bool foundAluminumWire = false;
            bool foundCopperWire = false;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int tinPlateslotNumber = 0;
            int aluminumWireSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateslotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronPlate == false || foundIronPipe == false || foundTinPlate == false || foundCopperWire == false || foundAluminumWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[tinPlateslotNumber].amountInSlot -= 5;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinPlateslotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Gear Cutter", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftAlloySmelter()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Alloy Smelter") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundIronPlate = false;
            bool foundIronPipe = false;
            bool foundTinPlate = false;
            bool foundAluminumWire = false;
            bool foundCopperWire = false;
            bool foundIronGear = false;
            int ironGearSlotNumber = 0;
            int ironPlateSlotNumber = 0;
            int ironPipeSlotNumber = 0;
            int tinPlateslotNumber = 0;
            int aluminumWireSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 20)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateslotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 40)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundIronGear == false || foundIronPlate == false || foundIronPipe == false || foundTinPlate == false || foundCopperWire == false || foundAluminumWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[ironPlateSlotNumber].amountInSlot -= 20;
                playerInventory.inventory[ironPipeSlotNumber].amountInSlot -= 20;
                playerInventory.inventory[tinPlateslotNumber].amountInSlot -= 20;
                playerInventory.inventory[ironGearSlotNumber].amountInSlot -= 20;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 40;
                playerInventory.inventory[aluminumWireSlotNumber].amountInSlot -= 40;
                if (playerInventory.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinPlateslotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Alloy Smelter", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftTurret()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Turret") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundSteelPlate = false;
            bool foundSteelPipe = false;
            bool foundBronzePlate = false;
            bool foundAluminumWire = false;
            bool foundCopperWire = false;
            bool foundSteelGear = false;
            bool foundElectricMotor = false;
            bool foundCircuitBoard = false;
            int steelPlateSlotNumber = 0;
            int steelPipeSlotNumber = 0;
            int bronzePlateSlotNumber = 0;
            int aluminumWireSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int steelGearSlotNumber = 0;
            int electricMotorSlotNumber = 0;
            int circuitBoardSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Plate"))
                    {
                        foundBronzePlate = true;
                        bronzePlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundElectricMotor == false || foundCircuitBoard == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundBronzePlate == false || foundCopperWire == false || foundAluminumWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[steelPipeSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[bronzePlateSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[steelGearSlotNumber].amountInSlot -= 5;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                playerInventory.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                playerInventory.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[bronzePlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[bronzePlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Turret", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftDarkMatterCollector()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Dark Matter Collector") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundSteelPlate = false;
            bool foundSteelPipe = false;
            bool foundTinGear = false;
            bool foundAluminumWire = false;
            bool foundCopperWire = false;
            bool foundSteelGear = false;
            bool foundBronzeGear = false;
            bool foundDarkMatter = false;
            int steelPlateSlotNumber = 0;
            int steelPipeSlotNumber = 0;
            int tinGearSlotNumber = 0;
            int aluminumWireSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int steelGearSlotNumber = 0;
            int bronzeGearSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 50)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 100)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundDarkMatter == false || foundTinGear == false || foundBronzeGear == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundAluminumWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[steelPipeSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[tinGearSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[steelGearSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[bronzeGearSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 100;
                playerInventory.inventory[aluminumWireSlotNumber].amountInSlot -= 100;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 100;
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Dark Matter Collector", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

    public void CraftDarkMatterConduit()
    {
        bool spaceAvailable = false;
        foreach (InventorySlot slot in playerInventory.inventory)
        {
            if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals("Dark Matter Conduit") && slot.amountInSlot < 1000)
            {
                spaceAvailable = true;
            }
        }
        if (spaceAvailable == true)
        {
            bool foundSteelPlate = false;
            bool foundSteelPipe = false;
            bool foundTinGear = false;
            bool foundAluminumWire = false;
            bool foundCopperWire = false;
            bool foundSteelGear = false;
            bool foundBronzeGear = false;
            bool foundDarkMatter = false;
            int steelPlateSlotNumber = 0;
            int steelPipeSlotNumber = 0;
            int tinGearSlotNumber = 0;
            int aluminumWireSlotNumber = 0;
            int copperWireSlotNumber = 0;
            int steelGearSlotNumber = 0;
            int bronzeGearSlotNumber = 0;
            int darkMatterSlotNumber = 0;
            int currentSlot = 0;
            foreach (InventorySlot slot in playerInventory.inventory)
            {
                if (slot.amountInSlot >= 25)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 50)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
            if (foundDarkMatter == false || foundTinGear == false || foundBronzeGear == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundAluminumWire == false)
            {
                missingItem = true;
                playerController.playMissingItemsSound();
            }
            else
            {
                playerInventory.inventory[steelPlateSlotNumber].amountInSlot -= 25;
                playerInventory.inventory[steelPipeSlotNumber].amountInSlot -= 25;
                playerInventory.inventory[tinGearSlotNumber].amountInSlot -= 25;
                playerInventory.inventory[steelGearSlotNumber].amountInSlot -= 25;
                playerInventory.inventory[bronzeGearSlotNumber].amountInSlot -= 25;
                playerInventory.inventory[copperWireSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[aluminumWireSlotNumber].amountInSlot -= 50;
                playerInventory.inventory[darkMatterSlotNumber].amountInSlot -= 50;
                if (playerInventory.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (playerInventory.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    playerInventory.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
                playerInventory.AddItem("Dark Matter Conduit", 1);
                playerController.playCraftingSound();
            }
        }
        else
        {
            playerController.outOfSpace = true;
            playerController.playMissingItemsSound();
        }
    }

}
