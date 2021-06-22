﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class TerrainGenerator : MonoBehaviour
{
    private GameObject player;
    private List<Vector3> worldLocations;
    public List<Vector3> chunkLocations;
    private List<Vector3> treeLocations;
    private Coroutine terrainGenCoroutine;
    private StateManager stateManager;
    private GameManager gameManager;
    public GameObject blockHolder;
    public GameObject foliage;
    public GameObject tree;
    public GameObject billboardGrass;
    public GameObject dirt;
    public GameObject grass;
    public bool initialized;
    private int interval;

    //! Called by unity engine on start to initialize variables.
    public void Start()
    {
        player = GameObject.Find("Player");
        stateManager = GetComponent<StateManager>();
        gameManager = GetComponent<GameManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (stateManager.worldLoaded == true)
        {
            worldLocations = PlayerPrefsX.GetVector3Array(stateManager.worldName + "worldLocations").ToList();
            chunkLocations = PlayerPrefsX.GetVector3Array(stateManager.worldName + "chunkLocations").ToList();
            treeLocations = PlayerPrefsX.GetVector3Array(stateManager.worldName + "treeLocations").ToList();

            if (worldLocations == null || worldLocations.Count < 1)
            {
                for (int i = -1000; i < 1000; i += 50)
                {
                    for (int j = -1000; j < 1000; j += 50)
                    {
                        int randHeight = Random.Range(0, 125);
                        if (randHeight >= 0 && randHeight < 25)
                        {
                            randHeight = -10;
                        }
                        if (randHeight >= 25 && randHeight < 50)
                        {
                            randHeight = -5;
                        }
                        if (randHeight >= 50 && randHeight < 75)
                        {
                            randHeight = 0;
                        }
                        if (randHeight >= 75 && randHeight < 100)
                        {
                            randHeight = 5;
                        }
                        if (randHeight >= 100 && randHeight < 125)
                        {
                            randHeight = 10;
                        }
                        if (i == 0 && j == 0)
                        {
                            randHeight = 0;
                        }
                        worldLocations.Add(new Vector3(Mathf.Round(i), Mathf.Round(-72) + randHeight, Mathf.Round(j)));
                        int randomTree = Random.Range(0, 100);
                        bool atLander = i == 0 && j == 0;
                        if (randomTree >= 50 && atLander == false)
                        {
                            treeLocations.Add(new Vector3(Mathf.Round(i), Mathf.Round(-72) + randHeight, Mathf.Round(j)));
                        }
                    }
                }
                PlayerPrefsX.SetVector3Array(stateManager.worldName + "worldLocations", worldLocations.ToArray());
                PlayerPrefsX.SetVector3Array(stateManager.worldName + "treeLocations", treeLocations.ToArray());
            }
            terrainGenCoroutine = StartCoroutine(GenerateTerrain());
        }
    }

    //! Generates chunks of dirt and grass near the player in procedural worlds.
    private IEnumerator GenerateTerrain()
    {
        bool chunkRequired = false;
        foreach(Vector3 worldLoc in worldLocations)
        {
            float distance = Vector3.Distance(player.transform.position, worldLoc);
            if (distance <= 256)
            {
                if (!Physics.Raycast(new Vector3(worldLoc.x, worldLoc.y + 5, worldLoc.z), Vector3.down, out RaycastHit hit, 10))
                {
                    if (!chunkLocations.Contains(worldLoc))
                    {
                        chunkRequired = true;
                        GetComponent<OreManager>().init = false;
                        GenerateChunk(worldLoc);
                    }
                }
            }

            interval++;
            if (interval >= 10)
            {
                interval = 0;
                yield return null;
            }
        }

        initialized |= chunkRequired == false;
        PlayerPrefsX.SetVector3Array(stateManager.worldName + "chunkLocations", chunkLocations.ToArray());
    }

    //! Creates a blockHolder object which is vector array representing a "chunk" of blocks that will be combined into a single object.
    private void GenerateChunk(Vector3 worldLoc)
    {
        GameObject spawnedObject = Instantiate(blockHolder, gameManager.transform.position, gameManager.transform.rotation);
        GameObject grassObject = Instantiate(grass, new Vector3(worldLoc.x - 2.3f, worldLoc.y, worldLoc.z - 2.7f), new Quaternion());
        grassObject.transform.parent = spawnedObject.transform;
        spawnedObject.GetComponent<BlockHolder>().grass = grassObject;
        spawnedObject.GetComponent<BlockHolder>().worldLoc = worldLoc;
        spawnedObject.transform.parent = gameManager.builtObjects.transform;
        List<BlockHolder.BlockInfo> blockData = new List<BlockHolder.BlockInfo>();
        spawnedObject.GetComponent<BlockHolder>().blockData = blockData;
        for (int i = (int)worldLoc.x - 25; i < worldLoc.x + 25; i += 5)
        {
            for (int j = (int)worldLoc.z - 25; j < worldLoc.z + 25; j += 5)
            {
                blockData.Add(new BlockHolder.BlockInfo(new Vector3(i, worldLoc.y, j), new Quaternion()));
            }
        }
        spawnedObject.GetComponent<BlockHolder>().blockType = "Grass";
        spawnedObject.GetComponent<BlockHolder>().unloaded = true;
        GetComponent<GameManager>().meshManager.SetMaterial(spawnedObject, "Grass");
        spawnedObject.GetComponent<MeshCollider>().enabled = true;

        spawnedObject = Instantiate(blockHolder, gameManager.transform.position, gameManager.transform.rotation);
        GameObject dirtObject = Instantiate(dirt, new Vector3(worldLoc.x - 2.3f, worldLoc.y - 5, worldLoc.z - 2.7f), new Quaternion());
        dirtObject.transform.parent = spawnedObject.transform;
        spawnedObject.GetComponent<BlockHolder>().dirt = dirtObject;
        spawnedObject.GetComponent<BlockHolder>().worldLoc = worldLoc;
        spawnedObject.transform.parent = gameManager.builtObjects.transform;
        blockData = new List<BlockHolder.BlockInfo>();
        spawnedObject.GetComponent<BlockHolder>().blockData = blockData;
        for (int i = (int)worldLoc.x - 25; i < worldLoc.x + 25; i += 5)
        {
            for (int j = (int)worldLoc.z - 25; j < worldLoc.z + 25; j += 5)
            {
                for (int k = (int)worldLoc.y - 5; k >= worldLoc.y - 60; k -= 5)
                {
                    blockData.Add(new BlockHolder.BlockInfo(new Vector3(i, k, j), new Quaternion()));
                }
            }
        }
        spawnedObject.GetComponent<BlockHolder>().blockType = "Dirt";
        spawnedObject.GetComponent<BlockHolder>().unloaded = true;
        GetComponent<GameManager>().meshManager.SetMaterial(spawnedObject, "Dirt");
        spawnedObject.GetComponent<MeshCollider>().enabled = true;

        if (treeLocations.Contains(worldLoc))
        {
            GameObject treeObj = Instantiate(tree, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
            treeObj.transform.parent = foliage.transform;
        }
        else
        {
            GameObject grassObj = Instantiate(billboardGrass, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
            grassObj.transform.parent = foliage.transform;
        }
    }
}
