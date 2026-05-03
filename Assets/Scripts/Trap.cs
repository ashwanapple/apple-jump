using UnityEngine;

public class Trap : MonoBehaviour, Damagers
{
    public float bounceForceY = 5f;
    public float bounceForceX = 2f;
    public int dmg = 1;


    public void handlePlayerBounce(GameObject player)
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        PlayerHealth ph = GetComponent<PlayerHealth>();

        if (ph != null && ph.isDead) return;

        if (pm != null)
        {
            Vector2 bounceDirection = (player.transform.position - transform.position).normalized;
            pm.ApplyKnockback(new Vector2(bounceDirection.x * bounceForceX, bounceForceY));
        }
    }
}
