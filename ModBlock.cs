using UnityEngine;

public class ModBlock : Block
{
    private Material material;
    private StateManager stateManager;
    private PlayerController playerController;
    private GameManager gameManager;
    public GameObject glassBreak;
    private bool init;
    public int address;
    public string ID = "unassigned";
    public string blockName;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        material = new Material(Shader.Find("Standard"));
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        stateManager = gameManager.GetComponent<StateManager>();
    }

    //! Called once per frame by unity engine.
    public override void UpdateBlock()
    {
        if (!stateManager.Busy() && init == false)
        {
            BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
            if (blockDictionary.meshDictionary.ContainsKey(blockName))
            {
                GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[blockName];
            }
            gameManager.meshManager.SetMaterial(gameObject, blockName);
            if (blockName.ToUpper().Contains("GLASS"))
            {
                GetComponent<PhysicsHandler>().explosion = glassBreak;
            }
            init = true;
        }

        if (ID != "unassigned")
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
        }
    }
}