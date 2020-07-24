using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MachineGUI : MonoBehaviour
{
    private PlayerController playerController;
    private bool hubStopWindowOpen;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
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
                if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                {
                    if (playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed == false)
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().FourButtonSpeedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Dual Output: " + playerController.objectInSight.GetComponent<PowerConduit>().dualOutput))
                        {
                            if (playerController.objectInSight.GetComponent<PowerConduit>().dualOutput == true)
                            {
                                playerController.objectInSight.GetComponent<PowerConduit>().dualOutput = false;
                            }
                            else
                            {
                                playerController.objectInSight.GetComponent<PowerConduit>().dualOutput = true;
                            }
                            playerController.playButtonSound();
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().speedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<PowerConduit>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<RailCartHub>() != null)
                {
                    if (playerController.objectInSight.GetComponent<RailCartHub>().connectionFailed == false)
                    {
                        if (hubStopWindowOpen == false)
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().FourButtonSpeedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<RailCartHub>().range = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<RailCartHub>().range, 6, 120);
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Stop Settings"))
                            {
                                hubStopWindowOpen = true;
                                playerController.playButtonSound();
                            }
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                        }
                        else
                        {
                            GUI.DrawTexture(GetComponent<GuiCoordinates>().FiveButtonSpeedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                            GUI.Label(GetComponent<GuiCoordinates>().longOutputLabelRect, "Stop Time");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton0Rect, "Stop: " + playerController.objectInSight.GetComponent<RailCartHub>().stop))
                            {
                                if (playerController.objectInSight.GetComponent<RailCartHub>().stop == true)
                                {
                                    playerController.objectInSight.GetComponent<RailCartHub>().stop = false;
                                }
                                else
                                {
                                    playerController.objectInSight.GetComponent<RailCartHub>().stop = true;
                                }
                                playerController.playButtonSound();
                            }
                            playerController.objectInSight.GetComponent<RailCartHub>().stopTime = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<RailCartHub>().stopTime, 0, 600);
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Range Settings"))
                            {
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().speedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<RailCartHub>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<RailCartHub>().connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Retriever>() != null)
                {
                    if (playerController.objectInSight.GetComponent<Retriever>().connectionFailed == false)
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().FourButtonSpeedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Choose Items"))
                        {
                            if (playerController.objectInSight.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.playButtonSound();
                            }
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().speedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<Retriever>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<Retriever>().connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                {
                    if (playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed == false)
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().FourButtonSpeedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Choose Item"))
                        {
                            if (playerController.objectInSight.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.playButtonSound();
                            }
                        }
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.playButtonSound();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(GetComponent<GuiCoordinates>().speedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<AutoCrafter>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed = false;
                            playerController.playButtonSound();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<RailCart>() == null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().speedControlBGRect, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton3Rect, "Close"))
                    {
                        playerController.machineGUIopen = false;
                        playerController.playButtonSound();
                    }
                }

                if (playerController.objectInSight.GetComponent<UniversalConduit>() != null || playerController.objectInSight.GetComponent<DarkMatterConduit>() != null || playerController.objectInSight.GetComponent<PowerConduit>() != null)
                {
                    if (playerController.objectInSight.GetComponent<UniversalConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<UniversalConduit>().connectionFailed == false)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<UniversalConduit>().range = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<UniversalConduit>().range, 6, 120);
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<UniversalConduit>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<UniversalConduit>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed == false)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<PowerConduit>().range = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<PowerConduit>().range, 6, 120);
                        }
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionFailed == false)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<DarkMatterConduit>().range = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<DarkMatterConduit>().range, 6, 120);
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                }
                else
                {
                    if (playerController.objectInSight.GetComponent<HeatExchanger>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject != null)
                        {
                            if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>() != null)
                            {
                                if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().speed > 0)
                                {
                                    if (playerController.objectInSight.GetComponent<HeatExchanger>().connectionFailed == false)
                                    {
                                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                        playerController.objectInSight.GetComponent<HeatExchanger>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<HeatExchanger>().speed, 0, playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().speed);
                                    }
                                    else
                                    {
                                        GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                                        if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                                        {
                                            playerController.objectInSight.GetComponent<HeatExchanger>().connectionAttempts = 0;
                                            playerController.objectInSight.GetComponent<HeatExchanger>().connectionFailed = false;
                                            playerController.playButtonSound();
                                        }
                                    }
                                }
                                else
                                {
                                    //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input speed is zero");
                                    GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Input");
                                }
                            }
                            else
                            {
                                //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input object is not recognized as a conduit");
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Input");
                            }
                        }
                        else
                        {
                            //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input object is null");
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Input");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<PowerSource>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == true)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<PowerSource>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<PowerSource>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Online");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Auger>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Auger>().power > 0)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                            playerController.objectInSight.GetComponent<Auger>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Auger>().speed, 0, playerController.objectInSight.GetComponent<Auger>().power);
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<UniversalExtractor>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<UniversalExtractor>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<UniversalExtractor>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<UniversalExtractor>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<UniversalExtractor>().speed, 0, playerController.objectInSight.GetComponent<UniversalExtractor>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<UniversalExtractor>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<UniversalExtractor>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterCollector>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<DarkMatterCollector>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<DarkMatterCollector>().speed, 0, playerController.objectInSight.GetComponent<DarkMatterCollector>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Smelter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Smelter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Smelter>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Smelter>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Smelter>().speed, 0, playerController.objectInSight.GetComponent<Smelter>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Smelter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Smelter>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<AlloySmelter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<AlloySmelter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<AlloySmelter>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<AlloySmelter>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<AlloySmelter>().speed, 0, playerController.objectInSight.GetComponent<AlloySmelter>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<AlloySmelter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<AlloySmelter>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Press>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Press>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Press>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Press>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Press>().speed, 0, playerController.objectInSight.GetComponent<Press>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Press>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Press>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Extruder>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Extruder>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Extruder>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Extruder>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Extruder>().speed, 0, playerController.objectInSight.GetComponent<Extruder>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Extruder>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Extruder>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Retriever>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Retriever>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Retriever>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Retriever>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Retriever>().speed, 0, playerController.objectInSight.GetComponent<Retriever>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Retriever>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Retriever>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<AutoCrafter>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<AutoCrafter>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<AutoCrafter>().speed, 0, playerController.objectInSight.GetComponent<AutoCrafter>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<AutoCrafter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Turret>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Turret>().power > 0)
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                            if (playerController.objectInSight.GetComponent<Turret>().power < 30)
                            {
                                playerController.objectInSight.GetComponent<Turret>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Turret>().speed, 0, playerController.objectInSight.GetComponent<Turret>().power);
                            }
                            else
                            {
                                playerController.objectInSight.GetComponent<Turret>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<Turret>().speed, 0, 30);
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<GearCutter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<GearCutter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<GearCutter>().power > 0)
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<GearCutter>().speed = (int)GUI.HorizontalSlider(GetComponent<GuiCoordinates>().outputControlButton2Rect, playerController.objectInSight.GetComponent<GearCutter>().speed, 0, playerController.objectInSight.GetComponent<GearCutter>().power);
                            }
                            else
                            {
                                GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(GetComponent<GuiCoordinates>().outputLabelRect, "Offline");
                            if (GUI.Button(GetComponent<GuiCoordinates>().outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<GearCutter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<GearCutter>().connectionFailed = false;
                                playerController.playButtonSound();
                            }
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
