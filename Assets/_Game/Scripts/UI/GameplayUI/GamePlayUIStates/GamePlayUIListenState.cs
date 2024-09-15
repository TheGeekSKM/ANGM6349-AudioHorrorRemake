using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIListenState : GamePlayUIBaseState
{
    public GamePlayUIListenState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, panel, onScreenPos, offScreenPos, enableAnimations)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

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

