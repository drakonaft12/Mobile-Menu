using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEditor.Progress;

public class ItemShop : MonoBehaviour
{
    [SerializeField] BuyButton buyButton;
    [SerializeField] ItemController itemController;
    [SerializeField] int countItems = 4, maxCostMoney = 50;
    BuyItems[] items;

    private void Awake()
    {
        items = new BuyItems[countItems];
    }

    private void OnEnable()
    {
        for (int i = 0; i < countItems; i++)
        {
            if (items[i] == null || !items[i].enabled || !items[i].gameObject.activeSelf)
            {
                var itemComp = itemController.AddItem();
                items[i] = itemComp.gameObject.GetComponent<BuyItems>();                
                AddRandZnach(itemComp, items[i]);
                items[i].addButton = buyButton;
                items[i].Cost = UnityEngine.Random.Range(1, maxCostMoney);
            }
        }
    }

    private void AddRandZnach(ItemBase itemI, BuyItems itemB)
    {
        itemB.AddZar = (int)itemI.Money;
        itemB.AddDam = (int)itemI.Damage;
    }
   
}



