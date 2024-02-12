using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : ButtonBase
{
    [SerializeField] PageBase nextPage;
    private void Start()
    {
        AddListener(onButt);
    }
    private void onButt()
    {
        Controller.me.SetPage(nextPage);
    }
}
