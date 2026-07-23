using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMFadeIn : MonoBehaviour
{
    public float fadeDuration = 3f;
    public float targetVolume = 0.6f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0f;
        audioSource.Play();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(
                0f,
                targetVolume,
                timer / fadeDuration
            );

            yield return null;
        }

        audioSource.volume = 0.6f;
    }
}
