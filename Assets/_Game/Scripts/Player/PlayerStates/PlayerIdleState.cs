using UnityEngine;
using SaiUtils.StateMachine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController) {

    }

    public override void OnEnter()
    {
        // Debug.Log("PlayerIdleState OnEnter");
       _playerController.PlayerMovement.Stop();
    }
}
