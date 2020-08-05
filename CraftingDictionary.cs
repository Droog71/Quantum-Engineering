using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingDictionary : MonoBehaviour
{
    public Dictionary<string, bool> machineCraftingDictionary;
    public Dictionary<string, bool> computerCraftingDictionary;
    private MachineCrafting machineCrafting;
    private ComputerCrafting computerCrafting;

    public CraftingDictionary(MachineCrafting machineCrafting, ComputerCrafting computerCrafting)
    {
        this.machineCrafting = machineCrafting;
        this.computerCrafting = computerCrafting;
    }

    void Start()
    {
        machineCraftingDictionary = new Dictionary<string, bool>
        {
            { "CraftIronBlock", machineCrafting.CraftIronBlock() },
            { "CraftIronRamp", machineCrafting.CraftIronRamp() },
            { "CraftSteelBlock", machineCrafting.CraftSteelBlock() },
            { "CraftSteelRamp", machineCrafting.CraftSteelRamp() },
            { "CraftQuantumHatchway", machineCrafting.CraftQuantumHatchway() },
            { "CraftElectricLight", machineCrafting.CraftElectricLight() },
            { "CraftAuger", machineCrafting.CraftAuger() },
            { "CraftStorageContainer", machineCrafting.CraftStorageContainer() },
            { "CraftExtruder", machineCrafting.CraftExtruder() },
            { "CraftPress", machineCrafting.CraftPress() },
            { "CraftUniversalExtractor", machineCrafting.CraftUniversalExtractor() },
            { "CraftUniversalConduit", machineCrafting.CraftUniversalConduit() },
            { "CraftRetriever", machineCrafting.CraftRetriever() },
            { "CraftGenerator", machineCrafting.CraftGenerator() },
            { "CraftReactorTurbine", machineCrafting.CraftReactorTurbine() },
            { "CraftRailCartHub", machineCrafting.CraftRailCartHub() },
            { "CraftRailCart", machineCrafting.CraftRailCart() },
            { "CraftCircuitBoard", machineCrafting.CraftCircuitBoard() },
            { "CraftMotor", machineCrafting.CraftMotor() },
            { "CraftAutoCrafter", machineCrafting.CraftAutoCrafter() },
            { "CraftSolarPanel", machineCrafting.CraftSolarPanel() },
            { "CraftPowerConduit", machineCrafting.CraftPowerConduit() },
            { "CraftNuclearReactor", machineCrafting.CraftNuclearReactor() },
            { "CraftHeatExchanger", machineCrafting.CraftHeatExchanger() },
            { "CraftSmelter", machineCrafting.CraftSmelter() },
            { "CraftGearCutter", machineCrafting.CraftGearCutter() },
            { "CraftStorageComputer", machineCrafting.CraftStorageComputer() },
            { "CraftAlloySmelter", machineCrafting.CraftAlloySmelter() },
            { "CraftTurret", machineCrafting.CraftTurret() },
            { "CraftDarkMatterCollector", machineCrafting.CraftDarkMatterCollector() },
            { "CraftDarkMatterConduit", machineCrafting.CraftDarkMatterConduit() },
        };

        computerCraftingDictionary = new Dictionary<string, bool>
        {
            { "CraftIronBlock", computerCrafting.CraftIronBlock() },
            { "CraftIronRamp", computerCrafting.CraftIronRamp() },
            { "CraftSteelBlock", computerCrafting.CraftSteelBlock() },
            { "CraftSteelRamp", computerCrafting.CraftSteelRamp() },
            { "CraftQuantumHatchway", computerCrafting.CraftQuantumHatchway() },
            { "CraftElectricLight", computerCrafting.CraftElectricLight() },
            { "CraftAuger", computerCrafting.CraftAuger() },
            { "CraftStorageContainer", computerCrafting.CraftStorageContainer() },
            { "CraftExtruder", computerCrafting.CraftExtruder() },
            { "CraftPress", computerCrafting.CraftPress() },
            { "CraftUniversalExtractor", computerCrafting.CraftUniversalExtractor() },
            { "CraftUniversalConduit", computerCrafting.CraftUniversalConduit() },
            { "CraftRetriever", computerCrafting.CraftRetriever() },
            { "CraftGenerator", computerCrafting.CraftGenerator() },
            { "CraftReactorTurbine", computerCrafting.CraftReactorTurbine() },
            { "CraftRailCartHub", computerCrafting.CraftRailCartHub() },
            { "CraftRailCart", computerCrafting.CraftRailCart() },
            { "CraftCircuitBoard", computerCrafting.CraftCircuitBoard() },
            { "CraftMotor", computerCrafting.CraftMotor() },
            { "CraftAutoCrafter", computerCrafting.CraftAutoCrafter() },
            { "CraftSolarPanel", computerCrafting.CraftSolarPanel() },
            { "CraftPowerConduit", computerCrafting.CraftPowerConduit() },
            { "CraftNuclearReactor", computerCrafting.CraftNuclearReactor() },
            { "CraftHeatExchanger", computerCrafting.CraftHeatExchanger() },
            { "CraftSmelter", computerCrafting.CraftSmelter() },
            { "CraftGearCutter", computerCrafting.CraftGearCutter() },
            { "CraftStorageComputer", computerCrafting.CraftStorageComputer() },
            { "CraftAlloySmelter", computerCrafting.CraftAlloySmelter() },
            { "CraftTurret", computerCrafting.CraftTurret() },
            { "CraftDarkMatterCollector", computerCrafting.CraftDarkMatterCollector() },
            { "CraftDarkMatterConduit", computerCrafting.CraftDarkMatterConduit() },
        };
    }
}
