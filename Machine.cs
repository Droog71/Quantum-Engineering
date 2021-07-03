using UnityEngine;
using System;
using System.Net;
using System.Text.RegularExpressions;

public class Machine : MonoBehaviour
{
    public string ID;
    public bool logic;
    public bool powerON;
    public string creationMethod = "built";
    public GameManager gameManager;
    public StateManager stateManager;
    private string objectName;

    //! Called by MachineManager update coroutine.
    //! Overriden in various derived classes.
    public virtual void UpdateMachine()
    {

    }

    //! Destroyes the object and spawns explosion effects.
    public void Explode()
    {
        Instantiate(gameManager.machineExplosion, transform.position, transform.rotation);
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
        {
            if (GetComponent<Block>() != null)
            {
                objectName = GetComponent<Block>().blockName;
            }
            else if (GetComponent<ModMachine>() != null)
            {
                objectName = GetComponent<ModMachine>().machineName;
            }
            else
            {
                string[] splitName = Regex.Split(gameObject.name.Split('(')[0], @"(?<!^)(?=[A-Z])");
                if (splitName.Length > 1)
                {
                    objectName = splitName[0] + " " + splitName[1];
                }
                else
                {
                    objectName = splitName[0];
                }
            }

            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(PlayerPrefs.GetString("serverURL") + "/blocks");
                Vector3 pos = transform.position;
                Quaternion rot = transform.rotation;
                string position = Mathf.Round(pos.x) + "," + Mathf.Round(pos.y) + "," + Mathf.Round(pos.z);
                string rotation = Mathf.Round(rot.x) + "," + Mathf.Round(rot.y) + "," + Mathf.Round(rot.z) + "," + Mathf.Round(rot.w);
                client.UploadStringAsync(uri, "POST", "@" + 1 + ":" + objectName + ":" + position + ":" + rotation);
            }
        }
        Destroy(gameObject);
    }
}
