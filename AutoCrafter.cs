using UnityEngine;
using System.Collections;

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
    public string creationMethod;
    public GameObject inputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    private LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public bool hasHeatExchanger;
    private MachineCrafting machineCrafting;
    private CraftingDictionary craftingDictionary;
    private ComputerCrafting computerCrafting;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    private GameObject builtObjects;

    void Start()
    {
        machineCrafting = GetComponent<MachineCrafting>();
        computerCrafting = GetComponent<ComputerCrafting>();
        craftingDictionary = new CraftingDictionary(machineCrafting, computerCrafting);
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
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
                connectionLine.enabled = false;
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

    bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    private void ConnectToObject(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null && !obj.GetComponent<InventoryManager>().ID.Equals("player") && obj.GetComponent<Retriever>() == null && obj.GetComponent<Rocket>() == null && obj.GetComponent<AutoCrafter>() == null && obj != gameObject)
        {
            if (inputObject == null)
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
                        machineCrafting.inventoryManager = obj.GetComponent<InventoryManager>();
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
                        machineCrafting.inventoryManager = obj.GetComponent<InventoryManager>();
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<StorageComputer>() != null)
        {
            if (inputObject == null)
            {
                if (creationMethod.Equals("spawned"))
                {
                    //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + outputID);
                    if (obj.GetComponent<StorageComputer>().ID.Equals(inputID))
                    {
                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                        if (distance < 20)
                        {
                            inputObject = obj;
                            inputID = obj.GetComponent<StorageComputer>().ID;
                            computerCrafting.computerManager = obj.GetComponent<StorageComputer>().computerContainers;
                            computerCrafting.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                    }
                }
                else if (creationMethod.Equals("built"))
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance < 20)
                    {
                        inputObject = obj;
                        inputID = obj.GetComponent<StorageComputer>().ID;
                        computerCrafting.computerManager = obj.GetComponent<StorageComputer>().computerContainers;
                        computerCrafting.conduitItem = inputObject.GetComponent<StorageComputer>().conduitItem.GetComponent<ConduitItem>();
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
    }

    private void MachineCraftItem()
    {
        for (int count = 0; count < speed; count++)
        {
            bool craft = craftingDictionary.machineCraftingDictionary[type];
        }
    }

    private void ComputerCraftItem()
    {
        for (int count = 0; count < speed; count++)
        {
            bool craft = craftingDictionary.computerCraftingDictionary[type];
        }
    }

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
                    conduitItem.GetComponent<ConduitItem>().active = true;
                    GetComponent<Light>().enabled = true;
                    if (GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().Play();
                    }
                    connectionLine.enabled = true;
                    connectionLine.SetPosition(1, inputObject.transform.position);
                    machineTimer += 1;
                    if (machineTimer > 5 - (address * 0.01f))
                    {
                        MachineCraftItem();
                        machineTimer = 0;
                    }
                }
                if (inputObject.GetComponent<StorageComputer>() != null)
                {
                    inputID = inputObject.GetComponent<StorageComputer>().ID;
                    if (inputObject.GetComponent<StorageComputer>().initialized == true)
                    {
                        computerCrafting.computerManager = inputObject.GetComponent<StorageComputer>().computerContainers;
                        if (computerCrafting.conduitItem == null)
                        {
                            GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, inputObject.transform.position, inputObject.transform.rotation);
                            storageComputerItemObject.transform.parent = inputObject.transform;
                            computerCrafting.conduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
                        }
                        conduitItem.GetComponent<ConduitItem>().active = true;
                        GetComponent<Light>().enabled = true;
                        if (GetComponent<AudioSource>().isPlaying == false)
                        {
                            GetComponent<AudioSource>().Play();
                        }
                        connectionLine.enabled = true;
                        connectionLine.SetPosition(1, inputObject.transform.position);
                        machineTimer += 1;
                        if (machineTimer > 5 - (address * 0.01f))
                        {
                            ComputerCraftItem();
                            machineTimer = 0;
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

    private void ShutDown(bool disconnect)
    {
        if (computerCrafting.conduitItem != null)
        {
            computerCrafting.conduitItem.active = false;
        }
        connectionLine.enabled &= !disconnect;
        conduitItem.GetComponent<ConduitItem>().active = false;
        GetComponent<Light>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }
}