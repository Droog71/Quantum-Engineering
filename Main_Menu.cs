using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Main_Menu : MonoBehaviour
{
    public GUISkin thisGUIskin;
    List<string> worldList;
    string worldName = "Enter World Name";
    public Texture2D titleTexture;
    public Texture2D title2Texture;
    public Texture2D worlListBackground;
    public GameObject videoPlayer;
    public GameObject menuSoundObject;
    public GameObject ambientSoundObject;
    public bool worldSelected;
    bool playingVideo;
    bool deletePrompt;
    bool escapePrompt;
    bool worldSelectPrompt;
    public bool finishedLoading;
    AudioSource buttonSounds;
    AudioSource ambient;
    float waitForVideoTimer;

    // Start is called before the first frame update
    void Start()
    {
        worldList = new List<string>();
        videoPlayer.GetComponent<VP>().PlayVideo("QE_Title.webm",true,0);
        buttonSounds = menuSoundObject.GetComponent<AudioSource>();
        ambient = ambientSoundObject.GetComponent<AudioSource>();
        ambient.Play();
        if (PlayerPrefsX.GetBool("changingWorld") == true)
        {
            PlayerPrefsX.SetBool("changingWorld", false);
            GameObject.Find("GameManager").GetComponent<StateManager>().WorldName = PlayerPrefs.GetString("worldName");
            worldSelected = true;
            ambient.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //STYLE
        GUI.skin = thisGUIskin;

        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;

        Rect backgroundRect = new Rect(0, 0, ScreenWidth, ScreenHeight);

        Rect worldListBackgroundRect = new Rect((ScreenWidth * 0.40f), (ScreenHeight * 0.45f), (ScreenWidth * 0.17f), (ScreenHeight * 0.40f));
        Rect worldListRect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.47f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));
        Rect worldListTitleRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.46f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));

        Rect escapePromptLabelRect = new Rect((ScreenWidth * 0.46f), (ScreenHeight * 0.18f), (ScreenWidth * 0.20f), (ScreenHeight * 0.05f));

        Rect deletePromptBackgroundRect = new Rect((ScreenWidth * 0.35f), (ScreenHeight * 0.14f),(ScreenWidth * 0.30f), (ScreenHeight * 0.20f));
        Rect deletePromptLabelRect = new Rect((ScreenWidth * 0.435f), (ScreenHeight * 0.18f), (ScreenWidth * 0.20f), (ScreenHeight * 0.05f));
        Rect deletePromptButton1Rect = new Rect((ScreenWidth * 0.39f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect deletePromptButton2Rect = new Rect((ScreenWidth * 0.51f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));

        Rect loadingMessageRect = new Rect((ScreenWidth * 0.48f), (ScreenHeight * 0.30f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));

        Rect buttonRect1 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.50f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect2 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.532f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect3 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.564f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect4 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.596f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect5 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.628f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect6 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.660f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect7 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.692f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect8 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.724f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect9 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.756f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));
        Rect buttonRect10 = new Rect((ScreenWidth * 0.405f), (ScreenHeight * 0.788f), (ScreenWidth * 0.020f), (ScreenHeight * 0.025f));

        Rect world1Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.50f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world2Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.532f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world3Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.564f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world4Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.596f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world5Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.628f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world6Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.660f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world7Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.692f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world8Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.724f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world9Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.756f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));
        Rect world10Rect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.788f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));

        if (worldSelected == false)
        {
            if (waitForVideoTimer > 1 && playingVideo == false)
            {
                playingVideo = true;
            }
            if (playingVideo == false)
            {
                GUI.DrawTexture(backgroundRect, titleTexture);
                waitForVideoTimer += 1 * Time.deltaTime;
            }
            else
            {
                GUI.DrawTexture(backgroundRect, title2Texture);
            }
            if (GUI.Button(new Rect(ScreenWidth * 0.58f, ScreenHeight * 0.4f, ScreenWidth * 0.15f, ScreenHeight * 0.03f), "START GAME") || Event.current.keyCode.Equals(KeyCode.Return))
            {
                if (worldSelected == false && worldName != "Enter World Name")
                {
                    if (PlayerPrefsX.GetStringArray("Worlds") != null)
                    {
                        worldList = PlayerPrefsX.GetStringArray("Worlds").ToList<string>();
                        if (PlayerPrefsX.GetStringArray("Worlds").Length < 10)
                        {
                            if (!worldList.Contains(worldName))
                            {
                                worldList.Add(worldName);
                                worldSelectPrompt = true;
                            }
                            else
                            {
                                if (PlayerPrefsX.GetBool(worldName+"sceneChangeRequired") == true)
                                {
                                    PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                                    PlayerPrefsX.SetBool("changingWorld", true);
                                    PlayerPrefs.SetString("worldName", worldName);
                                    PlayerPrefsX.SetBool(worldName + "sceneChangeRequired", true);
                                    SceneManager.LoadScene(1);
                                }
                                else
                                {
                                    PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                                    GameObject.Find("GameManager").GetComponent<StateManager>().WorldName = worldName;
                                    worldSelected = true;
                                    ambient.enabled = false;

                                }
                            }
                        }
                        else if (worldList.Contains(worldName))
                        {
                            if (PlayerPrefsX.GetBool(worldName + "sceneChangeRequired") == true)
                            {
                                PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                                PlayerPrefsX.SetBool("changingWorld", true);
                                PlayerPrefs.SetString("worldName", worldName);
                                PlayerPrefsX.SetBool(worldName + "sceneChangeRequired", true);
                                SceneManager.LoadScene(1);
                            }
                            else
                            {
                                PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                                GameObject.Find("GameManager").GetComponent<StateManager>().WorldName = worldName;
                                worldSelected = true;
                                ambient.enabled = false;

                            }
                        }
                    }
                }
                buttonSounds.Play();
            }
            worldName = GUI.TextField(new Rect(ScreenWidth * 0.41f, ScreenHeight * 0.4f, ScreenWidth * 0.15f, ScreenHeight * 0.03f), worldName, 16);
            GUI.DrawTexture(worldListBackgroundRect, worlListBackground);
            GUI.Label(worldListTitleRect, "SAVED WORLDS");
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 0)
            {
                GUI.Label(world1Rect, "1. " + PlayerPrefsX.GetStringArray("Worlds")[0]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 1)
            {
                GUI.Label(world2Rect, "2. " + PlayerPrefsX.GetStringArray("Worlds")[1]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 2)
            {
                GUI.Label(world3Rect, "3. " + PlayerPrefsX.GetStringArray("Worlds")[2]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 3)
            {
                GUI.Label(world4Rect, "4. " + PlayerPrefsX.GetStringArray("Worlds")[3]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 4)
            {
                GUI.Label(world5Rect, "5. " + PlayerPrefsX.GetStringArray("Worlds")[4]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 5)
            {
                GUI.Label(world6Rect, "6. " + PlayerPrefsX.GetStringArray("Worlds")[5]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 6)
            {
                GUI.Label(world7Rect, "7. " + PlayerPrefsX.GetStringArray("Worlds")[6]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 7)
            {
                GUI.Label(world8Rect, "8. " + PlayerPrefsX.GetStringArray("Worlds")[7]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 8)
            {
                GUI.Label(world9Rect, "9. " + PlayerPrefsX.GetStringArray("Worlds")[8]);
            }
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 9)
            {
                GUI.Label(world10Rect, "10. " + PlayerPrefsX.GetStringArray("Worlds")[9]);
            }
            if (GUI.Button(buttonRect1, "1"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 0)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[0];  
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect2, "2"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 1)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[1];                  
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect3, "3"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 2)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[2];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect4, "4"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 3)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[3];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect5, "5"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 4)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[4];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect6, "6"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 5)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[5];  
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect7, "7"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 6)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[6];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect8, "8"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 7)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[7];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect9, "9"))
            {
            if (PlayerPrefsX.GetStringArray("Worlds").Length > 8)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[8];
                }
                buttonSounds.Play();
            }
            if (GUI.Button(buttonRect10, "10"))
            {
                if (PlayerPrefsX.GetStringArray("Worlds").Length > 9)
                {
                    worldName = PlayerPrefsX.GetStringArray("Worlds")[9];
                }
                buttonSounds.Play();
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                if (deletePrompt == false && escapePrompt == false)
                {
                    deletePrompt = true;
                }
                buttonSounds.Play();
            }
            if (worldSelectPrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                GUI.Label(escapePromptLabelRect, "Choose location");
                if (GUI.Button(deletePromptButton1Rect, "Kepler-1625"))
                {
                    PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                    GameObject.Find("GameManager").GetComponent<StateManager>().WorldName = worldName;
                    worldSelected = true;
                    ambient.enabled = false;
                    buttonSounds.Play();
                }
                if (GUI.Button(deletePromptButton2Rect, "Gliese 876"))
                {
                    buttonSounds.Play();
                    PlayerPrefsX.SetStringArray("Worlds", worldList.ToArray());
                    PlayerPrefsX.SetBool("changingWorld", true);
                    PlayerPrefs.SetString("worldName", worldName);
                    PlayerPrefsX.SetBool(worldName+"sceneChangeRequired", true);
                    SceneManager.LoadScene(1);
                }
            }
            if (deletePrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                GUI.Label(deletePromptLabelRect, "Delete all world data?");
                if (GUI.Button(deletePromptButton1Rect, "Yes"))
                {
                    PlayerPrefs.DeleteAll();
                    deletePrompt = false;
                    buttonSounds.Play();
                }
                if (GUI.Button(deletePromptButton2Rect, "No"))
                {
                    deletePrompt = false;
                    buttonSounds.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (escapePrompt == false && deletePrompt == false)
                {
                    escapePrompt = true;
                }
                buttonSounds.Play();
            }
            if (escapePrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                GUI.Label(escapePromptLabelRect, "Exit the game?");
                if (GUI.Button(deletePromptButton1Rect, "Yes"))
                {
                    buttonSounds.Play();
                    if (!Application.isEditor)
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
                if (GUI.Button(deletePromptButton2Rect, "No"))
                {
                    escapePrompt = false;
                    buttonSounds.Play();
                }
            }
        }
        else if (GameObject.Find("GameManager").GetComponent<StateManager>().Loaded == false && finishedLoading == false)
        {
            GUI.Label(loadingMessageRect, "Loading...");
        }
        else if (GameObject.Find("GameManager").GetComponent<GameManager>().working == true && finishedLoading == false)
        {
            GUI.Label(loadingMessageRect, "Loading...");
        }
        else if (finishedLoading == false)
        {
            videoPlayer.GetComponent<VP>().StopVideo();
            finishedLoading = true;
        }
    }
}
