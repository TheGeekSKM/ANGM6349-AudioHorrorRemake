using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/Dialogue Line")]
public class DialogueLineSO : ScriptableObject
{
    [Header("Text")] 
    [SerializeField] private string _characterName;
    [SerializeField, TextArea(10, 20)] private string _line;

    public string CharacterName => _characterName;
    public string Line => _line;

    [Header("Audio")]
    [SerializeField] private AudioClip _voiceClip;
    public AudioClip VoiceClip => _voiceClip;
    public float Duration => _voiceClip ? _voiceClip.length : 1f;

}
