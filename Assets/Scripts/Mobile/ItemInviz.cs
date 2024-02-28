using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ItemInviz : MonoBehaviour
{
    [SerializeField] ItemController controller;
    [SerializeField] int maxItems = 400;
    [SerializeField] ItemShop shop;
    List<ItemBase> items;
    ScrollRect rect;
    UnityEngine.Vector2 vectorSave = new UnityEngine.Vector2(0,0);
    int activItems = 26;
    int itemCount;
    ContentSizeFitter content;
    private void Awake()
    {
        rect = GetComponent<ScrollRect>();
        rect.onValueChanged.AddListener(ScrollContr);
        content = rect.content.GetComponent<ContentSizeFitter>();


    }
    private void Start()
    {
        rect.content.hierarchyCapacity = maxItems;
    }

    
    private void ScrollContr(UnityEngine.Vector2 vector)
    {
        items = controller.GetItems(0);
        if (items.Count > itemCount)
        {
            content.enabled = true;
            
        }
        else
        {
            content.enabled = false;
        }
        if(items.Count!= itemCount) { itemCount = items.Count; }
        float intr = (1 - (vector.y <= 0 ? 0 : vector.y)) * itemCount;
        int procent = (int)(intr <= 0 ? 0 : intr);
        for (int i = 0; i < maxItems; i++)
        {
            if (itemCount > i)
            {
                if (i >= procent - activItems ) { SetInv(true, i); }
                else { SetInv(false, i); }
                if (i < procent + activItems) { items[i].gameObject.SetActive(true); }
                else { items[i].gameObject.SetActive(false); }
                if (procent >= itemCount- activItems) shop.Load = true;
                else shop.Load = false;

            }
        }
        vectorSave = vector;
    }
    public void SetInv(bool t, int i)
    {
        if (items[i] != null)
        {
            items[i].Image.enabled = t;
            items[i].mask.enabled = t;
            items[i].texture.enabled = t;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < itemCount; i++)
        {
            SetInv(true, i);
            items[i]?.gameObject.SetActive(true);
        }
    }

}
