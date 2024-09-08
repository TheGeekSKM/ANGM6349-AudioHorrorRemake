using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndState : GameBaseState
{
    public GameEndState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _gameManager.LoadEndScene();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

