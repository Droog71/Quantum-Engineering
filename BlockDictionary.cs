using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

//This class contains GameObject dictionaries for easily referencing different machines and other blocks
public class BlockDictionary
{
    private PlayerController playerController;
    public Dictionary<string, GameObject> machineDictionary;
    public Dictionary<string, GameObject> blockDictionary;
    public Type[] objectTypes;

    public BlockDictionary(PlayerController playerController)
    {
        this.playerController = playerController;
        Init();
    }

    // Gets description for a specfic machine.
    public string GetMachineDescription(string machineName)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            DirectoryInfo d = new DirectoryInfo(machinePath);
            foreach (FileInfo file in d.GetFiles("*.qe"))
            {
                string filePath = machinePath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string fileName = file.Name.Remove(file.Name.Length - 3);
                if (fileName == machineName)
                {
                    string modName = new DirectoryInfo(path).Name;
                    Debug.Log(modName+" added description for [" + machineName + "]");
                    return fileContents.Split(']')[0].Substring(1);
                }
            }
        }
        Debug.Log("Failed to get description for [" + machineName + "]");
        return null;
    }

    // Gets recipes for a specfic machine.
    public BasicMachineRecipe[] GetMachineRecipes(string machineName)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            DirectoryInfo d = new DirectoryInfo(machinePath);
            foreach (FileInfo file in d.GetFiles("*.qe"))
            {
                string filePath = machinePath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string fileName = file.Name.Remove(file.Name.Length - 3);
                if (fileName == machineName)
                {
                    string recipeContents = fileContents.Split(']')[1];
                    string[] machineContents = recipeContents.Split('}');
                    BasicMachineRecipe[] machineRecipes = new BasicMachineRecipe[machineContents.Length - 1];
                    for (int i = 0; i < machineRecipes.Length; i++)
                    {
                        string input = machineContents[i].Split(':')[0];
                        string output = machineContents[i].Split(':')[1];
                        machineRecipes[i] = new BasicMachineRecipe(input, output);
                    }
                    string modName = new DirectoryInfo(path).Name;
                    Debug.Log(modName+" added recipes for [" + machineName + "]");
                    return machineRecipes;
                }
            }
        }
        Debug.Log("Failed to get recipes for [" + machineName + "]");
        return null;
    }

    // Adds machines from mods to the game.
    public void AddModMachines(Dictionary<string, GameObject> dictionary)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            DirectoryInfo d = new DirectoryInfo(machinePath);
            foreach (FileInfo file in d.GetFiles("*.qe"))
            {
                string filePath = machinePath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string machineName = file.Name.Remove(file.Name.Length - 3);
                if (!dictionary.ContainsKey(machineName))
                {
                    dictionary.Add(machineName, playerController.modMachine);
                    List<string> objList = playerController.blockSelector.objectNames.ToList();
                    objList.Add(machineName);
                    playerController.blockSelector.objectNames = objList.ToArray();
                    string modName = new DirectoryInfo(path).Name;
                    Debug.Log(modName+" created a new machine: [" + machineName + "]");
                }
            }
        }
    }

    // Initializes variables.
    private void Init()
    {
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
            { "Dark Matter Collector", playerController.darkMatterCollector },
            { "Dark Matter Conduit", playerController.darkMatterConduit },
            { "Electric Light", playerController.electricLight },
            { "Extruder", playerController.extruder },
            { "Gear Cutter", playerController.gearCutter },
            { "Generator", playerController.generator },
            { "Heat Exchanger", playerController.heatExchanger },
            { "Nuclear Reactor", playerController.nuclearReactor },
            { "Power Conduit", playerController.powerConduit },
            { "Press", playerController.press },
            { "Rail Cart", playerController.railCart },
            { "Rail Cart Hub", playerController.railCartHub },
            { "Reactor Turbine", playerController.reactorTurbine },
            { "Retriever", playerController.retriever },
            { "Smelter", playerController.smelter },
            { "Solar Panel", playerController.solarPanel },
            { "Storage Computer", playerController.storageComputer },
            { "Storage Container", playerController.storageContainer },
            { "Turret", playerController.turret },
            { "Universal Conduit", playerController.universalConduit },
            { "Universal Extractor", playerController.universalExtractor }
        };

        objectTypes = new Type[] 
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
