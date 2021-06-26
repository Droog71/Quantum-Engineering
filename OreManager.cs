using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class OreManager : MonoBehaviour
{
    public GameObject ironOre;
    public GameObject copperOre;
    public GameObject tinOre;
    public GameObject aluminumOre;
    public GameObject coal;
    public GameObject ice;
    public GameObject darkMatter;
    private GameObject[] ores;
    private Coroutine updateCoroutine;
    private bool coroutineBusy;
    private List<Vector3> orePositions;
    private List<int> oreTypes;
    private bool loaded;
    public bool paused;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (orePositions == null)
        {
            orePositions = new List<Vector3>();
        }

        if (oreTypes == null)
        {
            oreTypes = new List<int>();
        }

        if (ores == null)
        {
            ores = new GameObject[]
            {
                ironOre,
                copperOre,
                tinOre,
                aluminumOre,
                coal,
                ice,
                darkMatter
            };
        }

        if (!coroutineBusy && paused == false)
        {
            updateCoroutine = StartCoroutine(ManageOre());
        }
    }

    //! Spawns ore in procedural worlds.
    private IEnumerator ManageOre()
    {
        coroutineBusy = true;

        if (FileBasedPrefs.GetBool(GetComponent<StateManager>().worldName + "oldWorld") == true && loaded == false)
        {
            if (GetComponent<StateManager>().worldLoaded == true)
            {
                Vector3[] posArray = PlayerPrefsX.GetVector3Array(GetComponent<StateManager>().worldName + "orePositions");
                int[] typeArray = PlayerPrefsX.GetIntArray(GetComponent<StateManager>().worldName + "oreTypes");

                orePositions = posArray.ToList();
                oreTypes = typeArray.ToList();

                if (posArray.Length > 0)
                {
                    for (int i = 0; i < posArray.Length; i++)
                    {
                        GameObject spawnedObject = Instantiate(ores[typeArray[i]], posArray[i], new Quaternion());
                    }
                }

                loaded = true;
            }
        }

        if (GetComponent<StateManager>().worldLoaded == true && GetComponent<TerrainGenerator>().initialized == true)
        {
            BlockHolder[] blockHolders = GetComponent<GameManager>().builtObjects.GetComponentsInChildren<BlockHolder>(true);
            foreach (BlockHolder blockHolder in blockHolders)
            {
                if (blockHolder != null)
                {
                    if (blockHolder.ID != "" && blockHolder.blockData != null)
                    {
                        if (blockHolder.blockType == "Dirt" && blockHolder.blockData.Count > 0)
                        {
                            int interval = 0;
                            bool containsOre = false;

                            for (int i = 0; i < blockHolder.blockData.Count; i++)
                            {
                                if (orePositions.Contains(blockHolder.blockData[i].position))
                                {
                                    containsOre = true;
                                    break;
                                }

                                if (paused == true)
                                {
                                    break;
                                }

                                interval++;
                                if (interval >= 250)
                                {
                                    interval = 0;
                                    yield return null;
                                }
                            }

                            if (containsOre == false)
                            {
                                interval = 0;
                                for (int i = 0; i < blockHolder.blockData.Count; i++)
                                {
                                    try
                                    {
                                        if (blockHolder.blockData[i].position.y <= -90 && blockHolder.blockData[i].position.y >= -110)
                                        {
                                            float spawnChance = UnityEngine.Random.Range(0, 10000);
                                            if (spawnChance >= 9999)
                                            {
                                                int randomOre = UnityEngine.Random.Range(0, 7);
                                                GameObject spawnedObject = Instantiate(ores[randomOre], blockHolder.blockData[i].position, new Quaternion());
                                                spawnedObject.SetActive(true);
                                                blockHolder.blockData.RemoveAt(i);
                                                if (blockHolder.unloaded == false)
                                                {
                                                    GetComponent<GameManager>().meshManager.CreateCombinedMesh(blockHolder.gameObject);
                                                }
                                                orePositions.Add(blockHolder.blockData[i].position);
                                                oreTypes.Add(randomOre);
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.Log("Chunk was modified during ore generation: " + e.Message);
                                    }

                                    if (paused == true)
                                    {
                                        break;
                                    }

                                    interval++;
                                    if (interval >= 250)
                                    {
                                        interval = 0;
                                        yield return null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            PlayerPrefsX.SetVector3Array(GetComponent<StateManager>().worldName + "orePositions", orePositions.ToArray());
            PlayerPrefsX.SetIntArray(GetComponent<StateManager>().worldName + "oreTypes", oreTypes.ToArray());
        }          
        paused = true;
        coroutineBusy = false;
    }
}
