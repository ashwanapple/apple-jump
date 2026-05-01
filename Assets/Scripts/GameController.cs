using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressNum;
    public Slider progressSlider;

    public GameObject player;
    //public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressNum = 0;
        progressSlider.value = 0;
        Jar.OnJarCollect += IncreaseProgressAmount;
        // load next level
        //LoadCanvas.SetActive(false);
    }

    void IncreaseProgressAmount(int amount)
    {
        progressNum += amount;
        progressSlider.value = progressNum;
        if (progressNum >= 3)
        {
            // Complete
            //LoadCanvas.SetActive(true);
        }
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1;
        //LoadCanvas.SetActive(false);

        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[nextLevelIndex].gameObject.SetActive(true);

        currentLevelIndex = nextLevelIndex;

        ResetLevel();
    }

    void ResetLevel()
    {
        player.transform.position = new Vector3(0, 0, 0);
        progressNum = 0;
        progressSlider.value = 0;
        
    }
}
