using UnityEngine;

public class Item : MonoBehaviour
{
    public string type = "nothing";
    public int amount;
    public GameObject billboard;
    private StateManager stateManager;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        billboard.GetComponent<Renderer>().enabled = false;
        stateManager = FindObjectOfType<StateManager>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (!stateManager.Busy())
        {
            if (type != "nothing")
            {
                if (GetComponent<TextureDictionary>() != null)
                {
                    if (GetComponent<TextureDictionary>().dictionary != null)
                    {
                        if (GetComponent<TextureDictionary>().dictionary.ContainsKey(type + "_Icon"))
                        {
                            billboard.GetComponent<Renderer>().material.mainTexture = GetComponent<TextureDictionary>().dictionary[type + "_Icon"];
                            billboard.GetComponent<Renderer>().enabled = true;
                        }
                        else if (GetComponent<TextureDictionary>().dictionary.ContainsKey(type))
                        {
                            billboard.GetComponent<Renderer>().material.mainTexture = GetComponent<TextureDictionary>().dictionary[type];
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

