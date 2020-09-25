public class CraftingRecipe
{
    public string[] ingredients;
    public int[] amounts;
    public string output;
    public int outputAmount;

    //! Crafting recipes are used by both the player and the auto crafter machine.
    //! Ingredients and their amounts should be stored at the same index in their respective arrays.
    public CraftingRecipe(string[] ingredients, int[] amounts, string output, int outputAmount)
    {
        this.ingredients = ingredients;
        this.amounts = amounts;
        this.output = output;
        this.outputAmount = outputAmount;
    }
}