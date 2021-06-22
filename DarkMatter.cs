using UnityEngine;
using UnityEngine.SceneManagement;

public class DarkMatter: MonoBehaviour
{
    private float size;
    public GameObject collector;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine
    public void Update()
    {
        if (!stateManager.Busy() && SceneManager.GetActiveScene().name != "QE_Procedural")
        {
            if (size < 10)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);
                size += 1;
            }
            else if (size >= 10 && size < 20)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 5, 5), Time.deltaTime * 0.5f);
                size += 1;
            }
            else if (size >= 20)
            {
                size = 0;
            }
        }
    }
}