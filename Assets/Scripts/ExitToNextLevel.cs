using UnityEngine;

public class ExitToNextLevel : MonoBehaviour
{
    public GameController gameController;
    private bool complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !complete)
        {
  
            if (GameController.progressNum > PlayerPrefs.GetInt("jars" + LevelsMenuController.currentLev.ToString(), 0))
            {
                PlayerPrefs.SetInt("jars" + LevelsMenuController.currentLev.ToString(), GameController.progressNum);
            }

            LevelsMenuController.unlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelsMenuController.unlockedLevels);

            complete = true;
            gameController.LoadNextLevel();
        }
    }

    private void OnEnable()
    {
        complete = false;
    }
}
