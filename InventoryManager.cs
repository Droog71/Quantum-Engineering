using UnityEngine;

public class InventoryManager : Machine
{
    public InventorySlot[] inventory;
    private StateManager stateManager;
    public string ID = "unassigned";
    private string originalID;
    public bool initialized;
    public int maxStackSize = 1000;
    public bool itemAdded;

    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (!stateManager.Busy())
        {
            if (ID != "unassigned")
            {
                if (initialized == false)
                {
                    Initialize();
                }
                if (IsStorageContainer())
                {
                    GetComponent<PhysicsHandler>().UpdatePhysics();
                }
            }
        }
    }

    //! Loads saved inventory data.
    private void Initialize()
    {
        if (GetComponent<PlayerController>() != null && PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            LoadDataFromPrefs();
        }
        else
        {
            LoadDataFromFile();
        }
    }

    //! Loads data from .sav file.
    private void LoadDataFromFile()
    {
        inventory = new InventorySlot[16];
        int count = 0;
        while (count <= 15)
        {
            inventory[count] = gameObject.AddComponent<InventorySlot>();
            string countType = FileBasedPrefs.GetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type");
            if (!countType.Equals(""))
            {
                inventory[count].typeInSlot = FileBasedPrefs.GetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type");
                inventory[count].amountInSlot = FileBasedPrefs.GetInt(stateManager.worldName + "inventory" + ID + "slot" + count + "amount");
            }
            count++;
        }
        originalID = ID;
        initialized = true;
        maxStackSize = ID.Equals("Rocket") ? 100000 : 1000;
    }

    //! Loads data from prefs file.
    private void LoadDataFromPrefs()
    {
        inventory = new InventorySlot[16];
        int count = 0;
        while (count <= 15)
        {
            inventory[count] = gameObject.AddComponent<InventorySlot>();
            string countType = PlayerPrefs.GetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type");
            if (!countType.Equals(""))
            {
                inventory[count].typeInSlot = PlayerPrefs.GetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type");
                inventory[count].amountInSlot = PlayerPrefs.GetInt(stateManager.worldName + "inventory" + ID + "slot" + count + "amount");
            }
            count++;
        }
        originalID = ID;
        initialized = true;
        maxStackSize = ID.Equals("Rocket") ? 100000 : 1000;
    }

    //! Returns true if this object is a storage container.
    private bool IsStorageContainer()
    {
        return GetComponent<RailCart>() == null
        && GetComponent<Retriever>() == null
        && GetComponent<AutoCrafter>() == null
        && GetComponent<PlayerController>() == null
        && GetComponent<Rocket>() == null
        && ID != "Lander";
    }

    //! Saves inventory data.
    public void SaveData()
    {
        if (GetComponent<PlayerController>() != null && PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            SaveDataToPrefs();
        }
        else
        {
            SaveDataToFile();
        }
    }

    //! Saves the inventory's contents to .sav file.
    private void SaveDataToFile()
    {
        if (initialized == true)
        {
            if (ID != originalID)
            {
                int originalCount = 0;
                while (originalCount <= 15)
                {
                    FileBasedPrefs.SetString(stateManager.worldName + "inventory" + originalID + "slot" + originalCount + "type", "nothing");
                    FileBasedPrefs.SetInt(stateManager.worldName + "inventory" + originalID + "slot" + originalCount + "amount", 0);
                    originalCount++;
                }
                originalID = ID;
            }
            int count = 0;
            while (count <= 15)
            {
                FileBasedPrefs.SetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type", inventory[count].typeInSlot);
                FileBasedPrefs.SetInt(stateManager.worldName + "inventory" + ID + "slot" + count + "amount", inventory[count].amountInSlot);
                count++;
            }
        }
    }

    //! Saves the inventory's contents to prefs file.
    private void SaveDataToPrefs()
    {
        if (initialized == true)
        {
            if (ID != originalID)
            {
                int originalCount = 0;
                while (originalCount <= 15)
                {
                    PlayerPrefs.SetString(stateManager.worldName + "inventory" + originalID + "slot" + originalCount + "type", "nothing");
                    PlayerPrefs.SetInt(stateManager.worldName + "inventory" + originalID + "slot" + originalCount + "amount", 0);
                    originalCount++;
                }
                originalID = ID;
            }
            int count = 0;
            while (count <= 15)
            {
                PlayerPrefs.SetString(stateManager.worldName + "inventory" + ID + "slot" + count + "type", inventory[count].typeInSlot);
                PlayerPrefs.SetInt(stateManager.worldName + "inventory" + ID + "slot" + count + "amount", inventory[count].amountInSlot);
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
                        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && gameObject.tag != "Player")
                        {
                            slot.pendingNetworkUpdate = true;
                            slot.networkWaitTime = 0;
                        }
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
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && gameObject.tag != "Player")
        {
            inventory[slot].pendingNetworkUpdate = true;
            inventory[slot].networkWaitTime = 0;
        }
    }
}
