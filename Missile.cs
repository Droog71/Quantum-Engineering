using UnityEngine;

public class Missile : MonoBehaviour
{	
    public GameObject explosion;
	public AudioClip sound;
    public bool destroying;
    private float destroyTimer;
	private float lifeSpan = 0;
    private AudioSource audioSource;
	public GameObject target;
    public GameObject exhaust;
	
	//! Called by unity engine on start up to initialize variables.
	public void Start ()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.volume = 0.25f;
		audioSource.spatialBlend = 1.0f;
		audioSource.maxDistance = 250;
		audioSource.reverbZoneMix = 1.1f;
		audioSource.spread = 360;
		audioSource.clip = sound;
		audioSource.loop = true;
		audioSource.Play();
	}

	//! Called once per frame by unity engine.
	public void Update ()
	{
        if (destroying == false)
        {
            if (target != null)
            {
                transform.LookAt(target.transform);
            }

            lifeSpan += 1 * Time.deltaTime;
            transform.position += transform.forward * 250 * Time.deltaTime;

            if (Vector3.Distance(transform.position,target.transform.position) < 10)
            {
                Collide();
            }

            if (lifeSpan > 10)
            {
                Explode();
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

    //! Simulates collision of the missile and the target.
	private void Collide()
	{
        Meteor hitMeteor = target.gameObject.GetComponent<Meteor>();
        Pirate hitPirate = target.gameObject.GetComponent<Pirate>();

        if (hitMeteor != null)
        {
            if (hitMeteor.destroying == false)
            {
                hitMeteor.GetComponent<Meteor>().Explode();
            }
            Explode();
        }

        if (hitPirate != null)
        {
            if (hitPirate.destroying == false)
            {
                hitPirate.GetComponent<Pirate>().Explode();
            }
            Explode();
        }
	}

    //! Spawns explosion effect and disables rendering of the misile.
    private void Explode()
    {
        audioSource.Stop();
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.rotation);
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshes)
        {
            meshRenderer.enabled = false;
        }
        exhaust.SetActive(false);
        GetComponent<Collider>().enabled = false;
        destroying = true;
    }
}