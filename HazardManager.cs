using System.Collections;
using UnityEngine;

public class HazardManager
{
    private GameManager gameManager;

    public HazardManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void UpdateHazards()
    {
        if (gameManager.hazardsEnabled == true)
        {
            if (gameManager.player.timeToDeliver == false && gameManager.rocketScript.gameTime < 2000)
            {
                // Pirate attacks
                if (gameManager.rocketScript.day >= 10 && gameManager.GetComponent<StateManager>().worldLoaded == true)
                {
                    gameManager.pirateAttackTimer += 1 * Time.deltaTime;
                    if (gameManager.loadedPirateTimer == true)
                    {
                        FileBasedPrefs.SetFloat(gameManager.GetComponent<StateManager>().WorldName + "pirateAttackTimer", gameManager.pirateAttackTimer);
                    }
                    if (gameManager.pirateAttackTimer >= 530 & gameManager.pirateAttackTimer < 540)
                    {
                        if (gameManager.player != null)
                        {
                            gameManager.player.pirateAttackWarningActive = true;
                        }
                    }
                    else if (gameManager.pirateAttackTimer >= 540 && gameManager.pirateAttackTimer < 600)
                    {
                        if (gameManager.player != null)
                        {
                            gameManager.player.pirateAttackWarningActive = false;
                        }
                        gameManager.pirateFrequency = 40 - gameManager.rocketScript.day;
                        if (gameManager.pirateFrequency < 2)
                        {
                            gameManager.pirateFrequency = 2;
                        }
                        gameManager.pirateTimer += 1 * Time.deltaTime;
                        if (gameManager.pirateTimer >= gameManager.pirateFrequency && gameManager.GetComponent<StateManager>().worldLoaded == true)
                        {
                            float x = Random.Range(-4500, 4500);
                            float z = Random.Range(-4500, 4500);
                            int RandomSpawn = Random.Range(1, 5);
                            if (RandomSpawn == 1)
                            {
                                Object.Instantiate(gameManager.pirateObject, new Vector3(x, 400, 10000), gameManager.transform.rotation);
                            }
                            if (RandomSpawn == 2)
                            {
                                GameObject pirate = Object.Instantiate(gameManager.pirateObject, new Vector3(x, 400, -10000), gameManager.transform.rotation);
                            }
                            if (RandomSpawn == 3)
                            {
                                GameObject pirate = Object.Instantiate(gameManager.pirateObject, new Vector3(10000, 400, z), gameManager.transform.rotation);
                            }
                            if (RandomSpawn == 4)
                            {
                                GameObject pirate = Object.Instantiate(gameManager.pirateObject, new Vector3(-10000, 400, z), gameManager.transform.rotation);
                            }
                            gameManager.pirateTimer = 0;
                        }
                    }
                    else if (gameManager.pirateAttackTimer >= 900)
                    {
                        gameManager.pirateAttackTimer = 0;
                        gameManager.player.destructionMessageActive = false;
                    }
                }

                // Meteor showers
                if (gameManager.GetComponent<StateManager>().worldLoaded)
                {
                    gameManager.meteorShowerTimer += 1 * Time.deltaTime;
                }

                if (gameManager.loadedMeteorTimer == true)
                {
                    FileBasedPrefs.SetFloat(gameManager.GetComponent<StateManager>().WorldName + "meteorShowerTimer", gameManager.meteorShowerTimer);
                }
                if (gameManager.meteorShowerTimer >= 530 && gameManager.meteorShowerTimer < 540)
                {
                    if (gameManager.player != null)
                    {
                        gameManager.player.meteorShowerWarningActive = true;
                    }
                }
                else if (gameManager.meteorShowerTimer >= 540 && gameManager.meteorShowerTimer < 600)
                {
                    bool locationFound = false;
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject go in allObjects)
                    {
                        if (locationFound == false)
                        {
                            if (!gameManager.meteorShowerLocationList.Contains(go.transform.position))
                            {
                                gameManager.meteorShowerLocation = go.transform.position;
                                gameManager.meteorShowerLocationList.Add(gameManager.meteorShowerLocation);
                                locationFound = true;
                            }
                        }
                    }
                    if (locationFound == false && gameManager.meteorShowerLocationList.Count > 0)
                    {
                        gameManager.meteorShowerLocationList.Clear();
                    }
                    gameManager.meteorTimer += 1 * Time.deltaTime;
                    if (gameManager.meteorTimer > 0.5f && gameManager.GetComponent<StateManager>().worldLoaded == true)
                    {
                        float x = Random.Range(gameManager.meteorShowerLocation.x - 500, gameManager.meteorShowerLocation.x + 500);
                        float z = Random.Range(gameManager.meteorShowerLocation.z - 500, gameManager.meteorShowerLocation.z + 500);
                        Object.Instantiate(gameManager.meteorObject, new Vector3(x, 500, z), gameManager.transform.rotation);
                        gameManager.meteorTimer = 0;
                    }
                    if (gameManager.player != null)
                    {
                        gameManager.player.meteorShowerWarningActive = false;
                    }
                }
                else if (gameManager.meteorShowerTimer >= 900)
                {
                    gameManager.meteorShowerTimer = 0;
                    gameManager.player.destructionMessageActive = false;
                }
            }
            else
            {
                gameManager.pirateAttackTimer = 0;
                gameManager.meteorShowerTimer = 120;
                if (gameManager.player != null)
                {
                    gameManager.player.destructionMessageActive = false;
                }
            }
        }
        else
        {
            StopHazards();
        }
    }

    // Removes all hazards from the world
    private void StopHazards()
    {
        gameManager.pirateTimer = 0;
        gameManager.meteorTimer = 0;
        gameManager.pirateAttackTimer = 0;
        gameManager.meteorShowerTimer = 120;
        if (gameManager.loadedMeteorTimer == true)
        {
            FileBasedPrefs.SetFloat(gameManager.GetComponent<StateManager>().WorldName + "meteorShowerTimer", gameManager.meteorShowerTimer);
        }
        if (gameManager.loadedPirateTimer == true)
        {
            FileBasedPrefs.SetFloat(gameManager.GetComponent<StateManager>().WorldName + "pirateAttackTimer", gameManager.pirateAttackTimer);
        }
        if (gameManager.player != null)
        {
            gameManager.player.meteorShowerWarningActive = false;
            gameManager.player.pirateAttackWarningActive = false;
            gameManager.player.destructionMessageActive = false;
        }
        gameManager.hazardRemovalCoroutine = gameManager.StartCoroutine(HazardRemovalCoroutine());
    }

    // Removes all hazards from the world
    private IEnumerator HazardRemovalCoroutine()
    {
        Meteor[] allMeteors = Object.FindObjectsOfType<Meteor>();
        foreach (Meteor meteor in allMeteors)
        {
            if (meteor.destroying == false)
            {
                meteor.Explode();
            }
            yield return new WaitForSeconds(0.1f);
        }
        Pirate[] allPirates = Object.FindObjectsOfType<Pirate>();
        foreach (Pirate pirate in allPirates)
        {
            if (pirate.destroying == false)
            {
                pirate.Explode();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

