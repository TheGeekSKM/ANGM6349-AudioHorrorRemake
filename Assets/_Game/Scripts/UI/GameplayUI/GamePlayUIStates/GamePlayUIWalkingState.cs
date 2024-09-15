using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIWalkingState : GamePlayUIBaseState
{
    public GamePlayUIWalkingState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, panel, onScreenPos, offScreenPos, enableAnimations)
    {
    }
}

