using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Reflection;
using System.Collections;

public class PluginLoader :MonoBehaviour
{
    private Assembly assembly;

    //! Adds plugins from mods to the game.
    public void Start()
    {
        string modPath = Path.Combine(Application.persistentDataPath, "Mods");
        Directory.CreateDirectory(modPath);
        string[] modDirs = Directory.GetDirectories(modPath);
        foreach (string path in modDirs)
        {
            string pluginPath = path + "/Plugins/";
            Directory.CreateDirectory(pluginPath);
            DirectoryInfo d = new DirectoryInfo(pluginPath);
            foreach (FileInfo file in d.GetFiles("*.dll"))
            {
                string filePath = pluginPath + file.Name;
                string pluginName = file.Name.Remove(file.Name.Length - 4);
                LoadPlugin(pluginName, filePath);
                Debug.Log("Loading mod plugin: " + pluginName);
            }
        }
    }

    //! Starts the assembly loading coroutine.
    private void LoadPlugin(string pluginName, string url)
    {
        StartCoroutine(LoadAssembly(pluginName, url));
    }

    //! Uses GetAssembly to load classes from the dll and add them to a dictionary.
    private IEnumerator LoadAssembly(string pluginName, string url)
    {
        if(url == null)
        {
            yield break;
        }
        UriBuilder uriBuildier = new UriBuilder(url) { Scheme = "file" };
        using (UnityWebRequest uwr = UnityWebRequest.Get(uriBuildier.ToString()))
        {
            yield return uwr.SendWebRequest();
            if (!uwr.isNetworkError && !uwr.isHttpError)
            {
                assembly = GetAssembly(uwr);
            }
        }
        if (assembly != null)
        {
            AddPlugin(assembly, pluginName);
        }
    }

    //! Loads assembly from file.
    private Assembly GetAssembly(UnityWebRequest uwr)
    {
        if (uwr != null)
        {
            if (uwr.downloadHandler != null)
            {
                if (uwr.downloadHandler.data != null)
                {
                    return Assembly.Load(uwr.downloadHandler.data);
                }
            }
        }
        return null;
    }

    //! Gets type by name from dll.
    private void AddPlugin(Assembly dll, string pluginName)
    {
        Type type = dll.GetType(pluginName);
        if (type != null)
        {
            GameObject obj = new GameObject { name = pluginName };
            obj.AddComponent(type);
        }
    }
}