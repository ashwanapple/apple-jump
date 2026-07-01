using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public GameObject knifePrefab;
    public float timer = 2f;
    private List<GameObject> activeKnifes = new List<GameObject>();

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
            activeKnifes.Add(knife);
        }
    }

    private void ResetSpawner()
    {
        StopAllCoroutines(); // kill current loop
        ClearKnifes();
        StartCoroutine(SpawnLoop()); // start new loop for new level

    }

    void OnEnable()
    {
        GameController.OnReset += ResetSpawner;
        StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        GameController.OnReset -= ResetSpawner;
        StopAllCoroutines();
        ClearKnifes();
    }

    private void ClearKnifes()
    {
        foreach (var knife in activeKnifes)
        {
            if (knife != null)
            {
                Destroy(knife);
            }
        }

        activeKnifes.Clear();
    }

}

