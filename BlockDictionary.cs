using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BlockDictionary : MonoBehaviour
{
    public Dictionary<string, GameObject> machineDictionary;
    public Dictionary<string, GameObject> blockDictionary;
    public Type[] machineTypes;

    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();

        blockDictionary = new Dictionary<string, GameObject>
        {
            { "Brick", playerController.brick },
            { "Glass Block", playerController.glass },
            { "Iron Block", playerController.ironBlock },
            { "Iron Ramp", playerController.ironRamp },
            { "Steel Block", playerController.steel },
            { "Steel Ramp", playerController.steelRamp }
        };

        machineDictionary = new Dictionary<string, GameObject>
        {
            { "Quantum Hatchway", playerController.airlock },
            { "Alloy Smelter", playerController.alloySmelter },
            { "Auger", playerController.auger },
            { "Auto Crafter", playerController.autoCrafter },
            { "Brick", playerController.brick },
            { "Dark Matter Collector", playerController.darkMatterCollector },
            { "Dark Matter Conduit", playerController.darkMatterConduit },
            { "Electric Light", playerController.electricLight },
            { "Extruder", playerController.extruder },
            { "Gear Cutter", playerController.gearCutter },
            { "Generator", playerController.generator },
            { "Glass Block", playerController.glass },
            { "Heat Exchanger", playerController.heatExchanger },
            { "Iron Block", playerController.ironBlock },
            { "Nuclear Reactor", playerController.nuclearReactor },
            { "Power Conduit", playerController.powerConduit },
            { "Press", playerController.press },
            { "Rail Cart", playerController.railCart },
            { "Rail Cart Hub", playerController.railCartHub },
            { "Reactor Turbine", playerController.reactorTurbine },
            { "Retriever", playerController.retriever },
            { "Smelter", playerController.smelter },
            { "Solar Panel", playerController.solarPanel },
            { "Steel", playerController.steel },
            { "Storage Computer", playerController.storageComputer },
            { "Storage Container", playerController.storageContainer },
            { "Turret", playerController.turret },
            { "Universal Conduit", playerController.universalConduit },
            { "Universal Extractor", playerController.universalExtractor }
        };

        machineTypes = new Type[] 
        { 
            typeof(AirLock), 
            typeof(AlloySmelter), 
            typeof(Auger),  
            typeof(AutoCrafter), 
            typeof(Brick), 
            typeof(DarkMatterCollector), 
            typeof(DarkMatterConduit), 
            typeof(ElectricLight),
            typeof(Extruder), 
            typeof(GearCutter), 
            typeof(PowerSource), 
            typeof(Glass),
            typeof(HeatExchanger),
            typeof(UniversalResource),
            typeof(NuclearReactor),
            typeof(PowerConduit),
            typeof(Press),
            typeof(RailCart),
            typeof(RailCartHub),
            typeof(PowerSource),
            typeof(Retriever),
            typeof(Smelter),
            typeof(PowerSource),
            typeof(Steel),
            typeof(StorageComputer),
            typeof(InventoryManager),
            typeof(Turret),
            typeof(UniversalConduit),
            typeof(UniversalExtractor)
        };
    }
}
