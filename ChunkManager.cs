using System;
using System.Collections;
using UnityEngine;

//! This class controls update functions on instances of BlockHolder in the world.
//! One chunk is updated per frame.
public class ChunkManager : MonoBehaviour
{
    private bool busy;
    private Coroutine chunkUpdateCoroutine;

    private bool ShouldUpdate()
    {
        return busy == false &&
        GetComponent<StateManager>().worldLoaded == true;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (ShouldUpdate() == true)
        {
            chunkUpdateCoroutine = StartCoroutine(ChunkUpdateCoroutine());
        }
    }

    //! Calls the Load function on each BlockHolder in the world.
    private IEnumerator ChunkUpdateCoroutine()
    {
        busy = true;
        int interval = 0;
        int activeHolders = 0;
        BlockHolder[] blockHolders = FindObjectsOfType<BlockHolder>();
        foreach (BlockHolder blockHolder in blockHolders)
        {
            if (blockHolder != null && activeHolders < 5)
            {
                try
                {
                    if (blockHolder.unloaded == true)
                    {
                        blockHolder.Load();
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                interval++;
                if (interval >= blockHolders.Length * GetComponent<GameManager>().simulationSpeed)
                {
                    yield return null;
                    interval = 0;
                }
            }
        }

        busy = false;
    }
}