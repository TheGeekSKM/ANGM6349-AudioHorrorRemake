using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIListenState : GamePlayUIBaseState
{
    public GamePlayUIListenState(GamePlayUIController controller) : base(controller)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowListenPanel();

        // make the player visible
        PlayerController.Instance.PlayerArtDisplay.SetPlayerArtActive();
    }

    public override void OnExit()
    {
        base.OnExit();

        // make the player invisible
        PlayerController.Instance.PlayerArtDisplay.SetPlayerArtInactive();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

