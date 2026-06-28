using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuScreen;
    public Button PauseButton;
    public GameObject GameOverScreen;

    void Update()
    {
        if (PauseMenuScreen.activeSelf || GameOverScreen.activeSelf)
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
