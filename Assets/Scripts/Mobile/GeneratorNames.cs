using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GeneratorNames
{
    private NameVariation variation;
    private ImageVariation images;

    public GeneratorNames()
    {
        Load();
        LoadImages();
    }

    private void Load()
    {

        variation = JsonConvert.DeserializeObject<NameVariation>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NamesStat.json"));
    }

    private void Save()
    {
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/NamesStat.json", JsonConvert.SerializeObject(variation));
    }
    public string ReturnName(out Sprite mask, out Sprite texture, out int _lP, out int _lS)
    {
        if (variation != null)
        {
            var lP = variation.прилагательные.Length;
            var lS = variation.существительные.Length;
            lP = _lP = UnityEngine.Random.Range(0, lP);
            lS = _lS = UnityEngine.Random.Range(0, lS);
            mask = images.spritesSush[lS];
            texture = images.spritesPrilag[lP];
            return AlwaceGrandChar(variation.прилагательные[lP], 'а', 'А') + " " + variation.существительные[lS];
        }
        mask = null;
        texture = null;
        _lP = 0;
        _lS = 0;
        return null;
    }

    public Sprite ReturnMask(int i)
    {
        return images.spritesSush[i];
    }

    public Sprite ReturnTexture(int i)
    {
        return images.spritesPrilag[i];
    }
    public string AlwaceGrandChar(string _name, char iz, char v)
    {
        int r = iz - v;
        char[] n = _name.ToCharArray();
        if (n[0] < iz)
            return _name;
        else
        {
            n[0] -= (char)r;
            return n.ArrayToString();
        }
    }
    private void LoadImages()
    {
        images = new ImageVariation();
        images.spritesPrilag = new Sprite[variation.прилагательные.Length];
        images.spritesSush = new Sprite[variation.существительные.Length];
        for (int i = 0; i < variation.прилагательные.Length; i++)
        {
            LoadTexture(variation.прилагательные[i], i);
        }
        for (int i = 0; i < variation.существительные.Length; i++)
        {
            LoadMask(variation.существительные[i], i);
        }

    }
    
    private async void LoadTexture(string name,int index)
    {

        WWW wWW = new WWW("file:///" + UnityEngine.Application.dataPath + $"/Textures/{name}.png");
        UnityWebRequest unity = new UnityWebRequest();

        while (!wWW.isDone && string.IsNullOrEmpty(wWW.error))
        {
            await Task.Delay(10);
        }

        if (string.IsNullOrEmpty(wWW.error))
        {
            var tex = wWW.textureNonReadable;
            images.spritesPrilag[index] = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
    }
    private async void LoadMask(string name, int index)
    {

        WWW wWW = new WWW("file:///" + UnityEngine.Application.dataPath + $"/Masks/{name}.png");
        while (!wWW.isDone && string.IsNullOrEmpty(wWW.error))
        {
            await Task.Delay(10);
        }

        if (string.IsNullOrEmpty(wWW.error))
        {
            var tex = wWW.textureNonReadable;
            images.spritesSush[index] = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
    }
}

[Serializable]
public class NameVariation
{
    public string[] прилагательные;
    
    public string[] существительные;
    
}

public class ImageVariation
{
    public Sprite[] spritesPrilag;
    public Sprite[] spritesSush;
}
