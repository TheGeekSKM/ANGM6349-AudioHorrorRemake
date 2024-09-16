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
    protected bool _enableAnimations;

    protected GamePlayUIController Controller => _controller;
    protected RectTransform Panel => _panel;
    protected Vector2 OnScreenPos => _onScreenPos;
    protected Vector2 OffScreenPos => _offScreenPos;

    public GamePlayUIBaseState(GamePlayUIController controller, RectTransform panel, Vector2 onScreenPos, Vector2 offScreenPos, bool enableAnimations = true)
    {
        _controller = controller;
        _panel = panel;
        _onScreenPos = onScreenPos;
        _offScreenPos = offScreenPos;
        _enableAnimations = enableAnimations;
    }

    public GamePlayUIBaseState(GamePlayUIController controller)
    {
        this.controller = controller;
    }

    public virtual void OnEnter()
    {
        if (!Panel) return;
        Panel.anchoredPosition = OffScreenPos;
        if (_enableAnimations) {
            Panel.DOAnchorPos(OnScreenPos, 0.3f).SetEase(Ease.OutExpo);
            SoundManager.Instance.PlaySound2D(SoundAtlas.Instance.WhooshSound);
        }
        else Panel.anchoredPosition = OnScreenPos;
    }

    public virtual void OnExit()
    {
        if (!Panel) return;
        
        if (_enableAnimations) Panel.DOAnchorPos(OffScreenPos, 0.3f).SetEase(Ease.OutExpo);
        else Panel.anchoredPosition = OffScreenPos;
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}
