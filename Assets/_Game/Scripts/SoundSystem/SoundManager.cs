using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] GameObject _soundPrefab;
    [SerializeField] GameObject _enemySoundPrefab;

    public void PlaySound(Transform soundSource, SoundData soundData, Transform parent = null)
    {
        GameObject sound = Instantiate(_soundPrefab, soundSource.position, Quaternion.identity);
        sound.GetComponent<SoundController>().InitSound(soundData);
        
        if (parent != null) sound.transform.SetParent(parent);
        else sound.transform.SetParent(soundSource);
    }


    public void PlayEnemySound(Transform soundSource, SoundData soundData, Transform parent = null)
    {
        GameObject sound = Instantiate(_enemySoundPrefab, soundSource.position, Quaternion.identity);
        sound.GetComponent<SoundController>().InitSound(soundData);
        
        if (parent != null) sound.transform.SetParent(parent);
        else sound.transform.SetParent(soundSource);
    }


}
