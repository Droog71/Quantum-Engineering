using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCutter : MonoBehaviour
{
    public GameObject gearCutter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gearCutter.GetComponent<AudioSource>().enabled == true)
        {
            transform.Rotate(-Vector3.up * 600 * Time.deltaTime);
        }
    }
}
