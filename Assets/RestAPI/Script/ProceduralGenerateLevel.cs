using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ProceduralGenerateLevel : MonoBehaviour
{
    public List<GameObject> easyTracks;
    public List<GameObject> mediumTracks;
    public List<GameObject> hardTracks;
    public ObstacleSpawner obstacleSpawner;

    public CoinSpawner coinSpawner;
    private float startTime;
    private float elapsedTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerLevel"))
        {
            GameObject selectedTrack = GetRandomTrack();
            if (selectedTrack != null)
            {

                GameObject newTrack = Instantiate(selectedTrack, new Vector3(-4.3f, 1.1f, 107.6f), selectedTrack.transform.rotation);


                if (coinSpawner != null)
                {
                    coinSpawner.SpawnCoins(newTrack.transform);

                }

                if (obstacleSpawner != null)
                    obstacleSpawner.SpawnObstacles(newTrack.transform);
            }
        }
    }

    private GameObject GetRandomTrack()
    {
        List<GameObject> trackList = new List<GameObject>();

        if (elapsedTime < 120) trackList = easyTracks;
        else if (elapsedTime < 300) trackList = mediumTracks;
        else trackList = hardTracks;

        if (trackList.Count > 0)
        {
            return trackList[Random.Range(0, trackList.Count)];
        }


        return null;
    }
}
