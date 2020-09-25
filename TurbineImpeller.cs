using UnityEngine;

//! This class handles animations for reactor turbines.
public class TurbineImpeller : MonoBehaviour
{
    public GameObject turbine;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Update is called once per frame.
    void Update()
    {
        if (!stateManager.Busy())
        {
            if (turbine.GetComponent<AudioSource>().isPlaying == true)
            {
                transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
            }
        }
    }
}
