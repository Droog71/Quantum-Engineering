using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfoHUD : MonoBehaviour
{
    private PlayerController playerController;
    private string machineDisplayID = "unassigned";
    private string machineDisplayOutputID = "unassigned";
    private string machineDisplayOutputID2 = "unassigned";
    private string machineDisplayInputID = "unassigned";
    private string machineDisplayInputID2 = "unassigned";

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private bool ShouldDrawInfoHud()
    {
        return playerController.stateManager.worldLoaded == true
        && GetComponent<MainMenu>().finishedLoading == true
        && playerController.objectInSight != null
        && playerController.building == false
        && playerController.inventoryOpen == false
        && playerController.escapeMenuOpen == false
        && playerController.tabletOpen == false;
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

        if (ShouldDrawInfoHud())
        {
            GameObject obj = playerController.objectInSight;

            if (playerController.machineID.Equals("Lander") || playerController.machineID.Equals("Rocket"))
            {
                machineDisplayID = playerController.machineID;
            }
            else if (playerController.machineID.Length > playerController.stateManager.WorldName.Length)
            {
                machineDisplayID = playerController.machineID.Substring(playerController.stateManager.WorldName.Length);
            }
            else
            {
                machineDisplayID = "unassigned";
            }

            if (playerController.machineInputID.Equals("Lander") || playerController.machineInputID.Equals("Rocket"))
            {
                machineDisplayInputID = playerController.machineInputID;
            }
            else if (playerController.machineInputID.Length > playerController.stateManager.WorldName.Length)
            {
                machineDisplayInputID = playerController.machineInputID.Substring(playerController.stateManager.WorldName.Length);
            }
            else
            {
                machineDisplayInputID = "unassigned";
            }

            if (playerController.machineInputID2.Equals("Lander") || playerController.machineInputID2.Equals("Rocket") || playerController.machineInputID2.Equals("unassigned"))
            {
                machineDisplayInputID2 = playerController.machineInputID2;
            }
            else if (playerController.machineInputID2.Length > playerController.stateManager.WorldName.Length)
            {
                machineDisplayInputID2 = playerController.machineInputID2.Substring(playerController.stateManager.WorldName.Length);
            }
            else
            {
                machineDisplayInputID2 = "unassigned";
            }

            if (playerController.machineOutputID.Equals("Lander") || playerController.machineOutputID.Equals("Rocket"))
            {
                machineDisplayOutputID = playerController.machineOutputID;
            }
            else if (playerController.machineOutputID.Length > playerController.stateManager.WorldName.Length)
            {
                machineDisplayOutputID = playerController.machineOutputID.Substring(playerController.stateManager.WorldName.Length);
            }
            else
            {
                machineDisplayOutputID = "unassigned";
            }

            if (playerController.machineOutputID2.Equals("Lander") || playerController.machineOutputID2.Equals("Rocket"))
            {
                machineDisplayOutputID2 = playerController.machineOutputID2;
            }
            else if (playerController.machineOutputID2.Length > playerController.stateManager.WorldName.Length)
            {
                machineDisplayOutputID2 = playerController.machineOutputID2.Substring(playerController.stateManager.WorldName.Length);
            }
            else
            {
                machineDisplayOutputID2 = "unassigned";
            }

            if (obj.GetComponent<InventoryManager>() != null && obj.GetComponent<AutoCrafter>() == null && obj.GetComponent<Retriever>() == null && obj != gameObject)
            {
                if (obj.GetComponent<RailCart>() != null)
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Rail Cart" + "\nPress E to interact." + "\nPress F to Collect.");
                }
                else if (obj.GetComponent<InventoryManager>().ID.Equals("Lander"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Lunar Lander" + "\nPress E to interact.");
                }
                else if (obj.GetComponent<InventoryManager>().ID.Equals("Rocket"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Rocket" + "\nPress E to interact.");
                }
                else
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Storage Container" + "\nPress E to open." + "\nPress F to Collect.");
                }
            }
            else if (obj.GetComponent<DarkMatter>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Dark Matter");
            }
            else if (obj.GetComponent<UniversalResource>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, obj.GetComponent<UniversalResource>().type);
            }
            else if (obj.GetComponent<IronBlock>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Iron Block");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
            }
            else if (obj.GetComponent<Steel>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Steel Block");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
            }
            else if (obj.GetComponent<Glass>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Glass Block");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
            }
            else if (obj.GetComponent<Brick>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Brick Block");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
            }
            else if (obj.GetComponent<ElectricLight>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Electric Light" + "\nPress F to Collect.");
            }
            else if (obj.GetComponent<AirLock>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Quantum Hatchway" + "\nPress E to interact." + "\nPress F to Collect.");
            }
            else if (obj.GetComponent<StorageComputer>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Storage Computer" + "\nPress E to interact." + "\nPress F to Collect.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                if (obj.GetComponent<StorageComputer>().initialized == true)
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Storage Computer" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower);
                }
                else
                {
                    if (obj.GetComponent<StorageComputer>().bootTimer > 0)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Storage Computer" + "\nBooting up...");
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Storage Computer" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<RailCartHub>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<RailCartHub>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Rail Cart Hub" + 
                        "\nID: " + machineDisplayID + 
                        "\nCircuit: " + obj.GetComponent<RailCartHub>().circuit + 
                        "\nRange: " + playerController.machineRange / 10 + " meters" + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Stop: " + obj.GetComponent<RailCartHub>().stop + 
                        "\n" + "Stop Duration: " + obj.GetComponent<RailCartHub>().stopTime + " seconds");
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Rail Cart Hub" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<DarkMatterConduit>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<DarkMatterConduit>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Dark Matter Conduit" + 
                        "\nID: " + machineDisplayID + 
                        "\nRange: " + playerController.machineRange / 10 + " meters" + 
                        "\nHolding: " + (int)playerController.machineAmount + " Dark Matter" + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Dark Matter Conduit" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<UniversalConduit>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<UniversalConduit>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Universal Conduit" + 
                        "\nID: " + machineDisplayID + 
                        "\nRange: " + playerController.machineRange / 10 + " meters" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Universal Conduit" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<PowerSource>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                if (obj.GetComponent<PowerSource>().type == "Solar Panel")
                {
                    if (obj.GetComponent<PowerSource>().connectionFailed == false && obj.GetComponent<PowerSource>().blocked == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Solar Panel" + 
                        "\nID: " + machineDisplayID + 
                        "\nOutput ID: " + machineDisplayOutputID + 
                        "\nPower: " + playerController.machinePower + " MW");
                    }
                    else if (obj.GetComponent<PowerSource>().connectionFailed == true)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Solar Panel" + "\nOffline");
                    }
                    else if (obj.GetComponent<PowerSource>().blocked == true)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Solar Panel" + "\nBlocked");
                    }
                }
                else if (obj.GetComponent<PowerSource>().type == "Generator")
                {
                    if (obj.GetComponent<PowerSource>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Generator" + 
                        "\nID: " + machineDisplayID + 
                        "\nOutput ID: " + machineDisplayOutputID + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nFuel: " + playerController.machineAmount + " " + playerController.machineType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Generator" + "\nOffline");
                    }
                }
                else if (obj.GetComponent<PowerSource>().type == "Reactor Turbine")
                {
                    if (obj.GetComponent<PowerSource>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Reactor Turbine" + 
                        "\nID: " + machineDisplayID + 
                        "\nOutput ID: " + machineDisplayOutputID + 
                        "\nPower: " + playerController.machinePower + " MW");
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Reactor Turbine" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<NuclearReactor>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Nuclear Reactor" + 
                    "\nID: " + machineDisplayID + 
                    "\nCooling: " + playerController.machineCooling + " KBTU" + 
                    "\nRequired Cooling: " + obj.GetComponent<NuclearReactor>().turbineCount * 5 + " KBTU");
                }
            }
            else if (obj.GetComponent<PowerConduit>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                if (obj.GetComponent<PowerConduit>().connectionFailed == false)
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Power Conduit" +
                    "\nID: " + machineDisplayID + 
                    "\nRange: " + playerController.machineRange / 10 + " meters" + 
                    "\nPower: " + playerController.machinePower + " MW" + 
                    "\nOutput 1: " + machineDisplayOutputID + 
                    "\nOutput 2: " + machineDisplayOutputID2);
                }
                else
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Power Conduit" + "\nOffline");
                }
            }
            else if (obj.GetComponent<UniversalExtractor>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                if (obj.GetComponent<UniversalExtractor>().connectionFailed == false)
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Universal Extractor" + 
                    "\nID: " + machineDisplayID + 
                    "\nEnergized: " + playerController.machineHasPower + 
                    "\nPower: " + playerController.machinePower + " MW" + 
                    "\nOutput: " + playerController.machineSpeed + " IPC" + 
                    "\nHeat: " + playerController.machineHeat + " KBTU" + 
                    "\nCooling: " + playerController.machineCooling + " KBTU" + 
                    "\nHolding: " + (int)playerController.collectorAmount + " " + playerController.machineType);
                }
                else
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Universal Extractor" + "\nOffline");
                }
            }
            else if (obj.GetComponent<Auger>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Auger" + 
                "\nID: " + machineDisplayID + 
                "\nEnergized: " + playerController.machineHasPower + 
                "\nPower: " + playerController.machinePower + " MW" + 
                "\nOutput: " + playerController.machineSpeed + " IPC" +
                "\nHeat: " + playerController.machineHeat + " KBTU" + 
                "\nCooling: " + playerController.machineCooling + " KBTU" + 
                "\nHolding: " + (int)playerController.collectorAmount + " Regolith");
            }
            else if (obj.GetComponent<DarkMatterCollector>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                if (obj.GetComponent<DarkMatterCollector>().connectionFailed == false)
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Dark Matter Collector" + 
                    "\nID: " + machineDisplayID + 
                    "\nEnergized: " + playerController.machineHasPower + 
                    "\nPower: " + playerController.machinePower + " MW" + 
                    "\nOutput: " + playerController.machineSpeed + " IPC" + 
                    "\nHeat: " + playerController.machineHeat + " KBTU" + 
                    "\nCooling: " + playerController.machineCooling + " KBTU" + 
                    "\nHolding: " + (int)playerController.collectorAmount + " Dark Matter");
                }
                else
                {
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Dark Matter Collector" + "\nOffline");
                }
            }
            else if (obj.GetComponent<Smelter>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<Smelter>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Smelter" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Smelter" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<AlloySmelter>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<AlloySmelter>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Alloy Smelter" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" +
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\nHolding: " + (int)playerController.machineAmount2 + " " + playerController.machineType2 + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Input ID 2: " + machineDisplayInputID2 + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Input 2 Holding: " + (int)playerController.machineInputAmount2 + " " + playerController.machineInputType2 + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Alloy Smelter" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<Press>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<Press>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Press" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Press" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<Extruder>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<Extruder>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Extruder" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Extruder" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<Retriever>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<Retriever>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Retriever" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" +
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nRetrieving: " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Retriever" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<AutoCrafter>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<AutoCrafter>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Auto Crafter" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nCrafting: " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayInputID);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Auto Crafter" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<GearCutter>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<GearCutter>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Gear Cutter" + 
                        "\nID: " + machineDisplayID + 
                        "\nEnergized: " + playerController.machineHasPower + 
                        "\nPower: " + playerController.machinePower + " MW" + 
                        "\nOutput: " + playerController.machineSpeed + " IPC" + 
                        "\nHeat: " + playerController.machineHeat + " KBTU" + 
                        "\nCooling: " + playerController.machineCooling + " KBTU" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID +
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + 
                        "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Gear Cutter" + "\nOffline");
                    }
                }
            }
            else if (obj.GetComponent<Turret>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    int rpm = (int)(60 / (0.2f + (3 - (playerController.machineSpeed * 0.1f))));
                    GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Turret" + 
                    "\nID: " + machineDisplayID + 
                    "\nEnergized: " + playerController.machineHasPower + 
                    "\nPower: " + playerController.machinePower + " MW" +
                    "\nOutput: " + rpm + " RPM" + 
                    "\nHeat: " + playerController.machineHeat + " KBTU" + 
                    "\nCooling: " + playerController.machineCooling);
                }
            }
            else if (obj.GetComponent<HeatExchanger>() != null)
            {
                GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Press F to collect.\nPress E to interact.");
                if (playerController.machineInSight != null)
                {
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().infoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    if (obj.GetComponent<HeatExchanger>().connectionFailed == false)
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Heat Exchanger" + 
                        "\nID: " + machineDisplayID + 
                        "\nCooling: " + obj.GetComponent<HeatExchanger>().providingCooling + 
                        "\nOutput: " + playerController.machineSpeed + " KBTU" + 
                        "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + 
                        "\n" + "Input ID: " + machineDisplayInputID + 
                        "\n" + "Output ID: " + machineDisplayOutputID + 
                        "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType);
                    }
                    else
                    {
                        GUI.Label(GetComponent<GuiCoordinates>().infoRect, "Heat Exchanger" + "\nOffline");
                    }
                }
            }
            else if (playerController.lookingAtCombinedMesh == true && playerController.paintGunActive == false)
            {
                if (obj.name.Equals("ironHolder(Clone)"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Iron Structure");
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                if (obj.name.Equals("glassHolder(Clone)"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Glass Structure");
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                if (obj.name.Equals("steelHolder(Clone)"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Steel Structure");
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                if (obj.name.Equals("brickHolder(Clone)"))
                {
                    GUI.Label(GetComponent<GuiCoordinates>().messageRect, "Brick Structure");
                    GUI.DrawTexture(GetComponent<GuiCoordinates>().buildInfoRectBG, GetComponent<TextureDictionary>().dictionary["Interface Background"]);
                    GUI.Label(GetComponent<GuiCoordinates>().buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
            }
        }
    }
}
