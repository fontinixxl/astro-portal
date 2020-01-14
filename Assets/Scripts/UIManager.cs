using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private Canvas canvas;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        canvas = GetComponentInChildren<Canvas>();
    }

    public void ShowMenu()
    {
        instance.canvas.enabled = true;
    }

    public void HideMenu()
    {
        instance.canvas.enabled = false;
    }
    public void playGame()
    {
        if (instance.canvas.enabled)
            GameManager.instance.ResumeGameplay();
    }
}
