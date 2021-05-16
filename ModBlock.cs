using UnityEngine;

public class ModBlock : MonoBehaviour
{
    private Material material;
    private StateManager stateManager;
    private PlayerController playerController;
    private GameManager gameManager;
    public GameObject glassBreak;
    private bool init;
    private float updateTick;
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
    public void Update()
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
            updateTick += 1 * Time.deltaTime;
            if (updateTick > 0.5f + (address * 0.001f))
            {
                if (stateManager.Busy())
                {
                    updateTick = 0;
                    return;
                }
                GetComponent<PhysicsHandler>().UpdatePhysics();
            }
        }
    }
}