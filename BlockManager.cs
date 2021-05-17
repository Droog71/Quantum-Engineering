using System.Collections;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private bool busy;
    private Coroutine blockUpdateCoroutine;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (busy == false && GetComponent<StateManager>().worldLoaded == true)
        {
            blockUpdateCoroutine = StartCoroutine(BlockUpdateCoroutine());
        }
    }

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