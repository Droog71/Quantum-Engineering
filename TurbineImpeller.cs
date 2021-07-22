using UnityEngine;

//! This class handles animations for reactor turbines.
public class TurbineImpeller : MonoBehaviour
{
    public GameObject turbine;
    private StateManager stateManager;

    //! Update is called once per frame.
    void Update()
    {
        if (stateManager == null)
        {
            stateManager = turbine.GetComponent<PowerSource>().stateManager;
        }
        else if (!stateManager.Busy())
        {
            if (turbine.GetComponent<AudioSource>().isPlaying == true)
            {
                transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
            }
        }
    }
}
