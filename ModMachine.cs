using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.IO;

public class ModMachine : BasicMachine
{
    private Material material;
    private PlayerController playerController;
    private GameManager gameManager;
    private Coroutine modSoundCoroutine;
    public string machineName;
    public bool init;

    //! Called by unity engine on start up to initialize variables.
    public new void Start()
    {
        base.Start();
        material = new Material(Shader.Find("Standard"));
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //! Called by MachineManager update coroutine.
    public override void UpdateMachine()
    {
        base.UpdateMachine();
        if (!stateManager.Busy() && init == false)
        {
            BlockDictionary blockDictionary = playerController.GetComponent<BuildController>().blockDictionary;
            if (blockDictionary != null)
            {
                if (blockDictionary.meshDictionary.ContainsKey(machineName) && GetComponent<MeshFilter>() != null)
                {
                    GetComponent<MeshFilter>().mesh = blockDictionary.meshDictionary[machineName];
                }
            }
            gameManager.meshManager.SetMaterial(gameObject, machineName);
            modSoundCoroutine = StartCoroutine(GetAudioFile(this, GetComponent<AudioSource>(), machineName));
            init = true;
        }

        if (recipes == null)
        {
            recipes = GameObject.Find("Player").GetComponent<BuildController>().blockDictionary.GetMachineRecipes(machineName);
        }
    }

    //! Loads sound files.
    public static IEnumerator GetAudioFile(ModMachine machine, AudioSource source, string machineName)
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string soundPath = path + "/Sounds/";
            Directory.CreateDirectory(soundPath);
            DirectoryInfo d = new DirectoryInfo(soundPath);
            foreach (FileInfo file in d.GetFiles("*.wav"))
            {
                string filePath = soundPath + file.Name;
                UriBuilder soundUriBuildier = new UriBuilder(filePath) { Scheme = "file" };
                string url = soundUriBuildier.ToString();
                using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
                {
                    yield return uwr.SendWebRequest();
                    if (!uwr.isNetworkError && !uwr.isHttpError)
                    {
                        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(uwr);
                        string soundName = file.Name.Remove(file.Name.Length - 4);
                        string modName = new DirectoryInfo(path).Name;
                        if (audioClip != null)
                        {
                            if (soundName == machineName)
                            {
                                audioClip.name = soundName;
                                source.clip = audioClip;
                                machine.hasCustomSound = true;
                                Debug.Log("Mod "+"["+modName+"]"+" loaded sound effect [" + soundName + ".wav] "+"for "+machineName);
                            }                           
                        }
                        else
                        {
                            Debug.Log("Mod "+"["+modName+"]"+" failed to load sound effect [" + soundName + ".wav] "+"for "+machineName);
                        }
                    }
                }
            }
        }
    }
}