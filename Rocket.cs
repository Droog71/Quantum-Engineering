using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject exhaust;
    public int payloadRequired = 25;
    public int day = 1;
    private bool readyForTakeOff;
    private bool visible;
    public bool landed;
    public float gameTime;
    private float liftOffTimer;
    private PlayerController player;
    private bool initialized;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<StateManager>().worldLoaded == true)
        {
            if (initialized == false)
            {
                if (PlayerPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired") != 0)
                {
                    payloadRequired = PlayerPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired");
                }
                if (PlayerPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day") != 0)
                {
                    day = PlayerPrefs.GetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day");
                }
                gameTime = PlayerPrefs.GetFloat(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "gameTime");
                initialized = true;
            }
            if (landed == false)
            {
                gameTime += 1 * Time.deltaTime;
                PlayerPrefs.SetFloat(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "gameTime", gameTime);
                if (player.timeToDeliver == false)
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
                    if (gameTime >= 2400)
                    {
                        if (payloadRequired <= 800000)
                        {
                            payloadRequired = payloadRequired * 2;
                        }
                        day += 1;
                        gameTime = 0;
                        player.timeToDeliver = true;
                        PlayerPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "payloadRequired", payloadRequired);
                        PlayerPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "day", day);
                    }
                }
                else
                {
                    if (visible == false)
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
                    if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.5f) || transform.position.y <= -66)
                    {
                        landed = true;
                        GetComponent<AudioSource>().enabled = false;
                        exhaust.SetActive(false);
                    }
                    else
                    {
                        //Debug.Log("Rocket landing.");
                        GetComponent<AudioSource>().enabled = true;
                        exhaust.SetActive(true);
                        transform.position -= transform.up * 25 * Time.deltaTime;
                    }
                }
            }

            if (landed == true)
            {
                liftOffTimer += 1 * Time.deltaTime;
                int amountInRocket = 0;
                InventoryManager thisContainer = GetComponent<InventoryManager>();
                foreach (InventorySlot slot in thisContainer.inventory)
                {
                    if (slot.amountInSlot > 0 && slot.typeInSlot == "Dark Matter")
                    {
                        amountInRocket += slot.amountInSlot;
                    }
                }
                if (amountInRocket >= payloadRequired && readyForTakeOff == false)
                {
                    player.money += 1000;
                    PlayerPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "money", player.money);
                    readyForTakeOff = true;
                }
                else if (liftOffTimer >= 600 && readyForTakeOff == false)
                {
                    player.money -= 500;
                    PlayerPrefs.SetInt(GameObject.Find("GameManager").GetComponent<StateManager>().WorldName + "money", player.money);
                    readyForTakeOff = true;
                }

                if (readyForTakeOff == true)
                {
                    if (transform.position.y < 2000)
                    {
                        //Debug.Log("Rocket leaving");
                        GetComponent<AudioSource>().enabled = true;
                        exhaust.SetActive(true);
                        transform.position += transform.up * 25 * Time.deltaTime;
                    }
                    else
                    {
                        InventoryManager rocketContainer = GetComponent<InventoryManager>();
                        foreach (InventorySlot s in rocketContainer.inventory)
                        {
                            s.typeInSlot = "nothing";
                            s.amountInSlot = 0;
                        }
                        thisContainer.SaveData();
                        liftOffTimer = 0;
                        landed = false;
                        player.timeToDeliver = false;
                        readyForTakeOff = false;
                        GetComponent<AudioSource>().enabled = false;
                        exhaust.SetActive(false);
                        //Debug.Log("Rocket waiting.");
                    }
                }
            }
        }
    }
}