using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinedMeshManager
{
    private GameManager gameManager;
    private bool ironMeshRequired;
    private bool steelMeshRequired;
    private bool glassMeshRequired;
    private bool brickMeshRequired;
    private bool modMeshRequired;

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
    private void SpawnDummy(GameObject realObject, GameObject toSpawn, string name)
    {
        GameObject dummyObject = Object.Instantiate(toSpawn, gameManager.transform.position, gameManager.transform.rotation);
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
        dummyObject.GetComponent<Renderer>().material = realObject.GetComponent<Renderer>().material;
        dummyObject.GetComponent<Renderer>().material.mainTexture = realObject.GetComponent<Renderer>().material.mainTexture;
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

        int totalIron = 0;
        int totalBrick = 0;
        int totalGlass = 0;
        int totalSteel = 0;
        int totalModBlocks = 0;

        int ironSeprationInterval = 0;
        for (int i = 0; i < gameManager.ironHolders.Length; i++)
        {
            Transform[] blocks = gameManager.ironHolders[i].GetComponentsInChildren<Transform>(true);
            foreach (Transform ironBlock in blocks)
            {
                if (ironBlock != null)
                {
                    float distance = Vector3.Distance(ironBlock.position, target);
                    if (distance < gameManager.chunkSize && type.Equals("all"))
                    {
                        ironBlock.gameObject.SetActive(true);
                        ironBlock.parent = gameManager.builtObjects.transform;
                        totalIron++;
                    }
                    if (distance < 20 && type.Equals("iron"))
                    {
                        ironBlock.gameObject.SetActive(true);
                        ironBlock.parent = gameManager.builtObjects.transform;
                        totalIron++;
                    }
                    if (distance < 10 && type.Equals("iron"))
                    {
                        ironBlock.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalIron++;
                    }
                }
                ironSeprationInterval++;
                if (ironSeprationInterval >= 250)
                {
                    yield return null;
                    ironSeprationInterval = 0;
                }
            }
        }

        int glassSeprationInterval = 0;
        for (int i = 0; i < gameManager.glassHolders.Length; i++)
        {
            Transform[] glassBlocks = gameManager.glassHolders[i].GetComponentsInChildren<Transform>(true);
            foreach (Transform glassBlock in glassBlocks)
            {
                if (glassBlock != null)
                {
                    float distance = Vector3.Distance(glassBlock.position, target);
                    if (distance < gameManager.chunkSize && type.Equals("all"))
                    {
                        glassBlock.gameObject.SetActive(true);
                        glassBlock.parent = gameManager.builtObjects.transform;
                        totalGlass++;
                    }
                    if (distance < 20 && type.Equals("glass"))
                    {
                        glassBlock.gameObject.SetActive(true);
                        glassBlock.parent = gameManager.builtObjects.transform; ;
                        totalGlass++;
                    }
                    if (distance < 10 && type.Equals("glass"))
                    {
                        glassBlock.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalGlass++;
                    }
                }
                glassSeprationInterval++;
                if (glassSeprationInterval >= 250)
                {
                    yield return null;
                    glassSeprationInterval = 0;
                }
            }
        }

        int steelSeprationInterval = 0;
        for (int i = 0; i < gameManager.steelHolders.Length; i++)
        {
            Transform[] steelBlocks = gameManager.steelHolders[i].GetComponentsInChildren<Transform>(true);
            foreach (Transform steelBlock in steelBlocks)
            {
                if (steelBlock != null)
                {
                    float distance = Vector3.Distance(steelBlock.position, target);
                    if (distance < gameManager.chunkSize && type.Equals("all"))
                    {
                        steelBlock.gameObject.SetActive(true);
                        steelBlock.parent = gameManager.builtObjects.transform;
                        totalSteel++;
                    }
                    if (distance < 20 && type.Equals("steel"))
                    {
                        steelBlock.gameObject.SetActive(true);
                        steelBlock.parent = gameManager.builtObjects.transform;
                        totalSteel++;
                    }
                    if (distance < 10 && type.Equals("steel"))
                    {
                        steelBlock.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalSteel++;
                    }
                }
                steelSeprationInterval++;
                if (steelSeprationInterval >= 250)
                {
                    yield return null;
                    steelSeprationInterval = 0;
                }
            }
        }

        int brickSeprationInterval = 0;
        for (int i = 0; i < gameManager.brickHolders.Length; i++)
        {
            Transform[] brickBlocks = gameManager.brickHolders[i].GetComponentsInChildren<Transform>(true);
            foreach (Transform brickBlock in brickBlocks)
            {
                if (brickBlock != null)
                {
                    float distance = Vector3.Distance(brickBlock.position, target);
                    if (distance < gameManager.chunkSize && type.Equals("all"))
                    {
                        brickBlock.gameObject.SetActive(true);
                        brickBlock.parent = gameManager.builtObjects.transform;
                        totalBrick++;
                    }
                    if (distance < 20 && type.Equals("brick"))
                    {
                        brickBlock.gameObject.SetActive(true);
                        brickBlock.parent = gameManager.builtObjects.transform; ;
                        totalBrick++;
                    }
                    if (distance < 10 && type.Equals("brick"))
                    {
                        brickBlock.gameObject.GetComponent<PhysicsHandler>().Explode();
                        totalBrick++;
                    }
                }
                brickSeprationInterval++;
                if (brickSeprationInterval >= 250)
                {
                    yield return null;
                    brickSeprationInterval = 0;
                }
            }
        }

        int modBlockIndex = 0;
        foreach (string blockName in gameManager.modBlockNames)
        {
            int modBlockSeparationInterval = 0;
            GameObject[] currentBlockType = gameManager.modBlockHolders[modBlockIndex];
            for (int i = 0; i < currentBlockType.Length; i++)
            {
                Transform[] modBlocks = currentBlockType[i].GetComponentsInChildren<Transform>(true);
                foreach (Transform modBlock in modBlocks)
                {
                    if (modBlock != null)
                    {
                        float distance = Vector3.Distance(modBlock.position, target);
                        if (distance < gameManager.chunkSize && type.Equals("all"))
                        {
                            modBlock.gameObject.SetActive(true);
                            modBlock.parent = gameManager.builtObjects.transform;
                            totalModBlocks++;
                        }
                        if (distance < 20 && type.Equals(blockName))
                        {
                            modBlock.gameObject.SetActive(true);
                            modBlock.parent = gameManager.builtObjects.transform;
                            totalModBlocks++;
                        }
                        if (distance < 10 && type.Equals(blockName))
                        {
                            modBlock.gameObject.GetComponent<PhysicsHandler>().Explode();
                            totalModBlocks++;
                        }
                    }
                    modBlockSeparationInterval++;
                    if (modBlockSeparationInterval >= 250)
                    {
                        yield return null;
                        modBlockSeparationInterval = 0;
                    }
                }
            }
            modBlockIndex++;
        }

        if (totalIron > 0)
        {
            int ironDummyInterval = 0;
            for (int i = 0; i < gameManager.ironHolders.Length; i++)
            {
                GameObject realObject = gameManager.ironHolders[i];
                SpawnDummy(realObject, gameManager.ironHolder, "Iron Dummy");
                ironDummyInterval++;
                if (ironDummyInterval >= 10)
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
            for (int i = 0; i < gameManager.glassHolders.Length; i++)
            {
                GameObject realObject = gameManager.glassHolders[i];
                SpawnDummy(realObject, gameManager.glassHolder, "Glass Dummy");
                glassDummyInterval++;
                if (glassDummyInterval >= 10)
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
            for (int i = 0; i < gameManager.steelHolders.Length; i++)
            {
                GameObject realObject = gameManager.steelHolders[i];
                SpawnDummy(realObject, gameManager.steelHolder, "Steel Dummy");
                steelDummyInterval++;
                if (steelDummyInterval >= 10)
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
            for (int i = 0; i < gameManager.brickHolders.Length; i++)
            {
                GameObject realObject = gameManager.brickHolders[i];
                SpawnDummy(realObject, gameManager.brickHolder, "Brick Dummy");
                brickDummyInterval++;
                if (brickDummyInterval >= 10)
                {
                    yield return null;
                    brickDummyInterval = 0;
                }
            }
            brickMeshRequired = true;
        }

        if (totalModBlocks > 0)
        {
            modBlockIndex = 0;
            foreach (string blockName in gameManager.modBlockNames)
            {
                int modBlockInterval = 0;
                GameObject[] currentBlockType = gameManager.modBlockHolders[modBlockIndex];
                for (int i = 0; i < currentBlockType.Length; i++)
                {
                    GameObject realObject = currentBlockType[i];
                    SpawnDummy(realObject, gameManager.modBlockHolder, blockName + " Dummy");
                    modBlockInterval++;
                    if (modBlockInterval >= 10)
                    {
                        yield return null;
                        modBlockInterval = 0;
                    }
                }
                modBlockIndex++;
            }
            modMeshRequired = true;
        }

        if (CombinedMeshRequired() == true)
        {
            gameManager.replacingMeshFilters = true;
        }

        if (gameManager.replacingMeshFilters == false)
        {
            gameManager.working = false;
        }

        gameManager.blocksCombined = false;
    }

    //! Returns true if any combined mesh needs to be created or rebuilt.
    private bool CombinedMeshRequired()
    {
        return ironMeshRequired == true || glassMeshRequired == true || brickMeshRequired == true || steelMeshRequired == true || modMeshRequired == true;
    }

    //! Starts the coroutine to combine all blocks into combined meshes.
    public void CombineBlocks()
    {
        gameManager.blockCombineCoroutine = gameManager.StartCoroutine(BlockCombineCoroutine());
    }

    //! Sets up the material for mod block meshes.
    public void SetMaterial(GameObject obj, string blockType)
    {
        TextureDictionary textureDictionary = GameObject.Find("Player").GetComponent<TextureDictionary>();
        if (textureDictionary.dictionary.ContainsKey(blockType))
        {
            if (blockType.ToUpper().Contains("GLASS"))
            {
                obj.GetComponent<Renderer>().material = gameManager.glassMaterial;
            }
            else
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = textureDictionary.dictionary[blockType];
                if (textureDictionary.dictionary.ContainsKey(blockType + "_Normal"))
                {
                    mat.shaderKeywords = new string[] { "_NORMALMAP" };
                    mat.SetTexture("_BumpMap", textureDictionary.dictionary[blockType + "_Normal"]);
                    mat.SetFloat("_BumpScale", 2);
                }
                obj.GetComponent<Renderer>().material = mat;
            }
        }
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
        int ironHolderCount = 0;
        int steelHolderCount = 0;
        int glassHolderCount = 0;
        int brickHolderCount = 0;
        int modHolderCount = 0;
        int ironBlockCount = 0;
        int steelBlockCount = 0;
        int glassBlockCount = 0;
        int brickBlockCount = 0;
        int modBlockCount = 0;

        IronBlock[] allIronBlocks = Object.FindObjectsOfType<IronBlock>();
        foreach (IronBlock block in allIronBlocks)
        {
            if (block != null)
            {
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.ironHolders[ironHolderCount].transform;
                    ironBlockCount++;
                }

                Transform[] blocks = gameManager.ironHolders[ironHolderCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 500)
                {
                    if (ironHolderCount == gameManager.ironHolders.Length - 1)
                    {
                        List<GameObject> ironList = gameManager.ironHolders.ToList();
                        GameObject ironHolder = Object.Instantiate(gameManager.ironHolder, gameManager.transform.position, gameManager.transform.rotation);
                        ironHolder.transform.parent = gameManager.builtObjects.transform;
                        ironHolder.GetComponent<MeshPainter>().ID = ironHolderCount + 1;
                        ironHolder.SetActive(false);
                        ironList.Add(ironHolder);
                        gameManager.ironHolders = ironList.ToArray();
                    }
                    ironHolderCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 250)
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
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.glassHolders[glassHolderCount].transform;
                    glassBlockCount++;
                }

                Transform[] blocks = gameManager.glassHolders[glassHolderCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 500)
                {
                    if (glassHolderCount == gameManager.glassHolders.Length - 1)
                    {
                        List<GameObject> glassList = gameManager.glassHolders.ToList();
                        GameObject glassHolder = Object.Instantiate(gameManager.glassHolder, gameManager.transform.position, gameManager.transform.rotation);
                        glassHolder.transform.parent = gameManager.builtObjects.transform;
                        glassHolder.GetComponent<MeshPainter>().ID = glassHolderCount + 1;
                        glassHolder.SetActive(false);
                        glassList.Add(glassHolder);
                        gameManager.glassHolders = glassList.ToArray();
                    }
                    glassHolderCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 250)
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
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.steelHolders[steelHolderCount].transform;
                    steelBlockCount++;
                }

                Transform[] blocks = gameManager.steelHolders[steelHolderCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 500)
                {
                    if (steelHolderCount == gameManager.steelHolders.Length - 1)
                    {
                        List<GameObject> steelList = gameManager.steelHolders.ToList();
                        GameObject steelHolder = Object.Instantiate(gameManager.steelHolder, gameManager.transform.position, gameManager.transform.rotation);
                        steelHolder.transform.parent = gameManager.builtObjects.transform;
                        steelHolder.GetComponent<MeshPainter>().ID = steelHolderCount + 1;
                        steelHolder.SetActive(false);
                        steelList.Add(steelHolder);
                        gameManager.steelHolders = steelList.ToArray();
                    }
                    steelHolderCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 250)
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
                if (block.GetComponent<PhysicsHandler>().IsSupported())
                {
                    block.transform.parent = gameManager.brickHolders[brickHolderCount].transform;
                    brickBlockCount++;
                }

                Transform[] blocks = gameManager.brickHolders[brickHolderCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 500)
                {
                    if (brickHolderCount == gameManager.brickHolders.Length - 1)
                    {
                        List<GameObject> brickList = gameManager.brickHolders.ToList();
                        GameObject brickHolder = Object.Instantiate(gameManager.brickHolder, gameManager.transform.position, gameManager.transform.rotation);
                        brickHolder.transform.parent = gameManager.builtObjects.transform;
                        brickHolder.GetComponent<MeshPainter>().ID = brickHolderCount + 1;
                        brickHolder.SetActive(false);
                        brickList.Add(brickHolder);
                        gameManager.brickHolders = brickList.ToArray();
                    }
                    brickHolderCount++;
                }
            }
            combineInterval++;
            if (combineInterval >= 250)
            {
                combineInterval = 0;
                yield return null;
            }
        }

        int modBlockIndex = 0;
        foreach (string blockType in gameManager.modBlockNames)
        {
            ModBlock[] allModBlocks = Object.FindObjectsOfType<ModBlock>();
            foreach (ModBlock block in allModBlocks)
            {
                if (block != null)
                {
                    if (block.blockName == blockType)
                    {
                        GameObject[] currentBlockType = gameManager.modBlockHolders[modBlockIndex];
                        if (block.GetComponent<PhysicsHandler>().IsSupported())
                        {
                            block.transform.parent = currentBlockType[modHolderCount].transform;
                            modBlockCount++;
                        }

                        Transform[] blocks = currentBlockType[modHolderCount].GetComponentsInChildren<Transform>(true);
                        if (blocks.Length >= 500)
                        {
                            if (modHolderCount == currentBlockType.Length - 1)
                            {
                                List<GameObject> modBlockList = currentBlockType.ToList();
                                GameObject modBlockHolder = Object.Instantiate(gameManager.modBlockHolder, gameManager.transform.position, gameManager.transform.rotation);
                                modBlockHolder.transform.parent = gameManager.builtObjects.transform;
                                SetMaterial(modBlockHolder, blockType);
                                modBlockHolder.SetActive(false);
                                modBlockList.Add(modBlockHolder);
                                gameManager.modBlockHolders[modBlockIndex] = modBlockList.ToArray();
                            }
                            modHolderCount++;
                        }
                    }
                }
                combineInterval++;
                if (combineInterval >= 250)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            modHolderCount = 0;
            modBlockIndex++;
        }

        combineInterval = 0;

        if (ironBlockCount > 0)
        {
            for (int i = 0; i < gameManager.ironHolders.Length; i++)
            {
                GameObject realObject = gameManager.ironHolders[i];
                SpawnDummy(realObject, gameManager.ironHolder, "Iron Dummy");
                combineInterval++;
                if (combineInterval >= 10)
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
            for (int i = 0; i < gameManager.steelHolders.Length; i++)
            {
                GameObject realObject = gameManager.steelHolders[i];
                SpawnDummy(realObject, gameManager.steelHolder, "Steel Dummy");
                combineInterval++;
                if (combineInterval >= 10)
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
            for (int i = 0; i < gameManager.glassHolders.Length; i++)
            {
                GameObject realObject = gameManager.glassHolders[i];
                SpawnDummy(realObject, gameManager.glassHolder, "Glass Dummy");
                combineInterval++;
                if (combineInterval >= 10)
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
            for (int i = 0; i < gameManager.brickHolders.Length; i++)
            {
                GameObject realObject = gameManager.brickHolders[i];
                SpawnDummy(realObject, gameManager.brickHolder, "Brick Dummy");
                combineInterval++;
                if (combineInterval >= 10)
                {
                    combineInterval = 0;
                    yield return null;
                }
            }
            brickMeshRequired = true;
            gameManager.replacingMeshFilters = true;
        }

        if (modBlockCount > 0)
        {
            modBlockIndex = 0;
            foreach (string blockName in gameManager.modBlockNames)
            {
                GameObject[] currentBlockType = gameManager.modBlockHolders[modBlockIndex];
                for (int i = 0; i < currentBlockType.Length; i++)
                {
                    GameObject realObject = currentBlockType[i];
                    SpawnDummy(realObject, gameManager.modBlockHolder, blockName + " Dummy");
                    combineInterval++;
                    if (combineInterval >= 10)
                    {
                        combineInterval = 0;
                        yield return null;
                    }
                }
                modMeshRequired = true;
                gameManager.replacingMeshFilters = true;
                modBlockIndex++;
            }
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
            foreach (GameObject holder in gameManager.ironHolders)
            {
                CreateCombinedMesh(holder);
                ironCombineInterval++;
                if (ironCombineInterval >= 10)
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
            foreach (GameObject holder in gameManager.glassHolders)
            {
                CreateCombinedMesh(holder);
                glassCombineInterval++;
                if (glassCombineInterval >= 10)
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
            foreach (GameObject holder in gameManager.steelHolders)
            {
                CreateCombinedMesh(holder);
                steelCombineInterval++;
                if (steelCombineInterval >= 10)
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
            foreach (GameObject holder in gameManager.brickHolders)
            {
                CreateCombinedMesh(holder);
                brickCombineInterval++;
                if (brickCombineInterval >= 10)
                {
                    yield return null;
                    brickCombineInterval = 0;
                }
            }
            brickMeshRequired = false;
        }

        if (modMeshRequired == true)
        {
            int modBlockIndex = 0;
            foreach (string blockName in gameManager.modBlockNames)
            {
                int modCombineInterval = 0;
                foreach (GameObject holder in gameManager.modBlockHolders[modBlockIndex])
                {
                    CreateCombinedMesh(holder);
                    modCombineInterval++;
                    if (modCombineInterval >= 10)
                    {
                        yield return null;
                        modCombineInterval = 0;
                    }
                }
                modBlockIndex++;
            }
            modMeshRequired = false;
        }

        HolderDummy[] allHolders = Object.FindObjectsOfType<HolderDummy>();
        int dummyDestroyInterval = 0;
        foreach (HolderDummy h in allHolders)
        {
            if (h != null)
            {
                Object.Destroy(h.gameObject);
                dummyDestroyInterval++;
                if (dummyDestroyInterval >= 50)
                {
                    yield return null;
                    dummyDestroyInterval = 0;
                }
            }
        }
        gameManager.working = false;
    }
}
