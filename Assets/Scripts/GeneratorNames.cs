using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneratorNames
{
    private NameVariation variation;

    public GeneratorNames()
    {
        Load();
    }

    private void Load()
    {

        variation = JsonConvert.DeserializeObject<NameVariation>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NamesStat.json"));
    }

    private void Save()
    {
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/NamesStat.json", JsonConvert.SerializeObject(variation));
    }
    public string ReturnName()
    {
        if (variation != null)
        {
            var lP = variation.прилагательные.Length;
            var lS = variation.существительные.Length;
            lP = UnityEngine.Random.Range(0, lP);
            lS = UnityEngine.Random.Range(0, lS);
            return AlwaceGrandChar(variation.прилагательные[lP], 'а', 'А') + " " + variation.существительные[lS];
        }
        return null;
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
}

[Serializable]
public class NameVariation
{
    public string[] прилагательные;
    public string[] существительные;
}
