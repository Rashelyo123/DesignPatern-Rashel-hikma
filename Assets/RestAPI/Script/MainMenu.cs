using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        continueButton.SetActive(GameSaveSystem.HasSave());
    }

    public void NewGame()
    {
        GameSaveSystem.DeleteSave();
        SceneManager.LoadScene("GamePlay");
    }

    public void ContinueGame()
    {
        GameData data = GameSaveSystem.Load();
        if (data != null)
        {
            SceneManager.LoadScene(data.sceneName);
            // Load data setelah scene ter-load
            SceneLoader.lastLoadedData = data;
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
