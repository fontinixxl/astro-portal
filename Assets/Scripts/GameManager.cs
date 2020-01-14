using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool playerCanMove = false;

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
        UIManager.instance.ShowMenu();
        // pause game untill the start button is click
        instance.PauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape")){
            instance.PauseGame();
            UIManager.instance.ShowMenu();
        }
    }

    public void ResumeGameplay()
    {
        instance.ResumeGame();
        UIManager.instance.HideMenu();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

}
