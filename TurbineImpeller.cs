using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineImpeller : MonoBehaviour
{
    public GameObject turbine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turbine.GetComponent<AudioSource>().isPlaying == true)
        {
            transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
        }
    }
}
