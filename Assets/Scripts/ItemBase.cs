using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class ItemBase : MonoBehaviour,IPointerClickHandler
{
    private float damage;
    private float moneyMore;
    private TextMeshProUGUI text;

    public float Money { set => moneyMore = value; }
    public float Damage { set => damage = value; }
    public TextMeshProUGUI TextInfo { get => text; set => text = value; }

    private void Awake()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        var i = GetComponent<Image>();
        i.color = new Color(r,g,b);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        text.text = $"Money for click: \t{moneyMore}\nDamage for click: \t{damage}";
    }
}
