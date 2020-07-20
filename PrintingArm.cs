using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingArm : MonoBehaviour
{
    public GameObject autoCrafter;
    public GameObject laser;
    public GameObject horizontalArm;
    public GameObject verticalArm;
    private Vector3 horizontalArmStartPosition;
    private Vector3 verticalArmStartPosition;
    bool started;
    bool soundPlayed;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        horizontalArmStartPosition = horizontalArm.transform.position;
        verticalArmStartPosition = verticalArm.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (autoCrafter.GetComponent<AutoCrafter>().inputObject != null)
        {
            started = true;
        }
        else
        {
            started = false;
        }
        if (autoCrafter.GetComponent<Light>().enabled == true && started == true)
        {
            laser.SetActive(true);
            timer += 1 * Time.deltaTime;
            if (timer < 1)
            {
                if (soundPlayed == false)
                {
                    //retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                horizontalArm.transform.position += horizontalArm.transform.right * 1 * Time.deltaTime;
            }
            else if (timer >= 1 && timer < 2)
            {
                if (soundPlayed == true)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                verticalArm.transform.position -= verticalArm.transform.forward * 1 * Time.deltaTime;
            }
            else if (timer >= 2 && timer < 3)
            {
                if (soundPlayed == false)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                horizontalArm.transform.position -= horizontalArm.transform.right * 1 * Time.deltaTime;
            }
            else if (timer >= 3 && timer < 4)
            {
                if (soundPlayed == true)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                verticalArm.transform.position += verticalArm.transform.forward * 1 * Time.deltaTime;
            }
            if (timer >= 4 && timer < 5)
            {
                if (soundPlayed == false)
                {
                    //retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                horizontalArm.transform.position -= horizontalArm.transform.right * 1 * Time.deltaTime;
            }
            else if (timer >= 5 && timer < 6)
            {
                if (soundPlayed == true)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                verticalArm.transform.position += verticalArm.transform.forward * 1 * Time.deltaTime;
            }
            else if (timer >= 6 && timer < 7)
            {
                if (soundPlayed == false)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                horizontalArm.transform.position += horizontalArm.transform.right * 1 * Time.deltaTime;
            }
            else if (timer >= 7 && timer < 8)
            {
                if (soundPlayed == true)
                {
                    GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                verticalArm.transform.position -= verticalArm.transform.forward * 1 * Time.deltaTime;
            }
            else if (timer >= 8)
            {
                timer = 0;
                soundPlayed = false;
                horizontalArm.transform.position = horizontalArmStartPosition;
                verticalArm.transform.position = verticalArmStartPosition;
            }
        }
        else
        {
            laser.SetActive(false);
        }
    }
}
