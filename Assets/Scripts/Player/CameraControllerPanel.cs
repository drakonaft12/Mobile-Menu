using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed = false;
    public bool isMobile = true;
    public float sensitivity;
    private int fingerId;
    [SerializeField] PlayerController totch;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            pressed = true;
            fingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == fingerId)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        totch.EventTotch();
                    }
                }
            }
        }
    }
}
