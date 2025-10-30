using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Header("Speed Settings")]
    public float baseSpeed = -7f;
    public float speedIncreaseRate = -0.2f;
    public float maxSpeed = -25f;
    public float increaseInterval = 10f;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            timer = 0f;

            if (baseSpeed > maxSpeed)
            {
                baseSpeed += speedIncreaseRate; // contoh: -7 + (-0.2) = -7.2
                Debug.Log("Speed meningkat: " + baseSpeed);
            }
        }
    }
}
