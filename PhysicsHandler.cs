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
    public float lifetime;
    private float duplicateClearingTimer;
    private float missingBlockTimer;
    private float supportCheckTimer;
    private float worldLoadTimer;
    private bool worldLoadComplete;
    private StateManager stateManager;
    public string creationMethod = "built";
    public bool needsSupportCheck;

    void Start()
    {
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        rb.isKinematic = true;
        if (GetComponent<AirLock>() != null)
        {
            if (GetComponent<AirLock>().open == false)
            {
                GetComponent<Collider>().isTrigger = false;
            }
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }

    void Update()
    {

    }

    public void UpdatePhysics()
    {
        if (stateManager.worldLoaded == true)
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
                lifetime += 0.01f * Time.deltaTime;
                duplicateClearingTimer++;
                if (duplicateClearingTimer > 8 && duplicateClearingTimer < 10)
                {
                    if (Physics.Raycast(transform.position, transform.up, out RaycastHit playerHit, 5))
                    {
                        if (playerHit.collider.gameObject.GetComponent<PlayerController>() == null)
                        {
                            GetComponent<Collider>().isTrigger = true;
                        }
                        else
                        {
                            //Debug.Log("Block skipped duplicate check due to presence of player.");
                        }
                    }
                    else
                    {
                        GetComponent<Collider>().isTrigger = true;
                    }
                }
                else if (duplicateClearingTimer >= 10)
                {
                    if (GetComponent<AirLock>() != null)
                    {
                        if (GetComponent<AirLock>().open == false)
                        {
                            GetComponent<Collider>().isTrigger = false;
                        }
                    }
                    else
                    {
                        GetComponent<Collider>().isTrigger = false;
                    }
                    duplicateClearingTimer = 0;
                }
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
                            //Debug.Log("block found buried via raycast");
                        }
                    }
                    if (Physics.Raycast(transform.position, transform.up, out RaycastHit inGroundHit, 3))
                    {
                        if (inGroundHit.collider.tag.Equals("Landscape"))
                        {
                            buried = true;
                            needsSupportCheck = false;
                            //Debug.Log("block found buried via raycast");
                        }
                    }
                    Vector3 buriedVector = new Vector3(transform.position.x, transform.position.y + 2.4f, transform.position.z);
                    if (Physics.Raycast(buriedVector, -transform.up, out RaycastHit onGroundHit2, 6))
                    {
                        if (onGroundHit2.collider.tag.Equals("Landscape"))
                        {
                            buried = true;
                            needsSupportCheck = false;
                            //Debug.Log("block found buried via raycast");
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
                                        //GetComponent<Renderer>().material.color = Color.cyan;
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
                                GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all", false);
                                separatedBlocks = true;
                            }
                            falling = true;
                            rb.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePosition;
                            rb.isKinematic = false;
                            //GetComponent<Renderer>().material.color = Color.blue;
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

    void CheckForSupport()
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
                //Debug.Log("dir 1");
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
                        //GameObject lineObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //lineObject.transform.position = missingBlockVector;
                        //lineObject.GetComponent<Renderer>().enabled = false;
                        //lineObject.GetComponent<Collider>().enabled = false;
                        //LineRenderer debugLine = lineObject.AddComponent<LineRenderer>();
                        //debugLine.startWidth = 0.2f;
                        //debugLine.endWidth = 0.2f;
                        //debugLine.loop = true;
                        //debugLine.enabled = false;
                        //debugLine.SetPosition(0, missingBlockVector);
                        //debugLine.SetPosition(1, missingBlockVector - transform.up * 3);
                        //debugLine.enabled = true;
                        if (!Physics.Raycast(missingBlockVector, -transform.up,3))
                        {
                            missingBlockZpositive = true;
                        }
                    }
                }
            }
            if (dir == 2)
            {
                //Debug.Log("dir 2");
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
                        //GameObject lineObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //lineObject.transform.position = missingBlockVector;
                        //lineObject.GetComponent<Renderer>().enabled = false;
                        //lineObject.GetComponent<Collider>().enabled = false;
                        //LineRenderer debugLine = lineObject.AddComponent<LineRenderer>();
                        //debugLine.startWidth = 0.2f;
                        //debugLine.endWidth = 0.2f;
                        //debugLine.loop = true;
                        //debugLine.enabled = false;
                        //debugLine.SetPosition(0, missingBlockVector);
                        //debugLine.SetPosition(1, missingBlockVector - transform.up * 3);
                        //debugLine.enabled = true;
                        if (!Physics.Raycast(missingBlockVector, -transform.up, 3))
                        {
                            missingBlockZnegative = true;
                        }
                    }
                }
            }
            if (dir == 3)
            {
                //Debug.Log("dir 3");
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
                        //GameObject lineObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //lineObject.transform.position = missingBlockVector;
                        //lineObject.GetComponent<Renderer>().enabled = false;
                        //lineObject.GetComponent<Collider>().enabled = false;
                        //LineRenderer debugLine = lineObject.AddComponent<LineRenderer>();
                        //debugLine.startWidth = 0.2f;
                        //debugLine.endWidth = 0.2f;
                        //debugLine.loop = true;
                        //debugLine.enabled = false;
                        //debugLine.SetPosition(0, missingBlockVector);
                        //debugLine.SetPosition(1, missingBlockVector - transform.up * 3);
                        //debugLine.enabled = true;
                        if (!Physics.Raycast(missingBlockVector, -transform.up, 3))
                        {
                            missingBlockXpositive = true;
                        }
                    }
                }
            }
            if (dir == 4)
            {
                //Debug.Log("dir 4");
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
                        //GameObject lineObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //lineObject.transform.position = missingBlockVector;
                        //lineObject.GetComponent<Renderer>().enabled = false;
                        //lineObject.GetComponent<Collider>().enabled = false;
                        //LineRenderer debugLine = //lineObject.AddComponent<LineRenderer>();
                        //debugLine.startWidth = 0.2f;
                        //debugLine.endWidth = 0.2f;
                        //debugLine.loop = true;
                        //debugLine.enabled = false;
                        //debugLine.SetPosition(0, missingBlockVector);
                        //debugLine.SetPosition(1, missingBlockVector - transform.up * 3);
                        //debugLine.enabled = true;
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
                    //GetComponent<Renderer>().material.color = Color.yellow;
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
                    //Debug.Log("Partial structure collapse occuring!");
                    //GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
            else if (missingBlockTimer > 0)
            {
                missingBlockTimer = 0;
                //Debug.Log("Structure collapse false alarm, timer reset.");
            }
        }
        if (supported == true)
        {
            //GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            if (GetComponent<Renderer>().material.color != Color.yellow)
            {
                //GetComponent<Renderer>().material.color = Color.red;
            }
            if (separatedBlocks == false)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().SeparateBlocks(transform.position, "all",false);
                separatedBlocks = true;
            }
            falling = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePosition;
            rb.isKinematic = false;
        }
        needsSupportCheck = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Landscape"))
        {
            buried = true;
            needsSupportCheck = false;
            //Debug.Log("block found buried via trigger");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PhysicsHandler>() != null)
        {
            if (rb.constraints == (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition))
            {
                if (other.bounds.Contains(transform.position))
                {
                    if (lifetime < other.gameObject.GetComponent<PhysicsHandler>().lifetime)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

