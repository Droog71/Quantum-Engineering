using UnityEngine;

public class ModMachine : BasicMachine
{
    private Material material;
    public string machineName;

    //! Called by unity engine on start up to initialize variables.
    public new void Start()
    {
        base.Start();
        material = new Material(Shader.Find("Standard"));
    }

    //! Called once per frame by unity engine.
    public new void Update()
    {
        base.Update();
        TextureDictionary textureDictionary = GameObject.Find("Player").GetComponent<TextureDictionary>();
        if (textureDictionary.dictionary.ContainsKey(machineName))
        {
            material.mainTexture = GameObject.Find("Player").GetComponent<TextureDictionary>().dictionary[machineName];
        }
        GetComponent<Renderer>().material = material;
        if (recipes == null)
        {
            recipes = GameObject.Find("Player").GetComponent<BuildController>().blockDictionary.GetMachineRecipes(machineName);
        }
    }
}