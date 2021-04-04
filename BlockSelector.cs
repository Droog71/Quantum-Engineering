public class BlockSelector
{
    private PlayerController playerController;
    public string[] objectNames;
    private int selection;

    //! This class handles the building block selection at the top right corner of the screen.
    public BlockSelector(PlayerController playerController)
    {
        this.playerController = playerController;
        objectNames = new string[] 
        { 
            "Glass Block",
            "Brick", 
            "Iron Block",
            "Iron Ramp",
            "Steel Block",
            "Steel Ramp",
            "Quantum Hatchway",
            "Storage Container",
            "Storage Computer",
            "Electric Light",
            "Auger",
            "Extruder",
            "Press",
            "Smelter",
            "Universal Conduit",
            "Retriever",
            "Rail Cart Hub",
            "Rail Cart",
            "Universal Extractor",
            "Solar Panel",
            "Generator",
            "Nuclear Reactor",
            "Reactor Turbine",
            "Power Conduit",
            "Heat Exchanger",
            "Alloy Smelter",
            "Gear Cutter",
            "Auto Crafter",
            "Dark Matter Conduit",
            "Dark Matter Collector",
            "Turret",
            "Missile Turret"
        };
    }

    //! Applies changes.
    private void SetSelection()
    {
        playerController.buildType = objectNames[selection];
        playerController.previousBuildType = selection > 0 ? objectNames[selection - 1] : objectNames[objectNames.Length - 1];
        playerController.nextBuildType = selection == objectNames.Length - 1 ? objectNames[0] : objectNames[selection + 1];
        playerController.displayingBuildItem = true;
        playerController.buildItemDisplayTimer = 0;
        playerController.destroyTimer = 0;
        playerController.buildTimer = 0;
        playerController.PlayButtonSound();
    }

    //! Changes the currently selected building block.
    public void NextBlock()
    {
        selection = selection < objectNames.Length - 1 ? selection + 1 : 0;
        SetSelection();
    }

    //! Changes the currently selected building block.
    public void PreviousBlock()
    {
        selection = selection > 0 ? selection - 1 : objectNames.Length - 1;
        SetSelection();
    }
}
