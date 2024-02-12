using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMoney : MonoBehaviour
{
    private TextMeshProUGUI textMoney;
    private void Awake()
    {
        textMoney = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int t = (int)PlayerStats.me.GetMoney();
        textMoney.text ="money: "+ t;
    }
}
