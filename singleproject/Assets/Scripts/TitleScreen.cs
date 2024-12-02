using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleName;
    public GameObject startButton;
    public GameObject difficultyText;
    public GameObject easyButton, normalButton, hardButton;

    private int mazeWidth;
    private int mazeHeight;

    void Start()
    {
        difficultyText.SetActive(false);
        easyButton.SetActive(false);
        normalButton.SetActive(false);
        hardButton.SetActive(false);
    }

    // Update is called once per frame
    public void OnStartButtonPressed()
    {
        
        titleName.SetActive(false);
        startButton.SetActive(false);

        difficultyText.SetActive(true);
        easyButton.SetActive(true);
        normalButton.SetActive(true);
        hardButton.SetActive(true);
    }
    public void OnEasyButtonPressed()
    {
        SetMazeSize(21, 21);
        StartGame();
    }

    public void OnNormalButtonPressed()
    {
        SetMazeSize(51, 51);
        StartGame();
    }

    public void OnHardButtonPressed()
    {
        SetMazeSize(101, 101);
        StartGame();
    }
    private void SetMazeSize(int width, int height)
    {
        mazeWidth = width;
        mazeHeight = height;

       
        PlayerPrefs.SetInt("MazeWidth", mazeWidth);
        PlayerPrefs.SetInt("MazeHeight", mazeHeight);
    }
    public void StartGame()
    {       
        SceneManager.LoadScene("GameScene");
    }
}
