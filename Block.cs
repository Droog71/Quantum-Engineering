using UnityEngine;
using System;
using System.Net;
using System.Text.RegularExpressions;

public class Block : MonoBehaviour
{
    private Material material;
    private StateManager stateManager;
    private PlayerController playerController;
    private GameManager gameManager;
    public GameObject glassBreak;
    public GameObject explosion;
    private string objectName;
    private bool init;
    public string blockName;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        material = new Material(Shader.Find("Standard"));
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stateManager = gameManager.GetComponent<StateManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy() && init == false)
        {
            BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
            if (blockDictionary.meshDictionary.ContainsKey(blockName))
            {
                GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[blockName];
            }
            gameManager.meshManager.SetMaterial(gameObject, blockName);
            if (blockName.ToUpper().Contains("GLASS"))
            {
                explosion = glassBreak;
            }
            init = true;
        }
    }

    //! Destroyes the object and spawns explosion effects.
    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
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