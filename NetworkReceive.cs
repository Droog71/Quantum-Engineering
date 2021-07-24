using UnityEngine;
using System.Collections.Generic;
using MEC;
using System.Net;
using System.Threading.Tasks;

public class NetworkReceive
{
    private NetworkController networkController;
    private PlayerController playerController;
    private BlockDictionary blockDictionary;
    private string hubData;
    private string conduitData;
    private string powerData;
    private string machineData;
    private string chatData;
    private string banData;
    private string hazardData;
    private bool chatCoroutineBusy;
    private List<string> chatMessageList;
    private string[] localBlockList;
    private string[] localConduitList;
    private string[] localHubList;
    private string[] localStorageList;
    private string[] localPowerList;
    private string[] localMachineList;
    private string[] localItemList;
    public int itemDatabaseDelay;
    public bool hubDataCoroutineBusy;
    public bool conduitDataCoroutineBusy;
    public bool machineDataCoroutineBusy;
    public bool powerDataCoroutineBusy;
    public bool hazardDataCoroutineBusy;

    //! Network functions for multiplayer games.
    public NetworkReceive(NetworkController networkController)
    {
        this.networkController = networkController;
        playerController = networkController.playerController;
        blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
        chatMessageList = new List<string>();
    }

    //! Check if the player's ip is banned on this server.
    public IEnumerator<float> CheckForBan()
    {
        banData = "none";

        GetBanData();
        while (banData == "none")
        {
            yield return Timing.WaitForOneFrame;
        }

        string[] banList = banData.Split('[');
        for (int i=2; i < banList.Length; i++)
        {
            string banInfo = banList[i];
            string ip = banInfo.TrimStart('"').Split('\\')[0].Split('"')[0];
            if (ip == PlayerPrefs.GetString("ip"))
            {
                playerController.escapeMenuOpen = true;
                playerController.exiting = true;
                playerController.requestedSave = true;
                Debug.Log("Your IP address has been banned from " + networkController.serverURL);
            }
            yield return Timing.WaitForOneFrame;
        }
        networkController.checkForBanCoroutineBusy = false;
    }

    //! Processes data from chat database.
    public IEnumerator<float> ReceiveHazardData()
    {
        if (hazardDataCoroutineBusy == false)
        {
            hazardDataCoroutineBusy = true;

            hazardData = "none";

            GetHazardData();
            while (hazardData == "none")
            {
                yield return Timing.WaitForOneFrame;
            }

            string hazardsEnabled = hazardData.Split(':')[1].Split('}')[0].TrimStart('"').TrimEnd('"');
            playerController.gameManager.hazardsEnabled = hazardsEnabled == "True";

            hazardDataCoroutineBusy = false;
        }
    }

    //! Processes data from chat database.
    public IEnumerator<float> ReceiveChatData()
    {
        if (chatCoroutineBusy == false)
        {
            chatCoroutineBusy = true;

            chatData = "none";

            GetChatData();
            while (chatData == "none")
            {
                yield return Timing.WaitForOneFrame;
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
                yield return Timing.WaitForOneFrame;
            }

            chatCoroutineBusy = false;
        }
    }

