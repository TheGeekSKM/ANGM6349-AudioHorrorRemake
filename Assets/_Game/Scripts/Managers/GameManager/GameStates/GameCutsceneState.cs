using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCutsceneState : GameBaseState
{
    public GameCutsceneState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _gameManager.LoadCutsceneScene();
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

