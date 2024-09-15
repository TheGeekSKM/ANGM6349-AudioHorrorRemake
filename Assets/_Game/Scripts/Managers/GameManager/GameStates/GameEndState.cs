using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

public class GameEndState : GameBaseState
{
    SceneReference gameScene;
    public GameEndState(GameManager gameManager, SceneReference sceneReference, SceneReference gameScene) : base(gameManager, sceneReference) {
        this.gameScene = gameScene;
    }

    public override void OnEnter()
    {
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.End);
        SceneManager.UnloadSceneAsync(gameScene.BuildIndex);
        base.OnEnter();
    }

}

