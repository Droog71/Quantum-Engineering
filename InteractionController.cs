using System;
using UnityEngine;
using System.Net;

public class InteractionController : MonoBehaviour
{
    private PlayerController playerController;
    private MachineInteraction machineInteraction;
    private StorageInteraction storageInteraction;
    public BlockInteraction blockInteraction;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        machineInteraction = new MachineInteraction(playerController, this);
        storageInteraction = new StorageInteraction(playerController, this);
        blockInteraction = new BlockInteraction(playerController, this);
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!playerController.stateManager.Busy())
        {
            // Raycast and associated data for interacting with machines and other objects.
            float range = playerController.gameManager.chunkSize * 0.75f;
            Transform camPos = Camera.main.gameObject.transform;
            if (Physics.Raycast(camPos.position, camPos.forward, out RaycastHit hit, range))
            {
                float distance = Vector3.Distance(camPos.position, hit.point);
                GameObject obj = hit.collider.gameObject;
                if (obj != playerController.gameObject && GuiFree() && !IsNonInteractive(obj))
                {
                    playerController.objectInSight = obj;
                    if (IsStorageContainer(obj) && distance <= 40)
                    {
                        storageInteraction.InteractWithStorageContainer();
                    }
                    else if (obj.GetComponent<StorageComputer>() != null && distance <= 40)
                    {
                        storageInteraction.InteractWithStorageComputer();
                    }
                    else if (obj.GetComponent<LogicBlock>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithLogicBlock();
                    }
                    else if (obj.GetComponent<PowerSource>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithPowerSource();
                    }
                    else if (obj.GetComponent<NuclearReactor>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithNuclearReactor();
                    }
                    else if (obj.GetComponent<PowerConduit>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithPowerConduit();
                    }
                    else if (obj.GetComponent<Turret>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithTurret();
                    }
                    else if (obj.GetComponent<MissileTurret>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithMissileTurret();
                    }
                    else if (obj.GetComponent<UniversalExtractor>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithUniversalExtractor();
                    }
                    else if (obj.GetComponent<Auger>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithAuger();
                    }
                    else if (obj.GetComponent<DarkMatterCollector>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithDarkMatterCollector();
                    }
                    else if (obj.GetComponent<UniversalConduit>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithUniversalConduit();
                    }
                    else if (obj.GetComponent<DarkMatterConduit>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithDarkMatterConduit();
                    }
                    else if (obj.GetComponent<Smelter>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithSmelter();
                    }
                    else if (obj.GetComponent<AlloySmelter>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithAlloySmelter();
                    }
                    else if (obj.GetComponent<Extruder>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithExtruder();
                    }
                    else if (obj.GetComponent<RailCartHub>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithRailCartHub();
                    }
                    else if (obj.GetComponent<Retriever>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithRetriever();
                    }
                    else if (obj.GetComponent<AutoCrafter>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithAutoCrafter();
                    }
                    else if (obj.GetComponent<HeatExchanger>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithHeatExchanger();
                    }
                    else if (obj.GetComponent<GearCutter>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithGearCutter();
                    }
                    else if (obj.GetComponent<Press>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithPress();
                    }
                    else if (obj.GetComponent<ElectricLight>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithElectricLight();
                    }
                    else if (obj.GetComponent<Door>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithDoor();
                    }
                    else if (obj.GetComponent<ModMachine>() != null && distance <= 40)
                    {
                        machineInteraction.InteractWithModMachine();
                    }
                    else if (obj.GetComponent<ProtectionBlock>() != null)
                    {
                        machineInteraction.InteractWithProtectionBlock();
                    }
                    else if (obj.GetComponent<Block>() != null)
                    {
                        blockInteraction.InteractWithBlock(obj.GetComponent<Block>().blockName);
                    }
                    else if (obj.tag.Equals("CombinedMesh"))
                    {
                        blockInteraction.InteractWithCombinedMesh(hit.point);
                    }
                    else if (obj.transform.parent != null)
                    {
                        if (obj.transform.parent.tag.Equals("CombinedMesh"))
                        {
                            blockInteraction.InteractWithCombinedMesh(hit.point);
                        }
                        else
                        {
                            EndInteraction();
                        }
                    }
                    else
                    {
                        EndInteraction();
                    }
                }
                else if (obj != playerController.gameObject && GuiFree() && distance <= 40 && IsNonInteractive(obj))
                {
                    playerController.objectInSight = obj;
                }
                else
                {
                    EndInteraction();
                }
            }
            else
            {
                EndInteraction();
            }
        }
    }

    //! Returns true if the GUI is available.
    private bool GuiFree()
    {
        return playerController.inventoryOpen == false
        && playerController.escapeMenuOpen == false
        && playerController.tabletOpen == false
        && playerController.marketGUIopen == false;
    }

    //! Returns true if the object in question cannot be interacted with.
    private bool IsNonInteractive(GameObject obj)
    {
        return obj.GetComponent<DarkMatter>() != null
        || obj.GetComponent<UniversalResource>() != null
        || obj.GetComponent<NetworkPlayer>() != null;
    }

    //! Returns true if the object in question is a storage container.
    private Boolean IsStorageContainer(GameObject obj)
    {
        if (obj.GetComponent<InventoryManager>() != null && obj.GetComponent<Retriever>() == null && obj.GetComponent<AutoCrafter>() == null)
        {
            return true;
        }
        return false;
    }

    //! Destroys an object in the world and adds it's associated inventory item to the player's inventory.
    public void CollectObject(GameObject obj, string type)
    {
        bool canRemove = true;

        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            canRemove = CanInteract();
        }

        if (canRemove == true)
        {
            bool spaceAvailable = false;

            foreach (InventorySlot slot in playerController.playerInventory.inventory)
            {
                if (slot.typeInSlot.Equals("nothing") || slot.typeInSlot.Equals(type) && slot.amountInSlot < 1000)
                {
                    spaceAvailable = true;
                }
            }

            if (spaceAvailable == true)
            {
                playerController.playerInventory.AddItem(type, 1);
                Destroy(obj);
                playerController.PlayCraftingSound();
                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                {
                    Vector3 pos = obj.transform.position;
                    Quaternion rot = obj.transform.rotation;
                    UpdateNetwork(1, type, pos, rot);
                }
            }
            else
            {
                playerController.cannotCollect = true;
                playerController.PlayMissingItemsSound();
            }
        }
        else
        {
            playerController.PlayMissingItemsSound();
        }
    }

    //! Returns true if the player is allowed to remove the object.
    public bool CanInteract()
    {
        bool closeToProtectionBlock = false;

        ProtectionBlock[] protectionBlocks = FindObjectsOfType<ProtectionBlock>();
        foreach (ProtectionBlock protectionBlock in protectionBlocks)
        {
            Vector3 playerPos = playerController.gameObject.transform.position;
            Vector3 blockPos = protectionBlock.transform.position;
            Vector3 playerPosNoY = new Vector3(playerPos.x, 0, playerPos.z);
            Vector3 blockPosNoY = new Vector3(blockPos.x, 0, blockPos.z);
            float distance = Vector3.Distance(playerPosNoY, blockPosNoY);
            if (distance <= 160)
            {
                closeToProtectionBlock = true;
                if (protectionBlock.IsAuthorizedUser(PlayerPrefs.GetString("UserName")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        if (closeToProtectionBlock == false)
        {
            return true;
        }

        return false;
    }

    //! Opens the machine GUI.
    public void ToggleMachineGUI()
    {
        if (playerController.machineGUIopen == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.storageGUIopen = false;
            playerController.craftingGUIopen = false;
            playerController.inventoryOpen = false;
            playerController.machineGUIopen = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.inventoryOpen = false;
            playerController.craftingGUIopen = false;
            playerController.storageGUIopen = false;
            playerController.machineGUIopen = false;
        }
    }

    //! Sends instantiated block info to the server in multiplayer games.
    private void UpdateNetwork(int destroy, string type, Vector3 pos, Quaternion rot)
    {
        using(WebClient client = new WebClient())
        {
            Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/blocks");
            string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
            string rotation = Mathf.Round(rot.x) + "," + Mathf.Round(rot.y) + "," + Mathf.Round(rot.z) + "," + Mathf.Round(rot.w);
            client.UploadStringAsync(uri, "POST", "@" + destroy + ":" + type + ":" + position + ":" + rotation);
        }
    }

    //! Called when the player is no longer looking at any interactive objects.
    private void EndInteraction()
    {
        playerController.machineGUIopen = false;
        playerController.lookingAtCombinedMesh = false;
        playerController.objectInSight = null;
        playerController.machineInSight = null;
        playerController.machineInputID = "none";
        playerController.machineOutputID = "none";
        playerController.machineType = "none";
        playerController.machineAmount = 0;
        playerController.machineInputAmount = 0;
        playerController.machineOutputAmount = 0;
    }
}