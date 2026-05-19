using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI hpText;

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
        hpText.text = "HP: " + hp;
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