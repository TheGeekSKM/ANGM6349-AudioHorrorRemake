using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIRoomInventoryState : GamePlayUIBaseState
{
    RoomInventoryController roomInventoryController;
    public GamePlayUIRoomInventoryState(GamePlayUIController controller, RoomInventoryController roomInventoryController) : base(controller)
    {
        this.roomInventoryController = roomInventoryController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowRoomInventoryPanel();

        roomInventoryController.OpenRoomInventory();
    }

    public override void OnExit()
    {
        base.OnExit();

        roomInventoryController.CloseRoomInventory();
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
