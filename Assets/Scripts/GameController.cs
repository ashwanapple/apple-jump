using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int progressNum;
    public Slider progressSlider;

    public GameObject player;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;

    public static event Action OnReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevelIndex = LevelsMenuController.currentLev;

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(i == currentLevelIndex);

        }

        progressNum = 0;
        progressSlider.value = 0;
        progressSlider.maxValue = 3;
        gameOverScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        PlayerHealth.onPlayerDied += GameOverScreen;
        Jar.OnJarCollect += IncreaseProgressAmount;

    }

    void OnDestroy()
    {
        PlayerHealth.onPlayerDied -= GameOverScreen;
        Jar.OnJarCollect -= IncreaseProgressAmount;
    }


    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    // resets ENTIRE level
    public void ResetLevel()
    {
        gameOverScreen.SetActive(false);
        LoadLevel(currentLevelIndex);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("LevelsScene");
    }


    void IncreaseProgressAmount(int amount)
    {
        progressNum += amount;
        progressSlider.value = progressNum;
        //if (progressNum >= 3)
        //{
        //    // mark as bonus all collected at the end? or for main menu
        //}
    }

    void LoadLevel(int level)
    {
        levels[currentLevelIndex].SetActive(false);
        currentLevelIndex = level;
        levels[level].SetActive(true);

        ResetLevelComponents();
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1;
        LoadLevel(nextLevelIndex);
        
    }

    // resets player and progress bar
    public void ResetLevelComponents()
    {
        player.transform.position = new Vector3(-1, 0, 0);
        OnReset?.Invoke();
        progressNum = 0;
        progressSlider.value = 0;

    }
}
