using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    float timer;
    public bool active;
    Quaternion originalRotation;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void Reset()
    {
        transform.localRotation = originalRotation;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
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
