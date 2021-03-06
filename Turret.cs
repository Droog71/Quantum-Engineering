﻿using UnityEngine;
using System.Collections;

public class Turret : Machine
{
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public string ID = "unassigned";
    public string creationMethod = "built";
    public bool powerON;
    public Material laserMat;
    private Quaternion restingRotation;
    public GameObject barrel;
    public GameObject muzzle;
    public GameObject powerObject;
    private GameObject[] targets;
    private bool foundTarget;
    private bool hasTarget;
    private Coroutine fireCoroutine;
    private bool firing;
    private int warmup;
    private GameManager game;
    private StateManager stateManager;
    private LineRenderer laser;
    public PowerReceiver powerReceiver;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        laser = gameObject.AddComponent<LineRenderer>();
        stateManager = FindObjectOfType<StateManager>();
        laser.startWidth = 0.2f;
        laser.endWidth = 0.2f;
        laser.material = laserMat;
        laser.loop = true;
        laser.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        laser.enabled = false;
        restingRotation = barrel.transform.rotation;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        GetComponent<PhysicsHandler>().UpdatePhysics();
        UpdatePowerReceiver();

        if (game == null)
        {
            game = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (game != null)
        {
            if (warmup < 10)
            {
                warmup++;
            }
            else if (speed > power)
            {
                speed = power > 0 ? power : 1;
            }

            if (speed > 1)
            {
                heat = speed - 1 - cooling;
            }
            else
            {
                heat = 0;
            }

            if (heat < 0)
            {
                heat = 0;
            }

            if (HazardsPresent() == true)
            {
                if (foundTarget == false)
                {
                    targets = new GameObject[speed - heat];
                    int count = 0;
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Entity");
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj.activeInHierarchy)
                        {
                            float distance = Vector3.Distance(transform.position, obj.transform.position);
                            if (distance < 1000)
                            {
                                if (obj.GetComponent<Meteor>() != null)
                                {
                                    if (count < speed - heat && obj.GetComponent<Meteor>().destroying == false && obj.GetComponent<Meteor>().altitude > 200)
                                    {
                                        foundTarget = true;
                                        targets[count] = obj;
                                        count++;
                                    }
                                }
                                else if (obj.GetComponent<Pirate>() != null)
                                {
                                    if (count < speed - heat && obj.GetComponent<Pirate>().destroying == false)
                                    {
                                        foundTarget = true;
                                        targets[count] = obj;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (powerON == true && firing == false && speed > 0)
                    {
                        fireCoroutine = StartCoroutine(Fire());
                    }
                }
            }
        }
    }

    //! Fires at all targets on the target list.
    private IEnumerator Fire()
    {
        firing = true;
        hasTarget = false;
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                if (target.GetComponent<Meteor>() != null)
                {
                    if (target.GetComponent<Meteor>().destroying == false)
                    {
                        hasTarget = true;
                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            barrel.GetComponent<AudioSource>().Play();
                            float angle = Vector3.Angle(transform.forward, target.transform.position);
                            if (angle > 90)
                            {
                                barrel.transform.Rotate(transform.up, 180);
                            }
                            barrel.transform.LookAt(target.transform.position);
                            if (barrel.transform.rotation.x < -45)
                            {
                                barrel.transform.rotation = Quaternion.Euler(-45, barrel.transform.rotation.y, barrel.transform.rotation.z);
                            }
                            yield return new WaitForSeconds(0.45f);
                            GetComponent<AudioSource>().Play();
                            laser.enabled = true;
                            GetComponent<Light>().enabled = true;
                            laser.SetPosition(0, muzzle.transform.position);
                            laser.SetPosition(1, target.transform.position);
                            yield return new WaitForSeconds(0.10f);
                            if (Physics.Linecast(transform.position, target.transform.position, out RaycastHit hit))
                            {
                                if (hit.collider.gameObject == target)
                                {
                                    target.GetComponent<Meteor>().Explode();
                                }
                            }
                            laser.enabled = false;
                            GetComponent<Light>().enabled = false;
                            yield return new WaitForSeconds(0.45f);
                            barrel.transform.rotation = restingRotation;
                            float outputPenalty = 3 - (speed * 0.1f);
                            if (outputPenalty < 0)
                            {
                                outputPenalty = 0;
                            }
                            yield return new WaitForSeconds(outputPenalty);
                        }
                    }
                }
                else if (target.GetComponent<Pirate>() != null)
                {
                    if (target.GetComponent<Pirate>().destroying == false)
                    {
                        hasTarget = true;
                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            barrel.GetComponent<AudioSource>().Play();
                            float angle = Vector3.Angle(transform.forward, target.transform.position);
                            if (angle > 90)
                            {
                                barrel.transform.Rotate(transform.up, 180);
                            }
                            barrel.transform.LookAt(target.transform.position);
                            if (barrel.transform.rotation.x < -45)
                            {
                                barrel.transform.rotation = Quaternion.Euler(-45, barrel.transform.rotation.y, barrel.transform.rotation.z);
                            }
                            yield return new WaitForSeconds(0.45f);
                            GetComponent<AudioSource>().Play();
                            laser.enabled = true;
                            GetComponent<Light>().enabled = true;
                            laser.SetPosition(0, muzzle.transform.position);
                            laser.SetPosition(1, target.transform.position);
                            yield return new WaitForSeconds(0.10f);
                            if (Physics.Linecast(transform.position, target.transform.position, out RaycastHit hit))
                            {
                                if (hit.collider.gameObject == target)
                                {
                                    target.GetComponent<Pirate>().TakeDamage();
                                }
                            }
                            laser.enabled = false;
                            GetComponent<Light>().enabled = false;
                            yield return new WaitForSeconds(0.45f);
                            barrel.transform.rotation = restingRotation;
                            float outputPenalty = 3 - (speed * 0.1f);
                            if (outputPenalty < 0)
                            {
                                outputPenalty = 0;
                            }
                            yield return new WaitForSeconds(outputPenalty);
                        }
                    }
                }
            }
        }
        laser.enabled = false;
        GetComponent<Light>().enabled = false;
        barrel.transform.rotation = restingRotation;
        firing = false;
        foundTarget = hasTarget;
    }

    //! Returns true during a meteor shower or pirate attack.
    private bool HazardsPresent()
    {
        return game.meteorShowerTimer >= 540 &&
        game.meteorShowerTimer < 900 ||
        game.pirateAttackTimer >= 540 &&
        game.pirateAttackTimer < 900 &&
        GameObject.Find("Rocket").GetComponent<Rocket>().day >= 5;
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
    }
}