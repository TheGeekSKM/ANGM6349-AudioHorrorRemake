using System.Collections;
using System.Collections.Generic;
using SaiUtils.StateMachine;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerController _playerController;

    public PlayerBaseState(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnExit()
    {
    }
}
