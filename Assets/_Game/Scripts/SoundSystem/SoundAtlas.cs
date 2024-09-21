using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAtlas : MonoBehaviour
{
    public static SoundAtlas Instance;

    [Header("Player Sound References")]
    public SoundData[] PlayerFootstepSounds;
    public SoundData[] PlayerTurnSounds;

    [Header("Monster Sound References")]
    public SoundData[] MonsterFootstepSounds;
    public SoundData[] MonsterGrowlSounds;

    [Header("UI Sound References")]
    public SoundData[] WhooshSounds;
    public SoundData[] PencilSounds;


    public SoundData PlayerFootstepSound
    {
        get
        {
            return PlayerFootstepSounds[Random.Range(0, PlayerFootstepSounds.Length - 1)];
        }
    }

    public SoundData PlayerTurnSound
    {
        get
        {
            return PlayerTurnSounds[Random.Range(0, PlayerTurnSounds.Length - 1)];
        }
    }

    public SoundData MonsterFootstepSound
    {
        get
        {
            return MonsterFootstepSounds[Random.Range(0, MonsterFootstepSounds.Length - 1)];
        }
    }

    public SoundData MonsterGrowlSound
    {
        get
        {
            return MonsterGrowlSounds[Random.Range(0, MonsterGrowlSounds.Length - 1)];
        }
    }

    public SoundData WhooshSound
    {
        get
        {
            return WhooshSounds[Random.Range(0, WhooshSounds.Length - 1)];
        }
    }

    public SoundData PencilSound
    {
        get
        {
            return PencilSounds[Random.Range(0, PencilSounds.Length - 1)];
        }
    }
 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
