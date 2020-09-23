using UnityEngine;

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
        textureDictionary = GetComponent<TextureDictionary>();
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
                            powerConduit.connectionAttempts = 0;
                            powerConduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<RailCartHub>() != null)
                {
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
                            hub.connectionAttempts = 0;
                            hub.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Retriever>() != null)
                {
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
                            retriever.connectionAttempts = 0;
                            retriever.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<AutoCrafter>() != null)
                {
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
                            autoCrafter.connectionAttempts = 0;
                            autoCrafter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<UniversalConduit>() != null)
                {
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
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<DarkMatterConduit>() != null)
                {
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
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<HeatExchanger>() != null)
                {
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
                                    hx.connectionAttempts = 0;
                                    hx.connectionFailed = false;
                                    playerController.PlayButtonSound();
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
                }

                if (obj.GetComponent<UniversalExtractor>() != null)
                {
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
                            extractor.connectionAttempts = 0;
                            extractor.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }
                if (obj.GetComponent<DarkMatterCollector>() != null)
                {
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
                            collector.connectionAttempts = 0;
                            collector.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Smelter>() != null)
                {
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
                            smelter.connectionAttempts = 0;
                            smelter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<AlloySmelter>() != null)
                {
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
                            alloySmelter.connectionAttempts = 0;
                            alloySmelter.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Press>() != null)
                {
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
                            press.connectionAttempts = 0;
                            press.connectionFailed = false;
                            playerController.PlayButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Extruder>() != null)
                {
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
                            extruder.connectionAttempts = 0;
                            extruder.connectionFailed = false;
                            playerController.PlayButtonSound();
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
                }

                if (obj.GetComponent<GearCutter>() != null)
                {
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
                            gearCutter.connectionAttempts = 0;
                            gearCutter.connectionFailed = false;
                            playerController.PlayButtonSound();
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