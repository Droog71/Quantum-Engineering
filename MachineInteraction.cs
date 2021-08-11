using UnityEngine;
using System.Collections.Generic;

public class MachineInteraction
{
    private PlayerController playerController;
    private InteractionController interactionController;

    //! This class handles the player's interactions with machines.
    public MachineInteraction(PlayerController playerController, InteractionController interactionController)
    {
        this.playerController = playerController;
        this.interactionController = interactionController;
    }

    //! Called when the player is looking at an electric light.
    public void InteractWithElectricLight()
    {
        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Electric Light");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }
    }

    //! Called when the player is looking at a door.
    public void InteractWithDoor()
    {
        Door door = playerController.objectInSight.GetComponent<Door>();
        string doorType = playerController.objectInSight.GetComponent<Door>().type;
        playerController.doorToEdit = door;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, doorType);
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && door.type != "Quantum Hatchway")
            {
                if (!playerController.GuiOpen())
                {
                    playerController.doorGUIopen = !playerController.doorGUIopen;
                }
                else
                {
                    playerController.doorGUIopen = false;
                }
            }
            else if (door.coroutineBusy == false)
            {
                door.GetComponent<AudioSource>().Play();
                door.ToggleOpen();
            }
        }
    }

    //! Called when the player is looking at a power source.
    public void InteractWithPowerSource()
    {
        PowerSource powerSource = playerController.objectInSight.GetComponent<PowerSource>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = powerSource.ID;
        playerController.machineOutputID = powerSource.outputID;

        if (powerSource.type.Equals("Solar Panel"))
        {
            if (powerSource.blocked == false)
            {
                playerController.machinePower = 2;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }
        else if (powerSource.type.Equals("Generator"))
        {
            playerController.machineAmount = powerSource.fuelAmount;
            playerController.machineType = powerSource.fuelType;
            if (powerSource.outOfFuel == false)
            {
                playerController.machinePower = 20;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }
        else if (powerSource.type.Equals("Reactor Turbine"))
        {
            if (powerSource.noReactor == false)
            {
                playerController.machinePower = 200;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, powerSource.type);
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a nuclear reactor.
    public void InteractWithNuclearReactor()
    {
        NuclearReactor reactor = playerController.objectInSight.GetComponent<NuclearReactor>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = reactor.ID;
        playerController.machineCooling = reactor.cooling;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Nuclear Reactor");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }
    }

    //! Called when the player is looking at a nuclear reactor.
    public void InteractWithLogicBlock()
    {
        LogicBlock logicBlock = playerController.objectInSight.GetComponent<LogicBlock>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = logicBlock.ID;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, logicBlock.blockType);
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }
    }

    //! Called when the player is looking at a power conduit.
    public void InteractWithPowerConduit()
    {
        PowerConduit powerConduit = playerController.objectInSight.GetComponent<PowerConduit>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = powerConduit.ID;
        playerController.machineOutputID = powerConduit.outputID1;
        playerController.machineOutputID2 = powerConduit.outputID2;
        playerController.machinePower = powerConduit.powerAmount;
        playerController.machineRange = powerConduit.range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Power Conduit");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedConduitRange = powerConduit.range;
            playerController.networkedDualPower = powerConduit.dualOutput;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a turret.
    public void InteractWithTurret()
    {
        Turret turret = playerController.objectInSight.GetComponent<Turret>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = turret.ID;
        playerController.machineHasPower = turret.powerON;
        playerController.machineSpeed = turret.speed;
        playerController.machinePower = turret.power;
        playerController.machineHeat = turret.heat;
        playerController.machineCooling = turret.cooling;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Turret");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedMachineSpeed = turret.speed;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a turret.
    public void InteractWithMissileTurret()
    {
        MissileTurret turret = playerController.objectInSight.GetComponent<MissileTurret>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = turret.ID;
        playerController.machineHasPower = turret.powerON;
        playerController.machineSpeed = turret.speed;
        playerController.machinePower = turret.power;
        playerController.machineHeat = turret.heat;
        playerController.machineCooling = turret.cooling;
        playerController.machineType = turret.ammoType;
        playerController.machineAmount = turret.ammoAmount;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Missile Turret");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedMachineSpeed = turret.speed;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a universal extractor.
    public void InteractWithUniversalExtractor()
    {
        UniversalExtractor extractor = playerController.objectInSight.GetComponent<UniversalExtractor>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = extractor.ID;
        playerController.collectorAmount = extractor.amount;
        playerController.machineHasPower = extractor.powerON;
        playerController.machinePower = extractor.power;
        playerController.machineSpeed = extractor.speed;
        playerController.machineHeat = extractor.heat;
        playerController.machineCooling = extractor.cooling;
        playerController.machineType = extractor.type;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Universal Extractor");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedMachineSpeed = extractor.speed;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at an auger.
    public void InteractWithAuger()
    {
        Auger auger = playerController.objectInSight.GetComponent<Auger>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = auger.ID;
        playerController.collectorAmount = auger.amount;
        playerController.machineHasPower = auger.powerON;
        playerController.machinePower = auger.power;
        playerController.machineSpeed = auger.speed;
        playerController.machineHeat = auger.heat;
        playerController.machineCooling = auger.cooling;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Auger");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedMachineSpeed = auger.speed;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a dark matter collector.
    public void InteractWithDarkMatterCollector()
    {
        DarkMatterCollector collector = playerController.objectInSight.GetComponent<DarkMatterCollector>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = collector.ID;
        playerController.collectorAmount = collector.darkMatterAmount;
        playerController.machineHasPower = collector.powerON;
        playerController.machinePower = collector.power;
        playerController.machineSpeed = collector.speed;
        playerController.machineHeat = collector.heat;
        playerController.machineCooling = collector.cooling;

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Dark Matter Collector");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = collector.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a universal conduit.
    public void InteractWithUniversalConduit()
    {
        UniversalConduit conduit = playerController.objectInSight.GetComponent<UniversalConduit>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = conduit.amount;
        playerController.machineID = conduit.ID;
        playerController.machineType = conduit.type;
        playerController.machineSpeed = conduit.speed;
        playerController.machineRange = conduit.range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (conduit.inputObject != null)
        {
            GameObject conduitInput = conduit.inputObject;
            if (conduitInput.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = conduitInput.GetComponent<UniversalConduit>().type;
            }
            if (conduitInput.GetComponent<UniversalExtractor>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<UniversalExtractor>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<UniversalExtractor>().amount;
                playerController.machineInputType = conduitInput.GetComponent<UniversalExtractor>().type;
            }
            if (conduitInput.GetComponent<Auger>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<Auger>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<Auger>().amount;
                playerController.machineInputType = "Regolith";
            }
            if (conduitInput.GetComponent<Smelter>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<Smelter>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<Smelter>().amount;
                playerController.machineInputType = conduitInput.GetComponent<Smelter>().outputType;
            }
            if (conduitInput.GetComponent<Press>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<Press>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<Press>().amount;
                playerController.machineInputType = conduitInput.GetComponent<Press>().outputType;
            }
            if (conduitInput.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<AlloySmelter>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<AlloySmelter>().amount;
                playerController.machineInputType = conduitInput.GetComponent<AlloySmelter>().outputType;
            }
            if (conduitInput.GetComponent<Extruder>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<Extruder>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<Extruder>().amount;
                playerController.machineInputType = conduitInput.GetComponent<Extruder>().outputType;
            }
            if (conduitInput.GetComponent<Retriever>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<Retriever>().ID;
                playerController.machineInputType = conduitInput.GetComponent<Retriever>().currentType;
            }
            if (conduitInput.GetComponent<HeatExchanger>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<HeatExchanger>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<HeatExchanger>().amount;
                playerController.machineInputType = conduitInput.GetComponent<HeatExchanger>().inputType;
            }
            if (conduitInput.GetComponent<GearCutter>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<GearCutter>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<GearCutter>().amount;
                playerController.machineInputType = conduitInput.GetComponent<GearCutter>().outputType;
            }
            if (conduitInput.GetComponent<ModMachine>() != null)
            {
                playerController.machineInputID = conduitInput.GetComponent<ModMachine>().ID;
                playerController.machineInputAmount = conduitInput.GetComponent<ModMachine>().amount;
                playerController.machineInputType = conduitInput.GetComponent<ModMachine>().outputType;
            }
        }
        if (conduit.outputObject != null)
        {
            GameObject conduitOutput = conduit.outputObject;
            if (conduitOutput.GetComponent<PowerSource>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<PowerSource>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<PowerSource>().fuelAmount;
                playerController.machineOutputType = conduitOutput.GetComponent<PowerSource>().fuelType;
            }
            if (conduitOutput.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<UniversalConduit>().type;
            }
            if (conduitOutput.GetComponent<Smelter>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<Smelter>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<Smelter>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<Smelter>().inputType;
            }
            if (conduitOutput.GetComponent<Press>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<Press>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<Press>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<Press>().inputType;
            }
            if (conduitOutput.GetComponent<Extruder>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<Extruder>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<Extruder>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<Extruder>().inputType;
            }
            if (conduitOutput.GetComponent<ModMachine>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<ModMachine>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<ModMachine>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<ModMachine>().inputType;
            }
            if (conduitOutput.GetComponent<HeatExchanger>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<HeatExchanger>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<HeatExchanger>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<HeatExchanger>().inputType;
            }
            if (conduitOutput.GetComponent<GearCutter>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<GearCutter>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<GearCutter>().amount;
                playerController.machineOutputType = conduitOutput.GetComponent<GearCutter>().inputType;
            }
            if (conduitOutput.GetComponent<MissileTurret>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<MissileTurret>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<MissileTurret>().ammoAmount;
                playerController.machineOutputType = conduitOutput.GetComponent<MissileTurret>().ammoType;
            }
            if (conduitOutput.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<AlloySmelter>().ID;
                playerController.machineOutputAmount = conduitOutput.GetComponent<AlloySmelter>().amount;
                if (conduit.type.Equals(conduitOutput.GetComponent<AlloySmelter>().inputType1))
                {
                    playerController.machineOutputType = conduitOutput.GetComponent<AlloySmelter>().inputType1;
                }
                else if (conduit.type.Equals(conduitOutput.GetComponent<AlloySmelter>().inputType2))
                {
                    playerController.machineOutputType = conduitOutput.GetComponent<AlloySmelter>().inputType2;
                }
            }
            if (conduitOutput.GetComponent<InventoryManager>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<InventoryManager>().ID;
                int storageTotal = 0;
                foreach (InventorySlot slot in conduitOutput.GetComponent<InventoryManager>().inventory)
                {
                    if (slot.typeInSlot.Equals(conduit.type))
                    {
                        storageTotal += slot.amountInSlot;
                        playerController.machineOutputType = slot.typeInSlot;
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
            if (conduitOutput.GetComponent<StorageComputer>() != null)
            {
                playerController.machineOutputID = conduitOutput.GetComponent<StorageComputer>().ID;
                int storageTotal = 0;
                foreach (InventoryManager manager in conduitOutput.GetComponent<StorageComputer>().computerContainers)
                {
                    foreach (InventorySlot slot in manager.inventory)
                    {
                        if (slot.typeInSlot.Equals(conduit.type))
                        {
                            storageTotal += slot.amountInSlot;
                            playerController.machineOutputType = slot.typeInSlot;
                        }
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Universal Conduit");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedConduitRange = conduit.range;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a dark matter conduit.
    public void InteractWithDarkMatterConduit()
    {
        DarkMatterConduit dmConduit = playerController.objectInSight.GetComponent<DarkMatterConduit>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = dmConduit.darkMatterAmount;
        playerController.machineID = dmConduit.ID;
        playerController.machineSpeed = dmConduit.speed;
        playerController.machineRange = dmConduit.range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (dmConduit.inputObject != null)
        {
            if (dmConduit.inputObject.GetComponent<DarkMatterConduit>() != null)
            {
                playerController.machineInputID = dmConduit.inputObject.GetComponent<DarkMatterConduit>().ID;
                playerController.machineInputAmount = dmConduit.inputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                playerController.machineInputType = "Dark Matter";
            }
            if (dmConduit.inputObject.GetComponent<DarkMatterCollector>() != null)
            {
                playerController.machineInputID = dmConduit.inputObject.GetComponent<DarkMatterCollector>().ID;
                playerController.machineInputAmount = dmConduit.inputObject.GetComponent<DarkMatterCollector>().darkMatterAmount;
                playerController.machineInputType = "Dark Matter";
            }
        }
        if (dmConduit.outputObject != null)
        {
            if (dmConduit.outputObject.GetComponent<DarkMatterConduit>() != null)
            {
                playerController.machineOutputID = dmConduit.outputObject.GetComponent<DarkMatterConduit>().ID;
                playerController.machineOutputAmount = dmConduit.outputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                playerController.machineOutputType = "Dark Matter";
            }
            if (dmConduit.outputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineOutputID = dmConduit.outputObject.GetComponent<StorageComputer>().ID;
                int storageTotal = 0;
                foreach (InventoryManager manager in dmConduit.outputObject.GetComponent<StorageComputer>().computerContainers)
                {
                    foreach (InventorySlot slot in manager.inventory)
                    {
                        if (slot.typeInSlot.Equals("Dark Matter"))
                        {
                            storageTotal += slot.amountInSlot;
                            playerController.machineOutputType = "Dark Matter";
                        }
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
            if (dmConduit.outputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineOutputID = dmConduit.outputObject.GetComponent<InventoryManager>().ID;
                int storageTotal = 0;
                foreach (InventorySlot slot in dmConduit.outputObject.GetComponent<InventoryManager>().inventory)
                {
                    if (slot.typeInSlot.Equals("Dark Matter"))
                    {
                        storageTotal += slot.amountInSlot;
                        playerController.machineOutputType = "Dark Matter";
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Dark Matter Conduit");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            playerController.networkedConduitRange = dmConduit.range;
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a smelter.
    public void InteractWithSmelter()
    {
        Smelter smelter = playerController.objectInSight.GetComponent<Smelter>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = smelter.amount;
        playerController.machineID = smelter.ID;
        playerController.machineHasPower = smelter.powerON;
        playerController.machineType = smelter.inputType;
        playerController.machinePower = smelter.power;
        playerController.machineSpeed = smelter.speed;
        playerController.machineHeat = smelter.heat;
        playerController.machineCooling = smelter.cooling;
        if (smelter.inputObject != null)
        {
            if (smelter.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = smelter.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = smelter.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = smelter.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (smelter.outputObject != null)
        {
            if (smelter.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = smelter.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = smelter.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = smelter.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Smelter");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = smelter.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at an alloy smelter.
    public void InteractWithAlloySmelter()
    {
        AlloySmelter alloySmelter = playerController.objectInSight.GetComponent<AlloySmelter>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = alloySmelter.amount;
        playerController.machineAmount2 = alloySmelter.amount2;
        playerController.machineID = alloySmelter.ID;
        playerController.machineHasPower = alloySmelter.powerON;
        playerController.machineType = alloySmelter.inputType1;
        playerController.machineType2 = alloySmelter.inputType2;
        playerController.machinePower = alloySmelter.power;
        playerController.machineSpeed = alloySmelter.speed;
        playerController.machineHeat = alloySmelter.heat;
        playerController.machineCooling = alloySmelter.cooling;
        if (alloySmelter.inputObject1 != null)
        {
            if (alloySmelter.inputObject1.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = alloySmelter.inputObject1.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = alloySmelter.inputObject1.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = alloySmelter.inputObject1.GetComponent<UniversalConduit>().type;
            }
        }
        if (alloySmelter.inputObject2 != null)
        {
            if (alloySmelter.inputObject2.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID2 = alloySmelter.inputObject2.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount2 = alloySmelter.inputObject2.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType2 = alloySmelter.inputObject2.GetComponent<UniversalConduit>().type;
            }
        }
        if (alloySmelter.outputObject != null)
        {
            if (alloySmelter.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = alloySmelter.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = alloySmelter.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = alloySmelter.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Alloy Smelter");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = alloySmelter.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at an extruder.
    public void InteractWithExtruder()
    {
        Extruder extruder = playerController.objectInSight.GetComponent<Extruder>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = extruder.amount;
        playerController.machineID = extruder.ID;
        playerController.machineHasPower = extruder.powerON;
        playerController.machineType = extruder.inputType;
        playerController.machinePower = extruder.power;
        playerController.machineSpeed = extruder.speed;
        playerController.machineHeat = extruder.heat;
        playerController.machineCooling = extruder.cooling;
        if (extruder.inputObject != null)
        {
            if (extruder.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = extruder.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = extruder.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = extruder.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (extruder.outputObject != null)
        {
            if (extruder.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = extruder.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = extruder.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = extruder.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Extruder");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = extruder.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at any machine added through the modding API (excluding plugins).
    public void InteractWithModMachine()
    {
        ModMachine modMachine = playerController.objectInSight.GetComponent<ModMachine>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = modMachine.amount;
        playerController.machineID = modMachine.ID;
        playerController.machineHasPower = modMachine.powerON;
        playerController.machineType = modMachine.inputType;
        playerController.machinePower = modMachine.power;
        playerController.machineSpeed = modMachine.speed;
        playerController.machineHeat = modMachine.heat;
        playerController.machineCooling = modMachine.cooling;
        if (modMachine.inputObject != null)
        {
            if (modMachine.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = modMachine.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = modMachine.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = modMachine.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (modMachine.outputObject != null)
        {
            if (modMachine.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = modMachine.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = modMachine.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = modMachine.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, modMachine.machineName);
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = modMachine.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a rail cart hub.
    public void InteractWithRailCartHub()
    {
        RailCartHub railCartHub = playerController.objectInSight.GetComponent<RailCartHub>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = railCartHub.ID;
        playerController.machineRange = railCartHub.range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (railCartHub.inputObject != null)
        {
            if (railCartHub.inputObject.GetComponent<RailCartHub>() != null)
            {
                playerController.machineInputID = railCartHub.inputObject.GetComponent<RailCartHub>().ID;
            }
        }
        if (railCartHub.outputObject != null)
        {
            if (railCartHub.outputObject.GetComponent<RailCartHub>() != null)
            {
                playerController.machineOutputID = railCartHub.outputObject.GetComponent<RailCartHub>().ID;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Rail Cart Hub");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            interactionController.ToggleMachineGUI();
        }
    }

    //! Called when the player is looking at a retriever.
    public void InteractWithRetriever()
    {
        Retriever retriever = playerController.objectInSight.GetComponent<Retriever>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = retriever.ID;
        playerController.machineHasPower = retriever.powerON;
        if (retriever.type.Count > 1)
        {
            playerController.machineType = "multiple items";
        }
        else if (retriever.type.Count > 0)
        {
            playerController.machineType = retriever.type[0];
        }
        else
        {
            playerController.machineType = "nothing";
        }
        playerController.machinePower = retriever.power;
        playerController.machineSpeed = retriever.speed;
        playerController.machineHeat = retriever.heat;
        playerController.machineCooling = retriever.cooling;
        playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
        if (retriever.inputObject != null)
        {
            if (retriever.inputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineInputID = retriever.inputObject.GetComponent<InventoryManager>().ID;
            }
            if (retriever.inputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineInputID = retriever.inputObject.GetComponent<StorageComputer>().ID;
            }
        }
        if (retriever.outputObject != null)
        {
            if (retriever.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = retriever.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = retriever.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = retriever.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Retriever");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = retriever.speed;
                playerController.machineGUIopen = true;
                playerController.remoteStorageActive = false;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a heat exchanger.
    public void InteractWithHeatExchanger()
    {
        HeatExchanger heatExchanger = playerController.objectInSight.GetComponent<HeatExchanger>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = heatExchanger.amount;
        playerController.machineID = heatExchanger.ID;
        playerController.machineType = heatExchanger.inputType;
        playerController.machineSpeed = heatExchanger.speed;
        if (heatExchanger.inputObject != null)
        {
            if (heatExchanger.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = heatExchanger.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = heatExchanger.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = heatExchanger.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (heatExchanger.outputObject != null)
        {
            if (heatExchanger.outputObject.GetComponent<UniversalExtractor>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<UniversalExtractor>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<DarkMatterCollector>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<DarkMatterCollector>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Auger>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Auger>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Smelter>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Smelter>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Extruder>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Extruder>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Retriever>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Retriever>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<AutoCrafter>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<AutoCrafter>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Press>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Press>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<AlloySmelter>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<GearCutter>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<GearCutter>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<Turret>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<Turret>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<MissileTurret>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<MissileTurret>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<NuclearReactor>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<NuclearReactor>().ID;
            }
            if (heatExchanger.outputObject.GetComponent<ModMachine>() != null)
            {
                playerController.machineOutputID = heatExchanger.outputObject.GetComponent<ModMachine>().ID;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Heat Exchanger");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = heatExchanger.speed;
                playerController.hxAmount = heatExchanger.amount;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a gear cutter.
    public void InteractWithGearCutter()
    {
        GearCutter gearCutter = playerController.objectInSight.GetComponent<GearCutter>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = gearCutter.amount;
        playerController.machineID = gearCutter.ID;
        playerController.machineHasPower = gearCutter.powerON;
        playerController.machineType = gearCutter.inputType;
        playerController.machinePower = gearCutter.power;
        playerController.machineSpeed = gearCutter.speed;
        playerController.machineHeat = gearCutter.heat;
        playerController.machineCooling = gearCutter.cooling;
        if (gearCutter.inputObject != null)
        {
            if (gearCutter.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = gearCutter.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = gearCutter.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = gearCutter.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (gearCutter.outputObject != null)
        {
            if (gearCutter.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = gearCutter.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = gearCutter.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = gearCutter.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Gear Cutter");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = gearCutter.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a press.
    public void InteractWithPress()
    {
        Press press = playerController.objectInSight.GetComponent<Press>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = press.amount;
        playerController.machineID = press.ID;
        playerController.machineHasPower = press.powerON;
        playerController.machineType = press.inputType;
        playerController.machinePower = press.power;
        playerController.machineSpeed = press.speed;
        playerController.machineHeat = press.heat;
        playerController.machineCooling = press.cooling;
        if (press.inputObject != null)
        {
            if (press.inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = press.inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = press.inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = press.inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (press.outputObject != null)
        {
            if (press.outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = press.outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = press.outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = press.outputObject.GetComponent<UniversalConduit>().type;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Press");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = press.speed;
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at an auto crafter.
    public void InteractWithAutoCrafter()
    {
        AutoCrafter autoCrafter = playerController.objectInSight.GetComponent<AutoCrafter>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = autoCrafter.ID;
        playerController.machineHasPower = autoCrafter.powerON;
        playerController.machineType = autoCrafter.type;
        playerController.machinePower = autoCrafter.power;
        playerController.machineSpeed = autoCrafter.speed;
        playerController.machineHeat = autoCrafter.heat;
        playerController.machineCooling = autoCrafter.cooling;
        playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
        if (autoCrafter.inputObject != null)
        {
            if (autoCrafter.inputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineInputID = autoCrafter.inputObject.GetComponent<InventoryManager>().ID;
            }
            if (autoCrafter.inputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineInputID = autoCrafter.inputObject.GetComponent<StorageComputer>().ID;
            }
        }

        if(cInput.GetKey("Collect Object"))
        {
            playerController.digTime += 1 * Time.deltaTime;
            if (playerController.digTime > 0.15f)
            {
                interactionController.CollectObject(playerController.objectInSight, "Auto Crafter");
                playerController.digTime = 0;
            }
        }
        else
        {
            playerController.digTime = 0;
        }

        if (cInput.GetKeyDown("Interact"))
        {
            if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
            {
                if (!interactionController.CanInteract())
                {
                    return;
                }
            }
            if (playerController.machineGUIopen == false)
            {
                playerController.networkedMachineSpeed = autoCrafter.speed;
                playerController.machineGUIopen = true;
                playerController.remoteStorageActive = false;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    //! Called when the player is looking at a protection block.
    public void InteractWithProtectionBlock()
    {
        ProtectionBlock protectionBlock = playerController.objectInSight.GetComponent<ProtectionBlock>();
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = protectionBlock.ID;
        playerController.protectionList = "";
        List<string> userNames = protectionBlock.GetUserNames();
        if (userNames != null)
        {
            foreach (string userName in protectionBlock.GetUserNames())
            {
                playerController.protectionList += userName + "\n";
            }

            if(cInput.GetKey("Collect Object"))
            {
                playerController.digTime += 1 * Time.deltaTime;
                if (playerController.digTime > 0.15f)
                {
                    interactionController.CollectObject(playerController.objectInSight, "Protection Block");
                    playerController.digTime = 0;
                }
            }
            else
            {
                playerController.digTime = 0;
            }

            if (cInput.GetKeyDown("Interact"))
            {
                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                {
                    if (!interactionController.CanInteract())
                    {
                        return;
                    }
                }
                protectionBlock.visible = !protectionBlock.visible;
                playerController.PlayMissingItemsSound();
            }
        }
    }
}