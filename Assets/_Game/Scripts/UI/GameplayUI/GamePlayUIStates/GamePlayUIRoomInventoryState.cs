using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIRoomInventoryState : GamePlayUIBaseState
{
    public GamePlayUIRoomInventoryState(GamePlayUIController controller) : base(controller)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowRoomInventoryPanel();
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
