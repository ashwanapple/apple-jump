using UnityEngine;

public class Knife : MonoBehaviour
{
    bool hitGround;
    public float gravity = 2f;
    Rigidbody2D rb;

    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        if (!hitGround)
        {
            rb.gravityScale = gravity;

        } else
        {
            Debug.Log("Hit Ground!");
            Destroy(gameObject);
            
        }
    }

    private void GroundCheck()
    {
        hitGround = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }



}
