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
    List<ItemBase> items;
    ScrollRect rect;
    UnityEngine.Vector2 vectorSave = new UnityEngine.Vector2(0,0);
    int activItems = 6*4;
    int itemCount;
    private void Awake()
    {
        rect = GetComponent<ScrollRect>();
        rect.onValueChanged.AddListener(ScrollContr);
        
       
    }
    private void Start()
    {
        rect.content.hierarchyCapacity = maxItems;
    }

    private void Update()
    {
        items = controller.GetItems(0);
        if (items.Count != itemCount)
        {
            itemCount = items.Count;
        }
        if(itemCount == maxItems)
        {
            rect.content.GetComponent<ContentSizeFitter>().enabled = false;
        }
    }

    private void OnEnable()
    {
       
    }

    private void ScrollContr(UnityEngine.Vector2 vector)
    {
        float intr = (1-vector.y) * itemCount;
        float procent = (intr);
        for (int i = 0; i < maxItems; i++)
        {
            if (itemCount > i)
            {
                if (i >= procent - activItems ) { SetInv(true, i); }
                else { SetInv(false, i); }
                if (i <= procent + activItems) { items[i].gameObject.SetActive(true); }
                else { items[i].gameObject.SetActive(false); }
            }
        }
        vectorSave = vector;
    }
    public void SetInv(bool t, int i)
    {
        items[i].Image.enabled = t;
        items[i].mask.enabled = t;
        items[i].texture.enabled = t;
    }
    private void OnDisable()
    {
        for (int i = 0; i < maxItems; i++)
        {
            SetInv(true, i);
            items[i].gameObject.SetActive(true);
        }
    }

}
