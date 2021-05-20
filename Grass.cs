using UnityEngine;

public class Grass : MonoBehaviour
{
    //! Called oncer per frame by unity engine.
    public void Update()
    {
        if (QualitySettings.GetQualityLevel() < 3)
        {
            gameObject.SetActive(false);
        }
    }
}