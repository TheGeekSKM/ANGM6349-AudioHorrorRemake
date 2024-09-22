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
        _uiPanel.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        _uiPanel.gameObject.SetActive(false);
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

