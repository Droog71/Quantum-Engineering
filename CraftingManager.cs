using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private PlayerController playerController;
    public InventoryManager inventoryManager;
    public InventoryManager[] storageComputerInventoryManager;
    public ConduitItem conduitItem;
    public bool missingItem;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    //! Used to disable the conduit item if this block is destroyed.
    public void OnDestroy()
    {
        if (conduitItem != null)
        {
            conduitItem.active = false;
        }
    }

    //! Called when the player crafts an item from the inventory GUI.
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
                playerController.PlayCraftingSound();
            }
        }
        else
        {
            playerController.PlayMissingItemsSound();
        }
    }

    //! Called by the auto crafter when it is connected to a storage container.
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

    //! Called by the auto crafter when it is connected to a storage computer.
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
                                conduitItem.billboard.GetComponent<Renderer>().material.mainTexture = conduitItem.textureDictionary[recipe.output];
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
