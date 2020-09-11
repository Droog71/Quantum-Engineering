using UnityEngine;

public class GearCutter : BasicMachine
{
    // Called by unity engine on start up to initialize variables
    public new void Start()
    {
        base.Start();
        recipes = new BasicMachineRecipe[]
        {
            new BasicMachineRecipe("Copper Plate", "Copper Gear"),
            new BasicMachineRecipe("Iron Plate", "Iron Gear"),
            new BasicMachineRecipe("Tin Plate", "Tin Gear"),
            new BasicMachineRecipe("Bronze Plate", "Bronze Gear"),
            new BasicMachineRecipe("Steel Plate", "Steel Gear"),
            new BasicMachineRecipe("Aluminum Plate", "Aluminum Gear")
        };
    }

    // Called once per frame by unity engine
    public new void Update()
    {
        base.Update();
    }
}