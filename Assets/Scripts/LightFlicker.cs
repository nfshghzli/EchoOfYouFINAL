using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D light2D;

    public float minIntensity = 0.2f;
    public float maxIntensity = 1f;

    public float flickerDuration = 2f;

    public IEnumerator Flicker()
    {
        float timer = 0f;

        while (timer < flickerDuration)
        {
            timer += Time.deltaTime;

            light2D.intensity = Random.Range(
                minIntensity,
                maxIntensity
            );

            yield return new WaitForSeconds(
                Random.Range(0.03f, 0.15f)
            );
        }

        light2D.intensity = maxIntensity;
    }
}
