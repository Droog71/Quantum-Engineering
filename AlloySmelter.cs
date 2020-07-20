﻿using UnityEngine;
using System.Collections;

public class AlloySmelter : MonoBehaviour
{
    public int speed = 1;
    public int power;
    public int heat;
    public int cooling;
    public bool hasHeatExchanger;
    public float amount;
    public float amount2;
    public float outputAmount;
    public string inputType1;
    public string inputType2;
    public string outputType;
    public string ID = "unassigned";
    public string inputID1;
    public string inputID2;
    public string outputID;
    public GameObject fireObject;
    public bool powerON;
    public string creationMethod;
    public GameObject inputObject1;
    public GameObject inputObject2;
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
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                fireObject.SetActive(false);
            }
            if (inputObject1 == null || inputObject2 == null || outputObject == null)
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
                                                if (inputObject1 != null && inputObject2 != null)
                                                {
                                                    if (obj != inputObject1 && obj != inputObject2 && obj != this.gameObject)
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
            if (inputObject1 != null && inputObject2 != null)
            {
                if (inputObject1.GetComponent<UniversalConduit>() != null && inputObject2.GetComponent<UniversalConduit>() != null)
                {
                    if (amount < 1)
                    {
                        inputType1 = inputObject1.GetComponent<UniversalConduit>().type;
                    }
                    if (amount2 < 1)
                    {
                        inputType2 = inputObject2.GetComponent<UniversalConduit>().type;
                    }
                    //Debug.Log(ID + " receiving: " + inputObject1.GetComponent<UniversalConduit>().type + " and " + inputObject2.GetComponent<UniversalConduit>().type);
                    if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Copper Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Tin Ingot"))
                    {
                        outputType = "Bronze Ingot";
                        if (amount >= speed && amount2 >= speed)
                        {
                            if (powerON == true && connectionFailed == false && speed > 0)
                            {
                                outputAmount += speed - heat;
                                amount -= speed - heat;
                                amount2 -= speed - heat;
                                fireObject.SetActive(true);
                                GetComponent<AudioSource>().enabled = true;
                            }
                            else
                            {
                                fireObject.SetActive(false);
                                GetComponent<AudioSource>().enabled = false;
                            }
                            //Debug.Log(ID + " output: " + outputType);
                        }
                    }
                    else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Tin Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Copper Ingot"))
                    {
                        outputType = "Bronze Ingot";
                        if (amount >= speed && amount2 >= speed)
                        {
                            if (powerON == true && connectionFailed == false && speed > 0)
                            {
                                outputAmount += speed - heat;
                                amount -= speed - heat;
                                amount2 -= speed - heat;
                                fireObject.SetActive(true);
                                GetComponent<AudioSource>().enabled = true;
                            }
                            else
                            {
                                fireObject.SetActive(false);
                                GetComponent<AudioSource>().enabled = false;
                            }
                            //Debug.Log(ID + " output: " + outputType);
                        }
                    }
                    else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Iron Ingot") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Coal"))
                    {
                        outputType = "Steel Ingot";
                        if (amount >= speed && amount2 >= speed)
                        {
                            if (powerON == true && connectionFailed == false && speed > 0)
                            {
                                outputAmount += speed - heat;
                                amount -= speed - heat;
                                amount2 -= speed - heat;
                                fireObject.SetActive(true);
                                GetComponent<AudioSource>().enabled = true;
                            }
                            else
                            {
                                fireObject.SetActive(false);
                                GetComponent<AudioSource>().enabled = false;
                            }
                            //Debug.Log(ID + " output: " + outputType);
                        }
                    }
                    else if (inputObject1.GetComponent<UniversalConduit>().type.Equals("Coal") && inputObject2.GetComponent<UniversalConduit>().type.Equals("Iron Ingot"))
                    {
                        outputType = "Steel Ingot";
                        if (amount >= speed && amount2 >= speed)
                        {
                            if (powerON == true && connectionFailed == false && speed > 0)
                            {
                                outputAmount += speed - heat;
                                amount -= speed - heat;
                                amount2 -= speed - heat;
                                fireObject.SetActive(true);
                                GetComponent<AudioSource>().enabled = true;
                            }
                            else
                            {
                                fireObject.SetActive(false);
                                GetComponent<AudioSource>().enabled = false;
                            }
                            //Debug.Log(ID + " output: " + outputType);
                        }
                    }
                    if (inputObject1.GetComponent<UniversalConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
                    }
                    if (inputObject2.GetComponent<UniversalConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
                    }
                }
            }
            else
            {
                conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (outputAmount >= speed)
                    {
                        if (outputType.Equals(outputObject.GetComponent<UniversalConduit>().type))
                        {
                            if (powerON == true && connectionFailed == false && inputObject1 != null && inputObject2 != null && speed > 0)
                            {
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                if (GetComponent<AudioSource>().isPlaying == false)
                                {
                                    GetComponent<AudioSource>().Play();
                                    fireObject.SetActive(true);
                                }
                                machineTimer += 1;
                                if (machineTimer > 5 - (address * 0.01f))
                                {
                                    outputObject.GetComponent<UniversalConduit>().amount += speed - heat;
                                    outputAmount -= speed;
                                    machineTimer = 0;
                                }
                            }
                            else
                            {
                                conduitItem.GetComponent<ConduitItem>().active = false;
                                machineTimer = 0;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
                        }
                    }
                }
                else
                {
                    conduitItem.GetComponent<ConduitItem>().active = false;
                }
            }
            else
            {
                conduitItem.GetComponent<ConduitItem>().active = false;
                connectionLine.enabled = false;
                if (connectionFailed == true)
                {
                    if (creationMethod.Equals("spawned"))
                    {
                        creationMethod = "built";
                    }
                }
            }
            if (inputObject1 != null && inputObject2 != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }
}