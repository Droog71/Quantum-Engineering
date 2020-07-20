﻿using UnityEngine;
using System.Collections;

public class UniversalConduit : MonoBehaviour
{
    public float amount;
    public int speed = 1;
    public int power;
    public string type;
    public string ID = "unassigned";
    public string inputID;
    public string outputID;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    public GameObject conduitItem;
    public Material lineMat;
    LineRenderer connectionLine;
    private float updateTick;
    public bool inputMachineDisabled;
    public int address;
    public int range = 6;
    public int connectionAttempts;
    public bool connectionFailed;
    public GameObject storageComputerConduitItemObject;
    public ConduitItem storageComputerConduitItem;
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
        if (storageComputerConduitItem != null)
        {
            storageComputerConduitItem.active = false;
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
                                    if (obj.GetComponent<Auger>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (inputObject == null && obj.GetComponent<Auger>().outputObject == null)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<Auger>().ID.Equals(inputID))
                                                    {
                                                        inputObject = obj;
                                                        type = "Regolith";
                                                        obj.GetComponent<Auger>().outputObject = this.gameObject;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    inputObject = obj;
                                                    type = "Regolith";
                                                    obj.GetComponent<Auger>().outputObject = this.gameObject;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<UniversalExtractor>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (inputObject == null && obj.GetComponent<UniversalExtractor>().outputObject == null)
                                            {
                                                if (creationMethod.Equals("spawned"))
                                                {
                                                    if (obj.GetComponent<UniversalExtractor>().ID.Equals(inputID))
                                                    {
                                                        inputObject = obj;
                                                        obj.GetComponent<UniversalExtractor>().outputObject = this.gameObject;
                                                        creationMethod = "built";
                                                    }
                                                }
                                                else if (creationMethod.Equals("built"))
                                                {
                                                    inputObject = obj;
                                                    obj.GetComponent<UniversalExtractor>().outputObject = this.gameObject;
                                                }
                                            }
                                        }
                                    }
                                    if (obj.GetComponent<UniversalConduit>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
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
                                                                    obj.GetComponent<UniversalConduit>().type = type;
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
                                                                obj.GetComponent<UniversalConduit>().type = type;
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
                                    if (obj.GetComponent<InventoryManager>() != null && !obj.GetComponent<InventoryManager>().ID.Equals("player") && obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null)
                                    {
                                        if (outputObject == null)
                                        {
                                            if (inputObject != null)
                                            {
                                                if (obj != inputObject && obj != this.gameObject)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + outputID);
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
                                        }
                                    }
                                    if (obj.GetComponent<StorageComputer>() != null)
                                    {
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    if (creationMethod.Equals("spawned"))
                                                    {
                                                        //Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<InventoryManager>().ID + " vs " + outputID);
                                                        if (obj.GetComponent<StorageComputer>().ID.Equals(outputID))
                                                        {
                                                            outputObject = obj;
                                                            connectionLine.SetPosition(0, transform.position);
                                                            connectionLine.SetPosition(1, obj.transform.position);
                                                            connectionLine.enabled = true;
                                                        }
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
                                        }
                                    }
                                    if (obj.GetComponent<AlloySmelter>() != null)
                                    {
                                        ////Debug.Log("CONDUIT ALLOY FOUND SMELTER");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND ALLOY SMELTER IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<AlloySmelter>().inputObject1 == null)
                                                        {
                                                            ////Debug.Log("ALLOY SMELTER INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<AlloySmelter>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<AlloySmelter>().inputObject1 = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW ALLOY SMELTER");
                                                                outputObject = obj;
                                                                obj.GetComponent<AlloySmelter>().inputObject1 = this.gameObject;
                                                                connectionLine.SetPosition(0, transform.position);
                                                                connectionLine.SetPosition(1, obj.transform.position);
                                                                connectionLine.enabled = true;
                                                            }
                                                        }
                                                        ////Debug.Log("INPUT is VALID");
                                                        else if (obj.GetComponent<AlloySmelter>().inputObject2 == null)
                                                        {
                                                            ////Debug.Log("ALLOY SMELTER INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<AlloySmelter>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<AlloySmelter>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<AlloySmelter>().inputObject2 = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW ALLOY SMELTER");
                                                                outputObject = obj;
                                                                obj.GetComponent<AlloySmelter>().inputObject2 = this.gameObject;
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
                                    if (obj.GetComponent<Smelter>() != null)
                                    {
                                        ////Debug.Log("CONDUIT FOUND SMELTER");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND SMELTER IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<Smelter>().inputObject == null)
                                                        {
                                                            ////Debug.Log("SMELTER INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<Smelter>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<Smelter>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<Smelter>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW SMELTER");
                                                                outputObject = obj;
                                                                obj.GetComponent<Smelter>().inputObject = this.gameObject;
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
                                    if (obj.GetComponent<PowerSource>() != null)
                                    {
                                        if (obj.GetComponent<PowerSource>().type.Equals("Generator"))
                                        {
                                            ////Debug.Log("CONDUIT FOUND GENERATOR");
                                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                                            if (distance < range)
                                            {
                                                ////Debug.Log("CONDUIT FOUND GENERATOR IN RANGE");
                                                if (outputObject == null)
                                                {
                                                    if (inputObject != null)
                                                    {
                                                        ////Debug.Log("CONDUIT HAS INPUT");
                                                        if (obj != inputObject && obj != this.gameObject)
                                                        {
                                                            ////Debug.Log("INPUT is VALID");
                                                            if (obj.GetComponent<PowerSource>().inputObject == null)
                                                            {
                                                                ////Debug.Log("GENERATOR INPUT PORT IS EMPTY");
                                                                if (creationMethod.Equals("spawned"))
                                                                {
                                                                    ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<Smelter>().ID + " vs " + outputID);
                                                                    if (obj.GetComponent<PowerSource>().ID.Equals(outputID))
                                                                    {
                                                                        ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                        outputObject = obj;
                                                                        obj.GetComponent<PowerSource>().inputObject = this.gameObject;
                                                                        connectionLine.SetPosition(0, transform.position);
                                                                        connectionLine.SetPosition(1, obj.transform.position);
                                                                        connectionLine.enabled = true;
                                                                        creationMethod = "built";
                                                                    }
                                                                }
                                                                else if (creationMethod.Equals("built"))
                                                                {
                                                                    ////Debug.Log("NEW GENERATOR");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<PowerSource>().inputObject = this.gameObject;
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
                                    if (obj.GetComponent<Extruder>() != null)
                                    {
                                        ////Debug.Log("CONDUIT FOUND Extruder");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND Extruder IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<Extruder>().inputObject == null)
                                                        {
                                                            ////Debug.Log("Extruder INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<Extruder>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<Extruder>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<Extruder>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW Extruder");
                                                                outputObject = obj;
                                                                obj.GetComponent<Extruder>().inputObject = this.gameObject;
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
                                    if (obj.GetComponent<HeatExchanger>() != null)
                                    {
                                        ////Debug.Log("CONDUIT FOUND GearCutter");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND GearCutter IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<HeatExchanger>().inputObject == null)
                                                        {
                                                            ////Debug.Log("GearCutter INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<GearCutter>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<HeatExchanger>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<HeatExchanger>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW GearCutter");
                                                                outputObject = obj;
                                                                obj.GetComponent<HeatExchanger>().inputObject = this.gameObject;
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
                                    if (obj.GetComponent<GearCutter>() != null)
                                    {
                                        ////Debug.Log("CONDUIT FOUND GearCutter");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND GearCutter IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<GearCutter>().inputObject == null)
                                                        {
                                                            ////Debug.Log("GearCutter INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<GearCutter>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<GearCutter>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<GearCutter>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW GearCutter");
                                                                outputObject = obj;
                                                                obj.GetComponent<GearCutter>().inputObject = this.gameObject;
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
                                    if (obj.GetComponent<Press>() != null)
                                    {
                                        ////Debug.Log("CONDUIT FOUND Press");
                                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                                        if (distance < range)
                                        {
                                            ////Debug.Log("CONDUIT FOUND Press IN RANGE");
                                            if (outputObject == null)
                                            {
                                                if (inputObject != null)
                                                {
                                                    ////Debug.Log("CONDUIT HAS INPUT");
                                                    if (obj != inputObject && obj != this.gameObject)
                                                    {
                                                        ////Debug.Log("INPUT is VALID");
                                                        if (obj.GetComponent<Press>().inputObject == null)
                                                        {
                                                            ////Debug.Log("Press INPUT PORT IS EMPTY");
                                                            if (creationMethod.Equals("spawned"))
                                                            {
                                                                ////Debug.Log("trying to connect " + ID + " to " + obj.GetComponent<Press>().ID + " vs " + outputID);
                                                                if (obj.GetComponent<Press>().ID.Equals(outputID))
                                                                {
                                                                    ////Debug.Log("PRE-EXISTING CONNECTION ESTABLISHING");
                                                                    outputObject = obj;
                                                                    obj.GetComponent<Press>().inputObject = this.gameObject;
                                                                    connectionLine.SetPosition(0, transform.position);
                                                                    connectionLine.SetPosition(1, obj.transform.position);
                                                                    connectionLine.enabled = true;
                                                                    creationMethod = "built";
                                                                }
                                                            }
                                                            else if (creationMethod.Equals("built"))
                                                            {
                                                                ////Debug.Log("NEW Press");
                                                                outputObject = obj;
                                                                obj.GetComponent<Press>().inputObject = this.gameObject;
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
                if (inputObject.GetComponent<AlloySmelter>() != null)
                {
                    if (inputObject.GetComponent<AlloySmelter>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                        conduitItem.GetComponent<ConduitItem>().active = false;
                        GetComponent<Light>().enabled = false;
                        GetComponent<AudioSource>().enabled = false;
                        inputMachineDisabled = true;
                    }
                }
                if (inputObject.GetComponent<AutoCrafter>() != null)
                {
                    if (inputObject.GetComponent<AutoCrafter>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<Extruder>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<GearCutter>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<Press>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<Retriever>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<Smelter>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                    if (inputObject.GetComponent<UniversalConduit>().conduitItem.GetComponent<ConduitItem>().active == false)
                    {
                        //Debug.Log(ID+" input conduit item disabled");
                        conduitItem.GetComponent<ConduitItem>().active = false;
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
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                            inputMachineDisabled = true;
                        }
                    }
                }
            }
            else
            {
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
            }
            if (outputObject != null && connectionFailed == false)
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
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
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
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<PowerSource>().ID);
                            if (amount >= speed)
                            {
                                if (outputObject.GetComponent<PowerSource>().fuelAmount < 1000)
                                {
                                    outputObject.GetComponent<PowerSource>().fuelAmount += speed;
                                }
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
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
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<Smelter>().ID);
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Smelter>().amount += speed;
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
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
                            //Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<HeatExchanger>().ID);
                            if (amount >= speed)
                            {
                                //Debug.Log(ID + " adding ice to " + outputObject.GetComponent<HeatExchanger>().ID);
                                outputObject.GetComponent<HeatExchanger>().amount += speed;
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<AlloySmelter>() != null)
                    {
                        outputID = outputObject.GetComponent<AlloySmelter>().ID;
                        if (outputObject.GetComponent<AlloySmelter>().inputObject1 == this.gameObject)
                        {
                            outputObject.GetComponent<AlloySmelter>().inputID1 = ID;
                            //Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<AlloySmelter>().ID);
                            if (amount >= speed)
                            {
                                if (type.Equals(outputObject.GetComponent<AlloySmelter>().inputType1))
                                {
                                    outputObject.GetComponent<AlloySmelter>().amount += speed;
                                    amount -= speed;
                                    conduitItem.GetComponent<ConduitItem>().active = true;
                                    GetComponent<Light>().enabled = true;
                                    GetComponent<AudioSource>().enabled = true;
                                }
                                else
                                {
                                    conduitItem.GetComponent<ConduitItem>().active = false;
                                    GetComponent<Light>().enabled = false;
                                    GetComponent<AudioSource>().enabled = false;
                                }
                            }
                        }
                        else if (outputObject.GetComponent<AlloySmelter>().inputObject2 == this.gameObject)
                        {
                            outputObject.GetComponent<AlloySmelter>().inputID2 = ID;
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<AlloySmelter>().ID);
                            if (amount >= speed)
                            {
                                if (type.Equals(outputObject.GetComponent<AlloySmelter>().inputType2))
                                {
                                    outputObject.GetComponent<AlloySmelter>().amount2 += speed;
                                    amount -= speed;
                                    conduitItem.GetComponent<ConduitItem>().active = true;
                                    GetComponent<Light>().enabled = true;
                                    GetComponent<AudioSource>().enabled = true;
                                }
                                else
                                {
                                    conduitItem.GetComponent<ConduitItem>().active = false;
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
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<Press>().ID);
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Press>().amount += speed;
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
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
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<Press>().ID);
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<Extruder>().amount += speed;
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
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
                            ////Debug.Log("CONDUIT OUTPUT SET TO: " + outputObject.GetComponent<Press>().ID);
                            if (amount >= speed)
                            {
                                outputObject.GetComponent<GearCutter>().amount += speed;
                                amount -= speed;
                                conduitItem.GetComponent<ConduitItem>().active = true;
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                            }
                        }
                        else
                        {
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                    if (outputObject.GetComponent<InventoryManager>() != null)
                    {
                        if (Vector3.Distance(transform.position, outputObject.transform.position) <= range)
                        {
                            if (type != "" && type != "nothing")
                            {
                                if (inputObject != null)
                                {
                                    if (inputMachineDisabled == false && inputObject.GetComponent<UniversalConduit>() == null)
                                    {
                                        conduitItem.GetComponent<ConduitItem>().active = true;
                                    }
                                    else if (inputObject.GetComponent<UniversalConduit>() != null)
                                    {
                                        if (inputObject.GetComponent<UniversalConduit>().inputMachineDisabled == false)
                                        {
                                            conduitItem.GetComponent<ConduitItem>().active = true;
                                        }
                                    }
                                }
                                connectionLine.enabled = true;
                                connectionLine.SetPosition(1, outputObject.transform.position);
                                GetComponent<Light>().enabled = true;
                                GetComponent<AudioSource>().enabled = true;
                                if (outputObject.GetComponent<RailCart>() != null)
                                {
                                    outputID = outputObject.GetComponent<RailCart>().ID;
                                }
                                else
                                {
                                    outputID = outputObject.GetComponent<InventoryManager>().ID;
                                }
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
                            else
                            {
                                conduitItem.GetComponent<ConduitItem>().active = false;
                                GetComponent<Light>().enabled = false;
                                GetComponent<AudioSource>().enabled = false;
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
                    if (outputObject.GetComponent<StorageComputer>() != null)
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
                                conduitItem.GetComponent<ConduitItem>().active = true;
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
                            conduitItem.GetComponent<ConduitItem>().active = false;
                            GetComponent<Light>().enabled = false;
                            GetComponent<AudioSource>().enabled = false;
                        }
                    }
                }
                else
                {
                    GetComponent<Light>().enabled = false;
                    GetComponent<AudioSource>().enabled = false;
                }
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
                conduitItem.GetComponent<ConduitItem>().active = false;
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                connectionLine.enabled = false;
            }
            if (inputObject != null && outputObject != null)
            {
                connectionAttempts = 0;
            }
        }
    }
}