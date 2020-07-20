using UnityEngine;
using System.Collections;

public class RailCart : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    public GameObject target;
    private Vector3 targetPosition;
    public int address;
    public string targetID;
    private bool loadedTarget;
    private float stopTimer;
    private GameObject builtObjects;

    void Start()
    {
        builtObjects = GameObject.Find("Built_Objects");
    }

    void OnDestroy()
    {

    }

    void Update()
    {
        GetComponent<InventoryManager>().ID = ID;
        if (creationMethod.Equals("spawned"))
        {
            if (target == null && loadedTarget == false && !targetID.Equals(""))
            {
                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                foreach (GameObject obj in allObjects)
                {
                    if (obj != null)
                    {
                        if (obj.transform.parent != builtObjects.transform)
                        {
                            if (obj.activeInHierarchy)
                            {
                                if (obj.GetComponent<RailCartHub>() != null)
                                {
                                    if (obj.GetComponent<RailCartHub>().ID.Equals(targetID))
                                    {
                                        target = obj;
                                        loadedTarget = true;
                                    }
                                }
                            }
                        }
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
                    //Debug.Log(ID + " target is a hub");
                    if (target.GetComponent<RailCartHub>().stop == true)
                    {
                        //Debug.Log(ID + " target is a stopping point");
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
                            //Debug.Log(ID + " finding next hub");
                            stopTimer = 0;
                            target = target.GetComponent<RailCartHub>().outputObject;
                        }
                    }
                    else if (target.GetComponent<RailCartHub>().outputObject != null)
                    {
                        //Debug.Log(ID + " finding next hub");
                        stopTimer = 0;
                        target = target.GetComponent<RailCartHub>().outputObject;
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position,transform.forward,out RaycastHit crashHit, 5))
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