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

    public void Collect()
    {
        if (!isCollected)
        {
            OnJarCollect.Invoke(worth);
            gameObject.SetActive(false);
            isCollected = true;
        }
        
    }

    void ResetItems()
    {
        gameObject.SetActive(true);
        isCollected = false;
    }
}
