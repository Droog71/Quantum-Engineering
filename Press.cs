using UnityEngine;

public class Press : BasicMachine
{
    // Called by unity engine on start up to initialize variables
    public new void Start()
    {
        base.Start();
        recipes = new BasicMachineRecipe[]
        {
            new BasicMachineRecipe("Copper Ingot", "Copper Plate"),
            new BasicMachineRecipe("Iron Ingot", "Iron Plate"),
            new BasicMachineRecipe("Tin Ingot", "Tin Plate"),
            new BasicMachineRecipe("Bronze Ingot", "Bronze Plate"),
            new BasicMachineRecipe("Steel Ingot", "Steel Plate"),
            new BasicMachineRecipe("Aluminum Ingot", "Aluminum Plate"),
            new BasicMachineRecipe("Regolith", "Brick")
        };
    }

    // Called once per frame by unity engine
    public new void Update()
    {
        base.Update();
    }
}