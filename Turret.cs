using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    public int speed = 1;
    public int power;
    public int heat;
    public bool hasHeatExchanger;
    public int cooling;
    public string ID = "unassigned";
    public string creationMethod = "built";
    private float updateTick;
    public int address;
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
    private GameManager game;
    private LineRenderer laser;
    public PowerReceiver powerReceiver;

    void Start()
    {
        powerReceiver = gameObject.AddComponent<PowerReceiver>();
        laser = gameObject.AddComponent<LineRenderer>();
        laser.startWidth = 0.2f;
        laser.endWidth = 0.2f;
        laser.material = laserMat;
        laser.loop = true;
        laser.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        laser.enabled = false;
        restingRotation = barrel.transform.rotation;
    }

    private void UpdatePowerReceiver()
    {
        powerReceiver.ID = ID;
        power = powerReceiver.power;
        powerON = powerReceiver.powerON;
        powerObject = powerReceiver.powerObject;
        if (powerReceiver.overClocked == true)
        {
            speed = powerReceiver.speed;
        }
        else
        {
            powerReceiver.speed = speed;
        }
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            UpdatePowerReceiver();

            updateTick = 0;
            if (game == null)
            {
                game = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            if (game != null)
            {
                //Debug.Log(ID + " game manager is not null");
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
                if (game.meteorShowerTimer >= 540 && game.meteorShowerTimer < 900 || game.pirateAttackTimer >= 540 && game.pirateAttackTimer < 900 && GameObject.Find("Rocket").GetComponent<Rocket>().day >= 5)
                {
                    //Debug.Log(ID + " attack timer active");
                    if (foundTarget == false)
                    {
                        //Debug.Log(ID+" searching for target.");
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
                                            //Debug.Log(ID + " found pirate target");
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
                        //Debug.Log(ID + " update: " + " power: " + powerON + " firing: " + firing);
                        if (powerON == true && firing == false && speed > 0)
                        {
                            //Debug.Log(ID + " calling fire coroutine");
                            fireCoroutine = StartCoroutine(Fire());
                        }
                    }
                }
            }
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        hasTarget = false;
        //Debug.Log(ID+ " Fire coroutine");
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                //Debug.Log(ID + "Target is not null");
                if (target.GetComponent<Meteor>() != null)
                {
                    if (target.GetComponent<Meteor>().destroying == false)
                    {
                        //Debug.Log(ID + "Target is a meteor");
                        hasTarget = true;
                        if (!GetComponent<AudioSource>().isPlaying)
                        {

                            //Debug.Log(ID + " firing at meteor");
                            barrel.GetComponent<AudioSource>().Play();
                            float angle = Vector3.Angle(transform.forward, target.transform.position);
                            if (angle > 90)
                            {
                                barrel.transform.Rotate(transform.up, 180);
                            }
                            barrel.transform.LookAt(target.transform.position);
                            yield return new WaitForSeconds(0.10f);
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
                    //Debug.Log(ID + "Target is a pirate");
                    if (target.GetComponent<Pirate>().destroying == false)
                    {
                        //Debug.Log(ID + " Target is alive");
                        hasTarget = true;
                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            //Debug.Log("Turret firing.");
                            barrel.GetComponent<AudioSource>().Play();
                            float angle = Vector3.Angle(transform.forward, target.transform.position);
                            if (angle > 90)
                            {
                                barrel.transform.Rotate(transform.up, 180);
                            }
                            barrel.transform.LookAt(target.transform.position);
                            yield return new WaitForSeconds(0.10f);
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
        if (hasTarget == false)
        {
            foundTarget = false;
        }
        //Debug.Log(ID + "Has target: " + hasTarget);
        //Debug.Log(ID + "Firing: " + firing);
        //Debug.Log(ID + "Found Target: " + foundTarget);
    }
}