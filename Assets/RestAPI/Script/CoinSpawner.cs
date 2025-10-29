using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 10;    // jumlah koin per baris
    public float spacing = 2f;    // jarak antar koin
    public float height = 1.5f;   // tinggi koin dari permukaan track

    float[] lanePositions = { -2f, 0f, 2f }; // posisi x sesuai jalur player

    public void SpawnCoins(Transform parentTrack)
    {
        GameObject coinPrefabToUse = coinPrefab;
        if (coinPrefabToUse == null) return;

        // posisi awal relatif terhadap track
        Vector3 startPos = parentTrack.position + new Vector3(0, 1.5f, 0);
        float[] lanePositions = { -2f, 0f, 2f };

        float laneX = lanePositions[Random.Range(0, lanePositions.Length)];

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPos = startPos + new Vector3(laneX, 0, i * spacing);
            GameObject coin = Instantiate(coinPrefabToUse, coinPos, Quaternion.identity);
            coin.transform.SetParent(parentTrack); // biar koin ikut gerak bareng track
        }
    }

}
