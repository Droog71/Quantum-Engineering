using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;

//! This class controls machine update functions via coroutine .
//! One machine is updated per frame.
public class ItemManager : MonoBehaviour
{
    private bool busy;
    private bool paused;
    public List<ConduitItem> items;

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (items == null)
        {
            items = new List<ConduitItem>();
        }

        if (busy == false && paused == false && GetComponent<StateManager>().worldLoaded == true)
        {
            Timing.RunCoroutine(ItemUpdateCoroutine(), "item");
        }
    }

    public void AddItem(ConduitItem item)
    {
        paused = true;
        busy = false;

        Timing.KillCoroutines("item");
        items.Add(item);
        paused = false;
    }

    //! Calls the UpdateMachine function on each machine in the world, yielding after each call.
    private IEnumerator<float> ItemUpdateCoroutine()
    {
        busy = true;
        int interval = 0;
        foreach (ConduitItem item in items)
        {
            if (item != null)
            {
                try
                {
                    item.UpdateItem();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                interval++;
                if (interval >= items.Count * GetComponent<GameManager>().simulationSpeed * 5)
                {
                    yield return Timing.WaitForOneFrame;
                    interval = 0;
                }
            }
        }
        busy = false;
    }
}