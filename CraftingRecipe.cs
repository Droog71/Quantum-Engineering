using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingRecipe
{
    public string[] ingredients;
    public int[] amounts;
    public string output;
    public int outputAmount;

    public CraftingRecipe(string[] ingredients, int[] amounts, string output, int outputAmount)
    {
        this.ingredients = ingredients;
        this.amounts = amounts;
        this.output = output;
        this.outputAmount = outputAmount;
    }
}