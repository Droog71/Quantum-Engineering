using UnityEngine;

public class Brick : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    public int address;
    private float updateTick;

    // Called once per frame by unity engine.
    public void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
        }
    }
}
