using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour
{
    public LevelObject[] levelObjects;

    public static int currentLev;
    public static int unlockedLevels;

    public void OnClickLevel(int levelNum)
    {
        currentLev = levelNum;
        SceneManager.LoadScene("MainScene");
    }

    public void Return()
    {
        SceneManager.LoadScene("StartScene");

    }

    public void Start()
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);

        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (unlockedLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
            }
        }
    }

}
