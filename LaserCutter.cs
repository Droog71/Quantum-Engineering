using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCutter : MonoBehaviour
{
    public GameObject gearCutter;

    // Update is called once per frame
    public void Update()
    {
        if (gearCutter.GetComponent<AudioSource>().enabled == true)
        {
            transform.Rotate(-Vector3.up * 600 * Time.deltaTime);
        }
    }
}
