using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameBaseState
{
    public GamePlayState(GameManager gameManager) : base(gameManager) {}

    public override void OnEnter()
    {
        Debug.Log("GamePlayState OnEnter");
        _gameManager.OnGameStateChange?.Invoke(GameStateEnum.Play);
        GamePlayUIController.Instance.GameHidePanel.SetActive(false);
        base.OnEnter();
        // Debug.Log("GamePlayState Enter");
    }

    public override void OnExit()
    {
        GamePlayUIController.Instance.GameHidePanel.SetActive(true);
        PlayerController.Instance.PlayerMovement.Stop(PlayerStopType.UIStop);
        base.OnExit();
        // Debug.Log("GamePlayState Exit");
    }

}
