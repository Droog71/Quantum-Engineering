public class BlockSelector
{
    private PlayerController pc;

    public BlockSelector(PlayerController pc)
    {
        this.pc = pc;
    }

    // Changes the currently selected building block.
    public void NextBlock()
    {
        if (pc.buildType.Equals("Glass Block"))
        {
            pc.buildType = "Brick";
            pc.previousBuildType = "Glass Block";
            pc.nextBuildType = "Iron Block";
        }
        else if (pc.buildType.Equals("Brick"))
        {
            pc.buildType = "Iron Block";
            pc.previousBuildType = "Brick";
            pc.nextBuildType = "Iron Ramp";
        }
        else if (pc.buildType.Equals("Iron Block"))
        {
            pc.buildType = "Iron Ramp";
            pc.previousBuildType = "Iron Block";
            pc.nextBuildType = "Steel Block";
        }
        else if (pc.buildType.Equals("Iron Ramp"))
        {
            pc.buildType = "Steel Block";
            pc.previousBuildType = "Iron Ramp";
            pc.nextBuildType = "Steel Ramp";
        }
        else if (pc.buildType.Equals("Steel Block"))
        {
            pc.buildType = "Steel Ramp";
            pc.previousBuildType = "Steel Block";
            pc.nextBuildType = "Quantum Hatchway";
        }
        else if (pc.buildType.Equals("Steel Ramp"))
        {
            pc.buildType = "Quantum Hatchway";
            pc.previousBuildType = "Steel Ramp";
            pc.nextBuildType = "Storage Container";
        }
        else if (pc.buildType.Equals("Quantum Hatchway"))
        {
            pc.buildType = "Storage Container";
            pc.previousBuildType = "Quantum Hatchway";
            pc.nextBuildType = "Storage Computer";
        }
        else if (pc.buildType.Equals("Storage Container"))
        {
            pc.buildType = "Storage Computer";
            pc.previousBuildType = "Storage Container";
            pc.nextBuildType = "Electric Light";
        }
        else if (pc.buildType.Equals("Storage Computer"))
        {
            pc.buildType = "Electric Light";
            pc.previousBuildType = "Storage Computer";
            pc.nextBuildType = "Auger";
        }
        else if (pc.buildType.Equals("Electric Light"))
        {
            pc.buildType = "Auger";
            pc.previousBuildType = "Electric Light";
            pc.nextBuildType = "Extruder";
        }
        else if (pc.buildType.Equals("Auger"))
        {
            pc.buildType = "Extruder";
            pc.previousBuildType = "Auger";
            pc.nextBuildType = "Press";
        }
        else if (pc.buildType.Equals("Extruder"))
        {
            pc.buildType = "Press";
            pc.previousBuildType = "Extruder";
            pc.nextBuildType = "Smelter";
        }
        else if (pc.buildType.Equals("Press"))
        {
            pc.buildType = "Smelter";
            pc.previousBuildType = "Press";
            pc.nextBuildType = "Universal Conduit";
        }
        else if (pc.buildType.Equals("Smelter"))
        {
            pc.buildType = "Universal Conduit";
            pc.previousBuildType = "Smelter";
            pc.nextBuildType = "Retriever";
        }
        else if (pc.buildType.Equals("Universal Conduit"))
        {
            pc.buildType = "Retriever";
            pc.previousBuildType = "Universal Conduit";
            pc.nextBuildType = "Rail Cart Hub";
        }
        else if (pc.buildType.Equals("Retriever"))
        {
            pc.buildType = "Rail Cart Hub";
            pc.previousBuildType = "Retriever";
            pc.nextBuildType = "Rail Cart";
        }
        else if (pc.buildType.Equals("Rail Cart Hub"))
        {
            pc.buildType = "Rail Cart";
            pc.previousBuildType = "Rail Cart Hub";
            pc.nextBuildType = "Universal Extractor";
        }
        else if (pc.buildType.Equals("Rail Cart"))
        {
            pc.buildType = "Universal Extractor";
            pc.previousBuildType = "Rail Cart";
            pc.nextBuildType = "Solar Panel";
        }
        else if (pc.buildType.Equals("Universal Extractor"))
        {
            pc.buildType = "Solar Panel";
            pc.previousBuildType = "Universal Extractor";
            pc.nextBuildType = "Generator";
        }
        else if (pc.buildType.Equals("Solar Panel"))
        {
            pc.buildType = "Generator";
            pc.previousBuildType = "Solar Panel";
            pc.nextBuildType = "Nuclear Reactor";
        }
        else if (pc.buildType.Equals("Generator"))
        {
            pc.buildType = "Nuclear Reactor";
            pc.previousBuildType = "Generator";
            pc.nextBuildType = "Reactor Turbine";
        }
        else if (pc.buildType.Equals("Nuclear Reactor"))
        {
            pc.buildType = "Reactor Turbine";
            pc.previousBuildType = "Nuclear Reactor";
            pc.nextBuildType = "Power Conduit";
        }
        else if (pc.buildType.Equals("Reactor Turbine"))
        {
            pc.buildType = "Power Conduit";
            pc.previousBuildType = "Reactor Turbine";
            pc.nextBuildType = "Heat Exchanger";
        }
        else if (pc.buildType.Equals("Power Conduit"))
        {
            pc.buildType = "Heat Exchanger";
            pc.previousBuildType = "Power Conduit";
            pc.nextBuildType = "Alloy Smelter";
        }
        else if (pc.buildType.Equals("Heat Exchanger"))
        {
            pc.buildType = "Alloy Smelter";
            pc.previousBuildType = "Heat Exchanger";
            pc.nextBuildType = "Gear Cutter";
        }
        else if (pc.buildType.Equals("Alloy Smelter"))
        {
            pc.buildType = "Gear Cutter";
            pc.previousBuildType = "Alloy Smelter";
            pc.nextBuildType = "Auto Crafter";
        }
        else if (pc.buildType.Equals("Gear Cutter"))
        {
            pc.buildType = "Auto Crafter";
            pc.previousBuildType = "Gear Cutter";
            pc.nextBuildType = "Dark Matter Conduit";
        }
        else if (pc.buildType.Equals("Auto Crafter"))
        {
            pc.buildType = "Dark Matter Conduit";
            pc.previousBuildType = "Auto Crafter";
            pc.nextBuildType = "Dark Matter Collector";
        }
        else if (pc.buildType.Equals("Dark Matter Conduit"))
        {
            pc.buildType = "Dark Matter Collector";
            pc.previousBuildType = "Dark Matter Conduit";
            pc.nextBuildType = "Turret";
        }
        else if (pc.buildType.Equals("Dark Matter Collector"))
        {
            pc.buildType = "Turret";
            pc.previousBuildType = "Dark Matter Collector";
            pc.nextBuildType = "Glass Block";
        }
        else if (pc.buildType.Equals("Turret"))
        {
            pc.buildType = "Glass Block";
            pc.previousBuildType = "Turret";
            pc.nextBuildType = "Iron Block";
        }
        pc.displayingBuildItem = true;
        pc.buildItemDisplayTimer = 0;
        pc.destroyTimer = 0;
        pc.buildTimer = 0;
        pc.PlayButtonSound();
    }

    // Changes the currently selected building block.
    public void PreviousBlock()
    {
        if (pc.buildType.Equals("Turret"))
        {
            pc.buildType = "Dark Matter Collector";
            pc.previousBuildType = "Dark Matter Conduit";
            pc.nextBuildType = "Turret";
        }
        else if (pc.buildType.Equals("Dark Matter Collector"))
        {
            pc.buildType = "Dark Matter Conduit";
            pc.previousBuildType = "Auto Crafter";
            pc.nextBuildType = "Dark Matter Collector";
        }
        else if (pc.buildType.Equals("Dark Matter Conduit"))
        {
            pc.buildType = "Auto Crafter";
            pc.previousBuildType = "Gear Cutter";
            pc.nextBuildType = "Dark Matter Conduit";
        }
        else if (pc.buildType.Equals("Auto Crafter"))
        {
            pc.buildType = "Gear Cutter";
            pc.previousBuildType = "Alloy Smelter";
            pc.nextBuildType = "Auto Crafter";
        }
        else if (pc.buildType.Equals("Gear Cutter"))
        {
            pc.buildType = "Alloy Smelter";
            pc.previousBuildType = "Heat Exchanger";
            pc.nextBuildType = "Gear Cutter";
        }
        else if (pc.buildType.Equals("Alloy Smelter"))
        {
            pc.buildType = "Heat Exchanger";
            pc.previousBuildType = "Power Conduit";
            pc.nextBuildType = "Alloy Smelter";
        }
        else if (pc.buildType.Equals("Heat Exchanger"))
        {
            pc.buildType = "Power Conduit";
            pc.previousBuildType = "Reactor Turbine";
            pc.nextBuildType = "Heat Exchanger";
        }
        else if (pc.buildType.Equals("Power Conduit"))
        {
            pc.buildType = "Reactor Turbine";
            pc.previousBuildType = "Nuclear Reactor";
            pc.nextBuildType = "Power Conduit";
        }
        else if (pc.buildType.Equals("Reactor Turbine"))
        {
            pc.buildType = "Nuclear Reactor";
            pc.previousBuildType = "Generator";
            pc.nextBuildType = "Reactor Turbine";
        }
        else if (pc.buildType.Equals("Nuclear Reactor"))
        {
            pc.buildType = "Generator";
            pc.previousBuildType = "Solar Panel";
            pc.nextBuildType = "Nuclear Reactor";
        }
        else if (pc.buildType.Equals("Generator"))
        {
            pc.buildType = "Solar Panel";
            pc.previousBuildType = "Universal Extractor";
            pc.nextBuildType = "Generator";
        }
        else if (pc.buildType.Equals("Solar Panel"))
        {
            pc.buildType = "Universal Extractor";
            pc.previousBuildType = "Rail Cart";
            pc.nextBuildType = "Solar Panel";
        }
        else if (pc.buildType.Equals("Universal Extractor"))
        {
            pc.buildType = "Rail Cart";
            pc.previousBuildType = "Rail Cart Hub";
            pc.nextBuildType = "Universal Extractor";
        }
        else if (pc.buildType.Equals("Rail Cart"))
        {
            pc.buildType = "Rail Cart Hub";
            pc.previousBuildType = "Retriever";
            pc.nextBuildType = "Rail Cart";
        }
        else if (pc.buildType.Equals("Rail Cart Hub"))
        {
            pc.buildType = "Retriever";
            pc.previousBuildType = "Universal Conduit";
            pc.nextBuildType = "Rail Cart Hub";
        }
        else if (pc.buildType.Equals("Retriever"))
        {
            pc.buildType = "Universal Conduit";
            pc.previousBuildType = "Smelter";
            pc.nextBuildType = "Retriever";
        }
        else if (pc.buildType.Equals("Universal Conduit"))
        {
            pc.buildType = "Smelter";
            pc.previousBuildType = "Press";
            pc.nextBuildType = "Universal Conduit";
        }
        else if (pc.buildType.Equals("Smelter"))
        {
            pc.buildType = "Press";
            pc.previousBuildType = "Extruder";
            pc.nextBuildType = "Smelter";
        }
        else if (pc.buildType.Equals("Press"))
        {
            pc.buildType = "Extruder";
            pc.previousBuildType = "Auger";
            pc.nextBuildType = "Press";
        }
        else if (pc.buildType.Equals("Extruder"))
        {
            pc.buildType = "Auger";
            pc.previousBuildType = "Electric Light";
            pc.nextBuildType = "Extruder";
        }
        else if (pc.buildType.Equals("Auger"))
        {
            pc.buildType = "Electric Light";
            pc.previousBuildType = "Storage Computer";
            pc.nextBuildType = "Auger";
        }
        else if (pc.buildType.Equals("Electric Light"))
        {
            pc.buildType = "Storage Computer";
            pc.previousBuildType = "Storage Container";
            pc.nextBuildType = "Electric Light";
        }
        else if (pc.buildType.Equals("Storage Computer"))
        {
            pc.buildType = "Storage Container";
            pc.previousBuildType = "Quantum Hatchway";
            pc.nextBuildType = "Storage Computer";
        }
        else if (pc.buildType.Equals("Storage Container"))
        {
            pc.buildType = "Quantum Hatchway";
            pc.previousBuildType = "Steel Ramp";
            pc.nextBuildType = "Storage Container";
        }
        else if (pc.buildType.Equals("Quantum Hatchway"))
        {
            pc.buildType = "Steel Ramp";
            pc.previousBuildType = "Steel Block";
            pc.nextBuildType = "Quantum Hatchway";
        }
        else if (pc.buildType.Equals("Steel Ramp"))
        {
            pc.buildType = "Steel Block";
            pc.previousBuildType = "Iron Ramp";
            pc.nextBuildType = "Steel Ramp";
        }
        else if (pc.buildType.Equals("Steel Block"))
        {
            pc.buildType = "Iron Ramp";
            pc.previousBuildType = "Iron Block";
            pc.nextBuildType = "Steel Block";
        }
        else if (pc.buildType.Equals("Iron Ramp"))
        {
            pc.buildType = "Iron Block";
            pc.previousBuildType = "Brick";
            pc.nextBuildType = "Iron Ramp";
        }
        else if (pc.buildType.Equals("Iron Block"))
        {
            pc.buildType = "Brick";
            pc.previousBuildType = "Glass Block";
            pc.nextBuildType = "Iron Block";
        }
        else if (pc.buildType.Equals("Brick"))
        {
            pc.buildType = "Glass Block";
            pc.previousBuildType = "Turret";
            pc.nextBuildType = "Iron Block";
        }
        else if (pc.buildType.Equals("Glass Block"))
        {
            pc.buildType = "Turret";
            pc.previousBuildType = "Dark Matter Collector";
            pc.nextBuildType = "Glass Block";
        }
        pc.displayingBuildItem = true;
        pc.buildItemDisplayTimer = 0;
        pc.destroyTimer = 0;
        pc.buildTimer = 0;
        pc.PlayButtonSound();
    }
}
