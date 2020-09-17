using UnityEngine;

//! This class handles animations for reactor turbines.
public class TurbineImpeller : MonoBehaviour
{
    public GameObject turbine;

    //! Update is called once per frame.
    void Update()
    {
        if (turbine.GetComponent<AudioSource>().isPlaying == true)
        {
            transform.Rotate(-Vector3.forward * 600 * Time.deltaTime);
        }
    }
}
