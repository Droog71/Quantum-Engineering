using UnityEngine;
using System.Collections.Generic;

public class MarketGUI : MonoBehaviour
{
    private PlayerController playerController;
    private GuiCoordinates guiCoordinates;
    private TextureDictionary textureDictionary;
    private Dictionary<string, int> priceDictionary;
    private Descriptions descriptions;
    private int marketPage;
    private bool selling;
    private bool loadedValues;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textureDictionary = gameManager.GetComponent<TextureDictionary>();
        guiCoordinates = new GuiCoordinates();
        descriptions = new Descriptions();
        priceDictionary = new Dictionary<string, int>
        {
            { "Iron Block", 10 },
            { "Iron Ramp", 10 },
            { "Steel Block", 10 },
            { "Steel Ramp", 10 },
            { "Glass Block", 10 },
            { "Brick", 10 },
            { "Electric Light", 50 },
            { "Dark Matter Collector", 5000 },
            { "Dark Matter Conduit", 4000 },
            { "Universal Conduit", 100 },
            { "Universal Extractor", 250 },
            { "Auger", 50 },
            { "Door", 50 },
            { "Quantum Hatchway", 50 },
            { "Storage Container", 50 },
            { "Smelter", 500 },
            { "Turret", 2500 },
            { "Missile Turret", 3500 },
            { "Missile", 500 },
            { "Solar Panel", 250 },
            { "Generator", 500 },
            { "Power Conduit", 100 },
            { "Nuclear Reactor", 2500 },
            { "Reactor Turbine", 2500 },
            { "Alloy Smelter", 2000 },
            { "Press", 50},
            { "Extruder", 50 },
            { "Retriever", 250 },
            { "Heat Exchanger", 1500 },
            { "Gear Cutter", 2000 },
            { "Auto Crafter", 3000 },
            { "Rail Cart", 1500 },
            { "Rail Cart Hub", 1000 },
            { "Storage Computer", 2500 },
            { "Circuit Board", 50 },
            { "Electric Motor", 50 }
        };
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!playerController.stateManager.Busy() == true && loadedValues == false)
        {
            Dictionary<string, int> pd = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> i in priceDictionary)
            {
                if (FileBasedPrefs.GetInt(i.Key) != 0)
                {
                    pd.Add(i.Key, FileBasedPrefs.GetInt(i.Key));
                }
                else
                {
                    pd.Add(i.Key, i.Value);
                }
            }
            priceDictionary = pd;
            loadedValues = true;
        }
    }

    //! Saves world specific item prices.
    private void SavePrices()
    {
        foreach (KeyValuePair<string, int> i in priceDictionary)
        {
            FileBasedPrefs.SetInt(i.Key, i.Value);
        }
    }

    //! Buy an item from the market.
    private void BuyItem(string item)
    {
        if (playerController.money >= priceDictionary[item])
        {
            playerController.playerInventory.AddItem(item, 1);
            if (playerController.playerInventory.itemAdded == true)
            {
                playerController.money -= priceDictionary[item];
                priceDictionary[item] += (int)(priceDictionary[item] * 0.025f);
                FileBasedPrefs.SetInt(playerController.stateManager.worldName + "money", playerController.money);
                SavePrices();
                playerController.PlayCraftingSound();
            }
            else
            {
                GetComponent<InventoryGUI>().outOfSpace = true;
                playerController.PlayMissingItemsSound();
            }
        }
        else
        {
            GetComponent<InventoryGUI>().cannotAfford = true;
            playerController.PlayMissingItemsSound();
        }
    }

    //! Sell an item to the market.
    private void SellItem(string item)
    {
        InventorySlot sellSlot = null;
        foreach (InventorySlot slot in playerController.playerInventory.inventory)
        {
            if (slot.amountInSlot >= 1)
            {
                if (slot.typeInSlot.Equals(item))
                {
                    sellSlot = slot;
                }
            }
        }
        if (sellSlot != null)
        {
            sellSlot.amountInSlot -= 1;
            if (sellSlot.amountInSlot <= 0)
            {
                sellSlot.typeInSlot = "nothing";
            }
            playerController.money += priceDictionary[item];
            priceDictionary[item] -= (int)(priceDictionary[item] * 0.025f);
            FileBasedPrefs.SetInt(playerController.stateManager.worldName + "money", playerController.money);
            SavePrices();
            playerController.PlayCraftingSound();
        }
        else
        {
            GetComponent<InventoryGUI>().missingItem = true;
            playerController.PlayMissingItemsSound();
        }
    }

    //! Called by unity engine for rendering and handling GUI events
    public void OnGUI()
    {
        // Style.
        GUI.skin = GetComponent<PlayerGUI>().thisGUIskin;

        // Aspect ratio.
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

        if (playerController.marketGUIopen == true)
        {
            float distance = Vector3.Distance(playerController.gameObject.transform.position, GameObject.Find("Rocket").transform.position);
            if (distance <= 40)
            {
                playerController.inventoryOpen = true;
                if (marketPage == 0)
                {
                    if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.storageContainer + "\n\n[WORTH]\n$" + priceDictionary["Storage Container"]);
                    }
                    if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.auger + "\n\n[WORTH]\n$" + priceDictionary["Auger"]);
                    }
                    if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.extruder + "\n\n[WORTH]\n$" + priceDictionary["Extruder"]);
                    }
                    if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.press + "\n\n[WORTH]\n$" + priceDictionary["Press"]);
                    }
                    if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.gearCutter + "\n\n[WORTH]\n$" + priceDictionary["Gear Cutter"]);
                    }
                    if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.universalExtractor + "\n\n[WORTH]\n$" + priceDictionary["Universal Extractor"]);
                    }
                    if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.universalConduit + "\n\n[WORTH]\n$" + priceDictionary["Universal Conduit"]);
                    }
                    if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.retriever + "\n\n[WORTH]\n$" + priceDictionary["Retriever"]);
                    }
                    if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.solarPanel + "\n\n[WORTH]\n$" + priceDictionary["Solar Panel"]);
                    }
                    if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.generator + "\n\n[WORTH]\n$" + priceDictionary["Generator"]);
                    }
                    if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.nuclearReactor + "\n\n[WORTH]\n$" + priceDictionary["Nuclear Reactor"]);
                    }
                    if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.reactorTurbine + "\n\n[WORTH]\n$" + priceDictionary["Reactor Turbine"]);
                    }
                    if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.powerConduit + "\n\n[WORTH]\n$" + priceDictionary["Power Conduit"]);
                    }
                    if (guiCoordinates.button15Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.heatExchanger + "\n\n[WORTH]\n$" + priceDictionary["Heat Exchanger"]);
                    }
                    if (guiCoordinates.button17Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.smelter + "\n\n[WORTH]\n$" + priceDictionary["Smelter"]);
                    }
                    if (guiCoordinates.button18Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.alloySmelter + "\n\n[WORTH]\n$" + priceDictionary["Alloy Smelter"]);
                    }
                    if (guiCoordinates.button19Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.darkMatterCollector + "\n\n[WORTH]\n$" + priceDictionary["Dark Matter Collector"]);
                    }
                    if (guiCoordinates.button20Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.darkMatterConduit + "\n\n[WORTH]\n$" + priceDictionary["Dark Matter Conduit"]);
                    }
                    if (guiCoordinates.button21Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.autoCrafter + "\n\n[WORTH]\n$" + priceDictionary["Auto Crafter"]);
                    }
                    if (guiCoordinates.button22Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.railCartHub + "\n\n[WORTH]\n$" + priceDictionary["Rail Cart Hub"]);
                    }
                    if (guiCoordinates.button23Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.railCart + "\n\n[WORTH]\n$" + priceDictionary["Rail Cart"]);
                    }

                    GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    int f = GUI.skin.label.fontSize;
                    GUI.skin.label.fontSize = 24;
                    GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                    GUI.Label(guiCoordinates.marketTitleRect, "MARKET");
                    GUI.skin.label.fontSize = f;
                    GUI.Label(guiCoordinates.marketMoneyRect, "$" + playerController.money);
                    GUI.color = Color.white;

                    if (GUI.Button(guiCoordinates.button1Rect, "Storage Container"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Storage Container");
                        }
                        else
                        {
                            SellItem("Storage Container");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button2Rect, "Auger"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Auger");
                        }
                        else
                        {
                            SellItem("Auger");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button3Rect, "Extruder"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Extruder");
                        }
                        else
                        {
                            SellItem("Extruder");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button4Rect, "Press"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Press");
                        }
                        else
                        {
                            SellItem("Press");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button5Rect, "Gear Cutter"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Gear Cutter");
                        }
                        else
                        {
                            SellItem("Gear Cutter");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button6Rect, "Universal Extractor"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Universal Extractor");
                        }
                        else
                        {
                            SellItem("Universal Extractor");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button7Rect, "Universal Conduit"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Universal Conduit");
                        }
                        else
                        {
                            SellItem("Universal Conduit");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button9Rect, "Retriever"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Retriever");
                        }
                        else
                        {
                            SellItem("Retriever");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button10Rect, "Solar Panel"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Solar Panel");
                        }
                        else
                        {
                            SellItem("Solar Panel");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button11Rect, "Generator"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Generator");
                        }
                        else
                        {
                            SellItem("Generator");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button12Rect, "Nuclear Reactor"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Nuclear Reactor");
                        }
                        else
                        {
                            SellItem("Nuclear Reactor");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button13Rect, "Reactor Turbine"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Reactor Turbine");
                        }
                        else
                        {
                            SellItem("Reactor Turbine");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button14Rect, "Power Conduit"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Power Conduit");
                        }
                        else
                        {
                            SellItem("Power Conduit");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button15Rect, "Heat Exchanger"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Heat Exchanger");
                        }
                        else
                        {
                            SellItem("Heat Exchanger");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button17Rect, "Smelter"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Smelter");
                        }
                        else
                        {
                            SellItem("Smelter");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button18Rect, "Alloy Smelter"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Alloy Smelter");
                        }
                        else
                        {
                            SellItem("Alloy Smelter");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button19Rect, "DM Collector"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Dark Matter Collector");
                        }
                        else
                        {
                            SellItem("Dark Matter Collector");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button20Rect, "DM Conduit"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Dark Matter Conduit");
                        }
                        else
                        {
                            SellItem("Dark Matter Conduit");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button21Rect, "Auto Crafter"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Auto Crafter");
                        }
                        else
                        {
                            SellItem("Auto Crafter");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button22Rect, "Rail Cart Hub"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Rail Cart Hub");
                        }
                        else
                        {
                            SellItem("Rail Cart Hub");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button23Rect, "Rail Cart"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Rail Cart");
                        }
                        else
                        {
                            SellItem("Rail Cart");
                        }
                    }
                }
                if (marketPage == 1)
                {
                    if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.ironBlock + "\n\n[WORTH]\n$" + priceDictionary["Iron Block"]);
                    }
                    if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.ironRamp + "\n\n[WORTH]\n$" + priceDictionary["Iron Ramp"]);
                    }
                    if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.steelBlock + "\n\n[WORTH]\n$" + priceDictionary["Steel Block"]);
                    }
                    if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.steelRamp + "\n\n[WORTH]\n$" + priceDictionary["Steel Ramp"]);
                    }
                    if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.door + "\n\n[WORTH]\n$" + priceDictionary["Door"]);
                    }
                    if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.quantumHatchway + "\n\n[WORTH]\n$" + priceDictionary["Quantum Hatchway"]);
                    }
                    if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.electricLight + "\n\n[WORTH]\n$" + priceDictionary["Electric Light"]);
                    }
                    if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.circuitBoard + "\n\n[WORTH]\n$" + priceDictionary["Circuit Board"]);
                    }
                    if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.electricMotor + "\n\n[WORTH]\n$" + priceDictionary["Electric Motor"]);
                    }
                    if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.storageComputer + "\n\n[WORTH]\n$" + priceDictionary["Storage Computer"]);
                    }
                    if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.turret + "\n\n[WORTH]\n$" + priceDictionary["Turret"]);
                    }
                    if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.missileTurret + "\n\n[WORTH]\n$" + priceDictionary["Missile Turret"]);
                    }
                    if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, descriptions.missile + "\n\n[WORTH]\n$" + priceDictionary["Missile"]);
                    }

                    GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    int f = GUI.skin.label.fontSize;
                    GUI.skin.label.fontSize = 24;
                    GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                    GUI.Label(guiCoordinates.marketTitleRect, "MARKET");
                    GUI.skin.label.fontSize = f;
                    GUI.Label(guiCoordinates.marketMoneyRect, "$" + playerController.money);
                    GUI.color = Color.white;

                    if (GUI.Button(guiCoordinates.button1Rect, "Iron Block"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Iron Block");
                        }
                        else
                        {
                            SellItem("Iron Block");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button2Rect, "Iron Ramp"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Iron Ramp");
                        }
                        else
                        {
                            SellItem("Iron Ramp");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button3Rect, "Steel Block"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Steel Block");
                        }
                        else
                        {
                            SellItem("Steel Block");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button4Rect, "Steel Ramp"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Steel Ramp");
                        }
                        else
                        {
                            SellItem("Steel Ramp");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button5Rect, "Door"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Door");
                        }
                        else
                        {
                            SellItem("Door");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button6Rect, "Quantum Hatchway"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Quantum Hatchway");
                        }
                        else
                        {
                            SellItem("Quantum Hatchway");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button7Rect, "Electric Light"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Electric Light");
                        }
                        else
                        {
                            SellItem("Electric Light");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button9Rect, "Circuit Board"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Circuit Board");
                        }
                        else
                        {
                            SellItem("Circuit Board");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button10Rect, "Electric Motor"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Electric Motor");
                        }
                        else
                        {
                            SellItem("Electric Motor");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button11Rect, "Storage Computer"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Storage Computer");
                        }
                        else
                        {
                            SellItem("Storage Computer");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button12Rect, "Turret"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Turret");
                        }
                        else
                        {
                            SellItem("Turret");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button13Rect, "Missile Turret"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Missile Turret");
                        }
                        else
                        {
                            SellItem("Missile Turret");
                        }
                    }
                    if (GUI.Button(guiCoordinates.button14Rect, "Missile"))
                    {
                        if (selling == false)
                        {
                            BuyItem("Missile");
                        }
                        else
                        {
                            SellItem("Missile");
                        }
                    }
                }
                if (GUI.Button(guiCoordinates.craftingPreviousRect, "<-"))
                {
                    if (marketPage > 0)
                    {
                        marketPage -= 1;
                    }
                    playerController.PlayButtonSound();
                }
                if (GUI.Button(guiCoordinates.craftingNextRect, "->"))
                {
                    if (marketPage < 1)
                    {
                        marketPage += 1;
                    }
                    playerController.PlayButtonSound();
                }

                string buyingOrSelling;
                if (selling == true)
                {
                    buyingOrSelling = "SELLING";
                }
                else
                {
                    buyingOrSelling = "BUYING";
                }

                if (GUI.Button(guiCoordinates.craftingButtonRect, buyingOrSelling))
                {
                    selling = !selling;
                    playerController.PlayButtonSound();
                }

                if (GUI.Button(guiCoordinates.closeButtonRect, "CLOSE"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.inventoryOpen = false;
                    playerController.marketGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }
            else if (GameObject.Find("Rocket").GetComponent<Rocket>().landed == true || GameObject.Find("Rocket").GetComponent<Rocket>().rocketRequested == true)
            {
                playerController.inventoryOpen = false;
                string message = "You need to be within 4 meters of the rocket to use the market.";
                GUIContent content = new GUIContent(message);
                GUIStyle style = GUI.skin.box;
                style.alignment = TextAnchor.MiddleCenter;
                Vector2 size = style.CalcSize(content);
                float backgroundWidth = size.x * 1.1f;
                Rect backgroundRect = new Rect((Screen.width / 2) - (backgroundWidth / 2), ((ScreenHeight / 2) - 100), backgroundWidth * 1.05f, 200);
                Rect messageRect = new Rect((Screen.width / 2) - (size.x / 3), ((ScreenHeight / 2) - 50), size.x, size.y);
                GUI.DrawTexture(backgroundRect, textureDictionary.dictionary["Interface Background"]);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 12;
                GUI.Label(messageRect, message);
                GUI.skin.label.fontSize = f;
                if (GUI.Button(guiCoordinates.marketMessageButtonRect, "OK"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.marketGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }
            else if (GameObject.Find("Rocket").GetComponent<Rocket>().landed == false && GameObject.Find("Rocket").GetComponent<Rocket>().rocketRequested == false)
            {
                playerController.inventoryOpen = false;
                string message = "You need to be within 4 meters of the rocket to use the market.";
                GUIContent content = new GUIContent(message);
                GUIStyle style = GUI.skin.box;
                style.alignment = TextAnchor.MiddleCenter;
                Vector2 size = style.CalcSize(content);
                float backgroundWidth = size.x * 1.1f;
                Rect backgroundRect = new Rect((Screen.width / 2) - (backgroundWidth / 2), ((ScreenHeight / 2) - 100), backgroundWidth * 1.05f, 200);
                Rect messageRect = new Rect((Screen.width / 2) - (size.x / 3), ((ScreenHeight / 2) - 50), size.x, size.y);
                GUI.DrawTexture(backgroundRect, textureDictionary.dictionary["Interface Background"]);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 12;
                GUI.Label(messageRect, message);
                GUI.skin.label.fontSize = f;
                if (GUI.Button(guiCoordinates.marketMessageButtonRect, "Request Rocket"))
                {
                    GameObject.Find("Rocket").GetComponent<Rocket>().rocketRequested = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.marketGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }
        }
    }
}