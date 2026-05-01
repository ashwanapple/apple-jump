using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressNum;
    public Slider progressSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressNum = 0;
        progressSlider.value = 0;
        Jar.OnJarCollect += IncreaseProgressAmount;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressNum += amount;
        progressSlider.value = progressNum;
        if (progressNum >= 3)
        {
            // Complete
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
