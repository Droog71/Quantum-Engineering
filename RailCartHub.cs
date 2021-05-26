using UnityEngine;
using System.Collections.Generic;

public class RailCartHub : Machine
{
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod = "built";
    public GameObject inputObject;
    public GameObject outputObject;
    private StateManager stateManager;
    private LineRenderer connectionLine;
    public Material lineMat;
    public int address;
    public int range = 6;
    public float stopTime;
    public int connectionAttempts;
    public int circuit;
    public bool stop;
    public bool connectionFailed;
    public bool centralHub;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        stateManager = FindObjectOfType<StateManager>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();

        if (inputObject == null || outputObject == null)
        {
            connectionAttempts += 1;
            if (creationMethod.Equals("spawned"))
            {
                if (connectionAttempts >= 128)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
            }
            else
            {
                if (connectionAttempts >= 512)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
            }
            if (connectionFailed == false)
            {
                FindConnection();
            }
        }
        if (inputObject != null)
        {
            inputID = inputObject.GetComponent<RailCartHub>().ID;
        }
        if (outputObject != null)
        {
            outputID = outputObject.GetComponent<RailCartHub>().ID;
        }
        if (outputObject == null)
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
        if (inputObject != null && outputObject != null)
        {
            connectionAttempts = 0;
        }
    }

    //! Returns true if this hub is ready to connect to the next.
    private bool ReadyToConnect()
    {
        if (outputObject == null)
        {
            return inputObject != null || centralHub == true;
        }
        return false;
    }

    //! Returns true if the hub in question is a potential connection.
    private bool CanConnect(RailCartHub hub)
    {
        if (centralHub == true)
        {
            return hub.gameObject != gameObject && hub.inputObject == null;
        }
        return hub.gameObject != inputObject && hub.gameObject != gameObject && hub.inputObject == null;
    }

    //! Finds the first hub within range.
    private void FindConnection()
    {
        RailCartHub[] allHubs = FindObjectsOfType<RailCartHub>();
        List<RailCartHub> hubList = new List<RailCartHub>();
        foreach (RailCartHub hub in allHubs)
        {
            if (hub.circuit == circuit)
            {
                hubList.Add(hub);
            }
        }
        centralHub |= hubList.Count < 2;
        foreach (RailCartHub hub in hubList)
        {
            if (hub.gameObject.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, hub.gameObject.transform.position);
                if (distance < range && ReadyToConnect())
                {
                    if (CanConnect(hub))
                    {
                        if (creationMethod.Equals("spawned") && hub.ID.Equals(outputID))
                        {
                            outputObject = hub.gameObject;
                            hub.inputObject = gameObject;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, hub.gameObject.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = hub.gameObject;
                            outputID = hub.ID;
                            hub.inputObject = gameObject;
                            hub.inputID = ID;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, hub.gameObject.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
            }
        }
    }
}