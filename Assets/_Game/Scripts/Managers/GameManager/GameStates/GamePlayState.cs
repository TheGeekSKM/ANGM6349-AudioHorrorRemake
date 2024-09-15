using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameBaseState
{
    public GamePlayState(GameManager gameManager) : base(gameManager) {}

    public override void OnEnter()
    {
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.Play);
        GamePlayUIController.Instance.GameHidePanel.SetActive(false);
        base.OnEnter();
        // Debug.Log("GamePlayState Enter");
    }

    public override void OnExit()
    {
        GamePlayUIController.Instance.GameHidePanel.SetActive(true);
        base.OnExit();
        // Debug.Log("GamePlayState Exit");
    }

}
