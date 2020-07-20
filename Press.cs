﻿using UnityEngine;
using System.Collections;

public class Press : MonoBehaviour
{
    public float amount;
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public string inputType;
    public string outputType;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public bool powerON;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    LineRenderer connectionLine;
    private float updateTick;
    public int address;
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
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

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
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
                                                                    obj.GetComponent<UniversalConduit>().type = outputType;
                                                                    //Debug.Log("Setting " + ID + " output conduit type to: " + outputType);
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
                                                                obj.GetComponent<UniversalConduit>().type = outputType;
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
            if (inputObject != null)
            {
                if (inputObject.GetComponent<UniversalConduit>() != null)
                {
                    if (amount < 1)
                    {
                        inputType = inputObject.GetComponent<UniversalConduit>().type;
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Copper Ingot"))
                        {
                            outputType = "Copper Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Iron Ingot"))
                        {
                            outputType = "Iron Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Tin Ingot"))
                        {
                            outputType = "Tin Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Bronze Ingot"))
                        {
                            outputType = "Bronze Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Steel Ingot"))
                        {
                            outputType = "Steel Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Aluminum Ingot"))
                        {
                            outputType = "Aluminum Plate";
                        }
                        if (inputObject.GetComponent<UniversalConduit>().type.Equals("Regolith"))
                        {
                            outputType = "Brick";
                        }
                    }
                    if (inputObject.GetComponent<UniversalConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
                        GetComponent<Light>().enabled = false;
                    }
                }
            }
            else
            {
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
            }
            if (outputObject != null)
            {
                if (outputObject.GetComponent<UniversalConduit>() != null)
                {
                    outputObject.GetComponent<UniversalConduit>().inputID = ID;
                    outputObject.GetComponent<UniversalConduit>().type = outputType;
                    outputObject.GetComponent<UniversalConduit>().speed = speed;
                    //Debug.Log("Setting " + ID + " output conduit type to: " + outputType);
                    outputID = outputObject.GetComponent<UniversalConduit>().ID;
                    if (amount >= speed)
                    {
                        if (outputType.Equals(outputObject.GetComponent<UniversalConduit>().type))
                        {
                            if (powerON == true && connectionFailed == false && inputObject != null && speed > 0)
                            {
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                machineTimer += 1;
                                if (machineTimer > 5 - (address * 0.01f))
                                {
                                    outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                    amount -= speed - heat;
                                    machineTimer = 0;
                                }
                            }
                            else
                            {
                                machineTimer = 0;
                                conduitItem.GetComponent<ConduitItem>().active = false;
                                GetComponent<Light>().enabled = false;
                            }
                        }
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
            if (inputObject != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }
}