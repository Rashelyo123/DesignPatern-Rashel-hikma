using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float height = 0.5f;
    public float[] laneOffsets = { 2f, 4.3f, 6.6f };

    public void SpawnObstacles(Transform parentTrack)
    {
        Vector3 trackPos = parentTrack.position;


        int obstacleCount = Random.Range(1, 3);
        for (int i = 0; i < obstacleCount; i++)
        {

            float zOffset = Random.Range(5f, 20f);
            float laneX = laneOffsets[Random.Range(0, laneOffsets.Length)];
            GameObject obstacle = Instantiate(
                obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                new Vector3(trackPos.x + laneX, trackPos.y + height, trackPos.z + zOffset),
                Quaternion.identity,
                parentTrack
            );
            obstacle.tag = "Obstacle";
        }
    }
}
