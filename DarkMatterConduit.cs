using UnityEngine;

public class DarkMatterConduit : MonoBehaviour
{
    public float darkMatterAmount;
    public int speed = 1;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject conduitItem;
    public Material darkMatterMat;
    public Material lineMat;
    LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public int range = 6;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
    private GameObject builtObjects;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
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
            updateTick = 0;
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
                    FindConnections();
                }
            }

            HandleIO();

            if (inputObject != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }

    // Used to remove conduit item from storage computer if connected to one
    void OnDestroy()
    {
        if (storageComputerConduitItem != null)
        {
            storageComputerConduitItem.active = false;
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

    // The object is a potential output connection
    bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj != inputObject && obj != gameObject;
    }

    // Returns true if the object is a storage container
    private bool IsStorageContainer(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null)
        {
            return obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null && !obj.GetComponent<InventoryManager>().ID.Equals("player");
        }
        return false;
    }

    // Makes connections to collectors and other conduits
    private void FindConnections()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject obj in allObjects)
        {
            if (IsValidObject(obj))
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
                                    obj.GetComponent<DarkMatterCollector>().outputObject = this.gameObject;
                                }
                                creationMethod = "built";
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                inputObject = obj;
                                obj.GetComponent<DarkMatterCollector>().outputObject = this.gameObject;
                            }
                        }
                    }
                }
                if (IsStorageContainer(obj))
                {
                    if (IsValidOutputObject(obj))
                    {
                        if (creationMethod.Equals("spawned"))
                        {
                            if (obj.GetComponent<InventoryManager>().ID.Equals(outputID))
                            {
                                float distance = Vector3.Distance(transform.position, obj.transform.position);
                                if (distance < range || obj.GetComponent<RailCart>() != null)
                                {
                                    outputObject = obj;
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
                            if (distance < range)
                            {
                                outputObject = obj;
                                connectionLine.SetPosition(0, transform.position);
                                connectionLine.SetPosition(1, obj.transform.position);
                                connectionLine.enabled = true;
                            }
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
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
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
                                }
                            }
                            else if (creationMethod.Equals("built"))
                            {
                                outputObject = obj;
                                obj.GetComponent<DarkMatterConduit>().inputObject = gameObject;
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

    // Gets values from input object and calls HandleOutput
    private void HandleIO()
    {
        if (inputObject != null)
        {
            if (inputObject.GetComponent<DarkMatterCollector>() != null)
            {
                inputID = inputObject.GetComponent<DarkMatterCollector>().ID;
                speed = inputObject.GetComponent<DarkMatterCollector>().speed;
                if (inputObject.GetComponent<DarkMatterCollector>().powerON == true && inputObject.GetComponent<DarkMatterCollector>().foundDarkMatter == true && inputObject.GetComponent<DarkMatterCollector>().speed > 0)
                {
                    if (inputObject.GetComponent<DarkMatterCollector>().darkMatterAmount >= speed && connectionFailed == false)
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
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (inputObject.GetComponent<DarkMatterConduit>() != null)
            {
                if (inputObject.GetComponent<DarkMatterConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                {
                    if (storageComputerConduitItem != null)
                    {
                        storageComputerConduitItem.active = false;
                    }
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
        }
        else
        {
            conduitItem.GetComponent<ConduitItem>().active = false;
            GetComponent<Light>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }
        HandleOutput();
    }

    // Moves dark matter to the output object
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
                    conduitItem.GetComponent<ConduitItem>().active = true;
                    GetComponent<Light>().enabled = true;
                    GetComponent<AudioSource>().enabled = true;
                }
            }
            else if (outputObject.GetComponent<StorageComputer>() != null)
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
                        conduitItem.GetComponent<ConduitItem>().active = true;
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
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            else if (outputObject.GetComponent<InventoryManager>() != null)
            {
                if (outputObject.GetComponent<RailCart>() != null)
                {
                    outputID = outputObject.GetComponent<RailCart>().ID;
                }
                else
                {
                    outputID = outputObject.GetComponent<InventoryManager>().ID;
                }
                if (Vector3.Distance(transform.position, outputObject.transform.position) <= range)
                {
                    if (darkMatterAmount >= speed && connectionFailed == false && speed > 0)
                    {
                        connectionLine.enabled = true;
                        connectionLine.SetPosition(1, outputObject.transform.position);
                        conduitItem.GetComponent<ConduitItem>().active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                        connectionLine.enabled = true;
                        outputObject.GetComponent<InventoryManager>().AddItem("Dark Matter", speed);
                        if (outputObject.GetComponent<InventoryManager>().itemAdded == true)
                        {
                            darkMatterAmount -= speed;
                        }
                    }
                }
                else
                {
                    connectionLine.enabled = false;
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
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
}