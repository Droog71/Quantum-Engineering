using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    public int address;
    private float updateTick;

    void Start()
    {

    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
        }
    }
}
