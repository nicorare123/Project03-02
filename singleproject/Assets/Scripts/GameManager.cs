using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearUI;
    public Text gameOverText;

    
    public void GameClear()
    {
        Time.timeScale = 0f; 
        gameClearUI.SetActive(true); 
        Debug.Log("게임 클리어!");
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameClearUI.SetActive(true);
        gameOverText.text = "GAMEOVER"; 
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void ReturnToMenu()
    {
        Debug.Log("게임 종료!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }
}
