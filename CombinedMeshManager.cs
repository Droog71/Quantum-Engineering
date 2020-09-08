using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMeshManager
{
    private GameManager gameManager;

    public CombinedMeshManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // Separates combined meshes into blocks
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
        Transform[] allBlocks = gameManager.builtObjects.GetComponentsInChildren<Transform>(true);
        gameManager.totalBlockCount = allBlocks.Length;
        if (gameManager.totalBlockCount >= 12000 && gameManager.blockLimitReached == false)
        {
            gameManager.blockLimitReached = true;
        }
        else if (gameManager.totalBlockCount < 12000 && gameManager.blockLimitReached == true)
        {
            gameManager.blockLimitReached = false;
        }
    }

    // Separates combined meshes into blocks
    private IEnumerator BlockSeparationCoroutine(Vector3 target, string type)
    {
        if (target != null)
        {
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
                if (ironSeprationInterval >= 50)
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
                if (glassSeprationInterval >= 50)
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
                if (steelSeprationInterval >= 50)
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
                if (brickSeprationInterval >= 50)
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
                foreach (GameObject obj in gameManager.ironBlocks)
                {
                    gameManager.ironBlocksDummy[ironCount] = Object.Instantiate(gameManager.ironHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.ironBlocksDummy[ironCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (gameManager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                gameManager.ironMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }
            if (totalGlass > 0)
            {
                foreach (GameObject obj in gameManager.glass)
                {
                    gameManager.glassDummy[glassCount] = Object.Instantiate(gameManager.glassHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.glassDummy[glassCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (gameManager.glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.glassDummy[glassCount].GetComponent<MeshFilter>().mesh = gameManager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                gameManager.glassMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }
            if (totalSteel > 0)
            {
                foreach (GameObject obj in gameManager.steel)
                {
                    gameManager.steelDummy[steelCount] = Object.Instantiate(gameManager.steelHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.steelDummy[steelCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (gameManager.steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.steelDummy[steelCount].GetComponent<MeshFilter>().mesh = gameManager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                gameManager.steelMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }
            if (totalBrick > 0)
            {
                foreach (GameObject obj in gameManager.bricks)
                {
                    gameManager.bricksDummy[brickCount] = Object.Instantiate(gameManager.brickHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.bricksDummy[brickCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (gameManager.bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = gameManager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                gameManager.brickMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }

            if (gameManager.waitingForDestroy == false)
            {
                gameManager.working = false;
            }
        }
    }

    // Creates combined meshes from placed building blocks
    public void CombineBlocks()
    {
        if (gameManager.working == false && gameManager.GetComponent<StateManager>().saving == false)
        {
            gameManager.working = true;
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
                Transform[] blocks = gameManager.ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    ironCount++;
                }
                if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                {
                    block.transform.parent = gameManager.ironBlocks[ironCount].transform;
                    if (gameManager.initIron == false)
                    {
                        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        go.transform.position = block.transform.position;
                        go.transform.localScale = new Vector3(5, 5, 5);
                        go.GetComponent<Renderer>().material = block.GetComponent<Renderer>().material;
                        go.AddComponent<BlockDummy>().type = "iron";
                    }
                    ironBlockCount++;
                }
            }

            Glass[] allGlassBlocks = Object.FindObjectsOfType<Glass>();
            foreach (Glass block in allGlassBlocks)
            {
                if (block.GetComponent<Glass>() != null)
                {
                    Transform[] blocks = gameManager.glass[glassCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        glassCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = gameManager.glass[glassCount].transform;
                        if (gameManager.initGlass == false)
                        {
                            //UnityEngine.Debug.Log("CREATING GLASS BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = block.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = block.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "glass";
                        }
                        glassBlockCount++;
                    }
                }
            }

            Steel[] allSteelBlocks = Object.FindObjectsOfType<Steel>();
            foreach (Steel block in allSteelBlocks)
            {
                if (block.GetComponent<Steel>() != null)
                {
                    Transform[] blocks = gameManager.steel[steelCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        steelCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = gameManager.steel[steelCount].transform;
                        if (gameManager.initSteel == false)
                        {
                            //UnityEngine.Debug.Log("CREATING STEEL BLOCK DUMMIES");
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = block.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = block.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "steel";
                        }
                        steelBlockCount++;
                    }
                }
            }

            Brick[] allBrickBlocks = Object.FindObjectsOfType<Brick>();
            foreach (Brick block in allBrickBlocks)
            {
                if (block.GetComponent<Brick>() != null)
                {
                    Transform[] blocks = gameManager.bricks[brickCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        brickCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = gameManager.bricks[brickCount].transform;
                        if (gameManager.initBrick == false)
                        {
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = block.transform.position;
                            go.transform.localScale = new Vector3(5, 5, 5);
                            go.GetComponent<Renderer>().material = block.GetComponent<Renderer>().material;
                            go.AddComponent<BlockDummy>().type = "brick";
                        }
                        brickBlockCount++;
                    }
                }
            }

            if (ironBlockCount > 0)
            {
                ironCount = 0;
                foreach (GameObject obj in gameManager.ironBlocks)
                {
                    gameManager.ironBlocksDummy[ironCount] = Object.Instantiate(gameManager.ironHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.ironBlocksDummy[ironCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (gameManager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                gameManager.ironMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }

            if (steelBlockCount > 0)
            {
                steelCount = 0;
                foreach (GameObject obj in gameManager.steel)
                {
                    gameManager.steelDummy[steelCount] = Object.Instantiate(gameManager.steelHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.steelDummy[steelCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (gameManager.steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.steelDummy[steelCount].GetComponent<MeshFilter>().mesh = gameManager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                gameManager.steelMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }

            if (glassBlockCount > 0)
            {
                glassCount = 0;
                foreach (GameObject obj in gameManager.glass)
                {
                    gameManager.glassDummy[glassCount] = Object.Instantiate(gameManager.glassHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.glassDummy[glassCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (gameManager.glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.glassDummy[glassCount].GetComponent<MeshFilter>().mesh = gameManager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                gameManager.glassMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }

            if (brickBlockCount > 0)
            {
                brickCount = 0;
                foreach (GameObject obj in gameManager.bricks)
                {
                    gameManager.bricksDummy[brickCount] = Object.Instantiate(gameManager.brickHolder, gameManager.transform.position, gameManager.transform.rotation);
                    gameManager.bricksDummy[brickCount].transform.parent = gameManager.builtObjects.transform;
                    gameManager.bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (gameManager.bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        gameManager.bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (gameManager.bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        gameManager.bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = gameManager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(gameManager.bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                gameManager.brickMeshRequired = true;
                gameManager.waitingForDestroy = true;
            }

            gameManager.blocksCombined = true;
            if (gameManager.waitingForDestroy == false)
            {
                gameManager.working = false;
            }
        }
    }

    public void CombineMeshes()
    {
        gameManager.combineCoroutine = gameManager.StartCoroutine(CombineMeshCoroutine());
    }

    private IEnumerator CombineMeshCoroutine()
    {
        if (gameManager.ironMeshRequired == true)
        {
            int ironCount = 0;
            int ironCombineInterval = 0;
            //Debug.Log("Started mesh combine for iron at: " + System.DateTime.Now);
            foreach (GameObject obj in gameManager.ironBlocks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] meshFilters = gameManager.ironBlocks[ironCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in meshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                meshFilters = mfList.ToArray();
                CombineInstance[] combine = new CombineInstance[meshFilters.Length];
                int i = 0;
                while (i < meshFilters.Length)
                {
                    combine[i].mesh = meshFilters[i].sharedMesh;
                    combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                    meshFilters[i].gameObject.SetActive(false);
                    i++;
                }
                if (gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>() == null)
                {
                    gameManager.ironBlocks[ironCount].AddComponent<MeshFilter>();
                }
                gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh = new Mesh();
                gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                gameManager.ironBlocks[ironCount].GetComponent<MeshCollider>().sharedMesh = gameManager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                gameManager.ironBlocks[ironCount].GetComponent<MeshCollider>().enabled = true;
                if (meshFilters.Length > 0)
                {
                    gameManager.ironBlocks[ironCount].SetActive(true);
                }
                ironCount++;
                ironCombineInterval++;
                if (ironCombineInterval >= 50)
                {
                    yield return null;
                    ironCombineInterval = 0;
                }
            }
            gameManager.ironMeshRequired = false;
            if (gameManager.initIron == false)
            {
                gameManager.initIron = true;
                gameManager.clearIronDummies = true;
                FileBasedPrefs.SetBool(gameManager.GetComponent<StateManager>().WorldName + "gameManager.initIron", true);
            }
            //Debug.Log("Finished mesh combine for iron at: " + System.DateTime.Now);
        }

        if (gameManager.glassMeshRequired == true)
        {
            int glassCount = 0;
            int glassCombineInterval = 0;
            //Debug.Log("Started mesh combine for glass at: " + System.DateTime.Now);
            foreach (GameObject obj in gameManager.glass)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] glassMeshFilters = gameManager.glass[glassCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in glassMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                glassMeshFilters = mfList.ToArray();
                CombineInstance[] glassCombine = new CombineInstance[glassMeshFilters.Length];
                int g = 0;
                while (g < glassMeshFilters.Length)
                {
                    glassCombine[g].mesh = glassMeshFilters[g].sharedMesh;
                    glassCombine[g].transform = glassMeshFilters[g].transform.localToWorldMatrix;
                    glassMeshFilters[g].gameObject.SetActive(false);
                    g++;
                }
                if (gameManager.glass[glassCount].GetComponent<MeshFilter>() == null)
                {
                    gameManager.glass[glassCount].AddComponent<MeshFilter>();
                }
                gameManager.glass[glassCount].GetComponent<MeshFilter>().mesh = new Mesh();
                gameManager.glass[glassCount].GetComponent<MeshFilter>().mesh.CombineMeshes(glassCombine);
                gameManager.glass[glassCount].GetComponent<MeshCollider>().sharedMesh = gameManager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                gameManager.glass[glassCount].GetComponent<MeshCollider>().enabled = true;
                if (glassMeshFilters.Length > 0)
                {
                    gameManager.glass[glassCount].SetActive(true);
                }
                glassCount++;
                glassCombineInterval++;
                if (glassCombineInterval >= 50)
                {
                    yield return null;
                    glassCombineInterval = 0;
                }
            }
            gameManager.glassMeshRequired = false;
            if (gameManager.initGlass == false)
            {
                gameManager.initGlass = true;
                gameManager.clearGlassDummies = true;
                FileBasedPrefs.SetBool(gameManager.GetComponent<StateManager>().WorldName + "initGlass", true);
            }
            //Debug.Log("Finished mesh combine for glass at: " + System.DateTime.Now);
        }

        if (gameManager.steelMeshRequired == true)
        {
            int steelCount = 0;
            int steelCombineInterval = 0;
            //Debug.Log("Started mesh combine for steel at: " + System.DateTime.Now);
            foreach (GameObject obj in gameManager.steel)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] steelMeshFilters = gameManager.steel[steelCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in steelMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                steelMeshFilters = mfList.ToArray();
                CombineInstance[] steelCombine = new CombineInstance[steelMeshFilters.Length];
                int s = 0;
                while (s < steelMeshFilters.Length)
                {
                    steelCombine[s].mesh = steelMeshFilters[s].sharedMesh;
                    steelCombine[s].transform = steelMeshFilters[s].transform.localToWorldMatrix;
                    steelMeshFilters[s].gameObject.SetActive(false);
                    s++;
                }
                if (gameManager.steel[steelCount].GetComponent<MeshFilter>() == null)
                {
                    gameManager.steel[steelCount].AddComponent<MeshFilter>();
                }
                gameManager.steel[steelCount].GetComponent<MeshFilter>().mesh = new Mesh();
                gameManager.steel[steelCount].GetComponent<MeshFilter>().mesh.CombineMeshes(steelCombine);
                gameManager.steel[steelCount].GetComponent<MeshCollider>().sharedMesh = gameManager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                gameManager.steel[steelCount].GetComponent<MeshCollider>().enabled = true;
                if (steelMeshFilters.Length > 0)
                {
                    gameManager.steel[steelCount].SetActive(true);
                }
                steelCount++;
                steelCombineInterval++;
                if (steelCombineInterval >= 50)
                {
                    yield return null;
                    steelCombineInterval = 0;
                }
            }
            gameManager.steelMeshRequired = false;
            if (gameManager.initSteel == false)
            {
                gameManager.initSteel = true;
                gameManager.clearSteelDummies = true;
                FileBasedPrefs.SetBool(gameManager.GetComponent<StateManager>().WorldName + "initSteel", true);
            }
            //Debug.Log("Finished mesh combine for steel at: " + System.DateTime.Now);
        }

        if (gameManager.brickMeshRequired == true)
        {
            int brickCount = 0;
            int brickCombineInterval = 0;
            //Debug.Log("Started mesh combine for bricks at: " + System.DateTime.Now);
            foreach (GameObject obj in gameManager.bricks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] brickMeshFilters = gameManager.bricks[brickCount].GetComponentsInChildren<MeshFilter>(true);
                List<MeshFilter> mfList = new List<MeshFilter>();
                foreach (MeshFilter mf in brickMeshFilters)
                {
                    if (mf != null)
                    {
                        if (mf.gameObject != null)
                        {
                            if (mf.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (mf.gameObject.GetComponent<PhysicsHandler>().falling == false && mf.gameObject.GetComponent<PhysicsHandler>().fallingStack == false && mf.gameObject.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                                {
                                    mfList.Add(mf);
                                }
                            }
                        }
                    }
                }
                brickMeshFilters = mfList.ToArray();
                CombineInstance[] brickCombine = new CombineInstance[brickMeshFilters.Length];
                int b = 0;
                while (b < brickMeshFilters.Length)
                {
                    brickCombine[b].mesh = brickMeshFilters[b].sharedMesh;
                    brickCombine[b].transform = brickMeshFilters[b].transform.localToWorldMatrix;
                    brickMeshFilters[b].gameObject.SetActive(false);
                    b++;
                }
                if (gameManager.bricks[brickCount].GetComponent<MeshFilter>() == null)
                {
                    gameManager.bricks[brickCount].AddComponent<MeshFilter>();
                }
                gameManager.bricks[brickCount].GetComponent<MeshFilter>().mesh = new Mesh();
                gameManager.bricks[brickCount].GetComponent<MeshFilter>().mesh.CombineMeshes(brickCombine);
                gameManager.bricks[brickCount].GetComponent<MeshCollider>().sharedMesh = gameManager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                gameManager.bricks[brickCount].GetComponent<MeshCollider>().enabled = true;
                if (brickMeshFilters.Length > 0)
                {
                    gameManager.bricks[brickCount].SetActive(true);
                }
                brickCount++;
                brickCombineInterval++;
                if (brickCombineInterval >= 50)
                {
                    yield return null;
                    brickCombineInterval = 0;
                }
            }
            gameManager.brickMeshRequired = false;
            if (gameManager.initBrick == false)
            {
                gameManager.initBrick = true;
                gameManager.clearBrickDummies = true;
                FileBasedPrefs.SetBool(gameManager.GetComponent<StateManager>().WorldName + "initBrick", true);
            }
        }

        HolderDummy[] allHolders = Object.FindObjectsOfType<HolderDummy>();
        int dummyDestroyInterval = 0;
        foreach (HolderDummy h in allHolders)
        {
            Object.Destroy(h.gameObject);
            dummyDestroyInterval++;
            if (dummyDestroyInterval >= 50)
            {
                yield return null;
                dummyDestroyInterval = 0;
            }
        }
        gameManager.working = false;
    }
}

