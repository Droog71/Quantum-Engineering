using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NetworkController
{
    public PlayerController playerController;
    public NetworkSend networkSend;
    public NetworkReceive networkReceive;
    private Dictionary<string, GameObject> networkPlayers;
    private Dictionary<string, Vector3> playerPositions;
    private TextureDictionary textureDictionary;
    private Coroutine sendNetworkPlayerCoroutine;
    private Coroutine dedicatedServerCoroutine;
    private Coroutine blockUpdateCoroutine;
    private Coroutine storageReceiveCoroutine;
    private Coroutine networkPowerCoroutine;
    private Coroutine networkBlockCoroutine;
    private Coroutine networkStorageCoroutine;
    private Coroutine networkConduitCoroutine;
    private Coroutine networkMachineCoroutine;
    private Coroutine getNetworkPlayersCoroutine;
    private Coroutine networkMovementCoroutine;
    private List<string> playerNames;
    public bool dedicatedServerCoroutineBusy;
    public bool networkStorageCoroutineBusy;
    public bool sendNetworkPlayerCoroutineBusy;
    public bool getNetworkPlayersCoroutineBusy;
    public bool playerMovementCoroutineBusy;
    public bool networkBlockCoroutineBusy;
    public bool receivedNetworkStorage;
    public bool networkWorldUpdateCoroutineBusy;
    public string playerData;
    public string blockData;
    public string storageData;
    public string serverURL;

    //! Network functions for multiplayer games.
    public NetworkController(PlayerController playerController)
    {
        this.playerController = playerController;
        serverURL = PlayerPrefs.GetString("serverURL");
        Debug.Log("Connected to " + serverURL);
        textureDictionary = playerController.gameManager.GetComponent<TextureDictionary>();
        networkPlayers = new Dictionary<string, GameObject>();
        playerPositions = new Dictionary<string, Vector3>();
        playerNames = new List<string>();
        networkSend = new NetworkSend(this);
        networkReceive = new NetworkReceive(this);
    }

    //! Network functions called once per frame.
    public void NetworkFrame()
    {
        string commandLineOptions = Environment.CommandLine;
        if (commandLineOptions.Contains("-batchmode"))
        {
            if (dedicatedServerCoroutineBusy == false)
            {
                dedicatedServerCoroutine = playerController.StartCoroutine(DedicatedServerCoroutine());
            }
        }

        if (sendNetworkPlayerCoroutineBusy == false)
        {
            sendNetworkPlayerCoroutine = playerController.StartCoroutine(SendNetworkPlayerInfo());
        }

        if (getNetworkPlayersCoroutineBusy == false)
        {
            getNetworkPlayersCoroutine = playerController.StartCoroutine(GetNetworkPlayers());
        }

        if (playerMovementCoroutineBusy == false)
        {
            networkMovementCoroutine = playerController.StartCoroutine(MoveNetworkPlayers());
        }
    }

    //! Send information about this player once per second.
    public IEnumerator SendNetworkPlayerInfo()
    {
        sendNetworkPlayerCoroutineBusy = true;
        networkSend.SendPlayerInfo();
        yield return new WaitForSeconds(1.0f);
        sendNetworkPlayerCoroutineBusy = false;
    }

    //! Updates the world over the network.
    public IEnumerator NetWorkWorldUpdate()
    {
        networkWorldUpdateCoroutineBusy = true;

        if (NetworkAvailable())
        {
            networkBlockCoroutine = playerController.StartCoroutine(UpdateNetworkBlocks());
        }
        else
        {
            yield return null;
        }

        if (NetworkAvailable())
        {
            networkConduitCoroutine = playerController.StartCoroutine(networkReceive.ReceiveConduitData());
        }
        else
        {
            yield return null;
        }

        if (NetworkAvailable())
        {
            networkPowerCoroutine = playerController.StartCoroutine(networkReceive.ReceivePowerData());
        }
        else
        {
            yield return null;
        }

        if (NetworkAvailable())
        {
            networkStorageCoroutine = playerController.StartCoroutine(UpdateNetworkStorage());
        }
        else
        {
            yield return null;
        }

        if (NetworkAvailable())
        {
            networkMachineCoroutine = playerController.StartCoroutine(networkReceive.ReceiveMachineData());
        }
        else
        {
            yield return null;
        }

        networkWorldUpdateCoroutineBusy = false;
    }

    //! Returns true if none of the network world update coroutines are running.
    private bool NetworkAvailable()
    {
        return networkBlockCoroutineBusy == false && 
        networkReceive.conduitDataCoroutineBusy == false && 
        networkReceive.powerDataCoroutineBusy == false && 
        networkStorageCoroutineBusy == false && 
        networkReceive.machineDataCoroutineBusy == false;
    }

    //! Saves the world for dedicated servers.
    public IEnumerator DedicatedServerCoroutine()
    {
        dedicatedServerCoroutineBusy = true;
        yield return new WaitForSeconds(900);
        playerController.requestedSave = true;
        dedicatedServerCoroutineBusy = false;
    }

    //! Gets information about other players from the server.
    public IEnumerator GetNetworkPlayers()
    {
        getNetworkPlayersCoroutineBusy = true;

        playerData = "none";
        networkReceive.GetPlayerData();
        while (playerData == "none")
        {
            yield return null;
        }

        playerNames.Clear();
        string[] playerList = playerData.Split('[');
        for (int i=2; i < playerList.Length; i++)
        {
            string playerInfo = playerList[i];
            string playerName = playerInfo.Split(':')[0].Split(',')[0].TrimStart('"').TrimEnd('"');
            float red = float.Parse(playerInfo.Split(',')[6]);
            float green = float.Parse(playerInfo.Split(',')[7]);
            float blue = float.Parse(playerInfo.Split(',')[8].Split(']')[0]);
            Color playerColor = new Color(red, green, blue);
            if (playerName != PlayerPrefs.GetString("UserName") && playerName != playerController.stateManager.worldName)
            {
                if (!playerNames.Contains(playerName))
                {
                    playerNames.Add(playerName);
                }
                if (!networkPlayers.ContainsKey(playerName))
                {
                    CreateNetworkPlayer(playerName, playerColor);
                }
                else
                {
                    UpdateNetWorkPlayer(playerInfo, playerColor);
                }
            }
            yield return null;
        }

        Dictionary<string, GameObject> allPlayers = new Dictionary<string, GameObject>(networkPlayers);
        foreach (KeyValuePair<string,GameObject> entry in allPlayers)
        {
            if (!playerNames.Contains(entry.Key))
            {
                networkPlayers.Remove(entry.Key);
                UnityEngine.Object.Destroy(entry.Value);
            }
            yield return null;
        }

        getNetworkPlayersCoroutineBusy = false;
    }

    //! Sends player positions to server.
    private void UpdateNetWorkPlayer(string playerInfo, Color playerColor)
    {
        string playerName = playerInfo.Split(':')[0].Split(',')[0].TrimStart('"').TrimEnd('"');
        float x = float.Parse(playerInfo.Split(',')[1]);
        float y = float.Parse(playerInfo.Split(',')[2]);
        float z = float.Parse(playerInfo.Split(',')[3]);
        float fx = float.Parse(playerInfo.Split(',')[4]);
        float fz = float.Parse(playerInfo.Split(',')[5]);
        if (!playerPositions.ContainsKey(playerName))
        {
            playerPositions.Add(playerName, new Vector3(x, y, z));
        }
        else
        {
            playerPositions[playerName] = new Vector3(x, y, z);
        }
        if (networkPlayers[playerName] != null)
        {
            GameObject player = networkPlayers[playerName];
            player.transform.forward = new Vector3(fx,0,fz);
            Renderer renderer = player.GetComponentInChildren<Renderer>();
            if (renderer.material.color != playerColor)
            {
                SetPlayerColor(player, playerColor);
            }
        }
    }

    //! Moves network player game objects based on database values.
    public IEnumerator MoveNetworkPlayers()
    {
        playerMovementCoroutineBusy = true;
        Dictionary<string, GameObject> allPlayers = new Dictionary<string, GameObject>(networkPlayers);
        foreach (KeyValuePair<string,GameObject> player in allPlayers)
        {
            if (playerPositions.ContainsKey(player.Key) && player.Value != null)
            {
                if (Vector3.Distance(playerPositions[player.Key], player.Value.transform.position) > 1)
                {
                    Vector3 moveDir = (playerPositions[player.Key] - player.Value.transform.position ).normalized;
                    player.Value.transform.position += moveDir * 15 * Time.deltaTime;
                    player.Value.GetComponent<NetworkPlayer>().moving = true;
                }
                else
                {
                    player.Value.GetComponent<NetworkPlayer>().moving = false;
                }
            }
            yield return null;
        }
        playerMovementCoroutineBusy = false;
    }

    //! Instantiates blocks over the network.
    public IEnumerator UpdateNetworkBlocks()
    {
        if (networkBlockCoroutineBusy == false)
        {
            networkBlockCoroutineBusy = true;

            blockData = "none";
            networkReceive.GetBlockData();
            while (blockData == "none")
            {
                yield return null;
            }
            blockUpdateCoroutine = playerController.StartCoroutine(networkReceive.ReceiveNetworkBlocks());
        }
    }

    //! Updates inventories over the network.
    public IEnumerator UpdateNetworkStorage()
    {
        networkStorageCoroutineBusy = true;

        storageData = "none";
        networkReceive.GetStorageData();
        while (storageData == "none")
        {
            yield return null;
        }

        receivedNetworkStorage = false;
        storageReceiveCoroutine = playerController.StartCoroutine(networkReceive.ReceiveNetworkStorage());
        while (receivedNetworkStorage == false)
        {
            yield return null;
        }

        networkStorageCoroutineBusy = false;
    }

    //! Instantiates a gameobject representing another player over the network.
    private void CreateNetworkPlayer(string playerName, Color playerColor)
    {
        GameObject newPlayer = UnityEngine.Object.Instantiate(playerController.networkPlayer);
        newPlayer.name = playerName;
        SetPlayerColor(newPlayer, playerColor);
        networkPlayers.Add(playerName, newPlayer);
    }

    //! Sets the color of a player character.
    private void SetPlayerColor(GameObject player, Color playerColor)
    {
        Material playerMaterial = new Material(Shader.Find("Standard"));
        playerMaterial.SetTexture("_MainTex", textureDictionary.dictionary["Iron Block"]);
        playerMaterial.color = playerColor;
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] mats = renderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = playerMaterial;
            }
            renderer.materials = mats;
        }
    }
}