    //! Processes data from block database.
    public IEnumerator<float> ReceiveNetworkBlocks()
    {
        string[] blockList = networkController.blockData.Split('{');
        if (blockList != localBlockList)
        {
            localBlockList = blockList;
            for (int i = 2; i < blockList.Length; i++)
            {
                string blockInfo = blockList[i];
                float wRot = float.Parse(blockInfo.Split(',')[0].Split(':')[1].Replace("'",""));
                float zRot = float.Parse(blockInfo.Split(',')[1].Split(':')[1].Replace("'",""));
                float xPos = float.Parse(blockInfo.Split(',')[2].Split(':')[1].Replace("'",""));
                float yPos = float.Parse(blockInfo.Split(',')[3].Split(':')[1].Replace("'",""));
                int destroy = int.Parse(blockInfo.Split(',')[4].Split(':')[1].Replace("'",""));
                float zPos = float.Parse(blockInfo.Split(',')[5].Split(':')[1].Replace("'",""));
                float xRot = float.Parse(blockInfo.Split(',')[6].Split(':')[1].Replace("'",""));
                float yRot = float.Parse(blockInfo.Split(',')[7].Split(':')[1].Replace("'",""));
                string blockType = blockInfo.Split(',')[8].Split(':')[1].Split('}')[0].Replace("'", "");
                Debug.Log(wRot + "," + zRot + "," + xPos + "," + yPos + "," + destroy + "," + zPos + "," + xRot + "," + yRot + "," + blockType);
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
                                if (block.GetComponent<Block>() != null)
                                {
                                    if (block.GetComponent<Block>().blockName == blockType)
                                    {
                                        if (destroy == 1)
                                        {
                                            BlockHolder blockHolder = block.transform.parent.GetComponent<BlockHolder>();
                                            if (blockHolder != null)
                                            {
                                                Debug.Log("removing block line 213");
                                                playerController.gameManager.meshManager.RemoveBlock(blockHolder, blockPos, false);
                                            }
                                            else
                                            {
                                                Debug.Log("removing block line 218");
                                                Object.Destroy(block.gameObject);
                                            }
                                        }
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (found == false && destroy == 1)
                    {
                        BlockHolder[] blockHolders = Object.FindObjectsOfType<BlockHolder>();
                        foreach (BlockHolder blockHolder in blockHolders)
                        {
                            if (blockHolder.blockData != null)
                            {
                                if (blockHolder.blockType == blockType)
                                {
                                    if (blockHolder.blockData.Count > 0)
                                    {
                                        foreach (BlockHolder.BlockInfo info in blockHolder.blockData)
                                        {
                                            if (Vector3.Distance(info.position, blockPos) < 10)
                                            {
                                                Debug.Log("removing block line 245");
                                                playerController.gameManager.meshManager.RemoveBlock(blockHolder, blockPos, false);
                                                found = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (found == true)
                            {
                                break;
                            }
                        }
                    }
                }

                if (found == false && destroy == 0)
                {
                    if (blockDictionary.blockDictionary.ContainsKey(blockType))
                    {
                        GameObject obj = Object.Instantiate(blockDictionary.blockDictionary[blockType], blockPos, blockRot);
                        if (obj.GetComponent<Block>() != null)
                        {
                            obj.GetComponent<Block>().blockName = blockType;
                        }
                        obj.transform.parent = playerController.gameManager.builtObjects.transform;
                    }
                    else if (blockDictionary.machineDictionary.ContainsKey(blockType))
                    {
                        GameObject newObject = Object.Instantiate(blockDictionary.machineDictionary[blockType], blockPos, blockRot);
                        playerController.gameManager.GetComponent<MachineManager>().AddMachine(newObject.GetComponent<Machine>());
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
        yield return Timing.WaitForOneFrame;
        networkController.networkBlockCoroutineBusy = false;
    }

    //! Processes data from storge database.
    public IEnumerator<float> ReceiveNetworkStorage()
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
                            if (foundPos == storagePos && manager.inventory[slot].pendingNetworkUpdate == false)
                            {
                                manager.inventory[slot].typeInSlot = type;
                                manager.inventory[slot].amountInSlot = amount;
                                break;
                            }
                        }
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
            networkController.receivedNetworkStorage = true;
        }
    }

    //! Processes data from conduit database.
    public IEnumerator<float> ReceiveConduitData()
    {
        conduitDataCoroutineBusy = true;
        conduitData = "none";
        GetConduitData();
        while (conduitData == "none")
        {
            yield return Timing.WaitForOneFrame;
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
                    yield return Timing.WaitForOneFrame;
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
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        conduitDataCoroutineBusy = false;
    }

    //! Processes data from railcart hub database.
    public IEnumerator<float> ReceiveHubData()
    {
        hubDataCoroutineBusy = true;
        hubData = "none";
        GetHubData();
        while (hubData == "none")
        {
            yield return Timing.WaitForOneFrame;
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
                    yield return Timing.WaitForOneFrame;
                }
            }
        }
        hubDataCoroutineBusy = false;
    }

    //! Processes data from machine database.
    public IEnumerator<float> ReceiveMachineData()
    {
        machineDataCoroutineBusy = true;

        machineData = "none";
        GetMachineData();
        while (machineData == "none")
        {
            yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            if (machine.connectionFailed == true)
                            {
                                machine.connectionAttempts = 0;
                                machine.connectionFailed = false;
                            }
                            machine.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            if (extractor.connectionFailed == true)
                            {
                                extractor.connectionAttempts = 0;
                                extractor.connectionFailed = false;
                            }
                            extractor.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            if (collector.connectionFailed == true)
                            {
                                collector.connectionAttempts = 0;
                                collector.connectionFailed = false;
                            }
                            collector.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            if (hx.connectionFailed == true)
                            {
                                hx.connectionAttempts = 0;
                                hx.connectionFailed = false;
                            }
                            hx.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            if (alloySmelter.connectionFailed == true)
                            {
                                alloySmelter.connectionAttempts = 0;
                                alloySmelter.connectionFailed = false;
                            }
                            alloySmelter.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            auger.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            autoCrafter.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            retriever.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
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
                        if (foundPos == machinePos)
                        {
                            turret.speed = speed;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
        }
        machineDataCoroutineBusy = false;
    }

    //! Processes data from power conduit database.
    public IEnumerator<float> ReceivePowerData()
    {
        powerDataCoroutineBusy = true;
        powerData = "none";
        GetPowerData();
        while (powerData == "none")
        {
            yield return Timing.WaitForOneFrame;
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
                        if (foundPos == powerPos)
                        {
                            if (powerConduit.connectionFailed == true)
                            {
                                powerConduit.connectionAttempts = 0;
                                powerConduit.connectionFailed = false;
                            }
                            powerConduit.range = range;
                            powerConduit.dualOutput = dual == true;
                        }
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
        }
        powerDataCoroutineBusy = false;
    }

    //! Processes data from item database.
    public IEnumerator<float> ReceiveNetworkItems()
    {
        string[] itemlist = networkController.itemData.Split('[');
        if (itemlist != localItemList)
        {
            localItemList = itemlist;
            for (int i = 2; i < itemlist.Length; i++)
            {
                string itemInfo = itemlist[i];
                int destroy = int.Parse(itemInfo.Split(',')[0]);
                string itemType = itemInfo.Split(',')[1].Substring(2).TrimEnd('"');
                int itemAmount = int.Parse(itemInfo.Split(',')[2]);
                float xPos = float.Parse(itemInfo.Split(',')[3]);
                float yPos = float.Parse(itemInfo.Split(',')[4]);
                float zPos = float.Parse(itemInfo.Split(',')[5].Split(']')[0]);
                Vector3 itemPos = new Vector3(xPos, yPos, zPos);
                bool found = false;
                Item[] allItems = Object.FindObjectsOfType<Item>();
                foreach (Item item in allItems)
                {
                    GameObject obj = item.gameObject;
                    if (item.gameObject != null)
                    {
                        if (item.startPosition == itemPos)
                        {
                            if (destroy == 1 && item.type == itemType && item.amount == itemAmount)
                            {
                                Object.Destroy(obj);
                            }
                            found = true;
                            break;
                        }
                    }
                }
                if (found == false && destroy == 0)
                {
                    itemDatabaseDelay++;
                    if (itemDatabaseDelay >= 10)
                    {
                        itemDatabaseDelay = 0;
                        GameObject newItem = Object.Instantiate(playerController.item, itemPos, new Quaternion());
                        newItem.GetComponent<Item>().startPosition = itemPos;
                        newItem.GetComponent<Item>().type = itemType;
                        newItem.GetComponent<Item>().amount = itemAmount;
                    }
                }
            }
        }
        yield return Timing.WaitForSeconds(0.1f);
        itemDatabaseDelay++;
        networkController.networkItemCoroutineBusy = false;
    }

    //! Gets hazards setting from server.
    private async Task GetHazardData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/hazards");
            hazardData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets chat messages from server.
    private async Task GetChatData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/chat");
            chatData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets inventory contents from server.
    public async Task GetStorageData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/storage");
            networkController.storageData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets data on recently instantiated blocks from server.
    public async Task GetBlockData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/blocks");
            networkController.blockData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets machine data from server.
    public async Task GetItemData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/items");
            networkController.itemData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets player data from server.
    public async Task GetPlayerData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/players");
            networkController.playerData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets railcart hub data from server.
    private async Task GetHubData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/hubs");
            hubData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets universal and dark matter conduit data from server.
    private async Task GetConduitData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/conduits");
            conduitData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets power conduit data from server.
    private async Task GetPowerData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/power");
            powerData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets machine data from server.
    private async Task GetMachineData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/machines");
            machineData = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Gets machine data from server.
    private async Task GetBanData()
    {
        using (WebClient client = new WebClient())
        {    
            System.Uri uri = new System.Uri(networkController.serverURL+"/bans");
            banData = await client.DownloadStringTaskAsync(uri);
        }
    }
}
