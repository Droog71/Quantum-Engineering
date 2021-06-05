using UnityEngine;
using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;

public class ProtectionBlock : Machine
{
    public string ID = "unassigned";
    public bool visible;
    public Material lineMat;
    public GameObject connectionObject;
    public string creationMethod = "built";
    private List<string> userNames;
    private StateManager stateManager;
    private LineRenderer[] lines;
    private Vector3[] vectors;
    private Vector3 pos;
    private bool init;
    private int connectionAttempts;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        visible = true;
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        if (stateManager.worldLoaded == true && ID != "unassigned" && init == false && creationMethod == "built")
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            float distance = Vector3.Distance(transform.position, player.transform.position);
            string userName = PlayerPrefs.GetString("UserName");
            if (distance <= 160 && userName != stateManager.worldName)
            {
                AddUser(userName);
                init = true;
            }

            NetworkPlayer[] networkPlayers = FindObjectsOfType<NetworkPlayer>();
            if (networkPlayers != null)
            {
                foreach (NetworkPlayer netWorkPlayer in networkPlayers)
                {
                    if (netWorkPlayer != null)
                    {
                        float networkPlayerDistance = Vector3.Distance(netWorkPlayer.transform.position, transform.position);
                        if (networkPlayerDistance <= 160)
                        {
                            AddUser(netWorkPlayer.gameObject.name);
                            string[] commandLineOptions = Environment.GetCommandLineArgs();
                            if (commandLineOptions.Contains("-batchmode") && ID.Length > stateManager.worldName.Length)
                            {
                                string logID = ID.Substring(stateManager.worldName.Length);
                                UnityEngine.Debug.Log("Adding user " + netWorkPlayer.gameObject.name + " to " + logID);
                            }
                            init = true;
                        }
                    }
                }
            }

            connectionAttempts++;
            if (connectionAttempts >= 128)
            {
                Destroy(gameObject);
            }
        }

        if (vectors == null)
        {
            vectors = new Vector3[8];
        }

        if (lines == null)
        {
            lines = new LineRenderer[12];
            for (int i = 0; i < lines.Length; i++)
            {
                GameObject line = Instantiate(connectionObject, transform.position, transform.rotation);
                line.transform.parent = transform;
                line.SetActive(true);
                lines[i] = line.AddComponent<LineRenderer>();
                lines[i].startWidth = 0.2f;
                lines[i].endWidth = 0.2f;
                lines[i].material = lineMat;
                lines[i].loop = true;
                lines[i].enabled = true;
            }
        }

        if (vectors != null && lines != null)
        {
            pos = transform.position;

            vectors[0] = new Vector3(pos.x - 100, pos.y - 600, pos.z - 100);
            vectors[1] = new Vector3(pos.x + 100, pos.y - 600, pos.z + 100);
            vectors[2] = new Vector3(pos.x - 100, pos.y - 600, pos.z + 100);
            vectors[3] = new Vector3(pos.x + 100, pos.y - 600, pos.z - 100);
            vectors[4] = new Vector3(pos.x - 100, pos.y + 600, pos.z - 100);
            vectors[5] = new Vector3(pos.x + 100, pos.y + 600, pos.z + 100);
            vectors[6] = new Vector3(pos.x - 100, pos.y + 600, pos.z + 100);
            vectors[7] = new Vector3(pos.x + 100, pos.y + 600, pos.z - 100);

            lines[0].SetPosition(0, vectors[0]);
            lines[0].SetPosition(1, vectors[2]);

            lines[1].SetPosition(0, vectors[0]);
            lines[1].SetPosition(1, vectors[3]);

            lines[2].SetPosition(0, vectors[0]);
            lines[2].SetPosition(1, vectors[4]);

            lines[3].SetPosition(0, vectors[1]);
            lines[3].SetPosition(1, vectors[2]);

            lines[4].SetPosition(0, vectors[1]);
            lines[4].SetPosition(1, vectors[3]);

            lines[5].SetPosition(0, vectors[1]);
            lines[5].SetPosition(1, vectors[5]);

            lines[6].SetPosition(0, vectors[4]);
            lines[6].SetPosition(1, vectors[6]);

            lines[7].SetPosition(0, vectors[4]);
            lines[7].SetPosition(1, vectors[7]);

            lines[8].SetPosition(0, vectors[5]);
            lines[8].SetPosition(1, vectors[6]);

            lines[9].SetPosition(0, vectors[5]);
            lines[9].SetPosition(1, vectors[7]);

            lines[10].SetPosition(0, vectors[2]);
            lines[10].SetPosition(1, vectors[6]);

            lines[11].SetPosition(0, vectors[3]);
            lines[11].SetPosition(1, vectors[7]);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].enabled = visible;
            }
        }
    }

    //! Adds a user to the list of authorized users for this protection block.
    public void AddUser(string userName)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            if (userNames == null)
            {
                userNames = new List<string>();
            }
            userNames.Add(userName);
        }
    }

    //! Adds a user to the list of authorized users for this protection block.
    public List<string> GetUserNames()
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            return userNames;
        }
        return null;
    }

    //! Adds a user to the list of authorized users for this protection block.
    public void SetUserNames(List<string> names)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            userNames = names;
        }
    }

    //! Returns true if the player is an authorized user for this protection block.
    public bool IsAuthorizedUser(string userName)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            return userNames.Contains(userName);
        }
        return false;
    }

    //! Returns true if the method is called from a valid, authorized class.
    private bool IsValidCaller(StackFrame frame)
    {
        string methodName = frame.GetMethod().Name;
        string className = frame.GetMethod().DeclaringType.Name;
        string assembly = frame.GetMethod().DeclaringType.Assembly.ToString().Split(',')[0];

        if (methodName == "MoveNext" && className == "<LoadWorld>d__71" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        if (methodName == "MoveNext" && className == "<SaveDataCoroutine>d__4" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        if (methodName == "UpdateMachine" && className == "ProtectionBlock" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        if (methodName == "CanBuild" && className == "BuildController" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        if (methodName == "CanInteract" && className == "InteractionController" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        if (methodName == "InteractWithProtectionBlock" && className == "MachineInteraction" && assembly == "Assembly-CSharp")
        {
            return true;
        }

        return false;
    }
}
