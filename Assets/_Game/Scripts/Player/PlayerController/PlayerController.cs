using System.Collections;
using System.Collections.Generic;
using SaiUtils.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement => _playerMovement;

    [SerializeField] RoomController _roomController;
    public RoomController RoomController => _roomController;

    [SerializeField] InventoryController _inventoryController;
    public InventoryController InventoryController => _inventoryController;

    [SerializeField] bool _isCrouching;
    public bool IsCrouching => _isCrouching;

    StateMachine _playerMotionStateMachine;
    public StateMachine PlayerMotionStateMachine => _playerMotionStateMachine;

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
        var crouchState = new PlayerCrouchState(this);

        // Add the states to the state machine
        _playerMotionStateMachine.AddAnyTransition(idleState, new FuncPredicate(() => !_playerMovement.IsMoving && !_isCrouching));

        _playerMotionStateMachine.AddTransition(idleState, crouchState, new FuncPredicate(() => _isCrouching));

        _playerMotionStateMachine.AddTransition(idleState, moveState, new FuncPredicate(() => _playerMovement.IsMoving));


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

    [Button]
    public void SetCrouchState(bool crouch) // this can be called from any other script to set the crouch state
    {
        _isCrouching = crouch;
    }

    public void SetCrouch(bool crouch) // this is only meant to be called from the PlayerCrouchState
    {
        var playerCollider = _playerMovement.GetComponent<Collider>();
        var playerRigidbody = _playerMovement.GetComponent<Rigidbody>();

        if (crouch)
        {
            playerRigidbody.useGravity = false;
            playerCollider.isTrigger = true;
        }
        else
        {
            playerRigidbody.useGravity = true;
            playerCollider.isTrigger = false;
        }
    }


}
