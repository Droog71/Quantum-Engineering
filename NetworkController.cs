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
    private Coroutine dedicatedServerCoroutine;
    private Coroutine blockUpdateCoroutine;
    private Coroutine storageSendCoroutine;
    private Coroutine storageReceiveCoroutine;
    private Coroutine networkPowerCoroutine;
    private Coroutine networkBlockCoroutine;
    private Coroutine networkStorageCoroutine;
    private Coroutine networkConduitCoroutine;
    private Coroutine networkMachineCoroutine;
    private Coroutine networkPlayerCoroutine;
    private Coroutine networkMovementCoroutine;
    private List<string> playerNames;
    private float storageUpdateInterval;
    private float blockNetTimer;
    private float storageNetTimer;
    private float playerNetTimer;
    private float conduitNetTimer;
    private float powerNetTimer;
    private float machineNetTimer;
    public bool dedicatedServerCoroutineBusy;
    public bool storageCoroutineBusy;
    public bool playerCoroutineBusy;
    public bool playerMovementCoroutineBusy;
    public bool blockCoroutineBusy;
    public bool receivedNetworkStorage;
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

    //! Handles all network traffic for multiplayer.
    public void NetWorkUpdate()
    {
        string commandLineOptions = Environment.CommandLine;
        if (commandLineOptions.Contains("-batchmode"))
        {
            if (dedicatedServerCoroutineBusy == false)
            {
                dedicatedServerCoroutine = playerController.StartCoroutine(DedicatedServerCoroutine());
            }
        }

        if (playerCoroutineBusy == false)
        {
            networkPlayerCoroutine = playerController.StartCoroutine(UpdateNetworkPlayers());
        }

        if (playerMovementCoroutineBusy == false)
        {
            networkMovementCoroutine = playerController.StartCoroutine(MoveNetworkPlayers());
        }

        playerNetTimer += 1 * Time.deltaTime;
        if (playerNetTimer >= UnityEngine.Random.Range(0.75f, 1.0f))
        {
            networkSend.SendPlayerInfo();
            playerNetTimer = 0;
        }

        blockNetTimer += 1 * Time.deltaTime;
        if (blockNetTimer >= UnityEngine.Random.Range(1.0f, 1.25f))
        {
            if (blockCoroutineBusy == false)
            {
                networkBlockCoroutine = playerController.StartCoroutine(UpdateNetworkBlocks());
            }
            blockNetTimer = 0;
        }

        conduitNetTimer += 1 * Time.deltaTime;
        if (conduitNetTimer >= UnityEngine.Random.Range(1.25f, 1.5f))
        {
            if (networkReceive.conduitDataCoroutineBusy == false)
            {
                networkConduitCoroutine = playerController.StartCoroutine(networkReceive.ReceiveConduitData());
            }
            conduitNetTimer = 0;
        }

        powerNetTimer += 1 * Time.deltaTime;
        if (powerNetTimer >= UnityEngine.Random.Range(1.5f, 1.75f))
        {
            if (networkReceive.powerDataCoroutineBusy == false)
            {
                networkPowerCoroutine = playerController.StartCoroutine(networkReceive.ReceivePowerData());
            }
            powerNetTimer = 0;
        }

        storageNetTimer += 1 * Time.deltaTime;
        if (storageNetTimer >= UnityEngine.Random.Range(1.75f, 2.0f))
        {
            if (storageCoroutineBusy == false)
            {
                networkStorageCoroutine = playerController.StartCoroutine(UpdateNetworkStorage());
                storageNetTimer = 0;
            }
        }

        machineNetTimer += 1 * Time.deltaTime;
        if (machineNetTimer >= UnityEngine.Random.Range(2.0f, 2.25f))
        {
            if (networkReceive.machineDataCoroutineBusy == false)
            {
                networkMachineCoroutine = playerController.StartCoroutine(networkReceive.ReceiveMachineData());
            }
            machineNetTimer = 0;
        }
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
    public IEnumerator UpdateNetworkPlayers()
    {
        playerCoroutineBusy = true;

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

        playerCoroutineBusy = false;
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
        if (blockCoroutineBusy == false)
        {
            blockCoroutineBusy = true;

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
        storageCoroutineBusy = true;

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

        storageCoroutineBusy = false;
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