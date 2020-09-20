using UnityEngine;

//! This class handles animation of the gear cutter's laser.
public class LaserCutter : MonoBehaviour
{
    public GameObject gearCutter;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Update is called once per frame.
    public void Update()
    {
        if (!stateManager.Busy())
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
}
