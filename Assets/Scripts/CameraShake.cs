using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 shakeOffset;

    public float shakeIntensity = 0.2f;
    public float shakeDuration = 0.2f;

    private float timer;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (timer > 0)
        {
            shakeOffset =
                (Vector3)Random.insideUnitCircle
                * shakeIntensity;

            timer -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }

    public void Shake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        timer = duration;
    }

    public Vector3 GetShakeOffset()
    {
        return shakeOffset;
    }
}