using SaiUtils.StateMachine;
using UnityEngine;

public class PlayerWalkAnimState : PlayerAnimBaseState
{
    public PlayerWalkAnimState(Animator animator, int animationStateHash, float enterTime = 0.0f) : base(animator, animationStateHash, enterTime)
    {
    }
}
