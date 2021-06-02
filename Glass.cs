using UnityEngine;

//! This class is attached to all glass block prefabs.
public class Glass : Block
{
    public string ID = "unassigned";
    public string creationMethod;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called by BlockManager update coroutine.
    public override void UpdateBlock()
    {
        if (ID == "unassigned" || stateManager.Busy())
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
    }
}
