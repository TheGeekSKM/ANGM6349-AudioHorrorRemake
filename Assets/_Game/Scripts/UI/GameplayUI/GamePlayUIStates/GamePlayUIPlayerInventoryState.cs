using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIPlayerInventoryState : GamePlayUIBaseState
{
    InventoryDisplayController inventoryDisplayController;
    public GamePlayUIPlayerInventoryState(GamePlayUIController controller, InventoryDisplayController inventoryDisplayController) : base(controller)
    {
        this.inventoryDisplayController = inventoryDisplayController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.ShowPlayerInventoryPanel();

        inventoryDisplayController.OpenPlayerInventory();
    }

    public override void OnExit()
    {
        base.OnExit();

        inventoryDisplayController.ClosePlayerInventory();
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
