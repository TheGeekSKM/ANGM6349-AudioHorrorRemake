using Eflatun.SceneReference;

public class GameSafeRoomState : GameBaseState
{
    public GameSafeRoomState(GameManager gameManager, SceneReference sceneReference) : base(gameManager, sceneReference) { }

    public override void OnEnter()
    {
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.SafeRoom);
        base.OnEnter();
    }
}