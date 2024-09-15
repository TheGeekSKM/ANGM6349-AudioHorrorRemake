using UnityEngine;
using SaiUtils.StateMachine;
using DG.Tweening;

public class GamePlayUIBaseState : IState
{
    protected GamePlayUIController _controller;
    protected RectTransform _panel;
    protected Vector2 _onScreenPos;
    protected Vector2 _offScreenPos;
    private GamePlayUIController controller;

    protected GamePlayUIController Controller => _controller;
    protected RectTransform Panel => _panel;
    protected Vector2 OnScreenPos => _onScreenPos;
    protected Vector2 OffScreenPos => _offScreenPos;

    public GamePlayUIBaseState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos)
    {
        _controller = controller;
        _panel = panel;
        _onScreenPos = onScreenPos;
        _offScreenPos = offScreenPos;
    }

    public GamePlayUIBaseState(GamePlayUIController controller)
    {
        this.controller = controller;
    }

    public virtual void OnEnter()
    {
        if (!Panel) return;
        Panel.anchoredPosition = OffScreenPos;
        Panel.DOAnchorPos(OnScreenPos, 0.3f).SetEase(Ease.OutExpo);
    }

    public virtual void OnExit()
    {
        if (!Panel) return;
        Panel.DOAnchorPos(OffScreenPos, 0.3f).SetEase(Ease.InExpo);
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}
