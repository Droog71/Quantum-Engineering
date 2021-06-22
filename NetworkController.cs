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
    private Coroutine itemUpdateCoroutine;
    private Coroutine storageReceiveCoroutine;
    private Coroutine networkPowerCoroutine;
    private Coroutine networkBlockCoroutine;
    private Coroutine networkItemCoroutine;
    private Coroutine networkStorageCoroutine;
    private Coroutine networkConduitCoroutine;
    private Coroutine networkMachineCoroutine;
    private Coroutine networkHubCoroutine;
    private Coroutine networkHazardCoroutine;
    private Coroutine getNetworkPlayersCoroutine;
    private Coroutine networkMovementCoroutine;
    private Coroutine announceCoroutine;
    private Coroutine checkForBanCoroutine;
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

        if (networkBlockCoroutineBusy == false)
        {
            networkBlockCoroutineBusy = true;
            networkBlockCoroutine = playerController.StartCoroutine(UpdateNetworkBlocks());
        }

        if (networkItemCoroutineBusy == false)
        {
            networkItemCoroutineBusy = true;
            networkItemCoroutine = playerController.StartCoroutine(UpdateNetworkItems());
        }

        if (announceCoroutineBusy == false && PlayerPrefsX.GetPersistentBool("hosting") == true && PlayerPrefsX.GetPersistentBool("announce") == true)
        {
            announceCoroutineBusy = true;
            announceCoroutine = playerController.StartCoroutine(networkSend.Announce());
        }

        if (checkForBanCoroutineBusy == false && PlayerPrefsX.GetPersistentBool("hosting") == false)
        {
            checkForBanCoroutineBusy = true;
            checkForBanCoroutine = playerController.StartCoroutine(networkReceive.CheckForBan());
        }
    }

    //! Send information about this player to the server.
    public IEnumerator SendNetworkPlayerInfo()
    {
        sendNetworkPlayerCoroutineBusy = true;
        networkSend.SendPlayerInfo();
        yield return new WaitForSeconds(0.5f);
        sendNetworkPlayerCoroutineBusy = false;
    }

    //! Updates the world over the network.
    public IEnumerator NetWorkWorldUpdate()
    {
        networkWorldUpdateCoroutineBusy = true;

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
            networkHubCoroutine = playerController.StartCoroutine(networkReceive.ReceiveHubData());
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

        if (NetworkAvailable())
        {
            networkHazardCoroutine = playerController.StartCoroutine(networkReceive.ReceiveHazardData());
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
        return networkReceive.conduitDataCoroutineBusy == false &&
        networkReceive.powerDataCoroutineBusy == false &&
        networkReceive.machineDataCoroutineBusy == false &&
        networkReceive.hubDataCoroutineBusy == false &&
        networkReceive.hazardDataCoroutineBusy == false &&
        networkStorageCoroutineBusy == false;
    }

    //! Saves the world for dedicated servers.
    public IEnumerator DedicatedServerCoroutine()
    {
        dedicatedServerCoroutineBusy = true;
        yield return new WaitForSeconds(30);
        if (playerController.stateManager.saving == false)
        {
            playerController.requestedSave = true;
        }
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
            float x = float.Parse(playerInfo.Split(',')[1]);
            float y = float.Parse(playerInfo.Split(',')[2]);
            float z = float.Parse(playerInfo.Split(',')[3]);
            float fx = float.Parse(playerInfo.Split(',')[4]);
            float fz = float.Parse(playerInfo.Split(',')[5]);
            float red = float.Parse(playerInfo.Split(',')[6]);
            float green = float.Parse(playerInfo.Split(',')[7]);
            float blue = float.Parse(playerInfo.Split(',')[8].Split(']')[0]);
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
        blockData = "none";
        networkReceive.GetBlockData();
        while (blockData == "none")
        {
            yield return null;
        }
        blockUpdateCoroutine = playerController.StartCoroutine(networkReceive.ReceiveNetworkBlocks());
    }

    //! Instantiates blocks over the network.
    public IEnumerator UpdateNetworkItems()
    {
        itemData = "none";
        networkReceive.GetItemData();
        while (itemData == "none")
        {
            yield return null;
        }
        itemUpdateCoroutine = playerController.StartCoroutine(networkReceive.ReceiveNetworkItems());
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