using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using MEC;
using System.Linq;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
    public GUISkin thisGUIskin;
    public Texture2D titleTexture;
    public Texture2D title2Texture;
    public Texture2D titleTextureSD;
    public Texture2D guiBackground;
    public Texture2D worldListBackground;
    public Texture2D playerModel;
    public Texture2D scene1;
    public Texture2D scene2;
    public Texture2D scene3;
    public Texture2D scene4;
    private Texture2D colorSelectTexture;
    public GameObject videoPlayer;
    public GameObject menuSoundObject;
    public GameObject ambientSoundObject;
    public bool worldSelected;
    public bool finishedLoading;
    private string worldName = "Enter world name.";
    private string username = "Enter user name.";
    private string password = "Enter password.";
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
    private bool publicPrompt;
    private bool playerColorPrompt;
    private bool userNamePrompt;
    private bool passwordPrompt;
    private bool networkAddressPrompt;
    private bool downloadingWorld;
    private string serverList = "none";
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
                passwordPrompt = false;
                publicPrompt = false;
            }
            buttonSounds.Play();
        }
    }

    //! Gets the local address for LAN games.
    private string GetLocalAddress()
    {
        string commandLineOptions = Environment.CommandLine;
        bool devel = commandLineOptions.Contains("-devel");
        if (devel == true || Application.isEditor || UnityEngine.Debug.isDebugBuild)
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

    //! Gets server data from master server.
    private async Task GetServerList()
    {
        using (WebClient client = new WebClient())
        {    
            Uri uri = new Uri("http://45.77.158.179:48000/servers");
            serverList = await client.DownloadStringTaskAsync(uri);
        }
    }

    //! Sets up a dedicated server.
    private void SetupDedicatedServer(string[] commandLineOptions)
    {
        worldName = commandLineOptions.Contains("-local") ? GetLocalAddress() : GetExternalAddress();
        PlayerPrefs.SetString("ip", worldName);
        FileBasedPrefs.SetWorldName(worldName);
        PlayerPrefsX.SetPersistentBool("multiplayer", true);
        PlayerPrefsX.SetPersistentBool("hosting", true);
        PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");
        PlayerPrefs.SetString("UserName", worldName);

        bool creativeMode = commandLineOptions.Contains("-creative");
        FileBasedPrefs.SetBool(worldName + "creativeMode", creativeMode);

        bool hazardsEnabled = commandLineOptions.Contains("-hazards");
        PlayerPrefsX.SetPersistentBool("hazardsEnabled", hazardsEnabled);

        bool announce = commandLineOptions.Contains("-announce");
        PlayerPrefsX.SetPersistentBool("announce", announce);

        UnityEngine.Debug.Log("Creative Mode: " + FileBasedPrefs.GetBool(worldName + "creativeMode"));
        UnityEngine.Debug.Log("Hazards: " + PlayerPrefsX.GetPersistentBool("hazardsEnabled"));
        UnityEngine.Debug.Log("Announce: " + PlayerPrefsX.GetPersistentBool("announce"));

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
                        scene = cmd == 1 || cmd == 3 || cmd == 4 ? cmd : 0;
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
        local = commandLineOptions.Contains("-local");
        bool headless = commandLineOptions.Contains("-headless");
        bool devel = commandLineOptions.Contains("-devel");
        bool hazards = commandLineOptions.Contains("-hazards");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            StartLinuxServer(headless, devel, hazards);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            StartWindowsServer(headless, devel, hazards);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            StartMacServer(headless, devel, hazards);
        }
    }

    //! Starts the server python app for Linux OS.
    private void StartLinuxServer(bool headless, bool devel, bool hazards)
    {
        if (Application.isEditor || UnityEngine.Debug.isDebugBuild || devel == true)
        {
            UnityEngine.Debug.Log("Starting development server...");
            string options = headless == true ? "local devel headless" : "local devel";
            if (hazards == true) { options += " hazards"; }
            Process.Start(Application.dataPath + "/Linux_Server/qe_server", options);
        }
        else if (local == true)
        {
            UnityEngine.Debug.Log("Starting LAN server...");
            string options = headless == true ? "local headless" : "local";
            if (hazards == true) { options += " hazards"; }
            Process.Start(Application.dataPath + "/Linux_Server/qe_server", options);
        }
        else if (headless == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            string options = hazards == true ? "headless hazards" : "headless";
            Process.Start(Application.dataPath + "/Linux_Server/qe_server", options);
        }
        else if (hazards == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Linux_Server/qe_server", "hazards");
        }
        else
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Linux_Server/qe_server");
        }
    }

    //! Starts the server python app for Windows OS.
    private void StartWindowsServer(bool headless, bool devel, bool hazards)
    {
        if (local == true)
        {
            UnityEngine.Debug.Log("Starting LAN server...");
            string options = headless == true ? "local headless" : "local";
            if (hazards == true) { options += " hazards"; }
            Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe", options);
        }
        else if (headless == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            string options = hazards == true ? "headless hazards" : "headless";
            Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe", options);
        }
        else if (hazards == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe", "hazards");
        }
        else
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Windows_Server/qe_server.exe");
        }
    }

    //! Starts the server python app for Mac OS.
    private void StartMacServer(bool headless, bool devel, bool hazards)
    {
        if (local == true)
        {
            UnityEngine.Debug.Log("Starting LAN server...");
            string options = headless == true ? "local headless" : "local";
            if (hazards == true) { options += " hazards"; }
            Process.Start(Application.dataPath + "/Mac_Server/qe_server", options);
        }
        else if (headless == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            string options = hazards == true ? "headless hazards" : "headless";
            Process.Start(Application.dataPath + "/Mac_Server/qe_server", options);
        }
        else if (hazards == true)
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Mac_Server/qe_server", "hazards");
        }
        else
        {
            UnityEngine.Debug.Log("Starting online server...");
            Process.Start(Application.dataPath + "/Mac_Server/qe_server");
        }
    }

    //! Downloads the .sav file from the server using UnityWebRequest.
    private IEnumerator<float> DownloadWorld() 
    {
        downloadingWorld = true;
        string url = PlayerPrefs.GetString("serverURL") + "/world";
        using (UnityWebRequest www = new UnityWebRequest(url))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequestAsyncOperation ao = www.SendWebRequest();
            while (!ao.isDone)
            {
                yield return Timing.WaitForOneFrame;
            }
            if (www.isNetworkError || www.isHttpError)
            {
                UnityEngine.Debug.Log(www.error);
                worldName = "Enter world name.";
                videoPlayer.GetComponent<VP>().PlayVideo("QE_Title.webm",true,0);
                downloadingWorld = false;
                downloadPrompt = true;
            }
            else
            {
                string commandLineOptions = Environment.CommandLine;
                bool devel = commandLineOptions.Contains("-devel");
                string savePath = Path.Combine(Application.persistentDataPath, "SaveData");
                if (devel == true || Application.isEditor || UnityEngine.Debug.isDebugBuild)
                {
                    savePath = Path.Combine(Application.persistentDataPath, "Downloads");
                }
                string saveFileLocation = Path.Combine(savePath + "/" + worldName + ".sav");
                Directory.CreateDirectory(savePath);
                File.WriteAllText(saveFileLocation, www.downloadHandler.text);
                UnityEngine.Debug.Log("World downloaded.");
                FileBasedPrefs.SetWorldName(worldName);
                PreStart();
            }
        }
        downloadingWorld = false;
    }

    //! Confirms world selection and loads the world.
    private void SelectWorld()
    {
        if (worldSelected == false && worldName != "Enter world name.")
        {
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
                            Timing.RunCoroutine(DownloadWorld());
                        }
                        else
                        {
                            FileBasedPrefs.SetWorldName(worldName);
                            worldSelectPrompt = true;
                        }
                    }
                    else
                    {
                        FileBasedPrefs.SetWorldName(worldName);
                        worldSelectPrompt = true;
                    }
                }
                else
                {
                    if (PlayerPrefsX.GetPersistentBool("multiplayer") == true)
                    {
                        if (hosting == false)
                        {
                            Timing.RunCoroutine(DownloadWorld());
                        }
                        else
                        {
                            FileBasedPrefs.SetWorldName(worldName);
                            PreStart();
                        }
                    }
                    else
                    {
                        FileBasedPrefs.SetWorldName(worldName);
                        PreStart();
                    }
                }
            }
            else if (worldList.Contains(worldName))
            {
                if (PlayerPrefsX.GetPersistentBool("multiplayer") == true && hosting == false)
                {
                    Timing.RunCoroutine(DownloadWorld());
                }
                else
                {
                    FileBasedPrefs.SetWorldName(worldName);
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

    //! Called when Gliese 876 or Kepler-452b is selected and the scene needs to be changed.
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
        && publicPrompt == false
        && passwordPrompt == false
        && userNamePrompt == false;
    }


    //! Returns true if the world is currently being loaded.
    private bool LoadingWorld()
    {
        if (stateManager.gameObject.GetComponent<TerrainGenerator>() != null)
        {
            if (stateManager.gameObject.GetComponent<TerrainGenerator>().initialized == false)
            {
                return true;
            }
        }

        return stateManager.worldLoaded == false || stateManager.GetComponent<GameManager>().combiningBlocks == true;
    }

    //! Returns true when the world has been loaded.
    private bool LoadingComplete()
    {
        return stateManager.blockProgress >= stateManager.blockIdList.Length && 
        stateManager.machineProgress >= stateManager.machineIdList.Length && 
        stateManager.currentMachine >= stateManager.totalMachines;
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

        Rect serverListBackgroundRect = new Rect((ScreenWidth * 0.01f), (ScreenHeight * 0.01f), (ScreenWidth * 0.23f), (ScreenHeight * 0.9f));
        Rect serverListRect = new Rect((ScreenWidth * 0.015f), (ScreenHeight * 0.018f), (ScreenWidth * 0.22f), (ScreenHeight * 0.88f));

        Rect deletePromptLabelRect = new Rect((ScreenWidth * 0.435f), (ScreenHeight * 0.18f), (ScreenWidth * 0.20f), (ScreenHeight * 0.05f));
        Rect deletePromptButton1Rect = new Rect((ScreenWidth * 0.39f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect deletePromptButton2Rect = new Rect((ScreenWidth * 0.51f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));

        Rect scenePromptBackgroundRect = new Rect(ScreenWidth * 0.22f, ScreenHeight * 0.14f, ScreenWidth * 0.54f, ScreenHeight * 0.20f);
        Rect scenePromptButton1Rect = new Rect((ScreenWidth * 0.26f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect scenePromptButton2Rect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect scenePromptButton3Rect = new Rect((ScreenWidth * 0.50f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));
        Rect scenePromptButton4Rect = new Rect((ScreenWidth * 0.62f), (ScreenHeight * 0.22f), (ScreenWidth * 0.10f), (ScreenHeight * 0.05f));

        Rect sceneDescriptionBackgroundRect = new Rect((ScreenWidth * 0.04f), (ScreenHeight * 0.6f), (ScreenWidth * 0.30f), (ScreenHeight * 0.35f));
        Rect scenePreviewRect = new Rect((ScreenWidth * 0.64f), (ScreenHeight * 0.6f), (ScreenWidth * 0.30f), (ScreenHeight * 0.35f));
        Rect sceneDescriptionRect = new Rect((ScreenWidth * 0.08f), (ScreenHeight * 0.64f), (ScreenWidth * 0.22f), (ScreenHeight * 0.31f));

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
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                GUI.Label(worldListTitleRect, "SAVED WORLDS");
                GUI.skin.label.fontSize = f;

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
                GUI.DrawTexture(scenePromptBackgroundRect, worldListBackground);
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

                if (GUI.Button(scenePromptButton4Rect, "Procedural"))
                {
                    buttonSounds.Play();
                    scene = 4;
                    FileBasedPrefs.SetInt(worldName + "scene", scene);
                    ChangeScene();
                }

                GUI.skin.label.fontSize = 14;

                if (scenePromptButton1Rect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(sceneDescriptionBackgroundRect, guiBackground);
                    GUI.DrawTexture(scenePreviewRect, scene1);
                    GUI.Label(sceneDescriptionRect, "A moon similar to Earth's moon, devoid of flora and fauna with no atmosphere.");
                }

                if (scenePromptButton2Rect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(sceneDescriptionBackgroundRect, guiBackground);
                    GUI.DrawTexture(scenePreviewRect, scene2);
                    GUI.Label(sceneDescriptionRect, "A planetary location similar to Earth in an area with a temperate climate.");
                }

                if (scenePromptButton3Rect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(sceneDescriptionBackgroundRect, guiBackground);
                    GUI.DrawTexture(scenePreviewRect, scene3);
                    GUI.Label(sceneDescriptionRect, "An atmospheric, alien planet with unusual flora and a colorful landscape.");
                }

                if (scenePromptButton4Rect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(sceneDescriptionBackgroundRect, guiBackground);
                    GUI.DrawTexture(scenePreviewRect, scene4);
                    GUI.Label(sceneDescriptionRect, "A jungle environment with voxel terrain. Ores and other resources spawn underground and can only be reached by digging.");
                }

                GUI.skin.label.fontSize = f;

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
                    string saveDataPath = Path.Combine(Application.persistentDataPath, "SaveData");
                    Directory.CreateDirectory(saveDataPath);
                    string savFile = "SaveData/" + worldName + ".sav";
                    string savPath = Path.Combine(Application.persistentDataPath, savFile);
                    string backupPath = Path.Combine(Application.persistentDataPath, "SaveData" + "/" + worldName);
                    Directory.CreateDirectory(backupPath);
                    File.Delete(savPath);
                    Directory.Delete(backupPath, true);
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
                    buttonSounds.Play();
                    hosting = true;
                    PlayerPrefsX.SetPersistentBool("hosting", true);
                    multiplayerPrompt = false;
                    localPrompt = true;
                }
                if (GUI.Button(deletePromptButton2Rect, "Join"))
                {
                    buttonSounds.Play();
                    hosting = false;
                    PlayerPrefsX.SetPersistentBool("hosting", false);
                    PlayerPrefs.SetString("ip", GetExternalAddress());
                    GetServerList();
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
                    buttonSounds.Play();
                    local = true;
                    localPrompt = false;
                    userNamePrompt = true;
                    worldName = local == true ? GetLocalAddress() : GetExternalAddress();
                    PlayerPrefs.SetString("ip", GetLocalAddress());
                    PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");                
                    PlayerPrefsX.SetPersistentBool("announce", false);
                    StartServer();
                }
                if (GUI.Button(deletePromptButton2Rect, "Online"))
                {
                    buttonSounds.Play();
                    local = false;
                    localPrompt = false;
                    publicPrompt = true;
                    worldName = local == true ? GetLocalAddress() : GetExternalAddress();
                    PlayerPrefs.SetString("ip", GetExternalAddress());
                    PlayerPrefs.SetString("serverURL", "http://" + worldName + ":5000");
                    StartServer();
                }
            }

            if (publicPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Multiplayer.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Multiplayer");
                GUI.skin.label.fontSize = f;
                if (GUI.Button(deletePromptButton1Rect, "Public"))
                {
                    buttonSounds.Play();
                    publicPrompt = false;
                    userNamePrompt = true;
                    PlayerPrefsX.SetPersistentBool("announce", true);
                }
                if (GUI.Button(deletePromptButton2Rect, "Private"))
                {
                    buttonSounds.Play();
                    publicPrompt = false;
                    userNamePrompt = true;
                    PlayerPrefsX.SetPersistentBool("announce", false);
                }
            }

            if (networkAddressPrompt == true)
            {
                GUI.DrawTexture(serverListBackgroundRect, worldListBackground);
                int textAreaFontSize = GUI.skin.textArea.fontSize;
                GUI.skin.textArea.fontSize = 18;
                string[] actualList;
                string displayList = "       Public Servers\n\n";
                try
                {
                    if (serverList.Split(':').Length > 1)
                    {
                        actualList = serverList.Split(':')[1].Split('[');
                        foreach (string serverEntry in actualList)
                        {
                            string serverInfo = serverEntry.Split(']')[0].Replace("\"","").Trim();
                            if (serverInfo != "")
                            {
                                displayList += serverInfo + " players\n\n";
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    UnityEngine.Debug.Log("Error reading server list: " + e.Message);
                }
                GUI.TextArea(serverListRect, displayList);
                GUI.skin.textArea.fontSize = textAreaFontSize;

                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                Vector2 size = GetStringSize("Enter server IP address.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Enter server IP address.");
                GUI.skin.label.fontSize = f;
                serverURL = GUI.TextField(textEntryRect, serverURL, 30);
                if (GUI.Button(scenePromptButton3Rect, "OK"))
                {
                    buttonSounds.Play();
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
                    buttonSounds.Play();
                    if (username != "" && username != "Enter user name.")
                    {
                        PlayerPrefs.SetString("UserName", username);
                        userNamePrompt = false;
                        passwordPrompt = true;
                    }
                }
            }

            if (passwordPrompt == true)
            {
                GUI.DrawTexture(promptBackgroundRect, guiBackground);
                int f = GUI.skin.label.fontSize;
                GUI.skin.label.fontSize = 14;
                Vector2 size = GetStringSize("Enter password.");
                Rect messagePos = new Rect((Screen.width / 2) - (size.x / 2.2f), ScreenHeight * 0.18f, size.x, size.y);
                GUI.Label(messagePos, "Enter password.");
                GUI.skin.label.fontSize = f;
                password = GUI.TextField(textEntryRect, password, 30);
                if (GUI.Button(scenePromptButton3Rect, "OK"))
                {
                    buttonSounds.Play();
                    if (password != "" && password != "Enter password.")
                    {
                        PlayerPrefs.SetString("password", password);
                        passwordPrompt = false;
                        playerColorPrompt = true;
                    }
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

            if (downloadingWorld == true)
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
                string loadingMessage = "Downloading world...";
                Vector2 size = GetStringSize(loadingMessage);
                Rect messagePos = new Rect((Screen.width / 2) - (size.x/2), Screen.height * 0.4f, size.x, size.y);
                GUI.Label(messagePos, loadingMessage);
                GUI.color = Color.white;
                GUI.skin.label.fontSize = f;
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

            string loadingMessage = "Loading... " + stateManager.blockProgress + "/" + stateManager.blockIdList.Length;

            if (stateManager.blockProgress > 0 && stateManager.blockIdList.Length > 0 && stateManager.blockProgress >= stateManager.blockIdList.Length)
            {
                loadingMessage = "Loading machines... " + stateManager.machineProgress + "/" + stateManager.machineIdList.Length;
            }

            if (stateManager.machineProgress > 0 && stateManager.machineIdList.Length > 0 && stateManager.machineProgress >= stateManager.machineIdList.Length)
            {
                loadingMessage = "Initializing machines... " + stateManager.currentMachine + "/" + stateManager.totalMachines;
            }
            
            if (stateManager.worldLoaded == true && LoadingComplete() == true)
            {
                TerrainGenerator tg = stateManager.gameObject.GetComponent<TerrainGenerator>();
                if (tg != null)
                {
                    if (tg.initialized == false)
                    {
                        loadingMessage = "Generating terrain.. " + tg.generated + "/" + tg.total;
                    }
                }
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