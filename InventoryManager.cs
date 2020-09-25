using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventory;
    private StateManager stateManager;
    public string ID = "unassigned";
    public int address;
    private string originalID;
    public bool initialized;
    private float updateTick;
    public int maxStackSize = 1000;
    public bool itemAdded;

    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (ID != "unassigned" && initialized == false)
            {
                inventory = new InventorySlot[16];
                int count = 0;
                while (count <= 15)
                {
                    inventory[count] = gameObject.AddComponent<InventorySlot>();
                    string countType = FileBasedPrefs.GetString(stateManager.WorldName + "inventory" + ID + "slot" + count + "type");
                    if (!countType.Equals(""))
                    {
                        inventory[count].typeInSlot = FileBasedPrefs.GetString(stateManager.WorldName + "inventory" + ID + "slot" + count + "type");
                        inventory[count].amountInSlot = FileBasedPrefs.GetInt(stateManager.WorldName + "inventory" + ID + "slot" + count + "amount");
                    }
                    count++;
                }
                originalID = ID;
                initialized = true;
                maxStackSize = ID.Equals("Rocket") ? 100000 : 1000;
            }

            updateTick += 1 * Time.deltaTime;
            if (updateTick > 0.5f + (address * 0.001f))
            {
                if (IsStorageContainer())
                {
                    GetComponent<PhysicsHandler>().UpdatePhysics();
                }
                updateTick = 0;
            }
        }
    }

    private bool IsStorageContainer()
    {
        return GetComponent<RailCart>() == null
        && GetComponent<Retriever>() == null
        && GetComponent<AutoCrafter>() == null
        && GetComponent<PlayerController>() == null
        && GetComponent<Rocket>() == null
        && ID != "Lander";
    }

    //! Saves the inventory's contents to disk.
    public void SaveData()
    {
        if (initialized == true)
        {
            if (ID != originalID)
            {
                int originalCount = 0;
                while (originalCount <= 15)
                {
                    FileBasedPrefs.SetString(stateManager.WorldName + "inventory" + originalID + "slot" + originalCount + "type", "nothing");
                    FileBasedPrefs.SetInt(stateManager.WorldName + "inventory" + originalID + "slot" + originalCount + "amount", 0);
                    originalCount++;
                }
                originalID = ID;
            }
            int count = 0;
            while (count <= 15)
            {
                FileBasedPrefs.SetString(stateManager.WorldName + "inventory" + ID + "slot" + count + "type", inventory[count].typeInSlot);
                FileBasedPrefs.SetInt(stateManager.WorldName + "inventory" + ID + "slot" + count + "amount", inventory[count].amountInSlot);
                count++;
            }
        }
    }

    //! Adds an item to the inventory.
    public void AddItem(string type, int amount)
    {
        itemAdded = false;
        foreach (InventorySlot slot in inventory)
        {
            if (slot != null && itemAdded == false)
            {
                if (slot.typeInSlot == "nothing" || slot.typeInSlot == type || slot.typeInSlot == "")
                {
                    if (slot.amountInSlot <= maxStackSize - amount)
                    {
                        slot.typeInSlot = type;
                        slot.amountInSlot += amount;
                        itemAdded = true;
                    }
                }
            }
        }
    }

    // Adds an item to a specific inventory slot.
    public void AddItemToSlot(string type, int amount, int slot)
    {
        inventory[slot].typeInSlot = type;
        inventory[slot].amountInSlot += amount;
    }
}
