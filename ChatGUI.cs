using UnityEngine;
using System.Collections.Generic;

public class ChatGUI : MonoBehaviour 
{
    public string messages;
    private string inputMsg = "";
    public GUISkin ChatGUIskin;
    private bool playersVisible = false;
    private PlayerController playerController;
    private NetworkController networkController;
    private Coroutine chatDataCoroutine;
    private string playersOnline;
    private int playerCount;
    private float chatNetTimer;
    Vector2 scrollPosition;

    // Use this for initialization
    void Start () 
    {
        playerController = GetComponent<PlayerController>();
    }

    //! Called once per frame by unity engine.
    public void Update()
    {
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == false || playerController.stateManager.worldLoaded == false)
        {
            return;
        }

        if (networkController != null)
        {
            chatNetTimer += 1 * Time.deltaTime;
            if (chatNetTimer >= Random.Range(0.75f, 1.0f))
            {
                chatDataCoroutine = StartCoroutine(networkController.networkReceive.ReceiveChatData());
                chatNetTimer = 0;
            }
        }
        else
        {
            networkController = playerController.networkController;
        }
    }

    //! Called by unity engine for rendering and handling GUI events.
    public void OnGUI()
    {
        if (PlayerPrefsX.GetPersistentBool("multiplayer") == false || playerController.stateManager.worldLoaded == false || playerController.GuiOpen())
        {
            return;
        }

        GUI.skin = ChatGUIskin;

        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;

        Rect textFieldRect = new Rect(0, (ScreenHeight * 0.80f), (ScreenWidth * 0.15f), (ScreenHeight * 0.03f));
        
        scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,false, GUILayout.Width(ScreenWidth * 0.4f), GUILayout.Height(ScreenHeight * 0.65f));
        GUILayout.Label(messages);
        GUILayout.EndScrollView();

        if (playerController.building == false)
        {
            GUI.SetNextControlName ("textfield");
            inputMsg = GUI.TextField (textFieldRect, inputMsg, 300);

            if (GUI.GetNameOfFocusedControl() != "textfield")
            {
                GUI.color = new Color(0.2824f, 0.7882f, 0.9569f);
                GUI.Label(textFieldRect, "  Press backspace to chat.");
                GUI.color = Color.white;
            }

            Event ev = Event.current;
            if (ev.keyCode == KeyCode.Backspace)
            {
                GUI.FocusControl ("textfield");
            }
            
            Event e = Event.current;
            if (e.keyCode == KeyCode.Return) 
            { 
                if (inputMsg != "")
                {
                    if (inputMsg == "/list" || inputMsg == "/players")
                    {
                        NetworkPlayer[] allPlayers = FindObjectsOfType<NetworkPlayer>();
                        playerCount = allPlayers.Length + 1;
                        List<string> nameList = new List<string>();
                        nameList.Add(PlayerPrefs.GetString("UserName"));
                        foreach (NetworkPlayer player in allPlayers)
                        {
                            nameList.Add(player.gameObject.name);
                        }
                        playersOnline = string.Join("\n", nameList.ToArray());
                        playersVisible = true;
                    }
                    else
                    {
                        networkController.networkSend.SendChatMessage(inputMsg);
                    }
                    inputMsg = "";
                }   
                GUIUtility.keyboardControl = 0;
            }

            if (playersVisible == true)
            {   
                messages += "\n\n"+playerCount+" players online.";
                messages += "\n"+playersOnline+"\n\n";
                playersVisible = false;
            }
        }
        
        if (messages.Length >= 500)
        {
            messages = "";
        }
        
        scrollPosition.y += 1000;
    }
}

