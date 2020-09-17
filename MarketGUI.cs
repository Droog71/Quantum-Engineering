using UnityEngine;
using System.Collections.Generic;

public class MarketGUI : MonoBehaviour
{
    private PlayerController playerController;
    private GuiCoordinates guiCoordinates;
    private TextureDictionary textureDictionary;
    private int marketPage;
    private Dictionary<string, int> priceDictionary;
    private bool selling;
    private bool loadedValues;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        textureDictionary = GetComponent<TextureDictionary>();
        guiCoordinates = new GuiCoordinates();
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
            { "Quantum Hatchway", 50 },
            { "Storage Container", 50 },
            { "Smelter", 500 },
            { "Turret", 2500 },
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
        if (playerController.stateManager.worldLoaded == true && loadedValues == false)
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
                FileBasedPrefs.SetInt(playerController.stateManager.WorldName + "money", playerController.money);
                SavePrices();
                playerController.PlayCraftingSound();
            }
            else
            {
                playerController.PlayMissingItemsSound();
            }
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
            FileBasedPrefs.SetInt(playerController.stateManager.WorldName + "money", playerController.money);
            SavePrices();
            playerController.PlayCraftingSound();
        }
        else
        {
            playerController.PlayMissingItemsSound();
        }
    }

    //! Called by unity engine for rendering and handling GUI events
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

        if (playerController.marketGUIopen == true)
        {
            float distance = Vector3.Distance(playerController.gameObject.transform.position, GameObject.Find("Rocket").transform.position);
            if (distance <= 40)
            {
                if (marketPage == 0)
                {
                    if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Storage container for objects and items. Can be used to manually store items or connected to machines for automation. Universal conduits, dark matter conduits, retrievers and auto crafters can all connect to storage containers.\n\n[WORTH]\n$" + priceDictionary["Storage Container"]);
                    }
                    if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Extracts regolith from the lunar surface which can be pressed into bricks or smelted to create glass. Glass blocks have a 100% chance of being destroyed by meteors and other hazards. Bricks have a 75% chance of being destroyed by meteors and other hazards. Augers must be placed directly on the lunar surface and require power from a solar panel, nuclear reactor or power conduit.\n\n[WORTH]\n$" + priceDictionary["Auger"]);
                    }
                    if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Creates wire from copper and aluminum ingots. Creates pipes from iron and steel ingots. Ingots must be supplied to the extruder using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. The extruder requires power from a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Extruder"]);
                    }
                    if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Presses iron, copper, aluminum or tin ingots into plates. Ingots must be supplied to the press using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. Must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Press"]);
                    }
                    if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Cuts plates into gears. Plates must be supplied to the gear cutter using universal conduits. Place another conduit within 2 meters of the machine for the output. The gear cutter must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The gear cutter has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Gear Cutter"]);
                    }
                    if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Extracts ore, coal and ice from deposits found on the lunar surface. Place within 2 meters of the desired resource and use a universal conduit to handle the harvested materials. When extracting ice, the extractor will not need a heat exchanger for cooling. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Universal Extractor"]);
                    }
                    if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Transfers items from a machine to another universal conduit, another machine or a storage container. Universal conduits have an adjustable input/output range and do not require power to operate.\n\n[WORTH]\n$" + priceDictionary["Universal Conduit"]);
                    }
                    if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Retrieves items from a storage container and transfers them to a universal conduit. Place an item of each desired type into the retrievers inventory to designate that item for retrieval. Place within 2 meters of a storage container and a universal conduit. This machine requires power and it's output is adjustable. If the retriever is moving ice, it will not require cooling. The retriever's output is measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Retriever"]);
                    }
                    if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Provides 1 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple solar panels can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[WORTH]\n$" + priceDictionary["Solar Panel"]);
                    }
                    if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Provides 10 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple generators can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate. Generators must be connected to a universal conduit supplying coal for fuel.\n\n[WORTH]\n$4x Iron Plate\n" + priceDictionary["Generator"]);
                    }
                    if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Nuclear reactors are used to drive reactor turbines. Turbines must be directly attached to the reactor. The reactor will require a heat exchanger providing 5 KBTU cooling per turbine.\n\n[WORTH]\n$" + priceDictionary["Nuclear Reactor"]);
                    }
                    if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Provides 100 MW of power to a single machine or power conduit. Reactor turbines must be directly attached to a properly functioning, adequately cooled nuclear reactor. Must be placed within 4 meters of the machine. Multiple reactor turbines can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[WORTH]\n$" + priceDictionary["Reactor Turbine"]);
                    }
                    if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Transfers power from a power source to a machine or to another power conduit. When used with two outputs, power will be distributed evenly. This machine has an adjustable range setting.\n\n[WORTH]\n$" + priceDictionary["Power Conduit"]);
                    }
                    if (guiCoordinates.button15Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Cools down a machine to allow overclocking. Requires a supply of ice from a universal conduit. Increasing the output of the heat exchanger increases the amount of ice required. This can be compensated for by overclocking the extractor that is supplying the ice. Machines cannot be connected to more than one heat exchanger. The heat exchanger's output is measured in KBTU and will consume 1 ice per 1 KBTU of cooling each cycle.\n\n[WORTH]\n$" + priceDictionary["Heat Exchanger"]);
                    }
                    if (guiCoordinates.button17Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Smelts ore into ingots. Can also be used to make glass when supplied with regolith. Ore must be supplied to the smelter using universal conduits. Place another conduit within 2 meters of the machine for the output. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The output of a smelter is measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Smelter"]);
                    }
                    if (guiCoordinates.button18Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Combines tin and copper ingots to make bronze ingots. Combines coal and iron ingots to make steel ingots. Requres 3 conduits. 1 for each input and 1 for the output. Requires a power source such as a solar panel, nuclear reactor or power conduit. The alloy smelter has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Alloy Smelter"]);
                    }
                    if (guiCoordinates.button19Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Harvests dark matter which is then transferred to a dark matter conduit. Requires a power source such as a solar panel, nuclear reactor or power conduit. The dark matter collector has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Dark Matter Collector"]);
                    }
                    if (guiCoordinates.button20Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Transfers dark matter from a collector to a storage container or another conduit. Dark matter conduits have an adjustable input/output range and do not require power to operate.\n\n[WORTH]\n$" + priceDictionary["Dark Matter Conduit"]);
                    }
                    if (guiCoordinates.button21Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Automatically crafts objects using items from an attached storage container. Place within 2 meters of the storage container. Then, place an item of the desired type into the auto crafter's inventory. This will designate that item as the item to be crafted. Crafted items will be deposited into the attached storage container. This machine requires power and has an adjustable output measured in items per cycle.\n\n[WORTH]\n$" + priceDictionary["Auto Crafter"]);
                    }
                    if (guiCoordinates.button22Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Provides a waypoint for rail carts. Has an adjustable range at which the next hub will be located and rails deployed to it's location. Rail cart hubs can be configured to stop the rail cart so it can be loaded and unloaded.\n\n[WORTH]\n$" + priceDictionary["Rail Cart Hub"]);
                    }
                    if (guiCoordinates.button23Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "A mobile storage container that rides on rails from one rail cart hub to the next. Configure the hubs to stop the cart near a conduit or retriever so it can be loaded or unloaded. Must be placed on a rail cart hub.\n\n[WORTH]\n$" + priceDictionary["Rail Cart"]);
                    }

                    GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                    int f = GUI.skin.label.fontSize;
                    GUI.skin.label.fontSize = 24;
                    GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                    GUI.Label(guiCoordinates.marketTitleRect, "MARKET");
                    GUI.skin.label.fontSize = f;
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
                        GUI.Label(guiCoordinates.craftingInfoRect, "Iron block for building structures. Iron blocks have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[WORTH]\n$" + priceDictionary["Iron Block"]);
                    }
                    if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Iron ramp for building structures. Iron ramps have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[WORTH]\n$" + priceDictionary["Iron Ramp"]);
                    }
                    if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Steel block for building structures. Steel blocks have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[WORTH]\n$" + priceDictionary["Steel Block"]);
                    }
                    if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Steel ramp for building structures. Steel ramps have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[WORTH]\n$" + priceDictionary["Steel Ramp"]);
                    }
                    if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Hatchway used for entering structures.\n\n[WORTH]\n$" + priceDictionary["Quantum Hatchway"]);
                    }
                    if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "For interior lighting. Requires power from a solar panel, nuclear reactor or power conduit.\n\n[WORTH]\n$" + priceDictionary["Electric Light"]);
                    }
                    if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "A combination of conductive, semi-conductive and insulating materials combined to create a logic processing circuit.\n\n[WORTH]\n$" + priceDictionary["Circuit Board"]);
                    }
                    if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "A device that converts electrical energy to mechanical torque.\n\n[WORTH]\n$" + priceDictionary["Electric Motor"]);
                    }
                    if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Provides access to all stationary storage containers within 4 meters. Can be accessed manually or connected to retrievers, auto crafters and conduits. When a conduit is connectextureDictionary.dictionary to the computer, the computer will store items starting with the first container found to have space available. When a retriever is connected to the computer, the computer will search all of the managed containers for desired items.\n\n[WORTH]\n$" + priceDictionary["Storage Computer"]);
                    }
                    if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                    {
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.Label(guiCoordinates.craftingInfoRect, "Protects your equipment from meteor showers and other hazards. Requires a power source such as a solar panel, nuclear reactor or power conduit. Turrets have an adjustable output measured in rounds per minute.\n\n[WORTH]\n$" + priceDictionary["Turret"]);
                    }

                    GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
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
                    if (GUI.Button(guiCoordinates.button5Rect, "Quantum Hatchway"))
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
                    if (GUI.Button(guiCoordinates.button6Rect, "Electric Light"))
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
                    if (GUI.Button(guiCoordinates.button7Rect, "Circuit Board"))
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
                    if (GUI.Button(guiCoordinates.button9Rect, "Electric Motor"))
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
                    if (GUI.Button(guiCoordinates.button10Rect, "Storage Computer"))
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
                    if (GUI.Button(guiCoordinates.button11Rect, "Turret"))
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
                    buyingOrSelling = "SELL";
                }
                else
                {
                    buyingOrSelling = "BUY";
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
                GUI.DrawTexture(guiCoordinates.marketMessageRect, textureDictionary.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.marketMessageLabelRect, "You need to be within 4 meters of the rocket to use the market.");
                if (GUI.Button(guiCoordinates.marketMessageButtonRect, "OK"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.inventoryOpen = false;
                    playerController.marketGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }
            else if (GameObject.Find("Rocket").GetComponent<Rocket>().landed == false && GameObject.Find("Rocket").GetComponent<Rocket>().rocketRequested == false)
            {
                GUI.DrawTexture(guiCoordinates.marketMessageRect, textureDictionary.dictionary["Interface Background"]);
                GUI.Label(guiCoordinates.marketMessageLabelRect, "You need to be within 4 meters of the rocket to use the market.");
                if (GUI.Button(guiCoordinates.marketMessageButtonRect, "Request Rocket"))
                {
                    GameObject.Find("Rocket").GetComponent<Rocket>().rocketRequested = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.inventoryOpen = false;
                    playerController.marketGUIopen = false;
                    playerController.PlayButtonSound();
                }
            }
        }
    }
}