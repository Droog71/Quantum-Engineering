using UnityEngine;

public class SmelterFire : MonoBehaviour
{
    public GameObject fireObject;

    //! Update is called once per frame.
    public void Update()
    {
        if (GetComponent<Light>().enabled == true)
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                fireObject.SetActive(false);
                GetComponent<AudioSource>().Play();
                fireObject.SetActive(true);
            }
        }
    }
}
