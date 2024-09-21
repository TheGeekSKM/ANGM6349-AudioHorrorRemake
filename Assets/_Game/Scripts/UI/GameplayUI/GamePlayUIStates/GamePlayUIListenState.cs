using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIListenState : GamePlayUIBaseState
{
    public GamePlayUIListenState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations) : 
        base(controller, panel, onScreenPos, offScreenPos, enableAnimations)
    {
    }

    public override void OnEnter()
    {
        var enemyPos = EnemyController.Instance.transform.position;
        var playerPos = PlayerController.Instance.transform.position;

        var distance = Vector3.Distance(enemyPos, playerPos);

        var finalDist = Mathf.RoundToInt(distance / 5) * 5;

        Controller.AddNotification($"<b>You: </b> I think the creature is...maybe {finalDist} feet away from me.");

        base.OnEnter();

        // make the player visible
        PlayerController.Instance.PlayerArtDisplay.SetPlayerArtActive();
    }

    public override void OnExit()
    {
        base.OnExit();

        // make the player invisible
        PlayerController.Instance.PlayerArtDisplay.SetPlayerArtInactive();
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

