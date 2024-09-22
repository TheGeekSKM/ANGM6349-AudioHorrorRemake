using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecordsData", menuName = "Records/RecordsData")]
public class RecordsData : ScriptableObject
{
    [SerializeField] string _recordsName;
    [SerializeField, TextArea(10, 25)] string _recordsTranscript;
    [SerializeField] AudioClip _recordsAudioClip;

    public string RecordsName => _recordsName;
    public string RecordsTranscript => _recordsTranscript;
    public AudioClip RecordsAudioClip => _recordsAudioClip;
    public float RecordsAudioClipLength => _recordsAudioClip.length;
}
