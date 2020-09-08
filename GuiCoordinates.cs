using UnityEngine;

public class GuiCoordinates : MonoBehaviour
{
    //MESSAGES
    public Rect messageRect;
    public Rect lowMessageRect;
    public Rect midMessageRect;
    public Rect highMessageRect;
    public Rect secondLineHighMessageRect;
    public Rect longHighMessageRect;
    public Rect lowMessageBackgroundRect;
    public Rect messageBackgroundRect;
    public Rect midMessageBackgroundRect;
    public Rect highMessageBackgroundRect;
    public Rect longHighMessageBackgroundRect;
    public Rect secondLineHighMessageBackgroundRect;
    public Rect twoLineHighMessageBackgroundRect;

    //CROSSHAIR
    public Rect crosshairRect;

    //INVENTORY
    public Rect inventoryMesageRect;
    public Rect storageInventoryMessageRect;

    public Rect craftingButtonRect;
    public Rect closeButtonRect;

    public Rect inventoryBackgroundRect;

    public Rect storageComputerPreviousRect;
    public Rect storageComputerNextRect;
    public Rect storageComputerRebootRect;
    public Rect storageComputerSearchRect;
    public Rect storageSearchLabelRect;
    public Rect storageComputerMessageRect;

    public Rect craftingTitleRect;
    public Rect marketTitleRect;

    public Rect craftingPreviousRect;
    public Rect craftingNextRect;

    public Rect inventorySlot1Rect;
    public Rect inventorySlot2Rect;
    public Rect inventorySlot3Rect;
    public Rect inventorySlot4Rect;
    public Rect inventorySlot5Rect;
    public Rect inventorySlot6Rect;
    public Rect inventorySlot7Rect;
    public Rect inventorySlot8Rect;
    public Rect inventorySlot9Rect;
    public Rect inventorySlot10Rect;
    public Rect inventorySlot11Rect;
    public Rect inventorySlot12Rect;
    public Rect inventorySlot13Rect;
    public Rect inventorySlot14Rect;
    public Rect inventorySlot15Rect;
    public Rect inventorySlot16Rect;
    public Rect[] inventorySlotLabelRects;

    public Rect inventorySlot1TexRect;
    public Rect inventorySlot2TexRect;
    public Rect inventorySlot3TexRect;
    public Rect inventorySlot4TexRect;
    public Rect inventorySlot5TexRect;
    public Rect inventorySlot6TexRect;
    public Rect inventorySlot7TexRect;
    public Rect inventorySlot8TexRect;
    public Rect inventorySlot9TexRect;
    public Rect inventorySlot10TexRect;
    public Rect inventorySlot11TexRect;
    public Rect inventorySlot12TexRect;
    public Rect inventorySlot13TexRect;
    public Rect inventorySlot14TexRect;
    public Rect inventorySlot15TexRect;
    public Rect inventorySlot16TexRect;
    public Rect[] inventorySlotRects;

    //STORAGE CONTAINERS
    public Rect storageInventoryBackgroundRect;
    public Rect storageInventorySlot1Rect;
    public Rect storageInventorySlot2Rect;
    public Rect storageInventorySlot3Rect;
    public Rect storageInventorySlot4Rect;
    public Rect storageInventorySlot5Rect;
    public Rect storageInventorySlot6Rect;
    public Rect storageInventorySlot7Rect;
    public Rect storageInventorySlot8Rect;
    public Rect storageInventorySlot9Rect;
    public Rect storageInventorySlot10Rect;
    public Rect storageInventorySlot11Rect;
    public Rect storageInventorySlot12Rect;
    public Rect storageInventorySlot13Rect;
    public Rect storageInventorySlot14Rect;
    public Rect storageInventorySlot15Rect ;
    public Rect storageInventorySlot16Rect;
    public Rect[] storageInventorySlotLabelRects;

