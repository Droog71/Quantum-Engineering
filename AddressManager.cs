using UnityEngine;
using System.Collections;

public class AddressManager
{
    private StateManager stateManager;

    public AddressManager(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    // Assigns ID to all objects in the world.
    public IEnumerator AddressingCoroutine()
    {
        stateManager.assigningIDs = true;
        int idCount = 0;
        int addressingInterval = 0;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.transform.parent != stateManager.BuiltObjects.transform)
                {
                    stateManager.PartNumber = idCount.ToString();
                    if (go.GetComponent<Auger>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Auger";
                        go.GetComponent<Auger>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<Auger>().address = idCount;
                    }
                    if (go.GetComponent<ElectricLight>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "ElectricLight";
                        go.GetComponent<ElectricLight>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<ElectricLight>().address = idCount;
                    }
                    if (go.GetComponent<DarkMatterCollector>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "DarkMatterCollector";
                        go.GetComponent<DarkMatterCollector>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<DarkMatterCollector>().address = idCount;
                    }
                    if (go.GetComponent<DarkMatterConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "DarkMatterConduit";
                        go.GetComponent<DarkMatterConduit>().address = idCount;
                        go.GetComponent<DarkMatterConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<RailCart>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "RailCart";
                        go.GetComponent<RailCart>().address = idCount;
                        go.GetComponent<RailCart>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<RailCartHub>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "RailCartHub";
                        go.GetComponent<RailCartHub>().address = idCount;
                        go.GetComponent<RailCartHub>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<UniversalConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "UniversalConduit";
                        go.GetComponent<UniversalConduit>().address = idCount;
                        go.GetComponent<UniversalConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<HeatExchanger>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "HeatExchanger";
                        go.GetComponent<HeatExchanger>().address = idCount;
                        go.GetComponent<HeatExchanger>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<Retriever>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Retriever";
                        go.GetComponent<Retriever>().address = idCount;
                        go.GetComponent<Retriever>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<AutoCrafter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AutoCrafter";
                        go.GetComponent<AutoCrafter>().address = idCount;
                        go.GetComponent<AutoCrafter>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<Smelter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Smelter";
                        go.GetComponent<Smelter>().address = idCount;
                        go.GetComponent<Smelter>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<Turret>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Turret";
                        go.GetComponent<Turret>().address = idCount;
                        go.GetComponent<Turret>().ID = (stateManager.PartName + stateManager.PartNumber);
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
                        go.GetComponent<PowerSource>().address = idCount;
                        go.GetComponent<PowerSource>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<NuclearReactor>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "NuclearReactor";
                        go.GetComponent<NuclearReactor>().address = idCount;
                        go.GetComponent<NuclearReactor>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<PowerConduit>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "PowerConduit";
                        go.GetComponent<PowerConduit>().address = idCount;
                        go.GetComponent<PowerConduit>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<AlloySmelter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AlloySmelter";
                        go.GetComponent<AlloySmelter>().address = idCount;
                        go.GetComponent<AlloySmelter>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<Press>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Press";
                        go.GetComponent<Press>().address = idCount;
                        go.GetComponent<Press>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<Extruder>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "Extruder";
                        go.GetComponent<Extruder>().address = idCount;
                        go.GetComponent<Extruder>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<GearCutter>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "GearCutter";
                        go.GetComponent<GearCutter>().address = idCount;
                        go.GetComponent<GearCutter>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<UniversalExtractor>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "UniversalExtractor";
                        go.GetComponent<UniversalExtractor>().address = idCount;
                        go.GetComponent<UniversalExtractor>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (stateManager.IsStorageContainer(go))
                    {
                        stateManager.PartName = stateManager.WorldName + "StorageContainer";
                        go.GetComponent<InventoryManager>().ID = (stateManager.PartName + stateManager.PartNumber);
                        go.GetComponent<InventoryManager>().address = idCount;
                        go.GetComponent<InventoryManager>().SaveData();
                    }
                    if (go.GetComponent<StorageComputer>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "StorageComputer";
                        go.GetComponent<StorageComputer>().address = idCount;
                        go.GetComponent<StorageComputer>().ID = (stateManager.PartName + stateManager.PartNumber);
                    }
                    if (go.GetComponent<AirLock>() != null)
                    {
                        stateManager.PartName = stateManager.WorldName + "AirLock";
                        go.GetComponent<AirLock>().address = idCount;
                        go.GetComponent<AirLock>().ID = (stateManager.PartName + stateManager.PartNumber);
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
                stateManager.PartNumber = idCount.ToString();
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
                    T.gameObject.GetComponent<IronBlock>().address = idCount;
                    idCount++;
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
                    T.gameObject.GetComponent<Steel>().address = idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Brick>() != null)
                {
                    stateManager.PartName = stateManager.WorldName + "Brick";
                    T.gameObject.GetComponent<Brick>().ID = (stateManager.PartName + stateManager.PartNumber);
                    T.gameObject.GetComponent<Brick>().address = idCount;
                    idCount++;
                }
                if (T.gameObject.GetComponent<Glass>() != null)
                {
                    stateManager.PartName = stateManager.WorldName + "Glass";
                    T.gameObject.GetComponent<Glass>().ID = (stateManager.PartName + stateManager.PartNumber);
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

