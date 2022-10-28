using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using MEC;

public class TerrainGenerator : MonoBehaviour
{
    private GameObject player;
    public List<Vector3> worldLocations;
    public List<Vector3> chunkLocations;
    public List<Vector3> treeLocations;
    public List<Vector3> grassLocations;
    private StateManager stateManager;
    private GameManager gameManager;
    public GameObject blockHolder;
    public GameObject foliage;
    public GameObject tree;
    public GameObject billboardGrass;
    public GameObject dirt;
    public GameObject grass;
    public bool initialized;
    public int generated;
    public int total;
    private int interval;
    private bool loading;
    private bool createdWorld;

    //! Called by unity engine on start to initialize variables.
    public void Start()
    {
        player = GameObject.Find("Player");
        worldLocations = new List<Vector3>();
        chunkLocations = new List<Vector3>();
        treeLocations = new List<Vector3>();
        stateManager = GetComponent<StateManager>();
        gameManager = GetComponent<GameManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (stateManager.worldLoaded == true)
        {
            if (loading == false)
            {
                loading = true;
                Timing.RunCoroutine(Initialize());
            }

            if (worldLocations.Count > 0 && createdWorld == true)
            {
                Timing.RunCoroutine(GenerateTerrain());
            }
        }
    }

    private IEnumerator<float> Initialize()
    {
        if (stateManager.worldLoaded == true)
        {
            if (worldLocations.Count < 1)
            {
                worldLocations = PlayerPrefsX.GetVector3Array(stateManager.worldName + "worldLocations").ToList();
                chunkLocations = PlayerPrefsX.GetVector3Array(stateManager.worldName + "chunkLocations").ToList();
            }

            if (worldLocations.Count < 1)
            {
                total = 1600;
                int loadInterval = 0;
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
                        generated++;
                        loadInterval++;
                        if (loadInterval >= 10)
                        {
                            loadInterval = 0;
                            yield return Timing.WaitForOneFrame;
                        }
                    }
                }
                createdWorld = true;
            }
            else if (createdWorld == false)
            {
                int loadInterval = 0;
                total = worldLocations.Count;
                foreach (Vector3 worldLoc in worldLocations)
                {
                    if (!((int)worldLoc.x == 0 && (int)worldLoc.z == 0))
                    {
                        GameObject treeObj = Instantiate(tree, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
                        treeObj.transform.parent = foliage.transform;
                        treeObj.GetComponent<ProceduralFoliage>().location = worldLoc;
                    }
                    GameObject grassObj = Instantiate(billboardGrass, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
                    grassObj.transform.parent = foliage.transform;
                    grassObj.GetComponent<ProceduralFoliage>().location = worldLoc;
                    generated++;
                    loadInterval++;
                    if (loadInterval >= 10)
                    {
                        loadInterval = 0;
                        yield return Timing.WaitForOneFrame;
                    }
                }
                createdWorld = true;
            }
        }
    }

