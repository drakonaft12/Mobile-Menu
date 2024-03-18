using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Mobile-Menu/SoundArray")]
public class SoundArray : ScriptableObject
{
    [SerializeField] List<AudioClip> clips;

    public AudioClip SetClip => clips[Random.Range(0, clips.Count)];
}
