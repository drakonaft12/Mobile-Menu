using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats me;
    private float money;

    private float moneyAdd = 1;
    public float speed = 5, sprintSpeed = 14, climbingSpeed = 1, heithJump = 6, maxStamina = 100, staminaPerTime = 0.3f, staminaSprint = 0.6f, stamina—limbing = 0.4f, staminaJump = 20, speedX = 2, speedY = 2;
    public void AddZar(float i)
    {
        moneyAdd += i;
    }

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

    public void AddParPlayer(ItemBase item, float mult)
    {
        speed += item.speedPlayer * mult;
        sprintSpeed+=item.sprintSpeedPlayer * mult; 
        climbingSpeed+= item.zalesSpeedPlayer * mult; 
        heithJump+=item.heithJumpPlayer * mult;
        maxStamina += item.maxStaminaPlayer * mult ; 
        staminaPerTime+=item.staminaPerTimePlayer * mult; 
        staminaSprint+=item.staminaSprintPlayer * mult; 
        stamina—limbing+=item.staminaZalesaniePlayer * mult; 
        staminaJump+=item.staminaJumpPlayer * mult;
    }
    public void Zarabotak(float _money)
    {
        money += _money;
    }
    public void Zarabotak()
    {
        money += moneyAdd;
    }

    public float GetMoney()
    {
        return money;
    }
}
