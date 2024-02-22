using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image))]
public abstract class ItemBase : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] public Image mask, texture;
    private ItemCharacters characters;
    private TextMeshProUGUI text;
    private ItemController controller;
    private Image image;
    private bool isDelete = false;

    


    public float Money { get => characters.moneyMore; set => characters.moneyMore = value; }
    public float Damage { get => characters.damage; set => characters.damage = value; }
    public TextMeshProUGUI TextInfo { get => text; set => text = value; }
    public string NameI { get => characters.nameItem; set { characters.nameItem = value; } }
    public int Mask { get => characters.indMask; set => characters.indMask = value; }
    public int Texture { get => characters.indTex; set => characters.indTex = value; }
    public ItemController Controller { set => controller = value; }
    public ItemCharacters ItemCharacters { get => characters; set => characters = value; }
    public Color Color { set {
            for (int i = 0; i < 4; i++)
            {
                characters.colorItem[i] = value[i];
            }
            Image.color = value; } }

    public bool IsDelete { get => isDelete; set { isDelete = value; if (isDelete) gameObject.SetActive(false); else { gameObject.SetActive(true); } } }

    public Image Image { get => image; set => image = value; }

    private void Awake()
    {
        Image = GetComponent<Image>();
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

    private void SetImageInString(string _string, int coll)
    {
        string[] slova = new string[coll];
        List<int> probel = new List<int>{-1};
        for (int i = 0; i < _string.Length; i++)
        {
            if (_string[i] == ' ') probel.Add(i);
        }
        probel.Add(_string.Length);
        
        for (int i = 0; i < slova.Length; i++)
        {
            int y = probel[i] + 1;
            while (y < probel[i + 1])
            {
                slova[i] += _string[y];
                y++;
            }
        }
        
        
    }
    private IEnumerator LoadTexture(string name)
    {
        
        WWW wWW = new WWW("file:///" + UnityEngine.Application.dataPath + $"/Textures/{name}.png");
        UnityWebRequest unity = new UnityWebRequest();
        
        while (!wWW.isDone && string.IsNullOrEmpty(wWW.error))
        {
            yield return null;
        }
        
        if (string.IsNullOrEmpty(wWW.error))
        {
            var tex = wWW.textureNonReadable;
            texture.sprite = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),Vector2.zero);
        }
    }
    private IEnumerator LoadMask(string name)
    {
        
        WWW wWW = new WWW("file:///" + UnityEngine.Application.dataPath + $"/Masks/{name}.png");
        while (!wWW.isDone && string.IsNullOrEmpty(wWW.error))
        {
            yield return null;
        }

        if (string.IsNullOrEmpty(wWW.error))
        {
            var tex = wWW.textureNonReadable;
            mask.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
    }
}

[Serializable]
public class ItemCharacters
{
    public float damage = 0;
    public float moneyMore = 0;
    public string nameItem = "Default";
    public int indTex = 0, indMask = 0;
    public float[] colorItem;

    public ItemCharacters()
    {
        colorItem = new float[4];
    }

}
