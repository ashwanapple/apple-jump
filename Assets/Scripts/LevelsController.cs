using UnityEngine;

public class LevelsController : MonoBehaviour
{
    public GameObject[] levels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levels[LevelsMenuController.currentLev].SetActive(true);
    }
}
