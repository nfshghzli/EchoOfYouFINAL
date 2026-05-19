using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    public static ScreenEffects instance;

    public Image overlay;

    private float targetAlpha = 0f;
    private float currentAlpha = 0f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * 5f);

        Color c = overlay.color;
        c.a = currentAlpha;
        overlay.color = c;
    }

    public void SetNormal()
    {
        targetAlpha = 0f;
    }

    public void SetWhisper()
    {
        targetAlpha = 0.2f;
    }

    public void SetPanic()
    {
        targetAlpha = 0.5f;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    System.Collections.IEnumerator FlashRoutine()
    {
        overlay.color = new Color(1, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.1f);
        overlay.color = new Color(0, 0, 0, currentAlpha);
    }
}
