using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Net;

public class NetworkSend
{
    private NetworkController networkController;
    private PlayerController playerController;
    private string serverURL;
    private bool conduitCoroutineBusy;
    private bool machineCoroutineBusy;
    private bool hubCoroutineBusy;
    public bool sentNetworkStorage;
    private string playerRed;
    private string playerGreen;
    private string playerBlue;

    //! Network functions for multiplayer games.
    public NetworkSend(NetworkController networkController)
    {
        this.networkController = networkController;
        playerController = networkController.playerController;
        serverURL = networkController.serverURL;
        playerRed = PlayerPrefs.GetFloat("playerRed").ToString();
        playerGreen = PlayerPrefs.GetFloat("playerGreen").ToString();
        playerBlue = PlayerPrefs.GetFloat("playerBlue").ToString();
    }

    //! Sends a chat message to the server.
    public void SendChatMessage(string message)
    {
        using(WebClient client = new WebClient()) 
        {
            Uri uri = new Uri(serverURL+"/chat");
            client.UploadStringAsync(uri, "POST", "@" + PlayerPrefs.GetString("UserName") + ":" + message);
        }
    }

    //! Gets external IP address for online games.
    private string GetExternalAddress()
    {
        using (WebClient client = new WebClient())
        {    
            Uri uri = new Uri("https://api.ipify.org");
            return client.DownloadString(uri);
        }
    }

    //! Sends player name, location and color to the server.
    public void SendPlayerInfo()
    {
        Dictionary<string, string> values = new Dictionary<string, string>
        {
            { "name", PlayerPrefs.GetString("UserName") },
            { "x", playerController.gameObject.transform.position.x.ToString() },
            { "y", playerController.gameObject.transform.position.y.ToString() },
            { "z", playerController.gameObject.transform.position.z.ToString() },
            { "fx", Camera.main.transform.forward.x.ToString() },
            { "fz", Camera.main.transform.forward.z.ToString() },
            { "r", playerRed },
            { "g", playerGreen },
            { "b", playerBlue }
        };

        using(WebClient client = new WebClient()) 
        {
            Uri uri = null;
            try
            {
                uri = new Uri(serverURL+"/players");
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                SceneManager.LoadScene(0);
            }
            client.UploadStringAsync(uri, "POST", "@" + values["name"] + ":"
            + values["x"] + "," + values["y"] + "," + values["z"]
            + "," + values["fx"] + "," + values["fz"] + ","
            + values["r"] + "," + values["g"] + "," + values["b"]);
        }
    }

    //! Sends conduit range to the server when changed by the player.
    public IEnumerator SendConduitData(Vector3 pos, int range)
    {
        if (conduitCoroutineBusy == false)
        {
            conduitCoroutineBusy = true;
            yield return new WaitForSeconds(1);
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z); 
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(serverURL+"/conduits");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + range);
            }
            conduitCoroutineBusy = false;
        }
    }

    //! Sends machine speed to the server when changed by the player.
    public IEnumerator SendMachineData(Vector3 pos, int speed)
    {
        if (machineCoroutineBusy == false)
        {
            machineCoroutineBusy = true;
            yield return new WaitForSeconds(1);
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z); 
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(serverURL+"/machines");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + speed);
            }
            machineCoroutineBusy = false;
        }
    }

    //! Sends rail cart hub data to the server when changed by the player.
    public IEnumerator SendHubData(Vector3 pos, int circuit, int range, bool stop, float stopTime)
    {
        if (hubCoroutineBusy == false)
        {
            hubCoroutineBusy = true;
            yield return new WaitForSeconds(1);
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z);
            int isStop = Convert.ToInt32(stop);
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(serverURL+"/hubs");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + circuit + "," + range + "," + isStop + "," + stopTime);
            }
            hubCoroutineBusy = false;
        }
    }

    //! Sends inventory data to the server.
    public IEnumerator SendNetworkStorage()
    {
        InventoryManager[] allInventories = UnityEngine.Object.FindObjectsOfType<InventoryManager>();
        foreach (InventoryManager manager in allInventories)
        {
            if (manager != null)
            {
                if (manager.ID != "player")
                {
                    InventorySlot[] inventory = manager.inventory;
                    for (int i = 0; i < inventory.Length; i++)
                    {
                        using(WebClient client = new WebClient()) 
                        {
                            Uri uri = new Uri(serverURL+"/storage");
                            Vector3 pos = manager.gameObject.transform.position;
                            float x = Mathf.Round(pos.x);
                            float y = Mathf.Round(pos.y); 
                            float z = Mathf.Round(pos.z); 
                            client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":"+i+";"+inventory[i].typeInSlot+"="+inventory[i].amountInSlot);
                        }
                        yield return null;
                    }
                }
            }
        }
        sentNetworkStorage = true;
    }

    //! Sends power conduit data to the server.
    public IEnumerator SendPowerData(Vector3 pos, int range, bool dual)
    {
        if (conduitCoroutineBusy == false)
        {
            conduitCoroutineBusy = true;
            yield return new WaitForSeconds(1);
            float x = Mathf.Round(pos.x);
            float y = Mathf.Round(pos.y); 
            float z = Mathf.Round(pos.z); 
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(serverURL+"/power");
                client.UploadStringAsync(uri, "POST", "@" + x + "," + y + "," + z + ":" + range + "," + dual);
            }
            conduitCoroutineBusy = false;
        }
    }

    //! Sends instantiated item info to the server in multiplayer games.
    public void SendItemData(int destroy, string type, int amount, Vector3 pos)
    {
        using(WebClient client = new WebClient())
        {
            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/items");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + amount + ":" + position);
        }
    }
}