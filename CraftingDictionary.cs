using UnityEngine;
using System.Collections.Generic;

// This class provides the crafting recipe dictionary used by the crafting manager
public class CraftingDictionary : MonoBehaviour
{
    public Dictionary<string, CraftingRecipe> dictionary;

    private readonly string[] ironBlockIngredients = { "Iron Plate" };
    private readonly string[] steelBlockIngredients = { "Steel Plate" };
    private readonly string[] quantumHatchwayIngredients = { "Tin Plate", "Dark Matter" };
    private readonly string[] electricLightIngredients = { "Glass Block", "Tin Plate", "Copper Wire" };
    private readonly string[] augerIngredients = { "Iron Ingot", "Copper Ingot" };
    private readonly string[] storageContainerIngredients = { "Iron Plate" };
    private readonly string[] extruderIngredients = { "Iron Ingot", "Copper Ingot" };
    private readonly string[] pressIngredients = { "Iron Ingot", "Iron Pipe", "Copper Wire" };
    private readonly string[] universalExtractorIngredients = { "Iron Plate", "Iron Pipe", "Copper Wire", "Dark Matter" };
    private readonly string[] universalConduitIngredients = { "Iron Plate", "Iron Pipe", "Copper Wire", "Dark Matter" };
    private readonly string[] retrieverIngredients = { "Iron Plate", "Iron Pipe", "Copper Wire", "Electric Motor", "Circuit Board" };
    private readonly string[] generatorIngredients = { "Iron Plate", "Iron Pipe", "Copper Wire", "Copper Plate", "Glass Block" };
    private readonly string[] reactorTurbineIngredients = { "Generator", "Glass Block", "Steel Pipe", "Steel Gear", "Copper Wire", "Steel Plate" };
    private readonly string[] railCartHubIngredients = { "Iron Plate", "Iron Pipe", "Circuit Board" };
    private readonly string[] railCartIngredients = { "Electric Motor", "Copper Wire", "Tin Plate", "Aluminum Gear", "Storage Container", "Solar Panel" };
    private readonly string[] circuitBoardIngredients = { "Tin Plate", "Glass Block", "Copper Wire", "Dark Matter" };
    private readonly string[] electricMotorIngredients = { "Iron Plate", "Iron Pipe", "Iron Gear", "Copper Wire" };
    private readonly string[] autoCrafterIngredients = { "Bronze Gear", "Steel Plate", "Electric Motor", "Circuit Board", "Dark Matter" };
    private readonly string[] solarPanelIngredients = { "Iron Plate", "Iron Pipe", "Copper Wire", "Copper Plate", "Glass Block" };
    private readonly string[] powerConduitIngredients = { "Aluminum Plate", "Copper Wire", "Glass Block" };
    private readonly string[] nuclearReactorIngredients = { "Steel Plate", "Steel Pipe", "Copper Wire", "Copper Plate", "Glass Block", "Dark Matter" };
    private readonly string[] heatExchangerIngredients = { "Steel Plate", "Steel Pipe" };
    private readonly string[] smelterIngredients = { "Iron Pipe", "Iron Plate", "Copper Wire" };
    private readonly string[] gearCutterIngredients = { "Iron Plate", "Tin Plate", "Iron Pipe", "Aluminum Wire", "Copper Wire" };
    private readonly string[] storageComputerIngredients = { "Retriever", "Universal Conduit", "Aluminum Plate", "Copper Wire", "Tin Gear", "Dark Matter Conduit", "Glass Block" };
    private readonly string[] alloySmelterIngredients = { "Iron Plate", "Tin Plate", "Iron Pipe", "Iron Gear", "Aluminum Wire", "Copper Wire" };
    private readonly string[] turretIngredients = { "Steel Plate", "Steel Pipe", "Bronze Plate", "Steel Gear", "Aluminum Wire", "Copper Wire", "Electric Motor", "Circuit Board" };
    private readonly string[] darkMatterCollectorIngredients = { "Steel Plate", "Steel Pipe", "Tin Gear", "Steel Gear", "Bronze Gear", "Aluminum Wire", "Copper Wire", "Dark Matter" };
    private readonly string[] darkMatterConduitIngredients = { "Steel Plate", "Steel Pipe", "Tin Gear", "Steel Gear", "Bronze Gear", "Aluminum Wire", "Copper Wire", "Dark Matter" };

