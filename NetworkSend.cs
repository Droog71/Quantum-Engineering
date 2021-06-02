using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Net;
using System.Linq;
using System.Diagnostics;

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
            { "b", playerBlue },
            { "ip", PlayerPrefs.GetString("ip") }
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
                UnityEngine.Debug.Log(e.Message);
                SceneManager.LoadScene(0);
            }
            client.UploadStringAsync(uri, "POST", "@" + values["name"] + ":"
            + values["x"] + "," + values["y"] + "," + values["z"]
            + "," + values["fx"] + "," + values["fz"] + ","
            + values["r"] + "," + values["g"] + "," + values["b"] + "," + values["ip"]);
        }
    }

    //! Sends conduit range to the server when changed by the player.
    public IEnumerator SendConduitData(Vector3 pos, int range)
    {
        if (conduitCoroutineBusy == false)
        {
            conduitCoroutineBusy = true;
            yield return new WaitForSeconds(0.5f);
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
            yield return new WaitForSeconds(0.5f);
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
            yield return new WaitForSeconds(0.5f);
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
            yield return new WaitForSeconds(0.5f);
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
        using (WebClient client = new WebClient())
        {
            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/items");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + amount + ":" + position);
        }
    }

    //! Adds server to master server database.
    public IEnumerator Announce()
    {
        yield return new WaitForSeconds(30);
        using (WebClient client = new WebClient()) 
        {
            Uri uri = new Uri("http://45.77.158.179:48000/servers");
            string ip = PlayerPrefs.GetString("ip");
            string creative = FileBasedPrefs.GetBool(ip + "creativeMode").ToString();
            NetworkPlayer[] allPlayers = UnityEngine.Object.FindObjectsOfType<NetworkPlayer>();
            int playerCount = allPlayers.Length;
            string[] commandLineOptions = Environment.GetCommandLineArgs();
            if (!commandLineOptions.Contains("-batchmode"))
            {
                playerCount += 1;
            }
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string[] scenes = { "Kepler-1625", "Gliese 876", "Mods Menu", "Kepler-452b" };
            string worldName = scenes[sceneIndex];
            client.UploadStringAsync(uri, "POST", "@" + PlayerPrefs.GetString("ip") + ":" + worldName + "," + creative + "," + playerCount);
            UnityEngine.Debug.Log("Announce: updating master server >> address: " + PlayerPrefs.GetString("ip") + " scene: " + worldName + " creative: " + creative + " players: " + playerCount);
        }
        networkController.announceCoroutineBusy = false;
    }
}