using UnityEngine;

public class RailCart : Machine
{
    public string ID = "unassigned";
    public string creationMethod = "built";
    public GameObject target;
    private Vector3 targetPosition;
    private StateManager stateManager;
    public int address;
    public string targetID;
    private bool loadedTarget;
    private float stopTimer;
    private GameObject builtObjects;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        builtObjects = GameObject.Find("BuiltObjects");
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (!stateManager.Busy())
        {
            GetComponent<InventoryManager>().ID = ID;
            if (creationMethod.Equals("spawned"))
            {
                if (target == null && loadedTarget == false && !targetID.Equals(""))
                {
                    RailCartHub[] allHubs = FindObjectsOfType<RailCartHub>();
                    foreach (RailCartHub hub in allHubs)
                    {
                        if (hub.ID.Equals(targetID))
                        {
                            target = hub.gameObject;
                            loadedTarget = true;
                        }
                    }
                }
            }
            if (target != null)
            {
                targetPosition = target.transform.position;
                transform.LookAt(targetPosition);
                if (Vector3.Distance(transform.position, targetPosition) < 1)
                {
                    if (target.GetComponent<RailCartHub>() != null)
                    {
                        targetID = target.GetComponent<RailCartHub>().ID;
                        if (target.GetComponent<RailCartHub>().stop == true)
                        {
                            if (GetComponent<AudioSource>().enabled == true)
                            {
                                GetComponent<AudioSource>().enabled = false;
                            }
                            if (stopTimer <= target.GetComponent<RailCartHub>().stopTime)
                            {
                                stopTimer += 1 * Time.deltaTime;
                            }
                            else if (target.GetComponent<RailCartHub>().outputObject != null)
                            {
                                stopTimer = 0;
                                target = target.GetComponent<RailCartHub>().outputObject;
                            }
                        }
                        else if (target.GetComponent<RailCartHub>().outputObject != null)
                        {
                            stopTimer = 0;
                            target = target.GetComponent<RailCartHub>().outputObject;
                        }
                    }
                }
                else
                {
                    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit crashHit, 5))
                    {
                        if (crashHit.collider != null)
                        {
                            if (crashHit.collider.gameObject != null)
                            {
                                if (crashHit.collider.gameObject.GetComponent<RailCartHub>() != null || crashHit.collider.gameObject.tag.Equals("Landscape"))
                                {
                                    transform.position += 8 * transform.forward * Time.deltaTime;
                                }
                            }
                        }
                    }
                    else
                    {
                        transform.position += 8 * transform.forward * Time.deltaTime;
                    }
                    if (GetComponent<AudioSource>().enabled == false)
                    {
                        GetComponent<AudioSource>().enabled = true;
                    }
                }
            }
        }
    }
}