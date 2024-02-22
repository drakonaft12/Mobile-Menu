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
    bool isLoad;
    int maxItems;

    public bool Load { get => isLoad; set => isLoad = value; }

    private void Awake()
    {
        items = new BuyItems[countItems];
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnItems(itemController));
    }

    private IEnumerator SpawnItems(ItemController Controller)
    {
        isLoad = true;
        for (int i = 0; i < countItems; i++)
        {
            if (items[i] == null || !items[i].enabled || items[i].gameObject.GetComponent<ItemBase>().IsDelete)
            {
                var itemComp = Controller.AddItem();
                items[i] = itemComp.gameObject.GetComponent<BuyItems>();
                AddZnach(itemComp, items[i]);
                items[i].addButton = buyButton;
                items[i].Cost = UnityEngine.Random.Range(1, maxCostMoney);
            }

            yield return new WaitWhile(()=> !isLoad);
        }
       
        isLoad = false;
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnItems(itemController));
    }

    private void AddZnach(ItemBase itemI, BuyItems itemB)
    {
        itemB.AddZar = (int)itemI.Money;
        itemB.AddDam = (int)itemI.Damage;
    }
   
}



