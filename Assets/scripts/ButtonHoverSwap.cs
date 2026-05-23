using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonHoverSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite normalSprite;  
    [SerializeField] private Sprite hoverSprite;   

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (normalSprite == null) normalSprite = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null) image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (normalSprite != null) image.sprite = normalSprite;
    }
}