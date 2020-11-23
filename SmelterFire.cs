using UnityEngine;

public class SmelterFire : MonoBehaviour
{
    public GameObject fireObject;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Update is called once per frame.
    public void Update()
    {
        if (GetComponent<Light>().enabled == true && !stateManager.Busy())
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                fireObject.SetActive(false);
                GetComponent<AudioSource>().Play();
                fireObject.SetActive(true);
            }
        }
        else
        {
            fireObject.SetActive(false);
        }
    }
}
