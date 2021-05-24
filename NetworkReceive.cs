using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

public class NetworkReceive
{
    private NetworkController networkController;
    private PlayerController playerController;
    private BlockDictionary blockDictionary;
    private string serverURL;
    private string hubData;
    private string conduitData;
    private string powerData;
    private string machineData;
    private string chatData;
    private string paintData;
    private bool chatCoroutineBusy;
    private List<string> chatMessageList;
    private string[] localBlockList;
    private string[] localConduitList;
    private string[] localHubList;
    private string[] localStorageList;
    private string[] localPowerList;
    private string[] localMachineList;
    public bool hubDataCoroutineBusy;
    public bool conduitDataCoroutineBusy;
    public bool machineDataCoroutineBusy;
    public bool powerDataCoroutineBusy;

    //! Network functions for multiplayer games.
    public NetworkReceive(NetworkController networkController)
    {
        this.networkController = networkController;
        playerController = networkController.playerController;
        blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
        serverURL = networkController.serverURL;
        chatMessageList = new List<string>();
    }

    //! Processes data from chat database.
    public IEnumerator ReceiveChatData()
    {
        if (chatCoroutineBusy == false)
        {
            chatCoroutineBusy = true;

            chatData = "none";

            GetChatData();
            while (chatData == "none")
            {
                yield return null;
            }

            string[] chatMessages = chatData.Split('[');
            if (chatMessages.Length == 2)
            {
                chatMessageList.Clear();
            }

            for (int i=2; i < chatMessages.Length; i++)
            {
                string messageInfo = chatMessages[i];
                string playerName = messageInfo.Split(':')[0].Split(',')[0].TrimStart('"').TrimEnd('"');
                string message = messageInfo.Split(',')[1].Split(']')[0].Substring(2).TrimEnd('"');
                string messageToSend = "\n" + playerName + ": " + message;
                if (!chatMessageList.Contains(messageToSend))
                {
                    chatMessageList.Add(messageToSend);
                    playerController.GetComponent<ChatGUI>().messages += messageToSend;
                }
                yield return null;
            }

            chatCoroutineBusy = false;
        }
    }

