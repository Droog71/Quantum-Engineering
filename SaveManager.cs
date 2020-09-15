using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveManager
{
    private StateManager stateManager;

    public SaveManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public IEnumerator SaveDataCoroutine()
    {
        stateManager.dataSaved = false;
        stateManager.saving = true;

        int saveInterval = 0;
        int objectID = 0;
        string worldID = "";
        string objectName = "";
        List<int> idList = new List<int>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.BuiltObjects.transform)
                {
                    Vector3 objectPosition = go.transform.position;
                    Quaternion objectRotation = go.transform.rotation;
                    if (go.GetComponent<Auger>() != null)
                    {
                        objectName = stateManager.WorldName + "Auger";
                        worldID = go.GetComponent<Auger>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        int speed = go.GetComponent<Auger>().speed;
                        float amount = go.GetComponent<Auger>().amount;
                        FileBasedPrefs.SetInt(worldID + "speed", speed);
                        FileBasedPrefs.SetFloat(worldID + "amount", amount);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        objectName = stateManager.WorldName + "ElectricLight";
                        worldID = go.GetComponent<ElectricLight>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        objectName = stateManager.WorldName + "DarkMatterCollector";
                        worldID = go.GetComponent<DarkMatterCollector>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        int speed = go.GetComponent<DarkMatterCollector>().speed;
                        float darkMatterAmount = go.GetComponent<DarkMatterCollector>().darkMatterAmount;
                        FileBasedPrefs.SetInt(worldID + "speed", speed);
                        FileBasedPrefs.SetFloat(worldID + "darkMatterAmount", darkMatterAmount);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "DarkMatterConduit";
                        worldID = go.GetComponent<DarkMatterConduit>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<RailCart>() != null)
                    {
                        objectName = stateManager.WorldName + "RailCart";
                        worldID = go.GetComponent<RailCart>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        string targetID = go.GetComponent<RailCart>().targetID;
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        FileBasedPrefs.SetString(worldID + "targetID", targetID);
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        objectName = stateManager.WorldName + "RailCartHub";
                        worldID = go.GetComponent<RailCartHub>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "UniversalConduit";
                        worldID = go.GetComponent<UniversalConduit>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        objectName = stateManager.WorldName + "HeatExchanger";
                        worldID = go.GetComponent<HeatExchanger>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<Retriever>() != null)
                    {
                        objectName = stateManager.WorldName + "Retriever";
                        worldID = go.GetComponent<Retriever>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        objectName = stateManager.WorldName + "AutoCrafter";
                        worldID = go.GetComponent<AutoCrafter>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<Smelter>() != null)
                    {
                        objectName = stateManager.WorldName + "Smelter";
                        worldID = go.GetComponent<Smelter>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<Turret>() != null)
                    {
                        objectName = stateManager.WorldName + "Turret";
                        worldID = go.GetComponent<Turret>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        int speed = go.GetComponent<Turret>().speed;
                        FileBasedPrefs.SetInt(worldID + "speed", speed);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerSource>() != null)
                    {
                        if (go.GetComponent<PowerSource>().type == "Solar Panel")
                        {
                            objectName = stateManager.WorldName + "SolarPanel";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Generator")
                        {
                            objectName = stateManager.WorldName + "Generator";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Reactor Turbine")
                        {
                            objectName = stateManager.WorldName + "ReactorTurbine";
                        }
                        worldID = go.GetComponent<PowerSource>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        string outputID = go.GetComponent<PowerSource>().outputID;
                        string fuelType = go.GetComponent<PowerSource>().fuelType;
                        int fuelAmount = go.GetComponent<PowerSource>().fuelAmount;
                        FileBasedPrefs.SetString(worldID + "outputID", outputID);
                        FileBasedPrefs.SetString(worldID + "fuelType", outputID);
                        FileBasedPrefs.SetInt(worldID + "fuelAmount", fuelAmount);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        objectName = stateManager.WorldName + "NuclearReactor";
                        worldID = go.GetComponent<NuclearReactor>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "PowerConduit";
                        worldID = go.GetComponent<PowerConduit>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        objectName = stateManager.WorldName + "AlloySmelter";
                        worldID = go.GetComponent<AlloySmelter>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<Press>() != null)
                    {
                        objectName = stateManager.WorldName + "Press";
                        worldID = go.GetComponent<Press>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<Extruder>() != null)
                    {
                        objectName = stateManager.WorldName + "Extruder";
                        worldID = go.GetComponent<Extruder>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        objectName = stateManager.WorldName + "GearCutter";
                        worldID = go.GetComponent<GearCutter>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        objectName = stateManager.WorldName + "UniversalExtractor";
                        worldID = go.GetComponent<UniversalExtractor>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        int speed = go.GetComponent<UniversalExtractor>().speed;
                        float amount = go.GetComponent<UniversalExtractor>().amount;
                        string type = go.GetComponent<UniversalExtractor>().type;
                        FileBasedPrefs.SetInt(worldID + "speed", speed);
                        FileBasedPrefs.SetFloat(worldID + "amount", amount);
                        FileBasedPrefs.SetString(worldID + "type", type);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (stateManager.IsStorageContainer(go))
                    {
                        objectName = stateManager.WorldName + "StorageContainer";
                        worldID = go.GetComponent<InventoryManager>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        go.GetComponent<InventoryManager>().SaveData();
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        objectName = stateManager.WorldName + "StorageComputer";
                        worldID = go.GetComponent<StorageComputer>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        objectName = stateManager.WorldName + "AirLock";
                        worldID = go.GetComponent<AirLock>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
                        FileBasedPrefs.SetBool(worldID + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(worldID + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<ModMachine>() != null)
                    {
                        objectName = stateManager.WorldName + "ModMachine";
                        worldID = go.GetComponent<ModMachine>().ID;
                        objectID = int.Parse(worldID.Substring(objectName.Length));
                        idList.Add(objectID);
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

                    FileBasedPrefs.SetString(stateManager.WorldName + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + objectID + "Rotation", objectRotation);

                    saveInterval++;
                    if (saveInterval >= 10)
                    {
                        yield return null;
                        saveInterval = 0;
                    }
                }
            }
        }

        Transform[] allTransforms = stateManager.BuiltObjects.GetComponentsInChildren<Transform>(true);
        foreach (Transform T in allTransforms)
        {
            if (T != null)
            {
                Vector3 objectPosition = T.position;
                Quaternion objectRotation = T.rotation;
                if (T.gameObject.GetComponent<IronBlock>() != null)
                {
                    if (T.gameObject.name.Equals("IronRamp(Clone)"))
                    {
                        objectName = stateManager.WorldName + "IronRamp";
                    }
                    else
                    {
                        objectName = stateManager.WorldName + "IronBlock";
                    }
                    worldID = T.gameObject.GetComponent<IronBlock>().ID;
                    objectID = int.Parse(worldID.Substring(objectName.Length));
                    idList.Add(objectID);
                    FileBasedPrefs.SetString(stateManager.WorldName + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + objectID + "Rotation", objectRotation);
                    FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                }
                if (T.gameObject.GetComponent<Steel>() != null)
                {
                    if (T.gameObject.name.Equals("SteelRamp(Clone)"))
                    {
                        objectName = stateManager.WorldName + "SteelRamp";
                    }
                    else
                    {
                        objectName = stateManager.WorldName + "Steel";
                    }
                    worldID = T.gameObject.GetComponent<Steel>().ID;
                    objectID = int.Parse(worldID.Substring(objectName.Length));
                    idList.Add(objectID);
                    FileBasedPrefs.SetString(stateManager.WorldName + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + objectID + "Rotation", objectRotation);
                    FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    objectName = stateManager.WorldName + "Brick";
                    worldID = T.gameObject.GetComponent<Brick>().ID;
                    objectID = int.Parse(worldID.Substring(objectName.Length));
                    idList.Add(objectID);
                    FileBasedPrefs.SetString(stateManager.WorldName + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + objectID + "Rotation", objectRotation);
                    FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    worldID = T.gameObject.GetComponent<Glass>().ID;
                    objectID = int.Parse(worldID.Substring(objectName.Length));
                    idList.Add(objectID);
                    FileBasedPrefs.SetString(stateManager.WorldName + objectID + "Name", objectName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + objectID + "Position", objectPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + objectID + "Rotation", objectRotation);
                    FileBasedPrefs.SetBool(worldID + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(worldID + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                }

                saveInterval++;
                if (saveInterval >= 10)
                {
                    yield return null;
                    saveInterval = 0;
                }
            }
        }

        if (idList.Count > 0)
        {
            PlayerPrefsX.SetIntArray(stateManager.WorldName + "idList", idList.ToArray());
        }

        FileBasedPrefs.ManuallySave();
        stateManager.dataSaved = true;
        stateManager.saving = false;
    }
}

