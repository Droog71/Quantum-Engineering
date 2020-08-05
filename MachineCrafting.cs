using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MachineCrafting : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public bool missingItem;

    public void CraftItem(string[] ingredients, int[] amounts, string output, int outputAmount)
    {
        int[] slots = new int[ingredients.Length];
        bool[] found = new bool[ingredients.Length];

        for (int i = 0; i < ingredients.Length; i++)
        {
            int currentSlot = 0;
            foreach (InventorySlot slot in inventoryManager.inventory)
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
            inventoryManager.AddItem(output, outputAmount);
            if (inventoryManager.itemAdded)
            {
                for (int i = 0; i < ingredients.Length; i++)
                {
                    inventoryManager.inventory[slots[i]].amountInSlot -= amounts[i];
                    if (inventoryManager.inventory[slots[i]].amountInSlot <= 0)
                    {
                        inventoryManager.inventory[slots[i]].typeInSlot = "nothing";
                    }
                }
            }
        }
    }

    public bool CraftIronBlock()
    {
        bool foundItems = false;
        foreach (InventorySlot slot in inventoryManager.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundItems = true;
                        inventoryManager.AddItem("Iron Block", 10);
                        if (inventoryManager.itemAdded == true)
                        {
                            slot.amountInSlot -= 1;
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
        }
        return true;
    }

    public bool CraftIronRamp()
    {
        bool foundItems = false;
        foreach (InventorySlot slot in inventoryManager.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundItems = true;
                        inventoryManager.AddItem("Iron Ramp", 10);
                        if (inventoryManager.itemAdded == true)
                        {
                            slot.amountInSlot -= 1;
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
        }
        return true;
    }

    public bool CraftSteelBlock()
    {
        bool foundItems = false;
        foreach (InventorySlot slot in inventoryManager.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundItems = true;
                        inventoryManager.AddItem("Steel Block", 10);
                        if (inventoryManager.itemAdded == true)
                        {
                            slot.amountInSlot -= 1;
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
        }
        return true;
    }

    public bool CraftSteelRamp()
    {
        bool foundItems = false;
        foreach (InventorySlot slot in inventoryManager.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundItems = true;
                        inventoryManager.AddItem("Steel Ramp", 10);
                        if (inventoryManager.itemAdded == true)
                        {
                            slot.amountInSlot -= 1;
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
        }
        return true;
    }

    public bool CraftQuantumHatchway()
    {
        bool foundTin = false;
        bool foundDarkMatter = false;
        int tinSlotNumber = 0;
        int darkMatterSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Quantum Hatchway", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[tinSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                if (inventoryManager.inventory[tinSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftElectricLight()
    {
        bool foundGlass = false;
        bool foundCopperWire = false;
        bool foundTinPlates = false;
        int glassSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int tinPlateSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Electric Light", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[glassSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 2;
                if (inventoryManager.inventory[glassSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[glassSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftAuger()
    {
        bool foundIron = false;
        bool foundCopper = false;
        int ironSlotNumber = 0;
        int copperSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Auger", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftStorageContainer()
    {
        bool foundItems = false;
        foreach (InventorySlot slot in inventoryManager.inventory)
        {
            if (foundItems == false)
            {
                if (slot.amountInSlot >= 6)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundItems = true;
                        inventoryManager.AddItem("Storage Container", 1);
                        if (inventoryManager.itemAdded == true)
                        {
                            slot.amountInSlot -= 6;
                            if (slot.amountInSlot <= 0)
                            {
                                slot.typeInSlot = "nothing";
                            }
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
        }
        return true;
    }

    public bool CraftExtruder()
    {
        bool foundIron = false;
        bool foundCopper = false;
        int ironSlotNumber = 0;
        int copperSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Extruder", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftPress()
    {
        bool foundIronIngot = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        int ironPipeSlotNumber = 0;
        int ironIngotSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Press", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironIngotSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironIngotSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironIngotSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftUniversalExtractor()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Universal Extractor", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftUniversalConduit()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Universal Conduit", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 5;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftRetriever()
    {
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        bool foundElectricMotor = false;
        bool foundCircuitBoard = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int electricMotorSlotNumber = 0;
        int circuitBoardSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundElectricMotor == false || foundCircuitBoard == false)
        {
            missingItem = true;
        }
        else
        {
            inventoryManager.AddItem("Retriever", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot -= 2;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftGenerator()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Generator", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[smelterSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[electricMotorSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[ironGearSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[smelterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[smelterSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftReactorTurbine()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Reactor Turbine", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[generatorSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[steelPipeSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[steelGearSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[generatorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[generatorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftRailCartHub()
    {
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCircuitBoard = false;
        int circuitBoardSlotNumber = 0;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Rail Cart Hub", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 6;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot -= 1;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftRailCart()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
                if (slot.typeInSlot.Equals("Storage Container"))
                {
                    foundStorageContainer = true;
                    storageContainerSlotNumber = currentSlot;
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
            currentSlot++;
        }
        if (foundSolarPanel == false || foundElectricMotor == false || foundCopperWire == false || foundTinPlate == false || foundAluminumGear == false || foundStorageContainer == false)
        {
            missingItem = true;
        }
        else
        {
            inventoryManager.AddItem("Rail Cart", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[tinPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[aluminumGearSlotNumber].amountInSlot -= 8;
                inventoryManager.inventory[storageContainerSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[solarPanelSlotNumber].amountInSlot -= 1;
                if (inventoryManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[storageContainerSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[storageContainerSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[solarPanelSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[solarPanelSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftCircuitBoard()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Circuit Board", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                if (inventoryManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftMotor()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Electric Motor", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[ironGearSlotNumber].amountInSlot -= 2;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftAutoCrafter()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Auto Crafter", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftSolarPanel()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Solar Panel", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[copperPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[glassSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftPowerConduit()
    {
        bool foundAluminumPlate = false;
        bool foundGlassBlock = false;
        bool foundCopperWire = false;
        int aluminumPlateSlotNumber = 0;
        int glassBlockSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Power Conduit", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[aluminumPlateSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[glassBlockSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftNuclearReactor()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Nuclear Reactor", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[steelPipeSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperPlateSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[glassSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftHeatExchanger()
    {
        bool foundPlates = false;
        bool foundPipe = false;
        int plateSlotNumber = 0;
        int pipeSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Heat Exchanger", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[plateSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[pipeSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[plateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[plateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[pipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[pipeSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftSmelter()
    {
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Smelter", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftGearCutter()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Gear Cutter", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[tinPlateslotNumber].amountInSlot -= 5;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinPlateslotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftStorageComputer()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Storage Computer", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[retrieverSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[universalConduitSlotNubmer].amountInSlot -= 5;
                inventoryManager.inventory[aluminumPlateSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[tinGearSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[darkMatterConduitSlotNumber].amountInSlot -= 1;
                inventoryManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                if (inventoryManager.inventory[retrieverSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[retrieverSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[universalConduitSlotNubmer].amountInSlot <= 0)
                {
                    inventoryManager.inventory[universalConduitSlotNubmer].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterConduitSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterConduitSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftAlloySmelter()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Alloy Smelter", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[ironPlateSlotNumber].amountInSlot -= 20;
                inventoryManager.inventory[ironPipeSlotNumber].amountInSlot -= 20;
                inventoryManager.inventory[tinPlateslotNumber].amountInSlot -= 20;
                inventoryManager.inventory[ironGearSlotNumber].amountInSlot -= 20;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 40;
                inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot -= 40;
                if (inventoryManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinPlateslotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftTurret()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Turret", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[steelPipeSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[bronzePlateSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[steelGearSlotNumber].amountInSlot -= 5;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                inventoryManager.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[bronzePlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[bronzePlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftDarkMatterCollector()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Dark Matter Collector", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[steelPipeSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[tinGearSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[steelGearSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 100;
                inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot -= 100;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 100;
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }

    public bool CraftDarkMatterConduit()
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
        foreach (InventorySlot slot in inventoryManager.inventory)
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
        }
        else
        {
            inventoryManager.AddItem("Dark Matter Conduit", 1);
            if (inventoryManager.itemAdded == true)
            {
                inventoryManager.inventory[steelPlateSlotNumber].amountInSlot -= 25;
                inventoryManager.inventory[steelPipeSlotNumber].amountInSlot -= 25;
                inventoryManager.inventory[tinGearSlotNumber].amountInSlot -= 25;
                inventoryManager.inventory[steelGearSlotNumber].amountInSlot -= 25;
                inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot -= 25;
                inventoryManager.inventory[copperWireSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot -= 50;
                inventoryManager.inventory[darkMatterSlotNumber].amountInSlot -= 50;
                if (inventoryManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                }
                if (inventoryManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                {
                    inventoryManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                }
            }
        }
        return true;
    }
}

