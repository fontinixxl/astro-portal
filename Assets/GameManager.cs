using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MainMenuController mainMenuController;
    
    // [HideInInspector]
    public bool playerCanMove = false;

    // public Canvas canvas;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.playerCanMove = true;
    }

    void Start()
    {
        instance.playerCanMove = true;
        // show menu
        mainMenuController.ShowMenu();
        // pause game untill the start button is click
        instance.PauseGame();
    }

    private void Update()
    {
        // if (Input.GetKeyDown("escape")){
        //     TogglePauseMenu();
        // }
    }

    public void ResumeGameplay()
    {
        instance.ResumeGame();
        mainMenuController.HideMenu();
        // instance.playerCanMove = !instance.playerCanMove;
    }

    public void PauseGame()
    {
        // instance.playerCanMove = false;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        // instance.playerCanMove = true;
        Time.timeScale = 1.0f;
    }

}
