using System;
using UnityEngine;

public class Jar : MonoBehaviour, Item
{
    public static event Action<int> OnJarCollect;
    public static int worth = 1;
    private bool isCollected = false;


    public void Collect()
    {
        if (!isCollected)
        {
            OnJarCollect.Invoke(worth);
            gameObject.SetActive(false);
            isCollected = true;
        }
        
    }

    void OnEnable()
    {
        GameController.OnReset += ResetItems;
    }

    void OnDisable()
    {
        GameController.OnReset -= ResetItems;
    }

    // remove Start()

    void ResetItems()
    {
        gameObject.SetActive(true);
        isCollected = false;
    }
}
