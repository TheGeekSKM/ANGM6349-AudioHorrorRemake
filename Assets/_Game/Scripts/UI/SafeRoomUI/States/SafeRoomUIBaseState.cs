using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;
using DG.Tweening;

public class SafeRoomUIBaseState : IState
{
    protected SafeRoomController _safeRoomController;
    protected RectTransform _uiPanel;
    protected Vector2 _onScreenPos;
    protected Vector2 _offScreenPos;

    protected bool _enableAnimations;

    public SafeRoomUIBaseState(SafeRoomController controller, RectTransform uiPanel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations = true)
    {
        _safeRoomController = controller;
        _uiPanel = uiPanel;
        _onScreenPos = onScreenPos;
        _offScreenPos = offScreenPos;
        _enableAnimations = enableAnimations;
    }

    public virtual void OnEnter()
    {
        if (!_uiPanel) return; 
        
        if (_enableAnimations) _uiPanel.DOAnchorPos(_onScreenPos, 0.3f).SetEase(Ease.OutExpo);
        else _uiPanel.anchoredPosition = _onScreenPos;
    }

    public virtual void OnExit()
    {
        if (!_uiPanel) return; 
        
        if (_enableAnimations) _uiPanel.DOAnchorPos(_offScreenPos, 0.3f).SetEase(Ease.OutExpo);
        else _uiPanel.anchoredPosition = _offScreenPos;
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

}
