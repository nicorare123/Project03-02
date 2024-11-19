using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject titleUI;       
    public GameObject difficultyUI;
    public GameObject gameUI;

    private void Start()
    {
        titleUI.SetActive(true);       
        difficultyUI.SetActive(false); 
        gameUI.SetActive(false);
    }
    public void ShowDifficultySelection()
    {
        titleUI.SetActive(false);
        difficultyUI.SetActive(true);
        Debug.Log("Start Button Clicked!");
    }
    public void StartGame()
    {
        difficultyUI.SetActive(false);
        gameUI.SetActive(true);
    }
}
