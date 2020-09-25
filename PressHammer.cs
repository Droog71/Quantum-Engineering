using UnityEngine;

//! This class handles animation of the hammer and piston of a press.
public class PressHammer : MonoBehaviour
{
    public GameObject press;
    private StateManager stateManager;
    public float originalYposition;
    bool movingDown = true;
    bool movingUp;
    bool soundPlayed;

    //! Start is called before the first frame update.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        originalYposition = transform.position.y;
    }

    //! Update is called once per frame.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (press.GetComponent<Light>().enabled == true)
            {
                if (transform.position.y > originalYposition - 0.4f && movingDown == true)
                {
                    if (soundPlayed == false)
                    {
                        press.GetComponent<AudioSource>().Play();
                        soundPlayed = true;
                    }
                    transform.position -= transform.up * 0.3f * Time.deltaTime;
                }
                else
                {
                    movingUp = true;
                    movingDown = false;
                }
                if (transform.position.y <= originalYposition && movingUp == true)
                {
                    if (soundPlayed == true)
                    {
                        soundPlayed = false;
                    }
                    transform.position += transform.up * 0.3f * Time.deltaTime;
                }
                else
                {
                    movingUp = false;
                    movingDown = true;
                }
            }
        }
    }
}