﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageComputer : MonoBehaviour
{
    public InventoryManager[] computerContainers;
    private List<InventoryManager> computerContainerList;
    public string ID = "unassigned";
    public bool powerON;
    public int bootTimer;
    public bool initialized;
    public int address;
    private float updateTick;
    private List<GameObject> spawnedConnectionList;
    public GameObject connectionObject;
    public Material lineMat;
    public GameObject powerObject;
    public GameObject conduitItem;

    // Start is called before the first frame update
    void Start()
    {
        computerContainerList = new List<InventoryManager>();
        spawnedConnectionList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
            if (powerON == true)
            {
                if (initialized == false)
                {
                    bootTimer++;
                    if (bootTimer >= 5)
                    {
                        GetContainers();
                        initialized = true;
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
    }

    void OnDestroy()
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

    // Called when player interacts with the computer.
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
            if (container.initialized == true && containerObject.GetComponent<RailCart>() == null && containerObject.GetComponent<Retriever>() == null && containerObject.GetComponent<AutoCrafter>() == null && container.ID != "player" && container.ID != "Rocket")
            {
                if (Vector3.Distance(transform.position,containerObject.transform.position) < 40)
                {
                    computerContainerList.Add(container);
                    GameObject spawnedConnection = Instantiate(connectionObject, containerObject.transform.position, containerObject.transform.rotation);
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
        }
        computerContainers = computerContainerList.ToArray();
    }
}