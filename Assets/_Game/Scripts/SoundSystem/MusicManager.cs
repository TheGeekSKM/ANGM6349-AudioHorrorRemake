using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SoundData music;

    [SerializeField] AudioSource _audioSource;

    void OnValidate()
    {
        if (_audioSource == null) _audioSource = gameObject.GetOrAdd<AudioSource>();
    }

    void Start()
    {
        _audioSource.PlayOneShot(music.GetRandomSound());
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(music.GetRandomSound());
        }
    }
}
