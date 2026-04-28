using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceForceY = 5f;
    public float bounceForceX = 2f;
    public int dmg = 1;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        handlePlayerBounce(collision.gameObject);
    //    }
    //}

    public void handlePlayerBounce(GameObject player)
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            Vector2 bounceDirection = (player.transform.position - transform.position).normalized;
            pm.ApplyKnockback(new Vector2(bounceDirection.x * bounceForceX, bounceForceY));
        }
    }
}
