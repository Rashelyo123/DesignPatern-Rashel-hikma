using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 10;
    public float spacing = 2f;
    public float baseHeight = 1.5f;


    // Tambahkan kompensasi X agar sejajar dengan jalan
    public float trackCenterOffsetX = 12f; // sesuaikan dengan posisi pivot lintasan kamu

    float[] laneOffsets = { -2.5f, 0f, 2.5f };

    public void SpawnCoins(Transform parentTrack)
    {
        Vector3 trackPos = parentTrack.position;

        // 🔹 Tambahkan offset ke kanan sebesar trackCenterOffsetX
        float baseX = trackPos.x + trackCenterOffsetX;
        float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPos = new Vector3(
                baseX + laneX,
                trackPos.y + baseHeight,
                trackPos.z + i * spacing
            );

            Instantiate(coinPrefab, coinPos, Quaternion.identity, parentTrack);
        }
    }


    void SpawnStraight(Transform parentTrack, Vector3 trackPos)
    {
        float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPos = new Vector3(trackPos.x + laneX, trackPos.y + baseHeight, trackPos.z + i * spacing);
            Instantiate(coinPrefab, coinPos, Quaternion.identity, parentTrack);
        }
    }


    void SpawnZigzag(Transform parentTrack, Vector3 trackPos)
    {
        int direction = Random.value > 0.5f ? 1 : -1;
        float laneX = laneOffsets[direction == 1 ? 0 : laneOffsets.Length - 1];

        for (int i = 0; i < coinCount; i++)
        {

            if (i % 3 == 0)
            {
                laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];
            }

            Vector3 coinPos = new Vector3(trackPos.x + laneX, trackPos.y + baseHeight, trackPos.z + i * spacing);
            Instantiate(coinPrefab, coinPos, Quaternion.identity, parentTrack);
        }
    }


    void SpawnJumpArc(Transform parentTrack, Vector3 trackPos)
    {
        float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];

        for (int i = 0; i < coinCount; i++)
        {

            float heightOffset = Mathf.Sin((float)i / coinCount * Mathf.PI) * 3f;
            Vector3 coinPos = new Vector3(trackPos.x + laneX, trackPos.y + baseHeight + heightOffset, trackPos.z + i * spacing);
            Instantiate(coinPrefab, coinPos, Quaternion.identity, parentTrack);
        }
    }


    void SpawnMixedPattern(Transform parentTrack, Vector3 trackPos)
    {
        for (int i = 0; i < coinCount; i++)
        {
            float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];
            float heightOffset = Mathf.Sin(i * 0.5f) * 2f;
            Vector3 coinPos = new Vector3(trackPos.x + laneX, trackPos.y + baseHeight + heightOffset, trackPos.z + i * spacing);
            Instantiate(coinPrefab, coinPos, Quaternion.identity, parentTrack);
        }
    }
}
