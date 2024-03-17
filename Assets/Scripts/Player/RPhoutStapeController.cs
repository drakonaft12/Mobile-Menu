using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPhoutStapeController : MonoBehaviour
{
    [SerializeField] ParticleSystem RingtP;
    [SerializeField] Sound sound;
    public void RingtPhout()
    {
        RingtP.Play();
        sound.PlaySound();
    }
}