    public Rect storageInventorySlot1TexRect;
    public Rect storageInventorySlot2TexRect;
    public Rect storageInventorySlot3TexRect;
    public Rect storageInventorySlot4TexRect;
    public Rect storageInventorySlot5TexRect;
    public Rect storageInventorySlot6TexRect;
    public Rect storageInventorySlot7TexRect;
    public Rect storageInventorySlot8TexRect;
    public Rect storageInventorySlot9TexRect;
    public Rect storageInventorySlot10TexRect;
    public Rect storageInventorySlot11TexRect;
    public Rect storageInventorySlot12TexRect;
    public Rect storageInventorySlot13TexRect;
    public Rect storageInventorySlot14TexRect;
    public Rect storageInventorySlot15TexRect;
    public Rect storageInventorySlot16TexRect;
    public Rect[] storageInventorySlotRects;

    //CRAFTING
    public Rect craftingBackgroundRect;
    public Rect craftingInfoBackgroundRect;
    public Rect craftingInfoRect;

    //CRAFTING GUI BUTTONS
    public Rect button1Rect;
    public Rect button2Rect;
    public Rect button3Rect;
    public Rect button4Rect;
    public Rect button5Rect;
    public Rect button6Rect;
    public Rect button7Rect;
    public Rect button8Rect;
    public Rect button9Rect;
    public Rect button10Rect;
    public Rect button11Rect;
    public Rect button12Rect;
    public Rect button13Rect;
    public Rect button14Rect;
    public Rect button15Rect;
    public Rect button16Rect;
    public Rect button17Rect;
    public Rect button18Rect;
    public Rect button19Rect;
    public Rect button20Rect;
    public Rect button21Rect;
    public Rect button22Rect;
    public Rect button23Rect;
    public Rect button24Rect;

    //INVENTORY INSTRUCTIONS
    public Rect inventoryInfoRectBG;
    public Rect inventoryInfoRect;

    //MACHINE INFO HUD
    public Rect infoRectBG;
    public Rect infoRect;

    //BUILDING INSTRUCTIONS
    public Rect buildInfoRectBG;
    public Rect buildInfoRect;

    //MACHINE INTERACTION WINDOW
    public Rect speedControlBGRect;
    public Rect FourButtonSpeedControlBGRect;
    public Rect FiveButtonSpeedControlBGRect;
    public Rect outputLabelRect;
    public Rect longOutputLabelRect;
    public Rect railCartHubCircuitRect;
    public Rect railCartHubCircuitLabelRect;
    public Rect outputControlButton0Rect;
    public Rect outputControlButton1Rect;
    public Rect outputControlButton2Rect;
    public Rect outputControlButton3Rect;
    public Rect outputControlButton4Rect;

    //OPTIONS/EXIT MENU
    public Rect escapeMenuRect;
    public Rect escapeButton1Rect;
    public Rect escapeButton2Rect;
    public Rect escapeButton3Rect;
    public Rect escapeButton4Rect;
    public Rect escapeButton5Rect;

    //TABLET MESSAGES
    public Rect topLeftInfoRect;

    //TABLET
    public Rect tabletBackgroundRect;
    public Rect tabletMessageRect;
    public Rect tabletButtonRect;
    public Rect tabletTimeRect;

    //BUILD ITEM SELECTION HUD
    public Rect topRightInfoRect;
    public Rect previousBuildItemTextureRect;
    public Rect buildItemTextureRect;
    public Rect nextBuildItemTextureRect;
    public Rect currentBuildItemTextureRect;
    public Rect buildItemCountRect;

    //OPTIONS MENU
    public Rect optionsMenuBackgroundRect;
    public Rect videoMenuBackgroundRect;
    public Rect schematicsMenuBackgroundRect;
    public Rect optionsButton1Rect;
    public Rect optionsButton2Rect;
    public Rect optionsButton3Rect;
    public Rect optionsButton4Rect;
    public Rect optionsButton5Rect;
    public Rect optionsButton6Rect;
    public Rect optionsButton7Rect;
    public Rect optionsButton8Rect;
    public Rect optionsButton9Rect;
    public Rect optionsButton10Rect;
    public Rect optionsButton11Rect;
    public Rect sliderLabel1Rect;
    public Rect sliderLabel2Rect;
    public Rect sliderLabel3Rect;
    public Rect sliderLabel4Rect;
    public Rect sliderLabel5Rect;
    public Rect schematicCloseRect;

