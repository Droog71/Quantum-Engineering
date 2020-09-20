using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMeshManager
{
    private GameManager gameManager;
    private bool ironMeshRequired;
    private bool steelMeshRequired;
    private bool glassMeshRequired;
    private bool brickMeshRequired;

    //! The Combined Mesh Manager handles all combined meshes for building blocks.
    //! All standard building blocks are stored in combined meshes after being placed.
    //! Parts of the combined mesh are converted to individual blocks when edited by the player.
    //! Meteor strikes and weapons also cause small sections of the combined mesh to separate.
    //! The combined mesh is refactored after changes occur.
    public CombinedMeshManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    //! Separates combined meshes into blocks
    public void SeparateBlocks(Vector3 target, string type, bool building)
    {
        if (gameManager.working == false && gameManager.GetComponent<StateManager>().saving == false)
        {
            if (building == true)
            {
                CombineBlocks();
            }
            gameManager.separateCoroutine = gameManager.StartCoroutine(BlockSeparationCoroutine(target, type));
        }
    }

    //! Spawns a combined mesh identical to the one being rebuilt, so it is not invisible during the process.
    private void SpawnDummy(GameObject realObject, GameObject dummyObject, GameObject toSpawn, string name)
    {
        dummyObject = Object.Instantiate(toSpawn, gameManager.transform.position, gameManager.transform.rotation);
        dummyObject.transform.parent = gameManager.builtObjects.transform;
        dummyObject.AddComponent<HolderDummy>();
        dummyObject.name = name;
        if (dummyObject.GetComponent<MeshFilter>() == null)
        {
            dummyObject.AddComponent<MeshFilter>();
        }
        if (realObject.GetComponent<MeshFilter>() != null)
        {
            dummyObject.GetComponent<MeshFilter>().mesh = realObject.GetComponent<MeshFilter>().mesh;
        }
        dummyObject.GetComponent<Renderer>().material.color = realObject.GetComponent<Renderer>().material.color;
        Object.Destroy(realObject.GetComponent<MeshFilter>());
    }

    //! Separates combined meshes into blocks
    private IEnumerator BlockSeparationCoroutine(Vector3 target, string type)
    {
        while (gameManager.blocksCombined == false)
        {
            yield return null;
        }

        int ironCount = 0;
        int steelCount = 0;
        int brickCount = 0;
        int glassCount = 0;
        int totalIron = 0;
        int totalBrick = 0;
        int totalGlass = 0;
        int totalSteel = 0;

        int ironSeprationInterval = 0;
        foreach (GameObject obj in gameManager.ironBlocks)
        {
            Transform[] blocks = gameManager.ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
            foreach (Transform i in blocks)
            {
                if (i != null)
                {
                    float distance = Vector3.Distance(i.position, target);
                    if (distance < 40 && type.Equals("all"))
                    {
                        i.gameObject.SetActive(true);
                        i.parent = gameManager.builtObjects.transform;
                        totalIron++;
                    }
                    if (distance < 20 && type.Equals("iron"))
                    {
                        i.gameObject.SetActive(true);
                        i.parent = gameManager.builtObjects.transform; ;
                        totalIron++;
                    }
                    if (distance < 10 && type.Equals("iron"))
                    {
                        i.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalIron++;
                    }
                }
            }
            ironCount++;
            ironSeprationInterval++;
            if (ironSeprationInterval >= 150)
            {
                yield return null;
                ironSeprationInterval = 0;
            }
        }

        int glassSeprationInterval = 0;
        foreach (GameObject obj in gameManager.glass)
        {
            Transform[] glassBlocks = gameManager.glass[glassCount].GetComponentsInChildren<Transform>(true);
            foreach (Transform g in glassBlocks)
            {
                if (g != null)
                {
                    float distance = Vector3.Distance(g.position, target);
                    if (distance < 40 && type.Equals("all"))
                    {
                        g.gameObject.SetActive(true);
                        g.parent = gameManager.builtObjects.transform;
                        totalGlass++;
                    }
                    if (distance < 20 && type.Equals("glass"))
                    {
                        g.gameObject.SetActive(true);
                        g.parent = gameManager.builtObjects.transform; ;
                        totalGlass++;
                    }
                    if (distance < 10 && type.Equals("glass"))
                    {
                        g.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalGlass++;
                    }
                }
            }
            glassCount++;
            glassSeprationInterval++;
            if (glassSeprationInterval >= 150)
            {
                yield return null;
                glassSeprationInterval = 0;
            }
        }

        int steelSeprationInterval = 0;
        foreach (GameObject obj in gameManager.steel)
        {
            Transform[] steelBlocks = gameManager.steel[steelCount].GetComponentsInChildren<Transform>(true);
            foreach (Transform s in steelBlocks)
            {
                if (s != null)
                {
                    float distance = Vector3.Distance(s.position, target);
                    if (distance < 40 && type.Equals("all"))
                    {
                        s.gameObject.SetActive(true);
                        s.parent = gameManager.builtObjects.transform;
                        totalSteel++;
                    }
                    if (distance < 20 && type.Equals("steel"))
                    {
                        s.gameObject.SetActive(true);
                        s.parent = gameManager.builtObjects.transform;
                        totalSteel++;
                    }
                    if (distance < 10 && type.Equals("steel"))
                    {
                        s.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalSteel++;
                    }
                }
            }
            steelCount++;
            steelSeprationInterval++;
            if (steelSeprationInterval >= 150)
            {
                yield return null;
                steelSeprationInterval = 0;
            }
        }

        int brickSeprationInterval = 0;
        foreach (GameObject obj in gameManager.bricks)
        {
            Transform[] brickBlocks = gameManager.bricks[brickCount].GetComponentsInChildren<Transform>(true);
            foreach (Transform b in brickBlocks)
            {
                if (b != null)
                {
                    float distance = Vector3.Distance(b.position, target);
                    if (distance < 40 && type.Equals("all"))
                    {
                        b.gameObject.SetActive(true);
                        b.parent = gameManager.builtObjects.transform;
                        totalBrick++;
                    }
                    if (distance < 20 && type.Equals("brick"))
                    {
                        b.gameObject.SetActive(true);
                        b.parent = gameManager.builtObjects.transform; ;
                        totalBrick++;
                    }
                    if (distance < 10 && type.Equals("brick"))
                    {
                        b.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalBrick++;
                    }
                }
            }
            brickCount++;
            brickSeprationInterval++;
            if (brickSeprationInterval >= 150)
            {
                yield return null;
                brickSeprationInterval = 0;
            }
        }

        ironCount = 0;
        steelCount = 0;
        glassCount = 0;
        brickCount = 0;
        if (totalIron > 0)
        {
            int ironDummyInterval = 0;
            foreach (GameObject obj in gameManager.ironBlocks)
            {
                GameObject realObject = gameManager.ironBlocks[ironCount];
                GameObject dummyObject = gameManager.ironBlocksDummy[ironCount];
                SpawnDummy(realObject, dummyObject, gameManager.ironHolder, "Iron Dummy");
                ironCount++;
                ironDummyInterval++;
                if (ironDummyInterval >= 150)
                {
                    yield return null;
                    ironDummyInterval = 0;
                }
            }
            ironMeshRequired = true;
        }
        if (totalGlass > 0)
        {
            int glassDummyInterval = 0;
            foreach (GameObject obj in gameManager.glass)
            {
                GameObject realObject = gameManager.glass[glassCount];
                GameObject dummyObject = gameManager.glassDummy[glassCount];
                SpawnDummy(realObject, dummyObject, gameManager.glassHolder, "Glass Dummy");
                glassCount++;
                glassDummyInterval++;
                if (glassDummyInterval >= 150)
                {
                    yield return null;
                    glassDummyInterval = 0;
                }
            }
            glassMeshRequired = true;
        }
        if (totalSteel > 0)
        {
            int steelDummyInterval = 0;
            foreach (GameObject obj in gameManager.steel)
            {
                GameObject realObject = gameManager.steel[steelCount];
                GameObject dummyObject = gameManager.steelDummy[steelCount];
                SpawnDummy(realObject, dummyObject, gameManager.steelHolder, "Steel Dummy");
                steelCount++;
                steelDummyInterval++;
                if (steelDummyInterval >= 150)
                {
                    yield return null;
                    steelDummyInterval = 0;
                }
            }
            steelMeshRequired = true;
        }
        if (totalBrick > 0)
        {
            int brickDummyInterval = 0;
            foreach (GameObject obj in gameManager.bricks)
            {
                GameObject realObject = gameManager.bricks[brickCount];
                GameObject dummyObject = gameManager.bricksDummy[brickCount];
                SpawnDummy(realObject, dummyObject, gameManager.brickHolder, "Brick Dummy");
                brickCount++;
                brickDummyInterval++;
                if (brickDummyInterval >= 150)
                {
                    yield return null;
                    brickDummyInterval = 0;
                }
            }
            brickMeshRequired = true;
        }

        if (CombinedMeshRequired() == true)
        {
            gameManager.replacingMeshFilters = true;
        }

        if (gameManager.replacingMeshFilters == false)
        {
            gameManager.working = false;
        }
    }

    //! Returns true if any combined mesh needs to be created or rebuilt.
    private bool CombinedMeshRequired()
    {
        return ironMeshRequired == true || glassMeshRequired == true || brickMeshRequired == true || steelMeshRequired == true;
    }

    //! Starts the coroutine to combine all blocks into combined meshes.
    public void CombineBlocks()
    {
        gameManager.blockCombineCoroutine = gameManager.StartCoroutine(BlockCombineCoroutine());
    }

    //! Creates combined meshes from placed building blocks.
    public IEnumerator BlockCombineCoroutine()
    {
        while (gameManager.working == true || gameManager.GetComponent<StateManager>().saving == true)
        {
            yield return null;
        }

        gameManager.working = true;
        int combineInterval = 0;
        int ironCount = 0;
        int steelCount = 0;
        int glassCount = 0;
        int brickCount = 0;
        int ironBlockCount = 0;
        int steelBlockCount = 0;
        int glassBlockCount = 0;
        int brickBlockCount = 0;

        IronBlock[] allIronBlocks = Object.FindObjectsOfType<IronBlock>();
        foreach (IronBlock block in allIronBlocks)
        {
            if (block != null)
            {
                Transform[] blocks = gameManager.ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    ironCount++;
                }
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.ironBlocks[ironCount].transform;
                    ironBlockCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 150)
            {
                combineInterval = 0;
                yield return null;
            }
        }

        Glass[] allGlassBlocks = Object.FindObjectsOfType<Glass>();
        foreach (Glass block in allGlassBlocks)
        {
            if (block != null)
            {
                Transform[] blocks = gameManager.glass[glassCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    glassCount++;
                }
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.glass[glassCount].transform;
                    glassBlockCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 150)
            {
                combineInterval = 0;
                yield return null;
            }
        }

        Steel[] allSteelBlocks = Object.FindObjectsOfType<Steel>();
        foreach (Steel block in allSteelBlocks)
        {
            if (block != null)
            {
                Transform[] blocks = gameManager.steel[steelCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    steelCount++;
                }
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.steel[steelCount].transform;
                    steelBlockCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 150)
            {
                combineInterval = 0;
                yield return null;
            }
        }

        Brick[] allBrickBlocks = Object.FindObjectsOfType<Brick>();
        foreach (Brick block in allBrickBlocks)
        {
            if (block != null)
            {
                Transform[] blocks = gameManager.bricks[brickCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    brickCount++;
                }
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.bricks[brickCount].transform;
                    brickBlockCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 150)
            {
                combineInterval = 0;
                yield return null;
            }
        }

        if (ironBlockCount > 0)
        {
            ironCount = 0;
            foreach (GameObject obj in gameManager.ironBlocks)
            {
                GameObject realObject = gameManager.ironBlocks[ironCount];
                GameObject dummyObject = gameManager.ironBlocksDummy[ironCount];
                SpawnDummy(realObject, dummyObject, gameManager.ironHolder, "Iron Dummy");
                ironCount++;
                combineInterval++;
                if (combineInterval >= 150)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            ironMeshRequired = true;
            gameManager.replacingMeshFilters = true;
        }

        if (steelBlockCount > 0)
        {
            steelCount = 0;
            foreach (GameObject obj in gameManager.steel)
            {
                GameObject realObject = gameManager.steel[steelCount];
                GameObject dummyObject = gameManager.steelDummy[steelCount];
                SpawnDummy(realObject, dummyObject, gameManager.steelHolder, "Steel Dummy");
                steelCount++;
                combineInterval++;
                if (combineInterval >= 150)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            steelMeshRequired = true;
            gameManager.replacingMeshFilters = true;
        }

        if (glassBlockCount > 0)
        {
            glassCount = 0;
            foreach (GameObject obj in gameManager.glass)
            {
                GameObject realObject = gameManager.glass[glassCount];
                GameObject dummyObject = gameManager.glassDummy[glassCount];
                SpawnDummy(realObject, dummyObject, gameManager.glassHolder, "Glass Dummy");
                glassCount++;
                combineInterval++;
                if (combineInterval >= 150)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            glassMeshRequired = true;
            gameManager.replacingMeshFilters = true;
        }

        if (brickBlockCount > 0)
        {
            brickCount = 0;
            foreach (GameObject obj in gameManager.bricks)
            {
                GameObject realObject = gameManager.bricks[brickCount];
                GameObject dummyObject = gameManager.bricksDummy[brickCount];
                SpawnDummy(realObject, dummyObject, gameManager.brickHolder, "Brick Dummy");
                brickCount++;
                combineInterval++;
                if (combineInterval >= 150)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            brickMeshRequired = true;
            gameManager.replacingMeshFilters = true;
        }

        gameManager.blocksCombined = true;
        if (gameManager.replacingMeshFilters == false)
        {
            gameManager.working = false;
        }
    }

    //! Starts the combined mesh coroutine.
    public void CombineMeshes()
    {
        gameManager.meshCombineCoroutine = gameManager.StartCoroutine(CombineMeshCoroutine());
    }

    //! Creats a combined mesh from all meshes attached to the given object.
    private void CreateCombinedMesh(GameObject holder)
    {
        MeshFilter[] meshFilters = holder.GetComponentsInChildren<MeshFilter>(true);
        List<MeshFilter> mfList = new List<MeshFilter>();
        foreach (MeshFilter mf in meshFilters)
        {
            if (mf != null)
            {
                if (mf.gameObject != null)
                {
                    if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        if (mf.gameObject.GetComponent<PhysicsHandler>().IsSupported())
                        {
                            mfList.Add(mf);
                        }
                    }
                }
            }
        }
        meshFilters = mfList.ToArray();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        if (holder.GetComponent<MeshFilter>() == null)
        {
            holder.AddComponent<MeshFilter>();
        }
        holder.GetComponent<MeshFilter>().mesh = new Mesh();
        holder.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        holder.GetComponent<MeshCollider>().sharedMesh = holder.GetComponent<MeshFilter>().mesh;
        holder.GetComponent<MeshCollider>().enabled = true;
        if (meshFilters.Length > 0)
        {
            holder.SetActive(true);
        }
    }

    //! Creates combined meshes from blocks placed in the world.
    private IEnumerator CombineMeshCoroutine()
    {
        if (ironMeshRequired == true)
        {
            int ironCombineInterval = 0;
            foreach (GameObject holder in gameManager.ironBlocks)
            {
                CreateCombinedMesh(holder);
                ironCombineInterval++;
                if (ironCombineInterval >= 150)
                {
                    yield return null;
                    ironCombineInterval = 0;
                }
            }
            ironMeshRequired = false;
        }

        if (glassMeshRequired == true)
        {
            int glassCombineInterval = 0;
            foreach (GameObject holder in gameManager.glass)
            {
                CreateCombinedMesh(holder);
                glassCombineInterval++;
                if (glassCombineInterval >= 150)
                {
                    yield return null;
                    glassCombineInterval = 0;
                }
            }
            glassMeshRequired = false;
        }

        if (steelMeshRequired == true)
        {
            int steelCombineInterval = 0;
            foreach (GameObject holder in gameManager.steel)
            {
                CreateCombinedMesh(holder);
                steelCombineInterval++;
                if (steelCombineInterval >= 150)
                {
                    yield return null;
                    steelCombineInterval = 0;
                }
            }
            steelMeshRequired = false;
        }

        if (brickMeshRequired == true)
        {
            int brickCombineInterval = 0;
            foreach (GameObject holder in gameManager.bricks)
            {
                CreateCombinedMesh(holder);
                brickCombineInterval++;
                if (brickCombineInterval >= 150)
                {
                    yield return null;
                    brickCombineInterval = 0;
                }
            }
            brickMeshRequired = false;
        }

        HolderDummy[] allHolders = Object.FindObjectsOfType<HolderDummy>();
        int dummyDestroyInterval = 0;
        foreach (HolderDummy h in allHolders)
        {
            if (h != null)
            {
                Object.Destroy(h.gameObject);
                dummyDestroyInterval++;
                if (dummyDestroyInterval >= 150)
                {
                    yield return null;
                    dummyDestroyInterval = 0;
                }
            }
        }
        gameManager.working = false;
    }
}

