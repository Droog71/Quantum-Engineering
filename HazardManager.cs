using System.Collections;
using UnityEngine;

public class HazardManager
{
    private GameManager manager;

    public HazardManager(GameManager manager)
    {
        this.manager = manager;
    }

    public void UpdateHazards()
    {
        if (manager.hazardsEnabled == true)
        {
            if (manager.player.timeToDeliver == false && manager.rocketScript.gameTime < 2000)
            {
                // Pirate attacks
                if (manager.rocketScript.day >= 10 && manager.GetComponent<StateManager>().worldLoaded == true)
                {
                    manager.pirateAttackTimer += 1 * Time.deltaTime;
                    if (manager.loadedPirateTimer == true)
                    {
                        FileBasedPrefs.SetFloat(manager.GetComponent<StateManager>().WorldName + "pirateAttackTimer", manager.pirateAttackTimer);
                    }
                    if (manager.pirateAttackTimer >= 530 & manager.pirateAttackTimer < 540)
                    {
                        if (manager.player != null)
                        {
                            manager.player.pirateAttackWarningActive = true;
                        }
                    }
                    else if (manager.pirateAttackTimer >= 540 && manager.pirateAttackTimer < 600)
                    {
                        if (manager.player != null)
                        {
                            manager.player.pirateAttackWarningActive = false;
                        }
                        manager.pirateFrequency = 40 - manager.rocketScript.day;
                        if (manager.pirateFrequency < 2)
                        {
                            manager.pirateFrequency = 2;
                        }
                        manager.pirateTimer += 1 * Time.deltaTime;
                        if (manager.pirateTimer >= manager.pirateFrequency && manager.GetComponent<StateManager>().worldLoaded == true)
                        {
                            float x = Random.Range(-4500, 4500);
                            float z = Random.Range(-4500, 4500);
                            int RandomSpawn = Random.Range(1, 5);
                            if (RandomSpawn == 1)
                            {
                                Object.Instantiate(manager.pirateObject, new Vector3(x, 400, 10000), manager.transform.rotation);
                            }
                            if (RandomSpawn == 2)
                            {
                                GameObject pirate = Object.Instantiate(manager.pirateObject, new Vector3(x, 400, -10000), manager.transform.rotation);
                            }
                            if (RandomSpawn == 3)
                            {
                                GameObject pirate = Object.Instantiate(manager.pirateObject, new Vector3(10000, 400, z), manager.transform.rotation);
                            }
                            if (RandomSpawn == 4)
                            {
                                GameObject pirate = Object.Instantiate(manager.pirateObject, new Vector3(-10000, 400, z), manager.transform.rotation);
                            }
                            manager.pirateTimer = 0;
                        }
                    }
                    else if (manager.pirateAttackTimer >= 900)
                    {
                        manager.pirateAttackTimer = 0;
                        manager.player.destructionMessageActive = false;
                    }
                }

                // Meteor showers
                if (manager.GetComponent<StateManager>().worldLoaded)
                {
                    manager.meteorShowerTimer += 1 * Time.deltaTime;
                }

                if (manager.loadedMeteorTimer == true)
                {
                    FileBasedPrefs.SetFloat(manager.GetComponent<StateManager>().WorldName + "meteorShowerTimer", manager.meteorShowerTimer);
                }
                if (manager.meteorShowerTimer >= 530 && manager.meteorShowerTimer < 540)
                {
                    if (manager.player != null)
                    {
                        manager.player.meteorShowerWarningActive = true;
                    }
                }
                else if (manager.meteorShowerTimer >= 540 && manager.meteorShowerTimer < 600)
                {
                    bool locationFound = false;
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject go in allObjects)
                    {
                        if (locationFound == false)
                        {
                            if (!manager.meteorShowerLocationList.Contains(go.transform.position))
                            {
                                manager.meteorShowerLocation = go.transform.position;
                                manager.meteorShowerLocationList.Add(manager.meteorShowerLocation);
                                locationFound = true;
                            }
                        }
                    }
                    if (locationFound == false && manager.meteorShowerLocationList.Count > 0)
                    {
                        manager.meteorShowerLocationList.Clear();
                    }
                    manager.meteorTimer += 1 * Time.deltaTime;
                    if (manager.meteorTimer > 0.5f && manager.GetComponent<StateManager>().worldLoaded == true)
                    {
                        float x = Random.Range(manager.meteorShowerLocation.x - 500, manager.meteorShowerLocation.x + 500);
                        float z = Random.Range(manager.meteorShowerLocation.z - 500, manager.meteorShowerLocation.z + 500);
                        Object.Instantiate(manager.meteorObject, new Vector3(x, 500, z), manager.transform.rotation);
                        manager.meteorTimer = 0;
                    }
                    if (manager.player != null)
                    {
                        manager.player.meteorShowerWarningActive = false;
                    }
                }
                else if (manager.meteorShowerTimer >= 900)
                {
                    manager.meteorShowerTimer = 0;
                    manager.player.destructionMessageActive = false;
                }
            }
            else
            {
                manager.pirateAttackTimer = 0;
                manager.meteorShowerTimer = 120;
                if (manager.player != null)
                {
                    manager.player.destructionMessageActive = false;
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
        manager.pirateTimer = 0;
        manager.meteorTimer = 0;
        manager.pirateAttackTimer = 0;
        manager.meteorShowerTimer = 120;
        if (manager.loadedMeteorTimer == true)
        {
            FileBasedPrefs.SetFloat(manager.GetComponent<StateManager>().WorldName + "meteorShowerTimer", manager.meteorShowerTimer);
        }
        if (manager.loadedPirateTimer == true)
        {
            FileBasedPrefs.SetFloat(manager.GetComponent<StateManager>().WorldName + "pirateAttackTimer", manager.pirateAttackTimer);
        }
        if (manager.player != null)
        {
            manager.player.meteorShowerWarningActive = false;
            manager.player.pirateAttackWarningActive = false;
            manager.player.destructionMessageActive = false;
        }
        manager.hazardRemovalCoroutine = manager.StartCoroutine(HazardRemovalCoroutine());
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

