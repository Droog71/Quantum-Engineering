﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinedMeshManager
{
    private GameManager gameManager;
    private StateManager stateManager;
    private GameObject player;

    //! The Combined Mesh Manager handles all combined meshes for building blocks.
    //! All standard building blocks are stored in combined meshes after being placed.
    //! Parts of the combined mesh are converted to individual blocks when edited by the player.
    //! Meteor strikes and weapons also cause small sections of the combined mesh to separate.
    //! The combined mesh is refactored after changes occur.
    public CombinedMeshManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
        stateManager = gameManager.GetComponent<StateManager>();
        player = GameObject.Find("Player");
    }

    //! Sets up the material of an object.
    public void SetMaterial(GameObject obj, string blockType)
    {
        TextureDictionary textureDictionary = gameManager.GetComponent<TextureDictionary>();
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
                    mat.enableInstancing = true;
                }
                obj.GetComponent<Renderer>().material = mat;
            }
        }
    }

    //! Separates combined meshes into blocks
    public void RemoveBlock(BlockHolder bh, Vector3 point, bool explosion)
    {
        if (bh.unloaded == true)
        {
            bh.Load();
        }
        else if (bh.blockData != null)
        {
            Block blockToRemove = null;
            int indexToRemove = 0;
            foreach (BlockHolder.BlockInfo info in bh.blockData)
            {
                float distance = Vector3.Distance(info.position, point);
                if (distance < 5)
                {
                    Block[] blocks = bh.gameObject.GetComponentsInChildren<Block>(true);
                    foreach (Block block in blocks)
                    {
                        Vector3 pos = block.gameObject.transform.position;
                        Vector3 roundPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
                        Vector3 roundInfoPos = new Vector3(Mathf.Round(info.position.x), Mathf.Round(info.position.y), Mathf.Round(info.position.z));
                        if (roundPos == roundInfoPos)
                        {
                            indexToRemove = bh.blockData.IndexOf(info);
                            blockToRemove = block;
                        }
                    }
                }
            }
            if (blockToRemove != null)
            {
                if (explosion == true)
                {
                    blockToRemove.GetComponent<Block>().Explode();
                }
                else
                {
                    Object.Destroy(blockToRemove.gameObject);
                }
                bh.blockData.RemoveAt(indexToRemove);
                gameManager.meshObject = bh.gameObject;
                gameManager.Invoke("CreateMesh", 0.1f);
                bh.gameObject.SetActive(true);
            }
        }
    }

    //! Starts the coroutine to combine all blocks into combined meshes.
    public void CombineBlocks()
    {
        gameManager.blockCombineCoroutine = gameManager.StartCoroutine(BlockCombineCoroutine());
    }

    //! Creates combined meshes from placed building blocks.
    public IEnumerator BlockCombineCoroutine()
    {
        while (gameManager.combiningBlocks == true || stateManager.saving == true)
        {
            yield return null;
        }

        gameManager.combiningBlocks = true;

        int combineInterval = 0;
        int blockHolderCount = 0;
        int blockCount = 0;
        int blockIndex = 0;

        foreach (string blockType in gameManager.blockNames)
        {
            List<Block> allBlocks = Object.FindObjectsOfType<Block>().ToList();
            foreach (Block block in allBlocks)
            {
                if (block != null)
                {
                    if (block.blockName == blockType)
                    {
                        GameObject[] currentBlockType = gameManager.blockHolders[blockIndex];
                        float distance = Vector3.Distance(block.gameObject.transform.position, player.transform.position);
                        if (distance > gameManager.chunkSize || gameManager.dataSaveRequested == true)
                        {
                            block.transform.parent = currentBlockType[blockHolderCount].transform;
                            BlockHolder bh = currentBlockType[blockHolderCount].GetComponent<BlockHolder>();
                            if (bh.blockData == null)
                            {
                                bh.blockData = new List<BlockHolder.BlockInfo>();
                            }
                            Vector3 pos = block.gameObject.transform.position;
                            Quaternion rot = block.gameObject.transform.rotation;
                            bool blockExists = false;
                            foreach (BlockHolder.BlockInfo blockInfo in bh.blockData)
                            {
                                if (blockInfo.position == pos && blockInfo.rotation == rot)
                                {
                                    blockExists = true;
                                    break;
                                }
                            }
                            if (blockExists == false)
                            {
                                bh.blockData.Add(new BlockHolder.BlockInfo(block.gameObject.transform.position, block.gameObject.transform.rotation));
                                blockCount++;
                            }
                        }

                        Transform[] blocks = currentBlockType[blockHolderCount].GetComponentsInChildren<Transform>(true);
                        if (blocks.Length >= 500)
                        {
                            if (blockHolderCount == currentBlockType.Length - 1)
                            {
                                List<GameObject> blockList = currentBlockType.ToList();
                                GameObject blockHolder = Object.Instantiate(gameManager.blockHolder, gameManager.transform.position, gameManager.transform.rotation);
                                blockHolder.transform.parent = gameManager.builtObjects.transform;
                                SetMaterial(blockHolder, blockType);
                                blockHolder.GetComponent<BlockHolder>().blockType = blockType;
                                blockHolder.SetActive(false);
                                blockList.Add(blockHolder);
                                gameManager.blockHolders[blockIndex] = blockList.ToArray();
                            }
                            blockHolderCount++;
                        }
                    }
                }
                if (stateManager.worldLoaded == true)
                {
                    combineInterval++;
                    if (combineInterval >= 250)
                    {
                        combineInterval = 0;
                        yield return null;
                    }
                }
            }
            blockHolderCount = 0;
            blockIndex++;
        }
        combineInterval = 0;

        if (blockCount > 0)
        {
            CombineMeshes();
        }
        else
        {
            gameManager.combiningBlocks = false;
        }
    }

    //! Starts the combined mesh coroutine.
    public void CombineMeshes()
    {
        gameManager.meshCombineCoroutine = gameManager.StartCoroutine(CombineMeshCoroutine());
    }

    //! Creates combined meshes from blocks placed in the world.
    private IEnumerator CombineMeshCoroutine()
    {
        int blockIndex = 0;
        foreach (string blockName in gameManager.blockNames)
        {
            int combineInterval = 0;
            foreach (GameObject holder in gameManager.blockHolders[blockIndex])
            {
                CreateCombinedMesh(holder);
                if (stateManager.worldLoaded == true)
                {
                    combineInterval++;
                    if (combineInterval >= 10)
                    {
                        combineInterval = 0;
                        yield return null;
                    }
                }
            }
            blockIndex++;
        }

        gameManager.combiningBlocks = false;
    }

    //! Creats a combined mesh from all meshes attached to the given object.
    public void CreateCombinedMesh(GameObject holder)
    {
        if (holder != null)
        {
            MeshFilter[] meshFilters = holder.GetComponentsInChildren<MeshFilter>(true);
            List<MeshFilter> mfList = new List<MeshFilter>();
            foreach (MeshFilter mf in meshFilters)
            {
                if (mf != null)
                {
                    if (mf.gameObject != null)
                    {
                        if (mf.gameObject.GetComponent<Block>() != null)
                        {
                            mfList.Add(mf);
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
            if (holder.GetComponent<MeshCollider>() != null)
            {
                holder.GetComponent<MeshCollider>().sharedMesh = holder.GetComponent<MeshFilter>().mesh;
                holder.GetComponent<MeshCollider>().enabled = true;
            }
            if (meshFilters.Length > 0)
            {
                holder.SetActive(true);
            }
        }
    }
}
