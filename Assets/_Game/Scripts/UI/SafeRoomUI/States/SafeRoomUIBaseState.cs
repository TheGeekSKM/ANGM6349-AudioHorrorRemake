using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;

public class SafeRoomUIBaseState : IState
{
    protected SafeRoomController _safeRoomController;
    protected GameObject _uiPanel;

    public SafeRoomUIBaseState(SafeRoomController controller, GameObject uiPanel)
    {
        _safeRoomController = controller;
        _uiPanel = uiPanel;
    }

    public virtual void OnEnter()
    {
        if (_uiPanel) _uiPanel.SetActive(true);
    }

    public virtual void OnExit()
    {
        if (_uiPanel) _uiPanel.SetActive(false);
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

}
