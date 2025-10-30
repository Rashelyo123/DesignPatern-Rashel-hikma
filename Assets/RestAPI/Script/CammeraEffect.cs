using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraEffects : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float followSmoothness = 5f;

    [Header("Tilt Settings")]
    public float tiltAmount = 10f;
    private float currentTilt = 0f;

    [Header("Zoom (FOV) Settings")]
    public float minFOV = 60f;
    public float maxFOV = 80f;
    public float fovSmoothness = 2f;

    [Header("Shake Settings")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.3f;
    private bool isShaking = false;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        if (target == null)
        {
            Debug.LogWarning("no camera target set for CameraEffects.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;


        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSmoothness * Time.deltaTime);


        transform.LookAt(target);


        float input = Input.GetAxis("Horizontal");
        float targetTilt = input * tiltAmount;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * 5f);
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -currentTilt);


        float speed = 10f;
        if (DifficultyManager.Instance != null)
            speed = Mathf.Abs(DifficultyManager.Instance.baseSpeed);

        float targetFOV = Mathf.Lerp(minFOV, maxFOV, speed / 20f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovSmoothness);
    }


    public void TriggerShake(float duration = -1f, float magnitude = -1f)
    {
        if (!isShaking)
        {
            if (duration <= 0) duration = shakeDuration;
            if (magnitude <= 0) magnitude = shakeMagnitude;
            StartCoroutine(Shake(duration, magnitude));
        }
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        isShaking = true;
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        isShaking = false;
    }
}
