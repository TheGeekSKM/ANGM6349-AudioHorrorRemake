using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCutsceneState : GameBaseState
{
    DialogueSceneSO _dialogueScene;
    public GameCutsceneState(GameManager gameManager, SceneReference sceneReference, DialogueSceneSO dialogueSceneSO = null) : base(gameManager, sceneReference) {
        _dialogueScene = dialogueSceneSO;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_dialogueScene) CutsceneManager.Instance.SetDialogue(_dialogueScene);
    }

}

