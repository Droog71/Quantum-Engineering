using UnityEngine;

public class AirLock : Machine
{
    private StateManager stateManager;
    public string ID = "unassigned";
    public bool open;
    public GameObject openObject;
    public GameObject closedObject;
    public GameObject effects;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        if (QualitySettings.GetQualityLevel() < 3)
        {
            effects.SetActive(false);
        }
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        GetComponent<PhysicsHandler>().UpdatePhysics();
    }

    //! Toggle the open or closed state of the hatchway.
    public void ToggleOpen()
    {
        if (open == false)
        {
            openObject.SetActive(true);
            closedObject.SetActive(false);
            GetComponent<Collider>().isTrigger = true;
            open = true;
        }
        else
        {
            openObject.SetActive(false);
            closedObject.SetActive(true);
            GetComponent<Collider>().isTrigger = false;
            open = false;
        }
    }
}