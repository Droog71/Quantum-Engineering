using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float timer;

    //! Called once per frame by unity engine.
    public void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
