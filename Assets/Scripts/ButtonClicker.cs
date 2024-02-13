using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicker : ButtonBase
{
    private float zarabotak = 1;

    public float AddZar { get => zarabotak; set => zarabotak = value; }

    private void Start()
    {
        AddListener(onButt);
    }
    private void onButt()
    {
        PlayerStats.me.Zarabotak(zarabotak);
    }

    public override void OnDown(GameObject gameObject)
    {
        
    }
}
