using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealsHUD : MonoBehaviour
{
    [SerializeField] Slider slider;
    List<BotController> bots;
    public static StealsHUD me;
    

    private void Awake()
    {
        if (me == null)
            me = this;
        bots = new List<BotController>();
    }
    public void BotAdd(BotController bot) { bots.Add(bot);  }

    private void Update()
    {
        slider.value = MaxStat();
    }
    float MaxStat()
    {
        var pr = 0f;
        foreach (var item in bots)
        {
            if (item.ThisSteals > pr) pr = item.ThisSteals;
        }
        return pr;
    }

}
