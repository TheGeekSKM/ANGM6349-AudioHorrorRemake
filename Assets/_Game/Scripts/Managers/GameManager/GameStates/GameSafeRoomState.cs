using Eflatun.SceneReference;
using UnityEngine;

public class GameSafeRoomState : GameBaseState
{
    public GameSafeRoomState(GameManager gameManager, SceneReference sceneReference) : base(gameManager, sceneReference) { }

    public override void OnEnter()
    {
        Debug.Log("GameSafeRoomState OnEnter");
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.SafeRoom);
        EnvironmentController.Instance.MovePlayerToSpawn();
        base.OnEnter();
    }
}