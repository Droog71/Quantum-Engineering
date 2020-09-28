using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GUISkin thisGUIskin;
    public Texture2D titleTexture;
    public Texture2D title2Texture;
    public Texture2D titleTextureSD;
    public Texture2D worlListBackground;
    public GameObject videoPlayer;
    public GameObject menuSoundObject;
    public GameObject ambientSoundObject;
    public bool worldSelected;
    public bool finishedLoading;
    private string worldName = "Enter World Name";
    private StateManager stateManager;
    private List<string> worldList;
    private AudioSource buttonSounds;
    private AudioSource ambient;
    private float waitForVideoTimer;
    private bool notWideScreen;
    private bool worldSelectPrompt;
    private bool playingVideo;
    private bool deletePrompt;
    private bool escapePrompt;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
        worldList = new List<string>();
        videoPlayer.GetComponent<VP>().PlayVideo("QE_Title.webm",true,0);
        buttonSounds = menuSoundObject.GetComponent<AudioSource>();
        ambient = ambientSoundObject.GetComponent<AudioSource>();
        ambient.Play();
        if (PlayerPrefsX.GetPersistentBool("changingWorld") == true)
        {
            PlayerPrefsX.SetPersistentBool("changingWorld", false);
            FileBasedPrefs.SetWorldName(PlayerPrefs.GetString("worldName"));
            stateManager.WorldName = PlayerPrefs.GetString("worldName");
            worldSelected = true;
            ambient.enabled = false;
        }
    }

    //! Confirms world selection and loads the world.
    private void SelectWorld()
    {
        if (worldSelected == false && worldName != "Enter World Name")
        {
            worldList = PlayerPrefsX.GetPersistentStringArray("Worlds").ToList();
            if (worldList.Count < 10)
            {
                if (!worldList.Contains(worldName))
                {
                    worldList.Add(worldName);
                    worldSelectPrompt = true;
                }
                else
                {
                    if (PlayerPrefsX.GetPersistentBool(worldName + "sceneChangeRequired") == true)
                    {
                        ChangeScene();
                    }
                    else
                    {
                        StartGame();
                    }
                }
            }
            else if (worldList.Contains(worldName))
            {
                if (PlayerPrefsX.GetPersistentBool(worldName + "sceneChangeRequired") == true)
                {
                    ChangeScene();
                }
                else
                {
                    StartGame();
                }
            }
        }
    }

    //! Called when Kepler-1625 is selected and the scene needs to be changed.
    private void ChangeScene()
    {
        PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
        PlayerPrefsX.SetPersistentBool("changingWorld", true);
        PlayerPrefs.SetString("worldName", worldName);
        PlayerPrefsX.SetPersistentBool(worldName + "sceneChangeRequired", true);
        SceneManager.LoadScene(1);
    }

    //! Called when Gliese 876 is selected and the scene does not need to be changed.
    private void StartGame()
    {
        PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
        FileBasedPrefs.SetWorldName(worldName);
        stateManager.WorldName = worldName;
        worldSelected = true;
        ambient.enabled = false;
    }

    //! Gets the size of in pixels of text so it can be positioned on the screen accordingly.
    private Vector2 GetStringSize(string str)
    {
        GUIContent content = new GUIContent(str);
        GUIStyle style = GUI.skin.box;
        style.alignment = TextAnchor.MiddleCenter;
        return style.CalcSize(content);
    }

    private bool loadingWorld()
    {
        return stateManager.worldLoaded == false || stateManager.GetComponent<GameManager>().working == true;
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        // STYLE
        GUI.skin = thisGUIskin;

        // ASPECT RATIO
        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;
        if (ScreenWidth / ScreenHeight < 1.7f)
        {
            ScreenHeight = (ScreenHeight * 0.75f);
            notWideScreen = true;
        }
        if (ScreenHeight < 700)
        {
            GUI.skin.label.fontSize = 10;
        }

        Rect backgroundRect = new Rect(0, 0, Screen.width, Screen.height);

        Rect worldListBackgroundRect = new Rect((ScreenWidth * 0.40f), (ScreenHeight * 0.45f), (ScreenWidth * 0.17f), (ScreenHeight * 0.40f));
        Rect worldListRect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.47f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));
        Rect worldListTitleRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.46f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));

        Rect deletePromptBackgroundRect = new Rect(((ScreenWidth / 2) - 200), ScreenHeight * 0.14f, 400, ScreenHeight * 0.20f);
        Rect deletePromptLabelRect = new Rect((ScreenWidth * 0.435f), (ScreenHeight * 0.18f), (ScreenWidth * 0.20f), (ScreenHeight * 0.05f));
        Rect deletePromptButton1Rect = new Rect((ScreenWidth * 0.39f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect deletePromptButton2Rect = new Rect((ScreenWidth * 0.51f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));

        Rect startGameButtonRect = new Rect(ScreenWidth * 0.58f, ScreenHeight * 0.4f, ScreenWidth * 0.15f, ScreenHeight * 0.03f);
        Rect textFieldRect = new Rect(ScreenWidth * 0.41f, ScreenHeight * 0.4f, ScreenWidth * 0.15f, ScreenHeight * 0.03f);
        Rect loadingMessageRect = new Rect((ScreenWidth * 0.46f), (ScreenHeight * 0.30f), (ScreenWidth * 0.5f), (ScreenHeight * 0.5f));

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
                Texture2D bgTexture = notWideScreen ? titleTextureSD : titleTexture;
                GUI.DrawTexture(backgroundRect, bgTexture);
                waitForVideoTimer += 1 * Time.deltaTime;
            }
            else
            {
                GUI.DrawTexture(backgroundRect, title2Texture);
            }
            if (GUI.Button(startGameButtonRect, "START GAME") || Event.current.keyCode.Equals(KeyCode.Return))
            {
                buttonSounds.Play();
                SelectWorld();
            }

            GUI.DrawTexture(worldListBackgroundRect, worlListBackground);
            GUI.Label(worldListTitleRect, "SAVED WORLDS");

            string[] worlds = PlayerPrefsX.GetPersistentStringArray("Worlds");
            if (worlds.Length > 0)
            {
                GUI.Label(world1Rect, "1. " + worlds[0]);
            }
            if (worlds.Length > 1)
            {
                GUI.Label(world2Rect, "2. " + worlds[1]);
            }
            if (worlds.Length > 2)
            {
                GUI.Label(world3Rect, "3. " + worlds[2]);
            }
            if (worlds.Length > 3)
            {
                GUI.Label(world4Rect, "4. " + worlds[3]);
            }
            if (worlds.Length > 4)
            {
                GUI.Label(world5Rect, "5. " + worlds[4]);
            }
            if (worlds.Length > 5)
            {
                GUI.Label(world6Rect, "6. " + worlds[5]);
            }
            if (worlds.Length > 6)
            {
                GUI.Label(world7Rect, "7. " + worlds[6]);
            }
            if (worlds.Length > 7)
            {
                GUI.Label(world8Rect, "8. " + worlds[7]);
            }
            if (worlds.Length > 8)
            {
                GUI.Label(world9Rect, "9. " + worlds[8]);
            }
            if (worlds.Length > 9)
            {
                GUI.Label(world10Rect, "10. " + worlds[9]);
            }

            worldName = GUI.TextField(textFieldRect, worldName, 16);

            if (GUI.Button(buttonRect1, "1"))
            {
                buttonSounds.Play();
                if (worlds.Length > 0)
                {
                    worldName = worlds[0];  
                }
            }
            if (GUI.Button(buttonRect2, "2"))
            {
                buttonSounds.Play();
                if (worlds.Length > 1)
                {
                    worldName = worlds[1];
                }
            }
            if (GUI.Button(buttonRect3, "3"))
            {
                buttonSounds.Play();
                if (worlds.Length > 2)
                {
                    worldName = worlds[2];
                }
            }
            if (GUI.Button(buttonRect4, "4"))
            {
                buttonSounds.Play();
                if (worlds.Length > 3)
                {
                    worldName = worlds[3];
                }
            }
            if (GUI.Button(buttonRect5, "5"))
            {
                buttonSounds.Play();
                if (worlds.Length > 4)
                {
                    worldName = worlds[4];
                }
            }
            if (GUI.Button(buttonRect6, "6"))
            {
                buttonSounds.Play();
                if (worlds.Length > 5)
                {
                    worldName = worlds[5];  
                }
            }
            if (GUI.Button(buttonRect7, "7"))
            {
                buttonSounds.Play();
                if (worlds.Length > 6)
                {
                    worldName = worlds[6];
                }
            }
            if (GUI.Button(buttonRect8, "8"))
            {
                buttonSounds.Play();
                if (worlds.Length > 7)
                {
                    worldName = worlds[7];
                }
            }
            if (GUI.Button(buttonRect9, "9"))
            {
                buttonSounds.Play();
                if (worlds.Length > 8)
                {
                    worldName = worlds[8];
                }
            }
            if (GUI.Button(buttonRect10, "10"))
            {
                buttonSounds.Play();
                if (worlds.Length > 9)
                {
                    worldName = worlds[9];
                }
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                buttonSounds.Play();
                if (deletePrompt == false && escapePrompt == false)
                {
                    deletePrompt = true;
                }
            }

            if (worldSelectPrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                Vector2 size = GetStringSize("Choose location.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 3), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Choose location.");
                if (GUI.Button(deletePromptButton1Rect, "Kepler-1625"))
                {
                    buttonSounds.Play();
                    StartGame();
                }
                if (GUI.Button(deletePromptButton2Rect, "Gliese 876"))
                {
                    buttonSounds.Play();
                    ChangeScene();
                }
            }

            if (deletePrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                Vector2 size = GetStringSize("Delete selected world?");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 3), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Delete selected world?");
                if (GUI.Button(deletePromptButton1Rect, "Yes"))
                {
                    buttonSounds.Play();
                    string savePath = Path.Combine(Application.persistentDataPath, "SaveData");
                    Directory.CreateDirectory(savePath);
                    string savFile = "SaveData/" + worldName + ".sav";
                    string bakFile = "SaveData/" + worldName + ".bak";
                    string savPath = Path.Combine(Application.persistentDataPath, savFile);
                    string bakPath = Path.Combine(Application.persistentDataPath, bakFile);
                    File.Delete(savPath);
                    File.Delete(bakPath);
                    worldList = worlds.ToList();
                    foreach (string w in worlds)
                    {
                        if (w == worldName)
                        {
                            worldList.Remove(w);
                        }
                    }
                    PlayerPrefs.DeleteKey(worldName + "sceneChangeRequired");
                    PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
                    deletePrompt = false;
                }
                if (GUI.Button(deletePromptButton2Rect, "No"))
                {
                    buttonSounds.Play();
                    deletePrompt = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonSounds.Play();
                if (escapePrompt == false && deletePrompt == false)
                {
                    escapePrompt = true;
                }
            }

            if (escapePrompt == true)
            {
                GUI.DrawTexture(deletePromptBackgroundRect, worlListBackground);
                Vector2 size = GetStringSize("Exit the game?");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 3), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Exit the game?");
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
                    buttonSounds.Play();
                    escapePrompt = false;
                }
            }
        }
        else if (finishedLoading == false && loadingWorld() == true)
        {
            if (videoPlayer.GetComponent<VP>().IsPlaying())
            {
                videoPlayer.GetComponent<VP>().StopVideo();
            }
            Texture2D bgTexture = notWideScreen ? titleTextureSD : titleTexture;
            GUI.DrawTexture(backgroundRect, bgTexture);
            int f = GUI.skin.label.fontSize;
            GUI.skin.label.fontSize = 16;
            string loadingMessage = "Loading... " + stateManager.progress + "/" + stateManager.idList.Length;
            Vector2 size = GetStringSize(loadingMessage);
            Rect messagePos = new Rect((Screen.width / 2) - (size.x/2), Screen.height * 0.4f, size.x, size.y);
            GUI.Label(messagePos, loadingMessage);
            GUI.skin.label.fontSize = f;
        }
        else if (finishedLoading == false)
        {
            if (videoPlayer.GetComponent<VP>().IsPlaying())
            {
                videoPlayer.GetComponent<VP>().StopVideo();
            }
            finishedLoading = true;
        }
    }
}