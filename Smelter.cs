using UnityEngine;

public class Smelter : BasicMachine
{
    // Called by unity engine on start up to initialize variables
    public new void Start()
    {
        base.Start();
        recipes = new BasicMachineRecipe[]
        {
            new BasicMachineRecipe("Copper Ore", "Copper Ingot"),
            new BasicMachineRecipe("Iron Ore", "Iron Ingot"),
            new BasicMachineRecipe("Tin Ore", "Tin Ingot"),
            new BasicMachineRecipe("Aluminum Ore", "Aluminum Ingot"),
            new BasicMachineRecipe("Regolith", "Glass Block")
        };
    }

    // Called once per frame by unity engine
    public new void Update()
    {
        base.Update();
    }
}