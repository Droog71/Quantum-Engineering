﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! This class handles unique ID assignment and saving & loading of worlds.
public class StateManager : MonoBehaviour
{
    public bool saving;
    public bool dataSaved;
    public bool worldLoaded;
    public int progress;
    public int[] idList;
    public List<string> modTextureList;
    public List<string> modRecipeList;
    public GameObject darkMatterCollector;
    public GameObject darkMatterConduit;
    public GameObject ironBlock;
    public GameObject ironRamp;
    public GameObject steel;
    public GameObject steelRamp;
    public GameObject storageContainer;
    public GameObject glass;
    public GameObject universalExtractor;
    public GameObject universalConduit;
    public GameObject powerConduit;
    public GameObject smelter;
    public GameObject press;
    public GameObject gearCutter;
    public GameObject alloySmelter;
    public GameObject solarPanel;
    public GameObject generator;
    public GameObject extruder;
    public GameObject turret;
    public GameObject missileTurret;
    public GameObject auger;
    public GameObject heatExchanger;
    public GameObject electricLight;
    public GameObject retriever;
    public GameObject door;
    public GameObject quantumHatchway;
    public GameObject nuclearReactor;
    public GameObject reactorTurbine;
    public GameObject storageComputer;
    public GameObject autoCrafter;
    public GameObject railCartHub;
    public GameObject railCart;
    public GameObject brick;
    public GameObject modMachine;
    public GameObject modBlock;
    public GameObject builtObjects;
    public bool assigningIDs;
    public string worldName = "World";
    public string partName = "";
    public SaveManager saveManager;
    public Vector3 partPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Quaternion partRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    private Vector3 emptyVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 objectPosition;
    private Quaternion objectRotation;
    private AddressManager addressManager;
    private Coroutine addressingCoroutine;
    private Coroutine loadCoroutine;
    private Coroutine saveCoroutine;
    private float updateTick;
    private string objectName = "";
    private bool loading;

    //! Called by unity engine before the first update.
    public void Start()
    {
        saveManager = new SaveManager(this);
    }

    //! Update is called once per frame.
    public void Update()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        MainMenu mainMenu = player.GetComponent<MainMenu>();

