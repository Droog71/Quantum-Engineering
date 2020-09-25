using UnityEngine;

//! This class handles repositioning of the scanner in first person view, based on FOV.
public class ScannerOffset : MonoBehaviour
{
    private StateManager stateManager;
    public GameObject standardScanner;
    public GameObject adjustedScanner;
    public GameObject adjustedScanner2;

    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (Camera.main.fieldOfView > 66.6667 && Camera.main.fieldOfView < 73.3334)
            {
                standardScanner.SetActive(false);
                adjustedScanner.SetActive(true);
                adjustedScanner2.SetActive(false);
            }
            else if (Camera.main.fieldOfView > 73.3334)
            {
                standardScanner.SetActive(false);
                adjustedScanner.SetActive(false);
                adjustedScanner2.SetActive(true);
            }
            else if (Camera.main.fieldOfView < 66.6667)
            {
                standardScanner.SetActive(true);
                adjustedScanner.SetActive(false);
                adjustedScanner2.SetActive(false);
            }
        }
    }
}
