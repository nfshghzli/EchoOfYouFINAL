using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 originalPos;

    public float shakeIntensity = 0.2f;
    public float shakeDuration = 0.2f;

    private float timer;

    void Awake()
    {
        instance = this;
        originalPos = transform.position;
    }

    void Update()
    {
        if (timer > 0)
        {
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeIntensity;
            timer -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPos;
        }
    }

    public void Shake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        timer = duration;
    }
}
