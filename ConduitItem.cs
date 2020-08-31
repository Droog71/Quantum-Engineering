using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConduitItem : MonoBehaviour
{
    public TextureDictionary textureDictionary;
    public Vector3 startPosition;
    public GameObject target;
    public GameObject machine;
    public GameObject billboard;
    public GameObject billboard2;
    public bool active;

    void Start()
    {
        textureDictionary = gameObject.AddComponent<TextureDictionary>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (active == true)
        {
            if (machine != null)
            {
                Texture billboardTexture = billboard.GetComponent<Renderer>().material.mainTexture;

                if (machine.GetComponent<AlloySmelter>() != null)
                {
                    if (machine.GetComponent<AlloySmelter>().outputObject != null)
                    {
                        target = machine.GetComponent<AlloySmelter>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<AlloySmelter>().outputType))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<AlloySmelter>().outputType];
                        }
                    }
                }

                if (machine.GetComponent<Auger>() != null)
                {
                    if (machine.GetComponent<Auger>().outputObject != null)
                    {
                        target = machine.GetComponent<Auger>().outputObject;
                        billboardTexture = textureDictionary.dictionary["Regolith"];
                    }
                }

                if (machine.GetComponent<AutoCrafter>() != null)
                {
                    if (machine.GetComponent<AutoCrafter>().inputObject != null)
                    {
                        target = machine.GetComponent<AutoCrafter>().inputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<AutoCrafter>().type))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<AutoCrafter>().type];
                        }
                    }
                }

                if (machine.GetComponent<DarkMatterCollector>() != null)
                {
                    if (machine.GetComponent<DarkMatterCollector>().outputObject != null)
                    {
                        target = machine.GetComponent<DarkMatterCollector>().outputObject;
                        billboardTexture = textureDictionary.dictionary["Dark Matter"];
                    }
                }

                if (machine.GetComponent<DarkMatterConduit>() != null)
                {
                    if (machine.GetComponent<DarkMatterConduit>().outputObject != null)
                    {
                        target = machine.GetComponent<DarkMatterConduit>().outputObject;
                        billboardTexture = textureDictionary.dictionary["Dark Matter"];
                    }
                }

                if (machine.GetComponent<Extruder>() != null)
                {
                    if (machine.GetComponent<Extruder>().outputObject != null)
                    {
                        target = machine.GetComponent<Extruder>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<Extruder>().outputType))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<Extruder>().outputType];
                        }
                    }
                }

                if (machine.GetComponent<GearCutter>() != null)
                {
                    if (machine.GetComponent<GearCutter>().outputObject != null)
                    {
                        target = machine.GetComponent<GearCutter>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<GearCutter>().outputType))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<GearCutter>().outputType];
                        }
                    }
                }

                if (machine.GetComponent<Press>() != null)
                {
                    if (machine.GetComponent<Press>().outputObject != null)
                    {
                        target = machine.GetComponent<Press>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<Press>().outputType))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<Press>().outputType];
                        }
                    }
                }

                if (machine.GetComponent<Retriever>() != null)
                {
                    if (machine.GetComponent<Retriever>().outputObject != null)
                    {
                        target = machine.GetComponent<Retriever>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(target.GetComponent<UniversalConduit>().type))
                        {
                            billboardTexture = textureDictionary.dictionary[target.GetComponent<UniversalConduit>().type];
                            billboard2.GetComponent<Renderer>().material.mainTexture = textureDictionary.dictionary[target.GetComponent<UniversalConduit>().type];
                        }
                    }
                }

                if (machine.GetComponent<Smelter>() != null)
                {
                    if (machine.GetComponent<Smelter>().outputObject != null)
                    {
                        target = machine.GetComponent<Smelter>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<Smelter>().outputType))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<Smelter>().outputType];
                        }
                    }
                }
                if (machine.GetComponent<UniversalConduit>() != null)
                {
                    if (machine.GetComponent<UniversalConduit>().outputObject != null)
                    {
                        target = machine.GetComponent<UniversalConduit>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<UniversalConduit>().type))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<UniversalConduit>().type];
                        }
                    }
                }

                if (machine.GetComponent<UniversalExtractor>() != null)
                {
                    if (machine.GetComponent<UniversalExtractor>().outputObject != null)
                    {
                        target = machine.GetComponent<UniversalExtractor>().outputObject;
                        if (textureDictionary.dictionary.ContainsKey(machine.GetComponent<UniversalExtractor>().type))
                        {
                            billboardTexture = textureDictionary.dictionary[machine.GetComponent<UniversalExtractor>().type];
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
            billboard.GetComponent<Renderer>().enabled = false;
        }
    }
}
