using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuScreen;

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
