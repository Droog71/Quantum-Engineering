using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    private GameManager game;
    private PlayerController playerController;
    private GameObject target;
    private List<Vector3> targetLocationList;
    private Vector3 targetLocation;
    private float fireTimer;
    private LineRenderer laser;
    public Material laserMat;
    public GameObject cannon;
    public GameObject explosion;
    public GameObject targetExplosion;
    public GameObject damageExplosion;
    public bool destroying;
    public GameObject model;
    private Quaternion originalRotation;
    private float lifeSpan = 300;
    private float destroyTimer;
    public float integrity = 100;

    //! Reduces integrity of the object and spawns damage effects.
    public void TakeDamage()
    {
        integrity -= 20;
        Instantiate(damageExplosion, transform.position, transform.rotation);
    }

    //! Destroys the object and spawns explosion effects.
    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        fireTimer = 0;
        laser.enabled = false;
        GetComponent<AudioSource>().enabled = false;
        GetComponent<Light>().enabled = false;
        model.GetComponent<MeshRenderer>().enabled = false;
        destroying = true;
    }

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        originalRotation = transform.rotation;
        targetLocationList = new List<Vector3>();
        laser = gameObject.AddComponent<LineRenderer>();
        laser.startWidth = 0.2f;
        laser.endWidth = 0.2f;
        laser.material = laserMat;
        laser.loop = true;
        laser.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        laser.enabled = false;
    }

    //! Returns true if a message can be sent to the player's tablet.
    private bool CanSendDestructionMessage()
    {
        return playerController.timeToDeliver == false && playerController.meteorShowerWarningActive == false && playerController.pirateAttackWarningActive == false;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        // Despawning
        lifeSpan -= 1 * Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        // Destroying
        if (destroying == true)
        {
            destroyTimer += 1 * Time.deltaTime;
            if (destroyTimer >= 30)
            {
                Destroy(gameObject);
            }
        }

        if (integrity <= 0 && destroying == false)
        {
            Explode();
        }

        if (target == null && destroying == false)
        {
            // Turn off any active weapon effects when there are no targets found.
            fireTimer = 0;
            laser.enabled = false;
            GetComponent<Light>().enabled = false;

            // Movement.
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit forwardHit, 1000))
            {
                transform.position += transform.up * 25 * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * 200 * Time.deltaTime;
            }
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit altitudeHit, 10000))
            {
                if (Vector3.Distance(transform.position, altitudeHit.point) > 500)
                {
                    transform.position -= transform.up * 25 * Time.deltaTime;
                }
                if (Vector3.Distance(transform.position, altitudeHit.point) < 100)
                {
                    transform.position += transform.up * 25 * Time.deltaTime;
                }
            }

            // Targeting.
            bool targetFound = false;
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
            foreach (GameObject obj in allObjects)
            {
                if (targetFound == false)
                {
                    if (!targetLocationList.Contains(obj.transform.position))
                    {
                        target = obj;
                        targetLocation = target.transform.position;
                        targetLocationList.Add(targetLocation);
                        targetFound = true;
                    }
                }
            }
            if (targetFound == false && targetLocationList.Count > 0)
            {
                targetLocationList.Clear();
            }
        }
        else if (destroying == false)
        {
            // Movement.
            Vector3 destination = targetLocation;
            destination.y = transform.position.y;
            if (Vector3.Distance(transform.position, targetLocation) > 2000)
            {
                transform.LookAt(destination);
            }
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit forwardHit, 1000))
            {
                transform.position += transform.up * 25 * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * 200 * Time.deltaTime;
            }
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit altitudeHit, 10000))
            {
                if (Vector3.Distance(transform.position, altitudeHit.point) > 500)
                {
                    transform.position -= transform.up * 25 * Time.deltaTime;
                }
                if (Vector3.Distance(transform.position, altitudeHit.point) < 100)
                {
                    transform.position += transform.up * 25 * Time.deltaTime;
                }
            }

            // Firing.
            if (Vector3.Distance(transform.position, target.transform.position) < 1000)
            {
                fireTimer += 1 * Time.deltaTime;
                if (fireTimer >= 1 && fireTimer < 1.1f)
                {
                    if (!cannon.GetComponent<AudioSource>().isPlaying)
                    {
                        cannon.GetComponent<AudioSource>().Play();
                        laser.enabled = true;
                        GetComponent<Light>().enabled = true;
                        laser.SetPosition(0, transform.position);
                        laser.SetPosition(1, target.transform.position);
                    }
                }
                if (fireTimer >= 1.1f)
                {
                    if (Physics.Linecast(transform.position, target.transform.position, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.tag.Equals("Built"))
                        {
                            int RandomDamage = Random.Range(1, 101);
                            if (RandomDamage > 75)
                            {
                                Instantiate(targetExplosion, hit.point, transform.rotation);
                                Destroy(hit.collider.gameObject);
                                if (CanSendDestructionMessage())
                                {
                                    if (playerController.destructionMessageActive == false)
                                    {
                                        playerController.destructionMessageActive = true;
                                        playerController.currentTabletMessage = "";
                                    }
                                    string objName = hit.collider.gameObject.name.Split('(')[0];
                                    playerController.currentTabletMessage += "ALERT: " + objName + " destroyed by hostile spacecraft!\n";
                                    playerController.destructionMessageCount += 1;
                                }
                            }
                            else
                            {
                                Instantiate(damageExplosion, hit.point, transform.rotation);
                            }
                        }
                        else if (hit.collider.gameObject.tag.Equals("CombinedMesh"))
                        {
                            if (hit.collider.gameObject.name.Equals("glassHolder(Clone)"))
                            {
                                int chanceOfDestruction = Random.Range(1, 101);
                                {
                                    if (chanceOfDestruction > 25)
                                    {
                                        Instantiate(targetExplosion, hit.point, transform.rotation);
                                        game.meshManager.SeparateBlocks(hit.point, "glass",false);
                                        if (CanSendDestructionMessage())
                                        {
                                            if (playerController.destructionMessageActive == false)
                                            {
                                                playerController.destructionMessageActive = true;
                                                playerController.currentTabletMessage = "";
                                            }
                                            playerController.currentTabletMessage += "ALERT: Some glass blocks were attacked by hostile spacecraft!\n";
                                            playerController.destructionMessageCount += 1;
                                        }
                                    }
                                }
                            }
                            else if (hit.collider.gameObject.name.Equals("brickHolder(Clone)"))
                            {
                                int chanceOfDestruction = Random.Range(1, 101);
                                {
                                    if (chanceOfDestruction > 50)
                                    {
                                        Instantiate(targetExplosion, hit.point, transform.rotation);
                                        game.meshManager.SeparateBlocks(hit.point, "brick",false);
                                        if (CanSendDestructionMessage())
                                        {
                                            if (playerController.destructionMessageActive == false)
                                            {
                                                playerController.destructionMessageActive = true;
                                                playerController.currentTabletMessage = "";
                                            }
                                            playerController.currentTabletMessage += "ALERT: Some bricks were attacked by hostile spacecraft!\n";
                                            playerController.destructionMessageCount += 1;
                                        }
                                    }
                                    else
                                    {
                                        Instantiate(damageExplosion, hit.point, transform.rotation);
                                    }
                                }
                            }
                            else if (hit.collider.gameObject.name.Equals("ironHolder(Clone)"))
                            {
                                int chanceOfDestruction = Random.Range(1, 101);
                                {
                                    if (chanceOfDestruction > 75)
                                    {
                                        Instantiate(targetExplosion, hit.point, transform.rotation);
                                        game.meshManager.SeparateBlocks(hit.point, "iron", false);
                                        if (CanSendDestructionMessage())
                                        {
                                            if (playerController.destructionMessageActive == false)
                                            {
                                                playerController.destructionMessageActive = true;
                                                playerController.currentTabletMessage = "";
                                            }
                                            playerController.currentTabletMessage += "ALERT: Some iron blocks were attacked by hostile spacecraft!\n";
                                            playerController.destructionMessageCount += 1;
                                        }
                                    }
                                    else
                                    {
                                        Instantiate(damageExplosion, hit.point, transform.rotation);
                                    }
                                }
                            }
                            else if (hit.collider.gameObject.name.Equals("steelHolder(Clone)"))
                            {
                                int chanceOfDestruction = Random.Range(1, 101);
                                {
                                    if (chanceOfDestruction > 99)
                                    {
                                        Instantiate(targetExplosion, hit.point, transform.rotation);
                                        game.meshManager.SeparateBlocks(hit.point, "steel", false);
                                        if (CanSendDestructionMessage())
                                        {
                                            if (playerController.destructionMessageActive == false)
                                            {
                                                playerController.destructionMessageActive = true;
                                                playerController.currentTabletMessage = "";
                                            }
                                            playerController.currentTabletMessage += "ALERT: Some steel blocks were attacked by hostile spacecraft!\n";
                                            playerController.destructionMessageCount += 1;
                                        }
                                    }
                                    else
                                    {
                                        Instantiate(damageExplosion, hit.point, transform.rotation);
                                    }
                                }
                            }
                        }
                    }
                    fireTimer = 0;
                    laser.enabled = false;
                    GetComponent<Light>().enabled = false;
                }
            }
            else
            {
                fireTimer = 0;
                laser.enabled = false;
                GetComponent<Light>().enabled = false;
            }
        }
    }
}