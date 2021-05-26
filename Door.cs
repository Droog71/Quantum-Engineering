using UnityEngine;
using System.Collections.Generic;

public class Door : Machine
{
    private StateManager stateManager;
    private GameManager gameManager;
    public string ID = "unassigned";
    public int address;
    public string type;
    public bool open;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip[] audioClips;
    public GameObject openObject;
    public GameObject closedObject;
    public GameObject effects;
    private List<string> textureList;
    public string[] textures;
    public int textureIndex;
    public string material;
    public int audioClip;
    public bool edited;
    private bool init;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        audioClips = new AudioClip[] { clip1, clip2, clip3 };
        stateManager = FindObjectOfType<StateManager>();
        gameManager = FindObjectOfType<GameManager>();
        textureList = new List<string>();
        Dictionary<string, Texture2D> textureDictionary = GameObject.Find("GameManager").GetComponent<TextureDictionary>().dictionary;
        Dictionary<string, GameObject> blockDictionary = GameObject.Find("Player").GetComponent<BuildController>().blockDictionary.blockDictionary;
        foreach (KeyValuePair<string, GameObject> kvp in blockDictionary)
        {
            if (textureDictionary.ContainsKey(kvp.Key) && !textureDictionary.ContainsKey(kvp.Key+"_Icon") && !kvp.Key.ToUpper().Contains("RAMP"))
            {
                textureList.Add(kvp.Key);
            }
        }
        textures = textureList.ToArray();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (ID == "unassigned" || stateManager.initMachines == false)
            return;

        if (edited == true && init == false)
        {
            GetComponent<AudioSource>().clip = audioClips[audioClip];
            gameManager.meshManager.SetMaterial(closedObject, material);
            init = true;
        }

        if (QualitySettings.GetQualityLevel() < 3 && type == "Quantum Hatchway")
        {
            effects.SetActive(false);
        }

        GetComponent<PhysicsHandler>().UpdatePhysics();
    }

    //! Toggle the open or closed state of the hatchway.
    public void ToggleOpen()
    {
        if (open == false)
        {
            openObject.SetActive(true);
            closedObject.SetActive(false);
            GetComponent<Collider>().isTrigger = true;
            open = true;
            Door[] doors = Object.FindObjectsOfType<Door>();
            foreach (Door d in doors)
            {
                if (d.type == type)
                {
                    float distance = Vector3.Distance(transform.position, d.transform.position);
                    if (distance <= 5 && d.open != open)
                    {
                        d.ToggleOpen();
                    }
                }
            }
        }
        else
        {
            openObject.SetActive(false);
            closedObject.SetActive(true);
            GetComponent<Collider>().isTrigger = false;
            open = false;
            Door[] doors = Object.FindObjectsOfType<Door>();
            foreach (Door d in doors)
            {
                if (d.type == type)
                {
                    float distance = Vector3.Distance(transform.position, d.transform.position);
                    if (distance <= 5 && d.open != open)
                    {
                        d.ToggleOpen();
                    }
                }
            }
        }
    }
}