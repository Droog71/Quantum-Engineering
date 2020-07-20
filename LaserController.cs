using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserController : MonoBehaviour
{
    GameManager game;

    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void HitTarget(GameObject target,RaycastHit hit)
    {
        if (target.GetComponent<Meteor>() != null)
        {
            target.GetComponent<Meteor>().Explode();
        }
        if (target.GetComponent<Pirate>() != null)
        {
            target.GetComponent<Pirate>().TakeDamage();
        }
        if (target.tag.Equals("Built"))
        {
            if (target.GetComponent<PhysicsHandler>() != null)
            {
                target.GetComponent<PhysicsHandler>().Explode();
            }
            if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
            {
                if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                }
                GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: " + hit.collider.gameObject.name.Split('(')[0] + " destroyed by your laser cannon!\n";
                GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
            }
        }
        if (target.tag.Equals("CombinedMesh"))
        {
            if (target.name.Equals("glassHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 25)
                    {
                        game.SeparateBlocks(hit.point, "glass",false);
                        if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                            }
                            GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some glass blocks were hit by your laser cannon!\n";
                            GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
                        }
                    }
                }
            }
            else if (target.name.Equals("brickHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 50)
                    {
                        game.SeparateBlocks(hit.point, "brick",false);
                        if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                            }
                            GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some bricks were hit by your laser cannon!\n";
                            GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
                        }
                    }
                }
            }
            if (target.name.Equals("ironHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 75)
                    {
                        game.SeparateBlocks(hit.point, "iron",false);
                        if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                            }
                            GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some iron blocks were hit by your laser cannon!\n";
                            GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
                        }
                    }
                }
            }
            else if (target.name.Equals("steelHolder(Clone)"))
            {
                int chanceOfDestruction = Random.Range(1, 101);
                {
                    if (chanceOfDestruction > 99)
                    {
                        game.SeparateBlocks(hit.point, "steel",false);
                        if (GameObject.Find("Player").GetComponent<PlayerController>().timeToDeliver == false && GameObject.Find("Player").GetComponent<PlayerController>().meteorShowerWarningActive == false && GameObject.Find("Player").GetComponent<PlayerController>().pirateAttackWarningActive == false)
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive == false)
                            {
                                GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageActive = true;
                                GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage = "";
                            }
                            GameObject.Find("Player").GetComponent<PlayerController>().currentTabletMessage += "ALERT: Some steel blocks were hit by your laser cannon!\n";
                            GameObject.Find("Player").GetComponent<PlayerController>().destructionMessageCount += 1;
                        }
                    }
                }
            }
        }
    }
}
