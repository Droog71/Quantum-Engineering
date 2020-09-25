using UnityEngine;

//! This class is attached to any machine that requires power to operate.
public class PowerReceiver : MonoBehaviour
{
    public string ID = "unassigned";
    public int power;
    public int speed = 1;
    public bool powerON;
    public GameObject powerObject;
}

