using UnityEngine;
using System.Collections.Generic;
using System.IO;

// This class provides the crafting recipe dictionary used by the crafting manager
public class CraftingDictionary
{
    public Dictionary<string, CraftingRecipe> dictionary;
    public Dictionary<string, CraftingRecipe> modDictionary;

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

    private readonly int[] ironBlockAmounts = { 1 };
    private readonly int[] steelBlockAmounts = { 1 };
    private readonly int[] quantumHatchwayAmounts = { 1, 1 };
    private readonly int[] electricLightAmounts = { 1, 1, 2 };
    private readonly int[] augerAmounts = { 10, 10 };
    private readonly int[] storageContainerAmounts = { 6 };
    private readonly int[] extruderAmounts = { 10, 10 };
    private readonly int[] pressAmounts = { 10, 10, 10 };
    private readonly int[] universalExtractorAmounts = { 10, 10, 10, 10 };
    private readonly int[] universalConduitAmounts = { 5, 5, 5, 5 };
    private readonly int[] retrieverAmounts = { 4, 2, 4, 2, 2 };
    private readonly int[] generatorAmounts = { 4, 4, 4, 4, 4 };
    private readonly int[] reactorTurbineAmounts = { 1, 1, 2, 2, 4, 4 };
    private readonly int[] railCartHubAmounts = { 6, 10, 1 };
    private readonly int[] railCartAmounts = { 2, 10, 4, 8, 1, 1 };
    private readonly int[] circuitBoardAmounts = { 1, 1, 2, 1 };
    private readonly int[] electricMotorAmounts = { 2, 1, 2, 10 };
    private readonly int[] autoCrafterAmounts = { 4, 4, 4, 4, 4 };
    private readonly int[] solarPanelAmounts = { 4, 4, 4, 4, 4 };
    private readonly int[] powerConduitAmounts = { 4, 4, 4 };
    private readonly int[] nuclearReactorAmounts = { 10, 10, 10, 10, 10, 10 };
    private readonly int[] heatExchangerAmounts = { 10, 10 };
    private readonly int[] smelterAmounts = { 5, 10, 10 };
    private readonly int[] gearCutterAmounts = { 5, 5, 5, 10, 10 };
    private readonly int[] storageComputerAmounts = { 5, 5, 5, 10, 10, 1, 1 };
    private readonly int[] alloySmelterAmounts = { 20, 20, 20, 20, 40, 40 };
    private readonly int[] turretAmounts = { 5, 5, 5, 5, 10, 10, 4, 4 };
    private readonly int[] darkMatterCollectorAmounts = { 50, 50, 50, 50, 50, 100, 100, 100 };
    private readonly int[] darkMatterConduitAmounts = { 25, 25, 25, 25, 25, 50, 50, 50 };

