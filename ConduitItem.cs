using UnityEngine;
using System.Collections.Generic;

public class ConduitItem : MonoBehaviour
{
    private StateManager stateManager;
    private GameManager gameManager;
    public Vector3 startPosition;
    public Dictionary<string, Texture2D> textureDictionary;
    public GameObject target;
    public GameObject machine;
    public GameObject billboard;
    public GameObject billboard2;
    public bool active;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        gameManager = FindObjectOfType<GameManager>();
        startPosition = transform.position;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (active == true && textureDictionary != null)
            {
                if (machine != null)
                {
                    if (machine.GetComponent<AlloySmelter>() != null)
                    {
                        if (machine.GetComponent<AlloySmelter>().outputObject != null)
                        {
                            target = machine.GetComponent<AlloySmelter>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<AlloySmelter>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<AlloySmelter>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<AlloySmelter>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<AlloySmelter>().outputType];
                            }
                        }
                    }

                    if (machine.GetComponent<Auger>() != null)
                    {
                        if (machine.GetComponent<Auger>().outputObject != null)
                        {
                            target = machine.GetComponent<Auger>().outputObject;
                            billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary["Regolith"];
                        }
                    }

                    if (machine.GetComponent<AutoCrafter>() != null)
                    {
                        if (machine.GetComponent<AutoCrafter>().inputObject != null)
                        {
                            target = machine.GetComponent<AutoCrafter>().inputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<AutoCrafter>().type + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<AutoCrafter>().type + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<AutoCrafter>().type))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<AutoCrafter>().type];
                            }
                        }
                    }

                    if (machine.GetComponent<DarkMatterCollector>() != null)
                    {
                        if (machine.GetComponent<DarkMatterCollector>().outputObject != null)
                        {
                            target = machine.GetComponent<DarkMatterCollector>().outputObject;
                            billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary["Dark Matter"];
                        }
                    }

                    if (machine.GetComponent<DarkMatterConduit>() != null)
                    {
                        if (machine.GetComponent<DarkMatterConduit>().outputObject != null)
                        {
                            target = machine.GetComponent<DarkMatterConduit>().outputObject;
                            billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary["Dark Matter"];
                        }
                    }

                    if (machine.GetComponent<Extruder>() != null)
                    {
                        if (machine.GetComponent<Extruder>().outputObject != null)
                        {
                            target = machine.GetComponent<Extruder>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<Extruder>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Extruder>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<Extruder>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Extruder>().outputType];
                            }
                        }
                    }

                    if (machine.GetComponent<GearCutter>() != null)
                    {
                        if (machine.GetComponent<GearCutter>().outputObject != null)
                        {
                            target = machine.GetComponent<GearCutter>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<GearCutter>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<GearCutter>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<GearCutter>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<GearCutter>().outputType];
                            }
                        }
                    }

                    if (machine.GetComponent<Press>() != null)
                    {
                        if (machine.GetComponent<Press>().outputObject != null)
                        {
                            target = machine.GetComponent<Press>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<Press>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Press>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<Press>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Press>().outputType];
                            }
                        }
                    }

                    if (machine.GetComponent<ModMachine>() != null)
                    {
                        if (machine.GetComponent<ModMachine>().outputObject != null)
                        {
                            target = machine.GetComponent<ModMachine>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<ModMachine>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<ModMachine>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<ModMachine>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<ModMachine>().outputType];
                            }
                        }
                    }

                    if (machine.GetComponent<Retriever>() != null)
                    {
                        if (machine.GetComponent<Retriever>().outputObject != null)
                        {
                            target = machine.GetComponent<Retriever>().outputObject;
                            if (textureDictionary.ContainsKey(target.GetComponent<UniversalConduit>().type + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[target.GetComponent<UniversalConduit>().type + "_Icon"];
                                billboard2.GetComponent<Renderer>().material.mainTexture = textureDictionary[target.GetComponent<UniversalConduit>().type + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(target.GetComponent<UniversalConduit>().type))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[target.GetComponent<UniversalConduit>().type];
                                billboard2.GetComponent<Renderer>().material.mainTexture = textureDictionary[target.GetComponent<UniversalConduit>().type];
                            }
                        }
                    }

                    if (machine.GetComponent<Smelter>() != null)
                    {
                        if (machine.GetComponent<Smelter>().outputObject != null)
                        {
                            target = machine.GetComponent<Smelter>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<Smelter>().outputType + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Smelter>().outputType + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<Smelter>().outputType))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<Smelter>().outputType];
                            }
                        }
                    }
                    if (machine.GetComponent<UniversalConduit>() != null)
                    {
                        if (machine.GetComponent<UniversalConduit>().outputObject != null)
                        {
                            target = machine.GetComponent<UniversalConduit>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<UniversalConduit>().type + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<UniversalConduit>().type + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<UniversalConduit>().type))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<UniversalConduit>().type];
                            }
                        }
                    }

                    if (machine.GetComponent<UniversalExtractor>() != null)
                    {
                        if (machine.GetComponent<UniversalExtractor>().outputObject != null)
                        {
                            target = machine.GetComponent<UniversalExtractor>().outputObject;
                            if (textureDictionary.ContainsKey(machine.GetComponent<UniversalExtractor>().type + "_Icon"))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<UniversalExtractor>().type + "_Icon"];
                            }
                            else if (textureDictionary.ContainsKey(machine.GetComponent<UniversalExtractor>().type))
                            {
                                billboard.GetComponent<Renderer>().material.mainTexture = textureDictionary[machine.GetComponent<UniversalExtractor>().type];
                            }
                        }
                    }
                }

                if (target != null)
                {
                    billboard.GetComponent<Renderer>().enabled = true;
                    transform.LookAt(target.transform.position);
                    transform.position += transform.forward * 10 * Time.deltaTime;
                    if (Vector3.Distance(transform.position, target.transform.position) < 1)
                    {
                        transform.position = startPosition;
                    }
                }
                else
                {
                    billboard.GetComponent<Renderer>().enabled = false;
                }
            }
            else
            {
                transform.position = startPosition;
                billboard.GetComponent<Renderer>().enabled = false;
                textureDictionary = gameManager.GetComponent<TextureDictionary>().dictionary;
            }
        }
    }
}
