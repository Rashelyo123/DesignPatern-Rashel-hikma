using UnityEngine;
using UnityEngine.SceneManagement;
using RestAPI.Script;


public class UI_Handle : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void GameOverBackmenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        GameSaveSystem.DeleteSave();

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        SaveCurrentState();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        SaveCurrentState();
        Application.Quit();
    }

    public void ShowGameOver()
    {
        ScoreManager.instance.StopScore();
        SaveSystem.Instance.SaveHighScore();
        gameOverUI.SetActive(true);
    }

    private void SaveCurrentState()
    {
        GameData data = new GameData();
        data.score = ScoreManager.instance.score;
        data.coins = ScoreManager.instance.coins;
        data.sceneName = SceneManager.GetActiveScene().name;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            data.playerPosition = new float[] { pos.x, pos.y, pos.z };
        }

        GameSaveSystem.Save(data);
        Debug.Log("Game state saved!");
    }
}
