using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        GameManager.Instance.GameStateMachine.ChangeState(GameManager.Instance.GamePlayState);
    }
}
