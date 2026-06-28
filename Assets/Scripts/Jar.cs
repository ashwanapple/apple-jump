using System;
using UnityEngine;

public class Jar : MonoBehaviour, Item
{
    public static event Action<int> OnJarCollect;
    public static int worth = 1;
    private bool isCollected = false;

    void Start()
    {
        GameController.OnReset += ResetItems;
    }

    void OnDestroy()
    {
        GameController.OnReset -= ResetItems;
    }

    public void Collect()
    {
        if (!isCollected)
        {
            OnJarCollect.Invoke(worth);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            isCollected = true;
        }
        
    }


    void ResetItems()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isCollected = false;
    }
}
