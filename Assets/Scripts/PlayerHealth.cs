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

    public HealthUI healthUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        animator.SetBool("isBoiled", isBoiled);
        animator.SetBool("isPeeled", isPeeled);
        animator.SetBool("isSliced", isSliced);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trap trap = collision.GetComponent<Trap>();
        //Enemy enemy = collision.GetComponent<Enemy>();

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
        //if (enemy)
        //{
        //    TakeDamage(enemy.dmg, null); 
        //}
    }

    private void TakeDamage(int dmg, Trap trap)
    {
        currentHealth -= dmg;
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            if (trap.CompareTag("Spike"))
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
