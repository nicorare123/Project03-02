using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject Hp1;
    public GameObject Hp2;
    public GameObject Hp3;
    public GameObject BatteryHome;
    // Start is called before the first frame update
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Title")
        {
            ActivateTitleSceneUI();
        }
        else if (currentScene == "GameScene")
        {
            ActivateGameSceneUI();
        }
        else
        {
            DeactivateAllUI();
        }
    }
    private void ActivateTitleSceneUI()
    {
        playerUI.SetActive(false);
        Hp1.SetActive(false);
        Hp2.SetActive(false);
        Hp3.SetActive(false);
        BatteryHome.SetActive(false);
    }

    private void ActivateGameSceneUI()
    {
        playerUI.SetActive(true);
        Hp1.SetActive(true);
        Hp2.SetActive(true);
        Hp3.SetActive(true);
        BatteryHome.SetActive(true);
    }

    private void DeactivateAllUI()
    {
        playerUI.SetActive(false);
        Hp1.SetActive(false);
        Hp2.SetActive(false);
        Hp3.SetActive(false);
        BatteryHome.SetActive(false);
    }
}
