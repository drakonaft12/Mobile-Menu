using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats me;
    private float money;

    private void Awake()
    {
        if (me == null)
            me = this;
        money = PlayerPrefs.GetFloat("money", 0);

    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("money", money);
    }

    public bool ShopItem(float _money)
    {
        if (money < _money) return false;
        else { money -= _money; return true; }
    }

    public void Zarabotak(float _money)
    {
        money += _money;
    }

    public float GetMoney()
    {
        return money;
    }
}
