using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;

public class GamePlayUIBaseState : IState
{
    protected GamePlayUIController _controller;
    protected GamePlayUIController Controller => _controller;

    public GamePlayUIBaseState(GamePlayUIController controller)
    {
        _controller = controller;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}
