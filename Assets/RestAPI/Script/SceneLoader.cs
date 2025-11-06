using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static GameData lastLoadedData;

    void Start()
    {
        if (lastLoadedData != null)
        {
            StartCoroutine(RestoreState());
        }
    }

    private IEnumerator RestoreState()
    {
        yield return null; // tunggu satu frame

        GameData data = lastLoadedData;
        lastLoadedData = null;

        ScoreManager.instance.score = data.score;
        ScoreManager.instance.coins = data.coins;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(
                data.playerPosition[0],
                data.playerPosition[1],
                data.playerPosition[2]
            );
        }

        Debug.Log("Game state restored!");
    }
}
