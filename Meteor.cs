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


    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //! Returns true if a message can be sent to the player's tablet.
    private bool CanSendDestructionMessage()
    {
        return playerController.timeToDeliver == false && playerController.meteorShowerWarningActive == false && playerController.pirateAttackWarningActive == false;
    }

    //! Called once per frame by unity engine.
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
                GameObject obj = hit.collider.gameObject;
                if (obj.tag.Equals("Built") || obj.tag.Equals("Machine"))
                {
                    string objName;
                    if (obj.GetComponent<Block>() != null)
                    {
                        objName = obj.GetComponent<Block>().blockName;
                    }
                    else
                    {
                        objName = obj.name.Split('(')[0];
                    }

                    if (obj.GetComponent<Block>() != null)
                    {
                        obj.GetComponent<Block>().Explode();
                    }
                    else if (obj.GetComponent<Machine>() != null)
                    {
                        obj.GetComponent<Machine>().Explode();
                    }

                    if (CanSendDestructionMessage())
                    {
                        if (playerController.destructionMessageActive == false)
                        {
                            playerController.destructionMessageActive = true;
                            playerController.currentTabletMessage = "";
                        }
                        playerController.currentTabletMessage += "ALERT: " + objName + " destroyed by a meteor!\n";
                        playerController.destructionMessageCount += 1;
                    }
                    Explode();
                }
                else if (obj.tag.Equals("CombinedMesh") || obj.transform.parent.tag.Equals("CombinedMesh"))
                {
                    BlockHolder blockHolder = obj.GetComponent<BlockHolder>();

                    if (blockHolder == null)
                    {
                        blockHolder = obj.transform.parent.GetComponent<BlockHolder>();
                    }

                    if (blockHolder != null)
                    {
                        int toughness = 75;

                        if (blockHolder.blockType.ToUpper().Contains("GLASS"))
                        {
                            toughness = 25;
                        }
                        else if (blockHolder.blockType.ToUpper().Contains("STEEL"))
                        {
                            toughness = 99;
                        }

                        int chanceOfDestruction = Random.Range(1, 101);
                        {
                            if (chanceOfDestruction > toughness)
                            {
                                game.meshManager.RemoveBlock(hit.collider.gameObject.GetComponent<BlockHolder>(), hit.point, true);
                                if (CanSendDestructionMessage())
                                {
                                    if (blockHolder.blockType != "Dirt" && blockHolder.blockType != "Grass")
                                    {
                                        if (playerController.destructionMessageActive == false)
                                        {
                                            playerController.destructionMessageActive = true;
                                            playerController.currentTabletMessage = "";
                                        }
                                        playerController.currentTabletMessage += "ALERT: " + blockHolder.blockType + " hit by a meteor!\n";
                                        playerController.destructionMessageCount += 1;
                                    }
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

    //! Destroys the meteor and spawns explosion effects.
    public void Explode()
    {
        Instantiate(explosion, new Vector3(transform.position.x,transform.position.y+10,transform.position.z), transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        fire.SetActive(false);
        destroying = true;
    }
}