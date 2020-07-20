using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLock : MonoBehaviour
{
    public string ID = "unassigned";
    public int address;
    private float updateTick;
    public bool open;
    public GameObject openObject;
    public GameObject closedObject;
    public GameObject effects;

    void Start()
    {
        if (QualitySettings.GetQualityLevel() < 3)
        {
            effects.SetActive(false);
        }
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Physics update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
        }
    }

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
