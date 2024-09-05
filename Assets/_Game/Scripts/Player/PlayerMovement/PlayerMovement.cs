using GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody _rigidbody;

    [Header("Events")]
    [SerializeField] BoolEvent _OnPlayerMove;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _moveDirection;
    [SerializeField, ReadOnly] bool _isMoving = false;
    public bool IsMoving => _isMoving;

    void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // set the initial move direction to forward
        _moveDirection = new Vector3(0, 0, -1);
    }

    [Button]
    public void Move()
    {
        _isMoving = true;
        _OnPlayerMove.Raise(_isMoving);
    }

    [Button]
    public void Stop()
    {
        _isMoving = false;
        _OnPlayerMove.Raise(_isMoving);
    }

    void OnCollisionEnter(Collision other)
    {
        // stop the player when it collides with something
        Stop();
    }

    [Button]
    public void TurnLeft()
    {
        // rotate the _moveDirection to the left
        _moveDirection = new Vector3(-_moveDirection.z, 0, _moveDirection.x);
    }

    [Button]
    public void TurnRight()
    {
        // rotate the _moveDirection to the right
        _moveDirection = new Vector3(_moveDirection.z, 0, -_moveDirection.x);
    }

    void FixedUpdate()
    {
        if (_isMoving) _rigidbody.velocity = _moveDirection * 5;
        else _rigidbody.velocity = Vector3.zero;
    }
}
