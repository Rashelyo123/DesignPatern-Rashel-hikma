using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Handle : MonoBehaviour
{
    public GameObject gameOverUI;
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowGameOver()
    {

        gameOverUI.SetActive(true);

    }
}
