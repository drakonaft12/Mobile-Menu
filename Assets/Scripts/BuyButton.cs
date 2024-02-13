using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : ButtonBase
{
    [SerializeField] TextMeshProUGUI textShop, textInv;
    [SerializeField] ButtonClicker clicker;
    [SerializeField] GridLayoutGroup inventar;

    public ButtonClicker Clicker { get => clicker; }

    public void AddListener(Action action, int cost)
    {
        onClick = action;
        textShop.text = cost.ToString();
    }

    public override void OnDown(GameObject gameObject)
    {
        gameObject.transform.parent = inventar.transform;
        var t = gameObject.GetComponent<ItemBase>();
        t.TextInfo.text = "Info of item";
        t.TextInfo = textInv;
        onClick = null;
        textShop.text = "Thank you!";
    }
}
