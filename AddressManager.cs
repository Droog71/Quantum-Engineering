using UnityEngine;
using System.Collections;

public class AddressManager
{
    private StateManager stateManager;

    //! This class assigns a unique ID to every block in the world.
    public AddressManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    //! Assigns ID to all objects in the world.
    public IEnumerator AddressingCoroutine()
    {
        stateManager.assigningIDs = true;
        int idCount = 0;
        int addressingInterval = 0;
        string objectName  = "";
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.BuiltObjects.transform)
                {
                    if (go.GetComponent<Auger>() != null)
                    {
                        objectName = stateManager.WorldName + "Auger";
                        go.GetComponent<Auger>().ID = objectName  + idCount;
                        go.GetComponent<Auger>().address = idCount;
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        objectName = stateManager.WorldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = objectName  + idCount;
                        go.GetComponent<ElectricLight>().address = idCount;
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        objectName = stateManager.WorldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = objectName  + idCount;
                        go.GetComponent<DarkMatterCollector>().address = idCount;
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().address = idCount;
                        go.GetComponent<DarkMatterConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        objectName = stateManager.WorldName + "RailCart";
                        go.GetComponent<RailCart>().address = idCount;
                        go.GetComponent<RailCart>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        objectName = stateManager.WorldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().address = idCount;
                        go.GetComponent<RailCartHub>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().address = idCount;
                        go.GetComponent<UniversalConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        objectName = stateManager.WorldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().address = idCount;
                        go.GetComponent<HeatExchanger>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        objectName = stateManager.WorldName + "Retriever";
                        go.GetComponent<Retriever>().address = idCount;
                        go.GetComponent<Retriever>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        objectName = stateManager.WorldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().address = idCount;
                        go.GetComponent<AutoCrafter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        objectName = stateManager.WorldName + "Smelter";
                        go.GetComponent<Smelter>().address = idCount;
                        go.GetComponent<Smelter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        objectName = stateManager.WorldName + "Turret";
                        go.GetComponent<Turret>().address = idCount;
                        go.GetComponent<Turret>().ID = (objectName   + idCount);
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
                        go.GetComponent<PowerSource>().address = idCount;
                        go.GetComponent<PowerSource>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        objectName = stateManager.WorldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().address = idCount;
                        go.GetComponent<NuclearReactor>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        objectName = stateManager.WorldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().address = idCount;
                        go.GetComponent<PowerConduit>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        objectName = stateManager.WorldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().address = idCount;
                        go.GetComponent<AlloySmelter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        objectName = stateManager.WorldName + "Press";
                        go.GetComponent<Press>().address = idCount;
                        go.GetComponent<Press>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        objectName = stateManager.WorldName + "Extruder";
                        go.GetComponent<Extruder>().address = idCount;
                        go.GetComponent<Extruder>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        objectName = stateManager.WorldName + "GearCutter";
                        go.GetComponent<GearCutter>().address = idCount;
                        go.GetComponent<GearCutter>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        objectName = stateManager.WorldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().address = idCount;
                        go.GetComponent<UniversalExtractor>().ID = objectName  + idCount;
                    }
                    if (stateManager.IsStorageContainer(go))
                    {
                        objectName = stateManager.WorldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = objectName  + idCount;
                        go.GetComponent<InventoryManager>().address = idCount;
                        go.GetComponent<InventoryManager>().SaveData();
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        objectName = stateManager.WorldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().address = idCount;
                        go.GetComponent<StorageComputer>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        objectName = stateManager.WorldName + "AirLock";
                        go.GetComponent<AirLock>().address = idCount;
                        go.GetComponent<AirLock>().ID = objectName  + idCount;
                    }
                    if (go.GetComponent<ModMachine>() != null)
                    {
                        ModMachine machine = go.GetComponent<ModMachine>();
                        objectName  = stateManager.WorldName + "ModMachine";
                        go.GetComponent<ModMachine>().ID = objectName + idCount;
                        go.GetComponent<ModMachine>().address = idCount;
                    }

                    idCount++;

                    addressingInterval++;
                    if (addressingInterval >= 10)
                    {
                        yield return null;
                        addressingInterval = 0;
                    }
                }
            }
        }

        Transform[] allTransforms = stateManager.BuiltObjects.GetComponentsInChildren<Transform>(true);
        foreach (Transform T in allTransforms)
        {
            if (T != null)
            {
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
                    T.gameObject.GetComponent<IronBlock>().ID = objectName  + idCount;
                    T.gameObject.GetComponent<IronBlock>().address = idCount;
                    idCount++;
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
                    T.gameObject.GetComponent<Steel>().ID = objectName  + idCount;
                    T.gameObject.GetComponent<Steel>().address = idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    objectName = stateManager.WorldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = objectName  + idCount;
                    T.gameObject.GetComponent<Brick>().address = idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    objectName = stateManager.WorldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = objectName  + idCount;
                    T.gameObject.GetComponent<Glass>().address = idCount;
                    idCount++;
                }

                addressingInterval++;
                if (addressingInterval >= 10)
                {
                    yield return null;
                    addressingInterval = 0;
                }
            }
        }

       stateManager.assigningIDs = false;
    }
}

