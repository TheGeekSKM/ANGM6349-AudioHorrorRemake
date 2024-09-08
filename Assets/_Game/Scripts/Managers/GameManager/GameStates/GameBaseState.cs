using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;

public class GameBaseState : IState
{
    protected GameManager _gameManager;

    public GameBaseState(GameManager gameManager)
    {
        _gameManager = gameManager;
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
