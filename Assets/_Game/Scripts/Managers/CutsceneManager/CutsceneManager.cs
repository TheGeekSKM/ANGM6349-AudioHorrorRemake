using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public static CutsceneManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    [SerializeField] DialogueSceneSO _currentDialogueScene;
    public DialogueSceneSO CurrentDialogueScene => _currentDialogueScene;

    public void SetDialogue(DialogueSceneSO dialogueSceneSO) {
        _currentDialogueScene = dialogueSceneSO;
    }

    public void ClearDialogue()
    {
        _currentDialogueScene = null;
    }
}
