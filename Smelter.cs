﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Smelter : BasicMachine
{
    //! Called by unity engine on start up to initialize variables.
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

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BlockDictionary blockDictionary = new BlockDictionary(playerController);
        BasicMachineRecipe[] modRecipes = blockDictionary.GetMachineRecipes("Smelter");
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

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        base.UpdateMachine();
    }
}