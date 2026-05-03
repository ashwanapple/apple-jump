using System.Collections;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    public float fadeDuration = 1;

    SpriteRenderer[] spriteRenderers;
    Color[] hiddenColors;
    Coroutine currentCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        hiddenColors = new Color[spriteRenderers.Length];
        for (int i = 0; i <spriteRenderers.Length; i++)
        {
            hiddenColors[i] = spriteRenderers[i].color;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(FadeSprite(true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(FadeSprite(false));
        }
    }

    private IEnumerator FadeSprite(bool fadeOut)
    {
        Color[] startColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            startColors[i] = spriteRenderers[i].color;
        }

        float timeFading = 0f;

        while (timeFading < fadeDuration)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Color target = fadeOut ? new Color(hiddenColors[i].r, hiddenColors[i].g, hiddenColors[i].b, 0f) : hiddenColors[i];
                spriteRenderers[i].color = Color.Lerp(startColors[i], target, timeFading / fadeDuration);
            }
            
            timeFading += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < spriteRenderers.Length; i++) {
            spriteRenderers[i].color = fadeOut ? new Color(hiddenColors[i].r, hiddenColors[i].g, hiddenColors[i].b, 0f) : hiddenColors[i];
        }
    }
}
