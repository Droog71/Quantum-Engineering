﻿using UnityEngine;
using System.Collections;

public class AddressManager
{
    private StateManager stateManager;
    private GameManager gameManager;
    private int totalObjects;
    public bool machineIdCoroutineActive;
    public bool blockIdCoroutineActive;

    //! This class assigns a unique ID to every block in the world.
    public AddressManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
        gameManager = stateManager.GetComponent<GameManager>();
    }

    public IEnumerator MachineIdCoroutine()
    {
        machineIdCoroutineActive = true;
        int idCount = 0;
        int addressingInterval = 0;
        string objectName  = "";
        GameObject[] machines = GameObject.FindGameObjectsWithTag("Machine");
        foreach (GameObject go in machines)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.builtObjects.transform)
                {
                    if (go.GetComponent<Auger>() != null)
                    {
                        objectName = stateManager.worldName + "Auger";
                        go.GetComponent<Auger>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        objectName = stateManager.worldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        objectName = stateManager.worldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        objectName = stateManager.worldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        objectName = stateManager.worldName + "RailCart";
                        go.GetComponent<RailCart>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        objectName = stateManager.worldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        objectName = stateManager.worldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        objectName = stateManager.worldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        objectName = stateManager.worldName + "Retriever";
                        go.GetComponent<Retriever>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        objectName = stateManager.worldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        objectName = stateManager.worldName + "Smelter";
                        go.GetComponent<Smelter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        objectName = stateManager.worldName + "Turret";
                        go.GetComponent<Turret>().ID = (objectName   + idCount);
                    }
                    if (go.GetComponent<MissileTurret>() != null)
                    {
                        objectName = stateManager.worldName + "MissileTurret";
                        go.GetComponent<MissileTurret>().ID = (objectName   + idCount);
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
                        go.GetComponent<PowerSource>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        objectName = stateManager.worldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        objectName = stateManager.worldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        objectName = stateManager.worldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        objectName = stateManager.worldName + "Press";
                        go.GetComponent<Press>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        objectName = stateManager.worldName + "Extruder";
                        go.GetComponent<Extruder>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        objectName = stateManager.worldName + "GearCutter";
                        go.GetComponent<GearCutter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        objectName = stateManager.worldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().ID = objectName  + idCount;
                    }
                    if (stateManager.IsStorageContainer(go))
                    {
                        objectName = stateManager.worldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = objectName  + idCount;
                        go.GetComponent<InventoryManager>().SaveData();
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        objectName = stateManager.worldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Door>() != null)
                    {
                        if (go.GetComponent<Door>().type == "Door")
                        {
                            objectName = stateManager.worldName + "Door";
                        }
                        else if (go.GetComponent<Door>().type == "Quantum Hatchway")
                        {
                            objectName = stateManager.worldName + "QuantumHatchway";
                        }
                        go.GetComponent<Door>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<ModMachine>() != null)
                    {
                        ModMachine machine = go.GetComponent<ModMachine>();
                        objectName  = stateManager.worldName + "ModMachine";
                        go.GetComponent<ModMachine>().ID = objectName + idCount;
                    }
                    if (go.GetComponent<ProtectionBlock>() != null)
                    {
                        ProtectionBlock machine = go.GetComponent<ProtectionBlock>();
                        objectName  = stateManager.worldName + "ProtectionBlock";
                        go.GetComponent<ProtectionBlock>().ID = objectName + idCount;
                    }

                    idCount++;

                    addressingInterval++;
                    if (addressingInterval >= machines.Length * (gameManager.simulationSpeed / 4))
                    {
                        yield return null;
                        addressingInterval = 0;
                    }
                }
            }
        }
        machineIdCoroutineActive = false;
        if (gameManager.dataSaveRequested == true)
        {
            stateManager.finalMachineAddress = true;
        }
    }

    public IEnumerator BlockIdCoroutine()
    {
        blockIdCoroutineActive = true;
        int idCount = 0;
        int addressingInterval = 0;
        string objectName  = "";
        Transform[] blocks = stateManager.builtObjects.GetComponentsInChildren<Transform>(true);
        foreach (Transform T in blocks)
        {
            if (T != null)
            {
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
                    T.gameObject.GetComponent<IronBlock>().ID = objectName  + idCount;
                    idCount++;
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
                    T.gameObject.GetComponent<Steel>().ID = objectName  + idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    objectName = stateManager.worldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = objectName  + idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    objectName = stateManager.worldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = objectName  + idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<ModBlock>() != null)
                {
                    objectName = stateManager.worldName + "ModBlock";
                    T.gameObject.GetComponent<ModBlock>().ID = objectName  + idCount;
                    idCount++;
                }

                addressingInterval++;
                if (addressingInterval >= blocks.Length * (gameManager.simulationSpeed / 4))
                {
                    yield return null;
                    addressingInterval = 0;
                }
            }
        }
        blockIdCoroutineActive = false;
        if (gameManager.dataSaveRequested == true)
        {
            stateManager.finalBlockAddress = true;
        }
    }
}