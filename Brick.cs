﻿using UnityEngine;

//! This class is attached to all brick block prefabs.
public class Brick : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    public int address;
    private float updateTick;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (ID == "unassigned")
            return;

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
}
