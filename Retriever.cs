using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Retriever : Machine
{
    public float amount;
    public int speed = 1;
    public int power;
    public int heat;
    public int cooling;
    public bool powerON;
    public List<string> type;
    public string ID = "unassigned";
    public string currentType;
    public string inputID;
    public string outputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    private LineRenderer connectionLine;
    private LineRenderer inputLine;
    public int address;
    public bool hasHeatExchanger;
    private bool retrievingIce;
    private int warmup;
    public GameObject connectionObject;
    private GameObject spawnedConnection;
    public int connectionAttempts;
    public bool connectionFailed;
    public int multipleItemIteration;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
    private InventoryManager storageComputerInventoryManager;
    public PowerReceiver powerReceiver;
    private GameObject builtObjects;
    private StateManager stateManager;
    private int findRailCartsInterval;

    //! Called by unity engine on start up to initialize variables.
    void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        connectionLine = gameObject.AddComponent<LineRenderer>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        stateManager = FindObjectOfType<StateManager>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.Busy())
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
        UpdatePowerReceiver();
        
        if (warmup < 10)
        {
            warmup++;
        }
        else if (speed > power)
        {
            speed = power > 0 ? power : 1;
        }
        if (speed > 1 && type.Count > 0 && retrievingIce == false)
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
        if (inputObject == null || outputObject == null)
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
                if (inputObject == null)
                {
                    FindInputObject();
                }
                if (outputObject == null)
                {
                    FindOutputObject();
                }
            }
        }
        if (powerON == true && connectionFailed == false && speed > 0)
        {
            RetrieveItems();
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }
        if (connectionFailed == true)
        {
            if (creationMethod.Equals("spawned"))
            {
                creationMethod = "built";
            }
        }
        if (inputObject != null && outputObject != null)
        {
            connectionAttempts = 0;
        }
        connectionLine.enabled &= outputObject != null;
    }

    //! Used to remove line renderers and conduit item sprites when the machine is destroyed.
    void OnDestroy()
    {
        if (spawnedConnection != null)
        {
            spawnedConnection.SetActive(false);
        }
        if (storageComputerConduitItem != null)
        {
            storageComputerConduitItem.active = false;
        }
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }

    //! The object is a potential output connection.
    private bool IsValidOutputConduit(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj.GetComponent<UniversalConduit>().inputObject == null;
    }

    //! Returns true if the object in question is a storage container.
    private bool IsStorageContainer(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.GetComponent<InventoryManager>() != null)
            {
                return obj != gameObject
                && obj.GetComponent<Retriever>() == null
                && obj.GetComponent<AutoCrafter>() == null
                && !obj.GetComponent<InventoryManager>().ID.Equals("player");
            }
        }
        return false;
    }

    //! Connects the retriever to an inventory manager for input and a conduit for output.
    private void FindInputObject()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
        List<GameObject> objList = allObjects.ToList();
        objList.Add(GameObject.Find("LanderCargo"));
        foreach (GameObject obj in objList)
        {
            if (obj != null)
            {
                if (inputObject == null && IsStorageContainer(obj))
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (creationMethod.Equals("spawned") && obj.GetComponent<InventoryManager>().ID.Equals(inputID))
                    {
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
                            spawnedConnection = Instantiate(connectionObject, obj.transform.position, obj.transform.rotation);
                            spawnedConnection.transform.parent = inputObject.transform;
                            spawnedConnection.SetActive(true);
                            inputLine = spawnedConnection.AddComponent<LineRenderer>();
                            inputLine.startWidth = 0.2f;
                            inputLine.endWidth = 0.2f;
                            inputLine.material = lineMat;
                            inputLine.SetPosition(0, transform.position);
                            inputLine.SetPosition(1, obj.transform.position);
                            inputLine.enabled = distance < 20;
                            creationMethod = "built";
                            break;
                        }
                    }
                    else if (creationMethod.Equals("built") && distance < 20)
                    {
                        inputObject = obj;
                        inputID = obj.GetComponent<InventoryManager>().ID;
                        spawnedConnection = Instantiate(connectionObject, obj.transform.position, obj.transform.rotation);
                        spawnedConnection.transform.parent = inputObject.transform;
                        spawnedConnection.SetActive(true);
                        inputLine = spawnedConnection.AddComponent<LineRenderer>();
                        inputLine.startWidth = 0.2f;
                        inputLine.endWidth = 0.2f;
                        inputLine.material = lineMat;
                        inputLine.SetPosition(0, transform.position);
                        inputLine.SetPosition(1, obj.transform.position);
                        break;
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
                            spawnedConnection = Instantiate(connectionObject, obj.transform.position, obj.transform.rotation);
                            spawnedConnection.transform.parent = inputObject.transform;
                            spawnedConnection.SetActive(true);
                            inputLine = spawnedConnection.AddComponent<LineRenderer>();
                            inputLine.startWidth = 0.2f;
                            inputLine.endWidth = 0.2f;
                            inputLine.material = lineMat;
                            inputLine.SetPosition(0, transform.position);
                            inputLine.SetPosition(1, obj.transform.position);
                            creationMethod = "built";
                            break;
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            inputObject = obj;
                            inputID = obj.GetComponent<StorageComputer>().ID;
                            spawnedConnection = Instantiate(connectionObject, obj.transform.position, obj.transform.rotation);
                            spawnedConnection.transform.parent = inputObject.transform;
                            spawnedConnection.SetActive(true);
                            inputLine = spawnedConnection.AddComponent<LineRenderer>();
                            inputLine.startWidth = 0.2f;
                            inputLine.endWidth = 0.2f;
                            inputLine.material = lineMat;
                            inputLine.SetPosition(0, transform.position);
                            inputLine.SetPosition(1, obj.transform.position);
                            break;
                        }
                    }
                }
            }
        }
    }

    //! Connects the retriever to an inventory manager for input and a conduit for output.
    private void FindOutputObject()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
        List<GameObject> objList = allObjects.ToList();
        objList.Add(GameObject.Find("LanderCargo"));
        foreach (GameObject obj in objList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<UniversalConduit>() != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (IsValidOutputConduit(obj) && distance < 20)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalConduit>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                            break;
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    //! Retrieves items.
    private void RetrieveItems()
    {
        if (inputObject != null)
        {
            if (inputObject.GetComponent<InventoryManager>() != null)
            {
                RetrieveFromStorageContainer();
            }
            else if (inputObject.GetComponent<StorageComputer>() != null)
            {
                RetrieveFromStorageComputer();
            }
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }
    }

    //! Retrieves items from storage containers.
    private void RetrieveFromStorageContainer()
    {
        bool linkedToRailCart = false;
        if (inputObject.GetComponent<RailCart>() != null)
        {
            inputID = inputObject.GetComponent<RailCart>().ID;
            linkedToRailCart = true;
        }
        else
        {
            inputID = inputObject.GetComponent<InventoryManager>().ID;
        }
        if (outputObject != null)
        {
            if (outputObject.GetComponent<UniversalConduit>() != null)
            {
                outputID = outputObject.GetComponent<UniversalConduit>().ID;
                outputObject.GetComponent<UniversalConduit>().inputID = ID;
                outputObject.GetComponent<UniversalConduit>().speed = speed;
                if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                {
                    type.Clear();
                    int count = 0;
                    retrievingIce = false;
                    foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
                    {
                        if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("") && !type.Equals("nothing") && retrievingIce == false)
                        {
                            if (slot.typeInSlot.Equals("Ice"))
                            {
                                type.Clear();
                                type.Add(slot.typeInSlot);
                                count++;
                                retrievingIce = true;
                            }
                            else
                            {
                                type.Add(slot.typeInSlot);
                                count++;
                                retrievingIce = false;
                            }
                        }
                    }
                }
                foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
                {
                    if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("") && !type.Equals("nothing"))
                    {
                        type.Clear();
                    }
                }
                if (Vector3.Distance(transform.position, inputObject.transform.position) < 20)
                {
                    if (type.Count > 0)
                    {
                        inputLine.enabled = true;
                        inputLine.SetPosition(1, inputObject.transform.position);
                        bool foundItems = false;
                        int currentSlot = 0;
                        int slotToUse = 0;
                        foreach (InventorySlot slot in inputObject.GetComponent<InventoryManager>().inventory)
                        {
                            if (foundItems == false)
                            {
                                if (slot.typeInSlot.Equals(type[multipleItemIteration]))
                                {
                                    if (slot.amountInSlot >= speed)
                                    {
                                        foundItems = true;
                                        slotToUse = currentSlot;
                                    }
                                    else
                                    {
                                        outputObject.GetComponent<UniversalConduit>().speed = (int)outputObject.GetComponent<UniversalConduit>().amount;
                                    }
                                }
                                currentSlot++;
                            }
                        }
                        if (foundItems == true)
                        {
                            outputObject.GetComponent<UniversalConduit>().type = type[multipleItemIteration];
                            currentType = type[multipleItemIteration];
                            if (inputObject.GetComponent<InventoryManager>().inventory[slotToUse].amountInSlot >= speed)
                            {
                                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                                if (player.storageInventory != inputObject.GetComponent<InventoryManager>() || player.draggingItem == false)
                                {
                                    outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                    inputObject.GetComponent<InventoryManager>().inventory[slotToUse].amountInSlot -= speed - heat;
                                    if (inputObject.GetComponent<InventoryManager>().inventory[slotToUse].amountInSlot < 1)
                                    {
                                        inputObject.GetComponent<InventoryManager>().inventory[slotToUse].typeInSlot = "nothing";
                                    }
                                }
                                conduitItem.active = true;
                                GetComponent<Light>().enabled = true;
                            }
                        }
                        else
                        {
                            if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                            {
                                multipleItemIteration += 1;
                                if (multipleItemIteration >= type.Count)
                                {
                                    multipleItemIteration = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        conduitItem.active = false;
                        GetComponent<Light>().enabled = false;
                    }
                }
                else
                {
                    if (linkedToRailCart == true)
                    {
                        CheckForRailCart();
                    }
                    else
                    {
                        inputLine.enabled = false;
                        conduitItem.active = false;
                        GetComponent<Light>().enabled = false;
                    }
                }
            }
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }
    }

    //!Checks if there is a rail cart near the retreiver.
    private void CheckForRailCart()
    {
        findRailCartsInterval++;
        if (findRailCartsInterval == 5)
        {
            bool foundRailCart = false;
            List<RailCart> railCarts = FindObjectsOfType<RailCart>().ToList();
            foreach (RailCart cart in railCarts)
            {
                if (foundRailCart == false)
                {
                    Vector3 retPos = gameObject.transform.position;
                    Vector3 cartPos = cart.gameObject.transform.position;
                    float cartDistance = Vector3.Distance(retPos, cartPos);
                    if (cartDistance < 20)
                    {
                        inputObject = cart.gameObject;
                        inputID = cart.ID;
                        inputLine.enabled = true;
                        inputLine.SetPosition(1, inputObject.transform.position);
                        foundRailCart = true;
                    }
                }
            }
            if (foundRailCart == false)
            {
                inputLine.enabled = false;
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
            }
            findRailCartsInterval = 0;
        }
        else
        {
            inputLine.enabled = false;
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }
    }

    //!Retrieves items from storage computers.
    private void RetrieveFromStorageComputer()
    {
        inputID = inputObject.GetComponent<StorageComputer>().ID;
        if (outputObject != null && inputObject.GetComponent<StorageComputer>().initialized == true)
        {
            if (outputObject.GetComponent<UniversalConduit>() != null)
            {
                outputID = outputObject.GetComponent<UniversalConduit>().ID;
                outputObject.GetComponent<UniversalConduit>().inputID = ID;
                outputObject.GetComponent<UniversalConduit>().speed = speed;
                if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                {
                    type.Clear();
                    int count = 0;
                    retrievingIce = false;
                    foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
                    {
                        if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("") && !type.Equals("nothing") && retrievingIce == false)
                        {
                            if (slot.typeInSlot.Equals("Ice"))
                            {
                                type.Clear();
                                type.Add(slot.typeInSlot);
                                count++;
                                retrievingIce = true;
                            }
                            else
                            {
                                type.Add(slot.typeInSlot);
                                count++;
                                retrievingIce = false;
                            }
                        }
                    }
                }
                foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
                {
                    if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("") && !type.Equals("nothing"))
                    {
                        type.Clear();
                    }
                }
                if (type.Count > 0)
                {
                    if (Vector3.Distance(transform.position, inputObject.transform.position) < 20)
                    {
                        inputLine.enabled = true;
                        inputLine.SetPosition(1, inputObject.transform.position);
                        bool foundItems = false;
                        int currentSlot = 0;
                        int slotToUse = 0;
                        if (foundItems == false)
                        {
                            foreach (InventoryManager manager in inputObject.GetComponent<StorageComputer>().computerContainers)
                            {
                                currentSlot = 0;
                                foreach (InventorySlot slot in manager.inventory)
                                {
                                    if (slot.typeInSlot.Equals(type[multipleItemIteration]))
                                    {
                                        if (slot.amountInSlot >= speed)
                                        {
                                            foundItems = true;
                                            storageComputerInventoryManager = manager;
                                            slotToUse = currentSlot;
                                        }
                                        else
                                        {
                                            outputObject.GetComponent<UniversalConduit>().speed = (int)outputObject.GetComponent<UniversalConduit>().amount;
                                        }
                                    }
                                    currentSlot++;
                                }
                            }
                        }
                        if (foundItems == true)
                        {
                            outputObject.GetComponent<UniversalConduit>().type = type[multipleItemIteration];
                            currentType = type[multipleItemIteration];
                            if (storageComputerInventoryManager.inventory[slotToUse].amountInSlot >= speed)
                            {
                                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                                if (player.storageInventory != storageComputerInventoryManager || player.draggingItem == false)
                                {
                                    outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                    storageComputerInventoryManager.inventory[slotToUse].amountInSlot -= speed - heat;
                                    if (storageComputerInventoryManager.inventory[slotToUse].amountInSlot < 1)
                                    {
                                        storageComputerInventoryManager.inventory[slotToUse].typeInSlot = "nothing";
                                    }
                                }
                                if (storageComputerConduitItem == null)
                                {
                                    GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, outputObject.transform.position, outputObject.transform.rotation);
                                    storageComputerItemObject.transform.parent = outputObject.transform;
                                    storageComputerConduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
                                }
                                else
                                {
                                    storageComputerConduitItem.startPosition = storageComputerInventoryManager.gameObject.transform.position;
                                    storageComputerConduitItem.target = inputObject;
                                    if (storageComputerConduitItem.textureDictionary != null)
                                    {
                                        storageComputerConduitItem.billboard.GetComponent<Renderer>().material.mainTexture = storageComputerConduitItem.textureDictionary[type[multipleItemIteration]];
                                    }
                                    storageComputerConduitItem.active = true;
                                }
                                conduitItem.active = true;
                                GetComponent<Light>().enabled = true;
                            }
                        }
                        else if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                        {
                            multipleItemIteration += 1;
                            if (multipleItemIteration >= type.Count)
                            {
                                multipleItemIteration = 0;
                            }
                        }
                    }
                    else
                    {
                        inputLine.enabled = false;
                        if (storageComputerConduitItem != null)
                        {
                            storageComputerConduitItem.active = false;
                        }
                        conduitItem.active = false;
                        GetComponent<Light>().enabled = false;
                    }
                }
                else
                {
                    if (storageComputerConduitItem != null)
                    {
                        storageComputerConduitItem.active = false;
                    }
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                }
            }
        }
        else
        {
            if (storageComputerConduitItem != null)
            {
                storageComputerConduitItem.active = false;
            }
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
        }
    }
}