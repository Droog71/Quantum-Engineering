using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fire;
    GameManager game;
    public bool destroying;
    float destroyTimer;
    public float altitude;

    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
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
                    if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                    {
                        if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                        {
                            GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                            GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                        }
                        GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: " + hit.collider.gameObject.name.Split('(')[0] + " destroyed by a meteor!\n";
                        GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
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
                                game.SeparateBlocks(transform.position, "glass", false);
                                if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                                {
                                    if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                        GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                                    }
                                    GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some glass blocks were hit by a meteor!\n";
                                    GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
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
                                game.SeparateBlocks(transform.position, "brick", false);
                                if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                                {
                                    if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                        GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                                    }
                                    GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some bricks were hit by a meteor!\n";
                                    GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
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
                                game.SeparateBlocks(transform.position, "iron", false);
                                if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                                {
                                    if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                        GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                                    }
                                    GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some iron blocks were hit by a meteor!\n";
                                    GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
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
                                game.SeparateBlocks(transform.position, "steel", false);
                                if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                                {
                                    if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                                    {
                                        GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                        GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                                    }
                                    GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some steel blocks were hit by a meteor!\n";
                                    GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
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

    public void Explode()
    {
        Instantiate(explosion, new Vector3(transform.position.x,transform.position.y+10,transform.position.z), transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        fire.SetActive(false);
        destroying = true;
    }
}