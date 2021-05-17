using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GearCutter : BasicMachine
{
    //! Called by unity engine on start up to initialize variables.
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

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BlockDictionary blockDictionary = new BlockDictionary(playerController);
        BasicMachineRecipe[] modRecipes = blockDictionary.GetMachineRecipes("Gear Cutter");
        if (modRecipes != null)
        {
            List<BasicMachineRecipe> recipeList = recipes.ToList();
            foreach (BasicMachineRecipe recipe in modRecipes)
            {
                recipeList.Add(recipe);
            }
            recipes = recipeList.ToArray();
        }
    }

    //! Called once per frame by unity engine.
    public override void UpdateMachine()
    {
        base.UpdateMachine();
    }
}