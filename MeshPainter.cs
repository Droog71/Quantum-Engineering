﻿using System.Collections;
using UnityEngine;

public class MeshPainter : MonoBehaviour
{
    public int ID;
    private Coroutine saveDataCoRoutine;

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        string worldName = FindObjectOfType<StateManager>().worldName;
        bool paintedIron = false;
        bool paintedGlass = false;
        bool paintedBrick = false;
        bool paintedSteel = false;
        if (gameObject.name.Equals("ironHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "ironHolder" + ID + "painted") == true)
            {
                float r = FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Red");
                float g = FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Green");
                float b = FileBasedPrefs.GetFloat(worldName + "ironHolder" + ID + "Blue");
                GetComponent<Renderer>().material.color = new Color(r, g, b);
                if (paintedIron == false)
                {
                    Transform[] allBuiltObjects = FindObjectOfType<GameManager>().builtObjects.GetComponentsInChildren<Transform>(true);
                    foreach (Transform block in allBuiltObjects)
                    {
                        if (block.GetComponent<IronBlock>() != null)
                        {
                            block.GetComponent<Renderer>().material.color = new Color(r, g, b);
                        }
                    }
                }
            }
        }
        if (gameObject.name.Equals("steelHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "steelHolder" + ID + "painted") == true)
            {
                float r = FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Red");
                float g = FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Green");
                float b = FileBasedPrefs.GetFloat(worldName + "steelHolder" + ID + "Blue");
                GetComponent<Renderer>().material.color = new Color(r, g, b);
                if (paintedSteel == false)
                {
                    Transform[] allBuiltObjects = FindObjectOfType<GameManager>().builtObjects.GetComponentsInChildren<Transform>(true);
                    foreach (Transform block in allBuiltObjects)
                    {
                        if (block.GetComponent<Steel>() != null)
                        {
                            block.GetComponent<Renderer>().material.color = new Color(r, g, b);
                        }
                    }
                }
            }
        }
        if (gameObject.name.Equals("brickHolder(Clone)"))
        {
            if (FileBasedPrefs.GetBool(worldName + "brickHolder" + ID + "painted") == true)
            {
                float r = FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Red");
                float g = FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Green");
                float b = FileBasedPrefs.GetFloat(worldName + "brickHolder" + ID + "Blue");
                GetComponent<Renderer>().material.color = new Color(r, g, b);
                if (paintedBrick == false)
                {
                    Transform[] allBuiltObjects = FindObjectOfType<GameManager>().builtObjects.GetComponentsInChildren<Transform>(true);
                    foreach (Transform block in allBuiltObjects)
                    {
                        if (block.GetComponent<Brick>() != null)
                        {
                            block.GetComponent<Renderer>().material.color = new Color(r, g, b);
                        }
                    }
                }
            }
       }
       if (gameObject.name.Equals("glassHolder(Clone)"))
       {
            if (FileBasedPrefs.GetBool(worldName + "glassHolder" + ID + "painted") == true)
            {
                float r = FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Red");
                float g = FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Green");
                float b = FileBasedPrefs.GetFloat(worldName + "glassHolder" + ID + "Blue");
                GetComponent<Renderer>().material.color = new Color(r, g, b);
                if (paintedGlass == false)
                {
                    Transform[] allBuiltObjects = FindObjectOfType<GameManager>().builtObjects.GetComponentsInChildren<Transform>(true);
                    foreach (Transform block in allBuiltObjects)
                    {
                        if (block.GetComponent<Glass>() != null)
                        {
                            block.GetComponent<Renderer>().material.color = new Color(r, g, b);
                        }
                    }
                }
            }
        }
    }

    //! Saves mesh colors.
    public void SaveData()
    {
        saveDataCoRoutine = StartCoroutine(SaveDataCoRoutine());
    }

    //! Saves the color of painted objects.
    private IEnumerator SaveDataCoRoutine()
    {
        string worldName = GameObject.Find("GameManager").GetComponent<StateManager>().worldName;
        if (gameObject.name.Equals("ironHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "ironHolder" + ID + "painted") == true)
        {
            float r = GetComponent<Renderer>().material.color.r;
            float g = GetComponent<Renderer>().material.color.g;
            float b = GetComponent<Renderer>().material.color.b;
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Red", r);
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Green", g);
            FileBasedPrefs.SetFloat(worldName + "ironHolder" + ID + "Blue", b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("steelHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "steelHolder" + ID + "painted") == true)
        {
            float r = GetComponent<Renderer>().material.color.r;
            float g = GetComponent<Renderer>().material.color.g;
            float b = GetComponent<Renderer>().material.color.b;
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Red", r);
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Green", g);
            FileBasedPrefs.SetFloat(worldName + "steelHolder" + ID + "Blue", b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("brickHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "brickHolder" + ID + "painted") == true)
        {
            float r = GetComponent<Renderer>().material.color.r;
            float g = GetComponent<Renderer>().material.color.g;
            float b = GetComponent<Renderer>().material.color.b;
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Red", r);
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Green", g);
            FileBasedPrefs.SetFloat(worldName + "brickHolder" + ID + "Blue", b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
        if (gameObject.name.Equals("glassHolder(Clone)") && FileBasedPrefs.GetBool(worldName + "glassHolder" + ID + "painted") == true)
        {
            float r = GetComponent<Renderer>().material.color.r;
            float g = GetComponent<Renderer>().material.color.g;
            float b = GetComponent<Renderer>().material.color.b;
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Red", r);
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Green", g);
            FileBasedPrefs.SetFloat(worldName + "glassHolder" + ID + "Blue", b);
            Transform[] blocks = gameObject.GetComponentsInChildren<Transform>(true);
            int paintInterval = 0;
            foreach (Transform T in blocks)
            {
                if (T != null)
                {
                    T.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
                }
                paintInterval++;
                if (paintInterval >= 10)
                {
                    yield return null;
                    paintInterval = 0;
                }
            }
        }
    }
}