        if (mainMenu.worldSelected == true && playerController.addedModBlocks == true && loading == false)
        {
            loadCoroutine = StartCoroutine(LoadWorld());
            loading = true;
        }
        if (worldLoaded == true)
        {
            if (addressManager == null)
            {
                addressManager = new AddressManager(this);
            }

            updateTick += 1 * Time.deltaTime;
            if (updateTick > 1)
            {
                if (assigningIDs == false && saving == false)
                {
                    AssignIDs();
                }
                updateTick = 0;
            }
        }
    }

    //! Loads a saved world.
    private IEnumerator LoadWorld()
    {
        if (worldLoaded == false && PlayerPrefsX.GetIntArray(worldName + "idList").Length > 0)
        {
            int loadInterval = 0;
            progress = 0;
            idList = PlayerPrefsX.GetIntArray(worldName + "idList");
            foreach (int objectID in idList)
            {
                while (GetComponent<GameManager>().working == true)
                {
                    yield return null;
                }
                objectPosition = PlayerPrefsX.GetVector3(worldName + objectID + "Position");
                objectRotation = PlayerPrefsX.GetQuaternion(worldName + objectID + "Rotation");
                objectName = FileBasedPrefs.GetString(worldName + objectID + "Name");
                if (objectName != "" && objectPosition != emptyVector)
                {
                    if (objectName == worldName + "Door")
                    {
                        GameObject SpawnedObject = Instantiate(door, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Door>().audioClip = FileBasedPrefs.GetInt(objectName + objectID + "audioClip");
                        SpawnedObject.GetComponent<Door>().textureIndex = FileBasedPrefs.GetInt(objectName + objectID + "textureIndex");
                        SpawnedObject.GetComponent<Door>().material = FileBasedPrefs.GetString(objectName + objectID + "material");
                        SpawnedObject.GetComponent<Door>().edited = FileBasedPrefs.GetBool(objectName + objectID + "edited");
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "QuantumHatchway")
                    {
                        GameObject SpawnedObject = Instantiate(quantumHatchway, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "DarkMatterCollector")
                    {
                        GameObject SpawnedObject = Instantiate(darkMatterCollector, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<DarkMatterCollector>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<DarkMatterCollector>().darkMatterAmount = FileBasedPrefs.GetFloat(objectName + objectID + "darkMatterAmount");
                        SpawnedObject.GetComponent<DarkMatterCollector>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "DarkMatterConduit")
                    {
                        GameObject SpawnedObject = Instantiate(darkMatterConduit, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<DarkMatterConduit>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<DarkMatterConduit>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<DarkMatterConduit>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<DarkMatterConduit>().range = FileBasedPrefs.GetInt(objectName + objectID + "range");
                        SpawnedObject.GetComponent<DarkMatterConduit>().darkMatterAmount = FileBasedPrefs.GetFloat(objectName + objectID + "darkMatterAmount");
                        SpawnedObject.GetComponent<DarkMatterConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "RailCartHub")
                    {
                        GameObject SpawnedObject = Instantiate(railCartHub, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<RailCartHub>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<RailCartHub>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<RailCartHub>().range = FileBasedPrefs.GetInt(objectName + objectID + "range");
                        SpawnedObject.GetComponent<RailCartHub>().stop = FileBasedPrefs.GetBool(objectName + objectID + "stop");
                        SpawnedObject.GetComponent<RailCartHub>().circuit = FileBasedPrefs.GetInt(objectName + objectID + "circuit");
                        SpawnedObject.GetComponent<RailCartHub>().stopTime = FileBasedPrefs.GetFloat(objectName + objectID + "stopTime");
                        SpawnedObject.GetComponent<RailCartHub>().centralHub = FileBasedPrefs.GetBool(objectName + objectID + "centralHub");
                        SpawnedObject.GetComponent<RailCartHub>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "RailCart")
                    {
                        GameObject SpawnedObject = Instantiate(railCart, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<RailCart>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<RailCart>().targetID = FileBasedPrefs.GetString(objectName + objectID + "targetID");
                    }
                    if (objectName == worldName + "UniversalConduit")
                    {
                        GameObject SpawnedObject = Instantiate(universalConduit, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<UniversalConduit>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<UniversalConduit>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<UniversalConduit>().type = FileBasedPrefs.GetString(objectName + objectID + "type");
                        SpawnedObject.GetComponent<UniversalConduit>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<UniversalConduit>().range = FileBasedPrefs.GetInt(objectName + objectID + "range");
                        SpawnedObject.GetComponent<UniversalConduit>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<UniversalConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Retriever")
                    {
                        GameObject SpawnedObject = Instantiate(retriever, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Retriever>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<Retriever>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<Retriever>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Retriever>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<Retriever>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "AutoCrafter")
                    {
                        GameObject SpawnedObject = Instantiate(autoCrafter, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<AutoCrafter>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<AutoCrafter>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<AutoCrafter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Smelter")
                    {
                        GameObject SpawnedObject = Instantiate(smelter, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Smelter>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<Smelter>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<Smelter>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<Smelter>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<Smelter>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Smelter>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<Smelter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "HeatExchanger")
                    {
                        GameObject SpawnedObject = Instantiate(heatExchanger, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<HeatExchanger>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<HeatExchanger>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<HeatExchanger>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<HeatExchanger>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<HeatExchanger>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<HeatExchanger>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "SolarPanel")
                    {
                        GameObject SpawnedObject = Instantiate(solarPanel, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Generator")
                    {
                        GameObject SpawnedObject = Instantiate(generator, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PowerSource>().fuelType = FileBasedPrefs.GetString(objectName + objectID + "fuelType");
                        SpawnedObject.GetComponent<PowerSource>().fuelAmount = FileBasedPrefs.GetInt(objectName + objectID + "fuelAmount");
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "NuclearReactor")
                    {
                        GameObject SpawnedObject = Instantiate(nuclearReactor, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<NuclearReactor>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "ReactorTurbine")
                    {
                        GameObject SpawnedObject = Instantiate(reactorTurbine, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PowerSource>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<PowerSource>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "PowerConduit")
                    {
                        GameObject SpawnedObject = Instantiate(powerConduit, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PowerConduit>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<PowerConduit>().outputID1 = FileBasedPrefs.GetString(objectName + objectID + "outputID1");
                        SpawnedObject.GetComponent<PowerConduit>().outputID2 = FileBasedPrefs.GetString(objectName + objectID + "outputID2");
                        SpawnedObject.GetComponent<PowerConduit>().dualOutput = FileBasedPrefs.GetBool(objectName + objectID + "dualOutput");
                        SpawnedObject.GetComponent<PowerConduit>().range = FileBasedPrefs.GetInt(objectName + objectID + "range");
                        SpawnedObject.GetComponent<PowerConduit>().powerAmount = FileBasedPrefs.GetInt(objectName + objectID + "powerAmount");
                        SpawnedObject.GetComponent<PowerConduit>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Auger")
                    {
                        GameObject SpawnedObject = Instantiate(auger, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Auger>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Auger>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<Auger>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "ElectricLight")
                    {
                        GameObject SpawnedObject = Instantiate(electricLight, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<ElectricLight>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Turret")
                    {
                        GameObject SpawnedObject = Instantiate(turret, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Turret>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Turret>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "MissileTurret")
                    {
                        GameObject SpawnedObject = Instantiate(missileTurret, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<MissileTurret>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<MissileTurret>().ammoType = FileBasedPrefs.GetString(objectName + objectID + "ammoType");
                        SpawnedObject.GetComponent<MissileTurret>().ammoAmount = FileBasedPrefs.GetInt(objectName + objectID + "ammoAmount");
                        SpawnedObject.GetComponent<MissileTurret>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "AlloySmelter")
                    {
                        GameObject SpawnedObject = Instantiate(alloySmelter, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<AlloySmelter>().inputID1 = FileBasedPrefs.GetString(objectName + objectID + "inputID1");
                        SpawnedObject.GetComponent<AlloySmelter>().inputID2 = FileBasedPrefs.GetString(objectName + objectID + "inputID2");
                        SpawnedObject.GetComponent<AlloySmelter>().inputType1 = FileBasedPrefs.GetString(objectName + objectID + "inputType1");
                        SpawnedObject.GetComponent<AlloySmelter>().inputType2 = FileBasedPrefs.GetString(objectName + objectID + "inputType2");
                        SpawnedObject.GetComponent<AlloySmelter>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<AlloySmelter>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<AlloySmelter>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<AlloySmelter>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<AlloySmelter>().amount2 = FileBasedPrefs.GetFloat(objectName + objectID + "amount2");
                        SpawnedObject.GetComponent<AlloySmelter>().outputAmount = FileBasedPrefs.GetFloat(objectName + objectID + "outputAmount");
                        SpawnedObject.GetComponent<AlloySmelter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Press")
                    {
                        GameObject SpawnedObject = Instantiate(press, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Press>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<Press>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<Press>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<Press>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<Press>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Press>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<Press>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Extruder")
                    {
                        GameObject SpawnedObject = Instantiate(extruder, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Extruder>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<Extruder>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<Extruder>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<Extruder>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<Extruder>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<Extruder>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<Extruder>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "GearCutter")
                    {
                        GameObject SpawnedObject = Instantiate(gearCutter, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<GearCutter>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<GearCutter>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<GearCutter>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<GearCutter>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<GearCutter>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<GearCutter>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<GearCutter>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "UniversalExtractor")
                    {
                        GameObject SpawnedObject = Instantiate(universalExtractor, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<UniversalExtractor>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<UniversalExtractor>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<UniversalExtractor>().type = FileBasedPrefs.GetString(objectName + objectID + "type");
                        SpawnedObject.GetComponent<UniversalExtractor>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "StorageContainer")
                    {
                        GameObject SpawnedObject = Instantiate(storageContainer, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "StorageComputer")
                    {
                        GameObject SpawnedObject = Instantiate(storageComputer, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "IronBlock")
                    {
                        GameObject SpawnedObject = Instantiate(ironBlock, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "IronRamp")
                    {
                        GameObject SpawnedObject = Instantiate(ironRamp, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<IronBlock>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Steel")
                    {
                        GameObject SpawnedObject = Instantiate(steel, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "SteelRamp")
                    {
                        GameObject SpawnedObject = Instantiate(steelRamp, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Steel>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Brick")
                    {
                        GameObject SpawnedObject = Instantiate(brick, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Brick>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "Glass")
                    {
                        GameObject SpawnedObject = Instantiate(glass, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<Glass>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                    }
                    if (objectName == worldName + "ModMachine")
                    {
                        GameObject SpawnedObject = Instantiate(modMachine, objectPosition, objectRotation);
                        SpawnedObject.GetComponent<ModMachine>().machineName = FileBasedPrefs.GetString(objectName + objectID + "machineName");
                        SpawnedObject.GetComponent<ModMachine>().inputID = FileBasedPrefs.GetString(objectName + objectID + "inputID");
                        SpawnedObject.GetComponent<ModMachine>().outputID = FileBasedPrefs.GetString(objectName + objectID + "outputID");
                        SpawnedObject.GetComponent<ModMachine>().inputType = FileBasedPrefs.GetString(objectName + objectID + "inputType");
                        SpawnedObject.GetComponent<ModMachine>().outputType = FileBasedPrefs.GetString(objectName + objectID + "outputType");
                        SpawnedObject.GetComponent<ModMachine>().speed = FileBasedPrefs.GetInt(objectName + objectID + "speed");
                        SpawnedObject.GetComponent<ModMachine>().amount = FileBasedPrefs.GetFloat(objectName + objectID + "amount");
                        SpawnedObject.GetComponent<ModMachine>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                    }
                    if (objectName == worldName + "ModBlock")
                    {
                        GameObject SpawnedObject = Instantiate(modBlock, objectPosition, objectRotation);
                        string blockName = FileBasedPrefs.GetString(objectName + objectID + "blockName");
                        SpawnedObject.GetComponent<ModBlock>().blockName = blockName;
                        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                        BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
                        if (blockDictionary.meshDictionary.ContainsKey(blockName))
                        {
                            SpawnedObject.GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[blockName];
                        }
                        GetComponent<GameManager>().meshManager.SetMaterial(SpawnedObject, blockName);
                        SpawnedObject.GetComponent<PhysicsHandler>().creationMethod = "spawned";
                        SpawnedObject.GetComponent<PhysicsHandler>().falling = FileBasedPrefs.GetBool(objectName + objectID + "falling");
                        SpawnedObject.GetComponent<PhysicsHandler>().fallingStack = FileBasedPrefs.GetBool(objectName + objectID + "fallingStack");
                    }
                }
                progress++;
                loadInterval++;
                if (loadInterval >= idList.Length * 0.025f)
                {
                    loadInterval = 0;
                    GetComponent<GameManager>().meshManager.CombineBlocks();
                    yield return null;
                }
            }
        }
        GetComponent<GameManager>().meshManager.CombineBlocks();
        worldLoaded = true;
    }

    //! Assigns ID to objects in the world.
    private void AssignIDs()
    {
        addressingCoroutine = StartCoroutine(addressManager.AddressingCoroutine());
    }

    //! Saves the game.
    public void SaveData()
    {
        if (assigningIDs == false)
            saveCoroutine = StartCoroutine(saveManager.SaveDataCoroutine());
    }

    //! Returns true if the object in question is a storage container.
    public bool IsStorageContainer(GameObject go)
    {
        return go.GetComponent<InventoryManager>() != null
        && go.GetComponent<RailCart>() == null
        && go.GetComponent<PlayerController>() == null
        && go.GetComponent<Retriever>() == null
        && go.GetComponent<AutoCrafter>() == null;
    }

    public bool Busy()
    {
        return worldLoaded == false || saving == true;
    }
}