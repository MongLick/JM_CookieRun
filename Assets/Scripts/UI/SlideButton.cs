using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SlideButton : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public UnityEvent OnButtonClick;
    public UnityEvent OnButtonExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnButtonExit?.Invoke();
    }
}
