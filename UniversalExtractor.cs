﻿using UnityEngine;
using System.Collections;

public class UniversalExtractor : MonoBehaviour
{
    public float amount;
    public int speed = 1;
    public int heat;
    public int cooling;
    public bool hasHeatExchanger;
    private int machineTimer;
    public int power;
    public string type;
    public GameObject outputObject;
    private GameObject inputObject;
    public GameObject powerObject;
    public GameObject conduitItem;
    public Material lineMat;
    public string ID = "unassigned";
    public string creationMethod;
    LineRenderer connectionLine;
    LineRenderer inputLine;
    private float updateTick;
    public int address;
    public bool powerON;
    private bool extractingIce;
    private bool hasResource;
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
        builtObjects = GameObject.Find("Built_Objects");
    }

    void OnDestroy()
    {
        if (inputLine != null)
        {
            Destroy(inputLine);
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
            if (speed > 1 && extractingIce == false)
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
            if (outputObject != null)
            {
                connectionLine.SetPosition(0, transform.position);
                connectionLine.SetPosition(1, outputObject.transform.position);
                connectionLine.enabled = true;
            }
            else
            {
                connectionLine.enabled = false;
            }

            if (hasResource == true)
            {
                connectionAttempts = 0;
                if (powerON == true && connectionFailed == false && speed > 0)
                {
                    conduitItem.GetComponent<ConduitItem>().active = true;
                    GetComponent<Light>().enabled = true;
                    GetComponent<AudioSource>().enabled = true;
                    machineTimer += 1;
                    if (machineTimer > 5 - (address * 0.01f))
                    {
                        amount += speed - heat;
                        machineTimer = 0;
                    }
                }
                else
                {
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
                //Debug.Log("Smelter trying to add " + outputType + " to " + outputObject.GetComponent<InventoryManager>().ID);
                type = inputObject.GetComponent<UniversalResource>().type;
            }
            else
            {
                connectionAttempts += 1;
                if (connectionAttempts >= 120)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }
                if (connectionFailed == false)
                {
                    UniversalResource[] allResources = FindObjectsOfType<UniversalResource>();
                    foreach (UniversalResource r in allResources)
                    {
                        GameObject obj = r.gameObject;
                        if (obj != null)
                        {
                            if (obj.transform.parent != builtObjects.transform)
                            {
                                if (obj.activeInHierarchy)
                                {
                                    if (obj.GetComponent<UniversalResource>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj.GetComponent<UniversalResource>().extractor == null)
                                            {
                                                obj.GetComponent<UniversalResource>().extractor = this.gameObject;
                                            }
                                            if (obj.GetComponent<UniversalResource>().extractor == this.gameObject)
                                            {
                                                if (obj.GetComponent<UniversalResource>().type.Equals("Ice"))
                                                {
                                                    extractingIce = true;
                                                }
                                                else
                                                {
                                                    extractingIce = false;
                                                }
                                                if (inputLine == null && obj.GetComponent<LineRenderer>() == null)
                                                {
                                                    inputLine = obj.AddComponent<LineRenderer>();
                                                    inputLine.startWidth = 0.2f;
                                                    inputLine.endWidth = 0.2f;
                                                    inputLine.material = lineMat;
                                                    inputLine.SetPosition(0, transform.position);
                                                    inputLine.SetPosition(1, obj.transform.position);
                                                }
                                                inputObject = obj;
                                                hasResource = true;
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