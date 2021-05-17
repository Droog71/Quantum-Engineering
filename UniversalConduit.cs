using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UniversalConduit : Machine
{
    public float amount;
    public int speed = 1;
    public int power;
    public string type;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    public ConduitItem conduitItem;
    public Material lineMat;
    public bool inputMachineDisabled;
    public int address;
    public int range = 6;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
    private StateManager stateManager;
    private GameObject builtObjects;
    private LineRenderer connectionLine;
    private List<GameObject> objList;
    private bool linkedToRailCart;
    private int findRailCartsInterval;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
        stateManager = FindObjectOfType<StateManager>();
        objList = new List<GameObject>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("BuiltObjects");
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
                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Machine");
                objList = allObjects.ToList();
                objList.Add(GameObject.Find("Rocket"));
                objList.Add(GameObject.Find("LanderCargo"));
                foreach (GameObject obj in objList)
                {
                    if (inputObject != null && outputObject != null)
                    {
                        break;
                    }
                    if (obj != null)
                    {
                        if (inputObject == null)
                        {
                            AttemptInputConnection(obj);
                        }
                        if (outputObject == null)
                        {
                            AttemptOutputConnection(obj);
                        }
                    }
                }
            }
        }
        if (inputObject != null)
        {
            HandleInput();
        }
        else
        {
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }
        if (outputObject != null && connectionFailed == false)
        {
            HandleOutput();
        }
        else
        {
            if (connectionFailed == true)
            {
                if (creationMethod.Equals("spawned"))
                {
                    creationMethod = "built";
                }
            }
            conduitItem.active = false;
            GetComponent<Light>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
            connectionLine.enabled = false;
        }
        if (inputObject != null && outputObject != null)
        {
            connectionAttempts = 0;
        }
    }

    //! The object is a potential output connection.
    private bool IsValidOutputObject(GameObject obj)
    {
        return outputObject == null && inputObject != null && obj != inputObject && obj != gameObject;
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

    //! Puts items into a storage container or other object with attached inventory manager.
    private void OutputToInventory()
    {
        SetOutputID();
        float distance = Vector3.Distance(transform.position, outputObject.transform.position);
        if (distance < range)
        {
            if (type != "" && type != "nothing")
            {
                ToggleConduitItem();
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                if (outputObject.GetComponent<Rocket>() == null || playerController.timeToDeliver == true)
                {
                    EnableEffects();
                    if (amount >= speed)
                    {
                        if (type.Equals("Brick") && inputObject.GetComponent<Press>() != null)
                        {
                            outputObject.GetComponent<InventoryManager>().AddItem(type, speed * 10);
                            if (outputObject.GetComponent<InventoryManager>().itemAdded == true)
                            {
                                amount -= speed;
                            }
                        }
                        else
                        {
                            outputObject.GetComponent<InventoryManager>().AddItem(type, speed);
                            if (outputObject.GetComponent<InventoryManager>().itemAdded == true)
                            {
                                amount -= speed;
                            }
                        }
                    }
                }
            }
            else
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
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

    //!Enables or disables conduit item rendering.
    private void ToggleConduitItem()
    {
        if (inputObject != null)
        {
            if (inputMachineDisabled == false && inputObject.GetComponent<UniversalConduit>() == null)
            {
                conduitItem.active = true;
            }
            else if (inputObject.GetComponent<UniversalConduit>() != null)
            {
                conduitItem.active |= inputObject.GetComponent<UniversalConduit>().inputMachineDisabled == false;
            }
        }
    }

    //!Turns on line renderer, audio and light effects.
    private void EnableEffects()
    {
        connectionLine.enabled = true;
        float lineHeight = outputObject.GetComponent<Rocket>() != null ? outputObject.transform.position.y + 40 : 0;
        connectionLine.SetPosition(1, outputObject.transform.position + outputObject.transform.up * lineHeight);
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

    //! Moves items into a storage computer.
    private void OutputToStorageComputer()
    {
        outputID = outputObject.GetComponent<StorageComputer>().ID;
        if (outputObject.GetComponent<StorageComputer>().initialized == true && type != "" && type != "nothing")
        {
            if (storageComputerConduitItem == null)
            {
                GameObject storageComputerItemObject = Instantiate(storageComputerConduitItemObject, outputObject.transform.position, outputObject.transform.rotation);
                storageComputerItemObject.transform.parent = outputObject.transform;
                storageComputerConduitItem = storageComputerItemObject.GetComponent<ConduitItem>();
            }
            else
            {
                if (inputMachineDisabled == true && inputObject.GetComponent<UniversalConduit>() == null)
                {
                    storageComputerConduitItem.active = false;
                }
                else if (inputObject.GetComponent<UniversalConduit>() != null)
                {
                    if (inputObject.GetComponent<UniversalConduit>().inputMachineDisabled == true)
                    {
                        storageComputerConduitItem.active = false;
                    }
                }
            }
            if (amount >= speed)
            {
                connectionLine.enabled = true;
                connectionLine.SetPosition(1, outputObject.transform.position);
                if (type.Equals("Brick") && inputObject.GetComponent<Press>() != null)
                {
                    bool itemAdded = false;
                    foreach (InventoryManager manager in outputObject.GetComponent<StorageComputer>().computerContainers)
                    {
                        if (itemAdded == false)
                        {
                            manager.AddItem(type, speed * 10);
                            if (manager.itemAdded == true)
                            {
                                itemAdded = true;
                                amount -= speed;
                                if (storageComputerConduitItem != null)
                                {
                                    if (storageComputerConduitItem.textureDictionary != null)
                                    {
                                        storageComputerConduitItem.billboard.GetComponent<Renderer>().material.mainTexture = storageComputerConduitItem.textureDictionary[type];
                                    }
                                    storageComputerConduitItem.target = manager.gameObject;
                                }
                            }
                        }
                    }
                }
                else
                {
                    bool itemAdded = false;
                    foreach (InventoryManager manager in outputObject.GetComponent<StorageComputer>().computerContainers)
                    {
                        if (itemAdded == false)
                        {
                            manager.AddItem(type, speed);
                            if (manager.itemAdded == true)
                            {
                                itemAdded = true;
                                amount -= speed;
                                if (storageComputerConduitItem != null)
                                {
                                    if (storageComputerConduitItem.textureDictionary != null)
                                    {
                                        storageComputerConduitItem.billboard.GetComponent<Renderer>().material.mainTexture = storageComputerConduitItem.textureDictionary[type];
                                    }
                                    storageComputerConduitItem.target = manager.gameObject;
                                }
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

    //! Makes input and output connections.
    private void AttemptInputConnection(GameObject obj)
    {
        if (obj.GetComponent<Auger>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < range && inputObject == null && obj.GetComponent<Auger>().outputObject == null)
            {
                if (creationMethod.Equals("spawned") && obj.GetComponent<Auger>().ID.Equals(inputID))
                {
                    inputObject = obj;
                    type = "Regolith";
                    obj.GetComponent<Auger>().outputObject = gameObject;
                    creationMethod = "built";
                }
                else if (creationMethod.Equals("built"))
                {
                    inputObject = obj;
                    type = "Regolith";
                    obj.GetComponent<Auger>().outputObject = gameObject;
                }
            }
        }
        if (obj.GetComponent<UniversalExtractor>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < range && inputObject == null && obj.GetComponent<UniversalExtractor>().outputObject == null)
            {
                if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalExtractor>().ID.Equals(inputID))
                {
                    inputObject = obj;
                    obj.GetComponent<UniversalExtractor>().outputObject = gameObject;
                    creationMethod = "built";
                }
                else if (creationMethod.Equals("built"))
                {
                    inputObject = obj;
                    obj.GetComponent<UniversalExtractor>().outputObject = gameObject;
                }
            }
        }
    }

    //! Makes input and output connections.
    private void AttemptOutputConnection(GameObject obj)
    {
        if (obj.GetComponent<UniversalConduit>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<UniversalConduit>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<UniversalConduit>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<UniversalConduit>().type = type;
                        obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<UniversalConduit>().type = type;
                        obj.GetComponent<UniversalConduit>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (IsStorageContainer(obj))
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj))
            {
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
                    }
                }
                else if (creationMethod.Equals("built") && distance < range)
                {
                    outputObject = obj;
                    float lineHeight = obj.GetComponent<Rocket>() != null ? obj.transform.position.y + 40 : 0;
                    connectionLine.SetPosition(0, transform.position);
                    connectionLine.SetPosition(1, obj.transform.position + obj.transform.up * lineHeight);
                    connectionLine.enabled = true;
                }
            }
        }
        if (obj.GetComponent<StorageComputer>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < range && outputObject == null && inputObject != null)
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
        if (obj.GetComponent<AlloySmelter>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<AlloySmelter>().inputObject1 == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<AlloySmelter>().inputObject1 = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<AlloySmelter>().inputObject1 = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
                else if (obj.GetComponent<AlloySmelter>().inputObject2 == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<AlloySmelter>().inputObject2 = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<AlloySmelter>().inputObject2 = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<Smelter>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<Smelter>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Smelter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<Smelter>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<Smelter>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<PowerSource>() != null)
        {
            if (obj.GetComponent<PowerSource>().type.Equals("Generator"))
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (IsValidOutputObject(obj) && distance < range)
                {
                    if (obj.GetComponent<PowerSource>().inputObject == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerSource>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            obj.GetComponent<PowerSource>().inputObject = gameObject;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            obj.GetComponent<PowerSource>().inputObject = gameObject;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
            }
        }
        if (obj.GetComponent<Extruder>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<Extruder>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Extruder>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<Extruder>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<Extruder>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<HeatExchanger>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<HeatExchanger>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<HeatExchanger>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<HeatExchanger>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<HeatExchanger>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<GearCutter>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<GearCutter>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<GearCutter>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<GearCutter>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<GearCutter>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<Press>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<Press>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<Press>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<Press>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<Press>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<MissileTurret>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<MissileTurret>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<MissileTurret>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<MissileTurret>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<MissileTurret>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
        if (obj.GetComponent<ModMachine>() != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (IsValidOutputObject(obj) && distance < range)
            {
                if (obj.GetComponent<ModMachine>().inputObject == null)
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<ModMachine>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        obj.GetComponent<ModMachine>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        obj.GetComponent<ModMachine>().inputObject = gameObject;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
    }

    //! Handles items received from input object.
    private void HandleInput()
    {
        if (inputObject.GetComponent<AlloySmelter>() != null)
        {
            if (inputObject.GetComponent<AlloySmelter>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<Auger>() != null)
        {
            inputID = inputObject.GetComponent<Auger>().ID;
            speed = inputObject.GetComponent<Auger>().speed;
            if (inputObject.GetComponent<Auger>().powerON == true && inputObject.GetComponent<Auger>().speed > 0)
            {
                if (inputObject.GetComponent<Auger>().amount >= speed)
                {
                    inputObject.GetComponent<Auger>().amount -= speed;
                    amount += speed;
                }
                inputMachineDisabled = false;
            }
            else
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
        }
        if (inputObject.GetComponent<AutoCrafter>() != null)
        {
            if (inputObject.GetComponent<AutoCrafter>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<Extruder>() != null)
        {
            if (inputObject.GetComponent<Extruder>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<GearCutter>() != null)
        {
            if (inputObject.GetComponent<GearCutter>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<Press>() != null)
        {
            if (inputObject.GetComponent<Press>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<Retriever>() != null)
        {
            if (inputObject.GetComponent<Retriever>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<Smelter>() != null)
        {
            if (inputObject.GetComponent<Smelter>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<ModMachine>() != null)
        {
            if (inputObject.GetComponent<ModMachine>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<UniversalConduit>() != null)
        {
            if (inputObject.GetComponent<UniversalConduit>().conduitItem.active == false)
            {
                conduitItem.active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                inputMachineDisabled = true;
            }
            else
            {
                inputMachineDisabled = false;
            }
        }
        if (inputObject.GetComponent<UniversalExtractor>() != null)
        {
            inputID = inputObject.GetComponent<UniversalExtractor>().ID;
            speed = inputObject.GetComponent<UniversalExtractor>().speed;
            if (inputObject.GetComponent<UniversalExtractor>().type != "" && inputObject.GetComponent<UniversalExtractor>().type != "nothing")
            {
                type = inputObject.GetComponent<UniversalExtractor>().type;
                if (inputObject.GetComponent<UniversalExtractor>().powerON == true && inputObject.GetComponent<UniversalExtractor>().speed > 0)
                {
                    if (inputObject.GetComponent<UniversalExtractor>().amount >= speed)
                    {
                        inputObject.GetComponent<UniversalExtractor>().amount -= speed;
                        amount += speed;
                    }
                    inputMachineDisabled = false;
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                    inputMachineDisabled = true;
                }
            }
        }
    }

    //! Moves items received to the output object.
    private void HandleOutput()
    {
        connectionLine.SetPosition(0, transform.position);
        connectionLine.SetPosition(1, outputObject.transform.position);
        connectionLine.enabled = true;
        if (speed > 0)
        {
            if (outputObject.GetComponent<UniversalConduit>() != null)
            {
                outputObject.GetComponent<UniversalConduit>().inputID = ID;
                outputID = outputObject.GetComponent<UniversalConduit>().ID;
                if (type.Equals("Brick") && inputObject.GetComponent<Press>() != null)
                {
                    outputObject.GetComponent<UniversalConduit>().speed = speed * 10;
                }
                else
                {
                    outputObject.GetComponent<UniversalConduit>().speed = speed;
                }
                if (type != "" && type != "nothing")
                {
                    if (outputObject.GetComponent<UniversalConduit>().amount < 1)
                    {
                        outputObject.GetComponent<UniversalConduit>().type = type;
                    }
                    if (amount >= speed && outputObject.GetComponent<UniversalConduit>().type.Equals(type))
                    {
                        if (type.Equals("Brick") && inputObject.GetComponent<Press>() != null)
                        {
                            outputObject.GetComponent<UniversalConduit>().amount += speed * 10;
                        }
                        else
                        {
                            outputObject.GetComponent<UniversalConduit>().amount += speed;
                        }
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<PowerSource>() != null)
            {
                if (type.Equals("Coal"))
                {
                    outputObject.GetComponent<PowerSource>().fuelType = type;
                    outputObject.GetComponent<PowerSource>().inputID = ID;
                    outputID = outputObject.GetComponent<PowerSource>().ID;
                    if (amount >= speed)
                    {
                        if (outputObject.GetComponent<PowerSource>().fuelAmount < 1000)
                        {
                            outputObject.GetComponent<PowerSource>().fuelAmount += speed;
                        }
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<MissileTurret>() != null)
            {
                if (type.Equals("Missile"))
                {
                    outputObject.GetComponent<MissileTurret>().ammoType = type;
                    outputObject.GetComponent<MissileTurret>().inputID = ID;
                    outputID = outputObject.GetComponent<MissileTurret>().ID;
                    if (amount >= speed)
                    {
                        if (outputObject.GetComponent<MissileTurret>().ammoAmount < 1000)
                        {
                            outputObject.GetComponent<MissileTurret>().ammoAmount += speed;
                            amount -= speed;
                        }
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<Smelter>() != null)
            {
                if (type.Equals(outputObject.GetComponent<Smelter>().inputType))
                {
                    outputObject.GetComponent<Smelter>().inputID = ID;
                    outputID = outputObject.GetComponent<Smelter>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<Smelter>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<HeatExchanger>() != null)
            {
                if (type.Equals(outputObject.GetComponent<HeatExchanger>().inputType))
                {
                    outputObject.GetComponent<HeatExchanger>().inputID = ID;
                    outputID = outputObject.GetComponent<HeatExchanger>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<HeatExchanger>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<AlloySmelter>() != null)
            {
                outputID = outputObject.GetComponent<AlloySmelter>().ID;
                if (outputObject.GetComponent<AlloySmelter>().inputObject1 == gameObject)
                {
                    outputObject.GetComponent<AlloySmelter>().inputID1 = ID;
                    if (amount >= speed)
                    {
                        if (type.Equals(outputObject.GetComponent<AlloySmelter>().inputType1))
                        {
                            outputObject.GetComponent<AlloySmelter>().amount += speed;
                            amount -= speed;
                            conduitItem.active = true;
                            GetComponent<Light>().enabled = true;
                            GetComponent<AudioSource>().enabled = true;
                        }
                        else
                        {
                            conduitItem.active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                }
                else if (outputObject.GetComponent<AlloySmelter>().inputObject2 == gameObject)
                {
                    outputObject.GetComponent<AlloySmelter>().inputID2 = ID;
                    if (amount >= speed)
                    {
                        if (type.Equals(outputObject.GetComponent<AlloySmelter>().inputType2))
                        {
                            outputObject.GetComponent<AlloySmelter>().amount2 += speed;
                            amount -= speed;
                            conduitItem.active = true;
                            GetComponent<Light>().enabled = true;
                            GetComponent<AudioSource>().enabled = true;
                        }
                        else
                        {
                            conduitItem.active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                }
            }
            if (outputObject.GetComponent<Press>() != null)
            {
                if (type.Equals(outputObject.GetComponent<Press>().inputType))
                {
                    outputObject.GetComponent<Press>().inputID = ID;
                    outputID = outputObject.GetComponent<Press>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<Press>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<Extruder>() != null)
            {
                if (type.Equals(outputObject.GetComponent<Extruder>().inputType))
                {
                    outputObject.GetComponent<Extruder>().inputID = ID;
                    outputID = outputObject.GetComponent<Extruder>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<Extruder>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<GearCutter>() != null)
            {
                if (type.Equals(outputObject.GetComponent<GearCutter>().inputType))
                {
                    outputObject.GetComponent<GearCutter>().inputID = ID;
                    outputID = outputObject.GetComponent<GearCutter>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<GearCutter>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<ModMachine>() != null)
            {
                if (type.Equals(outputObject.GetComponent<ModMachine>().inputType))
                {
                    outputObject.GetComponent<ModMachine>().inputID = ID;
                    outputID = outputObject.GetComponent<ModMachine>().ID;
                    if (amount >= speed)
                    {
                        outputObject.GetComponent<ModMachine>().amount += speed;
                        amount -= speed;
                        conduitItem.active = true;
                        GetComponent<Light>().enabled = true;
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
                else
                {
                    conduitItem.active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (outputObject.GetComponent<InventoryManager>() != null)
            {
                OutputToInventory();
            }
            if (outputObject.GetComponent<StorageComputer>() != null)
            {
                OutputToStorageComputer();
            }
        }
        else
        {
            GetComponent<Light>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }
    }

    //! Used to remove conduit item from storage computer if connected to one.
    public void OnDestroy()
    {
        if (storageComputerConduitItem != null)
        {
            storageComputerConduitItem.active = false;
        }
    }
}