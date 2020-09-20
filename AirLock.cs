using UnityEngine;

public class AirLock : MonoBehaviour
{
    private float updateTick;
    private StateManager stateManager;
    public string ID = "unassigned";
    public int address;
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

    //! Called once per frame by unity engine.
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            if (stateManager.Busy())
            {
                 updateTick = 0;
                return;
            }

            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
        }
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