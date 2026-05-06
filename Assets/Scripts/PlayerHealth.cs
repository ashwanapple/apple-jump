using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Animator animator;

    private bool isBoiled;
    private bool isPeeled;
    private bool isSliced;

    public bool isDead => currentHealth <= 0;

    public static event Action onPlayerDied;

    public HealthUI healthUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);

        GameController.OnReset += ResetPlayer;
    }

    void Update()
    {
        animator.SetBool("isBoiled", isBoiled);
        animator.SetBool("isPeeled", isPeeled);
        animator.SetBool("isSliced", isSliced);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("HurtBox"))
        {
            return;
        }

        Trap trap = collision.GetComponent<Trap>();
        Enemy enemy = collision.GetComponent<Enemy>();

        if (trap != null)
        {
            if (trap.CompareTag("Spike") || trap.CompareTag("Knife"))
            {
                TakeDamage(trap.dmg, trap);
                if (currentHealth > 0)
                {
                    trap.handlePlayerBounce(gameObject);
                }
                
            }
            else
            {
                TakeDamage(maxHealth, trap); // player is boiled, instant death
            }

        }
        if (enemy)
        {
            TakeDamage(enemy.dmg, null);
            enemy.handlePlayerBounce(gameObject);
        }
    }

    private void TakeDamage(int dmg, Trap trap)
    {
        currentHealth -= dmg;
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            if (trap == null)
            {
                Debug.Log("Worm hit!");
                
            }
            else if (trap.CompareTag("Spike"))
            {
                isSliced = true;
            }
            else if (trap.CompareTag("Knife"))
            {
                isPeeled = true;
            }
            else
            {
                isBoiled = true;
            }

            Die();

            onPlayerDied.Invoke();
        }
    }

    private void Die()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(FreezeDeath());
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;
        healthUI.UpdateHealth(maxHealth);

        isBoiled = false;
        isPeeled = false;
        isSliced = false;

        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;

        StopAllCoroutines();
        animator.speed = 1f;
        animator.Play("Idle");

        GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator FreezeDeath()
    {
        yield return null;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.speed = 0f;
    }
}
