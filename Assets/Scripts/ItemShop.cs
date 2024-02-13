using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ItemShop : MonoBehaviour
{
    [SerializeField] BuyItems prefab;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] BuyButton buyButton;
    int collTypesItems = 1;
    [SerializeField] int countItems = 4, moneyMax = 50, damageMax = 5, maxCostMoney = 50;
    BuyItems[] items;

    private void Awake()
    {
        items = new BuyItems[countItems];
    }

    private void OnEnable()
    {
        for (int i = 0; i < countItems; i++)
        {
            items[i] = Instantiate(prefab, transform);
            var r = UnityEngine.Random.Range(0, collTypesItems-1);
            var itemComp = AddItemComp(r, items[i].gameObject);
            AddRandZnach(itemComp, items[i]);
            items[i].addButton = buyButton;
            items[i].Cost = UnityEngine.Random.Range(1, maxCostMoney);
        }
    }

    private void OnDisable()
    {
        foreach (var item in items)
        {
            if(item!=null)
            Destroy(item.gameObject);
        }
    }
    private ItemBase AddItemComp(int i,GameObject @object)
    {
        switch (i) {
            case 0:
                return @object.AddComponent<Item>();
                }
        return null;
    }

    private void AddRandZnach(ItemBase itemI, BuyItems itemB)
    {
        itemI.Money = itemB.AddZar = UnityEngine.Random.Range(1, moneyMax - 1);
        itemI.Damage = itemB.AddDam = UnityEngine.Random.Range(1, damageMax - 1);
        itemI.TextInfo = text;
    }
   
}
