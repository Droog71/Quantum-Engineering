using UnityEngine;
using System.Collections.Generic;

public class BlockDictionary : MonoBehaviour
{
    public Dictionary<string, GameObject> dictionary;

    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        dictionary = new Dictionary<string, GameObject>
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
            { "Universal Conduit", playerController.universalConduit },
            { "Universal Extractor", playerController.universalExtractor }
        };
    }
}
