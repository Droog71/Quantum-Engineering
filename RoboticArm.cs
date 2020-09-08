using UnityEngine;

public class RoboticArm : MonoBehaviour
{
    public GameObject retriever;
    public GameObject item;
    public Transform rotationTransform;
    Vector3 rotationPoint;
    bool started;
    bool soundPlayed;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rotationPoint = rotationTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (retriever.GetComponent<Retriever>().inputObject != null && retriever.GetComponent<Retriever>().outputObject != null)
        {
            started = true;
        }
        else
        {
            started = false;
            item.GetComponent<Renderer>().enabled = false;
        }
        if (retriever.GetComponent<Light>().enabled == true && started == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer < 1)
            {
                if (soundPlayed == false)
                {
                    //retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                item.GetComponent<Renderer>().enabled = true;
                transform.RotateAround(rotationPoint, Vector3.up, 150 * Time.deltaTime);
            }
            else if (timer >= 1 && timer < 3)
            {
                if (soundPlayed == true)
                {
                    retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                item.GetComponent<Renderer>().enabled = false;
                transform.RotateAround(rotationPoint, -Vector3.up, 150 * Time.deltaTime);
            }
            else if (timer >= 3 && timer < 4)
            {
                if (soundPlayed == false)
                {
                    retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                item.GetComponent<Renderer>().enabled = true;
                transform.RotateAround(rotationPoint, Vector3.up, 150 * Time.deltaTime);
            }
            else if (timer >= 4 && timer < 5)
            {
                if (soundPlayed == true)
                {
                    retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = false;
                }
                item.GetComponent<Renderer>().enabled = false;
                transform.RotateAround(rotationPoint, -Vector3.up, 150 * Time.deltaTime);
            }
            else if (timer >= 5 && timer < 6)
            {
                if (soundPlayed == false)
                {
                    retriever.GetComponent<AudioSource>().Play();
                    soundPlayed = true;
                }
                item.GetComponent<Renderer>().enabled = true;
                transform.RotateAround(rotationPoint, Vector3.up, 150 * Time.deltaTime);
            }
            else if (timer >= 6)
            {
                timer = 0;
                soundPlayed = false;
            }
        }
        else
        {
            item.GetComponent<Renderer>().enabled = false;
        }
    }
}
