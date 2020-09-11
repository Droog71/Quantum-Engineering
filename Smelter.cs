using UnityEngine;

public class Smelter : BasicMachine
{
    public GameObject fireObject;

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

    // For mod blocks.
    public override void UpdateTick()
    {
        Debug.Log("Update tick!");
        if (GetComponent<Light>().enabled == true && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
            fireObject.SetActive(true);
        }
        else
        {
            fireObject.SetActive(false);
        }
    }
}