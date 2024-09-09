using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIWalkingState : GamePlayUIBaseState
{
    public GamePlayUIWalkingState(GamePlayUIController controller) : base(controller)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowWalkingPanel();

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

