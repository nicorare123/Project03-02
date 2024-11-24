using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearUI;

    public void GameClear()
    {
        Time.timeScale = 0f; 
        gameClearUI.SetActive(true); 
        Debug.Log("게임 클리어!");
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