    //! Generates chunks of dirt and grass near the player in procedural worlds.
    private IEnumerator<float> GenerateTerrain()
    {
        bool chunkRequired = false;

        foreach(Vector3 worldLoc in worldLocations)
        {
            if (!stateManager.Busy() && player != null)
            {
                bool playerInRange = false;

                float distance = Vector3.Distance(player.transform.position, worldLoc);
                if (distance <= 256)
                {
                    playerInRange = true;
                }

                if (playerInRange  == false && PlayerPrefsX.GetPersistentBool("multipayer") == true)
                {
                    NetworkPlayer[] networkPlayers = FindObjectsOfType<NetworkPlayer>();
                    foreach (NetworkPlayer netWorkPlayer in networkPlayers)
                    {
                        float networkPlayerDistance = Vector3.Distance(netWorkPlayer.transform.position, transform.position);
                        if (networkPlayerDistance <= 256)
                        {
                            playerInRange = true;
                            break;
                        }
                    }
                }

                if (playerInRange == true)
                {
                    if (!Physics.Raycast(new Vector3(worldLoc.x, worldLoc.y + 5, worldLoc.z), Vector3.down, out RaycastHit hit, 10))
                    {
                        if (!chunkLocations.Contains(worldLoc))
                        {
                            chunkRequired = true;
                            GetComponent<OreManager>().paused = false;
                            GenerateChunk(worldLoc);
                        }
                    }
                }

                interval++;
                if (interval >= 10)
                {
                    interval = 0;
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        initialized |= chunkRequired == false;
    }

    //! Creates a blockHolder object which is vector array representing a "chunk" of blocks that will be combined into a single object.
    private void GenerateChunk(Vector3 worldLoc)
    {
        GameObject grassChunk = Instantiate(blockHolder, gameManager.transform.position, gameManager.transform.rotation);
        grassChunk.GetComponent<BlockHolder>().worldLoc = worldLoc;
        grassChunk.transform.parent = gameManager.builtObjects.transform;
        List<BlockHolder.BlockInfo> blockData = new List<BlockHolder.BlockInfo>();
        grassChunk.GetComponent<BlockHolder>().blockData = blockData;
        for (int i = (int)worldLoc.x - 25; i < worldLoc.x + 25; i += 5)
        {
            for (int j = (int)worldLoc.z - 25; j < worldLoc.z + 25; j += 5)
            {
                blockData.Add(new BlockHolder.BlockInfo(new Vector3(i, worldLoc.y, j), new Quaternion()));
            }
        }
        grassChunk.GetComponent<BlockHolder>().blockType = "Grass";
        grassChunk.GetComponent<BlockHolder>().unloaded = true;
        GetComponent<GameManager>().meshManager.SetMaterial(grassChunk, "Grass");
        grassChunk.GetComponent<MeshCollider>().enabled = true;

        GameObject grassObject = Instantiate(grass, new Vector3(worldLoc.x - 2.3f, worldLoc.y, worldLoc.z - 2.7f), new Quaternion());
        grassObject.transform.parent = grassChunk.transform;
        grassChunk.GetComponent<BlockHolder>().grass = grassObject;

        GameObject dirtChunk = Instantiate(blockHolder, gameManager.transform.position, gameManager.transform.rotation);
        dirtChunk.GetComponent<BlockHolder>().worldLoc = worldLoc;
        dirtChunk.transform.parent = gameManager.builtObjects.transform;
        blockData = new List<BlockHolder.BlockInfo>();
        dirtChunk.GetComponent<BlockHolder>().blockData = blockData;
        for (int i = (int)worldLoc.x - 25; i < worldLoc.x + 25; i += 5)
        {
            for (int j = (int)worldLoc.z - 25; j < worldLoc.z + 25; j += 5)
            {
                for (int k = (int)worldLoc.y - 5; k > worldLoc.y - 55; k -= 5)
                {
                    blockData.Add(new BlockHolder.BlockInfo(new Vector3(i, k, j), new Quaternion()));
                }
            }
        }
        dirtChunk.GetComponent<BlockHolder>().blockType = "Dirt";
        dirtChunk.GetComponent<BlockHolder>().unloaded = true;
        GetComponent<GameManager>().meshManager.SetMaterial(dirtChunk, "Dirt");
        dirtChunk.GetComponent<MeshCollider>().enabled = true;

        GameObject dirtObject = Instantiate(dirt, new Vector3(worldLoc.x - 2.3f, worldLoc.y - 5, worldLoc.z - 2.7f), new Quaternion());
        dirtObject.transform.parent = dirtChunk.transform;
        dirtChunk.GetComponent<BlockHolder>().dirt = dirtObject;

        grassChunk.GetComponent<BlockHolder>().dirtHolder = dirtChunk;
        dirtChunk.GetComponent<BlockHolder>().grassHolder = grassChunk;

        if (!((int)worldLoc.x == 0 && (int)worldLoc.z == 0))
        {
            GameObject treeObj = Instantiate(tree, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
            treeObj.transform.parent = foliage.transform;
            treeObj.GetComponent<ProceduralFoliage>().location = worldLoc;
        }

        GameObject grassObj = Instantiate(billboardGrass, new Vector3(worldLoc.x - 2.3f, worldLoc.y + 3, worldLoc.z - 2.7f), new Quaternion());
        grassObj.transform.parent = foliage.transform;
        grassObj.GetComponent<ProceduralFoliage>().location = worldLoc;
    }
}
