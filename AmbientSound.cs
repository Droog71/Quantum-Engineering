using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    private StateManager stateManager;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        sound.enabled = stateManager.worldLoaded == true && !stateManager.Busy();
    }
}