    //! Processes data from block database.
    public IEnumerator ReceiveNetworkBlocks()
    {
        string[] blockList = networkController.blockData.Split('[');
        if (blockList != localBlockList)
        {
            localBlockList = blockList;
            for (int i = 2; i < blockList.Length; i++)
            {
                string blockInfo = blockList[i];
                int destroy = int.Parse(blockInfo.Split(',')[0]);
                string blockType = blockInfo.Split(',')[1].Substring(2).TrimEnd('"');
                float xPos = float.Parse(blockInfo.Split(',')[2]);
                float yPos = float.Parse(blockInfo.Split(',')[3]);
                float zPos = float.Parse(blockInfo.Split(',')[4]);
                float xRot = float.Parse(blockInfo.Split(',')[5]);
                float yRot = float.Parse(blockInfo.Split(',')[6]);
                float zRot = float.Parse(blockInfo.Split(',')[7]);
                float wRot = float.Parse(blockInfo.Split(',')[8].Split(']')[0]);
                Vector3 blockPos = new Vector3(xPos, yPos, zPos);
                Quaternion blockRot = new Quaternion(xRot, yRot, zRot, wRot);
                bool found = false;
                if (blockDictionary.machineDictionary.ContainsKey(blockType))
                {
                    System.Type t = blockDictionary.typeDictionary[blockType];
                    GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj != null)
                        {
                            float x = Mathf.Round(obj.transform.position.x);
                            float y = Mathf.Round(obj.transform.position.y);
                            float z = Mathf.Round(obj.transform.position.z);
                            Vector3 foundPos = new Vector3(x, y, z);
                            if (obj.GetComponent(t) != null && foundPos == blockPos)
                            {
                                if (destroy == 1)
                                {
                                    Object.Destroy(obj);
                                }
                                found = true;
                                break;
                            }
                            if (obj.GetComponent<RailCart>() != null)
                            {
                                if (obj.GetComponent<RailCart>().startPosition == blockPos)
                                {
                                    if (destroy == 1)
                                    {
                                        Object.Destroy(obj);
                                    }
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (blockDictionary.blockDictionary.ContainsKey(blockType))
                {
                    Transform[] allBlocks = playerController.gameManager.builtObjects.GetComponentsInChildren<Transform>(true);
                    foreach (Transform block in allBlocks)
                    {
                        if (block != null)
                        {
                            float x = Mathf.Round(block.position.x);
                            float y = Mathf.Round(block.position.y);
                            float z = Mathf.Round(block.position.z);
                            Vector3 foundPos = new Vector3(x, y, z);
                            if (foundPos == blockPos)
                            {
                                if (blockDictionary.typeDictionary.ContainsKey(blockType))
                                {
                                    System.Type t = blockDictionary.typeDictionary[blockType];
                                    if (block.GetComponent(t) != null)
                                    {
                                        if (destroy == 1)
                                        {
                                            Object.Destroy(block.gameObject);
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                                else if (block.GetComponent<ModBlock>() != null)
                                {
                                    if (block.GetComponent<ModBlock>().blockName == blockType)
                                    {
                                        if (destroy == 1)
                                        {
                                            Object.Destroy(block.gameObject);
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (found == false && destroy == 0)
                {
                    if (blockDictionary.blockDictionary.ContainsKey(blockType))
                    {
                        GameObject obj = Object.Instantiate(blockDictionary.blockDictionary[blockType], blockPos, blockRot);
                        if (obj.GetComponent<ModBlock>() != null)
                        {
                            obj.GetComponent<ModBlock>().blockName = blockType;
                        }
                        obj.transform.parent = playerController.gameManager.builtObjects.transform;
                    }
                    else if (blockDictionary.machineDictionary.ContainsKey(blockType))
                    {
                        GameObject newObject = Object.Instantiate(blockDictionary.machineDictionary[blockType], blockPos, blockRot);
                        if (newObject.GetComponent<RailCart>() != null)
                        {
                            newObject.GetComponent<RailCart>().startPosition = blockPos;
                            RailCartHub[] hubs = Object.FindObjectsOfType<RailCartHub>();
                            foreach (RailCartHub hub in hubs)
                            {
                                if (hub != null)
                                {
                                    float distance = Vector3.Distance(newObject.transform.position, hub.gameObject.transform.position);
                                    if (distance <= 5)
                                    {
                                        newObject.GetComponent<RailCart>().target = hub.gameObject;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        yield return null;
        networkController.networkBlockCoroutineBusy = false;
    }

    //! Processes data from storge database.
    public IEnumerator ReceiveNetworkStorage()
    {
        string[] storageList = networkController.storageData.Split('[');
        if (storageList != localStorageList)
        {
            localStorageList = storageList;
            for (int i = 2; i < storageList.Length; i++)
            {
                string storageInfo = storageList[i];
                float xPos = float.Parse(storageInfo.Split(',')[0]);
                float yPos = float.Parse(storageInfo.Split(',')[1]);
                float zPos = float.Parse(storageInfo.Split(',')[2]);
                Vector3 storagePos = new Vector3(xPos, yPos, zPos);
                int slot = int.Parse(storageInfo.Split(',')[3]);
                string type = storageInfo.Split(',')[4].Substring(2).TrimEnd('"');
                int amount = int.Parse(storageInfo.Split(',')[5].Split(']')[0]);
                InventoryManager[] allInventories = Object.FindObjectsOfType<InventoryManager>();
                foreach (InventoryManager manager in allInventories)
                {
                    if (manager != null)
                    {
                        if (manager.initialized == true)
                        {
                            Vector3 pos = manager.gameObject.transform.position;
                            float x = Mathf.Round(pos.x);
                            float y = Mathf.Round(pos.y);
                            float z = Mathf.Round(pos.z);
                            Vector3 foundPos = new Vector3(x, y, z);
                            if (foundPos == storagePos)
                            {
                                manager.inventory[slot].typeInSlot = type;
                                manager.inventory[slot].amountInSlot = amount;
                                break;
                            }
                        }
                    }
                    yield return null;
                }
            }
            networkController.receivedNetworkStorage = true;
        }
    }

    //! Processes data from conduit database.
    public IEnumerator ReceiveConduitData()
    {
        conduitDataCoroutineBusy = true;
        conduitData = "none";
        GetConduitData();
        while (conduitData == "none")
        {
            yield return null;
        }
        string[] conduitList = conduitData.Split('[');
        if (conduitList != localConduitList)
        {
            localConduitList = conduitList;
            for (int i = 2; i < conduitList.Length; i++)
            {
                string conduitInfo = conduitList[i];
                float xPos = float.Parse(conduitInfo.Split(',')[0]);
                float yPos = float.Parse(conduitInfo.Split(',')[1]);
                float zPos = float.Parse(conduitInfo.Split(',')[2]);
                Vector3 conduitPos = new Vector3(xPos, yPos, zPos);
                int range = int.Parse(conduitInfo.Split(',')[3].Split(']')[0]);

                UniversalConduit[] allConduits = Object.FindObjectsOfType<UniversalConduit>();
                foreach (UniversalConduit conduit in allConduits)
                {
                    if (conduit != null)
                    {
                        Vector3 pos = conduit.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == conduitPos && conduit.range != range)
                        {
                            if (conduit.connectionFailed == true)
                            {
                                conduit.connectionAttempts = 0;
                                conduit.connectionFailed = false;
                            }
                            conduit.range = range;
                        }
                    }
                    yield return null;
                }

                DarkMatterConduit[] allDarkMatterConduits = Object.FindObjectsOfType<DarkMatterConduit>();
                foreach (DarkMatterConduit conduit in allDarkMatterConduits)
                {
                    if (conduit != null)
                    {
                        Vector3 pos = conduit.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == conduitPos && conduit.range != range)
                        {
                            if (conduit.connectionFailed == true)
                            {
                                conduit.connectionAttempts = 0;
                                conduit.connectionFailed = false;
                            }
                            conduit.range = range;
                        }
                    }
                    yield return null;
                }
            }
        }

        conduitDataCoroutineBusy = false;
    }

    //! Processes data from railcart hub database.
    public IEnumerator ReceiveHubData()
    {
        hubDataCoroutineBusy = true;
        hubData = "none";
        GetHubData();
        while (hubData == "none")
        {
            yield return null;
        }
        string[] hubList = hubData.Split('[');
        if (hubList != localHubList)
        {
            localHubList = hubList;
            for (int i = 2; i < hubList.Length; i++)
            {
                string hubInfo = hubList[i];
                float xPos = float.Parse(hubInfo.Split(',')[0]);
                float yPos = float.Parse(hubInfo.Split(',')[1]);
                float zPos = float.Parse(hubInfo.Split(',')[2]);
                int circuit = int.Parse(hubInfo.Split(',')[3]);
                int range = int.Parse(hubInfo.Split(',')[4]);
                int stop = int.Parse(hubInfo.Split(',')[5]);
                float time = int.Parse(hubInfo.Split(',')[6].Split(']')[0]);
                Vector3 hubPos = new Vector3(xPos, yPos, zPos);

                RailCartHub[] allHubs = Object.FindObjectsOfType<RailCartHub>();
                foreach (RailCartHub hub in allHubs)
                {
                    if (hub != null)
                    {
                        Vector3 pos = hub.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == hubPos)
                        {
                            if (hub.connectionFailed == true)
                            {
                                hub.connectionAttempts = 0;
                                hub.connectionFailed = false;
                            }
                            hub.circuit = circuit;
                            hub.range = range;
                            hub.stop = stop == 1;
                            hub.stopTime = time;
                        }
                    }
                    yield return null;
                }
            }
        }
        hubDataCoroutineBusy = false;
    }

    //! Processes data from machine database.
    public IEnumerator ReceiveMachineData()
    {
        machineDataCoroutineBusy = true;

        machineData = "none";
        GetMachineData();
        while (machineData == "none")
        {
            yield return null;
        }

        string[] machineList = machineData.Split('[');
        if (machineList != localMachineList)
        {
            localMachineList = machineList;
            for (int i = 2; i < machineList.Length; i++)
            {
                string machineInfo = machineList[i];
                float xPos = float.Parse(machineInfo.Split(',')[0]);
                float yPos = float.Parse(machineInfo.Split(',')[1]);
                float zPos = float.Parse(machineInfo.Split(',')[2]);
                Vector3 machinePos = new Vector3(xPos, yPos, zPos);
                int speed = int.Parse(machineInfo.Split(',')[3].Split(']')[0]);
                BasicMachine[] allMachines = Object.FindObjectsOfType<BasicMachine>();
                foreach (BasicMachine machine in allMachines)
                {
                    if (machine != null)
                    {
                        Vector3 pos = machine.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && machine.speed != speed)
                        {
                            if (machine.connectionFailed == true)
                            {
                                machine.connectionAttempts = 0;
                                machine.connectionFailed = false;
                            }
                            machine.speed = speed;
                        }
                    }
                    yield return null;
                }
                UniversalExtractor[] allExtractors = Object.FindObjectsOfType<UniversalExtractor>();
                foreach (UniversalExtractor extractor in allExtractors)
                {
                    if (extractor != null)
                    {
                        Vector3 pos = extractor.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && extractor.speed != speed)
                        {
                            if (extractor.connectionFailed == true)
                            {
                                extractor.connectionAttempts = 0;
                                extractor.connectionFailed = false;
                            }
                            extractor.speed = speed;
                        }
                    }
                    yield return null;
                }
                DarkMatterCollector[] allCollectors = Object.FindObjectsOfType<DarkMatterCollector>();
                foreach (DarkMatterCollector collector in allCollectors)
                {
                    if (collector != null)
                    {
                        Vector3 pos = collector.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && collector.speed != speed)
                        {
                            if (collector.connectionFailed == true)
                            {
                                collector.connectionAttempts = 0;
                                collector.connectionFailed = false;
                            }
                            collector.speed = speed;
                        }
                    }
                    yield return null;
                }
                HeatExchanger[] allHX = Object.FindObjectsOfType<HeatExchanger>();
                foreach (HeatExchanger hx in allHX)
                {
                    if (hx != null)
                    {
                        Vector3 pos = hx.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && hx.speed != speed)
                        {
                            if (hx.connectionFailed == true)
                            {
                                hx.connectionAttempts = 0;
                                hx.connectionFailed = false;
                            }
                            hx.speed = speed;
                        }
                    }
                    yield return null;
                }
                AlloySmelter[] allAlloySmelters = Object.FindObjectsOfType<AlloySmelter>();
                foreach (AlloySmelter alloySmelter in allAlloySmelters)
                {
                    if (alloySmelter != null)
                    {
                        Vector3 pos = alloySmelter.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && alloySmelter.speed != speed)
                        {
                            if (alloySmelter.connectionFailed == true)
                            {
                                alloySmelter.connectionAttempts = 0;
                                alloySmelter.connectionFailed = false;
                            }
                            alloySmelter.speed = speed;
                        }
                    }
                    yield return null;
                }
                Auger[] allAugers = Object.FindObjectsOfType<Auger>();
                foreach (Auger auger in allAugers)
                {
                    if (auger != null)
                    {
                        Vector3 pos = auger.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && auger.speed != speed)
                        {
                            auger.speed = speed;
                        }
                    }
                    yield return null;
                }
                AutoCrafter[] allAutoCrafters = Object.FindObjectsOfType<AutoCrafter>();
                foreach (AutoCrafter autoCrafter in allAutoCrafters)
                {
                    if (autoCrafter != null)
                    {
                        Vector3 pos = autoCrafter.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && autoCrafter.speed != speed)
                        {
                            autoCrafter.speed = speed;
                        }
                    }
                    yield return null;
                }
                Retriever[] allRetrievers = Object.FindObjectsOfType<Retriever>();
                foreach (Retriever retriever in allRetrievers)
                {
                    if (retriever != null)
                    {
                        Vector3 pos = retriever.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && retriever.speed != speed)
                        {
                            retriever.speed = speed;
                        }
                    }
                    yield return null;
                }
                Turret[] allTurrets = Object.FindObjectsOfType<Turret>();
                foreach (Turret turret in allTurrets)
                {
                    if (turret != null)
                    {
                        Vector3 pos = turret.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == machinePos && turret.speed != speed)
                        {
                            turret.speed = speed;
                        }
                    }
                    yield return null;
                }
            }
        }
        machineDataCoroutineBusy = false;
    }

    //! Processes data from power conduit database.
    public IEnumerator ReceivePowerData()
    {
        powerDataCoroutineBusy = true;
        powerData = "none";
        GetPowerData();
        while (powerData == "none")
        {
            yield return null;
        }

        string[] powerList = powerData.Split('[');
        if (powerList != localPowerList)
        {
            localPowerList = powerList;
            for (int i = 2; i < powerList.Length; i++)
            {
                string powerInfo = powerList[i];
                float xPos = float.Parse(powerInfo.Split(',')[0]);
                float yPos = float.Parse(powerInfo.Split(',')[1]);
                float zPos = float.Parse(powerInfo.Split(',')[2]);
                Vector3 powerPos = new Vector3(xPos, yPos, zPos);
                int range = int.Parse(powerInfo.Split(',')[3]);
                bool dual = bool.Parse(powerInfo.Split(',')[4].Split(']')[0].Substring(2).TrimEnd('"'));

                PowerConduit[] allPowerConduits = Object.FindObjectsOfType<PowerConduit>();
                foreach (PowerConduit powerConduit in allPowerConduits)
                {
                    if (powerConduit != null)
                    {
                        Vector3 pos = powerConduit.gameObject.transform.position;
                        float x = Mathf.Round(pos.x);
                        float y = Mathf.Round(pos.y);
                        float z = Mathf.Round(pos.z);
                        Vector3 foundPos = new Vector3(x, y, z);
                        if (foundPos == powerPos && powerConduit.range != range)
                        {
                            if (powerConduit.connectionFailed == true)
                            {
                                powerConduit.connectionAttempts = 0;
                                powerConduit.connectionFailed = false;
                            }
                            powerConduit.dualOutput = dual == true;
                            powerConduit.range = range;
                        }
                    }
                    yield return null;
                }
            }
        }
        powerDataCoroutineBusy = false;
    }

    //! Gets chat messages from server.
    private async Task GetChatData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/chat");
            chatData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets inventory contents from server.
    public async Task GetStorageData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/storage");
            networkController.storageData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets data on recently instantiated blocks from server.
    public async Task GetBlockData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/blocks");
            networkController.blockData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets player data from server.
    public async Task GetPlayerData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/players");
            networkController.playerData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets railcart hub data from server.
    private async Task GetHubData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/hubs");
            hubData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets universal and dark matter conduit data from server.
    private async Task GetConduitData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/conduits");
            conduitData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets power conduit data from server.
    private async Task GetPowerData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/power");
            powerData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets machine data from server.
    private async Task GetMachineData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/machines");
            machineData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets painted block colors from server.
    private async Task GetPaintData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(serverURL+"/paint");
            paintData = await client.DownloadStringTaskAsync(uri);
        }
    }
}
