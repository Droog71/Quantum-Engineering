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
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            block.UpdateBlock();
            yield return null;
        }
        busy = false;
    }
}