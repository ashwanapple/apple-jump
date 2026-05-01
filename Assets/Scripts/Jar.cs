using System;
using UnityEngine;

public class Jar : MonoBehaviour, Item
{
    public static event Action<int> OnJarCollect;
    public int worth = 1;


    public void Collect()
    {
        OnJarCollect.Invoke(worth);
        Destroy(gameObject);
    }
}
