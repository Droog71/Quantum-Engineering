using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class ProtectionBlock : Machine
{
    public List<string> userNames;
    private List<string> passwords;
    public string ID;
    public bool visible;
    public Material lineMat;
    public GameObject connectionObject;
    public string creationMethod = "built";
    private StateManager stateManager;
    private LineRenderer[] lines;
    private Vector3[] vectors;
    private Vector3 pos;
    private bool init;

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
            if (distance <= 160)
            {
                string userName = PlayerPrefs.GetString("UserName");
                string password = PlayerPrefs.GetString("password");
                passwords = new List<string>();
                AddUser(userName, password);
            }
            init = true;
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
    public void AddUser(string userName, string password)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            passwords.Add(password);
            userNames.Add(userName);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //! Initializes the password list.
    public void SetPasswords(string[] pwArray)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            passwords = pwArray.ToList();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //! Returns true if the player is an authorized user for this protection block.
    public bool IsAuthorizedUser(string password)
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            return passwords.Contains(password);
        }
        return false;
    }

    //! Gets the list of passwords for this protection block. Used for saving and loading worlds.
    public List<string> GetPasswords()
    {
        StackFrame frame = new StackFrame(1);
        if (IsValidCaller(frame))
        {
            return passwords;
        }
        return null;
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

        return false;
    }
}
