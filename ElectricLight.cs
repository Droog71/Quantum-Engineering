using UnityEngine;
using System.Collections;

public class ElectricLight : MonoBehaviour
{
    public string ID = "unassigned";
    public string creationMethod;
    private float updateTick;
    public int address;
    public bool powerON;
    public GameObject powerObject;

    void Start()
    {

    }

    void OnDestroy()
    {

    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 1 + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;
            if (powerON == true)
            {
                GetComponent<Light>().enabled = true;
            }
            else
            {
                GetComponent<Light>().enabled = false;
            }
        }
    }
}