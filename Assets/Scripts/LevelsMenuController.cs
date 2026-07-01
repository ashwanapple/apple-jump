using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour
{
    public LevelObject[] levelButtons;
    public Sprite fullJarSprite;
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
        //PlayerPrefs.DeleteAll();

        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);


        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = unlockedLevels > i;
            
            levelButtons[i].levelButton.interactable = isUnlocked;
            levelButtons[i].levelNumText.SetActive(isUnlocked);

            if (isUnlocked)
            {
                int jars = PlayerPrefs.GetInt("jars" + i.ToString(), 0);

                for (int j = 0; j < jars; j++)
                {
                    levelButtons[i].jars[j].sprite = fullJarSprite;
                }
                
            }
        }
    }

}
