using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBase
{
    public void AnimRead(bool iz)
    {
        animator.isReadMobile = iz;
    }
}
