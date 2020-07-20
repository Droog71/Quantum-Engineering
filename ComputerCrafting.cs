using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComputerCrafting : MonoBehaviour
{
    public InventoryManager[] computerManager;
    public bool missingItem;
    public ConduitItem conduitItem;

    void OnDestroy()
    {
        if (conduitItem != null)
        {
            conduitItem.active = false;
        }
    }

    public void CraftIronBlock()
    {
        InventorySlot ironPlateSlot = null;
        bool foundItems = false;
        foreach (InventoryManager manager in computerManager)
        {
            if (foundItems == false)
            {
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= 1)
                    {
                        if (slot.typeInSlot.Equals("Iron Plate"))
                        {
                            foundItems = true;
                            ironPlateSlot = slot;
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (ironPlateSlot != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Iron Block", 10);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateSlot.amountInSlot -= 1;
                        if (ironPlateSlot.amountInSlot <= 0)
                        {
                            ironPlateSlot.typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Iron Block"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftIronRamp()
    {
        InventorySlot ironPlateSlot = null;
        bool foundItems = false;
        foreach (InventoryManager manager in computerManager)
        {
            if (foundItems == false)
            {
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= 1)
                    {
                        if (slot.typeInSlot.Equals("Iron Plate"))
                        {
                            foundItems = true;
                            ironPlateSlot = slot;
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (ironPlateSlot != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Iron Ramp", 10);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateSlot.amountInSlot -= 1;
                        if (ironPlateSlot.amountInSlot <= 0)
                        {
                            ironPlateSlot.typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Iron Ramp"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftSteelBlock()
    {
        InventorySlot steelPlateSlot = null;
        bool foundItems = false;
        foreach (InventoryManager manager in computerManager)
        {
            if (foundItems == false)
            {
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= 1)
                    {
                        if (slot.typeInSlot.Equals("Steel Plate"))
                        {
                            foundItems = true;
                            steelPlateSlot = slot;
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (steelPlateSlot != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Steel Block", 10);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateSlot.amountInSlot -= 1;
                        if (steelPlateSlot.amountInSlot <= 0)
                        {
                            steelPlateSlot.typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Steel Block"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftSteelRamp()
    {
        InventorySlot steelPlateSlot = null;
        bool foundItems = false;
        foreach (InventoryManager manager in computerManager)
        {
            if (foundItems == false)
            {
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= 1)
                    {
                        if (slot.typeInSlot.Equals("Steel Plate"))
                        {
                            foundItems = true;
                            steelPlateSlot = slot;
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (steelPlateSlot != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Steel Ramp", 10);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateSlot.amountInSlot -= 1;
                        if (steelPlateSlot.amountInSlot <= 0)
                        {
                            steelPlateSlot.typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Steel Ramp"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftStorageContainer()
    {
        InventorySlot ironPlateSlot = null;
        bool foundItems = false;
        foreach (InventoryManager manager in computerManager)
        {
            if (foundItems == false)
            {
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= 6)
                    {
                        if (slot.typeInSlot.Equals("Iron Plate"))
                        {
                            foundItems = true;
                            ironPlateSlot = slot;
                        }
                    }
                }
            }
        }
        if (foundItems == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (ironPlateSlot != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Storage Container", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateSlot.amountInSlot -= 6;
                        if (ironPlateSlot.amountInSlot <= 0)
                        {
                            ironPlateSlot.typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Storage Container"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftQuantumHatchway()
    {
        InventoryManager tinManager = null;
        InventoryManager darkMatterManager = null;
        bool foundTin = false;
        bool foundDarkMatter = false;
        int tinSlotNumber = 0;
        int darkMatterSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTin = true;
                        tinManager = manager;
                        tinSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundTin == false || foundDarkMatter == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else if (tinManager != null && darkMatterManager != null)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Quantum Hatchway", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        tinManager.inventory[tinSlotNumber].amountInSlot -= 1;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                        if (tinManager.inventory[tinSlotNumber].amountInSlot <= 0)
                        {
                            tinManager.inventory[tinSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Quantum Hatchway"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftElectricLight()
    {
        InventoryManager glassBlockManager = null;
        InventoryManager tinPlateManager = null;
        InventoryManager copperWireManager = null;
        bool foundGlass = false;
        bool foundCopperWire = false;
        bool foundTinPlates = false;
        int glassSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int tinPlateSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassBlockManager = manager;
                        glassSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlates = true;
                        tinPlateManager = manager;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundGlass == false || foundCopperWire == false || foundTinPlates == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Electric Light", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        glassBlockManager.inventory[glassSlotNumber].amountInSlot -= 1;
                        tinPlateManager.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 2;
                        if (glassBlockManager.inventory[glassSlotNumber].amountInSlot <= 0)
                        {
                            glassBlockManager.inventory[glassSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinPlateManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            tinPlateManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Electric Light"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftAuger()
    {
        InventoryManager ironIngotManager = null;
        InventoryManager copperIngotManager = null;
        bool foundIron = false;
        bool foundCopper = false;
        int ironSlotNumber = 0;
        int copperSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIron = true;
                        ironIngotManager = manager;
                        ironSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Ingot"))
                    {
                        foundCopper = true;
                        copperIngotManager = manager;
                        copperSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIron == false || foundCopper == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Auger", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironIngotManager.inventory[ironSlotNumber].amountInSlot -= 10;
                        copperIngotManager.inventory[copperSlotNumber].amountInSlot -= 10;
                        if (ironIngotManager.inventory[ironSlotNumber].amountInSlot <= 0)
                        {
                            ironIngotManager.inventory[ironSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperIngotManager.inventory[copperSlotNumber].amountInSlot <= 0)
                        {
                            copperIngotManager.inventory[copperSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Auger"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftStorageComputer()
    {
        InventoryManager retrieverManager = null;
        InventoryManager universalConduitManager = null;
        InventoryManager aluminumPlateManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager tinGearManager = null;
        InventoryManager darkMatterConduitManager = null;
        InventoryManager glassBlockManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Retriever"))
                    {
                        foundRetriever = true;
                        retrieverManager = manager;
                        retrieverSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Universal Conduit"))
                    {
                        foundUniversalConduit = true;
                        universalConduitManager = manager;
                        universalConduitSlotNubmer = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Aluminum Plate"))
                    {
                        foundAluminumPlate = true;
                        aluminumPlateManager = manager;
                        aluminumPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearManager = manager;
                        tinGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter Conduit"))
                    {
                        foundDarkMatterConduit = true;
                        darkMatterConduitManager = manager;
                        darkMatterConduitSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockManager = manager;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundRetriever == false || foundUniversalConduit == false || foundAluminumPlate == false || foundCopperWire == false || foundTinGear == false || foundDarkMatterConduit == false || foundGlassBlock == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Storage Computer", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        retrieverManager.inventory[retrieverSlotNumber].amountInSlot -= 5;
                        universalConduitManager.inventory[universalConduitSlotNubmer].amountInSlot -= 5;
                        aluminumPlateManager.inventory[aluminumPlateSlotNumber].amountInSlot -= 5;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        tinGearManager.inventory[tinGearSlotNumber].amountInSlot -= 10;
                        darkMatterConduitManager.inventory[darkMatterConduitSlotNumber].amountInSlot -= 1;
                        glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                        if (retrieverManager.inventory[retrieverSlotNumber].amountInSlot <= 0)
                        {
                            retrieverManager.inventory[retrieverSlotNumber].typeInSlot = "nothing";
                        }
                        if (universalConduitManager.inventory[universalConduitSlotNubmer].amountInSlot <= 0)
                        {
                            universalConduitManager.inventory[universalConduitSlotNubmer].typeInSlot = "nothing";
                        }
                        if (aluminumPlateManager.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                        {
                            aluminumPlateManager.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinGearManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                        {
                            tinGearManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterConduitManager.inventory[darkMatterConduitSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterConduitManager.inventory[darkMatterConduitSlotNumber].typeInSlot = "nothing";
                        }
                        if (glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                        {
                            glassBlockManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Storage Computer"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftExtruder()
    {
        InventoryManager ironIngotManager = null;
        InventoryManager copperIngotManager = null;
        bool foundIron = false;
        bool foundCopper = false;
        int ironSlotNumber = 0;
        int copperSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIron = true;
                        ironIngotManager = manager;
                        ironSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Ingot"))
                    {
                        foundCopper = true;
                        copperIngotManager = manager;
                        copperSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }

        }
        if (foundIron == false || foundCopper == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Extruder", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironIngotManager.inventory[ironSlotNumber].amountInSlot -= 10;
                        copperIngotManager.inventory[copperSlotNumber].amountInSlot -= 10;
                        if (ironIngotManager.inventory[ironSlotNumber].amountInSlot <= 0)
                        {
                            ironIngotManager.inventory[ironSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperIngotManager.inventory[copperSlotNumber].amountInSlot <= 0)
                        {
                            copperIngotManager.inventory[copperSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Extruder"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftPress()
    {
        InventoryManager ironIngotManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        bool foundIronIngot = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        int ironPipeSlotNumber = 0;
        int ironIngotSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Ingot"))
                    {
                        foundIronIngot = true;
                        ironIngotManager = manager;
                        ironIngotSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronIngot == false || foundIronPipe == false || foundCopperWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Press", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironIngotManager.inventory[ironIngotSlotNumber].amountInSlot -= 10;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                        if (ironIngotManager.inventory[ironIngotSlotNumber].amountInSlot <= 0)
                        {
                            ironIngotManager.inventory[ironIngotSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Press"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftUniversalExtractor()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager darkMatterManager = null;
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        bool foundDarkMatter = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int darkMatterSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundDarkMatter == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Universal Extractor", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Universal Extractor"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftUniversalConduit()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager darkMatterManager = null;
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        bool foundDarkMatter = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int darkMatterSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundDarkMatter == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Universal Conduit", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 5;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 5;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Universal Conduit"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftRetriever()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager electricMotorManager = null;
        InventoryManager circuitBoardManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorManager = manager;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardManager = manager;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }

        if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundElectricMotor == false || foundCircuitBoard == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Retriever", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                        electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                        circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot -= 2;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                        {
                            electricMotorManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                        }
                        if (circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                        {
                            circuitBoardManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Retriever"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftGenerator()
    {
        InventoryManager smelterManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager ironPlateManager = null;
        InventoryManager electricMotorManager = null;
        InventoryManager ironGearManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Smelter"))
                    {
                        foundSmelter = true;
                        smelterManager = manager;
                        smelterSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorManager = manager;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearManager = manager;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }

        if (foundSmelter == false || foundIronPipe == false || foundCopperWire == false || foundElectricMotor == false || foundIronGear == false || foundIronPlate == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Generator", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        smelterManager.inventory[smelterSlotNumber].amountInSlot -= 1;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 2;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                        electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot -= 1;
                        ironGearManager.inventory[ironGearSlotNumber].amountInSlot -= 2;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                        if (smelterManager.inventory[smelterSlotNumber].amountInSlot <= 0)
                        {
                            smelterManager.inventory[smelterSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                        {
                            electricMotorManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironGearManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                        {
                            ironGearManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Generator"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftReactorTurbine()
    {
        InventoryManager generatorManager = null;
        InventoryManager steelPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager steelPlateManager = null;
        InventoryManager glassBlockManager = null;
        InventoryManager steelGearManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Generator"))
                    {
                        foundGenerator = true;
                        generatorManager = manager;
                        generatorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockManager = manager;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeManager = manager;
                        steelPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearManager = manager;
                        steelGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }

        if (foundGenerator == false || foundSteelPipe == false || foundCopperWire == false || foundSteelGear == false || foundSteelPlate == false || foundGlassBlock == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Reactor Turbine", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        generatorManager.inventory[generatorSlotNumber].amountInSlot -= 1;
                        glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                        steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot -= 2;
                        steelGearManager.inventory[steelGearSlotNumber].amountInSlot -= 2;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                        if (generatorManager.inventory[generatorSlotNumber].amountInSlot <= 0)
                        {
                            generatorManager.inventory[generatorSlotNumber].typeInSlot = "nothing";
                        }
                        if (glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                        {
                            glassBlockManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelGearManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                        {
                            steelGearManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Reactor Turbine"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftAutoCrafter()
    {
        InventoryManager bronzeGearManager = null;
        InventoryManager steelPlateManager = null;
        InventoryManager electricMotorManager = null;
        InventoryManager circuitBoardManager = null;
        InventoryManager darkMatterManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearManager = manager;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorManager = manager;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardManager = manager;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundBronzeGear == false || foundSteelPlate == false || foundCircuitBoard == false || foundElectricMotor == false || foundDarkMatter == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                Debug.Log("crafting attempt");
                if (itemAdded == false)
                {
                    manager.AddItem("Auto Crafter", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot -= 4;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 4;
                        circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                        electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 4;
                        if (bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                        {
                            bronzeGearManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                        {
                            circuitBoardManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                        {
                            electricMotorManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Auto Crafter"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftRailCartHub()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager circuitBoardManager = null;
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCircuitBoard = false;
        int circuitBoardSlotNumber = 0;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 6)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardManager = manager;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundCircuitBoard == false)
        {
            missingItem = true;
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Rail Cart Hub", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 6;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 10;
                        circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot -= 1;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                        {
                            circuitBoardManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Rail Cart Hub"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftRailCart()
    {
        InventoryManager electricMotorManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager tinPlateManager = null; 
        InventoryManager aluminumGearManager = null;
        InventoryManager storageContainerManager = null;
        InventoryManager solarPanelManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorManager = manager;
                        electricMotorSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateManager = manager;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 8)
                {
                    if (slot.typeInSlot.Equals("Aluminum Gear"))
                    {
                        foundAluminumGear = true;
                        aluminumGearManager = manager;
                        aluminumGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Storage Container"))
                    {
                        foundStorageContainer = true;
                        storageContainerManager = manager;
                        storageContainerSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Solar Panel"))
                    {
                        foundSolarPanel = true;
                        solarPanelManager = manager;
                        solarPanelSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundSolarPanel == false || foundElectricMotor == false || foundCopperWire == false || foundTinPlate == false || foundAluminumGear == false || foundStorageContainer == false)
        {
            missingItem = true;
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Rail Cart", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot -= 2;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        tinPlateManager.inventory[tinPlateSlotNumber].amountInSlot -= 4;
                        aluminumGearManager.inventory[aluminumGearSlotNumber].amountInSlot -= 8;
                        storageContainerManager.inventory[storageContainerSlotNumber].amountInSlot -= 1;
                        solarPanelManager.inventory[solarPanelSlotNumber].amountInSlot -= 1;
                        if (electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                        {
                            electricMotorManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinPlateManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                        {
                            tinPlateManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumGearManager.inventory[aluminumGearSlotNumber].amountInSlot <= 0)
                        {
                            aluminumGearManager.inventory[aluminumGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (storageContainerManager.inventory[storageContainerSlotNumber].amountInSlot <= 0)
                        {
                            storageContainerManager.inventory[storageContainerSlotNumber].typeInSlot = "nothing";
                        }
                        if (solarPanelManager.inventory[solarPanelSlotNumber].amountInSlot <= 0)
                        {
                            solarPanelManager.inventory[solarPanelSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Rail Cart"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftSolarPanel()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager copperPlateManager = null;
        InventoryManager glassBlockManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPlateManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPipeManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Plate"))
                    {
                        foundCopperPlate = true;
                        copperPlateManager = manager;
                        copperPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassBlockManager = manager;
                        glassSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundGlass == false || foundIronPlate == false || foundIronPipe == false || foundCopperWire == false || foundCopperPlate == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Solar Panel", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 4;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 4;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                        copperPlateManager.inventory[copperPlateSlotNumber].amountInSlot -= 4;
                        glassBlockManager.inventory[glassSlotNumber].amountInSlot -= 4;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperPlateManager.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                        {
                            copperPlateManager.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Solar Panel"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftPowerConduit()
    {
        InventoryManager aluminumPlateManager = null;
        InventoryManager glassBlockManager = null;
        InventoryManager copperWireManager = null;
        bool foundAluminumPlate = false;
        bool foundGlassBlock = false;
        bool foundCopperWire = false;
        int aluminumPlateSlotNumber = 0;
        int glassBlockSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Aluminum Plate"))
                    {
                        foundAluminumPlate = true;
                        aluminumPlateManager = manager;
                        aluminumPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassBlockManager = manager;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundGlassBlock == false || foundCopperWire == false || foundAluminumPlate == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Power Conduit", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        aluminumPlateManager.inventory[aluminumPlateSlotNumber].amountInSlot -= 4;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 4;
                        glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot -= 4;
                        if (aluminumPlateManager.inventory[aluminumPlateSlotNumber].amountInSlot <= 0)
                        {
                            aluminumPlateManager.inventory[aluminumPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (glassBlockManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                        {
                            glassBlockManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Power Conduit"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftNuclearReactor()
    {
        InventoryManager steelPlateManager = null;
        InventoryManager steelPipeManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager copperPlateManager = null;
        InventoryManager darkMatterManager = null;
        InventoryManager glassManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeManager = manager;
                        steelPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Plate"))
                    {
                        foundCopperPlate = true;
                        copperPlateManager = manager;
                        copperPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlass = true;
                        glassManager = manager;
                        glassSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundDarkMatter == false || foundGlass == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundCopperPlate == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Nuclear Reactor", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 10;
                        steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot -= 10;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        copperPlateManager.inventory[copperPlateSlotNumber].amountInSlot -= 10;
                        glassManager.inventory[glassSlotNumber].amountInSlot -= 10;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 10;
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperPlateManager.inventory[copperPlateSlotNumber].amountInSlot <= 0)
                        {
                            copperPlateManager.inventory[copperPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (glassManager.inventory[glassSlotNumber].amountInSlot <= 0)
                        {
                            glassManager.inventory[glassSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Nuclear Reactor"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftHeatExchanger()
    {
        InventoryManager steelPlateManager = null;
        InventoryManager steelPipeManager = null;
        bool foundPlates = false;
        bool foundPipe = false;
        int plateSlotNumber = 0;
        int pipeSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundPlates = true;
                        steelPlateManager = manager;
                        plateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundPipe = true;
                        steelPipeManager = manager;
                        pipeSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundPlates == false || foundPipe == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Heat Exchanger", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateManager.inventory[plateSlotNumber].amountInSlot -= 10;
                        steelPipeManager.inventory[pipeSlotNumber].amountInSlot -= 10;
                        if (steelPlateManager.inventory[plateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[plateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[pipeSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[pipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Heat Exchanger"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftSmelter()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager copperWireManager = null;
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundCopperWire = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundCopperWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Smelter", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 10;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Smelter"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftGearCutter()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager tinPlateManager = null;
        InventoryManager aluminumWireManager = null;
        InventoryManager copperWireManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateManager = manager;
                        tinPlateslotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireManager = manager;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundTinPlate == false || foundCopperWire == false || foundAluminumWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Gear Cutter", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 5;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 5;
                        tinPlateManager.inventory[tinPlateslotNumber].amountInSlot -= 5;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinPlateManager.inventory[tinPlateslotNumber].amountInSlot <= 0)
                        {
                            tinPlateManager.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                        {
                            aluminumWireManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Gear Cutter"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftCircuitBoard()
    {
        InventoryManager tinManager = null;
        InventoryManager glassManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager darkMatterManager = null;
        bool foundTinPlate = false;
        bool foundGlassBlock = false;
        bool foundCopperWire = false;
        bool foundDarkMatter = false;
        int tinPlateSlotNumber = 0;
        int glassBlockSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int darkMatterSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinManager = manager;
                        tinPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Glass Block"))
                    {
                        foundGlassBlock = true;
                        glassManager = manager;
                        glassBlockSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundTinPlate == false || foundGlassBlock == false || foundCopperWire == false || foundDarkMatter == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Circuit Board", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        tinManager.inventory[tinPlateSlotNumber].amountInSlot -= 1;
                        glassManager.inventory[glassBlockSlotNumber].amountInSlot -= 1;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 2;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 1;
                        if (tinManager.inventory[tinPlateSlotNumber].amountInSlot <= 0)
                        {
                            tinManager.inventory[tinPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (glassManager.inventory[glassBlockSlotNumber].amountInSlot <= 0)
                        {
                            glassManager.inventory[glassBlockSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Circuit Board"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftMotor()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager ironGearManager = null;
        InventoryManager copperWireManager = null;
        bool foundIronPlate = false;
        bool foundIronPipe = false;
        bool foundIronGear = false;
        bool foundCopperWire = false;
        int ironPlateSlotNumber = 0;
        int ironPipeSlotNumber = 0;
        int ironGearSlotNumber = 0;
        int copperWireSlotNumber = 0;
        int currentSlot = 0;
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 1)
                {
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 2)
                {
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearManager = manager;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronPlate == false || foundIronPipe == false || foundIronGear == false || foundCopperWire == false)
        {
            missingItem = true;
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Electric Motor", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 2;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 1;
                        ironGearManager.inventory[ironGearSlotNumber].amountInSlot -= 2;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironGearManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                        {
                            ironGearManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Electric Motor"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftAlloySmelter()
    {
        InventoryManager ironPlateManager = null;
        InventoryManager ironPipeManager = null;
        InventoryManager tinPlateManager = null;
        InventoryManager aluminumWireManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager ironGearManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 20)
                {
                    if (slot.typeInSlot.Equals("Iron Plate"))
                    {
                        foundIronPlate = true;
                        ironPlateManager = manager;
                        ironPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Plate"))
                    {
                        foundTinPlate = true;
                        tinPlateManager = manager;
                        tinPlateslotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Pipe"))
                    {
                        foundIronPipe = true;
                        ironPipeManager = manager;
                        ironPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Iron Gear"))
                    {
                        foundIronGear = true;
                        ironGearManager = manager;
                        ironGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 40)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireManager = manager;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundIronGear == false || foundIronPlate == false || foundIronPipe == false || foundTinPlate == false || foundCopperWire == false || foundAluminumWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Alloy Smelter", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot -= 20;
                        ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot -= 20;
                        tinPlateManager.inventory[tinPlateslotNumber].amountInSlot -= 20;
                        ironGearManager.inventory[ironGearSlotNumber].amountInSlot -= 20;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 40;
                        aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot -= 40;
                        if (ironPlateManager.inventory[ironPlateSlotNumber].amountInSlot <= 0)
                        {
                            ironPlateManager.inventory[ironPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (ironPipeManager.inventory[ironPipeSlotNumber].amountInSlot <= 0)
                        {
                            ironPipeManager.inventory[ironPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinPlateManager.inventory[tinPlateslotNumber].amountInSlot <= 0)
                        {
                            tinPlateManager.inventory[tinPlateslotNumber].typeInSlot = "nothing";
                        }
                        if (ironGearManager.inventory[ironGearSlotNumber].amountInSlot <= 0)
                        {
                            ironGearManager.inventory[ironGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                        {
                            aluminumWireManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Alloy Smelter"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftTurret()
    {
        InventoryManager steelPlateManager = null;
        InventoryManager steelPipeManager = null;
        InventoryManager bronzePlateManager = null;
        InventoryManager aluminumWireManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager steelGearManager = null;
        InventoryManager electricMotorManager = null;
        InventoryManager circuitBoardManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 5)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeManager = manager;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Plate"))
                    {
                        foundBronzePlate = true;
                        bronzePlateManager = manager;
                        bronzePlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearManager = manager;
                        steelGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 10)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireManager = manager;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 4)
                {
                    if (slot.typeInSlot.Equals("Electric Motor"))
                    {
                        foundElectricMotor = true;
                        electricMotorManager = manager;
                        electricMotorSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Circuit Board"))
                    {
                        foundCircuitBoard = true;
                        circuitBoardManager = manager;
                        circuitBoardSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundElectricMotor == false || foundCircuitBoard == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundBronzePlate == false || foundCopperWire == false || foundAluminumWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Turret", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 5;
                        steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot -= 5;
                        bronzePlateManager.inventory[bronzePlateSlotNumber].amountInSlot -= 5;
                        steelGearManager.inventory[steelGearSlotNumber].amountInSlot -= 5;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 10;
                        aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot -= 10;
                        electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot -= 4;
                        circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot -= 4;
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (bronzePlateManager.inventory[bronzePlateSlotNumber].amountInSlot <= 0)
                        {
                            bronzePlateManager.inventory[bronzePlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelGearManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                        {
                            steelGearManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                        {
                            aluminumWireManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (electricMotorManager.inventory[electricMotorSlotNumber].amountInSlot <= 0)
                        {
                            electricMotorManager.inventory[electricMotorSlotNumber].typeInSlot = "nothing";
                        }
                        if (circuitBoardManager.inventory[circuitBoardSlotNumber].amountInSlot <= 0)
                        {
                            circuitBoardManager.inventory[circuitBoardSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Turret"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftDarkMatterCollector()
    {
        InventoryManager steelPlateManager = null;
        InventoryManager steelPipeManager = null;
        InventoryManager tinGearManager = null;
        InventoryManager aluminumWireManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager steelGearManager = null;
        InventoryManager bronzeGearManager = null;
        InventoryManager darkMatterManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 50)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeManager = manager;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearManager = manager;
                        tinGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearManager = manager;
                        steelGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearManager = manager;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 100)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireManager = manager;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundDarkMatter == false || foundTinGear == false || foundBronzeGear == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundAluminumWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Dark Matter Collector", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 50;
                        steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot -= 50;
                        tinGearManager.inventory[tinGearSlotNumber].amountInSlot -= 50;
                        steelGearManager.inventory[steelGearSlotNumber].amountInSlot -= 50;
                        bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot -= 50;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 100;
                        aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot -= 100;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 100;
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinGearManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                        {
                            tinGearManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelGearManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                        {
                            steelGearManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                        {
                            bronzeGearManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                        {
                            aluminumWireManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Dark Matter Collector"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }

    public void CraftDarkMatterConduit()
    {
        InventoryManager steelPlateManager = null;
        InventoryManager steelPipeManager = null;
        InventoryManager tinGearManager = null;
        InventoryManager aluminumWireManager = null;
        InventoryManager copperWireManager = null;
        InventoryManager steelGearManager = null;
        InventoryManager bronzeGearManager = null;
        InventoryManager darkMatterManager = null;
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
        foreach (InventoryManager manager in computerManager)
        {
            currentSlot = 0;
            foreach (InventorySlot slot in manager.inventory)
            {
                if (slot.amountInSlot >= 25)
                {
                    if (slot.typeInSlot.Equals("Steel Plate"))
                    {
                        foundSteelPlate = true;
                        steelPlateManager = manager;
                        steelPlateSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Pipe"))
                    {
                        foundSteelPipe = true;
                        steelPipeManager = manager;
                        steelPipeSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Tin Gear"))
                    {
                        foundTinGear = true;
                        tinGearManager = manager;
                        tinGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Steel Gear"))
                    {
                        foundSteelGear = true;
                        steelGearManager = manager;
                        steelGearSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Bronze Gear"))
                    {
                        foundBronzeGear = true;
                        bronzeGearManager = manager;
                        bronzeGearSlotNumber = currentSlot;
                    }
                }
                if (slot.amountInSlot >= 50)
                {
                    if (slot.typeInSlot.Equals("Aluminum Wire"))
                    {
                        foundAluminumWire = true;
                        aluminumWireManager = manager;
                        aluminumWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Copper Wire"))
                    {
                        foundCopperWire = true;
                        copperWireManager = manager;
                        copperWireSlotNumber = currentSlot;
                    }
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        foundDarkMatter = true;
                        darkMatterManager = manager;
                        darkMatterSlotNumber = currentSlot;
                    }
                }
                currentSlot++;
            }
        }
        if (foundDarkMatter == false || foundTinGear == false || foundBronzeGear == false || foundSteelGear == false || foundSteelPlate == false || foundSteelPipe == false || foundCopperWire == false || foundAluminumWire == false)
        {
            missingItem = true;
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
        else
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in computerManager)
            {
                if (itemAdded == false)
                {
                    manager.AddItem("Dark Matter Conduit", 1);
                    if (manager.itemAdded == true)
                    {
                        itemAdded = true;
                        steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot -= 25;
                        steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot -= 25;
                        tinGearManager.inventory[tinGearSlotNumber].amountInSlot -= 25;
                        steelGearManager.inventory[steelGearSlotNumber].amountInSlot -= 25;
                        bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot -= 25;
                        copperWireManager.inventory[copperWireSlotNumber].amountInSlot -= 50;
                        aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot -= 50;
                        darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot -= 50;
                        if (steelPlateManager.inventory[steelPlateSlotNumber].amountInSlot <= 0)
                        {
                            steelPlateManager.inventory[steelPlateSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelPipeManager.inventory[steelPipeSlotNumber].amountInSlot <= 0)
                        {
                            steelPipeManager.inventory[steelPipeSlotNumber].typeInSlot = "nothing";
                        }
                        if (tinGearManager.inventory[tinGearSlotNumber].amountInSlot <= 0)
                        {
                            tinGearManager.inventory[tinGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (steelGearManager.inventory[steelGearSlotNumber].amountInSlot <= 0)
                        {
                            steelGearManager.inventory[steelGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (bronzeGearManager.inventory[bronzeGearSlotNumber].amountInSlot <= 0)
                        {
                            bronzeGearManager.inventory[bronzeGearSlotNumber].typeInSlot = "nothing";
                        }
                        if (copperWireManager.inventory[copperWireSlotNumber].amountInSlot <= 0)
                        {
                            copperWireManager.inventory[copperWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (aluminumWireManager.inventory[aluminumWireSlotNumber].amountInSlot <= 0)
                        {
                            aluminumWireManager.inventory[aluminumWireSlotNumber].typeInSlot = "nothing";
                        }
                        if (darkMatterManager.inventory[darkMatterSlotNumber].amountInSlot <= 0)
                        {
                            darkMatterManager.inventory[darkMatterSlotNumber].typeInSlot = "nothing";
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary["Dark Matter Conduit"];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
    }
}
