using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject exhaust;
    public int payloadRequired = 25;
    public int day = 1;
    private bool ascending;
    private bool visible;
    public bool landed;
    public float gameTime;
    private float liftOffTimer;
    private PlayerController player;
    private bool initialized;
    public bool rocketRequested;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<StateManager>().worldLoaded == true)
        {
            if (initialized == false)
            {
                Init();
            }

            gameTime += 1 * Time.deltaTime;
            FileBasedPrefs.SetFloat(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "gameTime", gameTime);

            if (gameTime >= 2400)
            {
                EndOfDay();
            }

            if (landed == false && (player.timeToDeliver == true || rocketRequested == true))
            {
                Descend();
            }

            if (landed == true)
            {
                liftOffTimer += 1 * Time.deltaTime;

                if (player.timeToDeliver == true)
                {
                    CheckCargo();
                }
                else if (liftOffTimer >= 600 && ascending == false)
                {
                    ascending = true;
                }

                if (ascending == true)
                {
                    if (transform.position.y < 2000)
                    {
                        Ascend();
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }
    }

    //! Initializes variables when the world is loaded.
    private void Init()
    {
        if (FileBasedPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired") != 0)
        {
            payloadRequired = FileBasedPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired");
        }
        if (FileBasedPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day") != 0)
        {
            day = FileBasedPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day");
        }
        gameTime = FileBasedPrefs.GetFloat(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "gameTime");
        initialized = true;
    }

    //! Checks the cargo of the rocket for the required amount of dark matter.
    private void CheckCargo()
    {
        int amountInRocket = 0;
        foreach (InventorySlot slot in GetComponent<InventoryManager>().inventory)
        {
            if (slot.amountInSlot > 0 && slot.typeInSlot == "Dark Matter")
            {
                amountInRocket += slot.amountInSlot;
            }
        }
        if (amountInRocket >= payloadRequired && ascending == false)
        {
            player.money += 1000;
            FileBasedPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "money", player.money);
            ascending = true;
        }
        else if (liftOffTimer >= 600 && ascending == false)
        {
            player.money -= 500;
            FileBasedPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "money", player.money);
            ascending = true;
        }
    }

    //! Called at the end of a day to increase the payload required for the next day.
    private void EndOfDay()
    {
        day += 1;
        gameTime = 0;
        if (payloadRequired <= 800000)
        {
            payloadRequired = payloadRequired * 2;
        }
        FileBasedPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day", day);
        FileBasedPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired", payloadRequired);
        player.timeToDeliver = true;
    }

    //! Resets the rocket when it leaves the area, disabling all sounds and rendering and clearing the inventory.
    private void Reset()
    {
        ClearInventory();
        DisableRenderers();
        liftOffTimer = 0;
        landed = false;
        player.timeToDeliver = false;
        rocketRequested = false;
        ascending = false;
        GetComponent<AudioSource>().enabled = false;
        exhaust.SetActive(false);
    }

    //! Moves the rocket in the positive Y direction.
    private void Ascend()
    {
        GetComponent<AudioSource>().enabled = true;
        exhaust.SetActive(true);
        transform.position += transform.up * 25 * Time.deltaTime;
    }

    //! Moves the rocket in the negative Y direction.
    private void Descend()
    {
        if (visible == false)
        {
            EnableRenderers();
        }
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.5f) || transform.position.y <= -66)
        {
            landed = true;
            GetComponent<AudioSource>().enabled = false;
            exhaust.SetActive(false);
        }
        else
        {
            GetComponent<AudioSource>().enabled = true;
            exhaust.SetActive(true);
            transform.position -= transform.up * 25 * Time.deltaTime;
        }
    }

    //! Empty the rocket's inventory.
    private void ClearInventory()
    {
        foreach (InventorySlot s in GetComponent<InventoryManager>().inventory)
        {
            s.typeInSlot = "nothing";
            s.amountInSlot = 0;
        }
        GetComponent<InventoryManager>().SaveData();
    }

    //! Disable all renderers attached to the rocket, so it is no longer visible.
    private void DisableRenderers()
    {
        if (visible == true)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
            foreach (Renderer r in renderers)
            {
                if (r.enabled == true)
                {
                    r.enabled = false;
                }
            }
            visible = false;
        }
    }

    //! Enabled all renderers attached to the rocket, making it visible.
    private void EnableRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in renderers)
        {
            if (r.enabled == false)
            {
                r.enabled = true;
            }
        }
        visible = true;
    }
}