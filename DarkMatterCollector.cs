﻿using UnityEngine;
using System.Collections;

public class DarkMatterCollector : MonoBehaviour
{
    public float darkMatterAmount;
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public GameObject outputObject;
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
    private int machineTimer;
    public int connectionAttempts;
    public bool connectionFailed;
    public bool foundDarkMatter;
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
            if (foundDarkMatter == true)
            {
                if (powerON == true && connectionFailed == false && speed > 0)
                {
                    conduitItem.GetComponent<ConduitItem>().active = true;
                    GetComponent<Light>().enabled = true;
                    GetComponent<AudioSource>().enabled = true;
                    machineTimer += 1;
                    if (machineTimer > 5 - (address * 0.01f))
                    {
                        darkMatterAmount += speed - heat;
                        machineTimer = 0;
                    }
                }
                else
                {
                    machineTimer = 0;
                    conduitItem.GetComponent<ConduitItem>().active = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
            }
            if (foundDarkMatter == false)
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
                    DarkMatter[] allDarkMatter = FindObjectsOfType<DarkMatter>();
                    foreach (DarkMatter d in allDarkMatter)
                    {
                        GameObject obj = d.gameObject;
                        if (obj != null)
                        {
                            if (obj.transform.parent != builtObjects.transform)
                            {
                                if (obj.activeInHierarchy)
                                {
                                    if (obj.GetComponent<DarkMatter>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < 20)
                                        {
                                            if (obj.GetComponent<DarkMatter>().collector == null)
                                            {
                                                obj.GetComponent<DarkMatter>().collector = this.gameObject;
                                            }
                                            if (obj.GetComponent<DarkMatter>().collector == this.gameObject)
                                            {
                                                if (inputLine == null && obj.GetComponent<LineRenderer>() == null)
                                                {
                                                    inputLine = obj.AddComponent<LineRenderer>();
                                                    inputLine.startWidth = 0.2f;
                                                    inputLine.endWidth = 0.2f;
                                                    inputLine.material = lineMat;
                                                    inputLine.SetPosition(0, transform.position);
                                                    inputLine.SetPosition(1, obj.transform.position);
                                                }
                                                foundDarkMatter = true;
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