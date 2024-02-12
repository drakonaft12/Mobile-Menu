using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image targetGraphic;

    protected Action onClick;

    private Vector2 startScale;
    public float animationModificator = 1.1f;

    private void OnEnable()
    {
        targetGraphic = GetComponent<Image>();
        startScale = transform.localScale;
    }

    public void AddListener(Action action)
    {
        if(onClick == null)
        onClick = action;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale *= animationModificator;

        onClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = startScale;
    }
}
