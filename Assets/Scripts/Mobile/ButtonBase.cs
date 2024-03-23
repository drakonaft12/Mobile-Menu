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
    public bool isClick;
    protected Action onClick;

    private Vector2 startScale;
    public float animationModificator = 1.1f;

    private void OnEnable()
    {
        targetGraphic = GetComponent<Image>();
        startScale = transform.localScale;
    }

    public virtual void AddListener(Action action)
    {
        if(onClick == null)
        onClick = action;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale *= animationModificator;
        isClick = true;
        onClick?.Invoke();
    }

    public abstract void OnDown(GameObject gameObject); 

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
        transform.localScale = startScale;
    }
    private void OnDisable()
    {
        transform.localScale = startScale;
    }
}
