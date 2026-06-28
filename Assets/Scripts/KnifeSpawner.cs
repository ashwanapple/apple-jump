using System.Collections;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public GameObject knifePrefab;
    public float timer = 2f;

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Instantiate(knifePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timer);
        }
    }

    private void ResetSpawner()
    {
        StopAllCoroutines(); // kill current loop
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
    }

}

