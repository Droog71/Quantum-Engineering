using UnityEngine;
using System.Collections;

public class RailCartHub : MonoBehaviour
{
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    LineRenderer connectionLine;
    private float updateTick;
    public Material lineMat;
    public int address;
    public int range = 6;
    public float stopTime;
    public int connectionAttempts;
    public bool stop;
    public bool connectionFailed;
    public bool centralHub;

    void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
    }

    void OnDestroy()
    {

    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
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
                    RailCartHub[] allHubs = FindObjectsOfType<RailCartHub>();
                    if (allHubs.Length < 2)
                    {
                        centralHub = true;
                    }
                    foreach (RailCartHub hub in allHubs)
                    {
                        if (hub.gameObject.activeInHierarchy)
                        {
                            //Debug.Log(ID + " found object ID: " + hub.ID);
                            float distance = Vector3.Distance(transform.position, hub.gameObject.transform.position);
                            if (distance < range)
                            {
                                //Debug.Log(ID + " found object ID: " + hub.ID);
                                if (outputObject == null)
                                {
                                    //Debug.Log(ID + " hub has no output yet");
                                    if (inputObject != null || centralHub == true)
                                    {
                                        //Debug.Log(ID + " found object ID: "+hub.ID);
                                        if (hub.gameObject != inputObject && hub.gameObject != this.gameObject || centralHub == true && hub.gameObject != this.gameObject)
                                        {
                                            //Debug.Log(ID + " hub is not our input and is not our self");
                                            if (hub.inputObject == null)
                                            {
                                                //Debug.Log(ID + " target hub has no input object yet");
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    //Debug.Log("trying to connect " + ID + " to " + hub.ID + " vs " + outputID);
                                                    if (hub.ID.Equals(outputID))
                                                    {
                                                        outputObject = hub.gameObject;
                                                        hub.inputObject = this.gameObject;
                                                        connectionLine.SetPosition(0, transform.position);
                                                        connectionLine.SetPosition(1, hub.gameObject.transform.position);
                                                        connectionLine.enabled = true;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    outputObject = hub.gameObject;
                                                    outputID = hub.ID;
                                                    hub.inputObject = this.gameObject;
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
                    }
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
    }
}