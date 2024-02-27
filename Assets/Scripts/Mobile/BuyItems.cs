using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyItems : MonoBehaviour, IPointerClickHandler
{
    BuyButton buyButton;
    int cost = 1, addZar = 0, addDam = 0;
    ItemBase item;

    public BuyButton addButton { set => buyButton = value; }
    public int Cost { set => cost = value; }
    public int AddZar { set => addZar = value; }
    public int AddDam { set => addDam = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        buyButton.AddListener(FromBuy,cost);
    }

    private void FromBuy()
    { 
        if (PlayerStats.me.ShopItem(cost)) { buyButton.OnDown(this.gameObject); }
    }

    private void Start()
    {
        TryGetComponent(out item);
        cost = cost < 0 ? 0 : cost;
        cost *= addZar + addDam/2;
    }
}
