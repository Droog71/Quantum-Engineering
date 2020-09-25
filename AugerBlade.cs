using UnityEngine;

public class AugerBlade : MonoBehaviour
{
    public GameObject auger;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (auger.GetComponent<AudioSource>().enabled == true)
            {
                transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
            }
        }
    } 
}
