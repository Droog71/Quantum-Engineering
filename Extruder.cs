using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Extruder : BasicMachine
{
    //! Called by unity engine on start up to initialize variables.
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

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BlockDictionary blockDictionary = new BlockDictionary(playerController);
        BasicMachineRecipe[] modRecipes = blockDictionary.GetMachineRecipes("Extruder");
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