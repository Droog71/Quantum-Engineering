using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DarkMatterConduit : Machine
{
    public float darkMatterAmount;
    public int speed = 1;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    public ConduitItem conduitItem;
    public Material darkMatterMat;
    public Material lineMat;
    private LineRenderer connectionLine;
    public int address;
    public int range = 6;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
    private GameObject builtObjects;
    private StateManager stateManager;
    private bool linkedToRailCart;
    private int findRailCartsInterval;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
        stateManager = FindObjectOfType<StateManager>();
    }

    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.Busy())
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();

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
                    FindInputConnection();
                }
                if (outputObject == null)
                {
                    FindOutputConnection();
                }
            }
        }

        HandleIO();

        if (inputObject != null && outputObject != null)
        {
            connectionAttempts = 0;
        }
    }

    //! Used to remove conduit item from storage computer if connected to one.
    void OnDestroy()
    {
        if (storageComputerConduitItem != null)
        {
            storageComputerConduitItem.active = false;
        }
    }

    //! The object is a potential output connection.
    bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj != inputObject && obj != gameObject;
    }

    //! Returns true if the object is a storage container.
    private bool IsStorageContainer(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null)
        {
            return obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null && !obj.GetComponent<InventoryManager>().ID.Equals("player");
        }
        return false;
    }

    //! Finds an input connection.
    private void FindInputConnection()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
        List<GameObject> objList = allObjects.ToList();
        objList.Add(GameObject.Find("Rocket"));
        objList.Add(GameObject.Find("LanderCargo"));
        foreach (GameObject obj in objList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<DarkMatterCollector>() != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance < range)
                    {
                        if (inputObject == null && obj.GetComponent<DarkMatterCollector>().outputObject == null)
                        {
                            if (creationMethod.Equals("spawned"))
                            {
                                if (obj.GetComponent<DarkMatterCollector>().ID.Equals(inputID))
                                {
                                    inputObject = obj;
                                    obj.GetComponent<DarkMatterCollector>().outputObject = gameObject;
                                    creationMethod = "built";
                                    break;
                                }
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                inputObject = obj;
                                obj.GetComponent<DarkMatterCollector>().outputObject = gameObject;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    //! Finds an output connection.
    private void FindOutputConnection()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
        List<GameObject> objList = allObjects.ToList();
        objList.Add(GameObject.Find("Rocket"));
        objList.Add(GameObject.Find("LanderCargo"));
        foreach (GameObject obj in objList)
        {
            if (obj != null)
            {
                if (IsStorageContainer(obj))
                {
                    if (IsValidOutputObject(obj))
                    {
                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                        if (creationMethod.Equals("spawned") && obj.GetComponent<InventoryManager>().ID.Equals(outputID))
                        {
                            if (distance < range || obj.GetComponent<RailCart>() != null || obj.GetComponent<Rocket>() != null)
                            {
                                outputObject = obj;
                                float lineHeight = obj.GetComponent<Rocket>() != null ? obj.transform.position.y + 40 : 0;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position + obj.transform.up * lineHeight);
                                connectionLine.enabled = distance < range;
                                creationMethod = "built";
                                break;
                            }
                        }
                        else if (creationMethod.Equals("built") && distance < range)
                        {
                            outputObject = obj;
                            float lineHeight = obj.GetComponent<Rocket>() != null ? obj.transform.position.y + 40 : 0;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position + obj.transform.up * lineHeight);
                            connectionLine.enabled = true;
                            break;
                        }
                    }
                }
                if (obj.GetComponent<StorageComputer>() != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (IsValidOutputObject(obj) && distance < range)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<StorageComputer>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            break;
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            break;
                        }
                    }
                }
                if (obj.GetComponent<DarkMatterConduit>() != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (IsValidOutputObject(obj) && distance < range)
                    {
                        if (obj.GetComponent<DarkMatterConduit>().inputObject == null)
                        {
                            if (creationMethod.Equals("spawned"))
                            {
                                if (obj.GetComponent<DarkMatterConduit>().ID.Equals(outputID))
                                {
                                    outputObject = obj;
                                    obj.GetComponent<DarkMatterConduit>().inputObject = gameObject;
                                    connectionLine.SetPosition(0, transform.position);
                                    connectionLine.SetPosition(1, obj.transform.position);
                                    connectionLine.enabled = true;
                                    creationMethod = "built";
                                    break;
                                }
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                outputObject = obj;
                                obj.GetComponent<DarkMatterConduit>().inputObject = gameObject;
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
    }

    //! Gets values from input object and calls HandleOutput.
    private void HandleIO()
    {
        if (inputObject != null)
        {
            if (inputObject.GetComponent<DarkMatterCollector>() != null)
            {
                DarkMatterCollector collector = inputObject.GetComponent<DarkMatterCollector>();
                inputID = collector.ID;
                speed = collector.speed;
                if (collector.powerON == true && collector.foundDarkMatter == true && collector.speed > 0)
                {
                    if (collector.darkMatterAmount >= speed && connectionFailed == false)
                    {
                        inputObject.GetComponent<DarkMatterCollector>().darkMatterAmount -= speed;
                        darkMatterAmount += speed;
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
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (inputObject.GetComponent<DarkMatterConduit>() != null)
            {
                if (inputObject.GetComponent<DarkMatterConduit>().conduitItem.active == false)
                {
                    if (storageComputerConduitItem != null)
                    {
                        storageComputerConduitItem.active = false;
                    }
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }
        HandleOutput();
    }

    //! Moves dark matter to the output object.
    private void HandleOutput()
    {
        if (outputObject != null)
        {
            if (outputObject.GetComponent<DarkMatterConduit>() != null)
            {
                outputObject.GetComponent<DarkMatterConduit>().inputID = ID;
                outputID = outputObject.GetComponent<DarkMatterConduit>().ID;
                outputObject.GetComponent<DarkMatterConduit>().speed = speed;
                if (darkMatterAmount >= speed && connectionFailed == false && speed > 0)
                {
                    outputObject.GetComponent<DarkMatterConduit>().darkMatterAmount += speed;
                    darkMatterAmount -= speed;
                    conduitItem.active = true;
                    GetComponent<Light>().enabled = true;
                    GetComponent<AudioSource>().enabled = true;
                }
            }
            else if (outputObject.GetComponent<StorageComputer>() != null)
            {
                OutputToStorageComputer();
            }
            else if (outputObject.GetComponent<InventoryManager>() != null)
            {
                OutputToInventory();
            }
        }
        else
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

    //! Moves dark matter from the conduit to a storage computer.
    private void OutputToStorageComputer()
    {
        outputID = outputObject.GetComponent<StorageComputer>().ID;
        if (outputObject.GetComponent<StorageComputer>().initialized == true)
        {
            if (storageComputerConduitItem == null)
            {
                GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, outputObject.transform.position, outputObject.transform.rotation);
                storageComputerItemObject.transform.parent = outputObject.transform;
                storageComputerConduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
            }
            if (darkMatterAmount >= speed && connectionFailed == false && speed > 0)
            {
                connectionLine.enabled = true;
                connectionLine.SetPosition(1, outputObject.transform.position);
                bool itemAdded = false;
                foreach (InventoryManager manager in outputObject.GetComponent<StorageComputer>().computerContainers)
                {
                    if (itemAdded == false)
                    {
                        manager.AddItem("Dark Matter", speed);
                        if (manager.itemAdded == true)
                        {
                            itemAdded = true;
                            darkMatterAmount -= speed;
                            if (storageComputerConduitItem != null)
                            {
                                if (storageComputerConduitItem.textureDictionary != null)
                                {
                                    storageComputerConduitItem.billboard.GetComponent<Renderer>().material.mainTexture = storageComputerConduitItem.textureDictionary["Dark Matter"];
                                }
                                storageComputerConduitItem.target = manager.gameObject;
                            }
                        }
                    }
                }
                if (storageComputerConduitItem != null)
                {
                    storageComputerConduitItem.active = true;
                }
                conduitItem.active = true;
                GetComponent<Light>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
                connectionLine.enabled = true;
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
            GetComponent<AudioSource>().enabled = false;
        }
    }

    //! Moves dark matter to a storage container.
    private void OutputToInventory()
    {
        SetOutputID();
        if (Vector3.Distance(transform.position, outputObject.transform.position) <= range)
        {
            if (darkMatterAmount >= speed && connectionFailed == false && speed > 0)
            {
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                if (outputObject.GetComponent<Rocket>() == null || playerController.timeToDeliver == true)
                {
                    EnableEffects();
                    outputObject.GetComponent<InventoryManager>().AddItem("Dark Matter", speed);
                    if (outputObject.GetComponent<InventoryManager>().itemAdded == true)
                    {
                        darkMatterAmount -= speed;
                    }
                }
            }
        }
        else
        {
            CheckForRailCart();
        }
    }

    //!Sets the output ID to that of the current output object.
    private void SetOutputID()
    {
        if (outputObject.GetComponent<RailCart>() != null)
        {
            outputID = outputObject.GetComponent<RailCart>().ID;
            linkedToRailCart = true;
        }
        else
        {
            outputID = outputObject.GetComponent<InventoryManager>().ID;
            linkedToRailCart = false;
        }
    }

    //!Turns on line renderer, audio and light effects.
    private void EnableEffects()
    {
        float lineHeight = outputObject.GetComponent<Rocket>() != null ? outputObject.transform.position.y + 40 : 0;
        connectionLine.SetPosition(1, outputObject.transform.position + outputObject.transform.up * lineHeight);
        connectionLine.enabled = true;
        conduitItem.active = true;
        GetComponent<Light>().enabled = true;
        GetComponent<AudioSource>().enabled = true;
    }

    //!Turns off line renderer, audio and light effects.
    private void DisableEffects()
    {
        connectionLine.enabled = false;
        conduitItem.active = false;
        GetComponent<Light>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
    }

    //!Checks if there is a rail cart near the conduit.
    private void CheckForRailCart()
    {
        if (linkedToRailCart == true)
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
                        Vector3 conPos = gameObject.transform.position;
                        Vector3 cartPos = cart.gameObject.transform.position;
                        float cartDistance = Vector3.Distance(conPos, cartPos);
                        if (cartDistance < range)
                        {
                            outputObject = cart.gameObject;
                            outputID = cart.ID;
                            EnableEffects();
                            foundRailCart = true;
                        }
                    }
                }
                if (foundRailCart == false)
                {
                    DisableEffects();
                }
                findRailCartsInterval = 0;
            }
            else
            {
                DisableEffects();
            }
        }
        else
        {
            DisableEffects();
        }
    }
}