public class BlockSelector
{
    private PlayerController playerController;

    public BlockSelector(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    // Changes the currently selected building block.
    public void NextBlock()
    {
        if (playerController.buildType.Equals("Glass Block"))
        {
            playerController.buildType = "Brick";
            playerController.previousBuildType = "Glass Block";
            playerController.nextBuildType = "Iron Block";
        }
        else if (playerController.buildType.Equals("Brick"))
        {
            playerController.buildType = "Iron Block";
            playerController.previousBuildType = "Brick";
            playerController.nextBuildType = "Iron Ramp";
        }
        else if (playerController.buildType.Equals("Iron Block"))
        {
            playerController.buildType = "Iron Ramp";
            playerController.previousBuildType = "Iron Block";
            playerController.nextBuildType = "Steel Block";
        }
        else if (playerController.buildType.Equals("Iron Ramp"))
        {
            playerController.buildType = "Steel Block";
            playerController.previousBuildType = "Iron Ramp";
            playerController.nextBuildType = "Steel Ramp";
        }
        else if (playerController.buildType.Equals("Steel Block"))
        {
            playerController.buildType = "Steel Ramp";
            playerController.previousBuildType = "Steel Block";
            playerController.nextBuildType = "Quantum Hatchway";
        }
        else if (playerController.buildType.Equals("Steel Ramp"))
        {
            playerController.buildType = "Quantum Hatchway";
            playerController.previousBuildType = "Steel Ramp";
            playerController.nextBuildType = "Storage Container";
        }
        else if (playerController.buildType.Equals("Quantum Hatchway"))
        {
            playerController.buildType = "Storage Container";
            playerController.previousBuildType = "Quantum Hatchway";
            playerController.nextBuildType = "Storage Computer";
        }
        else if (playerController.buildType.Equals("Storage Container"))
        {
            playerController.buildType = "Storage Computer";
            playerController.previousBuildType = "Storage Container";
            playerController.nextBuildType = "Electric Light";
        }
        else if (playerController.buildType.Equals("Storage Computer"))
        {
            playerController.buildType = "Electric Light";
            playerController.previousBuildType = "Storage Computer";
            playerController.nextBuildType = "Auger";
        }
        else if (playerController.buildType.Equals("Electric Light"))
        {
            playerController.buildType = "Auger";
            playerController.previousBuildType = "Electric Light";
            playerController.nextBuildType = "Extruder";
        }
        else if (playerController.buildType.Equals("Auger"))
        {
            playerController.buildType = "Extruder";
            playerController.previousBuildType = "Auger";
            playerController.nextBuildType = "Press";
        }
        else if (playerController.buildType.Equals("Extruder"))
        {
            playerController.buildType = "Press";
            playerController.previousBuildType = "Extruder";
            playerController.nextBuildType = "Smelter";
        }
        else if (playerController.buildType.Equals("Press"))
        {
            playerController.buildType = "Smelter";
            playerController.previousBuildType = "Press";
            playerController.nextBuildType = "Universal Conduit";
        }
        else if (playerController.buildType.Equals("Smelter"))
        {
            playerController.buildType = "Universal Conduit";
            playerController.previousBuildType = "Smelter";
            playerController.nextBuildType = "Retriever";
        }
        else if (playerController.buildType.Equals("Universal Conduit"))
        {
            playerController.buildType = "Retriever";
            playerController.previousBuildType = "Universal Conduit";
            playerController.nextBuildType = "Rail Cart Hub";
        }
        else if (playerController.buildType.Equals("Retriever"))
        {
            playerController.buildType = "Rail Cart Hub";
            playerController.previousBuildType = "Retriever";
            playerController.nextBuildType = "Rail Cart";
        }
        else if (playerController.buildType.Equals("Rail Cart Hub"))
        {
            playerController.buildType = "Rail Cart";
            playerController.previousBuildType = "Rail Cart Hub";
            playerController.nextBuildType = "Universal Extractor";
        }
        else if (playerController.buildType.Equals("Rail Cart"))
        {
            playerController.buildType = "Universal Extractor";
            playerController.previousBuildType = "Rail Cart";
            playerController.nextBuildType = "Solar Panel";
        }
        else if (playerController.buildType.Equals("Universal Extractor"))
        {
            playerController.buildType = "Solar Panel";
            playerController.previousBuildType = "Universal Extractor";
            playerController.nextBuildType = "Generator";
        }
        else if (playerController.buildType.Equals("Solar Panel"))
        {
            playerController.buildType = "Generator";
            playerController.previousBuildType = "Solar Panel";
            playerController.nextBuildType = "Nuclear Reactor";
        }
        else if (playerController.buildType.Equals("Generator"))
        {
            playerController.buildType = "Nuclear Reactor";
            playerController.previousBuildType = "Generator";
            playerController.nextBuildType = "Reactor Turbine";
        }
        else if (playerController.buildType.Equals("Nuclear Reactor"))
        {
            playerController.buildType = "Reactor Turbine";
            playerController.previousBuildType = "Nuclear Reactor";
            playerController.nextBuildType = "Power Conduit";
        }
        else if (playerController.buildType.Equals("Reactor Turbine"))
        {
            playerController.buildType = "Power Conduit";
            playerController.previousBuildType = "Reactor Turbine";
            playerController.nextBuildType = "Heat Exchanger";
        }
        else if (playerController.buildType.Equals("Power Conduit"))
        {
            playerController.buildType = "Heat Exchanger";
            playerController.previousBuildType = "Power Conduit";
            playerController.nextBuildType = "Alloy Smelter";
        }
        else if (playerController.buildType.Equals("Heat Exchanger"))
        {
            playerController.buildType = "Alloy Smelter";
            playerController.previousBuildType = "Heat Exchanger";
            playerController.nextBuildType = "Gear Cutter";
        }
        else if (playerController.buildType.Equals("Alloy Smelter"))
        {
            playerController.buildType = "Gear Cutter";
            playerController.previousBuildType = "Alloy Smelter";
            playerController.nextBuildType = "Auto Crafter";
        }
        else if (playerController.buildType.Equals("Gear Cutter"))
        {
            playerController.buildType = "Auto Crafter";
            playerController.previousBuildType = "Gear Cutter";
            playerController.nextBuildType = "Dark Matter Conduit";
        }
        else if (playerController.buildType.Equals("Auto Crafter"))
        {
            playerController.buildType = "Dark Matter Conduit";
            playerController.previousBuildType = "Auto Crafter";
            playerController.nextBuildType = "Dark Matter Collector";
        }
        else if (playerController.buildType.Equals("Dark Matter Conduit"))
        {
            playerController.buildType = "Dark Matter Collector";
            playerController.previousBuildType = "Dark Matter Conduit";
            playerController.nextBuildType = "Turret";
        }
        else if (playerController.buildType.Equals("Dark Matter Collector"))
        {
            playerController.buildType = "Turret";
            playerController.previousBuildType = "Dark Matter Collector";
            playerController.nextBuildType = "Glass Block";
        }
        else if (playerController.buildType.Equals("Turret"))
        {
            playerController.buildType = "Glass Block";
            playerController.previousBuildType = "Turret";
            playerController.nextBuildType = "Iron Block";
        }
        playerController.displayingBuildItem = true;
        playerController.buildItemDisplayTimer = 0;
        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
        playerController.PlayButtonSound();
    }

    // Changes the currently selected building block.
    public void PreviousBlock()
    {
        if (playerController.buildType.Equals("Turret"))
        {
            playerController.buildType = "Dark Matter Collector";
            playerController.previousBuildType = "Dark Matter Conduit";
            playerController.nextBuildType = "Turret";
        }
        else if (playerController.buildType.Equals("Dark Matter Collector"))
        {
            playerController.buildType = "Dark Matter Conduit";
            playerController.previousBuildType = "Auto Crafter";
            playerController.nextBuildType = "Dark Matter Collector";
        }
        else if (playerController.buildType.Equals("Dark Matter Conduit"))
        {
            playerController.buildType = "Auto Crafter";
            playerController.previousBuildType = "Gear Cutter";
            playerController.nextBuildType = "Dark Matter Conduit";
        }
        else if (playerController.buildType.Equals("Auto Crafter"))
        {
            playerController.buildType = "Gear Cutter";
            playerController.previousBuildType = "Alloy Smelter";
            playerController.nextBuildType = "Auto Crafter";
        }
        else if (playerController.buildType.Equals("Gear Cutter"))
        {
            playerController.buildType = "Alloy Smelter";
            playerController.previousBuildType = "Heat Exchanger";
            playerController.nextBuildType = "Gear Cutter";
        }
        else if (playerController.buildType.Equals("Alloy Smelter"))
        {
            playerController.buildType = "Heat Exchanger";
            playerController.previousBuildType = "Power Conduit";
            playerController.nextBuildType = "Alloy Smelter";
        }
        else if (playerController.buildType.Equals("Heat Exchanger"))
        {
            playerController.buildType = "Power Conduit";
            playerController.previousBuildType = "Reactor Turbine";
            playerController.nextBuildType = "Heat Exchanger";
        }
        else if (playerController.buildType.Equals("Power Conduit"))
        {
            playerController.buildType = "Reactor Turbine";
            playerController.previousBuildType = "Nuclear Reactor";
            playerController.nextBuildType = "Power Conduit";
        }
        else if (playerController.buildType.Equals("Reactor Turbine"))
        {
            playerController.buildType = "Nuclear Reactor";
            playerController.previousBuildType = "Generator";
            playerController.nextBuildType = "Reactor Turbine";
        }
        else if (playerController.buildType.Equals("Nuclear Reactor"))
        {
            playerController.buildType = "Generator";
            playerController.previousBuildType = "Solar Panel";
            playerController.nextBuildType = "Nuclear Reactor";
        }
        else if (playerController.buildType.Equals("Generator"))
        {
            playerController.buildType = "Solar Panel";
            playerController.previousBuildType = "Universal Extractor";
            playerController.nextBuildType = "Generator";
        }
        else if (playerController.buildType.Equals("Solar Panel"))
        {
            playerController.buildType = "Universal Extractor";
            playerController.previousBuildType = "Rail Cart";
            playerController.nextBuildType = "Solar Panel";
        }
        else if (playerController.buildType.Equals("Universal Extractor"))
        {
            playerController.buildType = "Rail Cart";
            playerController.previousBuildType = "Rail Cart Hub";
            playerController.nextBuildType = "Universal Extractor";
        }
        else if (playerController.buildType.Equals("Rail Cart"))
        {
            playerController.buildType = "Rail Cart Hub";
            playerController.previousBuildType = "Retriever";
            playerController.nextBuildType = "Rail Cart";
        }
        else if (playerController.buildType.Equals("Rail Cart Hub"))
        {
            playerController.buildType = "Retriever";
            playerController.previousBuildType = "Universal Conduit";
            playerController.nextBuildType = "Rail Cart Hub";
        }
        else if (playerController.buildType.Equals("Retriever"))
        {
            playerController.buildType = "Universal Conduit";
            playerController.previousBuildType = "Smelter";
            playerController.nextBuildType = "Retriever";
        }
        else if (playerController.buildType.Equals("Universal Conduit"))
        {
            playerController.buildType = "Smelter";
            playerController.previousBuildType = "Press";
            playerController.nextBuildType = "Universal Conduit";
        }
        else if (playerController.buildType.Equals("Smelter"))
        {
            playerController.buildType = "Press";
            playerController.previousBuildType = "Extruder";
            playerController.nextBuildType = "Smelter";
        }
        else if (playerController.buildType.Equals("Press"))
        {
            playerController.buildType = "Extruder";
            playerController.previousBuildType = "Auger";
            playerController.nextBuildType = "Press";
        }
        else if (playerController.buildType.Equals("Extruder"))
        {
            playerController.buildType = "Auger";
            playerController.previousBuildType = "Electric Light";
            playerController.nextBuildType = "Extruder";
        }
        else if (playerController.buildType.Equals("Auger"))
        {
            playerController.buildType = "Electric Light";
            playerController.previousBuildType = "Storage Computer";
            playerController.nextBuildType = "Auger";
        }
        else if (playerController.buildType.Equals("Electric Light"))
        {
            playerController.buildType = "Storage Computer";
            playerController.previousBuildType = "Storage Container";
            playerController.nextBuildType = "Electric Light";
        }
        else if (playerController.buildType.Equals("Storage Computer"))
        {
            playerController.buildType = "Storage Container";
            playerController.previousBuildType = "Quantum Hatchway";
            playerController.nextBuildType = "Storage Computer";
        }
        else if (playerController.buildType.Equals("Storage Container"))
        {
            playerController.buildType = "Quantum Hatchway";
            playerController.previousBuildType = "Steel Ramp";
            playerController.nextBuildType = "Storage Container";
        }
        else if (playerController.buildType.Equals("Quantum Hatchway"))
        {
            playerController.buildType = "Steel Ramp";
            playerController.previousBuildType = "Steel Block";
            playerController.nextBuildType = "Quantum Hatchway";
        }
        else if (playerController.buildType.Equals("Steel Ramp"))
        {
            playerController.buildType = "Steel Block";
            playerController.previousBuildType = "Iron Ramp";
            playerController.nextBuildType = "Steel Ramp";
        }
        else if (playerController.buildType.Equals("Steel Block"))
        {
            playerController.buildType = "Iron Ramp";
            playerController.previousBuildType = "Iron Block";
            playerController.nextBuildType = "Steel Block";
        }
        else if (playerController.buildType.Equals("Iron Ramp"))
        {
            playerController.buildType = "Iron Block";
            playerController.previousBuildType = "Brick";
            playerController.nextBuildType = "Iron Ramp";
        }
        else if (playerController.buildType.Equals("Iron Block"))
        {
            playerController.buildType = "Brick";
            playerController.previousBuildType = "Glass Block";
            playerController.nextBuildType = "Iron Block";
        }
        else if (playerController.buildType.Equals("Brick"))
        {
            playerController.buildType = "Glass Block";
            playerController.previousBuildType = "Turret";
            playerController.nextBuildType = "Iron Block";
        }
        else if (playerController.buildType.Equals("Glass Block"))
        {
            playerController.buildType = "Turret";
            playerController.previousBuildType = "Dark Matter Collector";
            playerController.nextBuildType = "Glass Block";
        }
        playerController.displayingBuildItem = true;
        playerController.buildItemDisplayTimer = 0;
        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
        playerController.PlayButtonSound();
    }
}
