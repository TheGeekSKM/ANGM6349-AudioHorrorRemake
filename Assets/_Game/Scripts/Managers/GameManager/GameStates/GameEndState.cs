using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : GameBaseState
{
    SceneReference gameScene;
    public GameEndState(GameManager gameManager, SceneReference sceneReference, SceneReference gameScene) : base(gameManager, sceneReference) {
        this.gameScene = gameScene;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SceneManager.UnloadSceneAsync(gameScene.BuildIndex);
    }

}

