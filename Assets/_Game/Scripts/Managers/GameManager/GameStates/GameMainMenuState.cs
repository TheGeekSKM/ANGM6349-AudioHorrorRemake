using Eflatun.SceneReference;

public class GameMainMenuState : GameBaseState
{
    public GameMainMenuState(GameManager gameManager, SceneReference sceneReference) : base(gameManager, sceneReference) { }

    public override void OnEnter()
    {
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.MainMenu);
        base.OnEnter();
    }

}

