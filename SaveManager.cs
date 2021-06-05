using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveManager
{
    private StateManager stateManager;
    public int currentObject;
    public int totalObjects;

    //! This class handles world saving.
    public SaveManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    //! Saves the world.
    public IEnumerator SaveDataCoroutine()
    {
        stateManager.dataSaved = false;
        stateManager.saving = true;
        currentObject = 0;
        int saveInterval = 0;
        int objectID = 0;
        string worldID = "";
        string objectName = "";
        List<int> machineIdList = new List<int>();
        List<int> blockIdList = new List<int>();

        GameObject[] machines = GameObject.FindGameObjectsWithTag("Machine");
        Transform[] blocks = stateManager.builtObjects.GetComponentsInChildren<Transform>(true);
        MeshPainter[] meshPainters = Object.FindObjectsOfType<MeshPainter>();

        if (totalObjects == 0)
        {
            totalObjects = machines.Length + blocks.Length + meshPainters.Length;
        }

        foreach (GameObject go in machines)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.builtObjects.transform)
                {
                    Vector3 objectPosition = go.transform.position;
                    Quaternion objectRotation = go.transform.rotation;
                    if (go.GetComponent<Auger>() != null)
                    {
                        objectName = stateManager.worldName + "Auger";
                        worldID = go.GetComponent<Auger>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<Auger>().speed;
                            float amount = go.GetComponent<Auger>().amount;
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        objectName = stateManager.worldName + "ElectricLight";
                        worldID = go.GetComponent<ElectricLight>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        objectName = stateManager.worldName + "DarkMatterCollector";
                        worldID = go.GetComponent<DarkMatterCollector>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<DarkMatterCollector>().speed;
                            float darkMatterAmount = go.GetComponent<DarkMatterCollector>().darkMatterAmount;
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "darkMatterAmount", darkMatterAmount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        objectName = stateManager.worldName + "DarkMatterConduit";
                        worldID = go.GetComponent<DarkMatterConduit>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<DarkMatterConduit>().inputID;
                            string outputID = go.GetComponent<DarkMatterConduit>().outputID;
                            int speed = go.GetComponent<DarkMatterConduit>().speed;
                            float darkMatterAmount = go.GetComponent<DarkMatterConduit>().darkMatterAmount;
                            int range = go.GetComponent<DarkMatterConduit>().range;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetInt(worldID + "range", range);
                            FileBasedPrefs.SetFloat(worldID + "darkMatterAmount", darkMatterAmount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        objectName = stateManager.worldName + "RailCart";
                        worldID = go.GetComponent<RailCart>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string targetID = go.GetComponent<RailCart>().targetID;
                            Vector3 startPosition = go.GetComponent<RailCart>().startPosition;
                            if (go.GetComponent<InventoryManager>() != null)
                            {
                                go.GetComponent<InventoryManager>().SaveData();
                            }
                            FileBasedPrefs.SetString(worldID + "targetID", targetID);
                            PlayerPrefsX.SetVector3(worldID + "startPosition", startPosition);
                        }
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        objectName = stateManager.worldName + "RailCartHub";
                        worldID = go.GetComponent<RailCartHub>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<RailCartHub>().inputID;
                            string outputID = go.GetComponent<RailCartHub>().outputID;
                            int range = go.GetComponent<RailCartHub>().range;
                            bool centralHub = go.GetComponent<RailCartHub>().centralHub;
                            bool stop = go.GetComponent<RailCartHub>().stop;
                            int circuit = go.GetComponent<RailCartHub>().circuit;
                            float stopTime = go.GetComponent<RailCartHub>().stopTime;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "range", range);
                            FileBasedPrefs.SetInt(worldID + "circuit", circuit);
                            FileBasedPrefs.SetBool(worldID + "centralHub", centralHub);
                            FileBasedPrefs.SetBool(worldID + "stop", stop);
                            FileBasedPrefs.SetFloat(worldID + "stopTime", stopTime);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        objectName = stateManager.worldName + "UniversalConduit";
                        worldID = go.GetComponent<UniversalConduit>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<UniversalConduit>().inputID;
                            string outputID = go.GetComponent<UniversalConduit>().outputID;
                            string type = go.GetComponent<UniversalConduit>().type;
                            int speed = go.GetComponent<UniversalConduit>().speed;
                            int range = go.GetComponent<UniversalConduit>().range;
                            float amount = go.GetComponent<UniversalConduit>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetString(worldID + "type", type);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetInt(worldID + "range", range);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        objectName = stateManager.worldName + "HeatExchanger";
                        worldID = go.GetComponent<HeatExchanger>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<HeatExchanger>().inputID;
                            string outputID = go.GetComponent<HeatExchanger>().outputID;
                            string inputType = go.GetComponent<HeatExchanger>().inputType;
                            int speed = go.GetComponent<HeatExchanger>().speed;
                            float amount = go.GetComponent<HeatExchanger>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        objectName = stateManager.worldName + "Retriever";
                        worldID = go.GetComponent<Retriever>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<Retriever>().inputID;
                            string outputID = go.GetComponent<Retriever>().outputID;
                            int speed = go.GetComponent<Retriever>().speed;
                            float amount = go.GetComponent<Retriever>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                            if (go.GetComponent<InventoryManager>() != null)
                            {
                                go.GetComponent<InventoryManager>().SaveData();
                            }
                        }
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        objectName = stateManager.worldName + "AutoCrafter";
                        worldID = go.GetComponent<AutoCrafter>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<AutoCrafter>().inputID;
                            int speed = go.GetComponent<AutoCrafter>().speed;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                            if (go.GetComponent<InventoryManager>() != null)
                            {
                                go.GetComponent<InventoryManager>().SaveData();
                            }
                        }
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        objectName = stateManager.worldName + "Smelter";
                        worldID = go.GetComponent<Smelter>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<Smelter>().inputID;
                            string outputID = go.GetComponent<Smelter>().outputID;
                            string inputType = go.GetComponent<Smelter>().inputType;
                            string outputType = go.GetComponent<Smelter>().outputType;
                            int speed = go.GetComponent<Smelter>().speed;
                            float amount = go.GetComponent<Smelter>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        objectName = stateManager.worldName + "Turret";
                        worldID = go.GetComponent<Turret>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<Turret>().speed;
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<MissileTurret>() != null)
                    {
                        objectName = stateManager.worldName + "MissileTurret";
                        worldID = go.GetComponent<MissileTurret>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<MissileTurret>().speed;
                            string ammoType = go.GetComponent<MissileTurret>().ammoType;
                            int ammoAmount = go.GetComponent<MissileTurret>().ammoAmount;
                            FileBasedPrefs.SetString(worldID + "ammoType", ammoType);
                            FileBasedPrefs.SetInt(worldID + "ammoAmount", ammoAmount);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<PowerSource>() != null)
                    {
                        if (go.GetComponent<PowerSource>().type == "Solar Panel")
                        {
                            objectName = stateManager.worldName + "SolarPanel";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Generator")
                        {
                            objectName = stateManager.worldName + "Generator";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Reactor Turbine")
                        {
                            objectName = stateManager.worldName + "ReactorTurbine";
                        }
                        worldID = go.GetComponent<PowerSource>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string outputID = go.GetComponent<PowerSource>().outputID;
                            string fuelType = go.GetComponent<PowerSource>().fuelType;
                            int fuelAmount = go.GetComponent<PowerSource>().fuelAmount;
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetString(worldID + "fuelType", fuelType);
                            FileBasedPrefs.SetInt(worldID + "fuelAmount", fuelAmount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        objectName = stateManager.worldName + "NuclearReactor";
                        worldID = go.GetComponent<NuclearReactor>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        objectName = stateManager.worldName + "PowerConduit";
                        worldID = go.GetComponent<PowerConduit>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<PowerConduit>().inputID;
                            string outputID1 = go.GetComponent<PowerConduit>().outputID1;
                            string outputID2 = go.GetComponent<PowerConduit>().outputID2;
                            bool dualOutput = go.GetComponent<PowerConduit>().dualOutput;
                            int range = go.GetComponent<PowerConduit>().range;
                            int powerAmount = go.GetComponent<PowerConduit>().powerAmount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID1", outputID1);
                            FileBasedPrefs.SetString(worldID + "outputID2", outputID2);
                            FileBasedPrefs.SetBool(worldID + "dualOutput", dualOutput);
                            FileBasedPrefs.SetInt(worldID + "range", range);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        objectName = stateManager.worldName + "AlloySmelter";
                        worldID = go.GetComponent<AlloySmelter>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID1 = go.GetComponent<AlloySmelter>().inputID1;
                            string inputID2 = go.GetComponent<AlloySmelter>().inputID2;
                            string inputType1 = go.GetComponent<AlloySmelter>().inputType1;
                            string inputType2 = go.GetComponent<AlloySmelter>().inputType2;
                            string outputType = go.GetComponent<AlloySmelter>().outputType;
                            string outputID = go.GetComponent<AlloySmelter>().outputID;
                            int speed = go.GetComponent<AlloySmelter>().speed;
                            float amount = go.GetComponent<AlloySmelter>().amount;
                            float amount2 = go.GetComponent<AlloySmelter>().amount2;
                            float outputAmount = go.GetComponent<AlloySmelter>().outputAmount;
                            FileBasedPrefs.SetString(worldID + "inputID1", inputID1);
                            FileBasedPrefs.SetString(worldID + "inputID2", inputID2);
                            FileBasedPrefs.SetString(worldID + "inputType1", inputType1);
                            FileBasedPrefs.SetString(worldID + "inputType2", inputType2);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetFloat(worldID + "amount2", amount2);
                            FileBasedPrefs.SetFloat(worldID + "amount", outputAmount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        objectName = stateManager.worldName + "Press";
                        worldID = go.GetComponent<Press>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<Press>().inputID;
                            string inputType = go.GetComponent<Press>().inputType;
                            string outputType = go.GetComponent<Press>().outputType;
                            string outputID = go.GetComponent<Press>().outputID;
                            int speed = go.GetComponent<Press>().speed;
                            float amount = go.GetComponent<Press>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        objectName = stateManager.worldName + "Extruder";
                        worldID = go.GetComponent<Extruder>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<Extruder>().inputID;
                            string inputType = go.GetComponent<Extruder>().inputType;
                            string outputType = go.GetComponent<Extruder>().outputType;
                            string outputID = go.GetComponent<Extruder>().outputID;
                            int speed = go.GetComponent<Extruder>().speed;
                            float amount = go.GetComponent<Extruder>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        objectName = stateManager.worldName + "GearCutter";
                        worldID = go.GetComponent<GearCutter>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            string inputID = go.GetComponent<GearCutter>().inputID;
                            string inputType = go.GetComponent<GearCutter>().inputType;
                            string outputType = go.GetComponent<GearCutter>().outputType;
                            string outputID = go.GetComponent<GearCutter>().outputID;
                            int speed = go.GetComponent<GearCutter>().speed;
                            float amount = go.GetComponent<GearCutter>().amount;
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        objectName = stateManager.worldName + "UniversalExtractor";
                        worldID = go.GetComponent<UniversalExtractor>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<UniversalExtractor>().speed;
                            float amount = go.GetComponent<UniversalExtractor>().amount;
                            string type = go.GetComponent<UniversalExtractor>().type;
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetString(worldID + "type", type);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (stateManager.IsStorageContainer(go))
                    {
                        objectName = stateManager.worldName + "StorageContainer";
                        worldID = go.GetComponent<InventoryManager>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            go.GetComponent<InventoryManager>().SaveData();
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        objectName = stateManager.worldName + "StorageComputer";
                        worldID = go.GetComponent<StorageComputer>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<Door>() != null)
                    {
                        if (go.GetComponent<Door>().type == "Door")
                        {
                            objectName = stateManager.worldName + "Door";
                        }
                        else if (go.GetComponent<Door>().type == "Quantum Hatchway")
                        {
                            Debug.Log("Saving hatch");
                            objectName = stateManager.worldName + "QuantumHatchway";
                        }
                        worldID = go.GetComponent<Door>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int audioClip = go.GetComponent<Door>().audioClip;
                            int texture = go.GetComponent<Door>().textureIndex;
                            string material = go.GetComponent<Door>().material;
                            bool edited = go.GetComponent<Door>().edited;
                            FileBasedPrefs.SetInt(worldID + "audioClip", audioClip);
                            FileBasedPrefs.SetInt(worldID + "texture", texture);
                            FileBasedPrefs.SetString(worldID + "material", material);
                            FileBasedPrefs.SetBool(worldID + "edited", edited);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<ModMachine>() != null)
                    {
                        objectName = stateManager.worldName + "ModMachine";
                        worldID = go.GetComponent<ModMachine>().ID;
                        if (worldID != "unassigned" && worldID != "")
                        {
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                            int speed = go.GetComponent<ModMachine>().speed;
                            float amount = go.GetComponent<ModMachine>().amount;
                            string machineName = go.GetComponent<ModMachine>().machineName;
                            string inputID = go.GetComponent<ModMachine>().inputID;
                            string outputID = go.GetComponent<ModMachine>().outputID;
                            string inputType = go.GetComponent<ModMachine>().inputType;
                            string outputType = go.GetComponent<ModMachine>().outputType;
                            FileBasedPrefs.SetString(worldID + "machineName", machineName);
                            FileBasedPrefs.SetString(worldID + "inputID", inputID);
                            FileBasedPrefs.SetString(worldID + "outputID", outputID);
                            FileBasedPrefs.SetString(worldID + "inputType", inputType);
                            FileBasedPrefs.SetString(worldID + "outputType", outputType);
                            FileBasedPrefs.SetInt(worldID + "speed", speed);
                            FileBasedPrefs.SetFloat(worldID + "amount", amount);
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        }
                    }
                    if (go.GetComponent<ProtectionBlock>() != null)
                    {
                        worldID = go.GetComponent<ProtectionBlock>().ID;
                        List<string> userNames = go.GetComponent<ProtectionBlock>().GetUserNames();
                        if (worldID != "unassigned" && worldID != "" && userNames != null)
                        {
                            PlayerPrefsX.SetStringArray(worldID + "userNames", userNames.ToArray());
                            FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                            FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                            objectName = stateManager.worldName + "ProtectionBlock";
                            objectID = int.Parse(worldID.Substring(objectName.Length));
                            machineIdList.Add(objectID);
                        }
                    }

                    FileBasedPrefs.SetString(stateManager.worldName + "machine" + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.worldName + "machine" + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.worldName + "machine" + objectID + "Rotation", objectRotation);

                    currentObject++;
                    saveInterval++;
                    if (saveInterval >= totalObjects * 0.05f)
                    {
                        yield return null;
                        saveInterval = 0;
                    }
                }
            }
        }

        foreach (Transform T in blocks)
        {
            if (T != null)
            {
                Vector3 objectPosition = T.position;
                Quaternion objectRotation = T.rotation;
                if (T.gameObject.GetComponent<IronBlock>() != null)
                {
                    if (T.gameObject.name.Equals("IronRamp(Clone)"))
                    {
                        objectName = stateManager.worldName + "IronRamp";
                    }
                    else
                    {
                        objectName = stateManager.worldName + "IronBlock";
                    }
                    worldID = T.gameObject.GetComponent<IronBlock>().ID;
                    if (worldID != "unassigned" && worldID != "")
                    {
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        blockIdList.Add(objectID);
                        FileBasedPrefs.SetString(stateManager.worldName + "block" + objectID + "Name", objectName);
                        PlayerPrefsX.SetVector3(stateManager.worldName + "block" + objectID + "Position", objectPosition);
                        PlayerPrefsX.SetQuaternion(stateManager.worldName + "block" + objectID + "Rotation", objectRotation);
                        FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    }
                }
                if (T.gameObject.GetComponent<Steel>() != null)
                {
                    if (T.gameObject.name.Equals("SteelRamp(Clone)"))
                    {
                        objectName = stateManager.worldName + "SteelRamp";
                    }
                    else
                    {
                        objectName = stateManager.worldName + "Steel";
                    }
                    worldID = T.gameObject.GetComponent<Steel>().ID;
                    if (worldID != "unassigned" && worldID != "")
                    {
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        blockIdList.Add(objectID);
                        FileBasedPrefs.SetString(stateManager.worldName + "block" + objectID + "Name", objectName);
                        PlayerPrefsX.SetVector3(stateManager.worldName + "block" + objectID + "Position", objectPosition);
                        PlayerPrefsX.SetQuaternion(stateManager.worldName + "block" + objectID + "Rotation", objectRotation);
                        FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    }
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    objectName = stateManager.worldName + "Brick";
                    worldID = T.gameObject.GetComponent<Brick>().ID;
                    if (worldID != "unassigned" && worldID != "")
                    {
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        blockIdList.Add(objectID);
                        FileBasedPrefs.SetString(stateManager.worldName + "block" + objectID + "Name", objectName);
                        PlayerPrefsX.SetVector3(stateManager.worldName + "block" + objectID + "Position", objectPosition);
                        PlayerPrefsX.SetQuaternion(stateManager.worldName + "block" + objectID + "Rotation", objectRotation);
                        FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    }
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    objectName = stateManager.worldName + "Glass";
                    worldID = T.gameObject.GetComponent<Glass>().ID;
                    if (worldID != "unassigned" && worldID != "")
                    {
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        blockIdList.Add(objectID);
                        FileBasedPrefs.SetString(stateManager.worldName + "block" + objectID + "Name", objectName);
                        PlayerPrefsX.SetVector3(stateManager.worldName + "block" + objectID + "Position", objectPosition);
                        PlayerPrefsX.SetQuaternion(stateManager.worldName + "block" + objectID + "Rotation", objectRotation);
                        FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    }
                }
                if (T.gameObject.GetComponent<ModBlock>() != null)
                {
                    objectName = stateManager.worldName + "ModBlock";
                    worldID = T.gameObject.GetComponent<ModBlock>().ID;
                    if (worldID != "unassigned" && worldID != "")
                    {
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        blockIdList.Add(objectID);
                        string blockName = T.gameObject.GetComponent<ModBlock>().blockName;
                        FileBasedPrefs.SetString(worldID + "blockName", blockName);
                        FileBasedPrefs.SetString(stateManager.worldName + "block" + objectID + "Name", objectName);
                        PlayerPrefsX.SetVector3(stateManager.worldName + "block" + objectID + "Position", objectPosition);
                        PlayerPrefsX.SetQuaternion(stateManager.worldName + "block" + objectID + "Rotation", objectRotation);
                        FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    }
                }

                currentObject++;
                saveInterval++;
                if (saveInterval >= totalObjects * 0.05f)
                {
                    yield return null;
                    saveInterval = 0;
                }
            }
        }

        foreach (MeshPainter painter in meshPainters)
        {
            painter.SaveData();
            currentObject++;
            saveInterval++;
            if (saveInterval >= totalObjects * 0.05f)
            {
                yield return null;
                saveInterval = 0;
            }
        }

        if (machineIdList.Count > 0)
        {
            PlayerPrefsX.SetIntArray(stateManager.worldName + "machineIdList", machineIdList.ToArray());
        }

        if (blockIdList.Count > 0)
        {
            PlayerPrefsX.SetIntArray(stateManager.worldName + "blockIdList", blockIdList.ToArray());
        }

        FileBasedPrefs.ManuallySave();
        stateManager.dataSaved = true;
        stateManager.saving = false;
        currentObject = 0;
        totalObjects = 0;
    }
}