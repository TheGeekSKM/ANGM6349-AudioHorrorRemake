using System;
using System.Collections;
using GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject _soundParent;

    [Header("Events")]
    [SerializeField] BoolEvent _OnPlayerMove;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _moveDirection;
    [SerializeField, ReadOnly] bool _isMoving = false;
    public bool IsMoving => _isMoving;


    Coroutine _moveSoundsRoutine;

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

    void Update()
    {
        _soundParent.transform.position = transform.position;
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
    public void Stop()
    {
        _isMoving = false;
        _OnPlayerMove.Raise(_isMoving);

        if (_moveSoundsRoutine != null)
        {
            StopCoroutine(_moveSoundsRoutine);
            _moveSoundsRoutine = null;
        }
    }

    IEnumerator MoveSoundsRoutine()
    {
        while (_isMoving)
        {
            SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerFootstepSound);
            yield return new WaitForSeconds(0.5f);
        }
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
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound);
    }

    [Button]
    public void TurnRight()
    {
        // rotate the _moveDirection to the right
        _moveDirection = new Vector3(_moveDirection.z, 0, -_moveDirection.x);
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound);
    }

    void FixedUpdate()
    {
        if (_isMoving) _rigidbody.velocity = _moveDirection * 5;
        else _rigidbody.velocity = Vector3.zero;
    }
}
