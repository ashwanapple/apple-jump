using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    
    public Button PauseButton;
    public GameObject PauseMenuScreen;
    public GameObject GameOverScreen;
    public GameObject LevelCompleteScreen;

    void Update()
    {
        if (PauseMenuScreen.activeSelf || GameOverScreen.activeSelf || LevelCompleteScreen.activeSelf)
        {
            PauseButton.interactable = false;
        }
        else
        {
            PauseButton.interactable = true;
        }
    }


    public void Pause()
    {
       PauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Levels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelsScene");
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
