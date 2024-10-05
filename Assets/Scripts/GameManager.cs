using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera MainCam { get; private set; }
   
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded;
            RegisterEvents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode arg1)
    {
        if (loadedScene.name == "Main")
        {
            MainCam = Camera.main;
        }
    }

    private static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void RegisterEvents()
    {
        //Debug.Log("register NewGameBTN event");
        MainMenuEvents.NewGameBTN += OnNewGameBTN;
    }

    private void OnNewGameBTN()
    {
        //Debug.Log("NewGame event");
        LoadScene("Main");
    }
}