using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "SoundSystem/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip[] Sounds;

    public AudioClip GetRandomSound()
    {
        return Sounds[Random.Range(0, Sounds.Length)];
    }

    [Range(0, 1)] public float Volume;
}
