using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ProceduralGenerateLevel : MonoBehaviour
{
    public List<GameObject> easyTracks;
    public List<GameObject> mediumTracks;
    public List<GameObject> hardTracks;

    public CoinSpawner coinSpawner; // 🔹 referensi ke coin spawner
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
                // spawn track baru
                GameObject newTrack = Instantiate(selectedTrack, new Vector3(-4.3f, 1.1f, 107.6f), selectedTrack.transform.rotation);

                // spawn coin di atas track baru
                if (coinSpawner != null)
                {
                    coinSpawner.SpawnCoins(newTrack.transform);
                    Debug.Log("Koin telah di-spawn di atas track baru.");
                }
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

        Debug.LogWarning("Tidak ada lintasan yang tersedia untuk kesulitan saat ini!");
        return null;
    }
}
