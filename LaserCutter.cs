using UnityEngine;

public class LaserCutter : MonoBehaviour
{
    public GameObject gearCutter;

    // Update is called once per frame
    public void Update()
    {
        if (gearCutter.GetComponent<Light>().enabled == true)
        {
            GetComponentInChildren<Renderer>().enabled = true;
            gearCutter.GetComponent<AudioSource>().enabled = true;
            transform.Rotate(-Vector3.up * 600 * Time.deltaTime);
        }
        else
        {
            gearCutter.GetComponent<AudioSource>().enabled = false;
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }
}
