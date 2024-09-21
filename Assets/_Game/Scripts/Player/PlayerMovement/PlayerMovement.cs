
using System.Collections;
using System.Collections.Generic;
using SaiUtils.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

public enum PlayerDirection
{
    Up,
    Right,
    Down,
    Left
}

public enum PlayerStopType
{
    UIStop,
    HitWall,
    Null
}

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject _soundParent;
    [Header("Settings")]
    [SerializeField] float _moveSpeed = 5;
    float _timeBetweenSteps = 0.5f;
    [SerializeField] float raycastDistance = 2.0f;


    [Header("Events")]
    [SerializeField] BoolEvent _OnPlayerMove;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _moveDirection;
    public Vector3 MoveDirection => _moveDirection;
    [SerializeField, ReadOnly] bool _isMoving = false;
    public bool IsMoving => _isMoving;
    [SerializeField, ReadOnly] PlayerDirection _playerDirection = PlayerDirection.Down;
    public PlayerDirection PlayerDirection => _playerDirection; 

    Dictionary<PlayerDirection, string> directionMap = new();

    Coroutine _moveSoundsRoutine;
    Coroutine _increaseSpeedRoutine;

    void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // set the initial move direction to forward
        _moveDirection = new Vector3(0, 0, -1);

        directionMap.Add(PlayerDirection.Up, "north");
        directionMap.Add(PlayerDirection.Right, "east");
        directionMap.Add(PlayerDirection.Down, "south");
        directionMap.Add(PlayerDirection.Left, "west");

    }

    [Button]
    public void Move()
    {
        _isMoving = true;
        _OnPlayerMove.Raise(_isMoving);

        if (_moveSoundsRoutine == null)
        {
            _moveSoundsRoutine = StartCoroutine(MoveSoundsRoutine());
        }
    }

    [Button]
    public void Stop(PlayerStopType stopType)
    {
        _isMoving = false;
        _OnPlayerMove.Raise(_isMoving);

        if (_moveSoundsRoutine != null)
        {
            StopCoroutine(_moveSoundsRoutine);
            _moveSoundsRoutine = null;
        }

        switch (stopType)
        {
            case PlayerStopType.HitWall:
                GamePlayUIController.Instance.AddNotification("<b>You:</b> I stopped walking...I must've hit a wall or something.");
                break;
            case PlayerStopType.UIStop:
                GamePlayUIController.Instance.AddNotification("<b>You:</b> I stopped walking...");
                break;
            default:
                break;
        }
    }

    IEnumerator MoveSoundsRoutine()
    {
        while (_isMoving)
        {
            SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerFootstepSound, _soundParent.transform);
            yield return new WaitForSeconds(_timeBetweenSteps);
        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     // stop the player when it collides with something that isn't the ground
    //     if (!other.gameObject.CompareTag("Ground")) Stop();
    // }



    [Button]
    public void TurnLeft()
    {
        // rotate the _moveDirection to the left
        _moveDirection = new Vector3(-_moveDirection.z, 0, _moveDirection.x);
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound, _soundParent.transform);
        UpdatePlayerDirection();
    }

    [Button]
    public void TurnRight()
    {
        // rotate the _moveDirection to the right
        _moveDirection = new Vector3(_moveDirection.z, 0, -_moveDirection.x);
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound, _soundParent.transform);
        UpdatePlayerDirection();
    }

    void UpdatePlayerDirection()
    {
        if (_moveDirection == new Vector3(0, 0, -1)) _playerDirection = PlayerDirection.Down;
        else if (_moveDirection == new Vector3(0, 0, 1)) _playerDirection = PlayerDirection.Up;
        else if (_moveDirection == new Vector3(-1, 0, 0)) _playerDirection = PlayerDirection.Left;
        else if (_moveDirection == new Vector3(1, 0, 0)) _playerDirection = PlayerDirection.Right;

        Debug.Log("Player Direction: " + _playerDirection);

        GamePlayUIController.Instance.AddNotification("<b>You:</b> I think I just turned to face " + directionMap[_playerDirection] + "...");

        // rotate the player to face the new direction
        transform.rotation = Quaternion.LookRotation(_moveDirection);
    }

    public void BoostSpeed(float speedBoost, float duration)
    {
        if (_increaseSpeedRoutine != null) StopCoroutine(_increaseSpeedRoutine);
        _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine(speedBoost, duration));
    }

    IEnumerator IncreaseSpeedRoutine(float speedBoost, float duration)
    {
        _moveSpeed += speedBoost;
        _timeBetweenSteps /= 2;
        
        yield return new WaitForSeconds(duration);
        GamePlayUIController.Instance.AddNotification("<b>You:</b> Ah shit, I'm slow again...");
        
        _moveSpeed -= speedBoost;
        _timeBetweenSteps *= 2;
    }

    void OnDrawGizmosSelected()
    {
        // Set the color for the gizmo
        Gizmos.color = Color.red;

        // Draw the ray in the direction the player is facing
        Gizmos.DrawRay(transform.position, _moveDirection * raycastDistance);
    }

    void FixedUpdate()
    {
        if (_isMoving) 
        {
            _rigidbody.velocity = _moveDirection * _moveSpeed;

            // Launch a raycast in front of the player in the direction the player is facing
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _moveDirection, out hit, raycastDistance))
            {
                // If the raycast hits an object that is tagged "Ground", call Stop function
                if (hit.collider.CompareTag("Ground")) Stop(PlayerStopType.HitWall);
            }
        }
        else _rigidbody.velocity = Vector3.zero;
    }
}
