using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        GameManager.Instance.GameStateMachine.ChangeState(GameManager.Instance.GameCutsceneState);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
