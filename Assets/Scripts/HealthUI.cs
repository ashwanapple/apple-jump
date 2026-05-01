using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    private List<Image> hearts = new List<Image>();

    public void SetMaxHealth(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform); // create heart img in container
            newHeart.sprite = fullHealthSprite;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHealthSprite;
            }
            else
            {
                hearts[i].sprite = emptyHealthSprite;
            }
        }
    }
    
}
