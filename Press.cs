using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Press : BasicMachine
{
    //! Called by unity engine on start up to initialize variables.
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

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BlockDictionary blockDictionary = new BlockDictionary(playerController);
        BasicMachineRecipe[] modRecipes = blockDictionary.GetMachineRecipes("Press");
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

    //! Called once per frame by unity engine
    public new void Update()
    {
        base.Update();
    }
}