using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;
    public float height = 0.5f;
    public float zMin = 5f;
    public float zMax = 20f;

    [Header("Lane Positions (relative to track center)")]
    public float[] laneOffsets = { -2.5f, 0f, 2.5f };

    [Header("Track Position Correction")]
    public float trackCenterOffsetX = 12f;

    public void SpawnObstacles(Transform parentTrack)
    {
        Vector3 trackPos = parentTrack.position;
        float baseX = trackPos.x + trackCenterOffsetX;

        int obstacleCount = Random.Range(1, 3);
        for (int i = 0; i < obstacleCount; i++)
        {

            float zOffset = Random.Range(zMin, zMax);


            float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];

            Vector3 obstaclePos = new Vector3(
                baseX + laneX,
                trackPos.y + height,
                trackPos.z + zOffset
            );

            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject obstacle = Instantiate(prefab, obstaclePos, Quaternion.identity, parentTrack);
            obstacle.tag = "Obstacle";
        }
    }
}
