using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PageBase : MonoBehaviour
{
    private Image[] allTargetGraphic;
    private TextMeshProUGUI[] allTargetText;
    [SerializeField]private float animationDuration = 2;
    private GraphicRaycaster[] raycaster;



    public async virtual void StartScreen()
    {
        gameObject.SetActive(true);
        allTargetGraphic = GetComponentsInChildren<Image>();
        allTargetText = GetComponentsInChildren<TextMeshProUGUI>();
        raycaster = GetComponentsInChildren<GraphicRaycaster>();
        foreach (var item in raycaster)item.enabled = false;
        Color color;
            foreach (var item in allTargetGraphic)
            {
                color = item.color;
                color.a = 0;
                item.color = color;
            }
            foreach (var item in allTargetText)
            {
                color = item.color;
                color.a = 0;
                item.color = color;
            }
            foreach (var item in allTargetGraphic)
            {
                item.DOFade(1, animationDuration);
            }
            foreach (var item in allTargetText)
            {
                item.DOFade(1, animationDuration);
            }
        await Task.Delay((int)(animationDuration * 1000));
        foreach (var item in raycaster) item.enabled = true;

    }

    public async void CloseScreen()
    {
        foreach (var item in raycaster) item.enabled = false;
        foreach (var item in allTargetGraphic)
        {
            item.DOFade(0, animationDuration);
        }

        foreach (var item in allTargetText)
        {
            item.DOFade(0, animationDuration);
        }

        await Task.Delay((int)(animationDuration*1000));
        gameObject.SetActive(false);
        foreach (var item in raycaster) item.enabled = true;
    }
}
