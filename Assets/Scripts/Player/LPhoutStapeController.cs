using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPhoutStapeController : MonoBehaviour
{
    [SerializeField] ParticleSystem LeftP, RingtP;
    [SerializeField] Sound sound;
    public virtual void LeftPhout()
    {
        LeftP.Play();
        sound.PlaySound();
    }
    public virtual void RingtPhout()
    {
        RingtP.Play();
        sound.PlaySound();
    }
}
