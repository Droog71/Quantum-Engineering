using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public bool moving;
    public bool onGround;
    public GameObject idle;
    public GameObject run;
    private float idleTimer;

    public void Start()
    {
    }

    public void Update()
    {
        onGround |= Physics.Raycast(transform.position, -transform.up, 12);

        if (moving == true && onGround == true)
        {
            idle.SetActive(false);
            run.SetActive(true);
        }
        else
        {
            idleTimer += 1 * Time.deltaTime;
            if (idleTimer >= 2)
            {
                idle.SetActive(true);
                run.SetActive(false);
                idleTimer = 0;
            }
        }
    }
}

