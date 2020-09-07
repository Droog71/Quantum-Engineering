using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMeshManager
{
    private GameManager manager;

    public CombinedMeshManager(GameManager manager)
    {
        this.manager = manager;
    }

    // Separates combined meshes into blocks
    public void SeparateBlocks(Vector3 target, string type, bool building)
    {
        if (manager.working == false && manager.GetComponent<StateManager>().saving == false)
        {
            if (building == true)
            {
                CombineBlocks();
            }
            manager.separateCoroutine = manager.StartCoroutine(BlockSeparationCoroutine(target, type));
        }
        Transform[] allBlocks = manager.builtObjects.GetComponentsInChildren<Transform>(true);
        manager.totalBlockCount = allBlocks.Length;
        if (manager.totalBlockCount >= 12000 && manager.blockLimitReached == false)
        {
            manager.blockLimitReached = true;
        }
        else if (manager.totalBlockCount < 12000 && manager.blockLimitReached == true)
        {
            manager.blockLimitReached = false;
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
            foreach (GameObject obj in manager.ironBlocks)
            {
                Transform[] blocks = manager.ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform i in blocks)
                {
                    if (i != null)
                    {
                        float distance = Vector3.Distance(i.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            i.gameObject.SetActive(true);
                            i.parent = manager.builtObjects.transform;
                            totalIron++;
                        }
                        if (distance < 20 && type.Equals("iron"))
                        {
                            i.gameObject.SetActive(true);
                            i.parent = manager.builtObjects.transform; ;
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
            foreach (GameObject obj in manager.glass)
            {
                Transform[] glassBlocks = manager.glass[glassCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform g in glassBlocks)
                {
                    if (g != null)
                    {
                        float distance = Vector3.Distance(g.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            g.gameObject.SetActive(true);
                            g.parent = manager.builtObjects.transform;
                            totalGlass++;
                        }
                        if (distance < 20 && type.Equals("glass"))
                        {
                            g.gameObject.SetActive(true);
                            g.parent = manager.builtObjects.transform; ;
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
            foreach (GameObject obj in manager.steel)
            {
                Transform[] steelBlocks = manager.steel[steelCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform s in steelBlocks)
                {
                    if (s != null)
                    {
                        float distance = Vector3.Distance(s.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            s.gameObject.SetActive(true);
                            s.parent = manager.builtObjects.transform;
                            totalSteel++;
                        }
                        if (distance < 20 && type.Equals("steel"))
                        {
                            s.gameObject.SetActive(true);
                            s.parent = manager.builtObjects.transform;
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
            foreach (GameObject obj in manager.bricks)
            {
                Transform[] brickBlocks = manager.bricks[brickCount].GetComponentsInChildren<Transform>(true);
                foreach (Transform b in brickBlocks)
                {
                    if (b != null)
                    {
                        float distance = Vector3.Distance(b.position, target);
                        if (distance < 40 && type.Equals("all"))
                        {
                            b.gameObject.SetActive(true);
                            b.parent = manager.builtObjects.transform;
                            totalBrick++;
                        }
                        if (distance < 20 && type.Equals("brick"))
                        {
                            b.gameObject.SetActive(true);
                            b.parent = manager.builtObjects.transform; ;
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
                foreach (GameObject obj in manager.ironBlocks)
                {
                    manager.ironBlocksDummy[ironCount] = Object.Instantiate(manager.ironHolder, manager.transform.position, manager.transform.rotation);
                    manager.ironBlocksDummy[ironCount].transform.parent = manager.builtObjects.transform;
                    manager.ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (manager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (manager.ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = manager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                manager.ironMeshRequired = true;
                manager.waitingForDestroy = true;
            }
            if (totalGlass > 0)
            {
                foreach (GameObject obj in manager.glass)
                {
                    manager.glassDummy[glassCount] = Object.Instantiate(manager.glassHolder, manager.transform.position, manager.transform.rotation);
                    manager.glassDummy[glassCount].transform.parent = manager.builtObjects.transform;
                    manager.glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (manager.glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (manager.glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.glassDummy[glassCount].GetComponent<MeshFilter>().mesh = manager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                manager.glassMeshRequired = true;
                manager.waitingForDestroy = true;
            }
            if (totalSteel > 0)
            {
                foreach (GameObject obj in manager.steel)
                {
                    manager.steelDummy[steelCount] = Object.Instantiate(manager.steelHolder, manager.transform.position, manager.transform.rotation);
                    manager.steelDummy[steelCount].transform.parent = manager.builtObjects.transform;
                    manager.steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (manager.steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (manager.steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.steelDummy[steelCount].GetComponent<MeshFilter>().mesh = manager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                manager.steelMeshRequired = true;
                manager.waitingForDestroy = true;
            }
            if (totalBrick > 0)
            {
                foreach (GameObject obj in manager.bricks)
                {
                    manager.bricksDummy[brickCount] = Object.Instantiate(manager.brickHolder, manager.transform.position, manager.transform.rotation);
                    manager.bricksDummy[brickCount].transform.parent = manager.builtObjects.transform;
                    manager.bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (manager.bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (manager.bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = manager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                manager.brickMeshRequired = true;
                manager.waitingForDestroy = true;
            }

            if (manager.waitingForDestroy == false)
            {
                manager.working = false;
            }
        }
    }

    // Creates combined meshes from placed building blocks
    public void CombineBlocks()
    {
        if (manager.working == false && manager.GetComponent<StateManager>().saving == false)
        {
            manager.working = true;
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
                Transform[] blocks = manager.ironBlocks[ironCount].GetComponentsInChildren<Transform>(true);
                if (blocks.Length >= 300)
                {
                    ironCount++;
                }
                if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                {
                    block.transform.parent = manager.ironBlocks[ironCount].transform;
                    if (manager.initIron == false)
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
                    Transform[] blocks = manager.glass[glassCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        glassCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = manager.glass[glassCount].transform;
                        if (manager.initGlass == false)
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
                    Transform[] blocks = manager.steel[steelCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        steelCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = manager.steel[steelCount].transform;
                        if (manager.initSteel == false)
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
                    Transform[] blocks = manager.bricks[brickCount].GetComponentsInChildren<Transform>(true);
                    if (blocks.Length >= 300)
                    {
                        brickCount++;
                    }
                    if (block.GetComponent<PhysicsHandler>().falling == false && block.GetComponent<PhysicsHandler>().fallingStack == false && block.GetComponent<PhysicsHandler>().needsSupportCheck == false)
                    {
                        block.transform.parent = manager.bricks[brickCount].transform;
                        if (manager.initBrick == false)
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
                foreach (GameObject obj in manager.ironBlocks)
                {
                    manager.ironBlocksDummy[ironCount] = Object.Instantiate(manager.ironHolder, manager.transform.position, manager.transform.rotation);
                    manager.ironBlocksDummy[ironCount].transform.parent = manager.builtObjects.transform;
                    manager.ironBlocksDummy[ironCount].AddComponent<HolderDummy>();
                    if (manager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.ironBlocksDummy[ironCount].AddComponent<MeshFilter>();
                    }
                    if (manager.ironBlocks[ironCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.ironBlocksDummy[ironCount].GetComponent<MeshFilter>().mesh = manager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.ironBlocks[ironCount].GetComponent<MeshFilter>());
                    ironCount++;
                }
                manager.ironMeshRequired = true;
                manager.waitingForDestroy = true;
            }

            if (steelBlockCount > 0)
            {
                steelCount = 0;
                foreach (GameObject obj in manager.steel)
                {
                    manager.steelDummy[steelCount] = Object.Instantiate(manager.steelHolder, manager.transform.position, manager.transform.rotation);
                    manager.steelDummy[steelCount].transform.parent = manager.builtObjects.transform;
                    manager.steelDummy[steelCount].AddComponent<HolderDummy>();
                    if (manager.steelDummy[steelCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.steelDummy[steelCount].AddComponent<MeshFilter>();
                    }
                    if (manager.steel[steelCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.steelDummy[steelCount].GetComponent<MeshFilter>().mesh = manager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.steel[steelCount].GetComponent<MeshFilter>());
                    steelCount++;
                }
                manager.steelMeshRequired = true;
                manager.waitingForDestroy = true;
            }

            if (glassBlockCount > 0)
            {
                glassCount = 0;
                foreach (GameObject obj in manager.glass)
                {
                    manager.glassDummy[glassCount] = Object.Instantiate(manager.glassHolder, manager.transform.position, manager.transform.rotation);
                    manager.glassDummy[glassCount].transform.parent = manager.builtObjects.transform;
                    manager.glassDummy[glassCount].AddComponent<HolderDummy>();
                    if (manager.glassDummy[glassCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.glassDummy[glassCount].AddComponent<MeshFilter>();
                    }
                    if (manager.glass[glassCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.glassDummy[glassCount].GetComponent<MeshFilter>().mesh = manager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.glass[glassCount].GetComponent<MeshFilter>());
                    glassCount++;
                }
                manager.glassMeshRequired = true;
                manager.waitingForDestroy = true;
            }

            if (brickBlockCount > 0)
            {
                brickCount = 0;
                foreach (GameObject obj in manager.bricks)
                {
                    manager.bricksDummy[brickCount] = Object.Instantiate(manager.brickHolder, manager.transform.position, manager.transform.rotation);
                    manager.bricksDummy[brickCount].transform.parent = manager.builtObjects.transform;
                    manager.bricksDummy[brickCount].AddComponent<HolderDummy>();
                    if (manager.bricksDummy[brickCount].GetComponent<MeshFilter>() == null)
                    {
                        manager.bricksDummy[brickCount].AddComponent<MeshFilter>();
                    }
                    if (manager.bricks[brickCount].GetComponent<MeshFilter>() != null)
                    {
                        manager.bricksDummy[brickCount].GetComponent<MeshFilter>().mesh = manager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                    }
                    Object.Destroy(manager.bricks[brickCount].GetComponent<MeshFilter>());
                    brickCount++;
                }
                manager.brickMeshRequired = true;
                manager.waitingForDestroy = true;
            }

            manager.blocksCombined = true;
            if (manager.waitingForDestroy == false)
            {
                manager.working = false;
            }
        }
    }

    public void CombineMeshes()
    {
        manager.combineCoroutine = manager.StartCoroutine(CombineMeshCoroutine());
    }

    private IEnumerator CombineMeshCoroutine()
    {
        if (manager.ironMeshRequired == true)
        {
            int ironCount = 0;
            int ironCombineInterval = 0;
            //Debug.Log("Started mesh combine for iron at: " + System.DateTime.Now);
            foreach (GameObject obj in manager.ironBlocks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] meshFilters = manager.ironBlocks[ironCount].GetComponentsInChildren<MeshFilter>(true);
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
                if (manager.ironBlocks[ironCount].GetComponent<MeshFilter>() == null)
                {
                    manager.ironBlocks[ironCount].AddComponent<MeshFilter>();
                }
                manager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh = new Mesh();
                manager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                manager.ironBlocks[ironCount].GetComponent<MeshCollider>().sharedMesh = manager.ironBlocks[ironCount].GetComponent<MeshFilter>().mesh;
                manager.ironBlocks[ironCount].GetComponent<MeshCollider>().enabled = true;
                if (meshFilters.Length > 0)
                {
                    manager.ironBlocks[ironCount].SetActive(true);
                }
                ironCount++;
                ironCombineInterval++;
                if (ironCombineInterval >= 50)
                {
                    yield return null;
                    ironCombineInterval = 0;
                }
            }
            manager.ironMeshRequired = false;
            if (manager.initIron == false)
            {
                manager.initIron = true;
                manager.clearIronDummies = true;
                FileBasedPrefs.SetBool(manager.GetComponent<StateManager>().WorldName + "manager.initIron", true);
            }
            //Debug.Log("Finished mesh combine for iron at: " + System.DateTime.Now);
        }

        if (manager.glassMeshRequired == true)
        {
            int glassCount = 0;
            int glassCombineInterval = 0;
            //Debug.Log("Started mesh combine for glass at: " + System.DateTime.Now);
            foreach (GameObject obj in manager.glass)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] glassMeshFilters = manager.glass[glassCount].GetComponentsInChildren<MeshFilter>(true);
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
                if (manager.glass[glassCount].GetComponent<MeshFilter>() == null)
                {
                    manager.glass[glassCount].AddComponent<MeshFilter>();
                }
                manager.glass[glassCount].GetComponent<MeshFilter>().mesh = new Mesh();
                manager.glass[glassCount].GetComponent<MeshFilter>().mesh.CombineMeshes(glassCombine);
                manager.glass[glassCount].GetComponent<MeshCollider>().sharedMesh = manager.glass[glassCount].GetComponent<MeshFilter>().mesh;
                manager.glass[glassCount].GetComponent<MeshCollider>().enabled = true;
                if (glassMeshFilters.Length > 0)
                {
                    manager.glass[glassCount].SetActive(true);
                }
                glassCount++;
                glassCombineInterval++;
                if (glassCombineInterval >= 50)
                {
                    yield return null;
                    glassCombineInterval = 0;
                }
            }
            manager.glassMeshRequired = false;
            if (manager.initGlass == false)
            {
                manager.initGlass = true;
                manager.clearGlassDummies = true;
                FileBasedPrefs.SetBool(manager.GetComponent<StateManager>().WorldName + "initGlass", true);
            }
            //Debug.Log("Finished mesh combine for glass at: " + System.DateTime.Now);
        }

        if (manager.steelMeshRequired == true)
        {
            int steelCount = 0;
            int steelCombineInterval = 0;
            //Debug.Log("Started mesh combine for steel at: " + System.DateTime.Now);
            foreach (GameObject obj in manager.steel)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] steelMeshFilters = manager.steel[steelCount].GetComponentsInChildren<MeshFilter>(true);
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
                if (manager.steel[steelCount].GetComponent<MeshFilter>() == null)
                {
                    manager.steel[steelCount].AddComponent<MeshFilter>();
                }
                manager.steel[steelCount].GetComponent<MeshFilter>().mesh = new Mesh();
                manager.steel[steelCount].GetComponent<MeshFilter>().mesh.CombineMeshes(steelCombine);
                manager.steel[steelCount].GetComponent<MeshCollider>().sharedMesh = manager.steel[steelCount].GetComponent<MeshFilter>().mesh;
                manager.steel[steelCount].GetComponent<MeshCollider>().enabled = true;
                if (steelMeshFilters.Length > 0)
                {
                    manager.steel[steelCount].SetActive(true);
                }
                steelCount++;
                steelCombineInterval++;
                if (steelCombineInterval >= 50)
                {
                    yield return null;
                    steelCombineInterval = 0;
                }
            }
            manager.steelMeshRequired = false;
            if (manager.initSteel == false)
            {
                manager.initSteel = true;
                manager.clearSteelDummies = true;
                FileBasedPrefs.SetBool(manager.GetComponent<StateManager>().WorldName + "initSteel", true);
            }
            //Debug.Log("Finished mesh combine for steel at: " + System.DateTime.Now);
        }

        if (manager.brickMeshRequired == true)
        {
            int brickCount = 0;
            int brickCombineInterval = 0;
            //Debug.Log("Started mesh combine for bricks at: " + System.DateTime.Now);
            foreach (GameObject obj in manager.bricks)
            {
                //Combine all meshes outside of building range.
                MeshFilter[] brickMeshFilters = manager.bricks[brickCount].GetComponentsInChildren<MeshFilter>(true);
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
                if (manager.bricks[brickCount].GetComponent<MeshFilter>() == null)
                {
                    manager.bricks[brickCount].AddComponent<MeshFilter>();
                }
                manager.bricks[brickCount].GetComponent<MeshFilter>().mesh = new Mesh();
                manager.bricks[brickCount].GetComponent<MeshFilter>().mesh.CombineMeshes(brickCombine);
                manager.bricks[brickCount].GetComponent<MeshCollider>().sharedMesh = manager.bricks[brickCount].GetComponent<MeshFilter>().mesh;
                manager.bricks[brickCount].GetComponent<MeshCollider>().enabled = true;
                if (brickMeshFilters.Length > 0)
                {
                    manager.bricks[brickCount].SetActive(true);
                }
                brickCount++;
                brickCombineInterval++;
                if (brickCombineInterval >= 50)
                {
                    yield return null;
                    brickCombineInterval = 0;
                }
            }
            manager.brickMeshRequired = false;
            if (manager.initBrick == false)
            {
                manager.initBrick = true;
                manager.clearBrickDummies = true;
                FileBasedPrefs.SetBool(manager.GetComponent<StateManager>().WorldName + "initBrick", true);
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
        manager.working = false;
    }
}