    public CraftingDictionary()
    {
        dictionary = new Dictionary<string, CraftingRecipe>
        {
            { "Iron Block", new CraftingRecipe(ironBlockIngredients, ironBlockAmounts, "Iron Block", 10) },
            { "Iron Ramp", new CraftingRecipe(ironBlockIngredients, ironBlockAmounts, "Iron Ramp", 10) },
            { "Steel Block", new CraftingRecipe(steelBlockIngredients, steelBlockAmounts, "Steel Block", 10) },
            { "Steel Ramp", new CraftingRecipe(steelBlockIngredients, steelBlockAmounts, "Steel Ramp", 10) },
            { "Quantum Hatchway", new CraftingRecipe(quantumHatchwayIngredients, quantumHatchwayAmounts, "Quantum Hatchway", 1)  },
            { "Electric Light", new CraftingRecipe(electricLightIngredients, electricLightAmounts, "Electric Light", 1) },
            { "Auger", new CraftingRecipe(augerIngredients, augerAmounts, "Auger", 1) },
            { "Storage Container", new CraftingRecipe(storageContainerIngredients, storageContainerAmounts, "Storage Container", 1) },
            { "Extruder", new CraftingRecipe(extruderIngredients, extruderAmounts, "Extruder", 1) },
            { "Press", new CraftingRecipe(pressIngredients, pressAmounts, "Press", 1) },
            { "Universal Extractor", new CraftingRecipe(universalExtractorIngredients, universalExtractorAmounts, "Universal Extractor", 1) },
            { "Universal Conduit", new CraftingRecipe(universalConduitIngredients, universalConduitAmounts, "Universal Conduit", 1) },
            { "Retriever", new CraftingRecipe(retrieverIngredients, retrieverAmounts, "Retriever", 1) },
            { "Generator", new CraftingRecipe(generatorIngredients, generatorAmounts, "Solar Panel", 1) },
            { "Reactor Turbine", new CraftingRecipe(reactorTurbineIngredients, reactorTurbineAmounts, "Reactor Turbine", 1) },
            { "Rail Cart Hub", new CraftingRecipe(railCartHubIngredients, railCartHubAmounts, "Rail Cart Hub", 1) },
            { "Rail Cart", new CraftingRecipe(railCartIngredients, railCartAmounts, "Rail Cart", 1) },
            { "Circuit Board", new CraftingRecipe(circuitBoardIngredients, circuitBoardAmounts, "Circuit Board", 1) },
            { "Electric Motor", new CraftingRecipe(electricMotorIngredients, electricMotorAmounts, "Electric Motor", 1) },
            { "Auto Crafter", new CraftingRecipe(autoCrafterIngredients, autoCrafterAmounts, "Auto Crafter", 1) },
            { "Solar Panel", new CraftingRecipe(solarPanelIngredients, solarPanelAmounts, "Solar Panel", 1) },
            { "Power Conduit", new CraftingRecipe(powerConduitIngredients, powerConduitAmounts, "Power Conduit", 1) },
            { "Nuclear Reactor", new CraftingRecipe(nuclearReactorIngredients, nuclearReactorAmounts, "Nuclear Reactor", 1) },
            { "Heat Exchanger", new CraftingRecipe(heatExchangerIngredients, heatExchangerAmounts, "Heat Exchanger", 1) },
            { "Smelter", new CraftingRecipe(smelterIngredients, smelterAmounts, "Smelter", 1) },
            { "Gear Cutter", new CraftingRecipe(gearCutterIngredients, gearCutterAmounts, "Gear Cutter", 1) },
            { "Storage Computer", new CraftingRecipe(storageComputerIngredients, storageComputerAmounts, "Storage Computer", 1) },
            { "Alloy Smelter", new CraftingRecipe(alloySmelterIngredients, alloySmelterAmounts, "Alloy Smelter", 1) },
            { "Turret", new CraftingRecipe(turretIngredients, turretAmounts, "Turret", 1)  },
            { "Dark Matter Collector", new CraftingRecipe(darkMatterCollectorIngredients, darkMatterCollectorAmounts, "Dark Matter Conduit", 1)  },
            { "Dark Matter Conduit", new CraftingRecipe(darkMatterConduitIngredients, darkMatterConduitAmounts, "Dark Matter Conduit", 1) }
        };

        AddModRecipes();
    }

    // Gets mod recipes from file.
    public void AddModRecipes()
    {
        modDictionary = new Dictionary<string, CraftingRecipe>();
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Recipes/";
            DirectoryInfo info = new DirectoryInfo(machinePath);
            foreach (FileInfo file in info.GetFiles("*.qe"))
            {
                string filePath = machinePath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string output = file.Name.Remove(file.Name.Length - 3);
                string[] machineContents = fileContents.Split('}');
                string[] ingredients = machineContents[0].Split(',');
                string[] amountsString = machineContents[1].Split(',');
                int[] amounts = new int[amountsString.Length];
                for (int i = 0; i < amountsString.Length; i++)
                {
                    amounts[i] = int.Parse(amountsString[i]);
                }
                int outputAmount = int.Parse(machineContents[2]);
                CraftingRecipe modRecipe = new CraftingRecipe(ingredients, amounts, output, outputAmount);
                if (!dictionary.ContainsKey(output))
                {
                    modDictionary.Add(output, modRecipe);
                }
                else
                {
                    dictionary[output] = modRecipe;
                }
            }
        }
    }
}
