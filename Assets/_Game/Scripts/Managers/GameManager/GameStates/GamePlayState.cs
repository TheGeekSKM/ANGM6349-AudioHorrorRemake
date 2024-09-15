using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameBaseState
{
    public GamePlayState(GameManager gameManager) : base(gameManager) {}

    public override void OnEnter()
    {
        base.OnEnter();
        // Debug.Log("GamePlayState Enter");
        GamePlayUIController.Instance.GameHidePanel.SetActive(false);
    }

    public override void OnExit()
    {
        base.OnExit();
        // Debug.Log("GamePlayState Exit");
        GamePlayUIController.Instance.GameHidePanel.SetActive(true);
    }

}
