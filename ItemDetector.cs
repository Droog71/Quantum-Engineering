using UnityEngine;

public class ItemDetector : LogicBlock
{
    private InventoryManager inventoryManager;

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (logic == true)
        {
            GetComponent<Renderer>().material.mainTexture = onTexture;
        }
        else
        {
            GetComponent<Renderer>().material.mainTexture = offTexture;
        }

        if (inventoryManager == null)
        {
            InventoryManager[] inventoryManagers = FindObjectsOfType<InventoryManager>();
            foreach (InventoryManager i in inventoryManagers)
            {
                float distance = Vector3.Distance(transform.position, i.transform.position);
                if (distance <= 6 && IsStorageContainer(i.gameObject))
                {
                    inventoryManager = i;
                }
            }
        }
        else
        {
            bool empty = true;
            foreach (InventorySlot slot in inventoryManager.inventory)
            {
                if (slot.amountInSlot > 0)
                {
                    empty = false;
                    break;
                }
            }

            logic = !empty;
        }
    }

    //! Returns true if the object in question is a storage container.
    private bool IsStorageContainer(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null)
        {
            return !obj.GetComponent<InventoryManager>().ID.Equals("player") && obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null;
        }
        return false;
    }
}

