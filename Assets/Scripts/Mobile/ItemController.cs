using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.Windows;


public class ItemController : MonoBehaviour
{
    private ListItems[] Items;
    [SerializeField] int indexMagaz = 0, indexInvnt = 1;
    [SerializeField] int moneyMax = 50, damageMax = 5;
    [SerializeField] ItemBase prefab;
    [SerializeField] PageForItems[] pages;
    GeneratorNames generator;


    private void Awake()
    {
        generator = new GeneratorNames();
    }
    private void Start()
    {
        Items = new ListItems[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            Items[i] = new ListItems();
        }
        
    }
    public ItemBase AddItem(int index = 0, bool buyItems = true, Color color = new Color())
    {
        Color c = color.a == 0 ? AddRandomColor() : color;
        
        for (int i = 0; i < pages.Length; i++)
        {
            if (Items[i] != null)
                if (FindDisable(i, out var itemDisable))
            {
                itemDisable.gameObject.SetActive(true);
                AddRandZnach(itemDisable);
                AddParametrs(i, index, buyItems, c, itemDisable);
                return itemDisable;
            }
        }

        var item = Instantiate(prefab, pages[index].invGroup.transform);
        AddRandZnach(item);
        AddParametrs(index, index, buyItems, c, item);
        Items[index].items.Add(item);
        
        return item;

    }
    public ItemBase AddItem(int index, ItemCharacters characters)
    {
        Color c = new Color(characters.colorItem[0], characters.colorItem[1], characters.colorItem[2], characters.colorItem[3]);

        var item = Instantiate(prefab, pages[index].invGroup.transform);
        AddLoadZnahc(characters, item);
        bool isMagaz = index == indexMagaz;
        AddParametrs(index, index, isMagaz, c, item);
        Items[index].items.Add(item);

        return item;

    }

    private void AddLoadZnahc(ItemCharacters characters, ItemBase item)
    {
        item.Money = characters.moneyMore;
        item.Damage = characters.damage;
        item.NameI = characters.nameItem;
        item.Mask = characters.indMask;
        item.Texture = characters.indTex;
        item.mask.sprite = generator.ReturnMask(characters.indMask);
        item.texture.sprite = generator.ReturnTexture(characters.indTex);

        item.speedPlayer = characters.speed;
        item.sprintSpeedPlayer = characters.sprintSpeed;
        item.zalesSpeedPlayer = characters.zalesSpeed;
        item.heithJumpPlayer = characters.heithJump;
        item.maxStaminaPlayer = characters.maxStamina;
        item.staminaPerTimePlayer = characters.staminaPerTime;
        item.staminaSprintPlayer = characters.staminaSprint;
        item.staminaZalesaniePlayer = characters.staminaZalesanie;
        item.staminaJumpPlayer = characters.staminaJump;
    }
    private void AddRandZnach(ItemBase itemI)
    {
        itemI.Money = UnityEngine.Random.Range(1, moneyMax - 1);
        itemI.Damage = UnityEngine.Random.Range(1, damageMax - 1);
        itemI.NameI = generator.ReturnName(out var mask, out var texture, out var _lP, out var _lS);
        itemI.Mask = _lS;
        itemI.Texture = _lP;
        itemI.mask.sprite = mask;
        itemI.texture.sprite = texture;

        itemI.speedPlayer = UnityEngine.Random.Range(-3f, 3f);
        itemI.sprintSpeedPlayer = UnityEngine.Random.Range(-4f, 6f);
        itemI.zalesSpeedPlayer = UnityEngine.Random.Range(-0.2f, 0.4f);
        itemI.heithJumpPlayer = UnityEngine.Random.Range(-1f, 1f);
        itemI.maxStaminaPlayer = UnityEngine.Random.Range(-20, 20);
        itemI.staminaPerTimePlayer = UnityEngine.Random.Range(-0.02f, 0.06f);
        itemI.staminaSprintPlayer = UnityEngine.Random.Range(-0.1f, 0.2f);
        itemI.staminaZalesaniePlayer = UnityEngine.Random.Range(-0.1f, 0.2f);
        itemI.staminaJumpPlayer = UnityEngine.Random.Range(-5, 5);
    }

    private void AddParametrs(int i ,int index, bool buyItems, Color c, ItemBase item)
    {
        item.GetComponent<BuyItems>().enabled = buyItems;
        item.TextInfo = pages[index].info;
        item.Controller = this;
        item.Color = c;
        IsOf(i, index, item);
    }

    public Color AddRandomColor()
    {
        var r = UnityEngine.Random.Range(0f, 1f);
        var g = UnityEngine.Random.Range(0f, 1f);
        var b = UnityEngine.Random.Range(0f, 1f);

        return new Color(r, g, b);
    }
    

    public List<ItemBase> GetItems(int i)
    {
        return Items[i].items;
    }

    public void IsOf(int _is,int _of, ItemBase item)
    {
        if (_is != _of)
        {
            Items[_is].items.Remove(item);
            Items[_of].items.Add(item);
            item.gameObject.transform.parent = pages[_of].invGroup.transform;
            item.TextInfo = pages[_of].info;
        }
        if(_of == indexInvnt)
        {
            SmenaParam(item);
        }
        else
        if(_is == indexInvnt && item.gameObject.activeSelf)
        {
            SmenaParam(item,-1);
        }
    }

    private void SmenaParam(ItemBase item, int mult = 1)
    {
        PlayerStats.me.AddZar(item.Money* mult);
        PlayerStats.me.AddParPlayer(item, mult);
    }

    private bool FindDisable(int index, out ItemBase item)
    { 
        item = Items[index].items.Find((val) => val?.gameObject.activeSelf == false);
        return item != null ? true : false;
    }
    
    public void DeleteItem(ItemBase item)
    {

        SmenaParam(item, -1);
        item.IsDelete = true;
    }

    public int FindInvenar(ItemBase item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i].items.Find((v) => v == item))return i;
        }
        return -1;
    }

    [ContextMenu("Save")]
    public void SaveAs()
    {
        Save(1);
    }

    [ContextMenu("Load")]
    public void LoadAs()
    {
        Controller.me.SetPage(3,100);
        Load(1);

    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Load(int i)
    {
        var characts = JsonConvert.DeserializeObject<List<ItemCharacters>>(System.IO.File.ReadAllText(UnityEngine.Application.streamingAssetsPath + "/ItemsInventory.json"));
        foreach (var item in characts)
        {
            AddItem(i, item);
        }
        
    }

    

    private void Save(int i)
    {
        List<ItemCharacters> characts = new List<ItemCharacters>();
        foreach (var item in Items[i].items)
        {
            characts.Add(item.ItemCharacters);
        }
        System.IO.File.WriteAllText(UnityEngine.Application.streamingAssetsPath + "/ItemsInventory.json", JsonConvert.SerializeObject(characts));
    }

}

[Serializable]
public class PageForItems
{
    public GridLayoutGroup invGroup;
    public TextMeshProUGUI info;
}

[Serializable]
public class ListItems
{
    public List<ItemBase> items;
    public ListItems()
    {
        items = new List<ItemBase>();
    }
}