    //MARKET
    public Rect marketMessageRect;
    public Rect marketMessageLabelRect;
    public Rect marketMessageButtonRect;

    void Start()
    {
        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;

        //MESSAGES
        lowMessageBackgroundRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.63f), (ScreenWidth * 0.18f), (ScreenHeight * 0.05f));
        messageBackgroundRect = new Rect((ScreenWidth / 2) - 100, ScreenHeight - 120, 200, 100);
        midMessageBackgroundRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.55f), (ScreenWidth * 0.18f), (ScreenHeight * 0.05f));
        highMessageBackgroundRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.28f), (ScreenWidth * 0.18f), (ScreenHeight * 0.05f));
        longHighMessageBackgroundRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.28f), (ScreenWidth * 0.23f), (ScreenHeight * 0.05f));

        messageRect = new Rect((ScreenWidth / 2) - 60, ScreenHeight - 90, 200, 100);
        lowMessageRect = new Rect((ScreenWidth * 0.47f), (ScreenHeight * 0.644f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));
        midMessageRect = new Rect((ScreenWidth * 0.455f), (ScreenHeight * 0.562f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));
        highMessageRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.30f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));
        longHighMessageRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.292f), (ScreenWidth * 0.55f), (ScreenHeight * 0.5f));

        secondLineHighMessageBackgroundRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.36f), (ScreenWidth * 0.18f), (ScreenHeight * 0.05f));
        secondLineHighMessageRect = new Rect((ScreenWidth * 0.452f), (ScreenHeight * 0.374f), (ScreenWidth * 0.18f), (ScreenHeight * 0.05f));
        twoLineHighMessageBackgroundRect = new Rect((ScreenWidth * 0.44f), (ScreenHeight * 0.27f), (ScreenWidth * 0.18f), (ScreenHeight * 0.10f));

        //CROSSHAIR
        crosshairRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.47f), (ScreenWidth * 0.04f), (ScreenHeight * 0.06f));

        //INVENTORY
        inventoryMesageRect = new Rect((ScreenWidth * 0.76f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));
        storageInventoryMessageRect = new Rect((ScreenWidth * 0.36f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));

        craftingButtonRect = new Rect((ScreenWidth * 0.675f), (ScreenHeight * 0.77f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        closeButtonRect = new Rect((ScreenWidth * 0.825f), (ScreenHeight * 0.77f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        inventoryBackgroundRect = new Rect((ScreenWidth * 0.40f), (ScreenHeight * 0.20f), (ScreenWidth * 0.60f), (ScreenHeight * 0.62f));

        storageComputerPreviousRect = new Rect((ScreenWidth * 0.295f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        storageComputerNextRect = new Rect((ScreenWidth * 0.465f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        storageComputerRebootRect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.72f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        storageComputerSearchRect = new Rect((ScreenWidth * 0.28f), (ScreenHeight * 0.28f), (ScreenWidth * 0.12f), (ScreenHeight * 0.025f));
        storageSearchLabelRect = new Rect((ScreenWidth * 0.32f), (ScreenHeight * 0.26f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));
        storageComputerMessageRect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.28f), (ScreenWidth * 0.2f), (ScreenHeight * 0.5f));

        craftingTitleRect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.60f), (ScreenWidth * 0.20f), (ScreenHeight * 0.10f));
        marketTitleRect = new Rect((ScreenWidth * 0.265f), (ScreenHeight * 0.60f), (ScreenWidth * 0.20f), (ScreenHeight * 0.10f));

        craftingPreviousRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.63f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));
        craftingNextRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.63f), (ScreenWidth * 0.07f), (ScreenHeight * 0.025f));

        inventorySlot1Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot2Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot3Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot4Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot5Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot6Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot7Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot8Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot9Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot10Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot11Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot12Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot13Rect = new Rect((ScreenWidth * 0.714f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot14Rect = new Rect((ScreenWidth * 0.768f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot15Rect = new Rect((ScreenWidth * 0.82f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        inventorySlot16Rect = new Rect((ScreenWidth * 0.874f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));

        inventorySlotLabelRects = new Rect[16];
        inventorySlotLabelRects[0] = inventorySlot1Rect;
        inventorySlotLabelRects[1] = inventorySlot2Rect;
        inventorySlotLabelRects[2] = inventorySlot3Rect;
        inventorySlotLabelRects[3] = inventorySlot4Rect;
        inventorySlotLabelRects[4] = inventorySlot5Rect;
        inventorySlotLabelRects[5] = inventorySlot6Rect;
        inventorySlotLabelRects[6] = inventorySlot7Rect;
        inventorySlotLabelRects[7] = inventorySlot8Rect;
        inventorySlotLabelRects[8] = inventorySlot9Rect;
        inventorySlotLabelRects[9] = inventorySlot10Rect;
        inventorySlotLabelRects[10] = inventorySlot11Rect;
        inventorySlotLabelRects[11] = inventorySlot12Rect;
        inventorySlotLabelRects[12] = inventorySlot13Rect;
        inventorySlotLabelRects[13] = inventorySlot14Rect;
        inventorySlotLabelRects[14] = inventorySlot15Rect;
        inventorySlotLabelRects[15] = inventorySlot16Rect;

        inventorySlot1TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot2TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot3TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot4TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot5TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot6TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot7TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot8TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot9TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot10TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot11TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot12TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot13TexRect = new Rect((ScreenWidth * 0.722f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot14TexRect = new Rect((ScreenWidth * 0.774f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot15TexRect = new Rect((ScreenWidth * 0.828f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        inventorySlot16TexRect = new Rect((ScreenWidth * 0.882f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));

        inventorySlotRects = new Rect[16];
        inventorySlotRects[0] = inventorySlot1TexRect;
        inventorySlotRects[1] = inventorySlot2TexRect;
        inventorySlotRects[2] = inventorySlot3TexRect;
        inventorySlotRects[3] = inventorySlot4TexRect;
        inventorySlotRects[4] = inventorySlot5TexRect;
        inventorySlotRects[5] = inventorySlot6TexRect;
        inventorySlotRects[6] = inventorySlot7TexRect;
        inventorySlotRects[7] = inventorySlot8TexRect;
        inventorySlotRects[8] = inventorySlot9TexRect;
        inventorySlotRects[9] = inventorySlot10TexRect;
        inventorySlotRects[10] = inventorySlot11TexRect;
        inventorySlotRects[11] = inventorySlot12TexRect;
        inventorySlotRects[12] = inventorySlot13TexRect;
        inventorySlotRects[13] = inventorySlot14TexRect;
        inventorySlotRects[14] = inventorySlot15TexRect;
        inventorySlotRects[15] = inventorySlot16TexRect;

        //STORAGE CONTAINERS
        storageInventoryBackgroundRect = new Rect(0, (ScreenHeight * 0.20f), (ScreenWidth * 0.60f), (ScreenHeight * 0.62f));
        storageInventorySlot1Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot2Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot3Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot4Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.325f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot5Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot6Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot7Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot8Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot9Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot10Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot11Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot12Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.513f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot13Rect = new Rect((ScreenWidth * 0.314f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot14Rect = new Rect((ScreenWidth * 0.368f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot15Rect = new Rect((ScreenWidth * 0.42f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        storageInventorySlot16Rect = new Rect((ScreenWidth * 0.474f), (ScreenHeight * 0.60f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));

        storageInventorySlotLabelRects = new Rect[16];
        storageInventorySlotLabelRects[0] = storageInventorySlot1Rect;
        storageInventorySlotLabelRects[1] = storageInventorySlot2Rect;
        storageInventorySlotLabelRects[2] = storageInventorySlot3Rect;
        storageInventorySlotLabelRects[3] = storageInventorySlot4Rect;
        storageInventorySlotLabelRects[4] = storageInventorySlot5Rect;
        storageInventorySlotLabelRects[5] = storageInventorySlot6Rect;
        storageInventorySlotLabelRects[6] = storageInventorySlot7Rect;
        storageInventorySlotLabelRects[7] = storageInventorySlot8Rect;
        storageInventorySlotLabelRects[8] = storageInventorySlot9Rect;
        storageInventorySlotLabelRects[9] = storageInventorySlot10Rect;
        storageInventorySlotLabelRects[10] = storageInventorySlot11Rect;
        storageInventorySlotLabelRects[11] = storageInventorySlot12Rect;
        storageInventorySlotLabelRects[12] = storageInventorySlot13Rect;
        storageInventorySlotLabelRects[13] = storageInventorySlot14Rect;
        storageInventorySlotLabelRects[14] = storageInventorySlot15Rect;
        storageInventorySlotLabelRects[15] = storageInventorySlot16Rect;

        storageInventorySlot1TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot2TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot3TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot4TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.35f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot5TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot6TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot7TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot8TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.445f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot9TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot10TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot11TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot12TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.535f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot13TexRect = new Rect((ScreenWidth * 0.322f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot14TexRect = new Rect((ScreenWidth * 0.374f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot15TexRect = new Rect((ScreenWidth * 0.428f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));
        storageInventorySlot16TexRect = new Rect((ScreenWidth * 0.482f), (ScreenHeight * 0.625f), (ScreenWidth * 0.029f), (ScreenHeight * 0.055f));

        storageInventorySlotRects = new Rect[16];
        storageInventorySlotRects[0] = storageInventorySlot1TexRect;
        storageInventorySlotRects[1] = storageInventorySlot2TexRect;
        storageInventorySlotRects[2] = storageInventorySlot3TexRect;
        storageInventorySlotRects[3] = storageInventorySlot4TexRect;
        storageInventorySlotRects[4] = storageInventorySlot5TexRect;
        storageInventorySlotRects[5] = storageInventorySlot6TexRect;
        storageInventorySlotRects[6] = storageInventorySlot7TexRect;
        storageInventorySlotRects[7] = storageInventorySlot8TexRect;
        storageInventorySlotRects[8] = storageInventorySlot9TexRect;
        storageInventorySlotRects[9] = storageInventorySlot10TexRect;
        storageInventorySlotRects[10] = storageInventorySlot11TexRect;
        storageInventorySlotRects[11] = storageInventorySlot12TexRect;
        storageInventorySlotRects[12] = storageInventorySlot13TexRect;
        storageInventorySlotRects[13] = storageInventorySlot14TexRect;
        storageInventorySlotRects[14] = storageInventorySlot15TexRect;
        storageInventorySlotRects[15] = storageInventorySlot16TexRect;

        //CRAFTING
        craftingBackgroundRect = new Rect(0, (ScreenHeight * 0.05f), (ScreenWidth * 0.60f), (ScreenHeight * 0.68f));
        craftingInfoBackgroundRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.65f), (ScreenWidth * 0.50f), (ScreenHeight * 0.35f));
        craftingInfoRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.69f), (ScreenWidth * 0.42f), (ScreenHeight * 0.31f));

        //CRAFTING GUI BUTTONS
        button1Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button2Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button3Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button4Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button5Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button6Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button7Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button8Rect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button9Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button10Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button11Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button12Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button13Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button14Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button15Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button16Rect = new Rect((ScreenWidth * 0.23f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button17Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.15f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button18Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.21f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button19Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.27f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button20Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.33f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button21Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.39f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button22Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button23Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        button24Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //INVENTORY INSTRUCTIONS
        inventoryInfoRectBG = new Rect(0, (ScreenHeight * 0.78f), (ScreenWidth * 0.40f), (ScreenHeight * 0.22f));
        inventoryInfoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.83f), (ScreenWidth * 0.35f), (ScreenHeight * 0.20f));

        //MACHINE INTERACTION WINDOW
        speedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.28f), (ScreenWidth * 0.24f), (ScreenHeight * 0.30f));
        FourButtonSpeedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.28f), (ScreenWidth * 0.24f), (ScreenHeight * 0.35f));
        FiveButtonSpeedControlBGRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.22f), (ScreenWidth * 0.24f), (ScreenHeight * 0.41f));
        outputLabelRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        longOutputLabelRect = new Rect((ScreenWidth * 0.29f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        railCartHubCircuitLabelRect = new Rect((ScreenWidth * 0.27f), (ScreenHeight * 0.29f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        railCartHubCircuitRect = new Rect((ScreenWidth * 0.335f), (ScreenHeight * 0.29f), (ScreenWidth * 0.04f), (ScreenHeight * 0.03f));
        outputControlButton0Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.28f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        outputControlButton1Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        outputControlButton2Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.40f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        outputControlButton3Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.46f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        outputControlButton4Rect = new Rect((ScreenWidth * 0.25f), (ScreenHeight * 0.52f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //OPTIONS/EXIT MENU
        escapeMenuRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.30f), (ScreenWidth * 0.2f), (ScreenHeight * 0.452f));
        escapeButton1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.34f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        escapeButton2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.42f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        escapeButton3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.50f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        escapeButton4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.58f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        escapeButton5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.66f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //TABLET MESSAGES
        topLeftInfoRect = new Rect(0, 0, (ScreenWidth * 0.5f), (ScreenHeight * 0.2f));

        //TABLET
        tabletBackgroundRect = new Rect(0, 0, (ScreenWidth * 0.5f), ScreenHeight);
        tabletMessageRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.15f), (ScreenWidth * 0.4f), (ScreenHeight * 0.70f));
        tabletButtonRect = new Rect((ScreenWidth * 0.175f), (ScreenHeight * 0.85f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        tabletTimeRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.8f), (ScreenWidth * 0.4f), (ScreenHeight * 0.05f));

        //BUILD ITEM SELECTION HUD
        topRightInfoRect = new Rect((ScreenWidth * 0.70f), 0, (ScreenWidth * 0.3f), (ScreenHeight * 0.2f));
        previousBuildItemTextureRect = new Rect((ScreenWidth * 0.70f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        buildItemTextureRect = new Rect((ScreenWidth * 0.78f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        nextBuildItemTextureRect = new Rect((ScreenWidth * 0.86f), (ScreenHeight * 0.08f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        currentBuildItemTextureRect = new Rect((ScreenWidth * 0.95f), (ScreenHeight * 0.21f), (ScreenWidth * 0.05f), (ScreenHeight * 0.1f));
        buildItemCountRect = new Rect((ScreenWidth * 0.92f), (ScreenHeight * 0.241f), (ScreenWidth * 0.05f), (ScreenHeight * 0.2f));

        //OPTIONS MENU
        optionsMenuBackgroundRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.22f), (ScreenWidth * 0.2f), (ScreenHeight * 0.73f));
        videoMenuBackgroundRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.22f), (ScreenWidth * 0.2f), (ScreenHeight * 0.67f));
        schematicsMenuBackgroundRect = new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.22f), (ScreenWidth * 0.2f), (ScreenHeight * 0.55f));
        optionsButton1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.26f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.32f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.38f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.44f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.50f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton6Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.56f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton7Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.62f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton8Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.68f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton9Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.74f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton10Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.80f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        optionsButton11Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.86f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        sliderLabel1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.41f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        sliderLabel2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.47f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        sliderLabel3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.53f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        sliderLabel4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.59f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
        sliderLabel5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.65f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));

        //MACHINE INFO HUD
        infoRectBG = new Rect(0, (ScreenHeight * 0.60f), (ScreenWidth * 0.30f), (ScreenHeight * 0.40f));
        infoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.65f), (ScreenWidth * 0.24f), (ScreenHeight * 0.30f));

        //BUILDING INSTRUCTIONS
        buildInfoRectBG = new Rect(0, (ScreenHeight * 0.75f), (ScreenWidth * 0.40f), (ScreenHeight * 0.25f));
        buildInfoRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.80f), (ScreenWidth * 0.35f), (ScreenHeight * 0.20f));

        //MARKET MESSAGE
        marketMessageRect = new Rect(((ScreenWidth / 2) - 300), ((ScreenHeight / 2) - 200), 600, 400);
        marketMessageLabelRect = new Rect(((ScreenWidth / 2) - 175), ((ScreenHeight / 2) - 100), 400, 100);
        marketMessageButtonRect = new Rect(((ScreenWidth / 2) - (ScreenWidth * 0.07f)), ((ScreenHeight / 2) + 30), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f));
    }
}
