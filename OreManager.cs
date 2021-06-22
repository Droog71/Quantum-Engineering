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
    private List<string> chunkList;
    private List<int> oreTypes;
    private bool loaded;
    public bool init;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (chunkList == null)
        {
            chunkList = PlayerPrefsX.GetStringArray(GetComponent<StateManager>().worldName + "chunkList").ToList();
        }

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

        if (!coroutineBusy && init == false)
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
            if (GetComponent<StateManager>().worldLoaded == true && GetComponent<TerrainGenerator>().initialized == true)
            {
                Vector3[] posArray = PlayerPrefsX.GetVector3Array(GetComponent<StateManager>().worldName + "orePositions");
                int[] typeArray = PlayerPrefsX.GetIntArray(GetComponent<StateManager>().worldName + "oreTypes");

                if (posArray.Length > 0)
                {
                    for (int i = 0; i < posArray.Length; i++)
                    {
                        GameObject spawnedObject = Instantiate(ores[typeArray[i]], posArray[i], new Quaternion());
                        spawnedObject.SetActive(true);
                        spawnedObject.transform.parent = null;
                        yield return null;
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
                        if (blockHolder.blockType == "Dirt" && !chunkList.Contains(blockHolder.ID) && blockHolder.blockData.Count > 0)
                        {
                            int interval = 0;
                            for (int i = 0; i < blockHolder.blockData.Count; i++)
                            {
                                try
                                {
                                    float spawnChance = UnityEngine.Random.Range(0, 10000);
                                    if (spawnChance >= 9999 && blockHolder.blockData[i].position.y <= -90)
                                    {
                                        int randomOre = UnityEngine.Random.Range(0, 7);
                                        GameObject spawnedObject = Instantiate(ores[randomOre], blockHolder.blockData[i].position, new Quaternion());
                                        spawnedObject.SetActive(true);
                                        spawnedObject.transform.parent = null;
                                        blockHolder.blockData.RemoveAt(i);
                                        if (blockHolder.unloaded == false)
                                        {
                                            GetComponent<GameManager>().meshManager.CreateCombinedMesh(blockHolder.gameObject);
                                        }
                                        orePositions.Add(blockHolder.blockData[i].position);
                                        oreTypes.Add(randomOre);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.Log("Chunk was modified during ore generation: " + e.Message);
                                }
                                interval++;
                                if (interval >= 1000)
                                {
                                    interval = 0;
                                    yield return null;
                                }
                            }

                            chunkList.Add(blockHolder.ID);
                        }
                    }
                }
            }
            PlayerPrefsX.SetVector3Array(GetComponent<StateManager>().worldName + "orePositions", orePositions.ToArray());
            PlayerPrefsX.SetIntArray(GetComponent<StateManager>().worldName + "oreTypes", oreTypes.ToArray());
            PlayerPrefsX.SetStringArray(GetComponent<StateManager>().worldName + "chunkList", chunkList.ToArray());
        }          
        init = true;
        coroutineBusy = false;
    }
}

