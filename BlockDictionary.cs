using UnityEngine;
using System.Collections.Generic;

public class BlockDictionary : MonoBehaviour
{
    public Dictionary<string, GameObject> machineDictionary;
    public Dictionary<string, GameObject> blockDictionary;

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
    }
}
