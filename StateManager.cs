﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StateManager : MonoBehaviour
{
    //BUILDING
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded = false;
    public GameObject DarkMatterCollector;
    public GameObject DarkMatterConduit;
    public GameObject IronBlock;
    public GameObject IronRamp;
    public GameObject Steel;
    public GameObject SteelRamp;
    public GameObject StorageContainer;
    public GameObject Glass;
    public GameObject UniversalExtractor;
    public GameObject UniversalConduit;
    public GameObject PowerConduit;
    public GameObject Smelter;
    public GameObject Press;
    public GameObject GearCutter;
    public GameObject AlloySmelter;
    public GameObject SolarPanel;
    public GameObject Generator;
    public GameObject Extruder;
    public GameObject Turret;
    public GameObject Auger;
    public GameObject HeatExchanger;
    public GameObject ElectricLight;
    public GameObject Retriever;
    public GameObject AirLock;
    public GameObject NuclearReactor;
    public GameObject ReactorTurbine;
    public GameObject StorageComputer;
    public GameObject AutoCrafter;
    public GameObject RailCartHub;
    public GameObject RailCart;
    public GameObject Brick;
    public GameObject BuiltObjects;
    public string WorldName = "World";
    private int ConstructionCount = 0;
    private string PartNumber = "";
    private string PartName = "";
    private string ObjectName = "";
    private Vector3 EmptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 PartPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 ObjectPrintingLocation;
    private Quaternion ObjectPrintingRotation;
    private Quaternion PartRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    private Coroutine saveCoroutine;
    public bool Loaded = false;

    void LoadWorld()
    {
        if (Loaded == false && FileBasedPrefs.GetInt(WorldName + "ConstructionTotal") != 0)
        {
            ConstructionCount = 0;
            do
            {
                PartNumber = ConstructionCount.ToString();
                ObjectPrintingLocation = PlayerPrefsX.GetVector3(WorldName + PartNumber + "Position");
                ObjectPrintingRotation = PlayerPrefsX.GetQuaternion(WorldName + PartNumber + "Rotation");
                ObjectName = FileBasedPrefs.GetString(WorldName + PartNumber + "Name");
                if (ObjectName != "" && ObjectPrintingLocation != EmptyVector)
                {
                    if (ObjectName == WorldName + "AirLock")
                    {
                        GameObject PrintedPart = Instantiate(AirLock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterCollector")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterCollector, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "darkMatterAmount");
                        PrintedPart.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "DarkMatterConduit")
                    {
                        GameObject PrintedPart = Instantiate(DarkMatterConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "darkMatterAmount");
                        PrintedPart.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCartHub")
                    {
                        GameObject PrintedPart = Instantiate(RailCartHub, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(ObjectName + PartNumber + "stop");
                        PrintedPart.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(ObjectName + PartNumber + "circuit");
                        PrintedPart.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "stopTime");
                        PrintedPart.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(ObjectName + PartNumber + "centralHub");
                        PrintedPart.GetComponent<RailCartHub>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "RailCart")
                    {
                        GameObject PrintedPart = Instantiate(RailCart, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<RailCart>().creationMethod = "spawned";
                        PrintedPart.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(ObjectName + PartNumber + "targetID");
                    }
                    if (ObjectName == WorldName + "UniversalConduit")
                    {
                        GameObject PrintedPart = Instantiate(UniversalConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(ObjectName + PartNumber + "type");
                        PrintedPart.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<UniversalConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Retriever")
                    {
                        GameObject PrintedPart = Instantiate(Retriever, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Retriever>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AutoCrafter")
                    {
                        GameObject PrintedPart = Instantiate(AutoCrafter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AutoCrafter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Smelter")
                    {
                        GameObject PrintedPart = Instantiate(Smelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Smelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "HeatExchanger")
                    {
                        GameObject PrintedPart = Instantiate(HeatExchanger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<HeatExchanger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SolarPanel")
                    {
                        GameObject PrintedPart = Instantiate(SolarPanel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Generator")
                    {
                        GameObject PrintedPart = Instantiate(Generator, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(ObjectName + PartNumber + "fuelType");
                        PrintedPart.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(ObjectName + PartNumber + "fuelAmount");
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "NuclearReactor")
                    {
                        GameObject PrintedPart = Instantiate(NuclearReactor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<NuclearReactor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ReactorTurbine")
                    {
                        GameObject PrintedPart = Instantiate(ReactorTurbine, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<PowerSource>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "PowerConduit")
                    {
                        GameObject PrintedPart = Instantiate(PowerConduit, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID1");
                        PrintedPart.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID2");
                        PrintedPart.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(ObjectName + PartNumber + "dualOutput");
                        PrintedPart.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(ObjectName + PartNumber + "range");
                        PrintedPart.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(ObjectName + PartNumber + "powerAmount");
                        PrintedPart.GetComponent<PowerConduit>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Auger")
                    {
                        GameObject PrintedPart = Instantiate(Auger, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Auger>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "ElectricLight")
                    {
                        GameObject PrintedPart = Instantiate(ElectricLight, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<ElectricLight>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Turret")
                    {
                        GameObject PrintedPart = Instantiate(Turret, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Turret>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "AlloySmelter")
                    {
                        GameObject PrintedPart = Instantiate(AlloySmelter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID1");
                        PrintedPart.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID2");
                        PrintedPart.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType1");
                        PrintedPart.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType2");
                        PrintedPart.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount2");
                        PrintedPart.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "outputAmount");
                        PrintedPart.GetComponent<AlloySmelter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Press")
                    {
                        GameObject PrintedPart = Instantiate(Press, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Press>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Press>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Press>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Press>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Press>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Press>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Extruder")
                    {
                        GameObject PrintedPart = Instantiate(Extruder, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<Extruder>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "GearCutter")
                    {
                        GameObject PrintedPart = Instantiate(GearCutter, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputID");
                        PrintedPart.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputID");
                        PrintedPart.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "inputType");
                        PrintedPart.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(ObjectName + PartNumber + "outputType");
                        PrintedPart.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<GearCutter>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "UniversalExtractor")
                    {
                        GameObject PrintedPart = Instantiate(UniversalExtractor, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(ObjectName + PartNumber + "speed");
                        PrintedPart.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(ObjectName + PartNumber + "amount");
                        PrintedPart.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(ObjectName + PartNumber + "type");
                        PrintedPart.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageContainer")
                    {
                        GameObject PrintedPart = Instantiate(StorageContainer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "StorageComputer")
                    {
                        GameObject PrintedPart = Instantiate(StorageComputer, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronBlock")
                    {
                        GameObject PrintedPart = Instantiate(IronBlock, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "IronRamp")
                    {
                        GameObject PrintedPart = Instantiate(IronRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<IronBlock>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Steel")
                    {
                        GameObject PrintedPart = Instantiate(Steel, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "SteelRamp")
                    {
                        GameObject PrintedPart = Instantiate(SteelRamp, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Steel>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Brick")
                    {
                        GameObject PrintedPart = Instantiate(Brick, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Brick>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (ObjectName == WorldName + "Glass")
                    {
                        GameObject PrintedPart = Instantiate(Glass, ObjectPrintingLocation, ObjectPrintingRotation);
                        PrintedPart.GetComponent<Glass>().creationMethod = "spawned";
                        PrintedPart.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(ObjectName + PartNumber + "falling");
                        PrintedPart.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(ObjectName + PartNumber + "fallingStack");
                        PrintedPart.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    ConstructionCount++;
                }
                else
                {
                    ConstructionCount++;
                }
            }
            while (ConstructionCount <= FileBasedPrefs.GetInt(WorldName + "ConstructionTotal"));
        }
        Loaded = true;
        GetComponent<GameManager>().initGlass = FileBasedPrefs.GetBool(WorldName + "initGlass");
        GetComponent<GameManager>().initBrick = FileBasedPrefs.GetBool(WorldName + "initBrick");
        GetComponent<GameManager>().initIron = FileBasedPrefs.GetBool(WorldName + "initIron");
        GetComponent<GameManager>().initSteel = FileBasedPrefs.GetBool(WorldName + "initSteel");
        GameObject.Find("GameManager").GetComponent<GameManager>().CombineBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<MainMenu>().worldSelected == true && worldLoaded == false)
        {
            LoadWorld();
            worldLoaded = true;
        }
    }

    //SAVING BUILT OBJECTS
    public void SaveData()
    {
        saveCoroutine = StartCoroutine(SaveDataCoroutine());
    }

    IEnumerator SaveDataCoroutine()
    {
        dataSaved = false;
        saving = true;
        ConstructionCount = 0;
        int saveInterval = 0;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != BuiltObjects.transform)
                {
                    PartPosition = go.transform.position;
                    PartRotation = go.transform.rotation;
                    PartNumber = ConstructionCount.ToString();
                    if (go.GetComponent<Auger>() != null)
                    {
                        PartName = WorldName + "Auger";
                        go.GetComponent<Auger>().ID = (PartName + PartNumber);
                        go.GetComponent<Auger>().address = ConstructionCount;
                        int speed = go.GetComponent<Auger>().speed;
                        float amount = go.GetComponent<Auger>().amount;
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        PartName = WorldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = (PartName + PartNumber);
                        go.GetComponent<ElectricLight>().address = ConstructionCount;
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        PartName = WorldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = (PartName + PartNumber);
                        go.GetComponent<DarkMatterCollector>().address = ConstructionCount;
                        int speed = go.GetComponent<DarkMatterCollector>().speed;
                        float darkMatterAmount = go.GetComponent<DarkMatterCollector>().darkMatterAmount;
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "darkMatterAmount", darkMatterAmount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        PartName = WorldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().address = ConstructionCount;
                        go.GetComponent<DarkMatterConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<DarkMatterConduit>().inputID;
                        string outputID = go.GetComponent<DarkMatterConduit>().outputID;
                        int speed = go.GetComponent<DarkMatterConduit>().speed;
                        float darkMatterAmount = go.GetComponent<DarkMatterConduit>().darkMatterAmount;
                        int range = go.GetComponent<DarkMatterConduit>().range;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "range", range);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "darkMatterAmount", darkMatterAmount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        PartName = WorldName + "RailCart";
                        go.GetComponent<RailCart>().address = ConstructionCount;
                        go.GetComponent<RailCart>().ID = (PartName + PartNumber);
                        string targetID = go.GetComponent<RailCart>().targetID;
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                        FileBasedPrefs.SetString(PartName + PartNumber + "targetID", targetID);
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        PartName = WorldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().address = ConstructionCount;
                        go.GetComponent<RailCartHub>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<RailCartHub>().inputID;
                        string outputID = go.GetComponent<RailCartHub>().outputID;
                        int range = go.GetComponent<RailCartHub>().range;
                        bool centralHub = go.GetComponent<RailCartHub>().centralHub;
                        bool stop = go.GetComponent<RailCartHub>().stop;
                        int circuit = go.GetComponent<RailCartHub>().circuit;
                        float stopTime = go.GetComponent<RailCartHub>().stopTime;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "range", range);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "circuit", circuit);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "centralHub", centralHub);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "stop", stop);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "stopTime", stopTime);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        PartName = WorldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().address = ConstructionCount;
                        go.GetComponent<UniversalConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<UniversalConduit>().inputID;
                        string outputID = go.GetComponent<UniversalConduit>().outputID;
                        string type = go.GetComponent<UniversalConduit>().type;
                        int speed = go.GetComponent<UniversalConduit>().speed;
                        int range = go.GetComponent<UniversalConduit>().range;
                        float amount = go.GetComponent<UniversalConduit>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "type", type);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "range", range);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        PartName = WorldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().address = ConstructionCount;
                        go.GetComponent<HeatExchanger>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<HeatExchanger>().inputID;
                        string outputID = go.GetComponent<HeatExchanger>().outputID;
                        string inputType = go.GetComponent<HeatExchanger>().inputType;
                        int speed = go.GetComponent<HeatExchanger>().speed;
                        float amount = go.GetComponent<HeatExchanger>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        PartName = WorldName + "Retriever";
                        go.GetComponent<Retriever>().address = ConstructionCount;
                        go.GetComponent<Retriever>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Retriever>().inputID;
                        string outputID = go.GetComponent<Retriever>().outputID;
                        int speed = go.GetComponent<Retriever>().speed;
                        float amount = go.GetComponent<Retriever>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        PartName = WorldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().address = ConstructionCount;
                        go.GetComponent<AutoCrafter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<AutoCrafter>().inputID;
                        int speed = go.GetComponent<AutoCrafter>().speed;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                        if (go.GetComponent<InventoryManager>() != null)
                        {
                            go.GetComponent<InventoryManager>().SaveData();
                        }
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        PartName = WorldName + "Smelter";
                        go.GetComponent<Smelter>().address = ConstructionCount;
                        go.GetComponent<Smelter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Smelter>().inputID;
                        string outputID = go.GetComponent<Smelter>().outputID;
                        string inputType = go.GetComponent<Smelter>().inputType;
                        string outputType = go.GetComponent<Smelter>().outputType;
                        int speed = go.GetComponent<Smelter>().speed;
                        float amount = go.GetComponent<Smelter>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        PartName = WorldName + "Turret";
                        go.GetComponent<Turret>().address = ConstructionCount;
                        go.GetComponent<Turret>().ID = (PartName + PartNumber);
                        int speed = go.GetComponent<Turret>().speed;
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerSource>() != null)
                    {
                        if (go.GetComponent<PowerSource>().type == "Solar Panel")
                        {
                            PartName = WorldName + "SolarPanel";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Generator")
                        {
                            PartName = WorldName + "Generator";
                        }
                        else if (go.GetComponent<PowerSource>().type == "Reactor Turbine")
                        {
                            PartName = WorldName + "ReactorTurbine";
                        }
                        go.GetComponent<PowerSource>().address = ConstructionCount;
                        go.GetComponent<PowerSource>().ID = (PartName + PartNumber);
                        string outputID = go.GetComponent<PowerSource>().outputID;
                        string fuelType = go.GetComponent<PowerSource>().fuelType;
                        int fuelAmount = go.GetComponent<PowerSource>().fuelAmount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "fuelType", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "fuelAmount", fuelAmount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        PartName = WorldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().address = ConstructionCount;
                        go.GetComponent<NuclearReactor>().ID = (PartName + PartNumber);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        PartName = WorldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().address = ConstructionCount;
                        go.GetComponent<PowerConduit>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<PowerConduit>().inputID;
                        string outputID1 = go.GetComponent<PowerConduit>().outputID1;
                        string outputID2 = go.GetComponent<PowerConduit>().outputID2;
                        bool dualOutput = go.GetComponent<PowerConduit>().dualOutput;
                        int range = go.GetComponent<PowerConduit>().range;
                        int powerAmount = go.GetComponent<PowerConduit>().powerAmount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID1", outputID1);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID2", outputID2);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "dualOutput", dualOutput);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "range", range);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        PartName = WorldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().address = ConstructionCount;
                        go.GetComponent<AlloySmelter>().ID = (PartName + PartNumber);

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
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID1", inputID1);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID2", inputID2);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType1", inputType1);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType2", inputType2);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount2", amount2);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", outputAmount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        PartName = WorldName + "Press";
                        go.GetComponent<Press>().address = ConstructionCount;
                        go.GetComponent<Press>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Press>().inputID;
                        string inputType = go.GetComponent<Press>().inputType;
                        string outputType = go.GetComponent<Press>().outputType;
                        string outputID = go.GetComponent<Press>().outputID;
                        int speed = go.GetComponent<Press>().speed;
                        float amount = go.GetComponent<Press>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        PartName = WorldName + "Extruder";
                        go.GetComponent<Extruder>().address = ConstructionCount;
                        go.GetComponent<Extruder>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<Extruder>().inputID;
                        string inputType = go.GetComponent<Extruder>().inputType;
                        string outputType = go.GetComponent<Extruder>().outputType;
                        string outputID = go.GetComponent<Extruder>().outputID;
                        int speed = go.GetComponent<Extruder>().speed;
                        float amount = go.GetComponent<Extruder>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        PartName = WorldName + "GearCutter";
                        go.GetComponent<GearCutter>().address = ConstructionCount;
                        go.GetComponent<GearCutter>().ID = (PartName + PartNumber);
                        string inputID = go.GetComponent<GearCutter>().inputID;
                        string inputType = go.GetComponent<GearCutter>().inputType;
                        string outputType = go.GetComponent<GearCutter>().outputType;
                        string outputID = go.GetComponent<GearCutter>().outputID;
                        int speed = go.GetComponent<GearCutter>().speed;
                        float amount = go.GetComponent<GearCutter>().amount;
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputID", inputID);
                        FileBasedPrefs.SetString(PartName + PartNumber + "inputType", inputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputType", outputType);
                        FileBasedPrefs.SetString(PartName + PartNumber + "outputID", outputID);
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        PartName = WorldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().address = ConstructionCount;
                        go.GetComponent<UniversalExtractor>().ID = (PartName + PartNumber);
                        int speed = go.GetComponent<UniversalExtractor>().speed;
                        float amount = go.GetComponent<UniversalExtractor>().amount;
                        string type = go.GetComponent<UniversalExtractor>().type;
                        FileBasedPrefs.SetInt(PartName + PartNumber + "speed", speed);
                        FileBasedPrefs.SetFloat(PartName + PartNumber + "amount", amount);
                        FileBasedPrefs.SetString(PartName + PartNumber + "type", type);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<InventoryManager>() != null && go.GetComponent<RailCart>() == null && go.GetComponent<PlayerController>() == null && go.GetComponent<Retriever>() == null && go.GetComponent<AutoCrafter>() == null)
                    {
                        PartName = WorldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = (PartName + PartNumber);
                        go.GetComponent<InventoryManager>().address = ConstructionCount;
                        go.GetComponent<InventoryManager>().SaveData();
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        PartName = WorldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().address = ConstructionCount;
                        go.GetComponent<StorageComputer>().ID = (PartName + PartNumber);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        PartName = WorldName + "AirLock";
                        go.GetComponent<AirLock>().address = ConstructionCount;
                        go.GetComponent<AirLock>().ID = (PartName + PartNumber);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "falling", go.GetComponent<PhysicsHandler>().falling);
                        FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", go.GetComponent<PhysicsHandler>().fallingStack);
                    }

                    FileBasedPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);

                    ConstructionCount++;

                    saveInterval++;
                    if (saveInterval >= 10)
                    {
                        yield return null;
                        saveInterval = 0;
                    }
                }
            }
        }

        Transform[] allTransforms = BuiltObjects.GetComponentsInChildren<Transform>(true);
        foreach (Transform T in allTransforms)
        {
            if (T != null)
            {
                PartPosition = T.position;
                PartRotation = T.rotation;
                PartNumber = ConstructionCount.ToString();
                if (T.gameObject.GetComponent<IronBlock>() != null)
                {
                    if (T.gameObject.name.Equals("IronRamp(Clone)"))
                    {
                        PartName = WorldName + "IronRamp";
                    }
                    else
                    {
                        PartName = WorldName + "IronBlock";
                    }
                    T.gameObject.GetComponent<IronBlock>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<IronBlock>().address = ConstructionCount;
                    FileBasedPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Steel>() != null)
                {
                    if (T.gameObject.name.Equals("SteelRamp(Clone)"))
                    {
                        PartName = WorldName + "SteelRamp";
                    }
                    else
                    {
                        PartName = WorldName + "Steel";
                    }
                    T.gameObject.GetComponent<Steel>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Steel>().address = ConstructionCount;
                    FileBasedPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    PartName = WorldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Brick>().address = ConstructionCount;
                    FileBasedPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    PartName = WorldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = (PartName + PartNumber);
                    T.gameObject.GetComponent<Glass>().address = ConstructionCount;
                    FileBasedPrefs.SetString(WorldName + PartNumber + "Name", PartName);
                    PlayerPrefsX.SetVector3(WorldName + PartNumber + "Position", PartPosition);
                    PlayerPrefsX.SetQuaternion(WorldName + PartNumber + "Rotation", PartRotation);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "falling", T.gameObject.GetComponent<PhysicsHandler>().falling);
                    FileBasedPrefs.SetBool(PartName + PartNumber + "fallingStack", T.gameObject.GetComponent<PhysicsHandler>().fallingStack);
                    ConstructionCount++;
                }

                saveInterval++;
                if (saveInterval >= 10)
                {
                    yield return null;
                    saveInterval = 0;
                }
            }
        }

        if (ConstructionCount != 0)
        {
            FileBasedPrefs.SetInt(WorldName + "ConstructionTotal", ConstructionCount);
        }

        FileBasedPrefs.ManuallySave();
        dataSaved = true;
        saving = false;
    }
}
