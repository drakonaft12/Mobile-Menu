using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPhoutStapeController : MonoBehaviour
{
    [SerializeField] ParticleSystem LeftP;
    [SerializeField] Sound sound;
    public void LeftPhout()
    {
        LeftP.Play();
        sound.PlaySound();
    }
}
