using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BuyButton : ButtonBase
{
    [SerializeField] TextMeshProUGUI textShop, textInv;
    [SerializeField] ButtonClicker clicker;
    [SerializeField] GridLayoutGroup inventar;
    [SerializeField]List<BuyItems> itemsIfBuy;
    [SerializeField] ItemController controller;

    private void Awake()
    {
        itemsIfBuy = new List<BuyItems>();
    }
    public ButtonClicker Clicker { get => clicker; }

    public void AddListener(Action action, int cost)
    {
        onClick = action;
        textShop.text = cost.ToString();
    }

    public override void OnDown(GameObject gameObject)
    {
        gameObject.transform.Rotate(Vector3.forward,45f);
        var t = gameObject.GetComponent<ItemBase>();
        t.TextInfo.text = "Info of item";
        t.TextInfo = textInv;
        var b = gameObject.GetComponent<BuyItems>();
        itemsIfBuy.Add(b);
        b.enabled = false;
        onClick = null;
        textShop.text = "Thank you!";
    }
    private void OnDisable()
    {
        foreach (var item in itemsIfBuy)
        {
            var It = item.gameObject.GetComponent<ItemBase>();
            controller.IsOf(0, 1, It);
            item.gameObject.transform.Rotate(Vector3.forward,-45f);
            item.enabled = false;
        }
        itemsIfBuy.Clear();
    }
}
