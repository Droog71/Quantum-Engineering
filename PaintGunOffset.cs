using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGunOffset : MonoBehaviour
{
    public GameObject standardPaintGun;
    public GameObject adjustedPaintGun;
    public GameObject adjustedPaintGun2;

    // Called once per frame by unity engine
    public void Update()
    {
        if (Camera.main.fieldOfView > 66.6667 && Camera.main.fieldOfView < 73.3334)
        {
            standardPaintGun.SetActive(false);
            adjustedPaintGun.SetActive(true);
            adjustedPaintGun2.SetActive(false);
        }
        else if (Camera.main.fieldOfView > 73.3334)
        {
            standardPaintGun.SetActive(false);
            adjustedPaintGun.SetActive(false);
            adjustedPaintGun2.SetActive(true);
        }
        else if (Camera.main.fieldOfView < 66.6667)
        {
            standardPaintGun.SetActive(true);
            adjustedPaintGun.SetActive(false);
            adjustedPaintGun2.SetActive(false);
        }
    }
}
