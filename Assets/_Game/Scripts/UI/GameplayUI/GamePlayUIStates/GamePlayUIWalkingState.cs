using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIWalkingState : GamePlayUIBaseState
{
    public GamePlayUIWalkingState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos) : 
        base(controller, panel, onScreenPos, offScreenPos)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // Set the player's movement state to walking
        PlayerController.Instance.PlayerMovement.Move();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        base.Update();

        // if player stops moving, change state to default
        if (PlayerController.Instance.PlayerMovement.IsMoving == false)
        {
            Controller.ChangeUIState(GamePlayUIState.Default);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

