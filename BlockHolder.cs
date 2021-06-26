using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BlockHolder : MonoBehaviour
{
    public string ID;
    public bool unloaded;
    public string blockType;
    public Vector3 worldLoc;
    public GameObject grass;
    public GameObject dirt;
    public GameObject grassHolder;
    public GameObject dirtHolder;
    public List<BlockInfo> blockData;
    public bool chunkLoadCoroutineBusy;
    public bool chunkUnloadCoroutineBusy;
    private int yieldTime;
    private GameManager gameManager;
    private StateManager stateManager;
    private TerrainGenerator terrainGenerator;
    private GameObject player;
    private Coroutine chunkLoadCoroutine;
    private Coroutine chunkUnloadCoroutine;

    //! Holds information about the blocks in this chunk.
    public class BlockInfo
    {
        public Vector3 position;
        public Quaternion rotation;

        public BlockInfo(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    //! Starts the chunk loading coroutine.
    public void Load()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        else if (terrainGenerator == null)
        {
            terrainGenerator = gameManager.GetComponent<TerrainGenerator>();
        }

        if (stateManager == null)
        {
            stateManager = FindObjectOfType<StateManager>();
        }

        if (chunkLoadCoroutineBusy == false && unloaded == true)
        {
            chunkLoadCoroutine = StartCoroutine(LoadChunk());
        }
    }

    //! Loads the chunk.
    private IEnumerator LoadChunk()
    {
        chunkLoadCoroutineBusy = true;

        yieldTime = stateManager.worldLoaded == false ? 500 : 50;

        if (SceneManager.GetActiveScene().name == "QE_Procedural" && blockType == "Dirt")
        {
            if (gameManager.GetComponent<OreManager>().paused == false)
            {
                gameManager.GetComponent<OreManager>().paused = true;
            }
        }

        if (blockData != null)
        {
            if (blockData.Count > 0)
            {
                int spawnInterval = 0;
                for (int i =0; i < blockData.Count; i++)
                {
                    GameObject spawnedObject = Instantiate(stateManager.block, blockData[i].position, blockData[i].rotation);
                    spawnedObject.SetActive(false);
                    spawnedObject.transform.parent = transform;
                    spawnedObject.GetComponent<Block>().blockName = blockType;
                    BlockDictionary blockDictionary = gameManager.player.gameObject.GetComponent<BuildController>().blockDictionary;
                    if (blockDictionary.meshDictionary.ContainsKey(blockType))
                    {
                        spawnedObject.GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[blockType];
                    }
                    gameManager.meshManager.SetMaterial(spawnedObject, blockType);

                    spawnInterval++;
                    if (spawnInterval >= yieldTime)
                    {
                        spawnInterval = 0;
                        yield return null;
                    }
                }

                int combineInterval = 0;
                for (int i = 0; i < gameManager.blockHolders.Count; i++)
                {
                    foreach (GameObject obj in gameManager.blockHolders[i])
                    {
                        if (obj.GetComponent<BlockHolder>().blockType == blockType)
                        {
                            List<GameObject> holderList = gameManager.blockHolders[i].ToList();
                            holderList.Add(gameObject);
                            gameManager.blockHolders[i] = holderList.ToArray();
                            break;
                        }
                        combineInterval++;
                        if (combineInterval >= yieldTime)
                        {
                            combineInterval = 0;
                            yield return null;
                        }
                    }
                }

                gameManager.meshManager.CreateCombinedMesh(gameObject);

                if (SceneManager.GetActiveScene().name == "QE_Procedural")
                {
                    if (blockType == "Grass")
                    {
                        if (dirtHolder != null)
                        {
                            dirtHolder.GetComponent<BlockHolder>().Load();
                            if (terrainGenerator != null)
                            {
                                if (!terrainGenerator.chunkLocations.Contains(worldLoc))
                                {
                                    terrainGenerator.chunkLocations.Add(worldLoc);
                                    string chunkLocationsKey = stateManager.worldName + "chunkLocations";
                                    PlayerPrefsX.SetVector3Array(chunkLocationsKey, terrainGenerator.chunkLocations.ToArray());
                                }
                            }
                        }
                    }

                    if (blockType == "Dirt")
                    {
                        if (grassHolder != null)
                        {
                            grassHolder.GetComponent<BlockHolder>().Load();
                            if (terrainGenerator != null)
                            {
                                if (!terrainGenerator.chunkLocations.Contains(worldLoc))
                                {
                                    terrainGenerator.chunkLocations.Add(worldLoc);
                                    string chunkLocationsKey = stateManager.worldName + "chunkLocations";
                                    PlayerPrefsX.SetVector3Array(chunkLocationsKey, terrainGenerator.chunkLocations.ToArray());
                                }
                            }
                        }
                    }

                    if (grass != null)
                    {
                        Destroy(grass);
                    }

                    if (dirt != null)
                    {
                        Destroy(dirt);
                    }
                }

                unloaded = false;

                if (stateManager.worldLoaded == false && chunkUnloadCoroutineBusy == false)
                {
                    //chunkUnloadCoroutine = StartCoroutine(UnloadChunk());
                }
            }
        }
        chunkLoadCoroutineBusy = false;
    }

    //! Unloads the chunk.
    private IEnumerator UnloadChunk()
    {
        chunkUnloadCoroutineBusy = true;
        yieldTime = stateManager.worldLoaded == false ? 500 : 50;
        int interval = 0;

        Transform[] blocks = GetComponentsInChildren<Transform>(true);
        foreach (Transform block in blocks)
        {
            if (block.GetComponent<Block>() != null)
            {
                Destroy(block.gameObject);
            }

            interval++;
            if (interval >= yieldTime)
            {
                interval = 0;
                yield return null;
            }
        }

        unloaded = true;
        chunkUnloadCoroutineBusy = false;
    }
}
