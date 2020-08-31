using UnityEngine;
using System.Collections.Generic;

// This class provides the crafting recipe dictionary used by the crafting manager
public class CraftingDictionary : MonoBehaviour
{
    public Dictionary<string, CraftingRecipe> dictionary;

    void Start()
    {
        dictionary = new Dictionary<string, CraftingRecipe>
        {
            { "Iron Block", new CraftingRecipe(new string[] { "Iron Plate" }, new int[] { 1 }, "Iron Block", 1) },
            { "Iron Ramp", new CraftingRecipe(new string[] { "Iron Plate" }, new int[] { 1 }, "Iron Ramp", 1) },
            { "Steel Block", new CraftingRecipe(new string[] { "Steel Plate" }, new int[] { 1 }, "Steel Block", 1) },
            { "Steel Ramp", new CraftingRecipe(new string[] { "Steel Plate" }, new int[] { 1 }, "Steel Ramp", 1) },
            { "Quantum Hatchway", new CraftingRecipe(new string[] { "Tin Plate", "Dark Matter" }, new int[] { 1, 1 }, "Quantum Hatchway", 1)  },
            { "Electric Light", new CraftingRecipe(new string[] { "Glass Block", "Tin Plate", "Copper Wire" }, new int[] { 1, 1, 2}, "Electric Light", 1) },
            { "Auger", new CraftingRecipe(new string[] { "Iron Ingot", "Copper Ingot" }, new int[] { 10, 10 }, "Auger", 1) },
            { "Storage Container", new CraftingRecipe(new string[] { "Iron Plate" },new int[] { 6 }, "Storage Container", 1) },
            { "Extruder", new CraftingRecipe(new string[] { "Iron Ingot", "Copper Ingot" }, new int[] { 10, 10 }, "Extruder", 1) },
            { "Press", new CraftingRecipe(new string[] { "Iron Ingot", "Iron Pipe", "Copper Wire" }, new int[] { 10, 10, 10 }, "Press", 1) },
            { "Universal Extractor", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Copper Wire", "Dark Matter" }, new int[] { 10, 10, 10, 10 }, "Universal Extractor", 1) },
            { "Universal Conduit", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Copper Wire", "Dark Matter" }, new int[] { 5, 5, 5, 5 }, "Universal Conduit", 1) },
            { "Retriever", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Copper Wire", "Electric Motor", "Circuit Board" }, new int[] { 4 , 2, 4, 2, 2 }, "Retriever", 1) },
            { "Generator", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Copper Wire", "Copper Plate", "Glass Block" }, new int[] { 4, 4, 4, 4, 4 }, "Solar Panel", 1) },
            { "Reactor Turbine", new CraftingRecipe(new string[] { "Generator", "Glass Block", "Steel Pipe", "Steel Gear", "Copper Wire", "Steel Plate" }, new int[] { 1, 1, 2, 2, 4, 4 }, "Reactor Turbine", 1) },
            { "Rail Cart Hub", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Circuit Board" }, new int[] { 6, 10, 1 }, "Rail Cart Hub", 1) },
            { "Rail Cart", new CraftingRecipe(new string[] { "Electric Motor", "Copper Wire", "Tin Plate", "Aluminum Gear", "Storage Container", "Solar Panel" }, new int[] { 2, 10, 4, 8, 1, 1 }, "Rail Cart", 1) },
            { "Circuit Board", new CraftingRecipe(new string[] { "Tin Plate", "Glass Block", "Copper Wire", "Dark Matter" }, new int[] { 1, 1, 2, 1}, "Circuit Board", 1) },
            { "Motor", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Iron Gear", "Copper Wire" }, new int[] { 2, 1, 2, 10}, "Electric Motor", 1) },
            { "Auto Crafter", new CraftingRecipe(new string[] { "Bronze Gear", "Steel Plate", "Electric Motor", "Circuit Board", "Dark Matter" }, new int[] { 4, 4, 4, 4, 4 }, "Auto Crafter", 1) },
            { "Solar Panel", new CraftingRecipe(new string[] { "Iron Plate", "Iron Pipe", "Copper Wire", "Copper Plate", "Glass Block" }, new int[] { 4, 4, 4, 4, 4 }, "Solar Panel", 1) },
            { "Power Conduit", new CraftingRecipe(new string[] { "Aluminum Plate", "Copper Wire", "Glass Block" }, new int[] { 4, 4, 4 }, "Power Conduit", 1) },
            { "Nuclear Reactor", new CraftingRecipe(new string[] { "Steel Plate", "Steel Pipe", "Copper Wire", "Copper Plate", "Glass Block", "Dark Matter" }, new int[] { 10, 10, 10, 10, 10, 10 }, "Nuclear Reactor", 1) },
            { "Heat Exchanger", new CraftingRecipe(new string[] { "Steel Plate", "Steel Pipe" }, new int[] { 10, 10 }, "Heat Exchanger", 1) },
            { "Smelter", new CraftingRecipe(new string[] { "Iron Pipe", "Iron Plate", "Copper Wire" }, new int[] { 5, 10, 10 }, "Smelter", 1) },
            { "Gear Cutter", new CraftingRecipe(new string[] { "Iron Plate", "Tin Plate", "Iron Pipe", "Aluminum Wire", "Copper Wire" }, new int[] { 5, 5, 5, 10, 10 }, "Gear Cutter", 1) },
            { "Storage Computer", new CraftingRecipe(new string[] { "Retriever", "Universal Conduit", "Aluminum Plate", "Copper Wire", "Tin Gear", "Dark Matter Conduit", "Glass Block" }, new int[] { 5, 5, 5, 10, 10, 1, 1 }, "Storage Computer", 1) },
            { "Alloy Smelter", new CraftingRecipe(new string[] { "Iron Plate", "Tin Plate", "Iron Pipe", "Iron Gear", "Aluminum Wire", "Copper Wire" }, new int[] { 20, 20, 20, 20, 40, 40 }, "Alloy Smelter", 1) },
            { "Turret", new CraftingRecipe(new string[] { "Steel Plate", "Steel Pipe", "Bronze Plate", "Steel Gear", "Aluminum Wire", "Copper Wire", "Electric Motor", "Circuit Board" }, new int[] { 5, 5, 5, 5, 10, 10, 4, 4 }, "Turret", 1)  },
            { "Dark Matter Collector", new CraftingRecipe(new string[] { "Steel Plate", "Steel Pipe", "Tin Gear", "Steel Gear", "Bronze Gear", "Aluminum Wire", "Copper Wire", "Dark Matter" }, new int[] { 50, 50, 50, 50, 50, 100, 100, 100 }, "Dark Matter Conduit", 1)  },
            { "Dark Matter Conduit", new CraftingRecipe(new string[] { "Steel Plate", "Steel Pipe", "Tin Gear", "Steel Gear", "Bronze Gear", "Aluminum Wire", "Copper Wire", "Dark Matter" }, new int[] { 25, 25, 25, 25, 25, 50, 50, 50 }, "Dark Matter Conduit", 1) },
        };
    }
}
