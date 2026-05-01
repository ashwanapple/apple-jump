using UnityEngine;

public class ExitToNextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Exit>() != null)
        {
            // do something
            Debug.Log("Finished Level");
        }
    }
}
