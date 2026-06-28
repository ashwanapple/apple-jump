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
    public GameObject completeLevelPanel;

    public List<GameObject> jars;
    public Sprite fullJarSprite;
    public Sprite emptyJarSprite;

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
        completeLevelPanel.SetActive(false);

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

    public void CompleteLevel()
    {
        for (int j = 0; j < progressNum; j++)
        {
            jars[j].GetComponent<Image>().sprite = fullJarSprite;
        }

        completeLevelPanel.SetActive(true);
        Time.timeScale = 0f;

    }

    // resets ENTIRE level
    public void ResetLevel()
    {
        gameOverScreen.SetActive(false);
        completeLevelPanel.SetActive(false);
        LoadLevel(currentLevelIndex);
    }

    public void ExitToMenu()
    {
        completeLevelPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelsScene");
    }


    void IncreaseProgressAmount(int amount)
    {
        progressNum += amount;
        progressSlider.value = progressNum;
    }

    void LoadLevel(int level)
    {
        Time.timeScale = 1f;
        completeLevelPanel.SetActive(false);
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

    // resets player and jar progress
    public void ResetLevelComponents()
    {
        for (int j = 0; j < jars.Count; j++)
        {
            jars[j].GetComponent<Image>().sprite = emptyJarSprite;
        }

        player.transform.position = new Vector3(-1, 0, 0);
        OnReset?.Invoke();
        progressNum = 0;
        progressSlider.value = 0;

    }
}
