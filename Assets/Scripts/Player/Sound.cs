using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource dinamo;
    [SerializeField] SoundArray sound;
    public void PlaySound()
    {
        dinamo.PlayOneShot(sound.SetClip);
    }
}
