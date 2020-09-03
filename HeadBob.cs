using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    float timer;
    public bool active;
    Quaternion originalRotation;
    public GameObject player;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        originalRotation = transform.localRotation;
    }

    // Resets the camera rotation
    public void Reset()
    {
        transform.localRotation = originalRotation;
        timer = 0;
    }

    // Called once per frame by unity engine
    public void Update()
    {
        if (active == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer < 0.25f)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed / 4);
            }
            else if (timer >= 0.25f && timer < 0.5f)
            {
                transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed / 4);
            }
            else if (timer >= 0.5f)
            {
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }
}
