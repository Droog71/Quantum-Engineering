public class Descriptions
{
    public string alloySmelter = "Combines tin and copper ingots to make bronze ingots." +
    " Combines coal and iron ingots to make steel ingots. Requires 3 conduits. 1 for each input and 1 for the output." +
    " Requires a power source such as a solar panel, nuclear reactor or power conduit." +
    " The alloy smelter has an adjustable output measured in items per cycle.";

    public string auger = "Extracts regolith from the lunar surface which can be pressed into bricks or smelted to create glass." +
    " Glass blocks have a 100% chance of being destroyed by meteors and other hazards." +
    " Bricks have a 75% chance of being destroyed by meteors and other hazards." +
    " Augers must be placed directly on the lunar surface and require power from a solar panel, nuclear reactor or power conduit.";
        
    public string autoCrafter = "Automatically crafts objects using items from an attached storage container." +
    " Place within 2 meters of the storage container. Then, place an item of the desired type into the auto crafter's inventory." +
    " This will designate that item as the item to be crafted. Crafted items will be deposited into the attached storage container." +
    " This machine requires power and has an adjustable output measured in items per cycle.";

    public string circuitBoard = "A combination of conductive, semi-conductive and insulating materials combined to create a logic processing circuit.";

    public string darkMatterCollector = "Harvests dark matter which is then transferred to a dark matter conduit." +
    " Requires a power source such as a solar panel, nuclear reactor or power conduit." +
    " The dark matter collector has an adjustable output measured in items per cycle.";

    public string darkMatterConduit = "Transfers dark matter from a collector to a storage container or another conduit." +
    " Dark matter conduits have an adjustable input/output range and do not require power to operate.";

    public string electricLight = "For interior lighting. Requires power from a solar panel, nuclear reactor or power conduit.";

    public string electricMotor = "A device that converts electrical energy to mechanical torque.";

    public string extruder = "Creates wire from copper and aluminum ingots." +
    " Creates pipes from iron and steel ingots." +
    " Ingots must be supplied to the extruder using universal conduits." +
    " Another universal conduit should be placed within 2 meters of the machine to accept the output." +
    " The extruder requires power from a solar panel, nuclear reactor or power conduit and has an adjustable output measured in items per cycle.";

    public string gearCutter = "Cuts plates into gears. Plates must be supplied to the gear cutter using universal conduits." +
    " Place another conduit within 2 meters of the machine for the output." +
    " The gear cutter must be connected to a power source such as a solar panel, nuclear reactor or power conduit." +
    " The gear cutter has an adjustable output measured in items per cycle.";

    public string generator = "Provides 20 MW of power to a single machine or power conduit." +
    " Must be placed within 4 meters of the machine. Multiple generators can be connected to a machine" +
    " or power conduit to increase the amount of power provided. If a machine is provided with greater than 2 MW of power," +
    " the machine's output can be increased. This will generate heat, requiring a heat exchanger to compensate." +
    " Generators must be connected to a universal conduit supplying coal for fuel.";

    public string heatExchanger = "Cools down a machine to allow overclocking." +
    " Requires a supply of ice from a universal conduit." +
    " Increasing the output of the heat exchanger increases the amount of ice required." +
    " This can be compensated for by overclocking the extractor that is supplying the ice." +
    " Machines cannot be connected to more than one heat exchanger. The heat exchanger's output is measured in KBTU" +
    " and will consume 1 ice per 1 KBTU of cooling each cycle.";

    public string ironBlock = "Iron block for building structures. " +
    "Iron blocks have a 25% chance of being destroyed by meteors and other hazards. " +
    "1 plate creates 10 blocks. Hold left shift when clicking to craft 100.";

    public string ironRamp = "Iron ramp for building structures." +
    " Iron ramps have a 25% chance of being destroyed by meteors and other hazards." +
    " 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.";

    public string nuclearReactor = "Nuclear reactors are used to drive reactor turbines." +
    " Turbines must be directly attached to the reactor." +
    " The reactor will require a heat exchanger providing 5 KBTU cooling per turbine.";

    public string powerConduit = "Transfers power from a power source to a machine or to another power conduit." +
    " When used with two outputs, power will be distributed evenly." +
    " This machine has an adjustable range setting.";

    public string press = "Presses iron, copper, aluminum or tin ingots into plates." +
    " Ingots must be supplied to the press using universal conduits." +
    " Another universal conduit should be placed within 2 meters of the machine to accept the output." +
    " Must be connected to a power source such as a solar panel, nuclear reactor or power conduit" +
    " and has an adjustable output measured in items per cycle.";

    public string door = "Standard door used for entering structures.";

    public string quantumHatchway = "Forcefield door used for entering structures.";

    public string railCart = "A mobile storage container that rides on rails from one rail cart hub to the next." +
    " Configure the hubs to stop the cart near a conduit or retriever so it can be loaded or unloaded." +
    " Must be placed on a rail cart hub.";

    public string railCartHub = "Provides a waypoint for rail carts." +
    " Has an adjustable range at which the next hub will be located and rails deployed to it's location." +
    " Rail cart hubs can be configured to stop the rail cart so it can be loaded and unloaded.";

    public string reactorTurbine = "Provides 200 MW of power to a single machine or power conduit." +
    " Reactor turbines must be directly attached to a properly functioning, adequately cooled nuclear reactor." +
    " Must be placed within 4 meters of the machine. Multiple reactor turbines can be connected to a machine" +
    " or power conduit to increase the amount of power provided." +
    " If a machine is provided with greater than 2 MW of power, the machine's output can be increased." +
    " This will generate heat, requiring a heat exchanger to compensate.";

    public string retriever = "Retrieves items from a storage container and transfers them to a universal conduit." +
    " Place an item of each desired type into the retrievers inventory to designate that item for retrieval." +
    " Place within 2 meters of a storage container and a universal conduit." +
    " This machine requires power and it's output is adjustable. If the retriever is moving ice, it will not require cooling." +
    " The retriever's output is measured in items per cycle.";

    public string smelter = "Smelts ore into ingots. Can also be used to make glass when supplied with regolith." +
    " Ore must be supplied to the smelter using universal conduits." +
    " Place another conduit within 2 meters of the machine for the output." +
    " This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit." +
    " The output of a smelter is measured in items per cycle.";

    public string solarPanel = "Provides 2 MW of power to a single machine or power conduit." +
    " Must be placed within 4 meters of the machine. Multiple solar panels can be connected" +
    " to a machine or power conduit to increase the amount of power provided." +
    " If a machine is provided with greater than 2 MW of power, the machine's output can be increased." +
    " This will generate heat, requiring a heat exchanger to compensate.";

    public string steelBlock = "Steel block for building structures." +
    " Steel blocks have a 1% chance of being destroyed by meteors and other hazards." +
    " 1 plate creates 10 blocks. Hold left shift when clicking to craft 100.";

    public string steelRamp = "Steel ramp for building structures." +
	" Steel ramps have a 1% chance of being destroyed by meteors and other hazards." +
	" 1 plate creates 10 ramps. Hold left shift when clicking to craft 100.";

    public string storageComputer = "Provides access to all stationary storage containers within 4 meters." +
	" Can be accessed manually or connected to retrievers, auto crafters and conduits." +
	" When a conduit is connectextureDictionary.dictionary to the computer," +
	" the computer will store items starting with the first container found to have space available." +
	" When a retriever is connected to the computer, the computer will search all of the managed containers for desired items.";

    public string storageContainer = "Storage container for objects and items." +
    " Can be used to manually store items or connected to machines for automation." +
    " Universal conduits, dark matter conduits, retrievers and auto crafters can all connect to storage containers.";

    public string turret = "Protects your equipment from meteor showers and other hazards." +
	" Requires a power source such as a solar panel, nuclear reactor or power conduit." +
	" Turrets have an adjustable output measured in rounds per minute.";

    public string missileTurret = "Protects your equipment from meteor showers and other hazards." +
    " Requires a power source such as a solar panel, nuclear reactor or power conduit." +
    " Turrets have an adjustable output measured in rounds per minute." +
    " Missile turrets must be supplied with missiles via universal conduit.";

    public string missile = "Ammo for missile turrets. Highly effective against hostile spacecraft.";

    public string universalConduit = "Transfers items from a machine to another universal conduit, another machine or a storage container." +
    " Universal conduits have an adjustable input/output range and do not require power to operate.";

    public string universalExtractor = "Extracts ore, coal and ice from deposits found on the lunar surface." +
    " Place within 2 meters of the desired resource and use a universal conduit to handle the harvested materials." +
    " When extracting ice, the extractor will not need a heat exchanger for cooling." +
    " This machine must be connected to a power source such as a solar panel, nuclear reactor or power conduit" +
    " and has an adjustable output measured in items per cycle.";

    public string protectionBlock = "In multiplayer games, this machine prevents other players from placing or removing objects within a certain boundary." +
    " If multiple players would like to share a protected area, the machine needs to be placed while all of those players are within the boundary." +
    " All players within range will be added to the authorized user list for that protection block." +
    " Each player is limited to 16 protection blocks, regardless of the number of users associated with the block.";

    public string chunkSize = "This setting adjusts the size of chunks loaded around the player." +
    " A 'chunk' is an area of blocks separated from their combined mesh to allow editing by the player." +
    " Blocks farther from the player that are not within the loaded chunk are combined into a single object." +
    " A smaller chunk size will increase the game's performance." +
    " A larger chunk size will allow you to modify structures you have built from a greater distance.";

    public string blockPhysics = "With block physics enabled, objects you place in the world are affected by gravity." +
    " Structures you build will need to be supported or they will collapse." +
    " Hazards can sometimes damage your structures in a way that will cause a collapse." +
    " Blocks that land with significant force when falling are often destroyed. ";

    public string hazards = "With hazards enabled, your base will be impacted by meteor showers and attacked by hostile spacecraft." +
    "You will need to shoot down meteors with your laser cannon and build turrets to defend your base.";

    public string simulationSpeed = "This setting affects the frequency of machine operations and block physics updates. " +
    "Lower simulation speeds increase the game's performance. Higher speeds cause machines to generate items faster.";
}

