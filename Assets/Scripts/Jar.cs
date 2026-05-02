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
            Destroy(gameObject);
            isCollected = true;
        }
        
    }
}
