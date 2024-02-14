using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

[RequireComponent(typeof(GridLayoutGroup))]
public class ItemShop : MonoBehaviour
{
    [SerializeField] BuyItems prefab;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] BuyButton buyButton;
    int collTypesItems = 1;
    [SerializeField] int countItems = 4, moneyMax = 50, damageMax = 5, maxCostMoney = 50;
    BuyItems[] items;
    GeneratorNames generator ;

    private void Awake()
    {
        items = new BuyItems[countItems];
        generator = new GeneratorNames();
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
        itemI.TextInfo = text; itemI.NameI = generator.ReturnName();

    }
   
}

public class GeneratorNames
{
    private NameVariation variation;

    public GeneratorNames()
    {
        Load();
    }

    private void Load()
    {
       
        variation = JsonUtility.FromJson<NameVariation>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NamesStat.json"));
    }

    private void Save()
    {
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/NamesStat.json", JsonUtility.ToJson(variation));
    }
    public string ReturnName()
    {
        if(variation != null)
        {
            var lP = variation.прилагательные.Length;
            var lS = variation.существительные.Length;
            lP = UnityEngine.Random.Range(0, lP - 1);
            lS = UnityEngine.Random.Range(0, lS - 1);
            return variation.прилагательные[lP] + " " + variation.существительные[lS];
        }
        return null;
    }
}

[Serializable]
public class NameVariation 
{
    public string[] прилагательные;
    public string[] существительные;
}

