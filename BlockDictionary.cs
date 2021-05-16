using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

//! This class contains GameObject dictionaries for easily referencing different machines and other blocks.
public class BlockDictionary
{
    private PlayerController playerController;
    public Dictionary<string, GameObject> machineDictionary;
    public Dictionary<string, GameObject> blockDictionary;
    public Dictionary<string, Mesh> meshDictionary;
    public Dictionary<string, Type> typeDictionary;

    public BlockDictionary(PlayerController playerController)
    {
        this.playerController = playerController;
        Init();
    }

    //! Gets description for a specfic machine.
    public string GetMachineDescription(string machineName)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            Directory.CreateDirectory(machinePath);
            DirectoryInfo d = new DirectoryInfo(machinePath);
            foreach (FileInfo file in d.GetFiles("*.qe"))
            {
                string filePath = machinePath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string fileName = file.Name.Remove(file.Name.Length - 3);
                if (fileName == machineName)
                {
                    string modName = new DirectoryInfo(path).Name;
                    return fileContents.Split(']')[0].Substring(1);
                }
            }
        }
        Debug.Log("Failed to get description for [" + machineName + "]");
        return null;
    }

    //! Gets recipes for a specfic machine.
    public BasicMachineRecipe[] GetMachineRecipes(string machineName)
    {
        StateManager stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            Directory.CreateDirectory(machinePath);
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
                    if (!stateManager.modRecipeList.Contains(machineName))
                    {
                        stateManager.modRecipeList.Add(machineName);
                        Debug.Log("Mod "+"["+modName+"]"+" added recipes for [" + machineName + "]");
                    }
                    return machineRecipes;
                }
            }
        }
        return null;
    }

    //! Adds machines from mods to the game.
    public void AddModMachines(Dictionary<string, GameObject> dictionary)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string machinePath = path + "/Machines/";
            Directory.CreateDirectory(machinePath);
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
                    Debug.Log("Mod "+"["+modName+"]"+" created a new machine: [" + machineName + "]");
                }
            }
        }
    }

    //! Adds blocks from mods to the game.
    public void AddModBlocks(Dictionary<string, GameObject> dictionary)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string blockPath = path + "/Blocks/";
            Directory.CreateDirectory(blockPath);
            DirectoryInfo d = new DirectoryInfo(blockPath);
            foreach (FileInfo file in d.GetFiles("*.qe"))
            {
                string filePath = blockPath + file.Name;
                string fileContents = File.ReadAllText(filePath);
                string blockName = file.Name.Remove(file.Name.Length - 3);
                if (!dictionary.ContainsKey(blockName))
                {
                    dictionary.Add(blockName, playerController.modBlock);
                    List<string> objList = playerController.blockSelector.objectNames.ToList();
                    objList.Add(blockName);
                    playerController.blockSelector.objectNames = objList.ToArray();
                    string modName = new DirectoryInfo(path).Name;
                    playerController.gameManager.modBlockNames.Add(blockName);
                    Debug.Log("Mod "+"["+modName+"]"+" created a new block: [" + blockName + "]");
                }
            }
        }
        playerController.gameManager.InitModBlocks();
    }

    //! Adds blocks from mods to the game.
    public void AddModMeshes(Dictionary<string, Mesh> dictionary)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string meshPath = path + "/Models/";
            Directory.CreateDirectory(meshPath);
            DirectoryInfo d = new DirectoryInfo(meshPath);
            foreach (FileInfo file in d.GetFiles("*.obj"))
            {
                string filePath = meshPath + file.Name;
                string meshName = file.Name.Remove(file.Name.Length - 4);
                if (!dictionary.ContainsKey(meshName))
                {
                    ObjImporter importer = new ObjImporter();
                    Mesh newMesh = importer.ImportFile(filePath);
                    dictionary.Add(meshName, newMesh);
                    string modName = new DirectoryInfo(path).Name;
                    Debug.Log("Mod "+"["+modName+"]"+" created a new mesh: [" + meshName + "]");
                }
            }
        }
        playerController.gameManager.InitModBlocks();
    }

    //! Initializes variables.
    private void Init()
    {
        meshDictionary = new Dictionary<string, Mesh>();

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
            { "Quantum Hatchway", playerController.quantumHatchway },
            { "Door", playerController.door },
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
            { "Missile Turret", playerController.missileTurret },
            { "Universal Conduit", playerController.universalConduit },
            { "Universal Extractor", playerController.universalExtractor }
        };

        typeDictionary = new Dictionary<string, Type>
        {
            { "Door", typeof (Door) },
            { "Quantum Hatchway", typeof (Door) },
            { "Alloy Smelter", typeof (AlloySmelter) },
            { "Auger", typeof (Auger) },
            { "Auto Crafter", typeof (AutoCrafter) },
            { "Dark Matter Collector", typeof (DarkMatterCollector) },
            { "Dark Matter Conduit", typeof (DarkMatterConduit) },
            { "Electric Light", typeof (ElectricLight) },
            { "Extruder", typeof (Extruder) },
            { "Gear Cutter", typeof (GearCutter) },
            { "Generator", typeof (PowerSource) },
            { "Heat Exchanger", typeof (HeatExchanger) },
            { "Nuclear Reactor", typeof (NuclearReactor) },
            { "Power Conduit", typeof (PowerConduit) },
            { "Press", typeof (Press) },
            { "Rail Cart", typeof (RailCart) },
            { "Rail Cart Hub", typeof (RailCartHub) },
            { "Reactor Turbine", typeof (PowerSource) },
            { "Retriever", typeof (Retriever) },
            { "Smelter", typeof (Smelter) },
            { "Solar Panel", typeof (PowerSource) },
            { "Storage Computer", typeof (StorageComputer) },
            { "Storage Container", typeof (InventoryManager) },
            { "Turret", typeof (Turret) },
            { "Universal Conduit", typeof (UniversalConduit) },
            { "Universal Extractor", typeof (UniversalExtractor) },
            { "Brick", typeof (Brick) },
            { "Glass Block", typeof (Glass) },
            { "Iron Block", typeof (IronBlock) },
            { "Iron Ramp", typeof (IronBlock) },
            { "Steel Block", typeof (Steel) },
            { "Steel Ramp", typeof (Steel) }
        };
    }
}
