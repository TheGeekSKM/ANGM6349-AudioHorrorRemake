using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _playerController.SetCrouch(true);
        Debug.Log("Crouch");
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.SetCrouch(false);
    }

    
}

