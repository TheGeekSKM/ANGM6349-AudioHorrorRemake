using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GameCutsceneState, 0.2f);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
