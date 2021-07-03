using UnityEngine;
using System.Collections.Generic;
using MEC;

public class MachineGUI : MonoBehaviour
{
    private PlayerController playerController;
    private GuiCoordinates guiCoordinates;
    private TextureDictionary textureDictionary;
    private bool hubStopWindowOpen;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textureDictionary = gameManager.GetComponent<TextureDictionary>();
        guiCoordinates = new GuiCoordinates();
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        // STYLE
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        // ASPECT RATIO
        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;
        if (ScreenWidth / ScreenHeight < 1.7f)
        {
            ScreenHeight = (ScreenHeight * 0.75f);
        }
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        if (!playerController.stateManager.Busy() && GetComponent<MainMenu>().finishedLoading == true)
        {
            // MACHINE CONTROL GUI
            if (playerController.inventoryOpen == false && playerController.machineGUIopen == true && playerController.objectInSight != null)
            {
                GameObject obj = playerController.objectInSight;

                if (obj.GetComponent<PowerConduit>() != null)
                {
                    bool netFlag = false;
                    PowerConduit powerConduit = obj.GetComponent<PowerConduit>();
                    if (powerConduit.connectionFailed == false)
                    {
                        GUI.DrawTexture(guiCoordinates.FourButtonSpeedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.outputLabelRect, "Range");
                        powerConduit.range = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, powerConduit.range, 6, 120);
                        if (GUI.Button(guiCoordinates.outputControlButton3Rect, "Dual Output: " + powerConduit.dualOutput))
                        {
                            if (powerConduit.dualOutput == true)
                            {
                                powerConduit.dualOutput = false;
                            }
                            else
                            {
                                powerConduit.dualOutput = true;
                            }
                            playerController.PlayButtonSound();
                        }
                        if (GUI.Button(guiCoordinates.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            powerConduit.connectionAttempts = 0;
                            powerConduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        bool rangeDeSync = powerConduit.range != playerController.networkedConduitRange;
                        bool outputDeSync = powerConduit.dualOutput != playerController.networkedDualPower;
                        if (rangeDeSync || outputDeSync || netFlag)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = powerConduit.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendPowerData(location,powerConduit.range,powerConduit.dualOutput));
                            playerController.networkedConduitRange = powerConduit.range;
                            playerController.networkedDualPower = powerConduit.dualOutput;
                        }
                    }
                }

                if (obj.GetComponent<RailCartHub>() != null)
                {
                    bool netFlag = false;
                    RailCartHub hub = obj.GetComponent<RailCartHub>();
                    if (hub.connectionFailed == false)
                    {
                        if (hubStopWindowOpen == false)
                        {
                            GUI.DrawTexture(guiCoordinates.FiveButtonSpeedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                            GUI.Label(guiCoordinates.railCartHubCircuitLabelRect, "Circuit");
                            int circuit = hub.circuit;
                            string circuitString = GUI.TextField(guiCoordinates.railCartHubCircuitRect, circuit.ToString(), 3);
                            try
                            {
                                hub.circuit = int.Parse(circuitString);
                            }
                            catch
                            {
                                // NOOP
                            }
                            GUI.Label(guiCoordinates.outputLabelRect, "Range");
                            hub.range = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, hub.range, 6, 120);
                            if (GUI.Button(guiCoordinates.outputControlButton3Rect, "Stop Settings"))
                            {
                                hubStopWindowOpen = true;
                                playerController.PlayButtonSound();
                            }
                            if (GUI.Button(guiCoordinates.outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.PlayButtonSound();
                            }
                        }
                        else
                        {
                            GUI.DrawTexture(guiCoordinates.FiveButtonSpeedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                            GUI.Label(guiCoordinates.longOutputLabelRect, "Stop Time");
                            if (GUI.Button(guiCoordinates.outputControlButton0Rect, "Stop: " + hub.stop))
                            {
                                if (hub.stop == true)
                                {
                                    hub.stop = false;
                                }
                                else
                                {
                                    hub.stop = true;
                                }
                                playerController.PlayButtonSound();
                            }
                            hub.stopTime = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, hub.stopTime, 0, 600);
                            if (GUI.Button(guiCoordinates.outputControlButton3Rect, "Range Settings"))
                            {
                                hubStopWindowOpen = false;
                                playerController.PlayButtonSound();
                            }
                            if (GUI.Button(guiCoordinates.outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.PlayButtonSound();
                            }
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            hub.connectionAttempts = 0;
                            hub.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        bool circuitDeSync = hub.circuit != playerController.networkedHubCircuit;
                        bool rangeDeSync = hub.range != playerController.networkedHubRange;
                        bool stopDeSync = hub.stop != playerController.networkedHubStop;
                        bool timeDeSync = (int)hub.stopTime != (int)playerController.networkedHubStopTime;
                        if (circuitDeSync || rangeDeSync || stopDeSync || timeDeSync || netFlag)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = hub.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendHubData(location, hub.circuit, hub.range, hub.stop, hub.stopTime));
                            playerController.networkedHubCircuit = hub.circuit;
                            playerController.networkedHubRange = hub.range;
                            playerController.networkedHubStop = hub.stop;
                            playerController.networkedHubStopTime = hub.stopTime;
                        }
                    }
                }

                if (obj.GetComponent<Retriever>() != null)
                {
                    bool netFlag = false;
                    Retriever retriever = obj.GetComponent<Retriever>();
                    if (retriever.connectionFailed == false)
                    {
                        GUI.DrawTexture(guiCoordinates.FourButtonSpeedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        if (retriever.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            retriever.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, retriever.speed, 0, retriever.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                        if (GUI.Button(guiCoordinates.outputControlButton3Rect, "Choose Items"))
                        {
                            if (obj.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.PlayButtonSound();
                            }
                        }
                        if (GUI.Button(guiCoordinates.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            retriever.connectionAttempts = 0;
                            retriever.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (retriever.speed != playerController.networkedConduitRange || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = retriever.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendConduitData(location,retriever.speed));
                            playerController.networkedConduitRange = retriever.speed;
                        }
                    }
                }

                if (obj.GetComponent<AutoCrafter>() != null)
                {
                    bool netFlag = false;
                    AutoCrafter autoCrafter = obj.GetComponent<AutoCrafter>();
                    if (autoCrafter.connectionFailed == false)
                    {
                        GUI.DrawTexture(guiCoordinates.FourButtonSpeedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        if (autoCrafter.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            autoCrafter.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, autoCrafter.speed, 0, autoCrafter.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                        if (GUI.Button(guiCoordinates.outputControlButton3Rect, "Choose Item"))
                        {
                            if (obj.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.PlayButtonSound();
                            }
                        }
                        if (GUI.Button(guiCoordinates.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            autoCrafter.connectionAttempts = 0;
                            autoCrafter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (autoCrafter.speed != playerController.networkedConduitRange || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = autoCrafter.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendConduitData(location,autoCrafter.speed));
                            playerController.networkedConduitRange = autoCrafter.speed;
                        }
                    }
                }

                if (obj.GetComponent<UniversalConduit>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    UniversalConduit conduit = obj.GetComponent<UniversalConduit>();
                    if (conduit.connectionFailed == false)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Range");
                        conduit.range = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, conduit.range, 6, 120);
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (conduit.range != playerController.networkedConduitRange || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = conduit.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendConduitData(location,conduit.range));
                            playerController.networkedConduitRange = conduit.range;
                        }
                    }
                }

                if (obj.GetComponent<DarkMatterConduit>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    DarkMatterConduit conduit = obj.GetComponent<DarkMatterConduit>();
                    if (conduit.connectionFailed == false)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Range");
                        conduit.range = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, conduit.range, 6, 120);
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (conduit.range != playerController.networkedConduitRange || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = conduit.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendConduitData(location,conduit.range));
                            playerController.networkedConduitRange = conduit.range;
                        }
                    }
                }

                if (obj.GetComponent<HeatExchanger>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    HeatExchanger hx = obj.GetComponent<HeatExchanger>();
                    if (hx.inputObject != null)
                    {
                        if (hx.inputObject.GetComponent<UniversalConduit>() != null)
                        {
                            if (hx.connectionFailed == false)
                            {
                                GUI.Label(guiCoordinates.outputLabelRect, "Output");
                                hx.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, hx.speed, 0, playerController.hxAmount);
                            }
                            else
                            {
                                GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                                if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                                {
                                    netFlag = true;
                                    hx.connectionAttempts = 0;
                                    hx.connectionFailed = false;
                                    playerController.PlayButtonSound();
                                }
                            }
                            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                            {
                                if (hx.speed != playerController.networkedMachineSpeed || netFlag == true)
                                {
                                    NetworkSend net = playerController.networkController.networkSend;
                                    Vector3 location = hx.gameObject.transform.position;
                                    Timing.RunCoroutine(net.SendMachineData(location,hx.speed));
                                    playerController.networkedMachineSpeed = hx.speed;
                                }
                            }
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Input");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "No Input");
                    }
                }

                if (obj.GetComponent<PowerSource>() != null)
                {
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    PowerSource powerSource = obj.GetComponent<PowerSource>();
                    if (powerSource.connectionFailed == true)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            powerSource.connectionAttempts = 0;
                            powerSource.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Online");
                    }
                }

                if (obj.GetComponent<Auger>() != null)
                {
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    Auger auger = obj.GetComponent<Auger>();
                    if (auger.power > 0)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Output");
                        auger.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, auger.speed, 0, auger.power);
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (auger.speed != playerController.networkedMachineSpeed)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = auger.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,auger.speed));
                            playerController.networkedMachineSpeed = auger.speed;
                        }
                    }
                }

                if (obj.GetComponent<UniversalExtractor>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    UniversalExtractor extractor = obj.GetComponent<UniversalExtractor>();
                    if (extractor.connectionFailed == false)
                    {
                        if (extractor.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            extractor.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, extractor.speed, 0, extractor.power);
                        }
                        else
                        {
                            GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            extractor.connectionAttempts = 0;
                            extractor.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (extractor.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = extractor.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,extractor.speed));
                            playerController.networkedMachineSpeed = extractor.speed;
                        }
                    }
                }
                if (obj.GetComponent<DarkMatterCollector>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    DarkMatterCollector collector = obj.GetComponent<DarkMatterCollector>();
                    if (collector.connectionFailed == false)
                    {
                        if (collector.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            collector.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, collector.speed, 0, collector.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            collector.connectionAttempts = 0;
                            collector.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (collector.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = collector.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,collector.speed));
                            playerController.networkedMachineSpeed = collector.speed;
                        }
                    }
                }

                if (obj.GetComponent<Smelter>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    Smelter smelter = obj.GetComponent<Smelter>();
                    if (smelter.connectionFailed == false)
                    {

                        if (smelter.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            smelter.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, smelter.speed, 0, smelter.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            smelter.connectionAttempts = 0;
                            smelter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (smelter.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = smelter.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,smelter.speed));
                            playerController.networkedMachineSpeed = smelter.speed;
                        }
                    }
                }

                if (obj.GetComponent<AlloySmelter>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    AlloySmelter alloySmelter = obj.GetComponent<AlloySmelter>();
                    if (alloySmelter.connectionFailed == false)
                    {
                        if (alloySmelter.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            alloySmelter.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, alloySmelter.speed, 0, alloySmelter.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            alloySmelter.connectionAttempts = 0;
                            alloySmelter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (alloySmelter.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = alloySmelter.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,alloySmelter.speed));
                            playerController.networkedMachineSpeed = alloySmelter.speed;
                        }
                    }
                }

                if (obj.GetComponent<Press>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    Press press = obj.GetComponent<Press>();
                    if (press.connectionFailed == false)
                    {
                        if (press.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            press.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, press.speed, 0, press.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            press.connectionAttempts = 0;
                            press.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (press.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = press.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,press.speed));
                            playerController.networkedMachineSpeed = press.speed;
                        }
                    }
                }

                if (obj.GetComponent<Extruder>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    Extruder extruder = obj.GetComponent<Extruder>();
                    if (extruder.connectionFailed == false)
                    {
                        if (extruder.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            extruder.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, extruder.speed, 0, extruder.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            extruder.connectionAttempts = 0;
                            extruder.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (extruder.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = extruder.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,extruder.speed));
                            playerController.networkedMachineSpeed = extruder.speed;
                        }
                    }
                }

                if (obj.GetComponent<ModMachine>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    ModMachine modMachine = obj.GetComponent<ModMachine>();
                    if (modMachine.connectionFailed == false)
                    {
                        if (modMachine.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            modMachine.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, modMachine.speed, 0, modMachine.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            modMachine.connectionAttempts = 0;
                            modMachine.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (modMachine.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = modMachine.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,modMachine.speed));
                            playerController.networkedMachineSpeed = modMachine.speed;
                        }
                    }
                }

                if (obj.GetComponent<Turret>() != null)
                {
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    Turret turret = obj.GetComponent<Turret>();
                    if (turret.power > 0)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Output");
                        if (turret.power < 30)
                        {
                            turret.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, turret.speed, 0, turret.power);
                        }
                        else
                        {
                            turret.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, turret.speed, 0, 30);
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (turret.speed != playerController.networkedMachineSpeed)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = turret.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,turret.speed));
                            playerController.networkedMachineSpeed = turret.speed;
                        }
                    }
                }

                if (obj.GetComponent<MissileTurret>() != null)
                {
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    MissileTurret turret = obj.GetComponent<MissileTurret>();
                    if (turret.power > 0)
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Output");
                        if (turret.power < 30)
                        {
                            turret.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, turret.speed, 0, turret.power);
                        }
                        else
                        {
                            turret.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, turret.speed, 0, 30);
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (turret.speed != playerController.networkedMachineSpeed)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = turret.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,turret.speed));
                            playerController.networkedMachineSpeed = turret.speed;
                        }
                    }
                }

                if (obj.GetComponent<GearCutter>() != null)
                {
                    bool netFlag = false;
                    GUI.DrawTexture(guiCoordinates.speedControlBGRect, textureDictionary.dictionary["Interface Background"]);
                    GearCutter gearCutter = obj.GetComponent<GearCutter>();
                    if (gearCutter.connectionFailed == false)
                    {
                        if (gearCutter.power > 0)
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "Output");
                            gearCutter.speed = (int)GUI.HorizontalSlider(guiCoordinates.outputControlButton2Rect, gearCutter.speed, 0, gearCutter.power);
                        }
                        else
                        {
                            GUI.Label(guiCoordinates.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(guiCoordinates.outputLabelRect, "Offline");
                        if (GUI.Button(guiCoordinates.outputControlButton2Rect, "Reboot"))
                        {
                            netFlag = true;
                            gearCutter.connectionAttempts = 0;
                            gearCutter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (gearCutter.speed != playerController.networkedMachineSpeed || netFlag == true)
                        {
                            NetworkSend net = playerController.networkController.networkSend;
                            Vector3 location = gearCutter.gameObject.transform.position;
                            Timing.RunCoroutine(net.SendMachineData(location,gearCutter.speed));
                            playerController.networkedMachineSpeed = gearCutter.speed;
                        }
                    }
                }
            }
            else
            {
                hubStopWindowOpen = false;
                gameObject.GetComponent<MSCameraController>().enabled = true;
            }
        }
    }
}