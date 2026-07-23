using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Image blackFade;

    public string nextScene;

    public void StartTransition()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        Color color = blackFade.color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                1f,
                timer / 2f
            );

            blackFade.color = color;

            yield return null;
        }

        SceneManager.LoadScene(nextScene);
    }
}