    void Start()
    {
        dictionary = new Dictionary<string, CraftingRecipe>
        {
            { "Iron Block", new CraftingRecipe(ironBlockIngredients, new int[] { 1 }, "Iron Block", 1) },
            { "Iron Ramp", new CraftingRecipe(ironBlockIngredients, new int[] { 1 }, "Iron Ramp", 1) },
            { "Steel Block", new CraftingRecipe(steelBlockIngredients, new int[] { 1 }, "Steel Block", 1) },
            { "Steel Ramp", new CraftingRecipe(steelBlockIngredients, new int[] { 1 }, "Steel Ramp", 1) },
            { "Quantum Hatchway", new CraftingRecipe(quantumHatchwayIngredients, new int[] { 1, 1 }, "Quantum Hatchway", 1)  },
            { "Electric Light", new CraftingRecipe(electricLightIngredients, new int[] { 1, 1, 2}, "Electric Light", 1) },
            { "Auger", new CraftingRecipe(augerIngredients, new int[] { 10, 10 }, "Auger", 1) },
            { "Storage Container", new CraftingRecipe(storageContainerIngredients,new int[] { 6 }, "Storage Container", 1) },
            { "Extruder", new CraftingRecipe(extruderIngredients, new int[] { 10, 10 }, "Extruder", 1) },
            { "Press", new CraftingRecipe(pressIngredients, new int[] { 10, 10, 10 }, "Press", 1) },
            { "Universal Extractor", new CraftingRecipe(universalExtractorIngredients, new int[] { 10, 10, 10, 10 }, "Universal Extractor", 1) },
            { "Universal Conduit", new CraftingRecipe(universalConduitIngredients, new int[] { 5, 5, 5, 5 }, "Universal Conduit", 1) },
            { "Retriever", new CraftingRecipe(retrieverIngredients, new int[] { 4 , 2, 4, 2, 2 }, "Retriever", 1) },
            { "Generator", new CraftingRecipe(generatorIngredients, new int[] { 4, 4, 4, 4, 4 }, "Solar Panel", 1) },
            { "Reactor Turbine", new CraftingRecipe(reactorTurbineIngredients, new int[] { 1, 1, 2, 2, 4, 4 }, "Reactor Turbine", 1) },
            { "Rail Cart Hub", new CraftingRecipe(railCartHubIngredients, new int[] { 6, 10, 1 }, "Rail Cart Hub", 1) },
            { "Rail Cart", new CraftingRecipe(railCartIngredients, new int[] { 2, 10, 4, 8, 1, 1 }, "Rail Cart", 1) },
            { "Circuit Board", new CraftingRecipe(circuitBoardIngredients, new int[] { 1, 1, 2, 1}, "Circuit Board", 1) },
            { "Electric Motor", new CraftingRecipe(electricMotorIngredients, new int[] { 2, 1, 2, 10}, "Electric Motor", 1) },
            { "Auto Crafter", new CraftingRecipe(autoCrafterIngredients, new int[] { 4, 4, 4, 4, 4 }, "Auto Crafter", 1) },
            { "Solar Panel", new CraftingRecipe(solarPanelIngredients, new int[] { 4, 4, 4, 4, 4 }, "Solar Panel", 1) },
            { "Power Conduit", new CraftingRecipe(powerConduitIngredients, new int[] { 4, 4, 4 }, "Power Conduit", 1) },
            { "Nuclear Reactor", new CraftingRecipe(nuclearReactorIngredients, new int[] { 10, 10, 10, 10, 10, 10 }, "Nuclear Reactor", 1) },
            { "Heat Exchanger", new CraftingRecipe(heatExchangerIngredients, new int[] { 10, 10 }, "Heat Exchanger", 1) },
            { "Smelter", new CraftingRecipe(smelterIngredients, new int[] { 5, 10, 10 }, "Smelter", 1) },
            { "Gear Cutter", new CraftingRecipe(gearCutterIngredients, new int[] { 5, 5, 5, 10, 10 }, "Gear Cutter", 1) },
            { "Storage Computer", new CraftingRecipe(storageComputerIngredients, new int[] { 5, 5, 5, 10, 10, 1, 1 }, "Storage Computer", 1) },
            { "Alloy Smelter", new CraftingRecipe(alloySmelterIngredients, new int[] { 20, 20, 20, 20, 40, 40 }, "Alloy Smelter", 1) },
            { "Turret", new CraftingRecipe(turretIngredients, new int[] { 5, 5, 5, 5, 10, 10, 4, 4 }, "Turret", 1)  },
            { "Dark Matter Collector", new CraftingRecipe(darkMatterCollectorIngredients, new int[] { 50, 50, 50, 50, 50, 100, 100, 100 }, "Dark Matter Conduit", 1)  },
            { "Dark Matter Conduit", new CraftingRecipe(darkMatterConduitIngredients, new int[] { 25, 25, 25, 25, 25, 50, 50, 50 }, "Dark Matter Conduit", 1) },
        };
    }
}
