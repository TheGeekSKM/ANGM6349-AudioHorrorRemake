using Eflatun.SceneReference;
using UnityEngine;

public class GameCutsceneState : GameBaseState
{
    DialogueSceneSO _dialogueScene;
    public GameCutsceneState(GameManager gameManager, SceneReference sceneReference, DialogueSceneSO dialogueSceneSO = null) : base(gameManager, sceneReference) {
        _dialogueScene = dialogueSceneSO;
    }

    public override void OnEnter()
    {
        // if (_dialogueScene) CutsceneManager.Instance.SetDialogue(_dialogueScene);
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.Cutscene);
        Debug.Log("GameCutsceneState OnEnter");
        base.OnEnter();
    }

}

