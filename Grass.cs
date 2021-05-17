using UnityEngine;

public class Grass : MonoBehaviour
{
    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        if (QualitySettings.GetQualityLevel() < 3)
        {
            gameObject.SetActive(false);
        }
    }
}