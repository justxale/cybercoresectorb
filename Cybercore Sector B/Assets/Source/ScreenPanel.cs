using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPointed;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointed = false;
    }
}
