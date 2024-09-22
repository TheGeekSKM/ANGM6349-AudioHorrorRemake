using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomUIRecordsPlayState : SafeRoomUIBaseState
{
    RecordsDisplayController _recordsDisplayController;
    public SafeRoomUIRecordsPlayState(SafeRoomController controller, RectTransform uiPanel, Vector2 onScreenPos, 
        Vector2 offScreenPos, bool enableAnimations, RecordsDisplayController recordsDisplayController) : 
        base(controller, uiPanel, onScreenPos, offScreenPos, enableAnimations) {
            _recordsDisplayController = recordsDisplayController;
        }

    public override void OnEnter()
    {
        base.OnEnter();
        _recordsDisplayController.Initialize(_safeRoomController.RecordsData);
        
    }
}

