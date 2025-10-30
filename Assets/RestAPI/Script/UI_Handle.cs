using RestAPI.Script;
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
        SaveSystem.Instance.SaveHighScore();
        gameOverUI.SetActive(true);

    }
}
