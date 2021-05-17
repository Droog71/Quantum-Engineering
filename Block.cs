using UnityEngine;

public class Block : MonoBehaviour
{
    //! Called by BlockManager update coroutine.
    //! Overriden in various derived classes.
    public virtual void UpdateBlock()
    {

    }
}