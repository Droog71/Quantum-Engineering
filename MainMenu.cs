using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System;

public class MainMenu : MonoBehaviour
{
    public GUISkin thisGUIskin;
    public Texture2D titleTexture;
    public Texture2D title2Texture;
    public Texture2D titleTextureSD;
    public Texture2D guiBackground;
    public Texture2D worldListBackground;
    public Texture2D playerModel;
    private Texture2D colorSelectTexture;
    public GameObject videoPlayer;
    public GameObject menuSoundObject;
    public GameObject ambientSoundObject;
    public bool worldSelected;
    public bool finishedLoading;
    private string worldName = "Enter world name.";
    private string username = "Enter user name.";
    private string serverURL = "Enter server IP address.";
    private string[] worlds;
    private StateManager stateManager;
    private List<string> worldList;
    private AudioSource buttonSounds;
    private AudioSource ambient;
    private int scene;
    private bool local;
    private float waitForVideoTimer;
    private bool notWideScreen;
    private bool worldSelectPrompt;
    private bool playingVideo;
    private bool hosting;
    private bool downloadPrompt;
    private bool worldNamePrompt;
    private bool deletePrompt;
    private bool escapePrompt;
    private bool multiplayerPrompt;
    private bool localPrompt;
    private bool playerColorPrompt;
    private bool userNamePrompt;
    private bool networkAddressPrompt;
    private float playerRed = 1.0f;
    private float playerGreen = 1.0f;
    private float playerBlue = 1.0f;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        string[] commandLineOptions = Environment.GetCommandLineArgs();
        stateManager = FindObjectOfType<StateManager>();
        worldList = new List<string>();
        colorSelectTexture = new Texture2D(512, 128);
        buttonSounds = menuSoundObject.GetComponent<AudioSource>();
        ambient = ambientSoundObject.GetComponent<AudioSource>();

        if (!commandLineOptions.Contains("-batchmode"))
        {
            videoPlayer.GetComponent<VP>().PlayVideo("QE_Title.webm",true,0);
            ambient.Play();
        }

