using UnityEngine;

public class Extruder : BasicMachine
{
    // Called by unity engine on start up to initialize variables
    public new void Start()
    {
        base.Start();
        recipes = new BasicMachineRecipe[] 
        { 
            new BasicMachineRecipe("Copper Ingot", "Copper Wire"),
            new BasicMachineRecipe("Aluminum Ingot", "Aluminum Wire"),
            new BasicMachineRecipe("Iron Ingot", "Iron Pipe"),
            new BasicMachineRecipe("Steel Ingot", "Steel Pipe")
        };
    }

    // Called once per frame by unity engine
    public new void Update()
    {
        base.Update();
    }
}