using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class NetworkSend
{
    private NetworkController networkController;
    private PlayerController playerController;
    private bool conduitCoroutineBusy;
    private bool machineCoroutineBusy;
    private bool hubCoroutineBusy;
    public bool sentNetworkStorage;
    private string modNames;
    private string playerRed;
    private string playerGreen;
    private string playerBlue;

    //! Network functions for multiplayer games.
    public NetworkSend(NetworkController networkController)
    {
        this.networkController = networkController;
        playerController = networkController.playerController;
        playerRed = PlayerPrefs.GetFloat("playerRed").ToString();
        playerGreen = PlayerPrefs.GetFloat("playerGreen").ToString();
        playerBlue = PlayerPrefs.GetFloat("playerBlue").ToString();
    }

    //! Sends a chat message to the server.
    public void SendChatMessage(string message)
    {
        using(WebClient client = new WebClient()) 
        {
            Uri uri = new Uri(networkController.serverURL+"/chat");
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
            { "ip", PlayerPrefs.GetString("ip") },
            { "password", PlayerPrefs.GetString("password") }
        };

        using(WebClient client = new WebClient()) 
        {
            Uri uri = null;
            try
            {
                uri = new Uri(networkController.serverURL+"/players");
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                SceneManager.LoadScene(0);
            }
            client.UploadStringAsync(uri, "POST", "@" + values["name"] + ":"
            + values["x"] + "," + values["y"] + "," + values["z"]
            + "," + values["fx"] + "," + values["fz"] + ","
            + values["r"] + "," + values["g"] + "," + values["b"]
            + "," + values["ip"] + "," + values["password"]);
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
                Uri uri = new Uri(networkController.serverURL+"/conduits");
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
                Uri uri = new Uri(networkController.serverURL+"/machines");
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
                Uri uri = new Uri(networkController.serverURL+"/hubs");
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
                            Uri uri = new Uri(networkController.serverURL+"/storage");
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
                Uri uri = new Uri(networkController.serverURL+"/power");
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
            Uri uri = new Uri(networkController.serverURL + "/items");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + amount + ":" + position);
        }
    }

    //! Sends hazard toggle to the server. Can only be called by the host.
    public void SendHazardData(bool hazardsEnabled)
    {
        using(WebClient client = new WebClient()) 
        {
            Uri uri = new Uri(networkController.serverURL+"/hazards");
            client.UploadStringAsync(uri, "POST", "@" + hazardsEnabled);
        }
    }

    //! Adds server to master server database.
    public IEnumerator Announce()
    {
        if (modNames == null)
        {
            modNames = "Mods > ";

            try
            {
                int[] modIds = ModIO.ModManager.GetEnabledModIds().ToArray();
                for (int i = 0; i < modIds.Length; i++)
                {
                    using (WebClient client = new WebClient())
                    {    
                        Uri uri = new Uri("https://api.mod.io/v1/games/943/mods/" + modIds[i] + "?api_key=" + "8832bfdcf22c63648e4a2f96284159ed");
                        string result = client.DownloadString(uri);
                        string modName = result.Split(':')[37].Split(',')[0].Split('"')[1].Split('"')[0];
                        string separator = modIds.Length > 1 && i < modIds.Length - 1 ? " & " : "";
                        modNames += modName + separator;
                    } 
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            if (modNames == "Mods > ")
            {
                try
                {
                    string modPath = Path.Combine(Application.persistentDataPath, "Mods");
                    Directory.CreateDirectory(modPath);
                    string[] modDirs = Directory.GetDirectories(modPath);
                    for (int i = 0; i < modDirs.Length; i++)
                    {
                        string[] path = modDirs[i].Split('/');
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            path = modDirs[i].Split('\\');
                        }
                        string mod = path[path.Length - 1];
                        string separator = modDirs.Length > 1 && i < modDirs.Length - 1 ? " & " : "";
                        modNames += mod + separator;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }

            if (modNames == "Mods > ")
            {
                modNames = "Mods > None";
            }
        }

        yield return new WaitForSeconds(30);

        using (WebClient client = new WebClient()) 
        {

            string ip = PlayerPrefs.GetString("ip");
            string creative = FileBasedPrefs.GetBool(ip + "creativeMode").ToString();

            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string[] scenes = { "Kepler-1625", "Gliese 876", "Mods Menu", "Kepler-452b" };
            string worldName = scenes[sceneIndex];

            NetworkPlayer[] allPlayers = UnityEngine.Object.FindObjectsOfType<NetworkPlayer>();
            int playerCount = allPlayers.Length;
            string[] commandLineOptions = Environment.GetCommandLineArgs();
            if (!commandLineOptions.Contains("-batchmode"))
            {
                playerCount += 1;
            }

            Uri uri = new Uri("http://45.77.158.179:48000/servers");
            client.UploadStringAsync(uri, "POST", "@" + PlayerPrefs.GetString("ip") + ":" + worldName + "," + creative + "," + modNames + "," + playerCount);

            Debug.Log("Announce >> address: " + 
            PlayerPrefs.GetString("ip") + ", scene: " + worldName + ", creative: " +
            creative + ", " + modNames + ", players: " + playerCount);
        }
        networkController.announceCoroutineBusy = false;
    }
}