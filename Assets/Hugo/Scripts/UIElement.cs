using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private bool mouse_over = false;
    public GameObject image;

    void Start()
    {
        image.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        image.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouse_over = false;
        image.SetActive(false);
    }
}
