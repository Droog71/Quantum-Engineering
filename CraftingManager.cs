using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    private PlayerController playerController;
    public InventoryManager inventoryManager;
    public InventoryManager[] storageComputerInventoryManager;
    public ConduitItem conduitItem;
    public bool missingItem;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void OnDestroy()
    {
        if (conduitItem != null)
        {
            conduitItem.active = false;
        }
    }

    public void CraftItemAsPlayer(CraftingRecipe recipe)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (int i = 0; i < recipe.amounts.Length; i++)
            {
                recipe.amounts[i] *= 10;
            }
            recipe.outputAmount *= 10;
        }

        InventorySlot[] slots = new InventorySlot[recipe.ingredients.Length];
        bool[] found = new bool[recipe.ingredients.Length];

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            int currentSlot = 0;
            foreach (InventorySlot slot in inventoryManager.inventory)
            {
                if (slot.amountInSlot >= recipe.amounts[i])
                {
                    if (slot.typeInSlot.Equals(recipe.ingredients[i]))
                    {
                        found[i] = true;
                        slots[i] = slot;
                    }
                }
                currentSlot++;
            }
        }

        bool itemNotFound = false;
        foreach (bool b in found)
        {
            if (!itemNotFound)
            {
                itemNotFound = !b;
            }
        }
        missingItem = itemNotFound;

        if (!missingItem)
        {
            inventoryManager.AddItem(recipe.output, recipe.outputAmount);
            if (inventoryManager.itemAdded)
            {
                for (int i = 0; i < recipe.ingredients.Length; i++)
                {
                    slots[i].amountInSlot -= recipe.amounts[i];
                    if (slots[i].amountInSlot <= 0)
                    {
                        slots[i].typeInSlot = "nothing";
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

    public void CraftItemUsingStorageContainer(CraftingRecipe recipe)
    {
        InventorySlot[] slots = new InventorySlot[recipe.ingredients.Length];
        bool[] found = new bool[recipe.ingredients.Length];

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            int currentSlot = 0;
            foreach (InventorySlot slot in inventoryManager.inventory)
            {
                if (slot.amountInSlot >= recipe.amounts[i])
                {
                    if (slot.typeInSlot.Equals(recipe.ingredients[i]))
                    {
                        found[i] = true;
                        slots[i] = slot;
                    }
                }
                currentSlot++;
            }
        }

        bool itemNotFound = false;
        foreach (bool b in found)
        {
            if (!itemNotFound)
            {
                itemNotFound = !b;
            }
        }
        missingItem = itemNotFound;

        if (!missingItem)
        {
            inventoryManager.AddItem(recipe.output, recipe.outputAmount);
            if (inventoryManager.itemAdded)
            {
                for (int i = 0; i < recipe.ingredients.Length; i++)
                {
                    slots[i].amountInSlot -= recipe.amounts[i];
                    if (slots[i].amountInSlot <= 0)
                    {
                        slots[i].typeInSlot = "nothing";
                    }
                }
            }
        }
    }

    public void CraftItemUsingStorageComputer(CraftingRecipe recipe)
    {
        InventorySlot[] slots = new InventorySlot[recipe.ingredients.Length];
        bool[] found = new bool[recipe.ingredients.Length];

        foreach (InventoryManager manager in storageComputerInventoryManager)
        {
            for (int i = 0; i < recipe.ingredients.Length; i++)
            {
                int currentSlot = 0;
                foreach (InventorySlot slot in manager.inventory)
                {
                    if (slot.amountInSlot >= recipe.amounts[i])
                    {
                        if (slot.typeInSlot.Equals(recipe.ingredients[i]))
                        {
                            found[i] = true;
                            slots[i] = slot;
                        }
                    }
                    currentSlot++;
                }
            }
        }

        bool itemNotFound = false;
        foreach (bool b in found)
        {
            if (!itemNotFound)
            {
                itemNotFound = !b;
            }
        }
        missingItem = itemNotFound;

        if (!missingItem)
        {
            bool itemAdded = false;
            foreach (InventoryManager manager in storageComputerInventoryManager)
            {
                if (!itemAdded)
                {
                    manager.AddItem(recipe.output, recipe.outputAmount);
                    if (manager.itemAdded)
                    {
                        itemAdded = true;
                        for (int i = 0; i < recipe.ingredients.Length; i++)
                        {
                            slots[i].amountInSlot -= recipe.amounts[i];
                            if (slots[i].amountInSlot <= 0)
                            {
                                slots[i].typeInSlot = "nothing";
                            }
                        }
                        if (conduitItem != null)
                        {
                            conduitItem.target = manager.gameObject;
                            if (conduitItem.textureDictionary != null)
                            {
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary.dictionary[recipe.output];
                            }
                            conduitItem.active = true;
                        }
                    }
                }
            }
        }
        else
        {
            if (conduitItem != null)
            {
                conduitItem.active = false;
            }
        }
    }
}
