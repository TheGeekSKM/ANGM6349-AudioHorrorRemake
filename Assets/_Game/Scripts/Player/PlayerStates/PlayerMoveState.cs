using UnityEngine;
using SaiUtils.StateMachine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController playerController) : base(playerController) {

    }

    public override void OnEnter()
    {
        // Debug.Log("PlayerMoveState OnEnter");
    //    _playerController.PlayerMovement.Move();

        GamePlayUIController.Instance.AddNotification("<b>You:</b> I started walking...");
        _playerController.EnemySpeed.Value = 2f;
    }

    public override void OnExit()
    {
        // GamePlayUIController.Instance.AddNotification("<b>You:</b> I stopped walking...I must've hit a wall or something.");
        _playerController.EnemySpeed.Value = 0f;
    }
}
