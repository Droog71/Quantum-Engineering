using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    public int speed = 1;
    public int power;
    public int heat;
    public int cooling;
    public bool powerON;
    public string type;
    public string ID = "unassigned";
    public string inputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    private LineRenderer connectionLine;
    public PowerReceiver powerReceiver;
    private float updateTick;
    public int address;
    public bool hasHeatExchanger;
    private CraftingManager craftingManager;
    private CraftingDictionary craftingDictionary;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    private GameObject builtObjects;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        craftingManager = GetComponent<CraftingManager>();
        craftingDictionary = gameObject.AddComponent<CraftingDictionary>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    // Called once per frame by unity engine
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

            updateTick = 0;
            if (speed > power)
            {
                speed = power;
            }
            if (speed > 1)
            {
                heat = speed - 1 - cooling;
            }
            else
            {
                heat = 0;
            }
            if (heat < 0)
            {
                heat = 0;
            }

            GetComponent<InventoryManager>().ID = ID;
            bool foundType = false;
            foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
            {
                if (foundType == false)
                {
                    if (slot.amountInSlot > 0)
                    {
                        foundType = true;
                        type = slot.typeInSlot;
                    }
                }
            }

            if (inputObject == null)
            {
                connectionAttempts += 1;
                if (creationMethod.Equals("spawned"))
                {
                    if (connectionAttempts >= 30)
                    {
                        connectionAttempts = 0;
                        connectionFailed = true;
                    }
                }
                else
                {
                    if (connectionAttempts >= 120)
                    {
                        connectionAttempts = 0;
                        connectionFailed = true;
                    }
                }
                if (connectionFailed == false)
                {
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject obj in allObjects)
                    {
                        if (IsValidObject(obj))
                        {
                            ConnectToObject(obj);
                        }
                    }
                }
            }

            if (connectionFailed == false && inputObject != null)
            {
                DoWork();
            }
            else
            {
                ShutDown(true);
            }

            if (inputObject == null)
            {
                ShutDown(true);
                if (connectionFailed == true)
                {
                    if (creationMethod.Equals("spawned"))
                    {
                        creationMethod = "built";
                    }
                }
            }
        }
    }

    // Gets power values from power receiver
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
        if (powerReceiver.overClocked == true)
        {
            speed = powerReceiver.speed;
        }
        else
        {
            powerReceiver.speed = speed;
        }
    }

    // The object exists, is active and is not a standard building block
    bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    private bool IsStorageContainer(GameObject obj)
    {
        return obj != gameObject
        && obj.GetComponent<InventoryManager>() != null
        && !obj.GetComponent<InventoryManager>().ID.Equals("player")
        && obj.GetComponent<Retriever>() == null
        && obj.GetComponent<Rocket>() == null
        && obj.GetComponent<AutoCrafter>() == null;
    }

    // Connects the auto crafter to a storage inventory
    private void ConnectToObject(GameObject obj)
    {
        if (inputObject == null && IsStorageContainer(obj))
        {
            if (creationMethod.Equals("spawned") && obj.GetComponent<InventoryManager>().ID.Equals(inputID))
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < 20 || obj.GetComponent<RailCart>() != null)
                {
                    inputObject = obj;
                    if (obj.GetComponent<RailCart>() != null)
                    {
                        inputID = obj.GetComponent<RailCart>().ID;
                    }
                    else
                    {
                        inputID = obj.GetComponent<InventoryManager>().ID;
                    }
                    craftingManager.inventoryManager = obj.GetComponent<InventoryManager>();
                    connectionLine.SetPosition(0, transform.position);
                    connectionLine.SetPosition(1, obj.transform.position);
                    connectionLine.enabled = true;
                    creationMethod = "built";
                }
            }
            else if (creationMethod.Equals("built"))
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < 20)
                {
                    inputObject = obj;
                    inputID = obj.GetComponent<InventoryManager>().ID;
                    craftingManager.inventoryManager = obj.GetComponent<InventoryManager>();
                    connectionLine.SetPosition(0, transform.position);
                    connectionLine.SetPosition(1, obj.transform.position);
                    connectionLine.enabled = true;
                }
            }
        }
        if (obj.GetComponent<StorageComputer>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (inputObject == null && distance < 20)
            {
                if (creationMethod.Equals("spawned") && obj.GetComponent<StorageComputer>().ID.Equals(inputID))
                {
                    inputObject = obj;
                    inputID = obj.GetComponent<StorageComputer>().ID;
                    craftingManager.storageComputerInventoryManager = obj.GetComponent<StorageComputer>().computerContainers;
                    craftingManager.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                    connectionLine.SetPosition(0, transform.position);
                    connectionLine.SetPosition(1, obj.transform.position);
                    connectionLine.enabled = true;
                    creationMethod = "built";
                }
                else if (creationMethod.Equals("built"))
                {
                    inputObject = obj;
                    inputID = obj.GetComponent<StorageComputer>().ID;
                    craftingManager.storageComputerInventoryManager = obj.GetComponent<StorageComputer>().computerContainers;
                    craftingManager.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                    connectionLine.SetPosition(0, transform.position);
                    connectionLine.SetPosition(1, obj.transform.position);
                    connectionLine.enabled = true;
                }
            }
        }
    }

    // Calls the appropriate item crafting method in the crafting manager
    private void CraftItems(bool usingStorageComputer)
    {
        if (usingStorageComputer)
        {
            for (int count = 0; count < speed; count++)
            {
                craftingManager.CraftItemUsingStorageComputer(craftingDictionary.dictionary[type]);
            }
        }
        else 
        {
            for (int count = 0; count < speed; count++)
            {
                craftingManager.CraftItemUsingStorageContainer(craftingDictionary.dictionary[type]);
            }
        }
    }

    // Handles overall operation of the machine
    private void DoWork()
    {
        float distance = Vector3.Distance(transform.position, inputObject.transform.position);
        if (distance < 20)
        {
            if (powerON == true && speed > 0 && type != "" && type != "nothing")
            {
                if (inputObject.GetComponent<InventoryManager>() != null)
                {
                    if (inputObject.GetComponent<RailCart>() != null)
                    {
                        inputID = inputObject.GetComponent<RailCart>().ID;
                    }
                    else
                    {
                        inputID = inputObject.GetComponent<InventoryManager>().ID;
                    }

                    machineTimer += 1;
                    if (machineTimer > 5 - (address * 0.01f))
                    {
                        CraftItems(false);
                        machineTimer = 0;
                    }

                    if (craftingManager.missingItem == false)
                    {
                        Activate();
                    }
                    else
                    {
                        ShutDown(false);
                    }
                }
                if (inputObject.GetComponent<StorageComputer>() != null)
                {
                    inputID = inputObject.GetComponent<StorageComputer>().ID;
                    if (inputObject.GetComponent<StorageComputer>().initialized == true)
                    {
                        craftingManager.storageComputerInventoryManager = inputObject.GetComponent<StorageComputer>().computerContainers;
                        if (craftingManager.conduitItem == null)
                        {
                            GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, inputObject.transform.position, inputObject.transform.rotation);
                            storageComputerItemObject.transform.parent = inputObject.transform;
                            craftingManager.conduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
                        }

                        machineTimer += 1;
                        if (machineTimer > 5 - (address * 0.01f))
                        {
                            CraftItems(true);
                            machineTimer = 0;
                        }

                        if (craftingManager.missingItem == false)
                        {
                            Activate();
                        }
                        else
                        {
                            ShutDown(false);
                        }
                    }
                    else
                    {
                        ShutDown(false);
                    }
                }
            }
            else
            {
                ShutDown(false);
            }
        }
        else
        {
            ShutDown(true);
        }
    }

    // Called when all requirements are met for the machine to be running, activates effects
    private void Activate()
    {
        conduitItem.GetComponent<ConduitItem>().active = true;
        GetComponent<Light>().enabled = true;
        if (GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
        }
        connectionLine.enabled = true;
        connectionLine.SetPosition(1, inputObject.transform.position);
    }

    // Called when requirements are not met for the machine to be running, disables effects
    private void ShutDown(bool disconnect)
    {
        if (craftingManager.conduitItem != null)
        {
            craftingManager.conduitItem.active = false;
        }
        connectionLine.enabled &= !disconnect;
        conduitItem.GetComponent<ConduitItem>().active = false;
        GetComponent<Light>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }
}