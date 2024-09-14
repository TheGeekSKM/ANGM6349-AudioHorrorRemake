using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

public class GameBaseState : IState
{
    protected GameManager _gameManager;
    protected SceneReference _sceneReference;

    public GameBaseState(GameManager gameManager, SceneReference sceneReference = null) {
        _gameManager = gameManager;
        _sceneReference = sceneReference;
    }

    public virtual void OnEnter() {
        if (_sceneReference != null) SceneManager.LoadSceneAsync(_sceneReference.BuildIndex, LoadSceneMode.Additive);
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void OnExit() {
        if (_sceneReference != null) 
        {
            SceneManager.UnloadSceneAsync(_sceneReference.BuildIndex);
            Debug.Log($"Unloaded {_sceneReference.Name}");
        }
    }
}
