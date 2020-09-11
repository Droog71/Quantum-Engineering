using UnityEngine;

public class SmelterFire : MonoBehaviour
{
    public GameObject fireObject;
    private float timer;

    // Update is called once per frame
    public void Update()
    {
        if (GetComponent<Light>().enabled == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= 1.5f)
            {
                timer = 0;
                if (fireObject.activeInHierarchy)
                {
                    fireObject.SetActive(false);
                }
                else
                {
                    GetComponent<AudioSource>().Play();
                    fireObject.SetActive(true);
                }
            }
        }
    }
}
