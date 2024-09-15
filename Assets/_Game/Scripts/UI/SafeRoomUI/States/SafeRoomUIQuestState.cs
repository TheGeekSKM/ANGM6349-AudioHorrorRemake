using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomUIQuestState : SafeRoomUIBaseState
{
    public SafeRoomUIQuestState(SafeRoomController controller, RectTransform uiPanel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, uiPanel, onScreenPos, offScreenPos, enableAnimations)
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

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
