using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    //! Loads the default scene (Kepler-1625).
    public void ReturnToMainMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(0);
    }
}

