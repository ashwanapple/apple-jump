using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    Rigidbody2D rb;
    GameObject player;

    public int bulletDmg = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            bulletSpeed *= -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(bulletSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(bulletDmg);

            Destroy(gameObject);
        }
    }
}
