using UnityEngine;

public class MachineInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private InteractionController interactionController;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionController = GetComponent<InteractionController>();
    }

    public void InteractWithElectricLight()
    {
        if(cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Electric Light");
        }
    }

    public void InteractWithAirLock()
    {
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Quantum Hatchway");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            AirLock[] airLocks = FindObjectsOfType<AirLock>();
            foreach (AirLock a in airLocks)
            {
                if (Vector3.Distance(transform.position, a.transform.position) < 40)
                {
                    a.ToggleOpen();
                }
            }
            if (playerController.objectInSight.GetComponent<AirLock>().open == true)
            {
                playerController.objectInSight.GetComponent<AirLock>().openObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                playerController.objectInSight.GetComponent<AirLock>().closedObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void InteractWithPowerSource()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<PowerSource>().ID;
        playerController.machineOutputID = playerController.objectInSight.GetComponent<PowerSource>().outputID;

        if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Solar Panel"))
        {
            if (playerController.objectInSight.GetComponent<PowerSource>().blocked == false)
            {
                playerController.machinePower = 1;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }
        else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Generator"))
        {
            playerController.machineAmount = playerController.objectInSight.GetComponent<PowerSource>().fuelAmount;
            playerController.machineType = playerController.objectInSight.GetComponent<PowerSource>().fuelType;
            if (playerController.objectInSight.GetComponent<PowerSource>().outOfFuel == false)
            {
                playerController.machinePower = 10;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }
        else if (playerController.objectInSight.GetComponent<PowerSource>().type.Equals("Reactor Turbine"))
        {
            if (playerController.objectInSight.GetComponent<PowerSource>().noReactor == false)
            {
                playerController.machinePower = 100;
            }
            else
            {
                playerController.machinePower = 0;
            }
        }

        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject(playerController.objectInSight.GetComponent<PowerSource>().type);
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithNuclearReactor()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<NuclearReactor>().ID;
        playerController.machineCooling = playerController.objectInSight.GetComponent<NuclearReactor>().cooling;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Nuclear Reactor");
        }
    }

    public void InteractWithPowerConduit()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<PowerConduit>().ID;
        playerController.machineOutputID = playerController.objectInSight.GetComponent<PowerConduit>().outputID1;
        playerController.machineOutputID2 = playerController.objectInSight.GetComponent<PowerConduit>().outputID2;
        playerController.machinePower = playerController.objectInSight.GetComponent<PowerConduit>().powerAmount;
        playerController.machineRange = playerController.objectInSight.GetComponent<PowerConduit>().range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Power Conduit");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithTurret()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<Turret>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Turret>().powerON;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Turret>().speed;
        playerController.machinePower = playerController.objectInSight.GetComponent<Turret>().power;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Turret>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Turret>().cooling;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Turret");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithUniversalExtractor()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<UniversalExtractor>().ID;
        playerController.collectorAmount = playerController.objectInSight.GetComponent<UniversalExtractor>().amount;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<UniversalExtractor>().powerON;
        playerController.machinePower = playerController.objectInSight.GetComponent<UniversalExtractor>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<UniversalExtractor>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<UniversalExtractor>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<UniversalExtractor>().cooling;
        playerController.machineType = playerController.objectInSight.GetComponent<UniversalExtractor>().type;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Universal Extractor");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithAuger()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<Auger>().ID;
        playerController.collectorAmount = playerController.objectInSight.GetComponent<Auger>().amount;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Auger>().powerON;
        playerController.machinePower = playerController.objectInSight.GetComponent<Auger>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Auger>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Auger>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Auger>().cooling;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Auger");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithDarkMatterCollector()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<DarkMatterCollector>().ID;
        playerController.collectorAmount = playerController.objectInSight.GetComponent<DarkMatterCollector>().darkMatterAmount;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<DarkMatterCollector>().powerON;
        playerController.machinePower = playerController.objectInSight.GetComponent<DarkMatterCollector>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<DarkMatterCollector>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<DarkMatterCollector>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<DarkMatterCollector>().cooling;
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Dark Matter Collector");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithUniversalConduit()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<UniversalConduit>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<UniversalConduit>().ID;
        playerController.machineType = playerController.objectInSight.GetComponent<UniversalConduit>().type;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<UniversalConduit>().speed;
        playerController.machineRange = playerController.objectInSight.GetComponent<UniversalConduit>().range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalConduit>().type;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<UniversalExtractor>().type;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Auger>().amount;
                playerController.machineInputType = "Regolith";
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Smelter>().outputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Press>().outputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<AlloySmelter>().outputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Extruder>().outputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>().ID;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<Retriever>().currentType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<HeatExchanger>().type;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<UniversalConduit>().inputObject.GetComponent<GearCutter>().outputType;
            }
        }
        if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<UniversalConduit>().type;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Smelter>().inputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Press>().inputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<Extruder>().inputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<HeatExchanger>().inputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<GearCutter>().inputType;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().amount;
                if (playerController.objectInSight.GetComponent<UniversalConduit>().type.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType1))
                {
                    playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType1;
                }
                else if (playerController.objectInSight.GetComponent<UniversalConduit>().type.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType2))
                {
                    playerController.machineOutputType = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<AlloySmelter>().inputType2;
                }
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>().ID;
                int storageTotal = 0;
                foreach (InventorySlot slot in playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<InventoryManager>().inventory)
                {
                    if (slot.typeInSlot.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().type))
                    {
                        storageTotal += slot.amountInSlot;
                        playerController.machineOutputType = slot.typeInSlot;
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
            if (playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>().ID;
                int storageTotal = 0;
                foreach (InventoryManager manager in playerController.objectInSight.GetComponent<UniversalConduit>().outputObject.GetComponent<StorageComputer>().computerContainers)
                {
                    foreach (InventorySlot slot in manager.inventory)
                    {
                        if (slot.typeInSlot.Equals(playerController.objectInSight.GetComponent<UniversalConduit>().type))
                        {
                            storageTotal += slot.amountInSlot;
                            playerController.machineOutputType = slot.typeInSlot;
                        }
                    }
                }
                playerController.machineOutputAmount = storageTotal;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Universal Conduit");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithDarkMatterConduit()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().darkMatterAmount;
        playerController.machineID = playerController.objectInSight.GetComponent<DarkMatterConduit>().ID;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<DarkMatterConduit>().speed;
        playerController.machineRange = playerController.objectInSight.GetComponent<DarkMatterConduit>().range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                playerController.machineInputType = "Dark Matter";
            }
            if (playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().inputObject.GetComponent<DarkMatterCollector>().darkMatterAmount;
                playerController.machineInputType = "Dark Matter";
            }
        }
        if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<DarkMatterConduit>().darkMatterAmount;
                playerController.machineOutputType = "Dark Matter";
            }
            if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>().ID;
                int storageTotal = 0;
                foreach (InventoryManager manager in playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<StorageComputer>().computerContainers)
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
            if (playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>().ID;
                int storageTotal = 0;
                foreach (InventorySlot slot in playerController.objectInSight.GetComponent<DarkMatterConduit>().outputObject.GetComponent<InventoryManager>().inventory)
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
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Dark Matter Conduit");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithSmelter()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<Smelter>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<Smelter>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Smelter>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<Smelter>().inputType;
        playerController.machinePower = playerController.objectInSight.GetComponent<Smelter>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Smelter>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Smelter>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Smelter>().cooling;
        if (playerController.objectInSight.GetComponent<Smelter>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<Smelter>().inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<Smelter>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<Smelter>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Smelter");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithAlloySmelter()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<AlloySmelter>().amount;
        playerController.machineAmount2 = playerController.objectInSight.GetComponent<AlloySmelter>().amount2;
        playerController.machineID = playerController.objectInSight.GetComponent<AlloySmelter>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<AlloySmelter>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<AlloySmelter>().inputType1;
        playerController.machineType2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputType2;
        playerController.machinePower = playerController.objectInSight.GetComponent<AlloySmelter>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<AlloySmelter>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<AlloySmelter>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<AlloySmelter>().cooling;
        if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1 != null)
        {
            if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject1.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2 != null)
        {
            if (playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType2 = playerController.objectInSight.GetComponent<AlloySmelter>().inputObject2.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<AlloySmelter>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<AlloySmelter>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Alloy Smelter");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithExtruder()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<Extruder>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<Extruder>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Extruder>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<Extruder>().inputType;
        playerController.machinePower = playerController.objectInSight.GetComponent<Extruder>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Extruder>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Extruder>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Extruder>().cooling;
        if (playerController.objectInSight.GetComponent<Extruder>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<Extruder>().inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<Extruder>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<Extruder>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Extruder");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithRailCartHub()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<RailCartHub>().ID;
        playerController.machineRange = playerController.objectInSight.GetComponent<RailCartHub>().range;
        if (playerController.machineRange < 10)
        {
            playerController.machineRange = 10;
        }
        if (playerController.objectInSight.GetComponent<RailCartHub>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<RailCartHub>().inputObject.GetComponent<RailCartHub>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<RailCartHub>().inputObject.GetComponent<RailCartHub>().ID;
            }
        }
        if (playerController.objectInSight.GetComponent<RailCartHub>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<RailCartHub>().outputObject.GetComponent<RailCartHub>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<RailCartHub>().outputObject.GetComponent<RailCartHub>().ID;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Rail Cart Hub");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            interactionController.OpenMachineGUI();
        }
    }

    public void InteractWithRetriever()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<Retriever>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Retriever>().powerON;
        if (playerController.objectInSight.GetComponent<Retriever>().type.Count > 1)
        {
            playerController.machineType = "multiple items";
        }
        else if (playerController.objectInSight.GetComponent<Retriever>().type.Count > 0)
        {
            playerController.machineType = playerController.objectInSight.GetComponent<Retriever>().type[0];
        }
        else
        {
            playerController.machineType = "nothing";
        }
        playerController.machinePower = playerController.objectInSight.GetComponent<Retriever>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Retriever>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Retriever>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Retriever>().cooling;
        playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
        if (playerController.objectInSight.GetComponent<Retriever>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<InventoryManager>().ID;
            }
            if (playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<Retriever>().inputObject.GetComponent<StorageComputer>().ID;
            }
        }
        if (playerController.objectInSight.GetComponent<Retriever>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<Retriever>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Retriever");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
                playerController.remoteStorageActive = false;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithHeatExchanger()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<HeatExchanger>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<HeatExchanger>().ID;
        playerController.machineType = playerController.objectInSight.GetComponent<HeatExchanger>().inputType;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<HeatExchanger>().speed;
        if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<HeatExchanger>().inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<UniversalExtractor>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<UniversalExtractor>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<DarkMatterCollector>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<DarkMatterCollector>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Auger>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Auger>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Smelter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Smelter>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Extruder>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Extruder>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Retriever>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Retriever>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AutoCrafter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AutoCrafter>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Press>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Press>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AlloySmelter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<AlloySmelter>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<GearCutter>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<GearCutter>().ID;
            }
            if (playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Turret>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<HeatExchanger>().outputObject.GetComponent<Turret>().ID;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Heat Exchanger");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithGearCutter()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<GearCutter>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<GearCutter>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<GearCutter>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<GearCutter>().inputType;
        playerController.machinePower = playerController.objectInSight.GetComponent<GearCutter>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<GearCutter>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<GearCutter>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<GearCutter>().cooling;
        if (playerController.objectInSight.GetComponent<GearCutter>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<GearCutter>().inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<GearCutter>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<GearCutter>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Gear Cutter");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithPress()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineAmount = playerController.objectInSight.GetComponent<Press>().amount;
        playerController.machineID = playerController.objectInSight.GetComponent<Press>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<Press>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<Press>().inputType;
        playerController.machinePower = playerController.objectInSight.GetComponent<Press>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<Press>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<Press>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<Press>().cooling;
        if (playerController.objectInSight.GetComponent<Press>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineInputAmount = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineInputType = playerController.objectInSight.GetComponent<Press>().inputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (playerController.objectInSight.GetComponent<Press>().outputObject != null)
        {
            if (playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>() != null)
            {
                playerController.machineOutputID = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().ID;
                playerController.machineOutputAmount = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().amount;
                playerController.machineOutputType = playerController.objectInSight.GetComponent<Press>().outputObject.GetComponent<UniversalConduit>().type;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Press");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }

    public void InteractWithAutoCrafter()
    {
        playerController.machineInSight = playerController.objectInSight;
        playerController.machineID = playerController.objectInSight.GetComponent<AutoCrafter>().ID;
        playerController.machineHasPower = playerController.objectInSight.GetComponent<AutoCrafter>().powerON;
        playerController.machineType = playerController.objectInSight.GetComponent<AutoCrafter>().type;
        playerController.machinePower = playerController.objectInSight.GetComponent<AutoCrafter>().power;
        playerController.machineSpeed = playerController.objectInSight.GetComponent<AutoCrafter>().speed;
        playerController.machineHeat = playerController.objectInSight.GetComponent<AutoCrafter>().heat;
        playerController.machineCooling = playerController.objectInSight.GetComponent<AutoCrafter>().cooling;
        playerController.storageInventory = playerController.objectInSight.GetComponent<InventoryManager>();
        if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject != null)
        {
            if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<InventoryManager>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<InventoryManager>().ID;
            }
            if (playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<StorageComputer>() != null)
            {
                playerController.machineInputID = playerController.objectInSight.GetComponent<AutoCrafter>().inputObject.GetComponent<StorageComputer>().ID;
            }
        }
        if (cInput.GetKeyDown("Collect Object"))
        {
            interactionController.CollectObject("Auto Crafter");
        }
        if (cInput.GetKeyDown("Interact"))
        {
            if (playerController.machineGUIopen == false)
            {
                playerController.machineGUIopen = true;
                playerController.remoteStorageActive = false;
            }
            else
            {
                playerController.machineGUIopen = false;
            }
        }
    }
}
