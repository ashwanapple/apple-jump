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

        if (trap != null)
        {
            if (trap.CompareTag("Spike") || trap.CompareTag("Knife"))
            {
                TakeDamage(trap.dmg, trap);
                trap.handlePlayerBounce(gameObject);
            }
            else
            {
                TakeDamage(maxHealth, trap); // player is boiled, instant death
            }

        }
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

        }
    }
}
