using UnityEngine;

public class MachineGUI : MonoBehaviour
{
    private PlayerController playerController;
    private bool hubStopWindowOpen;
    private TextureDictionary td;
    private GuiCoordinates gc;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        td = GetComponent<TextureDictionary>();
        gc = GetComponent<GuiCoordinates>();
    }

    void OnGUI()
    {
        //STYLE
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        if (playerController.stateManager.worldLoaded == true && GetComponent<MainMenu>().finishedLoading == true)
        {
            //MACHINE CONTROL GUI
            if (playerController.inventoryOpen == false && playerController.machineGUIopen == true && playerController.objectInSight != null)
            {
                GameObject obj = playerController.objectInSight;

                if (obj.GetComponent<PowerConduit>() != null)
                {
                    PowerConduit powerConduit = obj.GetComponent<PowerConduit>();
                    if (powerConduit.connectionFailed == false)
                    {
                        GUI.DrawTexture(gc.FourButtonSpeedControlBGRect, td.dictionary["Interface Background"]);
                        GUI.Label(gc.outputLabelRect, "Range");
                        powerConduit.range = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, powerConduit.range, 6, 120);
                        if (GUI.Button(gc.outputControlButton3Rect, "Dual Output: " + powerConduit.dualOutput))
                        {
                            if (powerConduit.dualOutput == true)
                            {
                                powerConduit.dualOutput = false;
                            }
                            else
                            {
                                powerConduit.dualOutput = true;
                            }
                            playerController.playButtonSound();
                        }
                        if (GUI.Button(gc.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(gc.speedControlBGRect, td.dictionary["Interface Background"]);
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            powerConduit.connectionAttempts = 0;
                            powerConduit.connectionFailed = false;
                            playerController.playButtonSound();
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
                            GUI.DrawTexture(gc.FiveButtonSpeedControlBGRect, td.dictionary["Interface Background"]);
                            GUI.Label(gc.railCartHubCircuitLabelRect, "Circuit");
                            int circuit = hub.circuit;
                            string circuitString = GUI.TextField(gc.railCartHubCircuitRect, circuit.ToString(), 3);
                            try
                            {
                                hub.circuit = int.Parse(circuitString);
                            }
                            catch
                            {
                                //NOOP
                            }
                            GUI.Label(gc.outputLabelRect, "Range");
                            hub.range = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, hub.range, 6, 120);
                            if (GUI.Button(gc.outputControlButton3Rect, "Stop Settings"))
                            {
                                hubStopWindowOpen = true;
                                playerController.playButtonSound();
                            }
                            if (GUI.Button(gc.outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                        }
                        else
                        {
                            GUI.DrawTexture(gc.FiveButtonSpeedControlBGRect, td.dictionary["Interface Background"]);
                            GUI.Label(gc.longOutputLabelRect, "Stop Time");
                            if (GUI.Button(gc.outputControlButton0Rect, "Stop: " + hub.stop))
                            {
                                if (hub.stop == true)
                                {
                                    hub.stop = false;
                                }
                                else
                                {
                                    hub.stop = true;
                                }
                                playerController.playButtonSound();
                            }
                            hub.stopTime = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, hub.stopTime, 0, 600);
                            if (GUI.Button(gc.outputControlButton3Rect, "Range Settings"))
                            {
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                            if (GUI.Button(gc.outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(gc.speedControlBGRect, td.dictionary["Interface Background"]);
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            hub.connectionAttempts = 0;
                            hub.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Retriever>() != null)
                {
                    Retriever retriever = obj.GetComponent<Retriever>();
                    if (retriever.connectionFailed == false)
                    {
                        GUI.DrawTexture(gc.FourButtonSpeedControlBGRect, td.dictionary["Interface Background"]);
                        if (retriever.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            retriever.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, retriever.speed, 0, retriever.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                        if (GUI.Button(gc.outputControlButton3Rect, "Choose Items"))
                        {
                            if (obj.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.playButtonSound();
                            }
                        }
                        if (GUI.Button(gc.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(gc.speedControlBGRect, td.dictionary["Interface Background"]);
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            retriever.connectionAttempts = 0;
                            retriever.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<AutoCrafter>() != null)
                {
                    AutoCrafter autoCrafter = obj.GetComponent<AutoCrafter>();
                    if (autoCrafter.connectionFailed == false)
                    {
                        GUI.DrawTexture(gc.FourButtonSpeedControlBGRect, td.dictionary["Interface Background"]);
                        if (autoCrafter.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            autoCrafter.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, autoCrafter.speed, 0, autoCrafter.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                        if (GUI.Button(gc.outputControlButton3Rect, "Choose Item"))
                        {
                            if (obj.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.playButtonSound();
                            }
                        }
                        if (GUI.Button(gc.outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(gc.speedControlBGRect, td.dictionary["Interface Background"]);
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            autoCrafter.connectionAttempts = 0;
                            autoCrafter.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<UniversalConduit>() != null)
                {
                    UniversalConduit conduit = obj.GetComponent<UniversalConduit>();
                    if (conduit.connectionFailed == false)
                    {
                        GUI.Label(gc.outputLabelRect, "Range");
                        conduit.range = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, conduit.range, 6, 120);
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<DarkMatterConduit>() != null)
                {
                    DarkMatterConduit conduit = obj.GetComponent<DarkMatterConduit>();
                    if (conduit.connectionFailed == false)
                    {
                        GUI.Label(gc.outputLabelRect, "Range");
                        conduit.range = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, conduit.range, 6, 120);
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            conduit.connectionAttempts = 0;
                            conduit.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<HeatExchanger>() != null)
                {
                    HeatExchanger hx = obj.GetComponent<HeatExchanger>();
                    if (hx.inputObject != null)
                    {
                        if (hx.inputObject.GetComponent<UniversalConduit>() != null)
                        {
                            if (hx.inputObject.GetComponent<UniversalConduit>().speed > 0)
                            {
                                if (hx.connectionFailed == false)
                                {
                                    GUI.Label(gc.outputLabelRect, "Output");
                                    hx.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, hx.speed, 0, hx.inputObject.GetComponent<UniversalConduit>().speed);
                                }
                                else
                                {
                                    GUI.Label(gc.outputLabelRect, "Offline");
                                    if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                                    {
                                        hx.connectionAttempts = 0;
                                        hx.connectionFailed = false;
                                        playerController.playButtonSound();
                                    }
                                }
                            }
                            else
                            {
                                //Debug.Log(hx.ID + " input speed is zero");
                                GUI.Label(gc.outputLabelRect, "No Input");
                            }
                        }
                        else
                        {
                            //Debug.Log(hx.ID + " input object is not recognized as a conduit");
                            GUI.Label(gc.outputLabelRect, "No Input");
                        }
                    }
                    else
                    {
                        //Debug.Log(hx.ID + " input object is null");
                        GUI.Label(gc.outputLabelRect, "No Input");
                    }
                }

                if (obj.GetComponent<PowerSource>() != null)
                {
                    PowerSource powerSource = obj.GetComponent<PowerSource>();
                    if (powerSource.connectionFailed == true)
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            powerSource.connectionAttempts = 0;
                            powerSource.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Online");
                    }
                }

                if (obj.GetComponent<Auger>() != null)
                {
                    Auger auger = obj.GetComponent<Auger>();
                    if (auger.power > 0)
                    {
                        GUI.Label(gc.outputLabelRect, "Output");
                        auger.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, auger.speed, 0, auger.power);
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "No Power");
                    }
                }

                if (obj.GetComponent<UniversalExtractor>() != null)
                {
                    UniversalExtractor extractor = obj.GetComponent<UniversalExtractor>();
                    if (extractor.connectionFailed == false)
                    {
                        if (extractor.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            extractor.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, extractor.speed, 0, extractor.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            extractor.connectionAttempts = 0;
                            extractor.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }
                if (obj.GetComponent<DarkMatterCollector>() != null)
                {
                    DarkMatterCollector collector = obj.GetComponent<DarkMatterCollector>();
                    if (collector.connectionFailed == false)
                    {
                        if (collector.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            collector.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, collector.speed, 0, collector.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            collector.connectionAttempts = 0;
                            collector.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Smelter>() != null)
                {
                    Smelter smelter = obj.GetComponent<Smelter>();
                    if (smelter.connectionFailed == false)
                    {
                        if (smelter.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            smelter.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, smelter.speed, 0, smelter.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            smelter.connectionAttempts = 0;
                            smelter.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<AlloySmelter>() != null)
                {
                    AlloySmelter alloySmelter = obj.GetComponent<AlloySmelter>();
                    if (alloySmelter.connectionFailed == false)
                    {
                        if (alloySmelter.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            alloySmelter.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, alloySmelter.speed, 0, alloySmelter.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            alloySmelter.connectionAttempts = 0;
                            alloySmelter.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Press>() != null)
                {
                    Press press = obj.GetComponent<Press>();
                    if (press.connectionFailed == false)
                    {
                        if (press.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            press.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, press.speed, 0, press.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            press.connectionAttempts = 0;
                            press.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Extruder>() != null)
                {
                    Extruder extruder = obj.GetComponent<Extruder>();
                    if (extruder.connectionFailed == false)
                    {
                        if (extruder.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            extruder.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, extruder.speed, 0, extruder.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            extruder.connectionAttempts = 0;
                            extruder.connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }

                if (obj.GetComponent<Turret>() != null)
                {
                    Turret turret = obj.GetComponent<Turret>();
                    if (turret.power > 0)
                    {
                        GUI.Label(gc.outputLabelRect, "Output");
                        if (turret.power < 30)
                        {
                            turret.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, turret.speed, 0, turret.power);
                        }
                        else
                        {
                            turret.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, turret.speed, 0, 30);
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "No Power");
                    }
                }

                if (obj.GetComponent<GearCutter>() != null)
                {
                    GearCutter gearCutter = obj.GetComponent<GearCutter>();
                    if (gearCutter.connectionFailed == false)
                    {
                        if (gearCutter.power > 0)
                        {
                            GUI.Label(gc.outputLabelRect, "Output");
                            gearCutter.speed = (int)GUI.HorizontalSlider(gc.outputControlButton2Rect, gearCutter.speed, 0, gearCutter.power);
                        }
                        else
                        {
                            GUI.Label(gc.outputLabelRect, "No Power");
                        }
                    }
                    else
                    {
                        GUI.Label(gc.outputLabelRect, "Offline");
                        if (GUI.Button(gc.outputControlButton2Rect, "Reboot"))
                        {
                            gearCutter.connectionAttempts = 0;
                            gearCutter.connectionFailed = false;
                            playerController.playButtonSound();
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
