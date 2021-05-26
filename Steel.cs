using UnityEngine;

//! This class is attached to all steel block prefabs.
public class Steel : Block
{
    public string ID = "unassigned";
    private StateManager stateManager;
    public string creationMethod;
    public int address;

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
