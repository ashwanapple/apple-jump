using System.Collections;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{

    public GameObject knifePrefab;
    public float timer = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Instantiate(knifePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timer);
        }
    }
}
