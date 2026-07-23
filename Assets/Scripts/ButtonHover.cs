using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler,
    IPointerClickHandler
{

    public Image buttonImage;

    public Sprite normalSprite;
    public Sprite hoverSprite;


    [Header("Click Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;



    void Start()
    {
        if(buttonImage != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if(buttonImage != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }




    public void OnPointerExit(PointerEventData eventData)
    {
        if(buttonImage != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if(audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}