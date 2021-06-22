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
    public List<BlockInfo> blockData;
    public bool blockHolderCoroutineBusy;
    private GameManager gameManager;
    private StateManager stateManager;
    private GameObject player;
    private Coroutine blockHolderCoroutine;

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

        if (stateManager == null)
        {
            stateManager = FindObjectOfType<StateManager>();
        }

        if (blockHolderCoroutineBusy == false && stateManager.worldLoaded == true && unloaded == true)
        {
            if (SceneManager.GetActiveScene().name == "QE_Procedural")
            {
                if (blockType == "Dirt")
                {
                    if (gameManager.GetComponent<OreManager>().init == true)
                    {
                        blockHolderCoroutine = StartCoroutine(LoadChunk());
                    }
                }
                else
                {
                    blockHolderCoroutine = StartCoroutine(LoadChunk());
                }
            }
            else
            {
                blockHolderCoroutine = StartCoroutine(LoadChunk());
            }
        }
    }

    //! Loads the chunk.
    private IEnumerator LoadChunk()
    {
        blockHolderCoroutineBusy = true;

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
                    if (spawnInterval >= 50)
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
                        if (combineInterval >= 50)
                        {
                            combineInterval = 0;
                            yield return null;
                        }
                    }
                }

                if (SceneManager.GetActiveScene().name == "QE_Procedural")
                {
                    if (blockType == "Grass" || blockType == "Dirt")
                    {
                        gameManager.GetComponent<TerrainGenerator>().chunkLocations.Add(worldLoc);
                    }

                    if (grass != null)
                    {
                        gameManager.meshManager.CreateCombinedMesh(gameObject);
                        Vector3 saveVector = new Vector3(Mathf.Round(grass.transform.position.x), Mathf.Round(grass.transform.position.y), Mathf.Round(grass.transform.position.z));
                        Destroy(grass);
                    }

                    if (dirt != null)
                    {
                        gameManager.meshManager.CreateCombinedMesh(gameObject);
                        Vector3 saveVector = new Vector3(Mathf.Round(dirt.transform.position.x), Mathf.Round(dirt.transform.position.y), Mathf.Round(dirt.transform.position.z));
                        Destroy(dirt);
                    }
                }

                unloaded = false;
            }
        }
        blockHolderCoroutineBusy = false;
    }
}
