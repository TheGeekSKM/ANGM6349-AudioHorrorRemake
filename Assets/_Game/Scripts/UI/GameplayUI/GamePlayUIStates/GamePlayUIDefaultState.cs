using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIDefaultState : GamePlayUIBaseState
{
    public GamePlayUIDefaultState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, panel, onScreenPos, offScreenPos, enableAnimations) { }

    public override void OnEnter()
    {
        base.OnEnter();

        // Set the player's movement state to default
        // if (PlayerController.Instance) PlayerController.Instance.PlayerMovement.Stop();
    }

    public override void OnExit()
    {
        base.OnExit();
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