        if (PlayerPrefsX.GetPersistentBool("changingWorld") == true)
        {
            stateManager.worldName = PlayerPrefs.GetString("worldName");
            PlayerPrefs.DeleteKey("changingWorld");
            PlayerPrefs.DeleteKey("worldName");
            PlayerPrefs.Save();
            worldSelected = true;
            ambient.enabled = false;
        }
        else if (commandLineOptions.Contains("-batchmode"))
        {
            SetupDedicatedServer(commandLineOptions);
        }
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GuiFree())
            {
                escapePrompt = !escapePrompt;
            }
            else
            {
                networkAddressPrompt = false;
                worldSelectPrompt = false;
                deletePrompt = false;
                escapePrompt = false;
                multiplayerPrompt = false;
                localPrompt = false;
                playerColorPrompt = false;
                userNamePrompt = false;
                downloadPrompt = false;
                userNamePrompt = false;
            }
            buttonSounds.Play();
        }
    }

    //! Gets the local address for LAN games.
    private string GetLocalAddress()
    {
        string[] commandLineOptions = Environment.GetCommandLineArgs();
        bool devel = commandLineOptions.Contains("-devel");
        if (Application.isEditor || UnityEngine.Debug.isDebugBuild || devel == true)
        {
            return "localhost";
        }
        string hostName = Dns.GetHostName();
        string address = Dns.GetHostEntry(hostName).AddressList[0].ToString();
        return address;
    }

    //! Gets external IP address for online games.
    private string GetExternalAddress()
    {
        using (WebClient client = new WebClient())
        {    
            Uri uri = new Uri("https://api.ipify.org");
            return client.DownloadString(uri);
        }
    }

    //! Sets up a dedicated server.
    private void SetupDedicatedServer(string[] commandLineOptions)
    {
        worldName = commandLineOptions.Contains("-local") ? GetLocalAddress() : GetExternalAddress();
        FileBasedPrefs.SetWorldName(worldName);
        PlayerPrefsX.SetPersistentBool("multiplayer", true);
        PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");
        PlayerPrefs.SetString("UserName", worldName);
        if (commandLineOptions.Contains("-creative"))
        {
            FileBasedPrefs.SetBool(worldName + "creativeMode", true);
        }
        StartServer();
        Thread.Sleep(5000);
        worldList = PlayerPrefsX.GetPersistentStringArray("Worlds").ToList();
        if (worldList.Count < 10)
        {
            if (!worldList.Contains(worldName))
            {
                worldList.Add(worldName);
                foreach (string option in commandLineOptions)
                {
                    if (option.Contains("-scene"))
                    {
                        int cmd = 0;
                        try
                        {
                            cmd = int.Parse(option.Split('=')[1]);
                        }
                        catch
                        {
                            break;
                        }
                        scene = cmd == 1 || cmd == 3 ? cmd : 0;
                        FileBasedPrefs.SetInt(worldName + "scene", scene);
                        break;
                    }
                }
                if (scene != 0)
                {
                    ChangeScene();
                }
                else
                {
                    PreStart();
                }
            }
            else
            {
                PreStart();
            }
        }
        else if (worldList.Contains(worldName))
        {
            PreStart();
        }
        else
        {
            UnityEngine.Debug.Log("Failed to start server. World list is full.");
        }
    }

    //! Starts the external server program.
    private void StartServer()
    {
        string[] commandLineOptions = Environment.GetCommandLineArgs();
        bool headless = commandLineOptions.Contains("-headless");
        bool devel = commandLineOptions.Contains("-devel");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            if (Application.isEditor || UnityEngine.Debug.isDebugBuild || devel == true)
            {
                UnityEngine.Debug.Log("Starting development server...");
                string options = headless == true ? "local devel headless" : "local devel";
                Process.Start(Application.dataPath + "/Linux_Server/qe_server", options);
            }
            else if (local == true)
            {
                UnityEngine.Debug.Log("Starting LAN server...");
                string options = headless == true ? "local headless" : "local";
                Process.Start(Application.dataPath + "/Linux_Server/qe_server", options);
            }
            else
            {
                UnityEngine.Debug.Log("Starting online server...");
                Process.Start(Application.dataPath + "/Linux_Server/qe_server");
            }
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (local == true)
            {
                UnityEngine.Debug.Log("Starting LAN server...");
                string options = headless == true ? "local headless" : "local";
                Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe", options);
            }
            else
            {
                UnityEngine.Debug.Log("Starting online server...");
                Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe");
            }
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            if (local == true)
            {
                UnityEngine.Debug.Log("Starting LAN server...");
                string options = headless == true ? "local headless" : "local";
                Process.Start(Application.dataPath + "/Mac_Server/qe_server", options);
            }
            else
            {
                UnityEngine.Debug.Log("Starting online server...");
                Process.Start(Application.dataPath + "/Mac_Server/qe_server");
            }
        }
    }

    //! Downloads saved world from server.
    private void DownloadWorld()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "SaveData");
        string[] commandLineOptions = Environment.GetCommandLineArgs();
        bool devel = commandLineOptions.Contains("-devel");
        if (Application.isEditor || UnityEngine.Debug.isDebugBuild || devel == true)
        {
            savePath = Path.Combine(Application.persistentDataPath, "Downloads");
        }

        Directory.CreateDirectory(savePath);
        string saveFileLocation = Path.Combine(savePath + "/" + worldName + ".sav");
        try
        {
            using (WebClient client = new WebClient()) 
            {
                string url = PlayerPrefs.GetString("serverURL");
                client.DownloadFile(url+"/world", saveFileLocation);
            }
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
            worldName = "Enter world name.";
            downloadPrompt = true;
        }
        finally
        {
            if (File.Exists(Path.Combine(savePath + "/" + worldName + ".sav")))
            {
                UnityEngine.Debug.Log("World downloaded.");
                PreStart();
            }
        }
    }

    //! Confirms world selection and loads the world.
    private void SelectWorld()
    {
        if (worldSelected == false && worldName != "Enter world name.")
        {
            FileBasedPrefs.SetWorldName(worldName);
            worldList = PlayerPrefsX.GetPersistentStringArray("Worlds").ToList();
            if (worldList.Count < 10)
            {
                if (!worldList.Contains(worldName))
                {
                    worldList.Add(worldName);
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (hosting == false)
                        {
                            DownloadWorld();
                        }
                        else
                        {
                            worldSelectPrompt = true;
                        }
                    }
                    else
                    {
                        worldSelectPrompt = true;
                    }
                }
                else
                {
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (hosting == false)
                        {
                            DownloadWorld();
                        }
                        else
                        {
                            PreStart();
                        }
                    }
                    else
                    {
                        PreStart();
                    }
                }
            }
            else if (worldList.Contains(worldName))
            {
                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && hosting == false)
                {
                    DownloadWorld();
                }
                else
                {
                    PreStart();
                }
            }
        }
    }

    //! Selects the scene and starts the game.
    private void PreStart()
    {
        if (FileBasedPrefs.GetBool(worldName + "sceneChangeRequired") == true)
        {
            scene = FileBasedPrefs.GetInt(worldName + "scene");
            ChangeScene();
        }
        else
        {
            StartGame();
        }
    }

    //! Called when Kepler-1625 or Kepler-452b is selected and the scene needs to be changed.
    private void ChangeScene()
    {
        FileBasedPrefs.SetBool(worldName + "sceneChangeRequired", true);
        PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
        PlayerPrefsX.SetPersistentBool("changingWorld", true);
        PlayerPrefs.SetString("worldName", worldName);
        FileBasedPrefs.ManuallySave();
        PlayerPrefs.Save();
        SceneManager.LoadScene(scene);
    }

    //! Called when Gliese 876 is selected and the scene does not need to be changed.
    private void StartGame()
    {
        FileBasedPrefs.SetBool(worldName + "sceneChangeRequired", false);
        PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
        FileBasedPrefs.ManuallySave();
        PlayerPrefs.Save();
        stateManager.worldName = worldName;
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

    //! Returns true if the GUI is available.
    private bool GuiFree()
    {
        return deletePrompt == false
        && worldSelectPrompt == false
        && escapePrompt == false
        && multiplayerPrompt == false
        && userNamePrompt == false
        && networkAddressPrompt == false
        && playerColorPrompt == false
        && localPrompt == false
        && downloadPrompt == false
        && userNamePrompt == false;
    }


    //! Returns true if the world is currently being loaded.
    private bool LoadingWorld()
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
        Rect textEntryRect = new Rect((ScreenWidth * 0.36f), (ScreenHeight * 0.235f), (ScreenWidth * 0.16f), (ScreenHeight * 0.03f));

        Rect worldListBackgroundRect = new Rect((ScreenWidth * 0.40f), (ScreenHeight * 0.45f), (ScreenWidth * 0.17f), (ScreenHeight * 0.40f));
        Rect worldListRect = new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.47f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));
        Rect worldListTitleRect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.46f), (ScreenWidth * 0.15f), (ScreenHeight * 0.55f));

        Rect promptBackgroundRect = new Rect(((ScreenWidth / 2) - 300), ScreenHeight * 0.14f, 600, ScreenHeight * 0.20f);

        Rect deletePromptLabelRect = new Rect((ScreenWidth * 0.435f), (ScreenHeight * 0.18f), (ScreenWidth * 0.20f), (ScreenHeight * 0.05f));
        Rect deletePromptButton1Rect = new Rect((ScreenWidth * 0.39f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect deletePromptButton2Rect = new Rect((ScreenWidth * 0.51f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));

        Rect scenePromptButton1Rect = new Rect((ScreenWidth * 0.33f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect scenePromptButton2Rect = new Rect((ScreenWidth * 0.45f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect scenePromptButton3Rect = new Rect((ScreenWidth * 0.57f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect creativeButtonRect = new Rect((ScreenWidth / 2) - (ScreenWidth * 0.025f), (ScreenHeight * 0.30f), (ScreenWidth * 0.014f), (ScreenHeight * 0.0225f));
        Rect creativeLabelrect = new Rect((ScreenWidth / 2), (ScreenHeight * 0.299f), (ScreenWidth * 0.12f), (ScreenHeight * 0.05f));

        Rect startGameButtonRect = new Rect(ScreenWidth * 0.58f, ScreenHeight * 0.4f, ScreenWidth * 0.15f, ScreenHeight * 0.03f);
        Rect multiplayerButtonRect = new Rect(ScreenWidth * 0.58f, ScreenHeight * 0.44f, ScreenWidth * 0.15f, ScreenHeight * 0.03f);
        Rect modioButtonRect = new Rect(ScreenWidth * 0.58f, ScreenHeight * 0.48f, ScreenWidth * 0.15f, ScreenHeight * 0.03f);
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

            if (playerColorPrompt == false)
            {
                if (GUI.Button(startGameButtonRect, "SINGLE PLAYER"))
                {
                    buttonSounds.Play();
                    if (worldName != "Enter world name.")
                    {
                        PlayerPrefsX.SetPersistentBool("multiplayer", false);
                        SelectWorld();
                    }
                    else
                    {
                        worldNamePrompt = true;
                    }
                }

                if (GUI.Button(multiplayerButtonRect, "MULTIPLAYER"))
                {
                    buttonSounds.Play();
                    PlayerPrefsX.SetPersistentBool("multiplayer", true);
                    multiplayerPrompt = true;
                }

                if (GUI.Button(modioButtonRect, "MODS"))
                {
                    buttonSounds.Play();
                    SceneManager.LoadScene(2);
                }

                GUI.DrawTexture(worldListBackgroundRect, worldListBackground);
                GUI.Label(worldListTitleRect, "SAVED WORLDS");

                worlds = PlayerPrefsX.GetPersistentStringArray("Worlds");
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

                worldName = GUI.TextField(textFieldRect, worldName, 20);

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
                    deletePrompt |= GuiFree();
                    buttonSounds.Play();
                }
            }

            if (worldSelectPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, worldListBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Choose location.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Choose location.");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(scenePromptButton1Rect, "Kepler-1625"))
                {
                    buttonSounds.Play();
                    StartGame();
                }
                if (GUI.Button(scenePromptButton2Rect, "Kepler-452b"))
                {
                    buttonSounds.Play();
                    scene = 3;
                    FileBasedPrefs.SetInt(worldName + "scene", scene);
                    ChangeScene();
                }
                if (GUI.Button(scenePromptButton3Rect, "Gliese 876"))
                {
                    buttonSounds.Play();
                    scene = 1;
                    FileBasedPrefs.SetInt(worldName + "scene", scene);
                    ChangeScene();
                }

                GUI.Label(creativeLabelrect, "Creative Mode");
                bool creativeMode = FileBasedPrefs.GetBool(worldName + "creativeMode");
                string check = creativeMode == true ? "X" : "";
                if (GUI.Button(creativeButtonRect, check))
                {
                    creativeMode = !creativeMode;
                    FileBasedPrefs.SetBool(worldName + "creativeMode", creativeMode);
                }
            }

            if (deletePrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Delete selected world?");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Delete selected world?");
                GUI.skin.label.fontSize = f;
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
                    PlayerPrefsX.SetPersistentStringArray("Worlds", worldList.ToArray());
                    PlayerPrefs.Save();
                    deletePrompt = false;
                }
                if (GUI.Button(deletePromptButton2Rect, "No"))
                {
                    buttonSounds.Play();
                    deletePrompt = false;
                }
            }

            if (multiplayerPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Multiplayer.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Multiplayer");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(deletePromptButton1Rect, "Host"))
                {
                    hosting = true;
                    multiplayerPrompt = false;
                    localPrompt = true;
                }
                if (GUI.Button(deletePromptButton2Rect, "Join"))
                {
                    multiplayerPrompt = false;
                    networkAddressPrompt = true;
                }
            }

            if (localPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Multiplayer.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Multiplayer");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(deletePromptButton1Rect, "Local"))
                {
                    local = true;
                    localPrompt = false;
                    userNamePrompt = true;
                    worldName = local == true ? GetLocalAddress() : GetExternalAddress();
                    PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");
                    StartServer();
                }
                if (GUI.Button(deletePromptButton2Rect, "Online"))
                {
                    local = false;
                    localPrompt = false;
                    userNamePrompt = true;
                    worldName = local == true ? GetLocalAddress() : GetExternalAddress();
                    PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");
                    StartServer();
                }
            }

            if (networkAddressPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Enter server IP address.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Enter server IP address.");
                GUI.skin.label.fontSize = f;
                serverURL = GUI.TextField(textEntryRect, serverURL, 30);
                if (GUI.Button(scenePromptButton3Rect, "OK"))
                {
                    worldName = serverURL;
                    PlayerPrefs.SetString("serverURL", "http://" + serverURL + ":5000");
                    networkAddressPrompt = false;
                    userNamePrompt = true;
                }
            }

            if (userNamePrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Enter user name.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Enter user name.");
                GUI.skin.label.fontSize = f;
                username = GUI.TextField(textEntryRect, username, 30);
                if (GUI.Button(scenePromptButton3Rect, "OK"))
                {
                    PlayerPrefs.SetString("UserName", username);
                    userNamePrompt = false;
                    playerColorPrompt = true;
                }
            }

            if (playerColorPrompt == true)
            {
                GUI.DrawTexture(new Rect((ScreenWidth * 0.4f), (ScreenHeight * 0.18f), (ScreenWidth * 0.2f), (ScreenHeight * 0.60f)), worldListBackground);

                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;

                GUIStyle style = GUI.skin.box;
                style.alignment = TextAnchor.MiddleCenter;
                GUIContent content = new GUIContent("Player Color");
                Vector2 size = style.CalcSize(content);
                Rect titleRect = new Rect((Screen.width / 2) - (size.x / 2.5f), ScreenHeight * 0.24f, size.x, size.y);
                GUI.Label(titleRect, "Player Color");

                GUIStyle style2 = GUI.skin.box;
                style2.alignment = TextAnchor.MiddleCenter;
                GUIContent content2 = new GUIContent("Select Color");
                Vector2 size2 = style2.CalcSize(content2);
                Rect titleRect2 = new Rect((Screen.width / 2) - (size2.x / 2.5f), ScreenHeight * 0.3f, size2.x, size2.y);
                GUI.Label(titleRect2, "Select Color");

                GUI.skin.label.fontSize = f;

                GUI.Label(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.45f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), "Red");
                GUI.Label(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.51f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), "Green");
                GUI.Label(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.57f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), "Blue");

                playerRed = GUI.HorizontalSlider(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.48f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), playerRed, 0, 1);
                playerGreen = GUI.HorizontalSlider(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.54f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), playerGreen, 0, 1);
                playerBlue = GUI.HorizontalSlider(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.60f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), playerBlue, 0, 1);

                Color playerColor = new Color(Mathf.Round(playerRed), Mathf.Round(playerGreen), Mathf.Round(playerBlue));
                Color actualColor = new Color(playerRed, playerGreen, playerBlue);

                GUI.color = actualColor;
                GUI.DrawTexture(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.36f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), colorSelectTexture);

                GUI.color = playerColor;
                GUI.DrawTexture(new Rect((ScreenWidth * 0.2f), (ScreenHeight * 0.2f), (ScreenWidth * 0.2f), (ScreenHeight * 0.56f)), playerModel);
                GUI.DrawTexture(new Rect((ScreenWidth * 0.6f), (ScreenHeight * 0.2f), (ScreenWidth * 0.2f), (ScreenHeight * 0.56f)), playerModel);
                GUI.color = Color.white;

                if (GUI.Button(new Rect((ScreenWidth * 0.43f), (ScreenHeight * 0.65f), (ScreenWidth * 0.14f), (ScreenHeight * 0.05f)), "DONE"))
                {
                    buttonSounds.Play();
                    playerColorPrompt = false;
                    PlayerPrefs.SetFloat("playerRed", playerRed);
                    PlayerPrefs.SetFloat("playerGreen", playerGreen);
                    PlayerPrefs.SetFloat("playerBlue", playerBlue);
                    SelectWorld();
                }
            }

            if (escapePrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Exit the game?");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Exit the game?");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(deletePromptButton1Rect, "Yes"))
                {
                    buttonSounds.Play();
                    if (!Application.isEditor)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
                if (GUI.Button(deletePromptButton2Rect, "No"))
                {
                    buttonSounds.Play();
                    escapePrompt = false;
                }
            }

            if (worldNamePrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Please enter a world name.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 2.5f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Please enter a world name.");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(scenePromptButton2Rect, "OK"))
                {
                    buttonSounds.Play();
                    worldNamePrompt = false;
                }
            }

            if (downloadPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Unable to connect to server.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x  / 2.5f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Unable to connect to server.");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(scenePromptButton2Rect, "OK"))
                {
                    buttonSounds.Play();
                    downloadPrompt = false;
                }
            }
        }
        else if (finishedLoading == false && LoadingWorld() == true)
        {
            if (videoPlayer.GetComponent<VP>().IsPlaying())
            {
                videoPlayer.GetComponent<VP>().StopVideo();
            }
            Texture2D bgTexture = notWideScreen ? titleTextureSD : titleTexture;
            GUI.DrawTexture(backgroundRect, bgTexture);
            int f = GUI.skin.label.fontSize;
            GUI.skin.label.fontSize = 16;
            GUI.color = new Color(0.2824f, 0.7882f, 0.9569f);
            int idTotal = stateManager.machineIdList.Length + stateManager.blockIdList.Length;
            string loadingMessage = "Loading... " + stateManager.progress + "/" + idTotal;
            if (stateManager.progress > 0 && stateManager.progress >= idTotal)
            {
                loadingMessage = "Initializing... " + stateManager.currentMachine + "/" + stateManager.totalMachines;
            }
            Vector2 size = GetStringSize(loadingMessage);
            Rect messagePos = new Rect((Screen.width / 2) - (size.x/2), Screen.height * 0.4f, size.x, size.y);
            GUI.Label(messagePos, loadingMessage);
            GUI.color = Color.white;
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