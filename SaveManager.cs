using UnityEngine;
using System.Collections;

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
        stateManager.ConstructionCount = 0;
        int saveInterval = 0;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.BuiltObjects.transform)
                {
                    stateManager.PartPosition = go.transform.position;
                    stateManager.PartRotation = go.transform.rotation;
                    stateManager.PartNumber = stateManager.ConstructionCount.ToString();
                    if (go.GetComponent<Auger>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Auger";
                        go.GetComponent<Auger>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<Auger>().address = stateManager.ConstructionCount;
                        int speed = go.GetComponent<Auger>().speed;
                        float amount = go.GetComponent<Auger>().amount;
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<ElectricLight>().address = stateManager.ConstructionCount;
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<DarkMatterCollector>().address = stateManager.ConstructionCount;
                        int speed = go.GetComponent<DarkMatterCollector>().speed;
                        float darkMatterAmount = go.GetComponent<DarkMatterCollector>().darkMatterAmount;
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "darkMatterAmount", darkMatterAmount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().address = stateManager.ConstructionCount;
                        go.GetComponent<DarkMatterConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<DarkMatterConduit>().inputID;
                        string outputID = go.GetComponent<DarkMatterConduit>().outputID;
                        int speed = go.GetComponent<DarkMatterConduit>().speed;
                        float darkMatterAmount = go.GetComponent<DarkMatterConduit>().darkMatterAmount;
                        int range = go.GetComponent<DarkMatterConduit>().range;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "range", range);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "darkMatterAmount", darkMatterAmount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "RailCart";
                        go.GetComponent<RailCart>().address = stateManager.ConstructionCount;
                        go.GetComponent<RailCart>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string targetID = go.GetComponent<RailCart>().targetID;
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "targetID", targetID);
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().address = stateManager.ConstructionCount;
                        go.GetComponent<RailCartHub>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<RailCartHub>().inputID;
                        string outputID = go.GetComponent<RailCartHub>().outputID;
                        int range = go.GetComponent<RailCartHub>().range;
                        bool centralHub = go.GetComponent<RailCartHub>().centralHub;
                        bool stop = go.GetComponent<RailCartHub>().stop;
                        int circuit = go.GetComponent<RailCartHub>().circuit;
                        float stopTime = go.GetComponent<RailCartHub>().stopTime;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "range", range);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "circuit", circuit);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "centralHub", centralHub);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "stop", stop);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "stopTime", stopTime);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().address = stateManager.ConstructionCount;
                        go.GetComponent<UniversalConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<UniversalConduit>().inputID;
                        string outputID = go.GetComponent<UniversalConduit>().outputID;
                        string type = go.GetComponent<UniversalConduit>().type;
                        int speed = go.GetComponent<UniversalConduit>().speed;
                        int range = go.GetComponent<UniversalConduit>().range;
                        float amount = go.GetComponent<UniversalConduit>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "type", type);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "range", range);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().address = stateManager.ConstructionCount;
                        go.GetComponent<HeatExchanger>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<HeatExchanger>().inputID;
                        string outputID = go.GetComponent<HeatExchanger>().outputID;
                        string inputType = go.GetComponent<HeatExchanger>().inputType;
                        int speed = go.GetComponent<HeatExchanger>().speed;
                        float amount = go.GetComponent<HeatExchanger>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Retriever";
                        go.GetComponent<Retriever>().address = stateManager.ConstructionCount;
                        go.GetComponent<Retriever>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<Retriever>().inputID;
                        string outputID = go.GetComponent<Retriever>().outputID;
                        int speed = go.GetComponent<Retriever>().speed;
                        float amount = go.GetComponent<Retriever>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().address = stateManager.ConstructionCount;
                        go.GetComponent<AutoCrafter>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<AutoCrafter>().inputID;
                        int speed = go.GetComponent<AutoCrafter>().speed;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Smelter";
                        go.GetComponent<Smelter>().address = stateManager.ConstructionCount;
                        go.GetComponent<Smelter>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<Smelter>().inputID;
                        string outputID = go.GetComponent<Smelter>().outputID;
                        string inputType = go.GetComponent<Smelter>().inputType;
                        string outputType = go.GetComponent<Smelter>().outputType;
                        int speed = go.GetComponent<Smelter>().speed;
                        float amount = go.GetComponent<Smelter>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Turret";
                        go.GetComponent<Turret>().address = stateManager.ConstructionCount;
                        go.GetComponent<Turret>().ID = (stateManager.PartName + stateManager.PartNumber);
                        int speed = go.GetComponent<Turret>().speed;
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerSource>() != null)
                    {
                        if (go.GetComponent<PowerSource>().type == "Solar Panel")
                        {
                            stateManager.PartName = stateManager.WorldName + "SolarPanel";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Generator")
                        {
                            stateManager.PartName = stateManager.WorldName + "Generator";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Reactor Turbine")
                        {
                            stateManager.PartName = stateManager.WorldName + "ReactorTurbine";
                        }
                        go.GetComponent<PowerSource>().address = stateManager.ConstructionCount;
                        go.GetComponent<PowerSource>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string outputID = go.GetComponent<PowerSource>().outputID;
                        string fuelType = go.GetComponent<PowerSource>().fuelType;
                        int fuelAmount = go.GetComponent<PowerSource>().fuelAmount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "fuelType", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "fuelAmount", fuelAmount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().address = stateManager.ConstructionCount;
                        go.GetComponent<NuclearReactor>().ID = (stateManager.PartName + stateManager.PartNumber);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().address = stateManager.ConstructionCount;
                        go.GetComponent<PowerConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<PowerConduit>().inputID;
                        string outputID1 = go.GetComponent<PowerConduit>().outputID1;
                        string outputID2 = go.GetComponent<PowerConduit>().outputID2;
                        bool dualOutput = go.GetComponent<PowerConduit>().dualOutput;
                        int range = go.GetComponent<PowerConduit>().range;
                        int powerAmount = go.GetComponent<PowerConduit>().powerAmount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID1", outputID1);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID2", outputID2);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "dualOutput", dualOutput);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "range", range);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().address = stateManager.ConstructionCount;
                        go.GetComponent<AlloySmelter>().ID = (stateManager.PartName + stateManager.PartNumber);

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
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID1", inputID1);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID2", inputID2);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType1", inputType1);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType2", inputType2);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount2", amount2);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", outputAmount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Press";
                        go.GetComponent<Press>().address = stateManager.ConstructionCount;
                        go.GetComponent<Press>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<Press>().inputID;
                        string inputType = go.GetComponent<Press>().inputType;
                        string outputType = go.GetComponent<Press>().outputType;
                        string outputID = go.GetComponent<Press>().outputID;
                        int speed = go.GetComponent<Press>().speed;
                        float amount = go.GetComponent<Press>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Extruder";
                        go.GetComponent<Extruder>().address = stateManager.ConstructionCount;
                        go.GetComponent<Extruder>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<Extruder>().inputID;
                        string inputType = go.GetComponent<Extruder>().inputType;
                        string outputType = go.GetComponent<Extruder>().outputType;
                        string outputID = go.GetComponent<Extruder>().outputID;
                        int speed = go.GetComponent<Extruder>().speed;
                        float amount = go.GetComponent<Extruder>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "GearCutter";
                        go.GetComponent<GearCutter>().address = stateManager.ConstructionCount;
                        go.GetComponent<GearCutter>().ID = (stateManager.PartName + stateManager.PartNumber);
                        string inputID = go.GetComponent<GearCutter>().inputID;
                        string inputType = go.GetComponent<GearCutter>().inputType;
                        string outputType = go.GetComponent<GearCutter>().outputType;
                        string outputID = go.GetComponent<GearCutter>().outputID;
                        int speed = go.GetComponent<GearCutter>().speed;
                        float amount = go.GetComponent<GearCutter>().amount;
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().address = stateManager.ConstructionCount;
                        go.GetComponent<UniversalExtractor>().ID = (stateManager.PartName + stateManager.PartNumber);
                        int speed = go.GetComponent<UniversalExtractor>().speed;
                        float amount = go.GetComponent<UniversalExtractor>().amount;
                        string type = go.GetComponent<UniversalExtractor>().type;
                        FileBasedPrefs.SetInt(stateManager.PartName + stateManager.PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(stateManager.PartName + stateManager.PartNumber + "amount", amount);
                        FileBasedPrefs.SetString(stateManager.PartName + stateManager.PartNumber + "type", type);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<InventoryManager>() != null && go.GetComponent<RailCart>() == null && go.GetComponent<PlayerController>() == null && go.GetComponent<Retriever>() == null && go.GetComponent<AutoCrafter>() == null)
                    {
                        stateManager.PartName = stateManager.WorldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<InventoryManager>().address = stateManager.ConstructionCount;
                        go.GetComponent<InventoryManager>().SaveData();
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().address = stateManager.ConstructionCount;
                        go.GetComponent<StorageComputer>().ID = (stateManager.PartName + stateManager.PartNumber);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AirLock";
                        go.GetComponent<AirLock>().address = stateManager.ConstructionCount;
                        go.GetComponent<AirLock>().ID = (stateManager.PartName + stateManager.PartNumber);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }

                    FileBasedPrefs.SetString(stateManager.WorldName + stateManager.PartNumber + "Name", stateManager.PartName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + stateManager.PartNumber + "Position", stateManager.PartPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + stateManager.PartNumber + "Rotation", stateManager.PartRotation);

                    stateManager.ConstructionCount++;

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
                stateManager.PartPosition = T.position;
                stateManager.PartRotation = T.rotation;
                stateManager.PartNumber = stateManager.ConstructionCount.ToString();
                if (T.gameObject.GetComponent<IronBlock>() != null)
                {
                    if (T.gameObject.name.Equals("IronRamp(Clone)"))
                    {
                        stateManager.PartName = stateManager.WorldName + "IronRamp";
                    }
                    else
                    {
                        stateManager.PartName = stateManager.WorldName + "IronBlock";
                    }
                    T.gameObject.GetComponent<IronBlock>().ID = (stateManager.PartName + stateManager.PartNumber);
                    T.gameObject.GetComponent<IronBlock>().address = stateManager.ConstructionCount;
                    FileBasedPrefs.SetString(stateManager.WorldName + stateManager.PartNumber + "Name", stateManager.PartName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + stateManager.PartNumber + "Position", stateManager.PartPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + stateManager.PartNumber + "Rotation", stateManager.PartRotation);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    stateManager.ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Steel>() != null)
                {
                    if (T.gameObject.name.Equals("SteelRamp(Clone)"))
                    {
                        stateManager.PartName = stateManager.WorldName + "SteelRamp";
                    }
                    else
                    {
                        stateManager.PartName = stateManager.WorldName + "Steel";
                    }
                    T.gameObject.GetComponent<Steel>().ID = (stateManager.PartName + stateManager.PartNumber);
                    T.gameObject.GetComponent<Steel>().address = stateManager.ConstructionCount;
                    FileBasedPrefs.SetString(stateManager.WorldName + stateManager.PartNumber + "Name", stateManager.PartName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + stateManager.PartNumber + "Position", stateManager.PartPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + stateManager.PartNumber + "Rotation", stateManager.PartRotation);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    stateManager.ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    stateManager.PartName = stateManager.WorldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = (stateManager.PartName + stateManager.PartNumber);
                    T.gameObject.GetComponent<Brick>().address = stateManager.ConstructionCount;
                    FileBasedPrefs.SetString(stateManager.WorldName + stateManager.PartNumber + "Name", stateManager.PartName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + stateManager.PartNumber + "Position", stateManager.PartPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + stateManager.PartNumber + "Rotation", stateManager.PartRotation);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    stateManager.ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    stateManager.PartName = stateManager.WorldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = (stateManager.PartName + stateManager.PartNumber);
                    T.gameObject.GetComponent<Glass>().address = stateManager.ConstructionCount;
                    FileBasedPrefs.SetString(stateManager.WorldName + stateManager.PartNumber + "Name", stateManager.PartName);
                    PlayerPrefsX.SetVector3(stateManager.WorldName + stateManager.PartNumber + "Position", stateManager.PartPosition);
                    PlayerPrefsX.SetQuaternion(stateManager.WorldName + stateManager.PartNumber + "Rotation", stateManager.PartRotation);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(stateManager.PartName + stateManager.PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    stateManager.ConstructionCount++;
                }

                saveInterval++;
                if (saveInterval >= 10)
                {
                    yield return null;
                    saveInterval = 0;
                }
            }
        }

        if (stateManager.ConstructionCount != 0)
        {
            FileBasedPrefs.SetInt(stateManager.WorldName + "ConstructionTotal", stateManager.ConstructionCount);
        }

        FileBasedPrefs.ManuallySave();
        stateManager.dataSaved = true;
        stateManager.saving = false;
    }
}

