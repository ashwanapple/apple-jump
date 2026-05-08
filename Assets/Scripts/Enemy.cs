using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, Damagers
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    [Header("Worm Health")]
    public int maxHealth = 3;
    private int currentHealth;
    public bool isDead;

    [Header("Hit Effects")]
    public float bounceForceX = 3f;
    public int dmg = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointA.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Vector2 point = currentPoint.position - transform.position; // gives direction to be direction of point
            if (currentPoint == pointA.transform)
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }


            // handles switching direction
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                flipSprite();
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                flipSprite();
                currentPoint = pointB.transform;
            }
        }
        

        anim.SetBool("isDead", isDead);
    }

    private void flipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void handlePlayerBounce(GameObject player)
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        PlayerHealth ph = GetComponent<PlayerHealth>();

        if (ph != null && ph.isDead) return;

        if (pm != null)
        {
            Vector2 bounceDirection = (player.transform.position - transform.position).normalized;
            pm.ApplyKnockback(new Vector2(bounceDirection.x * bounceForceX, player.transform.position.y));
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        StartCoroutine(FreezeDeath());
    }

    private IEnumerator FreezeDeath()
    {
        yield return null;

        while (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            yield return null;
        }

        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        anim.speed = 0f;

    }
}
