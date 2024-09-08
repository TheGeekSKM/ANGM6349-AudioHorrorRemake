using System.Collections;
using System.Collections.Generic;
using SaiUtils.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement => _playerMovement;

    StateMachine _playerMotionStateMachine;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        ConfigureStateMachine();
        
    }

    void ConfigureStateMachine()
    {
        // Create the state machine
        _playerMotionStateMachine = new StateMachine();

        // Create the states
        var idleState = new PlayerIdleState(this);
        var moveState = new PlayerMoveState(this);

        // if the player is moving, go to the move state
        _playerMotionStateMachine.AddTransition(idleState, moveState, new FuncPredicate(() => _playerMovement.IsMoving));
        // if the player is not moving, go to the idle state 
        _playerMotionStateMachine.AddTransition(moveState, idleState, new FuncPredicate(() => !_playerMovement.IsMoving)); 

        // Set the initial state
        _playerMotionStateMachine.SetState(idleState);
    }

    void Update()
    {
        _playerMotionStateMachine.Update();
    }

    void FixedUpdate()
    {
        _playerMotionStateMachine.FixedUpdate();
    }


}
