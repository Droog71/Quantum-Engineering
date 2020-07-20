using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressHammer : MonoBehaviour
{
    public GameObject press;
    public float originalYposition;
    bool movingDown = true;
    bool movingUp;
    bool soundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        originalYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (press.GetComponent<Light>().enabled == true)
        {
            if (transform.position.y > originalYposition - 0.4f && movingDown == true)
            {
                if (soundPlayed == false)
                {
                    press.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                transform.position -= transform.up * 0.3f * Time.deltaTime;
            }
            else
            {
                movingUp = true;
                movingDown = false;
            }
            if (transform.position.y <= originalYposition && movingUp == true)
            {  
                if (soundPlayed == true)
                {
                    soundPlayed = false;
                }
                transform.position += transform.up * 0.3f * Time.deltaTime;
            }
            else
            {
                movingUp = false;
                movingDown = true;
            }
        }
    }
}
