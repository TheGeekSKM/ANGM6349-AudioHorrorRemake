using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIPlayerInventoryState : GamePlayUIBaseState
{
    InventoryDisplayController inventoryDisplayController;
    public GamePlayUIPlayerInventoryState(GamePlayUIController controller, InventoryDisplayController inventoryDisplayController, 
        RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, panel, onScreenPos, offScreenPos, enableAnimations)
    {
        this.inventoryDisplayController = inventoryDisplayController;
    }

    public override void OnEnter()
    {
        base.OnEnter();

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
