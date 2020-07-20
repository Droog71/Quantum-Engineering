using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerCrafting playerCrafting;
    private InventoryManager playerInventory;
    private string storageComputerSearchText = "";
    private string machineDisplayID = "unassigned";
    private string machineDisplayOutputID = "unassigned";
    private string machineDisplayOutputID2 = "unassigned";
    private string machineDisplayInputID = "unassigned";
    private string machineDisplayInputID2 = "unassigned";
    public Texture2D menuBackground;
    public Texture2D containerBackground;
    public Texture2D inventoryBackground;
    public Texture2D craftingBackground;
    public Texture2D tabletBackground;
    public Texture2D selectionBox;
    public Texture2D narrowMenuBackground;
    public Texture2D crosshair;
    public GUISkin thisGUIskin;
    public GameObject videoPlayer;
    private float missingItemTimer;
    private int craftingPage;
    private bool schematic1;
    private bool schematic2;
    private bool schematic3;
    private bool schematic4;
    private bool schematic5;
    private bool schematic6;
    private bool schematic7;
    private bool hubStopWindowOpen;
    public Texture2D dmSchematic;
    public Texture2D gearSchematic;
    public Texture2D wireSchematic;
    public Texture2D plateSchematic;
    public Texture2D heatExchangerSchematic;
    public Texture2D steelSchematic;
    public Texture2D bronzeSchematic;

    public Dictionary<string, Texture2D> textureDictionary;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerCrafting = GetComponent<PlayerCrafting>();
        playerInventory = GetComponent<InventoryManager>();

        //INVENTORY ICONS
        textureDictionary = new Dictionary<string, Texture2D>();
        textureDictionary.Add("Dark Matter", Resources.Load("DarkMatter") as Texture2D);
        textureDictionary.Add("Iron Ingot", Resources.Load("IronIngot") as Texture2D);
        textureDictionary.Add("Tin Ingot", Resources.Load("TinIngot") as Texture2D);
        textureDictionary.Add("Aluminum Ingot", Resources.Load("AluminumIngot") as Texture2D);
        textureDictionary.Add("Copper Ingot", Resources.Load("CopperIngot") as Texture2D);
        textureDictionary.Add("Bronze Ingot", Resources.Load("BronzeIngot") as Texture2D);
        textureDictionary.Add("Steel Ingot", Resources.Load("IronIngot") as Texture2D);
        textureDictionary.Add("Iron Block", Resources.Load("IronBlock") as Texture2D);
        textureDictionary.Add("Iron Ramp", Resources.Load("IronRamp") as Texture2D);
        textureDictionary.Add("Steel Block", Resources.Load("SteelBlock") as Texture2D);
        textureDictionary.Add("Steel Ramp", Resources.Load("SteelRamp") as Texture2D);
        textureDictionary.Add("Glass Block", Resources.Load("Glass") as Texture2D);
        textureDictionary.Add("Brick", Resources.Load("Brick") as Texture2D);
        textureDictionary.Add("Electric Light", Resources.Load("Light") as Texture2D);
        textureDictionary.Add("Dark Matter Collector", Resources.Load("DarkMatterCollector") as Texture2D);
        textureDictionary.Add("Dark Matter Conduit", Resources.Load("DarkMatterConduit") as Texture2D);
        textureDictionary.Add("Universal Conduit", Resources.Load("UniversalConduit") as Texture2D);
        textureDictionary.Add("Universal Extractor", Resources.Load("UniversalExtractor") as Texture2D);
        textureDictionary.Add("Auger", Resources.Load("Auger") as Texture2D);
        textureDictionary.Add("Quantum Hatchway", Resources.Load("Hatch") as Texture2D);
        textureDictionary.Add("Storage Container", Resources.Load("StorageContainer") as Texture2D);
        textureDictionary.Add("Copper Ore", Resources.Load("CopperOre") as Texture2D);
        textureDictionary.Add("Iron Ore", Resources.Load("IronOre") as Texture2D);
        textureDictionary.Add("Tin Ore", Resources.Load("TinOre") as Texture2D);
        textureDictionary.Add("Aluminum Ore", Resources.Load("AluminumOre") as Texture2D);
        textureDictionary.Add("Copper Plate", Resources.Load("CopperPlate") as Texture2D);
        textureDictionary.Add("Iron Plate", Resources.Load("IronPlate") as Texture2D);
        textureDictionary.Add("Tin Plate", Resources.Load("TinPlate") as Texture2D);
        textureDictionary.Add("Bronze Plate", Resources.Load("BronzePlate") as Texture2D);
        textureDictionary.Add("Steel Plate", Resources.Load("IronPlate") as Texture2D);
        textureDictionary.Add("Aluminum Plate", Resources.Load("AluminumPlate") as Texture2D);
        textureDictionary.Add("Copper Gear", Resources.Load("CopperGear") as Texture2D);
        textureDictionary.Add("Iron Gear", Resources.Load("IronGear") as Texture2D);
        textureDictionary.Add("Tin Gear", Resources.Load("TinGear") as Texture2D);
        textureDictionary.Add("Bronze Gear", Resources.Load("BronzeGear") as Texture2D);
        textureDictionary.Add("Steel Gear", Resources.Load("IronGear") as Texture2D);
        textureDictionary.Add("Aluminum Gear", Resources.Load("AluminumGear") as Texture2D);
        textureDictionary.Add("Smelter", Resources.Load("Smelter") as Texture2D);
        textureDictionary.Add("Turret", Resources.Load("Turret") as Texture2D);
        textureDictionary.Add("Solar Panel", Resources.Load("SolarPanel") as Texture2D);
        textureDictionary.Add("Generator", Resources.Load("Generator") as Texture2D);
        textureDictionary.Add("Power Conduit", Resources.Load("PowerConduit") as Texture2D);
        textureDictionary.Add("Nuclear Reactor", Resources.Load("NuclearReactor") as Texture2D);
        textureDictionary.Add("Reactor Turbine", Resources.Load("ReactorTurbine") as Texture2D);
        textureDictionary.Add("Alloy Smelter", Resources.Load("AlloySmelter") as Texture2D);
        textureDictionary.Add("Press", Resources.Load("Press") as Texture2D);
        textureDictionary.Add("Extruder", Resources.Load("Extruder") as Texture2D);
        textureDictionary.Add("Retriever", Resources.Load("Retriever") as Texture2D);
        textureDictionary.Add("Heat Exchanger", Resources.Load("HeatExchanger") as Texture2D);
        textureDictionary.Add("Gear Cutter", Resources.Load("GearCutter") as Texture2D);
        textureDictionary.Add("Copper Wire", Resources.Load("CopperWire") as Texture2D);
        textureDictionary.Add("Aluminum Wire", Resources.Load("AluminumWire") as Texture2D);
        textureDictionary.Add("Iron Pipe", Resources.Load("IronPipe") as Texture2D);
        textureDictionary.Add("Steel Pipe", Resources.Load("SteelPipe") as Texture2D);
        textureDictionary.Add("Coal", Resources.Load("CoalOre") as Texture2D);
        textureDictionary.Add("Ice", Resources.Load("Ice") as Texture2D);
        textureDictionary.Add("Regolith", Resources.Load("Regolith") as Texture2D);
        textureDictionary.Add("Auto Crafter", Resources.Load("AutoCrafter") as Texture2D);
        textureDictionary.Add("Rail Cart", Resources.Load("RailCart") as Texture2D);
        textureDictionary.Add("Rail Cart Hub", Resources.Load("RailCartHub") as Texture2D);
        textureDictionary.Add("Storage Computer", Resources.Load("StorageComputer") as Texture2D);
        textureDictionary.Add("Circuit Board", Resources.Load("CircuitBoard") as Texture2D);
        textureDictionary.Add("Electric Motor", Resources.Load("Motor") as Texture2D);
    }

    void OnGUI()
    {
        //STYLE
        GUI.skin = thisGUIskin;

        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        //MESSAGES
        Rect messageRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.49f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));
        Rect highMessageRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.30f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));
        Rect longHighMessageRect = new Rect((ScreenWidth * 0.44f), (ScreenHeight * 0.30f), (ScreenWidth * 0.55f), (ScreenHeight * 0.5f));

        //CROSSHAIR
        Rect crosshairRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.47f), (ScreenWidth * 0.04f), (ScreenHeight * 0.06f));

        //INVENTORY
        Rect inventoryMesageRect = new Rect((ScreenWidth * 0.76f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));
        Rect storageInventoryMessageRect = new Rect((ScreenWidth * 0.36f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));

        Rect craftingButtonRect = new Rect((ScreenWidth * 0.675f), (ScreenHeight * 0.77f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect closeButtonRect = new Rect((ScreenWidth * 0.825f), (ScreenHeight * 0.77f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        Rect inventoryBackgroundRect = new Rect((ScreenWidth * 0.40f), (ScreenHeight * 0.20f), (ScreenWidth * 0.60f), (ScreenHeight * 0.62f));

        Rect storageComputerPreviousRect = new Rect((ScreenWidth * 0.295f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        Rect storageComputerNextRect = new Rect((ScreenWidth * 0.465f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        Rect storageComputerRebootRect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        Rect storageComputerSearchRect = new Rect((ScreenWidth * 0.28f), (ScreenHeight * 0.28f), (ScreenWidth * 0.12f), (ScreenHeight * 0.025f));
        Rect storageSearchLabelRect = new Rect((ScreenWidth * 0.32f), (ScreenHeight * 0.26f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));
        Rect storageComputerMessageRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));

        Rect craftingPreviousRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.63f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        Rect craftingNextRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.63f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));

        Rect inventorySlot1Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot2Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot3Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot4Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot5Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot6Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot7Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot8Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot9Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot10Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot11Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot12Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot13Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot14Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot15Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect inventorySlot16Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));

        Rect inventorySlot1TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot2TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot3TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot4TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot5TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot6TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot7TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot8TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot9TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot10TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot11TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot12TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot13TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot14TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot15TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect inventorySlot16TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));

        //STORAGE CONTAINERS
        Rect storageInventoryBackgroundRect = new Rect(0, (ScreenHeight * 0.20f), (ScreenWidth * 0.60f), (ScreenHeight * 0.62f));
        Rect storageInventorySlot1Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot2Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot3Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot4Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot5Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot6Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot7Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot8Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot9Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot10Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot11Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot12Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot13Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot14Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot15Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect storageInventorySlot16Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));

        Rect storageInventorySlot1TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot2TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot3TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot4TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot5TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot6TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot7TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot8TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot9TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot10TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot11TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot12TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot13TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot14TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot15TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        Rect storageInventorySlot16TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));

        //CRAFTING
        Rect craftingBackgroundRect = new Rect(0, (ScreenHeight * 0.05f), (ScreenWidth * 0.60f), (ScreenHeight * 0.68f));
        Rect craftingInfoBackgroundRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.65f), (ScreenWidth * 0.50f), (ScreenHeight * 0.35f));
        Rect craftingInfoRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.69f), (ScreenWidth * 0.42f), (ScreenHeight * 0.31f));

        //CRAFTING GUI BUTTONS
        Rect button1Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button2Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button3Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button4Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button5Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button6Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button7Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button8Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button9Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button10Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button11Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button12Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button13Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button14Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button15Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button16Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button17Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button18Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button19Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button20Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button21Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button22Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button23Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect button24Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //MACHINE INFO HUD
        Rect infoRectBG = new Rect(0, (ScreenHeight * 0.60f), (ScreenWidth * 0.30f), (ScreenHeight * 0.40f));
        Rect infoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.65f), (ScreenWidth * 0.24f), (ScreenHeight * 0.30f));

        //INVENTORY INSTRUCTIONS
        Rect inventoryInfoRectBG = new Rect(0, (ScreenHeight * 0.78f), (ScreenWidth * 0.40f), (ScreenHeight * 0.22f));
        Rect inventoryInfoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.83f), (ScreenWidth * 0.35f), (ScreenHeight * 0.20f));

        //BUILDING INSTRUCTIONS
        Rect buildInfoRectBG = new Rect(0, (ScreenHeight * 0.75f), (ScreenWidth * 0.40f), (ScreenHeight * 0.25f));
        Rect buildInfoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.80f), (ScreenWidth * 0.35f), (ScreenHeight * 0.20f));

        //MACHINE INTERACTION WINDOW
        Rect speedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.28f), (ScreenWidth * 0.24f), (ScreenHeight * 0.30f));
        Rect FourButtonSpeedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.28f), (ScreenWidth * 0.24f), (ScreenHeight * 0.35f));
        Rect FiveButtonSpeedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.22f), (ScreenWidth * 0.24f), (ScreenHeight * 0.41f));
        Rect outputLabelRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect longOutputLabelRect = new Rect((ScreenWidth * 0.29f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect outputControlButton0Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.28f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect outputControlButton1Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect outputControlButton2Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.40f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect outputControlButton3Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.46f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect outputControlButton4Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.52f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //OPTIONS/EXIT MENU
        Rect escapeMenuRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.30f), (ScreenWidth * 0.2f), (ScreenHeight * 0.37f));
        Rect escapeButton1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect escapeButton2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.42f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect escapeButton3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.50f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect escapeButton4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.58f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //TABLET MESSAGES
        Rect topLeftInfoRect = new Rect(0, 0, (ScreenWidth * 0.5f), (ScreenHeight * 0.2f));

        //TABLET
        Rect tabletBackgroundRect = new Rect(0, 0, (ScreenWidth * 0.5f), ScreenHeight);
        Rect tabletMessageRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.15f), (ScreenWidth * 0.4f), (ScreenHeight * 0.70f));
        Rect tabletButtonRect = new Rect((ScreenWidth * 0.175f), (ScreenHeight * 0.85f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect tabletTimeRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.8f), (ScreenWidth * 0.4f), (ScreenHeight * 0.05f));

        //BUILD ITEM SELECTION HUD
        Rect topRightInfoRect = new Rect((ScreenWidth * 0.70f), 0, (ScreenWidth * 0.3f), (ScreenHeight * 0.2f));
        Rect previousBuildItemTextureRect = new Rect((ScreenWidth * 0.70f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        Rect buildItemTextureRect = new Rect((ScreenWidth * 0.78f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        Rect nextBuildItemTextureRect = new Rect((ScreenWidth * 0.86f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        Rect currentBuildItemTextureRect = new Rect((ScreenWidth * 0.95f), (ScreenHeight * 0.21f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        Rect buildItemCountRect = new Rect((ScreenWidth * 0.92f), (ScreenHeight * 0.241f), (ScreenWidth * 0.05f), (ScreenHeight * 0.2f));

        //OPTIONS MENU
        Rect optionsMenuBackgroundRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.22f), (ScreenWidth * 0.2f), (ScreenHeight * 0.67f));
        Rect videoMenuBackgroundRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.22f), (ScreenWidth * 0.2f), (ScreenHeight * 0.67f));
        Rect optionsButton1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.26f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.32f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.38f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.44f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.50f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton6Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.56f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton7Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.62f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton8Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.68f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton9Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.74f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect optionsButton10Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.80f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        Rect sliderLabel1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.41f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect sliderLabel2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.47f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect sliderLabel3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.53f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect sliderLabel4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.59f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        Rect sliderLabel5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.65f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        Rect schematicCloseRect = new Rect(0,0,(ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        if (playerController.stateManager.worldLoaded == true && GetComponent<Main_Menu>().finishedLoading == true)
        {
            //BUILD ITEM HUD AT TOP RIGHT OF SCREEN
            if (playerController.displayingBuildItem == true)
            {
                GUI.Label(topRightInfoRect, "\n\nBuild item set to " + playerController.buildType);
                GUI.DrawTexture(previousBuildItemTextureRect, textureDictionary[playerController.previousBuildType]);
                GUI.DrawTexture(buildItemTextureRect, textureDictionary[playerController.buildType]);
                GUI.DrawTexture(currentBuildItemTextureRect, textureDictionary[playerController.buildType]);
                GUI.DrawTexture(buildItemTextureRect, selectionBox);
                GUI.DrawTexture(nextBuildItemTextureRect, textureDictionary[playerController.nextBuildType]);
                int buildItemCount = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.buildType))
                    {
                        buildItemCount += slot.amountInSlot;
                    }
                }
                GUI.Label(buildItemCountRect, "" + buildItemCount);
            }

            //METEOR SHOWER WARNINGS
            if (playerController.meteorShowerWarningActive == true || playerController.timeToDeliverWarningRecieved == true || playerController.pirateAttackWarningActive == true || playerController.destructionMessageActive == true)
            {
                GUI.Label(topLeftInfoRect, "Urgent message received! Check your tablet for more information.");
            }

            //TABLET
            if (playerController.tabletOpen == true)
            {
                int day = GameObject.Find("Rocket").GetComponent<Rocket>().day;
                int hour = (int)GameObject.Find("Rocket").GetComponent<Rocket>().gameTime;
                string hourString = "";
                if (hour < 10)
                {
                    hourString = "000" + hour;
                }
                else if (hour >= 10 && hour < 100)
                {
                    hourString = "00" + hour;
                }
                else if (hour >= 100 && hour < 1000)
                {
                    hourString = "0" + hour;
                }
                else if (hour >= 1000)
                {
                    hourString = "" + hour;
                }
                GUI.DrawTexture(tabletBackgroundRect, tabletBackground);
                GUI.Label(tabletMessageRect, playerController.currentTabletMessage);
                GUI.Label(tabletTimeRect, "\nDay: " + day + " Hour: " + hourString + ", Income: $" + playerController.money.ToString("N0"));
                if (GUI.Button(tabletButtonRect, "CLOSE"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.tabletOpen = false;
                    playerController.guiSound.volume = 0.15f;
                    playerController.guiSound.clip = playerController.buttonClip;
                    playerController.guiSound.Play();
                }
            }

            //OPTIONS/EXIT MENU
            if (playerController.escapeMenuOpen == true)
            {
                if (playerController.helpMenuOpen == false && playerController.optionsGUIopen == false && cGUI.showingInputGUI == false && playerController.exiting == false)
                {
                    GUI.DrawTexture(escapeMenuRect, narrowMenuBackground);
                    if (GUI.Button(escapeButton1Rect, "Resume"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        gameObject.GetComponent<MSCameraController>().enabled = true;
                        playerController.escapeMenuOpen = false;
                        playerController.optionsGUIopen = false;
                        playerController.helpMenuOpen = false;
                        playerController.videoMenuOpen = false;
                        playerController.schematicMenuOpen = false;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(escapeButton2Rect, "Options"))
                    {
                        playerController.optionsGUIopen = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(escapeButton3Rect, "Help"))
                    {
                        playerController.helpMenuOpen = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(escapeButton4Rect, "Exit"))
                    {
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                        PlayerPrefs.SetFloat("xSensitivity", GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX);
                        PlayerPrefs.SetFloat("ySensitivity", GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY);
                        PlayerPrefsX.SetBool("mouseInverted", GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput);
                        PlayerPrefs.SetFloat("FOV", playerController.mCam.fieldOfView);
                        PlayerPrefs.SetFloat("DrawDistance", playerController.mCam.farClipPlane);
                        PlayerPrefsX.SetVector3(playerController.stateManager.WorldName + "playerPosition", transform.position);
                        PlayerPrefsX.SetQuaternion(playerController.stateManager.WorldName + "playerRotation", transform.rotation);
                        PlayerPrefs.SetInt(playerController.stateManager.WorldName + "money", playerController.money);
                        PlayerPrefsX.SetBool(playerController.stateManager.WorldName + "oldWorld", true);
                        PlayerPrefs.SetFloat("volume", GetComponent<MSCameraController>().cameras[0].volume);
                        PlayerPrefs.Save();
                        playerController.exiting = true;
                        playerController.requestedExit = true;
                    }
                }
                if (playerController.exiting == true)
                {
                    GUI.Label(messageRect, "\n\n\n\n\nSaving world...");
                }
            }

            //HELP MENU
            if (playerController.helpMenuOpen == true)
            {
                if (playerController.videoMenuOpen == false && playerController.schematicMenuOpen == false)
                {
                    GUI.DrawTexture(escapeMenuRect, narrowMenuBackground);
                    if (GUI.Button(escapeButton1Rect, "Videos"))
                    {
                        playerController.videoMenuOpen = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(escapeButton2Rect, "Schematics"))
                    {
                        playerController.schematicMenuOpen = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(escapeButton4Rect, "BACK"))
                    {
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                        playerController.helpMenuOpen = false;
                    }
                }
                if (playerController.videoMenuOpen == true)
                {
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        GUI.DrawTexture(videoMenuBackgroundRect, narrowMenuBackground);
                    }
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        if (GUI.Button(optionsButton1Rect, "Intro"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Guide.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton2Rect, "Dark Matter"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("DarkMatter.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton3Rect, "Universal Extractor"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Extractor.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton4Rect, "Heat Exchanger"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("HeatExchanger.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton5Rect, "Alloy Smelter"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("AlloySmelter.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton6Rect, "Hazards"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("Hazards.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton7Rect, "Rail Carts"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("RailCarts.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton8Rect, "Storage Computers"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("StorageComputers.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton9Rect, "Nuclear Reactors"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            videoPlayer.GetComponent<VP>().PlayVideo("NuclearReactors.webm", false, 0.5f);
                        }
                        if (GUI.Button(optionsButton10Rect, "BACK"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            playerController.videoMenuOpen = false;
                        }
                    }

                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == false)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        videoPlayer.GetComponent<VP>().StopVideo();
                    }
                    if (playerController.mCam.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == true && Input.anyKey)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        videoPlayer.GetComponent<VP>().StopVideo();
                    }
                }
                if (playerController.schematicMenuOpen == true)
                {
                    if (schematic1 == false || schematic2 == false || schematic3 == false || schematic4 == false || schematic5 == false || schematic6 == false || schematic7 == false)
                    {
                        GUI.DrawTexture(optionsMenuBackgroundRect, narrowMenuBackground);
                        if (GUI.Button(optionsButton1Rect, "Dark Matter"))
                        {
                            if (schematic1 == false)
                            {
                                schematic1 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                        if (GUI.Button(optionsButton2Rect, "Plates"))
                        {
                            if (schematic2 == false)
                            {
                                schematic2 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                        if (GUI.Button(optionsButton3Rect, "Wires"))
                        {
                            if (schematic3 == false)
                            {
                                schematic3 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                        if (GUI.Button(optionsButton4Rect, "Gears"))
                        {
                            if (schematic4 == false)
                            {
                                schematic4 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();

                        }
                        if (GUI.Button(optionsButton5Rect, "Steel"))
                        {
                            if (schematic5 == false)
                            {
                                schematic5 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();

                        }
                        if (GUI.Button(optionsButton6Rect, "Bronze"))
                        {
                            if (schematic6 == false)
                            {
                                schematic6 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();

                        }
                        if (GUI.Button(optionsButton7Rect, "Heat Exchangers"))
                        {
                            if (schematic7 == false)
                            {
                                schematic7 = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                        if (GUI.Button(optionsButton8Rect, "BACK"))
                        {
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                            playerController.schematicMenuOpen = false;
                        }
                    }
                    if (schematic1 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight),dmSchematic);
                    }
                    if (schematic2 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), plateSchematic);
                    }
                    if (schematic3 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), wireSchematic);
                    }
                    if (schematic4 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), gearSchematic);
                    }
                    if (schematic5 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), steelSchematic);
                    }
                    if (schematic6 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), bronzeSchematic);
                    }
                    if (schematic7 == true)
                    {
                        GUI.DrawTexture(new Rect(0, 0, ScreenWidth, ScreenHeight), heatExchangerSchematic);
                    }
                    if (schematic1 == true || schematic2 == true || schematic3 == true || schematic4 == true || schematic5 == true || schematic6 == true || schematic7 == true)
                    {
                        if (GUI.Button(schematicCloseRect,"CLOSE") || Input.anyKey)
                        {
                            schematic1 = false;
                            schematic2 = false;
                            schematic3 = false;
                            schematic4 = false;
                            schematic5 = false;
                            schematic6 = false;
                            schematic7 = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                }
                else
                {
                    schematic1 = false;
                    schematic2 = false;
                    schematic3 = false;
                    schematic4 = false;
                    schematic5 = false;
                    schematic6 = false;
                    schematic7 = false;
                }
            }

            //OPTIONS MENU
            if (playerController.optionsGUIopen == true && cGUI.showingInputGUI == false)
            {
                GUI.DrawTexture(optionsMenuBackgroundRect, narrowMenuBackground);
                if (GUI.Button(optionsButton1Rect, "Bindings"))
                {
                    playerController.guiSound.volume = 0.15f;
                    playerController.guiSound.clip = playerController.buttonClip;
                    playerController.guiSound.Play();
                    cGUI.ToggleGUI();
                }
                if (GUI.Button(optionsButton2Rect, "Invert Mouse Y: " + GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput))
                {
                    if (GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput)
                    {
                        GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = false;
                    }
                    else
                    {
                        GetComponent<MSCameraController>().CameraSettings.firstPerson.invertYInput = true;
                    }
                    playerController.guiSound.volume = 0.15f;
                    playerController.guiSound.clip = playerController.buttonClip;
                    playerController.guiSound.Play();
                }
                GUI.Label(sliderLabel1Rect, "X sensitivity");
                GUI.Label(sliderLabel2Rect, "Y sensitivity");
                GUI.Label(sliderLabel3Rect, "Volume");
                GUI.Label(sliderLabel4Rect, "FOV");
                GUI.Label(sliderLabel5Rect, "Draw Distance");
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX = GUI.HorizontalSlider(optionsButton4Rect, GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityX, 0, 10);
                GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY = GUI.HorizontalSlider(optionsButton5Rect, GetComponent<MSCameraController>().CameraSettings.firstPerson.sensibilityY, 0, 10);
                AudioListener.volume = GUI.HorizontalSlider(optionsButton6Rect, AudioListener.volume, 0, 5);
                GetComponent<MSCameraController>().cameras[0].volume = AudioListener.volume;
                playerController.mCam.fieldOfView = GUI.HorizontalSlider(optionsButton7Rect, playerController.mCam.fieldOfView, 60, 80);
                playerController.mCam.farClipPlane = GUI.HorizontalSlider(optionsButton8Rect, playerController.mCam.farClipPlane, 1000, 100000);
                if (GUI.Button(optionsButton9Rect, "Block Physics: "+ GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics))
                {
                    if (GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics == false)
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics = true;
                    }
                    else
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics = false;
                    }
                    PlayerPrefsX.SetBool(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "blockPhysics", GameObject.Find("GameManager").GetComponent<GameManager>().blockPhysics);
                }
                if (GUI.Button(optionsButton10Rect, "BACK"))
                {
                    playerController.optionsGUIopen = false;
                    playerController.guiSound.volume = 0.15f;
                    playerController.guiSound.clip = playerController.buttonClip;
                    playerController.guiSound.Play();
                }
            }

            if (playerController.cannotCollect == true)
            {
                if (playerController.cannotCollectTimer < 3)
                {
                    GUI.Label(messageRect, "\n\nNo space in inventory.");
                    playerController.cannotCollectTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.cannotCollect = false;
                    playerController.cannotCollectTimer = 0;
                }
            }

            if (playerController.invalidAugerPlacement == true)
            {
                if (playerController.invalidAugerPlacementTimer < 3)
                {
                    GUI.Label(messageRect, "\n\n\n\nInvalid location.");
                    playerController.invalidAugerPlacementTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.invalidAugerPlacement = false;
                    playerController.invalidAugerPlacementTimer = 0;
                }
            }

            if (playerController.invalidRailCartPlacement == true)
            {
                if (playerController.invalidRailCartPlacementTimer < 3)
                {
                    GUI.Label(messageRect, "\n\n\n\nInvalid location.");
                    playerController.invalidRailCartPlacementTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.invalidRailCartPlacement = false;
                    playerController.invalidRailCartPlacementTimer = 0;
                }
            }

            if (playerController.stoppingBuildCoRoutine == true || playerController.requestedBuildingStop == true)
            {
                GUI.Label(longHighMessageRect, "Stopping Build System...");
            }

            if (playerController.blockLimitMessage == true)
            {
                if (playerController.blockLimitMessageTimer < 3)
                {
                    GUI.Label(longHighMessageRect, "\nWorld limit exceeded!");
                    playerController.blockLimitMessageTimer += 1 * Time.deltaTime;
                }
                else
                {
                    playerController.blockLimitMessage = false;
                    playerController.blockLimitMessageTimer = 0;
                }
            }

            //BUILDING INSTRUCTIONS
            if (playerController.building == true && playerController.tabletOpen == false)
            {
                GUI.DrawTexture(buildInfoRectBG, menuBackground);
                GUI.Label(buildInfoRect, "Right click to place block.\nPress F to collect.\nPress R to rotate.\nPress Q to stop building.");
                GUI.DrawTexture(currentBuildItemTextureRect, textureDictionary[playerController.buildType]);
                int buildItemCount = 0;
                foreach (InventorySlot slot in playerInventory.inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.buildType))
                    {
                        buildItemCount += slot.amountInSlot;
                    }
                }
                if (playerController.buildType == "Brick" || playerController.buildType == "Glass Block" || playerController.buildType == "Iron Block" || playerController.buildType == "Iron Ramp" || playerController.buildType == "Steel Block" || playerController.buildType == "Steel Ramp")
                {
                    GUI.Label(buildItemCountRect, "" + buildItemCount + "\nx" + playerController.buildMultiplier);
                }
                else
                {
                    GUI.Label(buildItemCountRect, "" + buildItemCount);
                }
            }

            //MACHINE INFO HUD
            else if (playerController.objectInSight != null && playerController.inventoryOpen == false && playerController.escapeMenuOpen == false && playerController.tabletOpen == false)
            {
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

                if (playerController.objectInSight.GetComponent<InventoryManager>() != null && playerController.objectInSight.GetComponent<AutoCrafter>() == null && playerController.objectInSight.GetComponent<Retriever>() == null && playerController.objectInSight != this.gameObject)
                {
                    if (playerController.objectInSight.GetComponent<RailCart>() != null)
                    {
                        GUI.Label(messageRect, "Rail Cart" + "\nPress E to interact." + "\nPress F to Collect.");
                    }
                    else if (playerController.objectInSight.GetComponent<InventoryManager>().ID.Equals("Lander"))
                    {
                        GUI.Label(messageRect, "Lunar Lander" + "\nPress E to interact.");
                    }
                    else if (playerController.objectInSight.GetComponent<InventoryManager>().ID.Equals("Rocket"))
                    {
                        GUI.Label(messageRect, "Rocket" + "\nPress E to interact.");
                    }
                    else
                    {
                        GUI.Label(messageRect, "Storage Container" + "\nPress E to open." + "\nPress F to Collect.");
                    }
                }
                else if (playerController.objectInSight.GetComponent<DarkMatter>() != null)
                {
                    GUI.Label(messageRect, "Dark Matter");
                }
                else if (playerController.objectInSight.GetComponent<UniversalResource>() != null)
                {
                    GUI.Label(messageRect, playerController.objectInSight.GetComponent<UniversalResource>().type);
                }
                else if (playerController.objectInSight.GetComponent<Iron>() != null)
                {
                    GUI.Label(messageRect, "Iron");
                }
                else if (playerController.objectInSight.GetComponent<IronBlock>() != null)
                {
                    GUI.Label(messageRect, "Iron Block");
                    GUI.DrawTexture(buildInfoRectBG, menuBackground);
                    GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                else if (playerController.objectInSight.GetComponent<Steel>() != null)
                {
                    GUI.Label(messageRect, "Steel Block");
                    GUI.DrawTexture(buildInfoRectBG, menuBackground);
                    GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                else if (playerController.objectInSight.GetComponent<Glass>() != null)
                {
                    GUI.Label(messageRect, "Glass Block");
                    GUI.DrawTexture(buildInfoRectBG, menuBackground);
                    GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                else if (playerController.objectInSight.GetComponent<Brick>() != null)
                {
                    GUI.Label(messageRect, "Brick Block");
                    GUI.DrawTexture(buildInfoRectBG, menuBackground);
                    GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                }
                else if (playerController.objectInSight.GetComponent<ElectricLight>() != null)
                {
                    GUI.Label(messageRect, "Electric Light" + "\nPress F to Collect.");
                }
                else if (playerController.objectInSight.GetComponent<AirLock>() != null)
                {
                    GUI.Label(messageRect, "Quantum Hatchway" + "\nPress E to interact." + "\nPress F to Collect.");
                }
                else if (playerController.objectInSight.GetComponent<StorageComputer>() != null)
                {
                    GUI.Label(messageRect, "Storage Computer" + "\nPress E to interact." + "\nPress F to Collect.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    if (playerController.objectInSight.GetComponent<StorageComputer>().initialized == true)
                    {
                        GUI.Label(infoRect, "Storage Computer" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower);
                    }
                    else
                    {
                        if (playerController.objectInSight.GetComponent<StorageComputer>().bootTimer > 0)
                        {
                            GUI.Label(infoRect, "Storage Computer" + "\nBooting up...");
                        }
                        else
                        {
                            GUI.Label(infoRect, "Storage Computer" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<RailCartHub>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<RailCartHub>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Rail Cart Hub" + "\nID: " + machineDisplayID + "\nRange: " + playerController.machineRange / 10 + " meters" + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Stop: " + playerController.objectInSight.GetComponent<RailCartHub>().stop + "\n" + "Stop Duration: " + playerController.objectInSight.GetComponent<RailCartHub>().stopTime+" seconds");
                        }
                        else
                        {
                            GUI.Label(infoRect, "Rail Cart Hub" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<DarkMatterConduit>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Dark Matter Conduit" + "\nID: " + machineDisplayID + "\nRange: " + playerController.machineRange / 10 + " meters" + "\nHolding: " + (int)playerController.machineAmount + " Dark Matter" + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Dark Matter Conduit" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<UniversalConduit>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<UniversalConduit>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Universal Conduit" + "\nID: " + machineDisplayID + "\nRange: " + playerController.machineRange / 10 + " meters" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Universal Conduit" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<PowerSource>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    if (playerController.objectInSight.GetComponent<PowerSource>().type == "Solar Panel")
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == false && playerController.objectInSight.GetComponent<PowerSource>().blocked == false)
                        {
                            GUI.Label(infoRect, "Solar Panel" + "\nID: " + machineDisplayID + "\nOutput ID: " + machineDisplayOutputID + "\nPower: " + playerController.machinePower + " MW");
                        }
                        else if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == true)
                        {
                            GUI.Label(infoRect, "Solar Panel" + "\nOffline");
                        }
                        else if (playerController.objectInSight.GetComponent<PowerSource>().blocked == true)
                        {
                            GUI.Label(infoRect, "Solar Panel" + "\nBlocked");
                        }
                    }
                    else if (playerController.objectInSight.GetComponent<PowerSource>().type == "Generator")
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Generator" + "\nID: " + machineDisplayID + "\nOutput ID: " + machineDisplayOutputID + "\nPower: " + playerController.machinePower + " MW" + "\nFuel: " + playerController.machineAmount + " " +playerController.machineType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Generator" + "\nOffline");
                        }
                    }
                    else if (playerController.objectInSight.GetComponent<PowerSource>().type == "Reactor Turbine")
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Reactor Turbine" + "\nID: " + machineDisplayID + "\nOutput ID: " + machineDisplayOutputID + "\nPower: " + playerController.machinePower + " MW");
                        }
                        else
                        {
                            GUI.Label(infoRect, "Reactor Turbine" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<NuclearReactor>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        GUI.Label(infoRect, "Nuclear Reactor" + "\nID: " + machineDisplayID + "\nCooling: " + playerController.machineCooling + " KBTU" +"\nRequired Cooling: " + playerController.objectInSight.GetComponent<NuclearReactor>().turbineCount * 5 + " KBTU");
                    }
                }
                else if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    if (playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed == false)
                    {
                        GUI.Label(infoRect, "Power Conduit" + "\nID: " + machineDisplayID + "\nRange: " + playerController.machineRange / 10 + " meters" + "\nPower: " + playerController.machinePower + " MW" + "\nOutput 1: " + machineDisplayOutputID + "\nOutput 2: " + machineDisplayOutputID2);
                    }
                    else
                    {
                        GUI.Label(infoRect, "Power Conduit" + "\nOffline");
                    }
                }
                else if (playerController.objectInSight.GetComponent<UniversalExtractor>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    if (playerController.objectInSight.GetComponent<UniversalExtractor>().connectionFailed == false)
                    { 
                        GUI.Label(infoRect, "Universal Extractor" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.collectorAmount + " " + playerController.machineType);
                    }
                    else
                    {
                        GUI.Label(infoRect, "Universal Extractor" + "\nOffline");
                    }
                }
                else if (playerController.objectInSight.GetComponent<Auger>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    GUI.Label(infoRect, "Auger" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.collectorAmount + " Regolith");
                }
                else if (playerController.objectInSight.GetComponent<DarkMatterCollector>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    GUI.DrawTexture(infoRectBG, menuBackground);
                    if (playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionFailed == false)
                    {
                        GUI.Label(infoRect, "Dark Matter Collector" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.collectorAmount + " Dark Matter");
                    }
                    else
                    {
                        GUI.Label(infoRect, "Dark Matter Collector" + "\nOffline");
                    }
                }
                else if (playerController.objectInSight.GetComponent<Smelter>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<Smelter>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Smelter" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Smelter" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<AlloySmelter>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<AlloySmelter>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Alloy Smelter" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\nHolding: " + (int)playerController.machineAmount2 + " " + playerController.machineType2 + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Input ID 2: " + machineDisplayInputID2 + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Input 2 Holding: " + (int)playerController.machineInputAmount2 + " " + playerController.machineInputType2 + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Alloy Smelter" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Press>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<Press>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Press" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Press" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Extruder>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<Extruder>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Extruder" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Extruder" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Retriever>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<Retriever>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Retriever" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nRetrieving: " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Retriever" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Auto Crafter" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nCrafting: " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayInputID);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Auto Crafter" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<GearCutter>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<GearCutter>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Gear Cutter" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + playerController.machineSpeed + " IPC"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling  + " KBTU" + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType + "\n" + "Output Holding: " + (int)playerController.machineOutputAmount + " " + playerController.machineOutputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Gear Cutter" + "\nOffline");
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Turret>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        int rpm = (int)(60/(0.2f+(3 - (playerController.machineSpeed * 0.1f))));
                        GUI.Label(infoRect, "Turret" + "\nID: " + machineDisplayID + "\nEnergized: " + playerController.machineHasPower + "\nPower: " + playerController.machinePower + " MW" + "\nOutput: " + rpm + " RPM"  + "\nHeat: " + playerController.machineHeat  + " KBTU" + "\nCooling: " + playerController.machineCooling);
                    }
                }
                else if (playerController.objectInSight.GetComponent<HeatExchanger>() != null)
                {
                    GUI.Label(messageRect, "Press F to collect.\nPress E to interact.");
                    if (playerController.machineInSight != null)
                    {
                        GUI.DrawTexture(infoRectBG, menuBackground);
                        if (playerController.objectInSight.GetComponent<HeatExchanger>().connectionFailed == false)
                        {
                            GUI.Label(infoRect, "Heat Exchanger" + "\nID: " + machineDisplayID + "\nCooling: " + playerController.objectInSight.GetComponent<HeatExchanger>().providingCooling + "\nOutput: " + playerController.machineSpeed + " KBTU"  + "\nHolding: " + (int)playerController.machineAmount + " " + playerController.machineType + "\n" + "Input ID: " + machineDisplayInputID + "\n" + "Output ID: " + machineDisplayOutputID + "\n" + "Input Holding: " + (int)playerController.machineInputAmount + " " + playerController.machineInputType);
                        }
                        else
                        {
                            GUI.Label(infoRect, "Heat Exchanger" + "\nOffline");
                        }
                    }
                }
                else if (playerController.lookingAtCombinedMesh == true && playerController.paintGunActive == false)
                {
                    if (playerController.objectInSight.name.Equals("ironHolder(Clone)"))
                    {
                        GUI.Label(messageRect, "Iron Structure");
                        GUI.DrawTexture(buildInfoRectBG, menuBackground);
                        GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                    }
                    if (playerController.objectInSight.name.Equals("glassHolder(Clone)"))
                    {
                        GUI.Label(messageRect, "Glass Structure");
                        GUI.DrawTexture(buildInfoRectBG, menuBackground);
                        GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                    }
                    if (playerController.objectInSight.name.Equals("steelHolder(Clone)"))
                    {
                        GUI.Label(messageRect, "Steel Structure");
                        GUI.DrawTexture(buildInfoRectBG, menuBackground);
                        GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                    }
                    if (playerController.objectInSight.name.Equals("brickHolder(Clone)"))
                    {
                        GUI.Label(messageRect, "Brick Structure");
                        GUI.DrawTexture(buildInfoRectBG, menuBackground);
                        GUI.Label(buildInfoRect, "Press F to remove blocks.\nPress B to add blocks.\nPress Q to stop building.");
                    }
                }
            }

            //PAINT COLOR SELECITON WINDOW
            if (playerController.paintGunActive == true)
            {
                if (playerController.paintColorSelected == false)
                {
                    GUI.DrawTexture(optionsMenuBackgroundRect, narrowMenuBackground);
                    GUI.Label(optionsButton1Rect, "      Paint  Gun");
                    GUI.Label(optionsButton2Rect, "     Select Color");
                    GUI.Label(sliderLabel1Rect, "Red");
                    GUI.Label(sliderLabel2Rect, "Green");
                    GUI.Label(sliderLabel3Rect, "Blue");
                    playerController.paintRed = GUI.HorizontalSlider(optionsButton4Rect, playerController.paintRed, 0, 1);
                    playerController.paintGreen = GUI.HorizontalSlider(optionsButton5Rect, playerController.paintGreen, 0, 1);
                    playerController.paintBlue = GUI.HorizontalSlider(optionsButton6Rect, playerController.paintBlue, 0, 1);
                    GUI.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.paintGunTank.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.adjustedPaintGunTank.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    playerController.adjustedPaintGunTank2.GetComponent<Renderer>().material.color = new Color(playerController.paintRed, playerController.paintGreen, playerController.paintBlue);
                    GUI.DrawTexture(optionsButton3Rect, textureDictionary["Iron Block"]);
                    GUI.color = Color.white;
                    if (GUI.Button(optionsButton8Rect, "DONE"))
                    {
                        playerController.paintColorSelected = true;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        gameObject.GetComponent<MSCameraController>().enabled = true;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                }
                else if (playerController.lookingAtCombinedMesh == true)
                {
                    GUI.Label(highMessageRect, "Left click to paint.\nRight click to stop.");
                }
                else
                {
                    GUI.Label(longHighMessageRect, "Only structures can be painted...");
                }
            }

            //INVENTORY ITEM DRAWING
            if (playerController.inventoryOpen == true)
            {
                gameObject.GetComponent<MSCameraController>().enabled = false;
                GUI.DrawTexture(inventoryBackgroundRect, containerBackground);

                if (!playerInventory.inventory[0].typeInSlot.Equals("") && !playerInventory.inventory[0].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot1TexRect, textureDictionary[playerInventory.inventory[0].typeInSlot]);
                }
                if (!playerInventory.inventory[1].typeInSlot.Equals("") && !playerInventory.inventory[1].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot2TexRect, textureDictionary[playerInventory.inventory[1].typeInSlot]);
                }
                if (!playerInventory.inventory[2].typeInSlot.Equals("") && !playerInventory.inventory[2].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot3TexRect, textureDictionary[playerInventory.inventory[2].typeInSlot]);
                }
                if (!playerInventory.inventory[3].typeInSlot.Equals("") && !playerInventory.inventory[3].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot4TexRect, textureDictionary[playerInventory.inventory[3].typeInSlot]);
                }
                if (!playerInventory.inventory[4].typeInSlot.Equals("") && !playerInventory.inventory[4].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot5TexRect, textureDictionary[playerInventory.inventory[4].typeInSlot]);
                }
                if (!playerInventory.inventory[5].typeInSlot.Equals("") && !playerInventory.inventory[5].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot6TexRect, textureDictionary[playerInventory.inventory[5].typeInSlot]);
                }
                if (!playerInventory.inventory[6].typeInSlot.Equals("") && !playerInventory.inventory[6].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot7TexRect, textureDictionary[playerInventory.inventory[6].typeInSlot]);
                }
                if (!playerInventory.inventory[7].typeInSlot.Equals("") && !playerInventory.inventory[7].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot8TexRect, textureDictionary[playerInventory.inventory[7].typeInSlot]);
                }
                if (!playerInventory.inventory[8].typeInSlot.Equals("") && !playerInventory.inventory[8].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot9TexRect, textureDictionary[playerInventory.inventory[8].typeInSlot]);
                }
                if (!playerInventory.inventory[9].typeInSlot.Equals("") && !playerInventory.inventory[9].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot10TexRect, textureDictionary[playerInventory.inventory[9].typeInSlot]);
                }
                if (!playerInventory.inventory[10].typeInSlot.Equals("") && !playerInventory.inventory[10].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot11TexRect, textureDictionary[playerInventory.inventory[10].typeInSlot]);
                }
                if (!playerInventory.inventory[11].typeInSlot.Equals("") && !playerInventory.inventory[11].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot12TexRect, textureDictionary[playerInventory.inventory[11].typeInSlot]);
                }
                if (!playerInventory.inventory[12].typeInSlot.Equals("") && !playerInventory.inventory[12].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot13TexRect, textureDictionary[playerInventory.inventory[12].typeInSlot]);
                }
                if (!playerInventory.inventory[13].typeInSlot.Equals("") && !playerInventory.inventory[13].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot14TexRect, textureDictionary[playerInventory.inventory[13].typeInSlot]);
                }
                if (!playerInventory.inventory[14].typeInSlot.Equals("") && !playerInventory.inventory[14].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot15TexRect, textureDictionary[playerInventory.inventory[14].typeInSlot]);
                }
                if (!playerInventory.inventory[15].typeInSlot.Equals("") && !playerInventory.inventory[15].typeInSlot.Equals("nothing"))
                {
                    GUI.DrawTexture(inventorySlot16TexRect, textureDictionary[playerInventory.inventory[15].typeInSlot]);
                }

                if (playerInventory.inventory[0].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot1Rect, playerInventory.inventory[0].amountInSlot.ToString());
                }
                if (playerInventory.inventory[1].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot2Rect, playerInventory.inventory[1].amountInSlot.ToString());
                }
                if (playerInventory.inventory[2].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot3Rect, playerInventory.inventory[2].amountInSlot.ToString());
                }
                if (playerInventory.inventory[3].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot4Rect, playerInventory.inventory[3].amountInSlot.ToString());
                }
                if (playerInventory.inventory[4].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot5Rect, playerInventory.inventory[4].amountInSlot.ToString());
                }
                if (playerInventory.inventory[5].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot6Rect, playerInventory.inventory[5].amountInSlot.ToString());
                }
                if (playerInventory.inventory[6].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot7Rect, playerInventory.inventory[6].amountInSlot.ToString());
                }
                if (playerInventory.inventory[7].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot8Rect, playerInventory.inventory[7].amountInSlot.ToString());
                }
                if (playerInventory.inventory[8].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot9Rect, playerInventory.inventory[8].amountInSlot.ToString());
                }
                if (playerInventory.inventory[9].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot10Rect, playerInventory.inventory[9].amountInSlot.ToString());
                }
                if (playerInventory.inventory[10].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot11Rect, playerInventory.inventory[10].amountInSlot.ToString());
                }
                if (playerInventory.inventory[11].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot12Rect, playerInventory.inventory[11].amountInSlot.ToString());
                }
                if (playerInventory.inventory[12].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot13Rect, playerInventory.inventory[12].amountInSlot.ToString());
                }
                if (playerInventory.inventory[13].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot14Rect, playerInventory.inventory[13].amountInSlot.ToString());
                }
                if (playerInventory.inventory[14].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot15Rect, playerInventory.inventory[14].amountInSlot.ToString());
                }
                if (playerInventory.inventory[15].amountInSlot != 0)
                {
                    GUI.Label(inventorySlot16Rect, playerInventory.inventory[15].amountInSlot.ToString());
                }

                //STORAGE CONTAINER ITEM DRAWING
                if (playerController.storageGUIopen == true)
                {
                    GUI.DrawTexture(inventoryInfoRectBG, menuBackground);
                    GUI.Label(inventoryInfoRect, "\nLeft Shift + Click & Drag to split stack.\n\nLeft Control + Click to transfer entire stack.");
                    GUI.DrawTexture(storageInventoryBackgroundRect, containerBackground);
                    if (!playerController.storageInventory.inventory[0].typeInSlot.Equals("") && !playerController.storageInventory.inventory[0].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot1TexRect, textureDictionary[playerController.storageInventory.inventory[0].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[1].typeInSlot.Equals("") && !playerController.storageInventory.inventory[1].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot2TexRect, textureDictionary[playerController.storageInventory.inventory[1].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[2].typeInSlot.Equals("") && !playerController.storageInventory.inventory[2].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot3TexRect, textureDictionary[playerController.storageInventory.inventory[2].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[3].typeInSlot.Equals("") && !playerController.storageInventory.inventory[3].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot4TexRect, textureDictionary[playerController.storageInventory.inventory[3].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[4].typeInSlot.Equals("") && !playerController.storageInventory.inventory[4].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot5TexRect, textureDictionary[playerController.storageInventory.inventory[4].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[5].typeInSlot.Equals("") && !playerController.storageInventory.inventory[5].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot6TexRect, textureDictionary[playerController.storageInventory.inventory[5].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[6].typeInSlot.Equals("") && !playerController.storageInventory.inventory[6].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot7TexRect, textureDictionary[playerController.storageInventory.inventory[6].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[7].typeInSlot.Equals("") && !playerController.storageInventory.inventory[7].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot8TexRect, textureDictionary[playerController.storageInventory.inventory[7].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[8].typeInSlot.Equals("") && !playerController.storageInventory.inventory[8].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot9TexRect, textureDictionary[playerController.storageInventory.inventory[8].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[9].typeInSlot.Equals("") && !playerController.storageInventory.inventory[9].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot10TexRect, textureDictionary[playerController.storageInventory.inventory[9].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[10].typeInSlot.Equals("") && !playerController.storageInventory.inventory[10].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot11TexRect, textureDictionary[playerController.storageInventory.inventory[10].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[11].typeInSlot.Equals("") && !playerController.storageInventory.inventory[11].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot12TexRect, textureDictionary[playerController.storageInventory.inventory[11].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[12].typeInSlot.Equals("") && !playerController.storageInventory.inventory[12].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot13TexRect, textureDictionary[playerController.storageInventory.inventory[12].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[13].typeInSlot.Equals("") && !playerController.storageInventory.inventory[13].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot14TexRect, textureDictionary[playerController.storageInventory.inventory[13].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[14].typeInSlot.Equals("") && !playerController.storageInventory.inventory[14].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot15TexRect, textureDictionary[playerController.storageInventory.inventory[14].typeInSlot]);
                    }
                    if (!playerController.storageInventory.inventory[15].typeInSlot.Equals("") && !playerController.storageInventory.inventory[15].typeInSlot.Equals("nothing"))
                    {
                        GUI.DrawTexture(storageInventorySlot16TexRect, textureDictionary[playerController.storageInventory.inventory[15].typeInSlot]);
                    }

                    if (playerController.storageInventory.inventory[0].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot1Rect, playerController.storageInventory.inventory[0].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[1].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot2Rect, playerController.storageInventory.inventory[1].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[2].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot3Rect, playerController.storageInventory.inventory[2].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[3].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot4Rect, playerController.storageInventory.inventory[3].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[4].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot5Rect, playerController.storageInventory.inventory[4].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[5].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot6Rect, playerController.storageInventory.inventory[5].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[6].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot7Rect, playerController.storageInventory.inventory[6].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[7].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot8Rect, playerController.storageInventory.inventory[7].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[8].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot9Rect, playerController.storageInventory.inventory[8].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[9].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot10Rect, playerController.storageInventory.inventory[9].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[10].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot11Rect, playerController.storageInventory.inventory[10].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[11].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot12Rect, playerController.storageInventory.inventory[11].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[12].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot13Rect, playerController.storageInventory.inventory[12].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[13].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot14Rect, playerController.storageInventory.inventory[13].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[14].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot15Rect, playerController.storageInventory.inventory[14].amountInSlot.ToString());
                    }
                    if (playerController.storageInventory.inventory[15].amountInSlot != 0)
                    {
                        GUI.Label(storageInventorySlot16Rect, playerController.storageInventory.inventory[15].amountInSlot.ToString());
                    }
                }

                //STORAGE COMPUTER INVENTORY SWITCHING
                if (playerController.storageGUIopen == true && playerController.remoteStorageActive == true)
                {
                    StorageComputer computer = playerController.currentStorageComputer.GetComponent<StorageComputer>();
                    GUI.Label(storageSearchLabelRect, "SEARCH");
                    storageComputerSearchText = GUI.TextField(storageComputerSearchRect, storageComputerSearchText);
                    if (Event.current.isKey && Event.current.keyCode != KeyCode.LeftShift && Event.current.keyCode != KeyCode.LeftControl)
                    {
                        int containerCount = 0;
                        foreach (InventoryManager manager in computer.computerContainers)
                        {
                            foreach (InventorySlot slot in manager.inventory)
                            {
                                if (storageComputerSearchText.Length < slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Search term shorter than item type string: "+slot.typeInSlot.Substring(storageComputerSearchText.Length) + " VS " + storageComputerSearchText);
                                    if (slot.typeInSlot.Substring(0,storageComputerSearchText.Length).ToLower().Equals(storageComputerSearchText.ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                                else if (storageComputerSearchText.Length > slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Search term longer than item type string: " + slot.typeInSlot + " VS " + storageComputerSearchText.Substring(slot.typeInSlot.Length));
                                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.Substring(0,slot.typeInSlot.Length).ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                                else if (storageComputerSearchText.Length == slot.typeInSlot.Length)
                                {
                                    //Debug.Log("Searching for exact match");
                                    if (slot.typeInSlot.ToLower().Equals(storageComputerSearchText.ToLower()))
                                    {
                                        playerController.storageComputerInventory = containerCount;
                                        playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                                    }
                                }
                            }
                            //Debug.Log("Current Inventory ID: " + playerController.storageComputerInventory);
                            containerCount++;
                        }
                    }
                    if (GUI.Button(storageComputerPreviousRect, "<-"))
                    {
                        if (playerController.storageComputerInventory > 0)
                        {
                            playerController.storageComputerInventory -= 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(storageComputerNextRect, "->"))
                    {
                        if (playerController.storageComputerInventory < computer.computerContainers.Length - 1)
                        {
                            playerController.storageComputerInventory += 1;
                            playerController.storageInventory = computer.computerContainers[playerController.storageComputerInventory];
                        }
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(storageComputerRebootRect,"REBOOT"))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        playerController.inventoryOpen = false;
                        playerController.craftingGUIopen = false;
                        playerController.storageGUIopen = false;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                        computer.Reboot();
                    }
                }

                //////////DRAG AND DROP//////////
                Vector2 mousePos = Event.current.mousePosition; //MOUSE POSITION

                if (playerController.storageGUIopen == true) //PLAYER IS ACCESSING A STORAGE CONTAINER
                {
                    //PLAYER IS DRAGGING AN ITEM
                    if (playerController.draggingItem == true)
                    {
                        //DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.029f), (ScreenHeight * 0.06f)), textureDictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            playerController.draggingItem = false;
                            if (inventorySlot1TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[0])
                            {
                                if (playerInventory.inventory[0].typeInSlot == "nothing" || playerInventory.inventory[0].typeInSlot == "" || playerInventory.inventory[0].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[0].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 0);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[0].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[0].amountInSlot;
                                    playerInventory.inventory[0].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[0].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot2TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[1])
                            {
                                if (playerInventory.inventory[1].typeInSlot == "nothing" || playerInventory.inventory[1].typeInSlot == "" || playerInventory.inventory[1].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[1].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 1);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[1].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[1].amountInSlot;
                                    playerInventory.inventory[1].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[1].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot3TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[2])
                            {
                                if (playerInventory.inventory[2].typeInSlot == "nothing" || playerInventory.inventory[2].typeInSlot == "" || playerInventory.inventory[2].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[2].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 2);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[2].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[2].amountInSlot;
                                    playerInventory.inventory[2].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[2].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot4TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[3])
                            {
                                if (playerInventory.inventory[3].typeInSlot == "nothing" || playerInventory.inventory[3].typeInSlot == "" || playerInventory.inventory[3].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[3].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 3);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[3].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[3].amountInSlot;
                                    playerInventory.inventory[3].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[3].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot5TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[4])
                            {
                                if (playerInventory.inventory[4].typeInSlot == "nothing" || playerInventory.inventory[4].typeInSlot == "" || playerInventory.inventory[4].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[4].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 4);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[4].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[4].amountInSlot;
                                    playerInventory.inventory[4].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[4].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot6TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[5])
                            {
                                if (playerInventory.inventory[5].typeInSlot == "nothing" || playerInventory.inventory[5].typeInSlot == "" || playerInventory.inventory[5].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[5].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 5);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[5].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[5].amountInSlot;
                                    playerInventory.inventory[5].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[5].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot7TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[6])
                            {
                                if (playerInventory.inventory[6].typeInSlot == "nothing" || playerInventory.inventory[6].typeInSlot == "" || playerInventory.inventory[6].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[6].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 6);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[6].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[6].amountInSlot;
                                    playerInventory.inventory[6].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[6].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot8TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[7])
                            {
                                if (playerInventory.inventory[7].typeInSlot == "nothing" || playerInventory.inventory[7].typeInSlot == "" || playerInventory.inventory[7].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[7].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 7);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[7].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[7].amountInSlot;
                                    playerInventory.inventory[7].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[7].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot9TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[8])
                            {
                                if (playerInventory.inventory[8].typeInSlot == "nothing" || playerInventory.inventory[8].typeInSlot == "" || playerInventory.inventory[8].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[8].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 8);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[8].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[8].amountInSlot;
                                    playerInventory.inventory[8].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[8].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot10TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[9])
                            {
                                if (playerInventory.inventory[9].typeInSlot == "nothing" || playerInventory.inventory[9].typeInSlot == "" || playerInventory.inventory[9].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[9].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 9);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[9].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[9].amountInSlot;
                                    playerInventory.inventory[9].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[9].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot11TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[10])
                            {
                                if (playerInventory.inventory[10].typeInSlot == "nothing" || playerInventory.inventory[10].typeInSlot == "" || playerInventory.inventory[10].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[10].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 10);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[10].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[10].amountInSlot;
                                    playerInventory.inventory[10].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[10].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot12TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[11])
                            {
                                if (playerInventory.inventory[11].typeInSlot == "nothing" || playerInventory.inventory[11].typeInSlot == "" || playerInventory.inventory[11].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[11].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 11);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[11].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[11].amountInSlot;
                                    playerInventory.inventory[11].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[11].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot13TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[12])
                            {
                                if (playerInventory.inventory[12].typeInSlot == "nothing" || playerInventory.inventory[12].typeInSlot == "" || playerInventory.inventory[12].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[12].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 12);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[12].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[12].amountInSlot;
                                    playerInventory.inventory[12].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[12].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot14TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[13])
                            {
                                if (playerInventory.inventory[13].typeInSlot == "nothing" || playerInventory.inventory[13].typeInSlot == "" || playerInventory.inventory[13].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[13].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 13);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[13].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[13].amountInSlot;
                                    playerInventory.inventory[13].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[13].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot15TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[14])
                            {
                                if (playerInventory.inventory[14].typeInSlot == "nothing" || playerInventory.inventory[14].typeInSlot == "" || playerInventory.inventory[14].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[14].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 14);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[14].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[14].amountInSlot;
                                    playerInventory.inventory[14].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[14].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot16TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[15])
                            {
                                if (playerInventory.inventory[15].typeInSlot == "nothing" || playerInventory.inventory[15].typeInSlot == "" || playerInventory.inventory[15].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[15].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 15);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[15].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[15].amountInSlot;
                                    playerInventory.inventory[15].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[15].amountInSlot = playerController.amountToDrag;
                                }
                            }

                            //DROPPING ITEMS INTO THE STORAGE CONTAINER
                            if (storageInventorySlot1TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[0])
                            {
                                if (playerController.storageInventory.inventory[0].typeInSlot == "nothing" || playerController.storageInventory.inventory[0].typeInSlot == "" || playerController.storageInventory.inventory[0].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[0].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 0);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[0].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[0].amountInSlot;
                                    playerController.storageInventory.inventory[0].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[0].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot2TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[1])
                            {
                                if (playerController.storageInventory.inventory[1].typeInSlot == "nothing" || playerController.storageInventory.inventory[1].typeInSlot == "" || playerController.storageInventory.inventory[1].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[1].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 1);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[1].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[1].amountInSlot;
                                    playerController.storageInventory.inventory[1].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[1].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot3TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[2])
                            {
                                if (playerController.storageInventory.inventory[2].typeInSlot == "nothing" || playerController.storageInventory.inventory[2].typeInSlot == "" || playerController.storageInventory.inventory[2].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[2].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 2);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[2].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[2].amountInSlot;
                                    playerController.storageInventory.inventory[2].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[2].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot4TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[3])
                            {
                                if (playerController.storageInventory.inventory[3].typeInSlot == "nothing" || playerController.storageInventory.inventory[3].typeInSlot == "" || playerController.storageInventory.inventory[3].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[3].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 3);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[3].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[3].amountInSlot;
                                    playerController.storageInventory.inventory[3].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[3].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot5TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[4])
                            {
                                if (playerController.storageInventory.inventory[4].typeInSlot == "nothing" || playerController.storageInventory.inventory[4].typeInSlot == "" || playerController.storageInventory.inventory[4].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[4].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 4);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[4].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[4].amountInSlot;
                                    playerController.storageInventory.inventory[4].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[4].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot6TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[5])
                            {
                                if (playerController.storageInventory.inventory[5].typeInSlot == "nothing" || playerController.storageInventory.inventory[5].typeInSlot == "" || playerController.storageInventory.inventory[5].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[5].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 5);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[5].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[5].amountInSlot;
                                    playerController.storageInventory.inventory[5].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[5].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot7TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[6])
                            {
                                if (playerController.storageInventory.inventory[6].typeInSlot == "nothing" || playerController.storageInventory.inventory[6].typeInSlot == "" || playerController.storageInventory.inventory[6].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[6].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 6);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[6].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[6].amountInSlot;
                                    playerController.storageInventory.inventory[6].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[6].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot8TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[7])
                            {
                                if (playerController.storageInventory.inventory[7].typeInSlot == "nothing" || playerController.storageInventory.inventory[7].typeInSlot == "" || playerController.storageInventory.inventory[7].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[7].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 7);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[7].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[7].amountInSlot;
                                    playerController.storageInventory.inventory[7].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[7].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot9TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[8])
                            {
                                if (playerController.storageInventory.inventory[8].typeInSlot == "nothing" || playerController.storageInventory.inventory[8].typeInSlot == "" || playerController.storageInventory.inventory[8].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[8].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 8);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[8].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[8].amountInSlot;
                                    playerController.storageInventory.inventory[8].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[8].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot10TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[9])
                            {
                                if (playerController.storageInventory.inventory[9].typeInSlot == "nothing" || playerController.storageInventory.inventory[9].typeInSlot == "" || playerController.storageInventory.inventory[9].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[9].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 9);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[9].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[9].amountInSlot;
                                    playerController.storageInventory.inventory[9].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[9].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot11TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[10])
                            {
                                if (playerController.storageInventory.inventory[10].typeInSlot == "nothing" || playerController.storageInventory.inventory[10].typeInSlot == "" || playerController.storageInventory.inventory[10].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[10].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 10);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[10].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[10].amountInSlot;
                                    playerController.storageInventory.inventory[10].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[10].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot12TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[11])
                            {
                                if (playerController.storageInventory.inventory[11].typeInSlot == "nothing" || playerController.storageInventory.inventory[11].typeInSlot == "" || playerController.storageInventory.inventory[11].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[11].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 11);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[11].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[11].amountInSlot;
                                    playerController.storageInventory.inventory[11].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[11].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot13TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[12])
                            {
                                if (playerController.storageInventory.inventory[12].typeInSlot == "nothing" || playerController.storageInventory.inventory[12].typeInSlot == "" || playerController.storageInventory.inventory[12].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[12].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 12);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[12].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[12].amountInSlot;
                                    playerController.storageInventory.inventory[12].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[12].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot14TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[13])
                            {
                                if (playerController.storageInventory.inventory[13].typeInSlot == "nothing" || playerController.storageInventory.inventory[13].typeInSlot == "" || playerController.storageInventory.inventory[13].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[13].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 13);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[13].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[13].amountInSlot;
                                    playerController.storageInventory.inventory[13].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[13].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot15TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[14])
                            {
                                if (playerController.storageInventory.inventory[14].typeInSlot == "nothing" || playerController.storageInventory.inventory[14].typeInSlot == "" || playerController.storageInventory.inventory[14].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[14].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 14);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[14].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[14].amountInSlot;
                                    playerController.storageInventory.inventory[14].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[14].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (storageInventorySlot16TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerController.storageInventory.inventory[15])
                            {
                                if (playerController.storageInventory.inventory[15].typeInSlot == "nothing" || playerController.storageInventory.inventory[15].typeInSlot == "" || playerController.storageInventory.inventory[15].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerController.storageInventory.inventory[15].amountInSlot <= playerController.storageInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerController.storageInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 15);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerController.storageInventory.inventory[15].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerController.storageInventory.inventory[15].amountInSlot;
                                    playerController.storageInventory.inventory[15].typeInSlot = playerController.itemToDrag;
                                    playerController.storageInventory.inventory[15].amountInSlot = playerController.amountToDrag;
                                }
                            }
                        }
                    }

                    //DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    if (inventorySlot1TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 1.");
                        if (playerInventory.inventory[0].typeInSlot != "" && !playerInventory.inventory[0].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[0].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[0].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[0].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[0].typeInSlot, playerInventory.inventory[0].amountInSlot);
                                            playerInventory.inventory[0].typeInSlot = "nothing";
                                            playerInventory.inventory[0].amountInSlot = 0;
                                        }
                                    }
                                
                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[0].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[0];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[0].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[0].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[0].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[0].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[0].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[0].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot2TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 2.");
                        if (playerInventory.inventory[1].typeInSlot != "" && !playerInventory.inventory[1].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[1].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[1].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[1].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[1].typeInSlot, playerInventory.inventory[1].amountInSlot);
                                            playerInventory.inventory[1].typeInSlot = "nothing";
                                            playerInventory.inventory[1].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[1].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[1];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[1].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[1].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[1].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[1].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[1].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[1].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot3TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 3.");
                        if (playerInventory.inventory[2].typeInSlot != "" && !playerInventory.inventory[2].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[2].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[2].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[2].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[2].typeInSlot, playerInventory.inventory[2].amountInSlot);
                                            playerInventory.inventory[2].typeInSlot = "nothing";
                                            playerInventory.inventory[2].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[2].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[2];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[2].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[2].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[2].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[2].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[2].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[2].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot4TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 4.");
                        if (playerInventory.inventory[3].typeInSlot != "" && !playerInventory.inventory[3].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[3].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[3].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[3].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[3].typeInSlot, playerInventory.inventory[3].amountInSlot);
                                            playerInventory.inventory[3].typeInSlot = "nothing";
                                            playerInventory.inventory[3].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[3].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[3];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[3].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[3].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[3].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[3].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[3].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[3].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot5TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 5.");
                        if (playerInventory.inventory[4].typeInSlot != "" && !playerInventory.inventory[4].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[4].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[4].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[4].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[4].typeInSlot, playerInventory.inventory[4].amountInSlot);
                                            playerInventory.inventory[4].typeInSlot = "nothing";
                                            playerInventory.inventory[4].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[4].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[4];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[4].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[4].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[4].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[4].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[4].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[4].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot6TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 6.");
                        if (playerInventory.inventory[5].typeInSlot != "" && !playerInventory.inventory[5].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[5].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[5].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[5].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[5].typeInSlot, playerInventory.inventory[5].amountInSlot);
                                            playerInventory.inventory[5].typeInSlot = "nothing";
                                            playerInventory.inventory[5].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[5].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[5];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[5].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[5].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[5].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[5].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[5].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[5].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot7TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 7.");
                        if (playerInventory.inventory[6].typeInSlot != "" && !playerInventory.inventory[6].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[6].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[6].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[6].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[6].typeInSlot, playerInventory.inventory[6].amountInSlot);
                                            playerInventory.inventory[6].typeInSlot = "nothing";
                                            playerInventory.inventory[6].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[6].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[6];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[6].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[6].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[6].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[6].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[6].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[6].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot8TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 8.");
                        if (playerInventory.inventory[7].typeInSlot != "" && !playerInventory.inventory[7].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[7].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[7].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[7].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[7].typeInSlot, playerInventory.inventory[7].amountInSlot);
                                            playerInventory.inventory[7].typeInSlot = "nothing";
                                            playerInventory.inventory[7].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[7].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[7];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[7].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[7].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[7].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[7].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[7].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[7].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot9TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 9.");
                        if (playerInventory.inventory[8].typeInSlot != "" && !playerInventory.inventory[8].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[8].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[8].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[8].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[8].typeInSlot, playerInventory.inventory[8].amountInSlot);
                                            playerInventory.inventory[8].typeInSlot = "nothing";
                                            playerInventory.inventory[8].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[8].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[8];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[8].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[8].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[8].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[8].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[8].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[8].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot10TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 10.");
                        if (playerInventory.inventory[9].typeInSlot != "" && !playerInventory.inventory[9].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[9].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[9].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[9].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[9].typeInSlot, playerInventory.inventory[9].amountInSlot);
                                            playerInventory.inventory[9].typeInSlot = "nothing";
                                            playerInventory.inventory[9].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[9].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[9];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[9].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[9].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[9].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[9].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[9].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[9].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot11TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 11.");
                        if (playerInventory.inventory[10].typeInSlot != "" && !playerInventory.inventory[10].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[10].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[10].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[10].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[10].typeInSlot, playerInventory.inventory[10].amountInSlot);
                                            playerInventory.inventory[10].typeInSlot = "nothing";
                                            playerInventory.inventory[10].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[10].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[10];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[10].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[10].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[10].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[10].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[10].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[10].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot12TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 12.");
                        if (playerInventory.inventory[11].typeInSlot != "" && !playerInventory.inventory[11].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[11].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[11].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[11].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[11].typeInSlot, playerInventory.inventory[11].amountInSlot);
                                            playerInventory.inventory[11].typeInSlot = "nothing";
                                            playerInventory.inventory[11].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[11].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[11];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[11].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[11].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[11].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[11].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[11].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[11].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot13TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 13.");
                        if (playerInventory.inventory[12].typeInSlot != "" && !playerInventory.inventory[12].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[12].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[12].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[12].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[12].typeInSlot, playerInventory.inventory[12].amountInSlot);
                                            playerInventory.inventory[12].typeInSlot = "nothing";
                                            playerInventory.inventory[12].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[12].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[12];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[12].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[12].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[12].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[12].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[12].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[12].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot14TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 14.");
                        if (playerInventory.inventory[13].typeInSlot != "" && !playerInventory.inventory[13].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[13].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[13].typeInSlot) && slot.amountInSlot < 1000 - playerInventory.inventory[13].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[13].typeInSlot, playerInventory.inventory[13].amountInSlot);
                                            playerInventory.inventory[13].typeInSlot = "nothing";
                                            playerInventory.inventory[13].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[13].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[13];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[13].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[13].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[13].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[13].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[13].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[13].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot15TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 15.");
                        if (playerInventory.inventory[14].typeInSlot != "" && !playerInventory.inventory[14].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[14].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[14].typeInSlot) && slot.amountInSlot <= 1000 - playerInventory.inventory[14].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[14].typeInSlot, playerInventory.inventory[14].amountInSlot);
                                            playerInventory.inventory[14].typeInSlot = "nothing";
                                            playerInventory.inventory[14].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[14].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[14];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[14].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[14].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[14].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[14].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[14].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[14].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (inventorySlot16TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 16.");
                        if (playerInventory.inventory[15].typeInSlot != "" && !playerInventory.inventory[15].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[15].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerController.storageInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerInventory.inventory[15].typeInSlot) && slot.amountInSlot < 1000 - playerInventory.inventory[15].amountInSlot)
                                        {
                                            playerController.storageInventory.AddItem(playerInventory.inventory[15].typeInSlot, playerInventory.inventory[15].amountInSlot);
                                            playerInventory.inventory[15].typeInSlot = "nothing";
                                            playerInventory.inventory[15].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerInventory.inventory[15].typeInSlot;
                                    playerController.slotDraggingFrom = playerInventory.inventory[15];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerInventory.inventory[15].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[15].amountInSlot / 2;
                                        }
                                        else if (playerInventory.inventory[15].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerInventory.inventory[15].amountInSlot;
                                        }
                                    }
                                    else if (playerInventory.inventory[15].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[15].amountInSlot;
                                    }
                                }
                            }
                        }
                    }

                    //DRAGGING ITEMS FROM THE STORAGE CONTAINER
                    if (storageInventorySlot1TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 1.");
                        if (playerController.storageInventory.inventory[0].typeInSlot != "" && !playerController.storageInventory.inventory[0].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[0].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[0].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[0].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[0].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[0].typeInSlot, playerController.storageInventory.inventory[0].amountInSlot);
                                            playerController.storageInventory.inventory[0].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[0].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[0].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[0];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[0].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[0].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[0].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[0].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[0].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[0].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot2TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 2.");
                        if (playerController.storageInventory.inventory[1].typeInSlot != "" && !playerController.storageInventory.inventory[1].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[1].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[1].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[1].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[1].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[1].typeInSlot, playerController.storageInventory.inventory[1].amountInSlot);
                                            playerController.storageInventory.inventory[1].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[1].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[1].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[1];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[1].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[1].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[1].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[1].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[1].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[1].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot3TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 3.");
                        if (playerController.storageInventory.inventory[2].typeInSlot != "" && !playerController.storageInventory.inventory[2].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[2].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[2].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[2].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[2].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[2].typeInSlot, playerController.storageInventory.inventory[2].amountInSlot);
                                            playerController.storageInventory.inventory[2].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[2].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[2].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[2];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[2].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[2].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[2].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[2].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[2].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[2].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot4TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 4.");
                        if (playerController.storageInventory.inventory[3].typeInSlot != "" && !playerController.storageInventory.inventory[3].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[3].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[3].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[3].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[3].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[3].typeInSlot, playerController.storageInventory.inventory[3].amountInSlot);
                                            playerController.storageInventory.inventory[3].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[3].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[3].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[3];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[3].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[3].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[3].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[3].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[3].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[3].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot5TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 5.");
                        if (playerController.storageInventory.inventory[4].typeInSlot != "" && !playerController.storageInventory.inventory[4].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[4].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[4].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[4].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[4].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[4].typeInSlot, playerController.storageInventory.inventory[4].amountInSlot);
                                            playerController.storageInventory.inventory[4].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[4].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[4].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[4];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[4].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[4].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[4].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[4].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[4].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[4].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot6TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 6.");
                        if (playerController.storageInventory.inventory[5].typeInSlot != "" && !playerController.storageInventory.inventory[5].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[5].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[5].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[5].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[5].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[5].typeInSlot, playerController.storageInventory.inventory[5].amountInSlot);
                                            playerController.storageInventory.inventory[5].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[5].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[5].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[5];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[5].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[5].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[5].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[5].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[5].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[5].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot7TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 7.");
                        if (playerController.storageInventory.inventory[6].typeInSlot != "" && !playerController.storageInventory.inventory[6].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[6].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[6].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[6].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[6].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[6].typeInSlot, playerController.storageInventory.inventory[6].amountInSlot);
                                            playerController.storageInventory.inventory[6].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[6].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[6].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[6];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[6].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[6].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[6].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[6].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[6].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[6].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot8TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 8.");
                        if (playerController.storageInventory.inventory[7].typeInSlot != "" && !playerController.storageInventory.inventory[7].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[7].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[7].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[7].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[7].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[7].typeInSlot, playerController.storageInventory.inventory[7].amountInSlot);
                                            playerController.storageInventory.inventory[7].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[7].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[7].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[7];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[7].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[7].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[7].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[7].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[7].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[7].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot9TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 9.");
                        if (playerController.storageInventory.inventory[8].typeInSlot != "" && !playerController.storageInventory.inventory[8].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[8].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[8].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[8].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[8].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[8].typeInSlot, playerController.storageInventory.inventory[8].amountInSlot);
                                            playerController.storageInventory.inventory[8].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[8].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[8].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[8];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[8].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[8].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[8].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[8].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[8].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[8].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot10TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 10.");
                        if (playerController.storageInventory.inventory[9].typeInSlot != "" && !playerController.storageInventory.inventory[9].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[9].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[9].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[9].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[9].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[9].typeInSlot, playerController.storageInventory.inventory[9].amountInSlot);
                                            playerController.storageInventory.inventory[9].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[9].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[9].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[9];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[9].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[9].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[9].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[9].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[9].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[9].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot11TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 11.");
                        if (playerController.storageInventory.inventory[10].typeInSlot != "" && !playerController.storageInventory.inventory[10].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[10].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[10].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[10].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[10].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[10].typeInSlot, playerController.storageInventory.inventory[10].amountInSlot);
                                            playerController.storageInventory.inventory[10].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[10].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[10].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[10];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[10].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[10].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[10].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[10].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[10].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[10].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot12TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 12.");
                        if (playerController.storageInventory.inventory[11].typeInSlot != "" && !playerController.storageInventory.inventory[11].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[11].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[11].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[11].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[11].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[11].typeInSlot, playerController.storageInventory.inventory[11].amountInSlot);
                                            playerController.storageInventory.inventory[11].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[11].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[11].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[11];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[11].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[11].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[11].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[11].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[11].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[11].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot13TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 13.");
                        if (playerController.storageInventory.inventory[12].typeInSlot != "" && !playerController.storageInventory.inventory[12].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[12].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[12].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[12].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[12].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[12].typeInSlot, playerController.storageInventory.inventory[12].amountInSlot);
                                            playerController.storageInventory.inventory[12].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[12].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[12].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[12];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[12].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[12].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[12].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[12].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[12].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[12].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot14TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 14.");
                        if (playerController.storageInventory.inventory[13].typeInSlot != "" && !playerController.storageInventory.inventory[13].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[13].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[13].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[13].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[13].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[13].typeInSlot, playerController.storageInventory.inventory[13].amountInSlot);
                                            playerController.storageInventory.inventory[13].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[13].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[13].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[13];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[13].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[13].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[13].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[13].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[13].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[13].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot15TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 15.");
                        if (playerController.storageInventory.inventory[14].typeInSlot != "" && !playerController.storageInventory.inventory[14].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[14].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[14].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[14].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[14].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[14].typeInSlot, playerController.storageInventory.inventory[14].amountInSlot);
                                            playerController.storageInventory.inventory[14].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[14].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[14].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[14];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[14].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[14].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[14].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[14].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[14].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[14].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                    else if (storageInventorySlot16TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 16.");
                        if (playerController.storageInventory.inventory[15].typeInSlot != "" && !playerController.storageInventory.inventory[15].typeInSlot.Equals("nothing"))
                        {
                            if (playerController.remoteStorageActive == true)
                            {
                                GUI.Label(storageComputerMessageRect, playerController.storageInventory.inventory[15].typeInSlot);
                            }
                            else
                            {
                                GUI.Label(storageInventoryMessageRect, playerController.storageInventory.inventory[15].typeInSlot);
                            }
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    foreach (InventorySlot slot in playerInventory.inventory)
                                    {
                                        if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(playerController.storageInventory.inventory[15].typeInSlot) && slot.amountInSlot <= 1000 - playerController.storageInventory.inventory[15].amountInSlot)
                                        {
                                            playerInventory.AddItem(playerController.storageInventory.inventory[15].typeInSlot, playerController.storageInventory.inventory[15].amountInSlot);
                                            playerController.storageInventory.inventory[15].typeInSlot = "nothing";
                                            playerController.storageInventory.inventory[15].amountInSlot = 0;
                                        }
                                    }

                                }
                                else
                                {
                                    playerController.draggingItem = true;
                                    playerController.itemToDrag = playerController.storageInventory.inventory[15].typeInSlot;
                                    playerController.slotDraggingFrom = playerController.storageInventory.inventory[15];
                                    if (Input.GetKey(KeyCode.LeftShift))
                                    {
                                        if (playerController.storageInventory.inventory[15].amountInSlot > 1)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[15].amountInSlot / 2;
                                        }
                                        else if (playerController.storageInventory.inventory[15].amountInSlot > 0)
                                        {
                                            playerController.amountToDrag = playerController.storageInventory.inventory[15].amountInSlot;
                                        }
                                    }
                                    else if (playerController.storageInventory.inventory[15].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerController.storageInventory.inventory[15].amountInSlot;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //NO STORAGE CONTAINER IS OPEN. THE PLAYER IS DRAGGING AND DROPPING ITEMS WITHIN THEIR OWN INVENTORY
                    if (playerController.draggingItem == true)
                    {
                        //DROPPING ITEMS INTO THE PLAYER'S INVENTORY
                        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - ScreenWidth * 0.0145f, Event.current.mousePosition.y - ScreenHeight * 0.03f, (ScreenWidth * 0.025f), (ScreenHeight * 0.05f)), textureDictionary[playerController.itemToDrag]);
                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            playerController.draggingItem = false;
                            if (inventorySlot1TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[0])
                            {
                                if (playerInventory.inventory[0].typeInSlot == "nothing" || playerInventory.inventory[0].typeInSlot == "" || playerInventory.inventory[0].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[0].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 0);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[0].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[0].amountInSlot;
                                    playerInventory.inventory[0].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[0].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot2TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[1])
                            {
                                if (playerInventory.inventory[1].typeInSlot == "nothing" || playerInventory.inventory[1].typeInSlot == "" || playerInventory.inventory[1].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[1].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 1);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[1].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[1].amountInSlot;
                                    playerInventory.inventory[1].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[1].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot3TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[2])
                            {
                                if (playerInventory.inventory[2].typeInSlot == "nothing" || playerInventory.inventory[2].typeInSlot == "" || playerInventory.inventory[2].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[2].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 2);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[2].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[2].amountInSlot;
                                    playerInventory.inventory[2].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[2].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot4TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[3])
                            {
                                if (playerInventory.inventory[3].typeInSlot == "nothing" || playerInventory.inventory[3].typeInSlot == "" || playerInventory.inventory[3].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[3].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 3);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[3].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[3].amountInSlot;
                                    playerInventory.inventory[3].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[3].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot5TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[4])
                            {
                                if (playerInventory.inventory[4].typeInSlot == "nothing" || playerInventory.inventory[4].typeInSlot == "" || playerInventory.inventory[4].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[4].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 4);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[4].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[4].amountInSlot;
                                    playerInventory.inventory[4].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[4].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot6TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[5])
                            {
                                if (playerInventory.inventory[5].typeInSlot == "nothing" || playerInventory.inventory[5].typeInSlot == "" || playerInventory.inventory[5].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[5].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 5);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[5].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[5].amountInSlot;
                                    playerInventory.inventory[5].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[5].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot7TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[6])
                            {
                                if (playerInventory.inventory[6].typeInSlot == "nothing" || playerInventory.inventory[6].typeInSlot == "" || playerInventory.inventory[6].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[6].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 6);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[6].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[6].amountInSlot;
                                    playerInventory.inventory[6].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[6].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot8TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[7])
                            {
                                if (playerInventory.inventory[7].typeInSlot == "nothing" || playerInventory.inventory[7].typeInSlot == "" || playerInventory.inventory[7].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[7].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 7);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[7].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[7].amountInSlot;
                                    playerInventory.inventory[7].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[7].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot9TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[8])
                            {
                                if (playerInventory.inventory[8].typeInSlot == "nothing" || playerInventory.inventory[8].typeInSlot == "" || playerInventory.inventory[8].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[8].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 8);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[8].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[8].amountInSlot;
                                    playerInventory.inventory[8].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[8].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot10TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[9])
                            {
                                if (playerInventory.inventory[9].typeInSlot == "nothing" || playerInventory.inventory[9].typeInSlot == "" || playerInventory.inventory[9].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[9].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 9);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[9].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[9].amountInSlot;
                                    playerInventory.inventory[9].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[9].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot11TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[10])
                            {
                                if (playerInventory.inventory[10].typeInSlot == "nothing" || playerInventory.inventory[10].typeInSlot == "" || playerInventory.inventory[10].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[10].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 10);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[10].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[10].amountInSlot;
                                    playerInventory.inventory[10].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[10].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot12TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[11])
                            {
                                if (playerInventory.inventory[11].typeInSlot == "nothing" || playerInventory.inventory[11].typeInSlot == "" || playerInventory.inventory[11].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[11].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 11);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[11].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[11].amountInSlot;
                                    playerInventory.inventory[11].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[11].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot13TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[12])
                            {
                                if (playerInventory.inventory[12].typeInSlot == "nothing" || playerInventory.inventory[12].typeInSlot == "" || playerInventory.inventory[12].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[12].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 12);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[12].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[12].amountInSlot;
                                    playerInventory.inventory[12].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[12].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot14TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[13])
                            {
                                if (playerInventory.inventory[13].typeInSlot == "nothing" || playerInventory.inventory[13].typeInSlot == "" || playerInventory.inventory[13].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[13].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 13);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[13].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[13].amountInSlot;
                                    playerInventory.inventory[13].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[13].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot15TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[14])
                            {
                                if (playerInventory.inventory[14].typeInSlot == "nothing" || playerInventory.inventory[14].typeInSlot == "" || playerInventory.inventory[14].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[14].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 14);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[14].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[14].amountInSlot;
                                    playerInventory.inventory[14].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[14].amountInSlot = playerController.amountToDrag;
                                }
                            }
                            if (inventorySlot16TexRect.Contains(mousePos) && playerController.slotDraggingFrom != playerInventory.inventory[15])
                            {
                                if (playerInventory.inventory[15].typeInSlot == "nothing" || playerInventory.inventory[15].typeInSlot == "" || playerInventory.inventory[15].typeInSlot == playerController.itemToDrag)
                                {
                                    if (playerInventory.inventory[15].amountInSlot <= playerInventory.maxStackSize - playerController.amountToDrag)
                                    {
                                        playerInventory.AddItemToSlot(playerController.itemToDrag, playerController.amountToDrag, 15);
                                        playerController.slotDraggingFrom.amountInSlot -= playerController.amountToDrag;
                                        if (playerController.slotDraggingFrom.amountInSlot <= 0)
                                        {
                                            playerController.slotDraggingFrom.typeInSlot = "nothing";
                                        }
                                    }
                                }
                                else
                                {
                                    playerController.slotDraggingFrom.typeInSlot = playerInventory.inventory[15].typeInSlot;
                                    playerController.slotDraggingFrom.amountInSlot = playerInventory.inventory[15].amountInSlot;
                                    playerInventory.inventory[15].typeInSlot = playerController.itemToDrag;
                                    playerInventory.inventory[15].amountInSlot = playerController.amountToDrag;
                                }
                            }
                        }
                    }

                    //DRAGGING ITEMS FROM THE PLAYER'S INVENTORY
                    if (inventorySlot1TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 1.");
                        if (playerInventory.inventory[0].typeInSlot != "" && !playerInventory.inventory[0].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[0].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[0].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[0];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[0].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[0].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[0].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[0].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[0].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[0].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot2TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 2.");
                        if (playerInventory.inventory[1].typeInSlot != "" && !playerInventory.inventory[1].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[1].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[1].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[1];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[1].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[1].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[1].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[1].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[1].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[1].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot3TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 3.");
                        if (playerInventory.inventory[2].typeInSlot != "" && !playerInventory.inventory[2].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[2].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[2].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[2];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[2].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[2].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[2].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[2].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[2].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[2].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot4TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 4.");
                        if (playerInventory.inventory[3].typeInSlot != "" && !playerInventory.inventory[3].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[3].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[3].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[3];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[3].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[3].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[3].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[3].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[3].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[3].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot5TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 5.");
                        if (playerInventory.inventory[4].typeInSlot != "" && !playerInventory.inventory[4].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[4].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[4].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[4];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[4].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[4].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[4].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[4].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[4].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[4].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot6TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 6.");
                        if (playerInventory.inventory[5].typeInSlot != "" && !playerInventory.inventory[5].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[5].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[5].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[5];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[5].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[5].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[5].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[5].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[5].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[5].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot7TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 7.");
                        if (playerInventory.inventory[6].typeInSlot != "" && !playerInventory.inventory[6].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[6].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[6].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[6];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[6].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[6].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[6].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[6].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[6].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[6].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot8TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 8.");
                        if (playerInventory.inventory[7].typeInSlot != "" && !playerInventory.inventory[7].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[7].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[7].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[7];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[7].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[7].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[7].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[7].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[7].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[7].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot9TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 9.");
                        if (playerInventory.inventory[8].typeInSlot != "" && !playerInventory.inventory[8].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[8].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[8].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[8];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[8].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[8].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[8].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[8].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[8].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[8].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot10TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 10.");
                        if (playerInventory.inventory[9].typeInSlot != "" && !playerInventory.inventory[9].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[9].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[9].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[9];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[9].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[9].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[9].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[9].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[9].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[9].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot11TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 11.");
                        if (playerInventory.inventory[10].typeInSlot != "" && !playerInventory.inventory[10].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[10].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[10].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[10];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[10].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[10].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[10].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[10].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[10].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[10].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot12TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 12.");
                        if (playerInventory.inventory[11].typeInSlot != "" && !playerInventory.inventory[11].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[11].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[11].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[11];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[11].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[11].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[11].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[11].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[11].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[11].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot13TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 13.");
                        if (playerInventory.inventory[12].typeInSlot != "" && !playerInventory.inventory[12].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[12].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[12].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[12];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[12].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[12].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[12].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[12].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[12].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[12].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot14TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 14.");
                        if (playerInventory.inventory[13].typeInSlot != "" && !playerInventory.inventory[13].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[13].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[13].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[13];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[13].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[13].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[13].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[13].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[13].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[13].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot15TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 15.");
                        if (playerInventory.inventory[14].typeInSlot != "" && !playerInventory.inventory[14].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[14].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[14].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[14];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[14].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[14].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[14].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[14].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[14].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[14].amountInSlot;
                                }
                            }
                        }
                    }
                    else if (inventorySlot16TexRect.Contains(mousePos))
                    {
                        //Debug.Log("Mouse in inventory slot 16.");
                        if (playerInventory.inventory[15].typeInSlot != "" && !playerInventory.inventory[15].typeInSlot.Equals("nothing"))
                        {
                            GUI.Label(inventoryMesageRect, playerInventory.inventory[15].typeInSlot);
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                playerController.draggingItem = true;
                                playerController.itemToDrag = playerInventory.inventory[15].typeInSlot;
                                playerController.slotDraggingFrom = playerInventory.inventory[15];
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    if (playerInventory.inventory[15].amountInSlot > 1)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[15].amountInSlot / 2;
                                    }
                                    else if (playerInventory.inventory[15].amountInSlot > 0)
                                    {
                                        playerController.amountToDrag = playerInventory.inventory[15].amountInSlot;
                                    }
                                }
                                else if (playerInventory.inventory[15].amountInSlot > 0)
                                {
                                    playerController.amountToDrag = playerInventory.inventory[15].amountInSlot;
                                }
                            }
                        }
                    }
                }

                //MESSAGE TELLING THE PLAYER THEY ARE MISSING THE ITEMS REQUIRED TO CRAFT AN OBJECT
                if (playerCrafting.missingItem == true)
                {
                    if (missingItemTimer < 3)
                    {
                        GUI.Label(inventoryMesageRect, "Missing items.");
                        missingItemTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        playerCrafting.missingItem = false;
                        missingItemTimer = 0;
                    }
                }

                //MESSAGE TELLING THE PLAYER THEIR INVENTORY IS FULL
                if (playerController.outOfSpace == true)
                {
                    if (playerController.outOfSpaceTimer < 3)
                    {
                        GUI.Label(inventoryMesageRect, "\nNo space in inventory.");
                        playerController.outOfSpaceTimer += 1 * Time.deltaTime;
                    }
                    else
                    {
                        playerController.outOfSpace = false;
                        playerController.outOfSpaceTimer = 0;
                    }
                }

                //BUTTON WHICH OPENS THE CRAFTING GUI
                if (playerController.storageGUIopen == false)
                {
                    if (GUI.Button(craftingButtonRect, "CRAFTING"))
                    {
                        if (playerController.craftingGUIopen == false && playerController.storageGUIopen == false)
                        {
                            playerController.craftingGUIopen = true;
                        }
                        else
                        {
                            playerController.craftingGUIopen = false;
                        }
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                }

                //BUTTON THAT CLOSES THE INVENTORY GUI
                if (GUI.Button(closeButtonRect, "CLOSE"))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.inventoryOpen = false;
                    playerController.craftingGUIopen = false;
                    playerController.storageGUIopen = false;
                    playerController.guiSound.volume = 0.15f;
                    playerController.guiSound.clip = playerController.buttonClip;
                    playerController.guiSound.Play();
                }

                //CRAFTING GUI
                if (playerController.craftingGUIopen == true)
                {
                    if (craftingPage == 0)
                    {
                        if (button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Storage container for objects and items. Can be used to manually store items or connected to machines for automation. Universal conduits, dark matter conduits, retrievers and auto crafters can all connect to storage containers.\n\n[CRAFTING]\n6x Iron Plate");
                        }
                        if (button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Extracts regolith from the lunar surface which can be pressed into bricks or smelted to create glass. Glass blocks have a 100% chance of being destroyed by meteors and other hazards. Bricks have a 75% chance of being destroyed by meteors and other hazards. Augers must be placed directly on the lunar surface and require power from a solar panel, nuclear reactor or power conduit.\n\n[CRAFTING]\n10x Iron Ingot\n10x Copper Ingot");
                        }
                        if (button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Creates wire from copper and aluminum ingots. Creates pipes from iron and steel ingots. Ingots must be supplied to the extruder using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. The extruder requires power from a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Ingot\n10x Copper Ingot");
                        }
                        if (button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Presses iron, copper, aluminum or tin ingots into plates. Ingots must be supplied to the press using universal conduits. Another universal conduit should be placed within 2 meters of the machine to accept the output. Must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Ingot\n10x Iron Pipe\n10x Copper Wire");
                        }
                        if (button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Cuts plates into gears. Plates must be supplied to the gear cutter using universal conduits. Place another conduit within 2 meters of the machine for the output. The gear cutter must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The gear cutter has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Aluminum Wire\n10x Copper Wire\n5x Iron Plate\n5x Tin Plate\n5x Iron Pipe");
                        }
                        if (button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Extracts ore, coal and ice from deposits found on the lunar surface. Place within 2 meters of the desired resource and use a universal conduit to handle the harvested materials. When extracting ice, the extractor will not need a heat exchanger for cooling. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n10x Iron Plate\n10x Iron Pipe\n10x Copper Wire\n10x Dark Matter");
                        }
                        if (button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Transfers items from a machine to another universal conduit, another machine or a storage container. Universal conduits have an adjustable input/output range and do not require power to operate.\n\n[CRAFTING]\n5x Iron Pipe\n5x Iron Plate\n5x Copper Wire\n5x Dark Matter");
                        }
                        if (button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Retrieves items from a storage container and transfers them to a universal conduit. Place an item of each desired type into the retrievers inventory to designate that item for retrieval. Place within 2 meters of a storage container and a universal conduit. This machine requires power and it's output is adjustable. If the retriever is moving ice, it will not require cooling. The retriever's output is measured in items per cycle.\n\n[CRAFTING]\n4x Iron Plate\n4x Copper Wire\n2x Iron Pipe\n2x Electric Motor\n2x Circuit Board");
                        }
                        if (button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Provides 1 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple solar panels can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[CRAFTING]\n4x Iron Pipe\n4x Iron Plate\n4x Copper Wire\n4x Copper Plate\n4x Glass Block");
                        }
                        if (button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Provides 10 MW of power to a single machine or power conduit. Must be placed within 4 meters of the machine. Multiple generators can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate. Generators must be connected to a universal conduit supplying coal for fuel.\n\n[CRAFTING]\n4x Iron Plate\n4x Copper Wire\n2x Iron Gear\n2x Iron Pipe\n1x Smelter\n1x Electric Motor");
                        }
                        if (button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Nuclear reactors are used to drive reactor turbines. Turbines must be directly attached to the reactor. The reactor will require a heat exchanger providing 5 KBTU cooling per turbine.\n\n[CRAFTING]\n10x Steel Pipe\n10x Steel Plate\n10x Copper Wire\n10x Copper Plate\n10x Glass Block\n10x Dark Matter");
                        }
                        if (button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Provides 100 MW of power to a single machine or power conduit. Reactor turbines must be directly attached to a properly functioning, adequately cooled nuclear reactor. Must be placed within 4 meters of the machine. Multiple reactor turbines can be connected to a machine or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power, the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate.\n\n[CRAFTING]\n4x Steel Plate\n4x Copper Wire\n2x Steel Gear\n2x Steel Pipe\n1x Generator\n1x Glass Block");
                        }
                        if (button14Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Transfers power from a power source to a machine or to another power conduit. When used with two outputs, power will be distributed evenly. This machine has an adjustable range setting.\n\n[CRAFTING]\n4x Aluminum Plate\n4x Copper Wire\n4x Glass Block");
                        }
                        if (button15Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Cools down a machine to allow overclocking. Requires a supply of ice from a universal conduit. Increasing the output of the heat exchanger increases the amount of ice required. This can be compensated for by overclocking the extractor that is supplying the ice. Machines cannot be connected to more than one heat exchanger. The heat exchanger's output is measured in KBTU and will consume 1 ice per 1 KBTU of cooling each cycle.\n\n[CRAFTING]\n10x Steel Plate\n10x Steel Pipe");
                        }
                        if (button17Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Smelts ore into ingots. Can also be used to make glass when supplied with regolith. Ore must be supplied to the smelter using universal conduits. Place another conduit within 2 meters of the machine for the output. This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit. The output of a smelter is measured in items per cycle.\n\n[CRAFTING]\n10x Iron Plate\n10x Copper Wire\n5x Iron Pipe");
                        }
                        if (button18Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Combines tin and copper ingots to make bronze ingots. Combines coal and iron ingots to make steel ingots. Requres 3 conduits. 1 for each input and 1 for the output. Requires a power source such as a solar panel, nuclear reactor or power conduit. The alloy smelter has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n40x Copper Wire\n40x Aluminum Wire\n20x Iron Plate\n20x Tin Plate\n20x Iron Pipe\n20x Iron Gear");
                        }
                        if (button19Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Harvests dark matter which is then transferred to a dark matter conduit. Requires a power source such as a solar panel, nuclear reactor or power conduit. The dark matter collector has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n100x Dark Matter\n100x Copper Wire\n100x Aluminum Wire\n50x Steel Plate\n50x Steel Pipe\n50x Steel Gear\n50x Tin Gear\n50x Bronze Gear");
                        }
                        if (button20Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Transfers dark matter from a collector to a storage container or another conduit. Dark matter conduits have an adjustable input/output range and do not require power to operate.\n\n[CRAFTING]\n50x Dark Matter\n50x Copper Wire\n50x Aluminum Wire\n25x Steel Plate\n25x Steel Pipe\n25x Steel Gear\n25x Tin Gear\n25x Bronze Gear");
                        }
                        if (button21Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Automatically crafts objects using items from an attached storage container. Place within 2 meters of the storage container. Then, place an item of the desired type into the auto crafter's inventory. This will designate that item as the item to be crafted. Crafted items will be deposited into the attached storage container. This machine requires power and has an adjustable output measured in items per cycle.\n\n[CRAFTING]\n4x Bronze Gear\n4x Steel Plate\n4x Electric Motor\n4x Circuit Board\n4x Dark Matter");
                        }
                        if (button22Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Provides a waypoint for rail carts. Has an adjustable range at which the next hub will be located and rails deployed to it's location. Rail cart hubs can be configured to stop the rail cart so it can be loaded and unloaded.\n\n[CRAFTING]\n10x Iron Pipe\n6x Iron Plate\n1x Circuit Board");
                        }
                        if (button23Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "A mobile storage container that rides on rails from one rail cart hub to the next. Configure the hubs to stop the cart near a conduit or retriever so it can be loaded or unloaded. Must be placed on a rail cart hub.\n\n[CRAFTING]\n10x Copper Wire\n8x Aluminum Gear\n4x Tin Plate\n2x Electric Motor\n1x Solar Panel\n1x Storage Container");
                        }

                        GUI.DrawTexture(craftingBackgroundRect, craftingBackground);
                        if (GUI.Button(button1Rect, "Storage Container"))
                        {
                            playerCrafting.CraftStorageContainer();
                        }
                        if (GUI.Button(button2Rect, "Auger"))
                        {
                            playerCrafting.CraftAuger();
                        }
                        if (GUI.Button(button3Rect, "Extruder"))
                        {
                            playerCrafting.CraftExtruder();
                        }
                        if (GUI.Button(button4Rect, "Press"))
                        {
                            playerCrafting.CraftPress();
                        }
                        if (GUI.Button(button5Rect, "Gear Cutter"))
                        {
                            playerCrafting.CraftGearCutter();
                        }
                        if (GUI.Button(button6Rect, "Universal Extractor"))
                        {
                            playerCrafting.CraftUniversalExtractor();
                        }
                        if (GUI.Button(button7Rect, "Universal Conduit"))
                        {
                            playerCrafting.CraftUniversalConduit();
                        }
                        if (GUI.Button(button9Rect, "Retriever"))
                        {
                            playerCrafting.CraftRetriever();
                        }
                        if (GUI.Button(button10Rect, "Solar Panel"))
                        {
                            playerCrafting.CraftSolarPanel();
                        }
                        if (GUI.Button(button11Rect, "Generator"))
                        {
                            playerCrafting.CraftGenerator();
                        }
                        if (GUI.Button(button12Rect, "Nuclear Reactor"))
                        {
                            playerCrafting.CraftNuclearReactor();
                        }
                        if (GUI.Button(button13Rect, "Reactor Turbine"))
                        {
                            playerCrafting.CraftReactorTurbine();
                        }
                        if (GUI.Button(button14Rect, "Power Conduit"))
                        {
                            playerCrafting.CraftPowerConduit();
                        }
                        if (GUI.Button(button15Rect, "Heat Exchanger"))
                        {
                            playerCrafting.CraftHeatExchanger();
                        }
                        if (GUI.Button(button17Rect, "Smelter"))
                        {
                            playerCrafting.CraftSmelter();
                        }
                        if (GUI.Button(button18Rect, "Alloy Smelter"))
                        {
                            playerCrafting.CraftAlloySmelter();
                        }
                        if (GUI.Button(button19Rect, "DM Collector"))
                        {
                            playerCrafting.CraftDarkMatterCollector();
                        }
                        if (GUI.Button(button20Rect, "DM Conduit"))
                        {
                            playerCrafting.CraftDarkMatterConduit();
                        }
                        if (GUI.Button(button21Rect, "Auto Crafter"))
                        {
                            playerCrafting.CraftAutoCrafter();
                        }
                        if (GUI.Button(button22Rect, "Rail Cart Hub"))
                        {
                            playerCrafting.CraftRailCartHub();
                        }
                        if (GUI.Button(button23Rect, "Rail Cart"))
                        {
                            playerCrafting.CraftRailCart();
                        }
                    }
                    if (craftingPage == 1)
                    {
                        if (button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Iron block for building structures. Iron blocks have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Iron Plate");
                        }
                        if (button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Iron ramp for building structures. Iron ramps have a 25% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Iron Plate");
                        }
                        if (button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Steel block for building structures. Steel blocks have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Steel Plate");
                        }
                        if (button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Steel ramp for building structures. Steel ramps have a 1% chance of being destroyed by meteors and other hazards. 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.\n\n[CRAFTING]\n1x Steel Plate");
                        }
                        if (button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Hatchway used for entering structures.\n\n[CRAFTING]\n1x Tin Plate\n1x Dark Matter");
                        }
                        if (button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "For interior lighting. Requires power from a solar panel, nuclear reactor or power conduit.\n\n[CRAFTING]\n2x Copper Wire\n1x Glass Block\n1x Tin Plate");
                        }
                        if (button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "A combination of conductive, semi-conductive and insulating materials combined to create a logic processing circuit.\n\n[CRAFTING]\n2x Copper Wire\n1x Glass Block\n1x Tin Plate\n1x Dark Matter");
                        }
                        if (button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "A device that converts electrical energy to mechanical torque.\n\n[CRAFTING]\n10x Copper Wire\n2x Iron Plate\n1x Iron Pipe\n2x Iron Gear");
                        }
                        if (button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Provides access to all stationary storage containers within 4 meters. Can be accessed manually or connected to retrievers, auto crafters and conduits. When a conduit is connectd to the computer, the computer will store items starting with the first container found to have space available. When a retriever is connected to the computer, the computer will search all of the managed containers for desired items.\n\n[CRAFTING]\n10x Copper Wire\n10x Tin Gear\n5x Retriever\n5x Universal Conduit\n5x Aluminum Plate\n1x Dark Matter Conduit\n1x Glass Block");
                        }
                        if (button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(craftingInfoBackgroundRect, menuBackground);
                            GUI.Label(craftingInfoRect, "Protects your equipment from meteor showers and other hazards. Requires a power source such as a solar panel, nuclear reactor or power conduit. Turrets have an adjustable output measured in rounds per minute.\n\n[CRAFTING]\n10x Copper Wire\n10x Aluminum Wire\n5x Steel Plate\n5x Steel Pipe\n5x Steel Gear\n5x Bronze Plate\n4x Electric Motor\n4x Circuit Board");
                        }

                        GUI.DrawTexture(craftingBackgroundRect, craftingBackground);
                        if (GUI.Button(button1Rect, "Iron Block"))
                        {
                            playerCrafting.CraftIronBlock();
                        }
                        if (GUI.Button(button2Rect, "Iron Ramp"))
                        {
                            playerCrafting.CraftIronRamp();
                        }
                        if (GUI.Button(button3Rect, "Steel Block"))
                        {
                            playerCrafting.CraftSteelBlock();
                        }
                        if (GUI.Button(button4Rect, "Steel Ramp"))
                        {
                            playerCrafting.CraftSteelRamp();
                        }
                        if (GUI.Button(button5Rect, "Quantum Hatchway"))
                        {
                            playerCrafting.CraftQuantumHatchway();
                        }
                        if (GUI.Button(button6Rect, "Electric Light"))
                        {
                            playerCrafting.CraftElectricLight();
                        }
                        if (GUI.Button(button7Rect, "Circuit Board"))
                        {
                            playerCrafting.CraftCircuitBoard();
                        }
                        if (GUI.Button(button9Rect, "Electric Motor"))
                        {
                            playerCrafting.CraftMotor();
                        }
                        if (GUI.Button(button10Rect, "Storage Computer"))
                        {
                            playerCrafting.CraftStorageComputer();
                        }
                        if (GUI.Button(button11Rect, "Turret"))
                        {
                            playerCrafting.CraftTurret();
                        }

                    }
                    if (GUI.Button(craftingPreviousRect, "<-"))
                    {
                        if (craftingPage > 0)
                        {
                            craftingPage -= 1;
                        }
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                    if (GUI.Button(craftingNextRect, "->"))
                    {
                        if (craftingPage < 1)
                        {
                            craftingPage += 1;
                        }
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                }
            }
            else if (playerController.machineGUIopen == true && playerController.objectInSight != null) //MACHINE CONTROL GUI
            {
                if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                {
                    if (playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed == false)
                    {
                        GUI.DrawTexture(FourButtonSpeedControlBGRect, menuBackground);
                        if (GUI.Button(outputControlButton3Rect, "Dual Output: " + playerController.objectInSight.GetComponent<PowerConduit>().dualOutput))
                        {
                            if (playerController.objectInSight.GetComponent<PowerConduit>().dualOutput == true)
                            {
                                playerController.objectInSight.GetComponent<PowerConduit>().dualOutput = false;
                            }
                            else
                            {
                                playerController.objectInSight.GetComponent<PowerConduit>().dualOutput = true;
                            }
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                        if (GUI.Button(outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(speedControlBGRect, menuBackground);
                        GUI.Label(outputLabelRect, "Offline");
                        if (GUI.Button(outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<PowerConduit>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<RailCartHub>() != null)
                {
                    if (playerController.objectInSight.GetComponent<RailCartHub>().connectionFailed == false)
                    {
                        if (hubStopWindowOpen == false)
                        {
                            GUI.DrawTexture(FourButtonSpeedControlBGRect, menuBackground);
                            GUI.Label(outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<RailCartHub>().range = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<RailCartHub>().range, 6, 120);
                            if (GUI.Button(outputControlButton3Rect, "Stop Settings"))
                            {
                                hubStopWindowOpen = true;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                            if (GUI.Button(outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                        else
                        {
                            GUI.DrawTexture(FiveButtonSpeedControlBGRect, menuBackground);
                            GUI.Label(longOutputLabelRect, "Stop Time");
                            if (GUI.Button(outputControlButton0Rect, "Stop: " + playerController.objectInSight.GetComponent<RailCartHub>().stop))
                            {
                                if (playerController.objectInSight.GetComponent<RailCartHub>().stop == true)
                                {
                                    playerController.objectInSight.GetComponent<RailCartHub>().stop = false;
                                }
                                else
                                {
                                    playerController.objectInSight.GetComponent<RailCartHub>().stop = true;
                                }
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                            playerController.objectInSight.GetComponent<RailCartHub>().stopTime = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<RailCartHub>().stopTime, 0, 600);
                            if (GUI.Button(outputControlButton3Rect, "Range Settings"))
                            {
                                hubStopWindowOpen = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                            if (GUI.Button(outputControlButton4Rect, "Close"))
                            {
                                playerController.machineGUIopen = false;
                                hubStopWindowOpen = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(speedControlBGRect, menuBackground);
                        GUI.Label(outputLabelRect, "Offline");
                        if (GUI.Button(outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<RailCartHub>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<RailCartHub>().connectionFailed = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<Retriever>() != null)
                {
                    if (playerController.objectInSight.GetComponent<Retriever>().connectionFailed == false)
                    {
                        GUI.DrawTexture(FourButtonSpeedControlBGRect, menuBackground);
                        if (GUI.Button(outputControlButton3Rect, "Choose Items"))
                        {
                            if (playerController.objectInSight.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                        if (GUI.Button(outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(speedControlBGRect, menuBackground);
                        GUI.Label(outputLabelRect, "Offline");
                        if (GUI.Button(outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<Retriever>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<Retriever>().connectionFailed = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                {
                    if (playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed == false)
                    {
                        GUI.DrawTexture(FourButtonSpeedControlBGRect, menuBackground);
                        if (GUI.Button(outputControlButton3Rect, "Choose Item"))
                        {
                            if (playerController.objectInSight.GetComponent<InventoryManager>().initialized == true)
                            {
                                playerController.inventoryOpen = true;
                                playerController.storageGUIopen = true;
                                playerController.machineGUIopen = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                        if (GUI.Button(outputControlButton4Rect, "Close"))
                        {
                            playerController.machineGUIopen = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                    else
                    {
                        GUI.DrawTexture(speedControlBGRect, menuBackground);
                        GUI.Label(outputLabelRect, "Offline");
                        if (GUI.Button(outputControlButton2Rect, "Reboot"))
                        {
                            playerController.objectInSight.GetComponent<AutoCrafter>().connectionAttempts = 0;
                            playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed = false;
                            playerController.guiSound.volume = 0.15f;
                            playerController.guiSound.clip = playerController.buttonClip;
                            playerController.guiSound.Play();
                        }
                    }
                }
                else if (playerController.objectInSight.GetComponent<RailCart>() == null)
                {
                    GUI.DrawTexture(speedControlBGRect, menuBackground);
                    if (GUI.Button(outputControlButton3Rect, "Close"))
                    {
                        playerController.machineGUIopen = false;
                        playerController.guiSound.volume = 0.15f;
                        playerController.guiSound.clip = playerController.buttonClip;
                        playerController.guiSound.Play();
                    }
                }

                if (playerController.objectInSight.GetComponent<UniversalConduit>() != null || playerController.objectInSight.GetComponent<DarkMatterConduit>() != null || playerController.objectInSight.GetComponent<PowerConduit>() != null)
                {
                    if (playerController.objectInSight.GetComponent<UniversalConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<UniversalConduit>().connectionFailed == false)
                        {
                            GUI.Label(outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<UniversalConduit>().range = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<UniversalConduit>().range, 6, 120);
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<UniversalConduit>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<UniversalConduit>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<PowerConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<PowerConduit>().connectionFailed == false)
                        {
                            GUI.Label(outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<PowerConduit>().range = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<PowerConduit>().range, 6, 120);
                        }
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterConduit>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionFailed == false)
                        {
                            GUI.Label(outputLabelRect, "Range");
                            playerController.objectInSight.GetComponent<DarkMatterConduit>().range = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<DarkMatterConduit>().range, 6, 120);
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<DarkMatterConduit>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
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
                                        GUI.Label(outputLabelRect, "Output");
                                        playerController.objectInSight.GetComponent<HeatExchanger>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<HeatExchanger>().speed, 0, playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().speed);
                                    }
                                    else
                                    {
                                        GUI.Label(outputLabelRect, "Offline");
                                        if (GUI.Button(outputControlButton2Rect, "Reboot"))
                                        {
                                            playerController.objectInSight.GetComponent<HeatExchanger>().connectionAttempts = 0;
                                            playerController.objectInSight.GetComponent<HeatExchanger>().connectionFailed = false;
                                            playerController.guiSound.volume = 0.15f;
                                            playerController.guiSound.clip = playerController.buttonClip;
                                            playerController.guiSound.Play();
                                        }
                                    }
                                }
                                else
                                {
                                    //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input speed is zero");
                                    GUI.Label(outputLabelRect, "No Input");
                                }
                            }
                            else
                            {
                                //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input object is not recognized as a conduit");
                                GUI.Label(outputLabelRect, "No Input");
                            }
                        }
                        else
                        {
                            //Debug.Log(playerController.objectInSight.GetComponent<HeatExchanger>().ID + " input object is null");
                            GUI.Label(outputLabelRect, "No Input");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<PowerSource>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<PowerSource>().connectionFailed == true)
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<PowerSource>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<PowerSource>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Online");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Auger>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Auger>().power > 0)
                        {
                            GUI.Label(outputLabelRect, "Output");
                            playerController.objectInSight.GetComponent<Auger>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Auger>().speed, 0, playerController.objectInSight.GetComponent<Auger>().power);
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "No Power");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<UniversalExtractor>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<UniversalExtractor>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<UniversalExtractor>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<UniversalExtractor>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<UniversalExtractor>().speed, 0, playerController.objectInSight.GetComponent<UniversalExtractor>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<UniversalExtractor>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<UniversalExtractor>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<DarkMatterCollector>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<DarkMatterCollector>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<DarkMatterCollector>().speed, 0, playerController.objectInSight.GetComponent<DarkMatterCollector>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<DarkMatterCollector>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Smelter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Smelter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Smelter>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Smelter>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Smelter>().speed, 0, playerController.objectInSight.GetComponent<Smelter>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Smelter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Smelter>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<AlloySmelter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<AlloySmelter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<AlloySmelter>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<AlloySmelter>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<AlloySmelter>().speed, 0, playerController.objectInSight.GetComponent<AlloySmelter>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<AlloySmelter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<AlloySmelter>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Press>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Press>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Press>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Press>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Press>().speed, 0, playerController.objectInSight.GetComponent<Press>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Press>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Press>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Extruder>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Extruder>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Extruder>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Extruder>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Extruder>().speed, 0, playerController.objectInSight.GetComponent<Extruder>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Extruder>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Extruder>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Retriever>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Retriever>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<Retriever>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<Retriever>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Retriever>().speed, 0, playerController.objectInSight.GetComponent<Retriever>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<Retriever>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<Retriever>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<AutoCrafter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<AutoCrafter>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<AutoCrafter>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<AutoCrafter>().speed, 0, playerController.objectInSight.GetComponent<AutoCrafter>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<AutoCrafter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<AutoCrafter>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                    if (playerController.objectInSight.GetComponent<Turret>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<Turret>().power > 0)
                        {
                            GUI.Label(outputLabelRect, "Output");
                            if (playerController.objectInSight.GetComponent<Turret>().power < 30)
                            {
                                playerController.objectInSight.GetComponent<Turret>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Turret>().speed, 0, playerController.objectInSight.GetComponent<Turret>().power);
                            }
                            else
                            {
                                playerController.objectInSight.GetComponent<Turret>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<Turret>().speed, 0, 30);
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "No Power");
                        }
                    }
                    if (playerController.objectInSight.GetComponent<GearCutter>() != null)
                    {
                        if (playerController.objectInSight.GetComponent<GearCutter>().connectionFailed == false)
                        {
                            if (playerController.objectInSight.GetComponent<GearCutter>().power > 0)
                            {
                                GUI.Label(outputLabelRect, "Output");
                                playerController.objectInSight.GetComponent<GearCutter>().speed = (int)GUI.HorizontalSlider(outputControlButton2Rect, playerController.objectInSight.GetComponent<GearCutter>().speed, 0, playerController.objectInSight.GetComponent<GearCutter>().power);
                            }
                            else
                            {
                                GUI.Label(outputLabelRect, "No Power");
                            }
                        }
                        else
                        {
                            GUI.Label(outputLabelRect, "Offline");
                            if (GUI.Button(outputControlButton2Rect, "Reboot"))
                            {
                                playerController.objectInSight.GetComponent<GearCutter>().connectionAttempts = 0;
                                playerController.objectInSight.GetComponent<GearCutter>().connectionFailed = false;
                                playerController.guiSound.volume = 0.15f;
                                playerController.guiSound.clip = playerController.buttonClip;
                                playerController.guiSound.Play();
                            }
                        }
                    }
                }
            }
            else
            {
                hubStopWindowOpen = false;
                gameObject.GetComponent<MSCameraController>().enabled = true;
                if (playerController.crosshairEnabled == true && playerController.objectInSight == null && playerController.escapeMenuOpen == false && playerController.tabletOpen == false && playerController.paintGunActive == false || playerController.crosshairEnabled == true && playerController.building == true)
                {
                    GUI.DrawTexture(crosshairRect, crosshair); //ONLY DRAW THE CROSSHAIR WHEN INTERFACES ARE CLOSED
                }
            }
        }
    }
}
