using UnityEngine;

public class AugerBlade : MonoBehaviour
{
    public GameObject auger;

    // Called once per frame by unity engine
    public void Update()
    {
        if (auger.GetComponent<AudioSource>().enabled == true)
        {
            transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
        }
    } 
}
