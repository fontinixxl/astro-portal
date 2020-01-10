using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas canvas;

    void Awake()
    {
    }

    public void ShowMenu()
    {
        canvas.enabled = true;
    }
    
    public void HideMenu()
    {
        canvas.enabled = false;
    }
    public void playGame()
    {
        if (canvas.enabled)
            GameManager.instance.ResumeGameplay();
    }
}
