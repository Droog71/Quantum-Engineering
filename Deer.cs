using UnityEngine;
using System.Collections.Generic;
using MEC;

public class Deer : MonoBehaviour
{
    private GameObject player;
    private StateManager stateManager;
    public AnimationClip[] clips;
    private float direction = 0.1f;
    private int thinkTimer;
    private bool coroutineBusy;
    private bool running = true;
    private bool startled;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        player = GameObject.Find("Player");
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        GetComponent<Animation>()["Run"].speed = 0.75f;
        GetComponent<Animation>()["Idle"].speed = 0.25f;
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (coroutineBusy == false && stateManager.worldLoaded == true && !stateManager.Busy())
        {
            Timing.RunCoroutine(Think());
        }
    }

    //! Controls movement, animation and player detection.
    public IEnumerator<float> Think()
    {
        coroutineBusy = true;

        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < 40)
        {
            startled = true;
        }

        NetworkPlayer[] networkPlayers = FindObjectsOfType<NetworkPlayer>();
        foreach (NetworkPlayer netWorkPlayer in networkPlayers)
        {
            float networkPlayerDistance = Vector3.Distance(netWorkPlayer.transform.position, transform.position);
            if (networkPlayerDistance < 40)
            {
                startled = true;
                break;
            }
        }

        if (startled == true)
        {
            direction = Random.Range(0, 1) > 0.5f ? 0.1f : -0.1f;
            GetComponent<Animation>().Play("Run");
            thinkTimer = 0;
            running = true;
            yield return Timing.WaitForSeconds(10);
            startled = false;
        }
        else
        {
            yield return Timing.WaitForSeconds(0.25f);
            thinkTimer++;
            if (thinkTimer > Random.Range(20, 60))
            {
                GetComponent<Animation>().Play("Idle");
                if (running == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.right * 12000);
                    running = false;
                }
            }
            if (thinkTimer > Random.Range(60, 120))
            {
                direction = Random.Range(0, 1) > 0.5f ? 0.1f : -0.1f;
                GetComponent<Animation>().Play("Run");
                running = true;
                thinkTimer = 0;
            }
        }

        EnforceWorldLimits();
        coroutineBusy = false;
    }

    //! Frame-rate independent physics calculations.
    public void FixedUpdate()
    {
        if (running == true)
        {
            GetComponent<Rigidbody>().AddForce(-transform.right * 6000);
            transform.Rotate(Vector3.up * direction);
        }
    }

    //! For avoiding obstacles.
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Landscape")
        {
            direction = Random.Range(0, 1) > 0.5f ? 1 : -1;
        }
    }

    //! Enforces world size limitations.
    private void EnforceWorldLimits()
    {
        if (gameObject.transform.position.x > 4500)
        {
            gameObject.transform.position = new Vector3(4500, gameObject.transform.position.y, gameObject.transform.position.z);
            direction = Random.Range(0, 1) > 0.5f ? 1 : -1;
        }
        if (gameObject.transform.position.z > 4500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4500);
            direction = Random.Range(0, 1) > 0.5f ? 1 : -1;
        }
        if (gameObject.transform.position.x < -4500)
        {
            gameObject.transform.position = new Vector3(-4500, gameObject.transform.position.y, gameObject.transform.position.z);
            direction = Random.Range(0, 1) > 0.5f ? 1 : -1;
        }
        if (gameObject.transform.position.z < -4500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4500);
            direction = Random.Range(0, 1) > 0.5f ? 1 : -1;
        }
        if (gameObject.transform.position.y > 500)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 500, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.y < -100)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 500, gameObject.transform.position.z);
        }
    }
}