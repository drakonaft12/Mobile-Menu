using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicker : ButtonBase
{
    private float zarabotak = 1;
    private void Start()
    {
        AddListener(onButt);
    }
    private void onButt()
    {
        PlayerStats.me.Zarabotak(zarabotak);
    }
}
