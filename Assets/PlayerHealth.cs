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
            TakeDamage(trap.dmg);
            trap.handlePlayerBounce(gameObject);
        }
    }

    private void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        healthUI.UpdateHealth(currentHealth);

        if(currentHealth <= 0)
        {
            // player dead
            isSliced = true;
       
            //isBoiled = true;

        }
    }
}
