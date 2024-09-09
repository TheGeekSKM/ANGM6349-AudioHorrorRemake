using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIPlayerInventoryState : GamePlayUIBaseState
{
    public GamePlayUIPlayerInventoryState(GamePlayUIController controller) : base(controller)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowPlayerInventoryPanel();
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
