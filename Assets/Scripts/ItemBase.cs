using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image))]
public abstract class ItemBase : MonoBehaviour,IPointerClickHandler
{
    private ItemCharacters characters;
    private TextMeshProUGUI text;
    private ItemController controller;
    private Image image;


    public float Money { get => characters.moneyMore; set => characters.moneyMore = value; }
    public float Damage { get => characters.damage; set => characters.damage = value; }
    public TextMeshProUGUI TextInfo { get => text; set => text = value; }
    public string NameI { get => characters.nameItem; set => characters.nameItem = value; }
    public ItemController Controller { set => controller = value; }
    public ItemCharacters ItemCharacters { get => characters; set => characters = value; }
    public Color Color { set {
            for (int i = 0; i < 4; i++)
            {
                characters.colorItem[i] = value[i];
            }
            image.color = value; } }

    private void Awake()
    {
        image = GetComponent<Image>();
        characters = new ItemCharacters();
    }

    [ContextMenu("Delete")]
    public void Delete()
    {
        controller.DeleteItem(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        text.text = $"{characters.nameItem}\n\nMoney for click: \t{characters.moneyMore}\nDamage for click: \t{characters.damage}";
    }
}

[Serializable]
public class ItemCharacters
{
    public float damage = 0;
    public float moneyMore = 0;
    public string nameItem = "Default";
    public float[] colorItem;

    public ItemCharacters()
    {
        colorItem = new float[4];
    }

}
