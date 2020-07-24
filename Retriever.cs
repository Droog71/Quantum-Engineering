using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Retriever : MonoBehaviour
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
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    LineRenderer connectionLine;
    LineRenderer inputLine;
    private float updateTick;
    public int address;
    public bool hasHeatExchanger;
    private bool retrievingIce;
    public GameObject connectionObject;
    private GameObject spawnedConnection;
    public int connectionAttempts;
    public bool connectionFailed;
    public int multipleItemIteration;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
    private InventoryManager storageComputerInventoryManager;
    private GameObject builtObjects;

    void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

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

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
            if (speed > 1 && retrievingIce == false)
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
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj != null)
                        {
                            if (obj.transform.parent != builtObjects.transform)
                            {
                                if (obj.activeInHierarchy)
                                {
                                    if (obj.GetComponent<InventoryManager>() != null && !obj.GetComponent<InventoryManager>().ID.Equals("player") && obj.GetComponent<Rocket>() == null && obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null && obj != this.gameObject)
                                    {
                                        if (inputObject == null)
                                        {
                                            if (creationMethod.Equals("spawned"))
                                            {
                                                //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + inputID);
                                                if (obj.GetComponent<InventoryManager>().ID.Equals(inputID))
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
                                                    }
                                                }
                                            }
                                            else if (creationMethod.Equals("built"))
                                            {
                                                float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                if (distance < 20)
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
                                                //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + inputID);
                                                if (obj.GetComponent<StorageComputer>().ID.Equals(inputID))
                                                {
                                                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                                                    if (distance < 20)
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
                                                    spawnedConnection = Instantiate(connectionObject, obj.transform.position, obj.transform.rotation);
                                                    spawnedConnection.transform.parent = inputObject.transform;
                                                    spawnedConnection.SetActive(true);
                                                    inputLine = spawnedConnection.AddComponent<LineRenderer>();
                                                    inputLine.startWidth = 0.2f;
                                                    inputLine.endWidth = 0.2f;
                                                    inputLine.material = lineMat;
                                                    inputLine.SetPosition(0, transform.position);
                                                    inputLine.SetPosition(1, obj.transform.position);
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<UniversalConduit>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        if (obj.GetComponent<UniversalConduit>().inputObject == null)
                                                        {
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<UniversalConduit>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<UniversalConduit>().ID.Equals(outputID))
                                                                {
                                                                    outputObject = obj;
                                                                    obj.GetComponent<UniversalConduit>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                outputObject = obj;
                                                                obj.GetComponent<UniversalConduit>().inputObject = this.gameObject;
                                                                connectionLine.SetPosition(0, transform.position);
                                                                connectionLine.SetPosition(1, obj.transform.position);
                                                                connectionLine.enabled = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (powerON == true && connectionFailed == false && speed > 0)
            {
                if (inputObject != null)
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
                                        if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("Dark Matter") && !type.Equals("") && !type.Equals("nothing") && retrievingIce == false)
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
                                    if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("Dark Matter") && !type.Equals("") && !type.Equals("nothing"))
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
                                        //Debug.Log(ID + " type list count greater than zero.");
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
                                            //Debug.Log(ID + " found items in container.");
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
                                                conduitItem.GetComponent<ConduitItem>().active = true;
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
                                            //Debug.Log(ID + " items not found in container.");
                                        }
                                    }
                                    else
                                    {
                                        //Debug.Log(ID + " type list less than 1.");
                                        conduitItem.GetComponent<ConduitItem>().active = false;
                                        GetComponent<Light>().enabled = false;
                                    }
                                }
                                else
                                {
                                    inputLine.enabled = false;
                                    conduitItem.GetComponent<ConduitItem>().active = false;
                                    GetComponent<Light>().enabled = false;
                                }
                            }
                        }
                        else
                        {
                            //Debug.Log(ID + " output object is null.");
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                        }
                    }
                    else if (inputObject.GetComponent<StorageComputer>() != null)
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
                                        if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("Dark Matter") && !type.Equals("") && !type.Equals("nothing") && retrievingIce == false)
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
                                    if (slot.amountInSlot > 0 && !type.Contains(slot.typeInSlot) && !type.Equals("Dark Matter") && !type.Equals("") && !type.Equals("nothing"))
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
                                        //Debug.Log(ID + " type list count greater than zero.");
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
                                                            //Debug.Log(ID + " found items in container.");
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
                                                        storageComputerConduitItem.billboard.GetComponent<Renderer>().material.mainTexture = storageComputerConduitItem.textureDictionary.dictionary[type[multipleItemIteration]];
                                                    }
                                                    storageComputerConduitItem.active = true;
                                                }
                                                conduitItem.GetComponent<ConduitItem>().active = true;
                                                GetComponent<Light>().enabled = true;
                                            }
                                        }
                                        else if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                                        {
                                            //Debug.Log(ID + " items not found in container.");
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
                                        conduitItem.GetComponent<ConduitItem>().active = false;
                                        GetComponent<Light>().enabled = false;
                                    }
                                }
                                else
                                {
                                    //Debug.Log(ID + " type list less than 1.");
                                    if (storageComputerConduitItem != null)
                                    {
                                        storageComputerConduitItem.active = false;
                                    }
                                    conduitItem.GetComponent<ConduitItem>().active = false;
                                    GetComponent<Light>().enabled = false;
                                }
                            }
                        }
                        else
                        {
                            //Debug.Log(ID + " output object is null.");
                            if (storageComputerConduitItem != null)
                            {
                                storageComputerConduitItem.active = false;
                            }
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                        }
                    }
                }
                else
                {
                    //Debug.Log(ID + " input object is null.");
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                }
            }
            else
            {
                //Debug.Log(ID + " power or connection failure.");
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
            }
            if (outputObject == null)
            {
                connectionLine.enabled = false;
            }
            if (connectionFailed ==  true)
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
        }
    }
}