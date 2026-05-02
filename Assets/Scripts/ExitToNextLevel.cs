using UnityEngine;

public class ExitToNextLevel : MonoBehaviour
{
    public GameController gameController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // do something
            Debug.Log("Finished Level");
            gameController.LoadNextLevel();
        }
    }
}
