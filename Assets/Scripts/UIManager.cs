using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    public GameObject warningImage;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        warningImage.SetActive(false);
    }

    public void UpdateHP(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void ShowWarning()
    {
        warningImage.SetActive(true);
    }

    public void HideWarning()
    {
        warningImage.SetActive(false);
    }
}