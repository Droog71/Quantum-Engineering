﻿using UnityEngine;
using System.Collections.Generic;
using MEC;

public class MissileTurret : Machine
{
    public int speed = 1;
    public int power;
    public int heat;
    public string ammoType;
    public int ammoAmount;
    public bool hasHeatExchanger;
    public int cooling;
    public string inputID;
    public GameObject inputObject;
    private Quaternion restingRotation;
    public GameObject launcher;
    public GameObject muzzle;
    public GameObject missile;
    public GameObject powerObject;
    private GameObject[] targets;
    private bool foundTarget;
    private bool hasTarget;
    private bool firing;
    private int warmup;
    public PowerReceiver powerReceiver;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        restingRotation = launcher.transform.rotation;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        UpdatePowerReceiver();

        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (gameManager != null)
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

            if (gameManager.meteorShowerTimer >= 540 && gameManager.meteorShowerTimer < 900 || gameManager.pirateAttackTimer >= 540 && gameManager.pirateAttackTimer < 900 && GameObject.Find("Rocket").GetComponent<Rocket>().day >= 5)
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
                        Timing.RunCoroutine(Fire());
                    }
                }
            }
        }
    }

    //! Fires at all targets on the target list.
    private IEnumerator<float> Fire()
    {
        firing = true;
        hasTarget = false;
        bool launchMissile = false;

        if (ammoType == "Missile")
        {
            foreach (GameObject target in targets)
            {
                if (target != null && ammoAmount > 0)
                {
                    if (target.GetComponent<Meteor>() != null)
                    {
                        launchMissile |= target.GetComponent<Meteor>().destroying == false;
                    }
                    else if (target.GetComponent<Pirate>() != null)
                    {
                        launchMissile |= target.GetComponent<Pirate>().destroying == false;
                    }

                    if (launchMissile == true)
                    {
                        hasTarget = true;
                        launcher.GetComponent<AudioSource>().Play();
                        Vector3 aimPos = new Vector3(target.transform.position.x, launcher.transform.position.y, target.transform.position.z);
                        launcher.transform.LookAt(aimPos);
                        launcher.transform.forward = -launcher.transform.forward;
                        yield return Timing.WaitForSeconds(0.50f);
                        GameObject m = Instantiate(missile, new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, muzzle.transform.position.z), muzzle.transform.rotation);
                        m.GetComponent<Missile>().target = target;
                        ammoAmount -= 1;
                        GetComponent<Light>().enabled = true;
                        yield return Timing.WaitForSeconds(0.50f);
                        GetComponent<Light>().enabled = false;
                        launcher.transform.rotation = restingRotation;
                        float outputPenalty = 3 - (speed * 0.1f);
                        if (outputPenalty < 0)
                        {
                            outputPenalty = 0;
                        }
                        yield return Timing.WaitForSeconds(outputPenalty);
                    }
                }
            }
        }
        GetComponent<Light>().enabled = false;
        launcher.transform.rotation = restingRotation;
        firing = false;
        foundTarget = hasTarget;
    }

    //! Gets power values from power receiver.
    private void UpdatePowerReceiver()
    {
        if (logic == false)
        {
            powerReceiver.ID = ID;
            power = powerReceiver.power;
            powerON = powerReceiver.powerON;
            powerObject = powerReceiver.powerObject;
        }
    }
}