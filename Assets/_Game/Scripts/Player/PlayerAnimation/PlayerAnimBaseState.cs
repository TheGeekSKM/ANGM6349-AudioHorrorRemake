using SaiUtils.StateMachine;
using UnityEngine;

public class PlayerAnimBaseState : IState
{
    protected Animator _animator;
    protected int _animationStateHash;

    protected float _enterTime;

    public PlayerAnimBaseState(Animator animator, int animationStateHash, float enterTime = 0.0f)
    {
        _animator = animator;
        _animationStateHash = animationStateHash;
        _enterTime = enterTime;
    }

    public virtual void OnEnter()
    {
        _animator.CrossFade(_animationStateHash, _enterTime);
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void OnExit() { }
}
