using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dropPosition;
    public bool falling;
    public GameObject explosion;
    public string type;
    private float fallDistance;
    public bool fallingStack;
    private bool separatedBlocks;
    public bool buried;
    private bool supported;
    private bool missingBlockZpositive;
    private bool missingBlockZnegative;
    private bool missingBlockXpositive;
    private bool missingBlockXnegative;
    private int supportedZpositive;
    private int supportedZnegative;
    private int supportedXpositive;
    private int supportedXnegative;
    private int supportCount;
    private float missingBlockTimer;
    private float supportCheckTimer;
    private float worldLoadTimer;
    private bool worldLoadComplete;
    private GameManager gameManager;
    private StateManager stateManager;
    public string creationMethod = "built";
    public bool needsSupportCheck;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stateManager = gameManager.GetComponent<StateManager>();
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        rb.isKinematic = true;
    }

    //! Updates the physics state of the object.
    public void UpdatePhysics()
    {
        if (!stateManager.Busy())
        {
            if (!creationMethod.Equals("spawned"))
            {
                worldLoadComplete = true;
            }
            if (worldLoadComplete == false)
            {
                worldLoadTimer++;
                if (worldLoadTimer >= 30)
                {
                    worldLoadComplete = true;
                    worldLoadTimer = 0;
                }
            }
            else
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics == true)
                {
                    if (transform.position.y > 500 || transform.position.y < -5000)
                    {
                        Destroy(gameObject);
                    }
                    if (Physics.Raycast(transform.position, -transform.up, out RaycastHit onGroundHit, 3))
                    {
                        if (onGroundHit.collider.tag.Equals("Landscape"))
                        {
                            buried = true;
                            needsSupportCheck = false;
                        }
                    }
                    if (Physics.Raycast(transform.position, transform.up, out RaycastHit inGroundHit, 3))
                    {
                        if (inGroundHit.collider.tag.Equals("Landscape"))
                        {
                            buried = true;
                            needsSupportCheck = false;
                        }
                    }
                    Vector3 buriedVector = new Vector3(transform.position.x, transform.position.y + 2.4f, transform.position.z);
                    if (Physics.Raycast(buriedVector, -transform.up, out RaycastHit onGroundHit2, 6))
                    {
                        if (onGroundHit2.collider.tag.Equals("Landscape"))
                        {
                            buried = true;
                            needsSupportCheck = false;
                        }
                    }
                    if (Physics.Raycast(transform.position, -transform.up, out RaycastHit stackHit, 5))
                    {
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit fallingStackhit, Mathf.Infinity))
                        {
                            if (fallingStackhit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (fallingStackhit.collider.gameObject.GetComponent<PhysicsHandler>().falling == true)
                                {
                                    if (supported == false)
                                    {
                                        fallingStack = true;
                                    }
                                }
                            }
                        }

                    }
                    if (fallingStack == true || buried == false && !Physics.Raycast(transform.position, -transform.up, out RaycastHit downHit, 3))
                    {
                        if (fallingStack == true || !Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, 3) && !Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, 3) && !Physics.Raycast(transform.position, transform.forward, out RaycastHit frontHit, 3) && !Physics.Raycast(transform.position, -transform.forward, out RaycastHit backHit, 3))
                        {
                            if (separatedBlocks == false)
                            {
                                gameManager.meshManager.SeparateBlocks(transform.position, "all", false);
                                separatedBlocks = true;
                            }
                            falling = true;
                            rb.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePosition;
                            rb.isKinematic = false;
                        }
                        else
                        {
                            needsSupportCheck = true;
                            supportCheckTimer++;
                            if (supportCheckTimer >= 10)
                            {
                                CheckForSupport();
                            }
                        }
                    }
                    if (falling == true)
                    {
                        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit landHit, 3))
                        {
                            if (type == "machine")
                            {
                                if (fallDistance >= 3)
                                {
                                    Explode();
                                }
                            }
                            if (landHit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                            {
                                if (landHit.collider.gameObject.GetComponent<PhysicsHandler>().falling == false && landHit.collider.gameObject.GetComponent<PhysicsHandler>().fallingStack == false)
                                {
                                    fallDistance = 0;
                                    falling = false;
                                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                                    rb.isKinematic = true;
                                    separatedBlocks = false;
                                }
                            }
                            else
                            {
                                fallDistance = 0;
                                falling = false;
                                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                                rb.isKinematic = true;
                                separatedBlocks = false;
                            }
                        }
                        else
                        {
                            fallDistance++;
                        }
                    }
                }
                else
                {
                    falling = false;
                    fallingStack = false;
                    needsSupportCheck = false;
                }
            }
        }
    }

    //! Checks whether or not a block is properly supported or should fall to the ground.
    private void CheckForSupport()
    {
        supported = false;
        supportedZpositive = 0;
        supportedZnegative = 0;
        supportedXpositive = 0;
        supportedXnegative = 0;
        missingBlockZpositive = false;
        missingBlockZnegative = false;
        missingBlockXpositive = false;
        missingBlockXnegative = false;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 200.0F);
        for (int dir = 0; dir < 5; dir++)
        {
            if (dir == 1)
            {
                float supportZ = 0;
                hits = Physics.RaycastAll(transform.position, transform.forward, 200.0F);
                foreach (RaycastHit supportHit in hits)
                {
                    if (supportHit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        if (Physics.Raycast(supportHit.point, -transform.up, 3))
                        {
                            supported = true;
                            supportZ = supportHit.point.z;
                            supportedZpositive = 1;
                        }
                    }
                    if (supportHit.collider.gameObject.tag.Equals("CombinedMesh"))
                    {
                        supported = true;
                        supportZ = supportHit.point.z;
                        supportedZpositive = 1;
                    }
                }
                if (supportedZpositive > 0)
                {
                    for (float z = transform.position.z; z < supportZ; z+=5)
                    {
                        Vector3 missingBlockVector = new Vector3(transform.position.x, transform.position.y + 5, z);
                        if (!Physics.Raycast(missingBlockVector, -transform.up,3))
                        {
                            missingBlockZpositive = true;
                        }
                    }
                }
            }
            if (dir == 2)
            {
                float supportZ = 0;
                hits = Physics.RaycastAll(transform.position, -transform.forward, 200.0F);
                foreach (RaycastHit supportHit in hits)
                {
                    if (supportHit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        if (Physics.Raycast(supportHit.point, -transform.up, 3))
                        {
                            supported = true;
                            supportZ = supportHit.point.z;
                            supportedZnegative = 1;
                        }
                    }
                    if (supportHit.collider.gameObject.tag.Equals("CombinedMesh"))
                    {
                        supported = true;
                        supportZ = supportHit.point.z;
                        supportedZnegative = 1;
                    }
                }
                if (supportedZnegative > 0)
                {
                    for (float z = transform.position.z; z > supportZ; z-=5)
                    {
                        Vector3 missingBlockVector = new Vector3(transform.position.x, transform.position.y + 5, z);
                        if (!Physics.Raycast(missingBlockVector, -transform.up, 3))
                        {
                            missingBlockZnegative = true;
                        }
                    }
                }
            }
            if (dir == 3)
            {
                float supportX = 0;
                hits = Physics.RaycastAll(transform.position, transform.right, 200.0F);
                foreach (RaycastHit supportHit in hits)
                {
                    if (supportHit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        if (Physics.Raycast(supportHit.point, -transform.up, 3))
                        {
                            supported = true;
                            supportX = supportHit.point.x;
                            supportedXpositive = 1;
                        }
                    }
                    if (supportHit.collider.gameObject.tag.Equals("CombinedMesh"))
                    {
                        supported = true;
                        supportX = supportHit.point.x;
                        supportedXpositive = 1;
                    }
                }
                if (supportedXpositive > 0)
                {
                    for (float x = transform.position.x; x < supportX; x+=5)
                    {
                        Vector3 missingBlockVector = new Vector3(x, transform.position.y + 5, transform.position.z);
                        if (!Physics.Raycast(missingBlockVector, -transform.up, 3))
                        {
                            missingBlockXpositive = true;
                        }
                    }
                }
            }
            if (dir == 4)
            {
                float supportX = 0;
                hits = Physics.RaycastAll(transform.position, -transform.right, 200.0F);
                foreach (RaycastHit supportHit in hits)
                {
                    if (supportHit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        if (Physics.Raycast(supportHit.point, -transform.up, 3))
                        {
                            supported = true;
                            supportX = supportHit.point.x;
                            supportedXnegative = 1;
                        }
                    }
                    if (supportHit.collider.gameObject.tag.Equals("CombinedMesh"))
                    {
                        supported = true;
                        supportX = supportHit.point.x;
                        supportedXnegative = 1;
                    }
                }
                if (supportedXnegative > 0)
                {
                    for (float x = transform.position.x; x > supportX; x-=5)
                    {
                        Vector3 missingBlockVector = new Vector3(x, transform.position.y + 5, transform.position.z);
                        if (!Physics.Raycast(missingBlockVector, -transform.up, 3))
                        {
                            missingBlockXnegative = true;
                        }
                    }
                }
            }
        }
        supportCount = supportedZpositive + supportedZnegative + supportedXpositive + supportedXnegative;
        if (supportCount < 2)
        {
            if (missingBlockZpositive == true || missingBlockZnegative == true || missingBlockXpositive == true || missingBlockXnegative == true)
            {
                missingBlockTimer++;
                if (missingBlockTimer >= 10)
                {
                    supported = false;
                    missingBlockTimer = 0;
                }
            }
            else
            {
                missingBlockTimer = 0;
            }
        }
        else
        {
            if (missingBlockZpositive == true && missingBlockZnegative == true || missingBlockXpositive == true && missingBlockXnegative == true)
            {
                missingBlockTimer++;
                if (missingBlockTimer >= 10)
                {
                    supported = false;
                    missingBlockTimer = 0;
                }
            }
            else if (missingBlockTimer > 0)
            {
                missingBlockTimer = 0;
            }
        }
        if (supported == false)
        {
            if (separatedBlocks == false)
            {
                gameManager.meshManager.SeparateBlocks(transform.position, "all", false);
                separatedBlocks = true;
            }
            falling = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePosition;
            rb.isKinematic = false;
        }
        needsSupportCheck = false;
    }

    //! Returns true if the block is not at risk of falling.
    public bool IsSupported()
    {
        return falling == false && fallingStack == false && needsSupportCheck == false;
    }

    //! Destroyes the object and spawns explosion effects.
    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}