using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressNum;
    public Slider progressSlider;

    public GameObject player;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(i == 0); // first level is active
        }
        progressNum = 0;
        progressSlider.value = 0;
        progressSlider.maxValue = 3;
        //Jar.OnJarCollect += IncreaseProgressAmount;
        
    }

    void Awake()
    {
        Jar.OnJarCollect += IncreaseProgressAmount;
    }

    void OnDestroy()
    {
        Jar.OnJarCollect -= IncreaseProgressAmount;
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

    public void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1;

        levels[currentLevelIndex].SetActive(false);
        currentLevelIndex = nextLevelIndex;
        levels[nextLevelIndex].SetActive(true);

        ResetLevel();
    }

    public void ResetLevel()
    {
        player.transform.position = new Vector3(-1, 0, 0);
        progressNum = 0;
        progressSlider.value = 0;
        
    }
}
