using UnityEngine;
using System.Collections.Generic;

public class CraftingGUI : MonoBehaviour
{
    private PlayerController playerController;
    private CraftingManager craftingManager;
    private CraftingDictionary craftingDictionary;
    private TextureDictionary textureDictionary;
    private GuiCoordinates guiCoordinates;
    private Descriptions descriptions;
    private int craftingPage;
    private int modCraftingIndex;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textureDictionary = gameManager.GetComponent<TextureDictionary>();
        craftingManager = GetComponent<CraftingManager>();
        craftingDictionary = new CraftingDictionary();
        guiCoordinates = new GuiCoordinates();
        descriptions = new Descriptions();
    }

    //! Called by unity engine for rendering and handling GUI events.
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

        if (!playerController.stateManager.Busy() && GetComponent<MainMenu>().finishedLoading == true)
        {
            if (playerController.inventoryOpen == true)
            {
                if (playerController.craftingGUIopen == true)
                {
                    if (craftingPage == 0)
                    {
                        if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Storage Container"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.storageContainer + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Auger"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.auger + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Extruder"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.extruder + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Press"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.press + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Gear Cutter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.gearCutter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Universal Extractor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.universalExtractor + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Universal Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.universalConduit + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Retriever"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.retriever + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Solar Panel"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.solarPanel + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Generator"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.generator + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Nuclear Reactor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.nuclearReactor + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Reactor Turbine"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.reactorTurbine + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Power Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.powerConduit + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button15Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Heat Exchanger"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.heatExchanger + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button17Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Smelter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.smelter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button18Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Alloy Smelter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.alloySmelter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button19Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Dark Matter Collector"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.darkMatterCollector + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button20Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Dark Matter Conduit"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.darkMatterConduit + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button21Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Auto Crafter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.autoCrafter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button22Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Rail Cart Hub"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.railCartHub + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button23Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Rail Cart"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.railCart + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        if (GUI.Button(guiCoordinates.button1Rect, "Storage Container"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Storage Container"]);
                        }
                        if (GUI.Button(guiCoordinates.button2Rect, "Auger"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Auger"]);
                        }
                        if (GUI.Button(guiCoordinates.button3Rect, "Extruder"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Extruder"]);
                        }
                        if (GUI.Button(guiCoordinates.button4Rect, "Press"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Press"]);
                        }
                        if (GUI.Button(guiCoordinates.button5Rect, "Gear Cutter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Gear Cutter"]);
                        }
                        if (GUI.Button(guiCoordinates.button6Rect, "Universal Extractor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Universal Extractor"]);
                        }
                        if (GUI.Button(guiCoordinates.button7Rect, "Universal Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Universal Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button9Rect, "Retriever"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Retriever"]);
                        }
                        if (GUI.Button(guiCoordinates.button10Rect, "Solar Panel"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Solar Panel"]);
                        }
                        if (GUI.Button(guiCoordinates.button11Rect, "Generator"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Generator"]);
                        }
                        if (GUI.Button(guiCoordinates.button12Rect, "Nuclear Reactor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Nuclear Reactor"]);
                        }
                        if (GUI.Button(guiCoordinates.button13Rect, "Reactor Turbine"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Reactor Turbine"]);
                        }
                        if (GUI.Button(guiCoordinates.button14Rect, "Power Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Power Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button15Rect, "Heat Exchanger"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Heat Exchanger"]);
                        }
                        if (GUI.Button(guiCoordinates.button17Rect, "Smelter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Smelter"]);
                        }
                        if (GUI.Button(guiCoordinates.button18Rect, "Alloy Smelter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Alloy Smelter"]);
                        }
                        if (GUI.Button(guiCoordinates.button19Rect, "DM Collector"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Dark Matter Collector"]);
                        }
                        if (GUI.Button(guiCoordinates.button20Rect, "DM Conduit"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Dark Matter Conduit"]);
                        }
                        if (GUI.Button(guiCoordinates.button21Rect, "Auto Crafter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Auto Crafter"]);
                        }
                        if (GUI.Button(guiCoordinates.button22Rect, "Rail Cart Hub"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Rail Cart Hub"]);
                        }
                        if (GUI.Button(guiCoordinates.button23Rect, "Rail Cart"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Rail Cart"]);
                        }
                    }
                    if (craftingPage == 1)
                    {
                        if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Iron Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.ironBlock + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Iron Ramp"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.ironRamp + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Steel Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.steelBlock + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Steel Ramp"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.steelRamp + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Door"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.door + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Quantum Hatchway"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.quantumHatchway + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Electric Light"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.electricLight + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Circuit Board"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.circuitBoard + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Electric Motor"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.electricMotor + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Storage Computer"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.storageComputer + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Turret"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.turret + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Missile Turret"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.missileTurret + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button14Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Missile"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.missile + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button15Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Protection Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.protectionBlock + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        if (GUI.Button(guiCoordinates.button1Rect, "Iron Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Iron Block"]);
                        }
                        if (GUI.Button(guiCoordinates.button2Rect, "Iron Ramp"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Iron Ramp"]);
                        }
                        if (GUI.Button(guiCoordinates.button3Rect, "Steel Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Steel Block"]);
                        }
                        if (GUI.Button(guiCoordinates.button4Rect, "Steel Ramp"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Steel Ramp"]);
                        }
                        if (GUI.Button(guiCoordinates.button5Rect, "Door"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Door"]);
                        }
                        if (GUI.Button(guiCoordinates.button6Rect, "Quantum Hatchway"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Quantum Hatchway"]);
                        }
                        if (GUI.Button(guiCoordinates.button7Rect, "Electric Light"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Electric Light"]);
                        }
                        if (GUI.Button(guiCoordinates.button9Rect, "Circuit Board"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Circuit Board"]);
                        }
                        if (GUI.Button(guiCoordinates.button10Rect, "Electric Motor"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Electric Motor"]);
                        }
                        if (GUI.Button(guiCoordinates.button11Rect, "Storage Computer"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Storage Computer"]);
                        }
                        if (GUI.Button(guiCoordinates.button12Rect, "Turret"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Turret"]);
                        }
                        if (GUI.Button(guiCoordinates.button13Rect, "Missile Turret"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Missile Turret"]);
                        }
                        if (GUI.Button(guiCoordinates.button14Rect, "Missile"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Missile"]);
                        }
                        if (GUI.Button(guiCoordinates.button15Rect, "Protection Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Protection Block"]);
                        }
                    }
                    if (craftingPage == 2)
                    {
                        if (guiCoordinates.button1Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Block"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicBlock + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button2Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Switch"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicSwitch + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button3Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic AND"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicAND + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button4Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic OR"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicOR + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button5Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic XOR"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicXOR + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button6Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Splitter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicSplitter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button7Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Inverter"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicInverter + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button9Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Delayer"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicDelayer + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button10Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Logic Relay"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.logicRelay + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button11Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Player Detector"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.playerDetector + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button12Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Item Detector"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.itemDetector + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }
                        if (guiCoordinates.button13Rect.Contains(Event.current.mousePosition))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                            CraftingRecipe recipe = craftingDictionary.dictionary["Power Detector"];
                            int length = recipe.ingredients.Length;
                            string[] crafting = new string[length];
                            for (int i = 0; i < length; i++)
                            {
                                crafting[i] = recipe.amounts[i] + "x " + recipe.ingredients[i];
                            }
                            GUI.Label(guiCoordinates.craftingInfoRect, descriptions.powerDetector + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));
                        }

                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        if (GUI.Button(guiCoordinates.button1Rect, "Logic Block"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Block"]);
                        }
                        if (GUI.Button(guiCoordinates.button2Rect, "Logic Switch"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Switch"]);
                        }
                        if (GUI.Button(guiCoordinates.button3Rect, "Logic AND Gate"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic AND"]);
                        }
                        if (GUI.Button(guiCoordinates.button4Rect, "Logic OR Gate"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic OR"]);
                        }
                        if (GUI.Button(guiCoordinates.button5Rect, "Logic XOR Gate"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic XOR"]);
                        }
                        if (GUI.Button(guiCoordinates.button6Rect, "Logic Splitter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Splitter"]);
                        }
                        if (GUI.Button(guiCoordinates.button7Rect, "Logic Inverter"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Inverter"]);
                        }
                        if (GUI.Button(guiCoordinates.button9Rect, "Logic Delayer"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Delayer"]);
                        }
                        if (GUI.Button(guiCoordinates.button10Rect, "Logic Relay"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Logic Relay"]);
                        }
                        if (GUI.Button(guiCoordinates.button11Rect, "Player Detector"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Player Detector"]);
                        }
                        if (GUI.Button(guiCoordinates.button12Rect, "Item Detector"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Item Detector"]);
                        }
                        if (GUI.Button(guiCoordinates.button13Rect, "Power Detector"))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.dictionary["Power Detector"]);
                        }
                    }
                    if (craftingPage == 3)
                    {
                        GUI.DrawTexture(guiCoordinates.craftingBackgroundRect, textureDictionary.dictionary["Interface Background"]);
                        GUI.DrawTexture(guiCoordinates.craftingInfoBackgroundRect, textureDictionary.dictionary["Interface Background"]);

                        int f = GUI.skin.label.fontSize;
                        GUI.skin.label.fontSize = 24;
                        GUI.color = new Color(0.44f, 0.72f, 0.82f, 1);
                        GUI.Label(guiCoordinates.craftingTitleRect, "CRAFTING");
                        GUI.skin.label.fontSize = f;
                        GUI.color = Color.white;

                        int index = 0;
                        KeyValuePair<string, CraftingRecipe>[] recipes = new KeyValuePair<string, CraftingRecipe>[craftingDictionary.modDictionary.Count];
                        foreach (KeyValuePair<string,CraftingRecipe> kvp in craftingDictionary.modDictionary)
                        {
                            recipes[index] = kvp;
                            index++;
                        }

                        string desc = recipes[modCraftingIndex].Value.output;
                        string iconKey = desc + "_Icon";

                        if (textureDictionary.dictionary.ContainsKey(iconKey))
                        {
                            GUI.DrawTexture(guiCoordinates.craftingItemRect, textureDictionary.dictionary[iconKey]);
                        }
                        else
                        {
                            GUI.DrawTexture(guiCoordinates.craftingItemRect, textureDictionary.dictionary[desc]);
                        }

                        if (GetComponent<BuildController>().blockDictionary.machineDictionary.ContainsKey(recipes[modCraftingIndex].Value.output))
                        {
                            desc = GetComponent<BuildController>().blockDictionary.GetMachineDescription(recipes[modCraftingIndex].Value.output);
                        }

                        string[] crafting = new string[recipes[modCraftingIndex].Value.ingredients.Length];
                        for (int i = 0; i < recipes[modCraftingIndex].Value.ingredients.Length; i++)
                        {
                            crafting[i] = recipes[modCraftingIndex].Value.amounts[i] + "x " + recipes[modCraftingIndex].Value.ingredients[i];
                        }

                        GUI.Label(guiCoordinates.craftingInfoRect, desc + "\n\n[CRAFTING]\n" + string.Join("\n", crafting));

                        if (GUI.Button(guiCoordinates.button3Rect, "<-"))
                        {
                            if (modCraftingIndex > 0)
                            {
                                modCraftingIndex--;
                            }
                            playerController.PlayButtonSound();
                        }

                        if (GUI.Button(guiCoordinates.button19Rect, "->"))
                        {
                            if (modCraftingIndex < recipes.Length - 1)
                            {
                                modCraftingIndex++;
                            }
                            playerController.PlayButtonSound();
                        }

                        if (GUI.Button(guiCoordinates.button11Rect, recipes[modCraftingIndex].Value.output))
                        {
                            craftingManager.CraftItemAsPlayer(craftingDictionary.modDictionary[recipes[modCraftingIndex].Value.output]);
                        }
                    }
                    if (GUI.Button(guiCoordinates.craftingPreviousRect, "<-"))
                    {
                        if (craftingPage > 0)
                        {
                            craftingPage -= 1;
                        }
                        playerController.PlayButtonSound();
                    }
                    if (GUI.Button(guiCoordinates.craftingNextRect, "->"))
                    {
                        if (craftingPage < 2)
                        {
                            craftingPage += 1;
                        }
                        else if (craftingPage < 3 && craftingDictionary.modDictionary.Count > 0)
                        {
                            craftingPage += 1;
                        }
                        playerController.PlayButtonSound();
                    }
                }
            }
        }
    }
}
