using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fire;
    public float altitude;
    public bool destroying;
    private GameManager game;
    private PlayerController playerController;
    private float destroyTimer;


    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Returns true if a message can be sent to the player's tablet
    private bool CanSendDestructionMessage()
    {
        return playerController.timeToDeliver == false && playerController.meteorShowerWarningActive == false && playerController.pirateAttackWarningActive == false;
    }

    // Called once per frame by unity engine
    public void Update()
    {
        if (destroying == false)
        {
            transform.position -= transform.up * 50 * Time.deltaTime;
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit altitudeHit, 5000))
            {
                altitude = Vector3.Distance(transform.position, altitudeHit.point);
            }
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 5))
            {
                if (hit.collider.gameObject.tag.Equals("Built"))
                {
                    if (hit.collider.gameObject.GetComponent<PhysicsHandler>() != null)
                    {
                        hit.collider.gameObject.GetComponent<PhysicsHandler>().Explode();
                    }
                    if (CanSendDestructionMessage())
                    {
                        if (playerController.destructionMessageActive == false)
                        {
                            playerController.destructionMessageActive = true;
                            playerController.currentTabletMessage = "";
                        }
                        playerController.currentTabletMessage += "ALERT: " + hit.collider.gameObject.name.Split('(')[0] + " destroyed by a meteor!\n";
                        playerController.destructionMessageCount += 1;
                    }
                    Explode();
                }
                else if (hit.collider.gameObject.tag.Equals("CombinedMesh"))
                {
                    if (hit.collider.gameObject.name.Equals("glassHolder(Clone)"))
                    {
                        int chanceOfDestruction = Random.Range(1, 101);
                        {
                            if (chanceOfDestruction > 25)
                            {
                                game.meshManager.SeparateBlocks(transform.position, "glass", false);
                                if (CanSendDestructionMessage())
                                {
                                    if (playerController.destructionMessageActive == false)
                                    {
                                        playerController.destructionMessageActive = true;
                                        playerController.currentTabletMessage = "";
                                    }
                                    playerController.currentTabletMessage += "ALERT: Some glass blocks were hit by a meteor!\n";
                                    playerController.destructionMessageCount += 1;
                                }
                            }
                        }
                        Explode();
                    }
                    else if (hit.collider.gameObject.name.Equals("brickHolder(Clone)"))
                    {
                        int chanceOfDestruction = Random.Range(1, 101);
                        {
                            if (chanceOfDestruction > 50)
                            {
                                game.meshManager.SeparateBlocks(transform.position, "brick", false);
                                if (CanSendDestructionMessage())
                                {
                                    if (playerController.destructionMessageActive == false)
                                    {
                                        playerController.destructionMessageActive = true;
                                        playerController.currentTabletMessage = "";
                                    }
                                    playerController.currentTabletMessage += "ALERT: Some bricks were hit by a meteor!\n";
                                    playerController.destructionMessageCount += 1;
                                }
                            }
                            Explode();
                        }
                    }
                    else if (hit.collider.gameObject.name.Equals("ironHolder(Clone)"))
                    {
                        int chanceOfDestruction = Random.Range(1, 101);
                        {
                            if (chanceOfDestruction > 75)
                            {
                                game.meshManager.SeparateBlocks(transform.position, "iron", false);
                                if (CanSendDestructionMessage())
                                {
                                    if (playerController.destructionMessageActive == false)
                                    {
                                        playerController.destructionMessageActive = true;
                                        playerController.currentTabletMessage = "";
                                    }
                                    playerController.currentTabletMessage += "ALERT: Some iron blocks were hit by a meteor!\n";
                                    playerController.destructionMessageCount += 1;
                                }
                            }
                            Explode();
                        }
                    }
                    else if (hit.collider.gameObject.name.Equals("steelHolder(Clone)"))
                    {
                        int chanceOfDestruction = Random.Range(1, 101);
                        {
                            if (chanceOfDestruction > 99)
                            {
                                game.meshManager.SeparateBlocks(transform.position, "steel", false);
                                if (CanSendDestructionMessage())
                                {
                                    if (playerController.destructionMessageActive == false)
                                    {
                                        playerController.destructionMessageActive = true;
                                        playerController.currentTabletMessage = "";
                                    }
                                    playerController.currentTabletMessage += "ALERT: Some steel blocks were hit by a meteor!\n";
                                    playerController.destructionMessageCount += 1;
                                }
                            }
                            Explode();
                        }
                    }
                }
                else
                {
                    Explode();
                }
            }
        }
        else
        {
            destroyTimer += 1 * Time.deltaTime;
            if (destroyTimer >= 30)
            {
                Destroy(gameObject);
            }
        }
    }

    // Destroys the meteor and spawns explosion effects
    public void Explode()
    {
        Instantiate(explosion, new Vector3(transform.position.x,transform.position.y+10,transform.position.z), transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        fire.SetActive(false);
        destroying = true;
    }
}