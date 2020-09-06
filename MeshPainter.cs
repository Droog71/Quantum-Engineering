using System.Collections;
using UnityEngine;

public class MeshPainter : MonoBehaviour
{
    public int ID;
    private float saveTimer;
    private Coroutine saveDataCoRoutine;

    // Called by unity engine on start up to initialize variables
    public void Start()
    {
        string worldName = GameObject.Find("GameManager").GetComponent<StateManager>().WorldName;
        if (gameObject.name.Equals("ironHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "ironHolder" + ID + "painted") == true)
            {
                GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Blue"));
                Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform T in blocks)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Blue"));
                }
            }
        }
        if (gameObject.name.Equals("steelHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "steelHolder" + ID + "painted") == true)
            {
                GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Blue"));
                Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform T in blocks)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Blue"));
                }
            }
        }
        if (gameObject.name.Equals("brickHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "brickHolder" + ID + "painted") == true)
            {
                GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Blue"));
                Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform T in blocks)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Blue"));
                }
            }
       }
        if (gameObject.name.Equals("glassHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "glassHolder" + ID + "painted") == true)
            {
                GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Blue"));
                Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform T in blocks)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Blue"));
                }
            }
        }
    }

    // Called once per frame by unity engine
    public void Update()
    {
        saveTimer += 1 * Time.deltaTime;
        if (saveTimer >= 1)
        {
            saveDataCoRoutine = StartCoroutine(SaveDataCoRoutine());
            saveTimer = 0;
        }
    }

    //Saves the color of painted objects
    private IEnumerator SaveDataCoRoutine()
    {
        string worldName = GameObject.Find("GameManager").GetComponent<StateManager>().WorldName;
        if (gameObject.name.Equals("ironHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "ironHolder" + ID + "painted") == true)
        {
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Red", GetComponent<Renderer>().material.color.r);
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Green", GetComponent<Renderer>().material.color.g);
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Blue", GetComponent<Renderer>().material.color.b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Blue"));
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("steelHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "steelHolder" + ID + "painted") == true)
        {
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Red", GetComponent<Renderer>().material.color.r);
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Green", GetComponent<Renderer>().material.color.g);
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Blue", GetComponent<Renderer>().material.color.b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Blue"));
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("brickHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "brickHolder" + ID + "painted") == true)
        {
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Red", GetComponent<Renderer>().material.color.r);
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Green", GetComponent<Renderer>().material.color.g);
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Blue", GetComponent<Renderer>().material.color.b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Blue"));
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("glassHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "glassHolder" + ID + "painted") == true)
        {
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Red", GetComponent<Renderer>().material.color.r);
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Green", GetComponent<Renderer>().material.color.g);
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Blue", GetComponent<Renderer>().material.color.b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Red"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Green"), FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Blue"));
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
    }
}
