using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HotBar : MonoBehaviour
{
    private Dictionary<int, string> slots;
    private PlayerController playerController;
    private TextureDictionary textureDictionary;
    private GuiCoordinates guiCoordinates;
    private Texture2D boxTexture;
    private string hoveredItem;
    private string draggedItem;
    private int currentSlot;
    private bool draggingItem;
    private bool hotbarLoaded;
    public bool toggled;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        guiCoordinates = new GuiCoordinates();
        slots = new Dictionary<int, string>();
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textureDictionary = gameManager.GetComponent<TextureDictionary>();
        boxTexture = Resources.Load("ShadedSelectionBox") as Texture2D;
        toggled = true;
    }

    //! Load hotbar state from save file.
    private void LoadHotBar()
    {
        string worldName = playerController.stateManager.worldName;
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.HasKey(worldName + "hotbarSlot" + i.ToString()))
            {
                slots[i] = PlayerPrefs.GetString(worldName + "hotbarSlot" + i.ToString());
            }
        }
        hotbarLoaded = true;
    }

    //! Returns true if the item can be placed in the world.
    private bool IsBuildable(string item)
    {
        foreach (string objectName in playerController.blockSelector.objectNames)
        {
            if (item == objectName)
            {
                return true;
            }
        }
        return false;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (playerController.stateManager.worldLoaded == true && hotbarLoaded == false)
        {
            string worldName = playerController.stateManager.worldName;
            List<string> worldList = PlayerPrefsX.GetPersistentStringArray("Worlds").ToList();
            if (worldList.Contains(worldName))
            {
                LoadHotBar();
            }
        }
        else if (toggled == true)
        {
            OverrideKeys();

            draggingItem = playerController.draggingItem;

            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                playerController.displayingBuildItem = false;

                if (currentSlot < 7)
                {
                    currentSlot++;
                }
                else
                {
                    currentSlot = 0;
                }
                if (slots.ContainsKey(currentSlot))
                {
                    if (slots[currentSlot] != "" && slots[currentSlot] != "nothing")
                    {
                        playerController.buildType = slots[currentSlot];
                    }
                }
            }

            if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                playerController.displayingBuildItem = false;

                if (currentSlot > 0)
                {
                    currentSlot--;
                }
                else
                {
                    currentSlot = 7;
                }
                if (slots.ContainsKey(currentSlot))
                {
                    if (slots[currentSlot] != "" && slots[currentSlot] != "nothing")
                    {
                        playerController.buildType = slots[currentSlot];
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (playerController.inventoryOpen == true)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[0] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot0", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(0))
                {
                    playerController.buildType = slots[0];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[1] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot1", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(1))
                {
                    playerController.buildType = slots[1];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[2] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot2", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(2))
                {
                    playerController.buildType = slots[2];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[3] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot3", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(3))
                {
                    playerController.buildType = slots[3];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[4] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot4", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(4))
                {
                    playerController.buildType = slots[4];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[5] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot5", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(5))
                {
                    playerController.buildType = slots[5];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[6] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot6", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(6))
                {
                    playerController.buildType = slots[6];
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (playerController.inventoryOpen)
                {
                    if (IsBuildable(hoveredItem))
                    {
                        slots[7] = hoveredItem;
                        PlayerPrefs.SetString(playerController.stateManager.worldName + "hotbarSlot7", hoveredItem);
                        PlayerPrefs.Save();
                    }
                }
                else if (slots.ContainsKey(7))
                {
                    playerController.buildType = slots[7];
                }
            }
        }
    }

    //! Returns true if the hotbar should be shown.
    private bool ShowHotBar()
    {
        return playerController.stateManager.worldLoaded == true
        && toggled == true
        && playerController.escapeMenuOpen == false
        && playerController.tabletOpen == false
        && playerController.marketGUIopen == false
        && playerController.craftingGUIopen == false
        && hotbarLoaded == true;
    }

    //! Reserve mousewheel for hotbar.
    private void OverrideKeys()
    {
        cInput.ChangeKey("Next Item", Keys.X, Keys.X);
        cInput.ChangeKey("Previous Item", Keys.Z, Keys.Z);
        cInput.ForbidKey("Mouse Wheel Up");
        cInput.ForbidKey("Mouse Wheel Down");
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        //ASPECT RATIO
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float heightOffset = 0;
        float widthOffset = 0;

        if (screenWidth / screenHeight < 1.7f)
        {
            screenHeight = screenHeight * 0.75f;
            heightOffset = 0.35f;
        }

        if (ShowHotBar() == true)
        {
            float hotBarHeight;
            int slot;
            Vector2 mousePos = Event.current.mousePosition;
            List<Rect> slotRects = new List<Rect>();

            bool raisedHotbar = playerController.building == true && !playerController.GuiOpen() && heightOffset < 0.1f;

            if (playerController.objectInSight != null || raisedHotbar)
            {
                hotBarHeight = 0.65f + heightOffset;
            }
            else
            {
                hotBarHeight = 0.85f + heightOffset;
            }

            if (playerController.inventoryOpen == true)
            {
                int index = 0;
                foreach (Rect rect in guiCoordinates.inventorySlotRects)
                {
                    if (rect.Contains(mousePos))
                    {
                        hoveredItem = playerController.playerInventory.inventory[index].typeInSlot;
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            draggedItem = playerController.playerInventory.inventory[index].typeInSlot;
                        }
                    }
                    index++;
                }
            }

            if (playerController.storageGUIopen == true)
            {
                int index = 0;
                foreach (Rect rect in guiCoordinates.storageInventorySlotRects)
                {
                    if (rect.Contains(mousePos))
                    {
                        hoveredItem = playerController.storageInventory.inventory[index].typeInSlot;
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            draggedItem = playerController.storageInventory.inventory[index].typeInSlot;
                        }
                    }
                    index++;
                }
            }

            slot = 0;
            widthOffset = playerController.GuiOpen() ? 0.10f : 0;
            for (float width = 0.305f + widthOffset; width < 0.68f + widthOffset; width = width + 0.05f)
            {
                if (slots.ContainsKey(slot))
                {
                    if (playerController.buildType == slots[slot])
                    {
                        GUI.color = Color.black;
                    }
                }

                Rect slotRect = new Rect(screenWidth * width, (screenHeight * (hotBarHeight - 0.004f)), (screenWidth * 0.046f), (screenHeight * 0.09f));
                slotRects.Add(slotRect);

                GUI.DrawTexture(slotRect, boxTexture);
                GUI.color = Color.white;

                slot++;
            }

            slot = 0;
            foreach (Rect slotPos in slotRects)
            {
                if (draggingItem == true)
                {
                    if (slotPos.Contains(mousePos))
                    {
                        if (playerController.draggingItem == false)
                        {
                            if (IsBuildable(draggedItem))
                            {
                                slots[slot] = draggedItem;
                                string worldName = playerController.stateManager.worldName;
                                string savedSlot =  worldName + "hotbarSlot" + slot.ToString();
                                PlayerPrefs.SetString(savedSlot, draggedItem);
                                PlayerPrefs.Save();
                                draggedItem = "nothing";
                                draggingItem = false;
                            }
                        }
                    }
                }
                slot++;
            }

            slot = 0;
            foreach (Rect slotPos in slotRects)
            {
                Rect slotTextureRect = new Rect(slotPos.x + screenWidth * 0.0025f, slotPos.y + screenHeight * 0.0045f, slotPos.width * 0.9f, slotPos.height * 0.9f);
                if (slots.ContainsKey(slot))
                {
                    string itemInSlot = slots[slot];
                    if (textureDictionary.dictionary.ContainsKey(itemInSlot + "_Icon"))
                    {
                        GUI.DrawTexture(slotTextureRect, textureDictionary.dictionary[itemInSlot + "_Icon"]);
                    }
                    else if (textureDictionary.dictionary.ContainsKey(itemInSlot))
                    {
                        GUI.DrawTexture(slotTextureRect, textureDictionary.dictionary[itemInSlot]);
                    }
                }

                Rect slotLabelRect = new Rect(slotPos.x + screenWidth * 0.005f, slotPos.y + screenHeight * 0.004f, slotPos.width * 0.9f, slotPos.height * 0.9f);
                GUI.Label(slotLabelRect, (slot + 1).ToString());

                slot++;
            }
        }
    }
}
