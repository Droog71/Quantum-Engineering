using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugerBlade : MonoBehaviour
{
    public GameObject auger;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (auger.GetComponent<AudioSource>().enabled == true)
        {
            transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
        }
    } 
}
