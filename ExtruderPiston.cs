using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtruderPiston : MonoBehaviour
{
    public GameObject extruder;
    private Vector3 originalPosition;
    private Vector3 endPosition;
    private bool setEndPosition;
    private bool movingForward = true;
    private bool movingBack;
    private bool soundPlayed;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        originalPosition = transform.position;
    }

    // Called once per frame by unity engine
    public void Update()
    {
        if (extruder.GetComponent<Light>().enabled == true)
        {
            float startDistance = Vector3.Distance(transform.position, originalPosition);
            if (startDistance < 1.2f && movingForward == true)
            {
                if (soundPlayed == false)
                {
                    extruder.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                transform.position += transform.right * 1.1f * Time.deltaTime;
            }
            else
            {
                if (setEndPosition == false)
                {
                    endPosition = transform.position;
                    setEndPosition = true;
                }
                movingBack = true;
                movingForward = false;
            }

            float endDistance = Vector3.Distance(transform.position, endPosition);
            if (endDistance < 1.2f && movingBack == true)
            {
                if (soundPlayed == true)
                {
                    soundPlayed = false;
                }
                transform.position -= transform.right * 1.1f * Time.deltaTime;
            }
            else
            {
                setEndPosition = false;
                movingBack = false;
                movingForward = true;
            }
        }
    }
}
