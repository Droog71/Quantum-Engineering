using System;
using System.Collections;
using UnityEngine;

//! This class controls PhysicsHandler update functions on blocks in the world.
//! One block is updated per frame.
public class BlockManager : MonoBehaviour
{
    private bool busy;
    private Coroutine blockUpdateCoroutine;

    private bool ShouldUpdate()
    {
        return busy == false &&
        GetComponent<StateManager>().worldLoaded == true &&
        GetComponent<GameManager>().blockPhysics == true;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (ShouldUpdate() == true)
        {
            blockUpdateCoroutine = StartCoroutine(BlockUpdateCoroutine());
        }
    }

    //! Calls the UpdateBlock function on each block in the world, yielding after each call.
    private IEnumerator BlockUpdateCoroutine()
    {
        busy = true;
        int interval = 0;
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            try
            {
                block.UpdateBlock();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            interval++;
            if (interval >= blocks.Length * GetComponent<GameManager>().simulationSpeed)
            {
                yield return null;
                interval = 0;
            }
        }
        busy = false;
    }
}