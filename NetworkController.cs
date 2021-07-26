using UnityEngine;
using System;
using MEC;
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
    private List<string> playerNames;
    public bool dedicatedServerCoroutineBusy;
    public bool networkStorageCoroutineBusy;
    public bool sendNetworkPlayerCoroutineBusy;
    public bool getNetworkPlayersCoroutineBusy;
    public bool playerMovementCoroutineBusy;
    public bool networkBlockCoroutineBusy;
    public bool networkItemCoroutineBusy;
    public bool receivedNetworkStorage;
    public bool networkWorldUpdateCoroutineBusy;
    public bool announceCoroutineBusy;
    public bool checkForBanCoroutineBusy;
    public string playerData;
    public string blockData;
    public string itemData;
    public string storageData;
    public string serverURL;

    //! Network functions for multiplayer games.
    public NetworkController(PlayerController playerController)
    {
        this.playerController = playerController;
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
        serverURL = PlayerPrefs.GetString("serverURL");

        string commandLineOptions = Environment.CommandLine;
        if (commandLineOptions.Contains("-batchmode"))
        {
            if (dedicatedServerCoroutineBusy == false)
            {
                Timing.RunCoroutine(DedicatedServerCoroutine());
            }
        }

        if (sendNetworkPlayerCoroutineBusy == false)
        {
            Timing.RunCoroutine(SendNetworkPlayerInfo());
        }

        if (getNetworkPlayersCoroutineBusy == false)
        {
            Timing.RunCoroutine(GetNetworkPlayers());
        }

        if (playerMovementCoroutineBusy == false)
        {
            Timing.RunCoroutine(MoveNetworkPlayers());
        }

        if (networkBlockCoroutineBusy == false)
        {
            networkBlockCoroutineBusy = true;
            Timing.RunCoroutine(UpdateNetworkBlocks());
        }

        if (networkItemCoroutineBusy == false)
        {
            networkItemCoroutineBusy = true;
            Timing.RunCoroutine(UpdateNetworkItems());
        }

        if (announceCoroutineBusy == false && PlayerPrefsX.GetPersistentBool("hosting") == true && PlayerPrefsX.GetPersistentBool("announce") == true)
        {
            announceCoroutineBusy = true;
            Timing.RunCoroutine(networkSend.Announce());
        }

        if (checkForBanCoroutineBusy == false && PlayerPrefsX.GetPersistentBool("hosting") == false)
        {
            checkForBanCoroutineBusy = true;
            Timing.RunCoroutine(networkReceive.CheckForBan());
        }
    }

    //! Send information about this player to the server.
    public IEnumerator<float> SendNetworkPlayerInfo()
    {
        sendNetworkPlayerCoroutineBusy = true;
        networkSend.SendPlayerInfo();
        yield return Timing.WaitForSeconds(0.5f);
        sendNetworkPlayerCoroutineBusy = false;
    }

    //! Updates the world over the network.
    public IEnumerator<float> NetWorkWorldUpdate()
    {
        networkWorldUpdateCoroutineBusy = true;

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(networkReceive.ReceiveConduitData());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(networkReceive.ReceivePowerData());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(UpdateNetworkStorage());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(networkReceive.ReceiveHubData());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(networkReceive.ReceiveMachineData());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        if (NetworkAvailable())
        {
            Timing.RunCoroutine(networkReceive.ReceiveHazardData());
        }
        else
        {
            yield return Timing.WaitForOneFrame;
        }

        networkWorldUpdateCoroutineBusy = false;
    }

    //! Returns true if none of the network world update coroutines are running.
    private bool NetworkAvailable()
    {
        return networkReceive.conduitDataCoroutineBusy == false &&
        networkReceive.powerDataCoroutineBusy == false &&
        networkReceive.machineDataCoroutineBusy == false &&
        networkReceive.hubDataCoroutineBusy == false &&
        networkReceive.hazardDataCoroutineBusy == false &&
        networkStorageCoroutineBusy == false;
    }

    //! Saves the world for dedicated servers.
    public IEnumerator<float> DedicatedServerCoroutine()
    {
        dedicatedServerCoroutineBusy = true;
        yield return Timing.WaitForSeconds(30);
        if (playerController.stateManager.saving == false)
        {
            playerController.requestedSave = true;
        }
        dedicatedServerCoroutineBusy = false;
    }

    //! Gets information about other players from the server.
    public IEnumerator<float> GetNetworkPlayers()
    {
        getNetworkPlayersCoroutineBusy = true;

        playerData = "none";
        networkReceive.GetPlayerData();
        while (playerData == "none")
        {
            yield return Timing.WaitForOneFrame;
        }

        playerNames.Clear();
        string[] playerList = playerData.Split('{');
        for (int i=2; i < playerList.Length; i++)
        {
            string playerInfo = playerList[i];

            string playerName = playerInfo.Split(',')[0].Split(':')[1].Replace("'", "").TrimStart();
            float x = float.Parse(playerInfo.Split(',')[1].Split(':')[1].Replace("'", ""));
            float y = float.Parse(playerInfo.Split(',')[2].Split(':')[1].Replace("'",""));
            float z = float.Parse(playerInfo.Split(',')[3].Split(':')[1].Replace("'",""));
            float fx = float.Parse(playerInfo.Split(',')[4].Split(':')[1].Replace("'", ""));
            float fz = float.Parse(playerInfo.Split(',')[5].Split(':')[1].Replace("'", ""));
            float red = float.Parse(playerInfo.Split(',')[6].Split(':')[1].Replace("'", ""));
            float green = float.Parse(playerInfo.Split(',')[7].Split(':')[1].Replace("'", ""));
            float blue = float.Parse(playerInfo.Split(',')[8].Split(':')[1].Split('}')[0].Replace("'",""));

            Vector3 playerPosition = new Vector3(x, y, z);
            Color playerColor = new Color(red, green, blue);

            if (playerName != PlayerPrefs.GetString("UserName") && playerName != playerController.stateManager.worldName)
            {
                if (!playerNames.Contains(playerName))
                {
                    playerNames.Add(playerName);
                }
                if (!networkPlayers.ContainsKey(playerName))
                {
                    CreateNetworkPlayer(playerName, playerPosition, playerColor);
                }
                else
                {
                    UpdateNetWorkPlayer(playerInfo, playerColor);
                }
            }
            yield return Timing.WaitForOneFrame;
        }

        Dictionary<string, GameObject> allPlayers = new Dictionary<string, GameObject>(networkPlayers);
        foreach (KeyValuePair<string,GameObject> entry in allPlayers)
        {
            if (!playerNames.Contains(entry.Key))
            {
                networkPlayers.Remove(entry.Key);
                UnityEngine.Object.Destroy(entry.Value);
            }
            yield return Timing.WaitForOneFrame;
        }

        getNetworkPlayersCoroutineBusy = false;
    }

    //! Sends player positions to server.
    private void UpdateNetWorkPlayer(string playerInfo, Color playerColor)
    {
        string playerName = playerInfo.Split(',')[0].Split(':')[1].Replace("'", "");
        float x = float.Parse(playerInfo.Split(',')[1].Split(':')[1].Replace("'", ""));
        float y = float.Parse(playerInfo.Split(',')[2].Split(':')[1].Replace("'",""));
        float z = float.Parse(playerInfo.Split(',')[3].Split(':')[1].Replace("'",""));
        float fx = float.Parse(playerInfo.Split(',')[4].Split(':')[1].Replace("'", ""));
        float fz = float.Parse(playerInfo.Split(',')[5].Split(':')[1].Replace("'", ""));

        if (!playerPositions.ContainsKey(playerName))
        {
            playerPositions.Add(playerName, new Vector3(x, y, z));
        }
        else
        {
            playerPositions[playerName] = new Vector3(x, y, z);
        }

        if (networkPlayers.ContainsKey(playerName))
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
    public IEnumerator<float> MoveNetworkPlayers()
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
            yield return Timing.WaitForOneFrame;
        }
        playerMovementCoroutineBusy = false;
    }

    //! Instantiates blocks over the network.
    public IEnumerator<float> UpdateNetworkBlocks()
    {
        blockData = "none";
        networkReceive.GetBlockData();
        while (blockData == "none")
        {
            yield return Timing.WaitForOneFrame;
        }
        Timing.RunCoroutine(networkReceive.ReceiveNetworkBlocks());
    }

    //! Instantiates blocks over the network.
    public IEnumerator<float> UpdateNetworkItems()
    {
        itemData = "none";
        networkReceive.GetItemData();
        while (itemData == "none")
        {
            yield return Timing.WaitForOneFrame;
        }
        Timing.RunCoroutine(networkReceive.ReceiveNetworkItems());
    }

    //! Updates inventories over the network.
    public IEnumerator<float> UpdateNetworkStorage()
    {
        networkStorageCoroutineBusy = true;

        storageData = "none";
        networkReceive.GetStorageData();
        while (storageData == "none")
        {
            yield return Timing.WaitForOneFrame;
        }

        receivedNetworkStorage = false;
        Timing.RunCoroutine(networkReceive.ReceiveNetworkStorage());
        while (receivedNetworkStorage == false)
        {
            yield return Timing.WaitForOneFrame;
        }

        networkStorageCoroutineBusy = false;
    }

    //! Instantiates a gameobject representing another player over the network.
    private void CreateNetworkPlayer(string playerName, Vector3 playerPosition, Color playerColor)
    {
        GameObject newPlayer = UnityEngine.Object.Instantiate(playerController.networkPlayer, playerPosition, new Quaternion());
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