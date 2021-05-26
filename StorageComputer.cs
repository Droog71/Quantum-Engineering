using System.Collections.Generic;
using UnityEngine;

public class StorageComputer : Machine
{
    public InventoryManager[] computerContainers;
    private List<InventoryManager> computerContainerList;
    private int connectionAttempts;
    public string ID = "unassigned";
    public bool powerON;
    public int bootTimer;
    public bool initialized;
    public int address;
    private StateManager stateManager;
    private List<GameObject> spawnedConnectionList;
    public GameObject connectionObject;
    public Material lineMat;
    public GameObject powerObject;
    public ConduitItem conduitItem;
    public PowerReceiver powerReceiver;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        computerContainerList = new List<InventoryManager>();
        spawnedConnectionList = new List<GameObject>();
        conduitItem = GetComponentInChildren<ConduitItem>(true);
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
        UpdatePowerReceiver();

        if (powerON == true)
        {
            if (initialized == false)
            {
                bootTimer++;
                if (bootTimer >= 5)
                {
                    GetContainers();
                    bool foundContainer = false;
                    int containerCount = 0;
                    foreach (InventoryManager manager in computerContainers)
                    {
                        foundContainer |= computerContainers[containerCount] != null;
                        containerCount++;
                    }
                    if (foundContainer == false)
                    {
                        Reboot();
                        connectionAttempts++;
                        initialized |= connectionAttempts >= 128;
                    }
                    else
                    {
                        initialized = true;
                    }
                    bootTimer = 0;
                }
            }
        }
        else
        {
            initialized = false;
            bootTimer = 0;
            GameObject[] spawnedConnections = spawnedConnectionList.ToArray();
            foreach (GameObject obj in spawnedConnections)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    //! Called when player interacts with the computer.
    public void GetContainers()
    {
        computerContainerList.Clear();
        GameObject[] spawnedConnections = spawnedConnectionList.ToArray();
        foreach (GameObject obj in spawnedConnections)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        InventoryManager[] allContainers = FindObjectsOfType<InventoryManager>();
        foreach (InventoryManager container in allContainers)
        {
            GameObject containerObject = container.gameObject;
            Transform containerTransform = containerObject.transform;
            float distance = Vector3.Distance(transform.position, containerObject.transform.position);
            if (IsValidContainer(containerObject) && distance <= 40)
            {
                computerContainerList.Add(container);
                GameObject spawnedConnection = Instantiate(connectionObject, containerTransform.position, containerTransform.rotation);
                spawnedConnection.transform.parent = containerObject.transform;
                spawnedConnection.SetActive(true);
                LineRenderer inputLine = spawnedConnection.AddComponent<LineRenderer>();
                inputLine.startWidth = 0.2f;
                inputLine.endWidth = 0.2f;
                inputLine.material = lineMat;
                inputLine.SetPosition(0, transform.position);
                inputLine.SetPosition(1, containerObject.transform.position);
                spawnedConnectionList.Add(spawnedConnection);
            }
        }
        computerContainers = computerContainerList.ToArray();
    }

    //! Returns true if the computer can access the container.
    private bool IsValidContainer(GameObject obj)
    {
        return obj.GetComponent<InventoryManager>().initialized == true
        && obj.GetComponent<RailCart>() == null
        && obj.GetComponent<Retriever>() == null
        && obj.GetComponent<AutoCrafter>() == null
        && obj.GetComponent<InventoryManager>().ID != "player"
        && obj.GetComponent<InventoryManager>().ID != "Rocket";
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }

    //! Removes all connections and allows the computer to search for storage containers.
    public void Reboot()
    {
        GameObject[] spawnedConnections = spawnedConnectionList.ToArray();
        foreach (GameObject obj in spawnedConnections)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        initialized = false;
        bootTimer = 0;
    }

    //! Removes line renderers when the machine is destroyed.
    public void OnDestroy()
    {
        GameObject[] spawnedConnections = spawnedConnectionList.ToArray();
        foreach (GameObject obj in spawnedConnections)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}