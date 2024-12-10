using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HelpButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject helpImage;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (helpImage != null)
        {
            helpImage.SetActive(true);
        }
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (helpImage != null)
        {
            helpImage.SetActive(false);
        }
    }
}
