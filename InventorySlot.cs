using UnityEngine;
using System.Collections.Generic;
using MEC;

//! This object is used in arrays by the inventory manager to hold item names and amounts.
public class InventorySlot : MonoBehaviour
{
    public int amountInSlot;
    public string typeInSlot = "nothing";
    public int networkWaitTime;
    public bool pendingNetworkUpdate;
    private bool networkCoroutineBusy;

    //! Returns true if this inventory slot requires network updates in multiplayer games.
    private bool ShouldDoNetworkUpdate()
    {
        return gameObject.tag != "Player" &&
        PlayerPrefsX.GetPersistentBool("multiplayer") == true &&
        pendingNetworkUpdate == true &&
        networkCoroutineBusy == false;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (ShouldDoNetworkUpdate())
        { 
            Timing.RunCoroutine(WaitForServer());
        }
    }

    //! Delays overwriting of values by the server while the database is being updated.
    private IEnumerator<float> WaitForServer()
    {
        networkCoroutineBusy = true;
        if (networkWaitTime < 30)
        {
            networkWaitTime++;
            yield return Timing.WaitForSeconds(1);
        }
        else
        {
            pendingNetworkUpdate = false;
            networkWaitTime = 0;
        }
        networkCoroutineBusy = false;
    }
}

