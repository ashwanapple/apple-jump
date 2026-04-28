using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    private bool isKnockback = false;
    private SpriteRenderer spriteRenderer;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }   

    // Update is called once per frame
    void Update()
    {
        if (!isKnockback)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

        }
        GroundCheck();
        Gravity();

        // flips sprite depending on direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // right
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);

    }

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; // prevents floaty falling
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                // button held down = full height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
            else if (context.canceled)
            {
                // button not held down = half height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
                // animator.SetTrigger("jump");
            }
        }
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }


    public void ApplyKnockback(Vector2 force, float duration = 0.2f)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockBackCoroutine(duration));
    }

    private IEnumerator KnockBackCoroutine(float duration)
    {
        isKnockback = true;
        yield return new WaitForSeconds(duration);
        isKnockback = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
