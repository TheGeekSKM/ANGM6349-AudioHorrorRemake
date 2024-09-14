using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "Dialogue/Dialogue Scene")]
public class DialogueSceneSO : ScriptableObject
{
    [Header("Dialogue")]
    [SerializeField] private string _sceneName;
    [SerializeField] private List<DialogueLineSO> _dialogueLines;

    public string SceneName => _sceneName;
    public List<DialogueLineSO> DialogueLines => _dialogueLines;

    [Header("Audio")]
    [SerializeField] private AudioClip _backgroundMusic;
}

