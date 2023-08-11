using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMainMenuCanvas : MonoBehaviour
{
    private static BGMainMenuCanvas instance = null;

    public static BGMainMenuCanvas Instance
    {
        get { return instance; }
    }

    public int[] indicesToDestroy; // array of scene indices where this object should be destroyed

    void Awake()
    {
        //DontDestroyOnLoad(gameObject); // Don't destroy when loading new scenes

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject); // Don't destroy when loading new scenes

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Array.IndexOf(indicesToDestroy, scene.buildIndex) != -1)
        {
            Destroy(gameObject); // Destroy  when entering a scene in indicesToDestroy
        }
    }
}
