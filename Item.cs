using UnityEngine;

public class Item : MonoBehaviour
{
    public string type = "nothing";
    public int amount;
    public GameObject billboard;
    private StateManager stateManager;
    private GameManager gameManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        billboard.GetComponent<Renderer>().enabled = false;
        stateManager = FindObjectOfType<StateManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (type != "nothing")
            {
                TextureDictionary td = gameManager.GetComponent<TextureDictionary>();
                if (td != null)
                {
                    if (td.dictionary != null)
                    {
                        if (td.dictionary.ContainsKey(type + "_Icon"))
                        {
                            billboard.GetComponent<Renderer>().material.mainTexture = td.dictionary[type + "_Icon"];
                            billboard.GetComponent<Renderer>().enabled = true;
                        }
                        else if (td.dictionary.ContainsKey(type))
                        {
                            billboard.GetComponent<Renderer>().material.mainTexture = td.dictionary[type];
                            billboard.GetComponent<Renderer>().enabled = true;
                        }
                    }
                }
            }
            transform.Rotate(transform.up * 100 * Time.deltaTime);
        }
    } 

    //! Items fall to the ground faster than gravity would allow otherwise.
    public void FixedUpdate()
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 1))
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.up * 5000);
        }
    }
}

