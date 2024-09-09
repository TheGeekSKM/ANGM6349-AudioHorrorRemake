using UnityEngine;
using SaiUtils.StateMachine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController playerController) : base(playerController) {

    }

    public override void OnEnter()
    {
        // Debug.Log("PlayerMoveState OnEnter");
       _playerController.PlayerMovement.Move();
    }
}
