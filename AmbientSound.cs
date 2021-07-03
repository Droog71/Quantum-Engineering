using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientSound : MonoBehaviour
{
    private StateManager stateManager;
    private TerrainGenerator terrainGenerator;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<StateManager>();
        terrainGenerator = GetComponent<TerrainGenerator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "QE_Procedural")
        {
            sound.enabled = terrainGenerator.initialized == true && !stateManager.Busy();
        }
        else
        {
            sound.enabled = stateManager.worldLoaded == true && !stateManager.Busy();
        }
    }
}
