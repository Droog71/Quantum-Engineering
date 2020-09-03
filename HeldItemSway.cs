using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemSway : MonoBehaviour
{
    float timer;
    public bool active;
    public string type;
    Quaternion originalRotation;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
    }

    // Resets the object's rotation
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
                if (type.Equals("gun"))
                {
                    transform.RotateAround(transform.position, -transform.right, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/4);
                }
                else if (type.Equals("scanner"))
                {
                    transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/2);
                }
                else
                {
                    transform.RotateAround(transform.position, transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/2);
                }
            }
            else if (timer >= 0.25f && timer < 0.5f)
            {
                if (type.Equals("gun"))
                {
                    transform.RotateAround(transform.position, transform.right, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/4);
                }
                else if (type.Equals("scanner"))
                {
                    transform.RotateAround(transform.position, transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/2);
                }
                else
                {
                    transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * player.GetComponent<PlayerController>().playerMoveSpeed/2);
                }
            }
            else if (timer >= 0.5f)
            {
                transform.localRotation = originalRotation;
                timer = 0;
            }
        }
        else
        {
            transform.localRotation = originalRotation;
            timer = 0;
        }
    }
}
