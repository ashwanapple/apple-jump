using UnityEngine;

public class ExitToNextLevel : MonoBehaviour
{
    public GameController gameController;
    private bool complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !complete)
        {
            complete = true;
            gameController.LoadNextLevel();
        }
    }

    private void OnEnable()
    {
        complete = false;
    }
}
