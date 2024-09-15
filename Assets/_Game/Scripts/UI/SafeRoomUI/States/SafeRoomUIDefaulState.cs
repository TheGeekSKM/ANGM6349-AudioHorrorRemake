using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomUIDefaultState : SafeRoomUIBaseState
{
    public SafeRoomUIDefaultState(SafeRoomController safeRoomController, RectTransform _null, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(safeRoomController, _null, onScreenPos, offScreenPos, enableAnimations)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnExit()
    {
        base.OnExit();

    }

    void OnQuestButtonClicked()
    {
    }

    void OnCraftButtonClicked()
    {
    }

    void OnNotePadButtonClicked()
    {
    }
}

