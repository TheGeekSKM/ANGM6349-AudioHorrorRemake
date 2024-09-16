
using System.Collections;
using SaiUtils.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject _soundParent;
    [Header("Settings")]
    [SerializeField] float _moveSpeed = 5;
    float _timeBetweenSteps = 0.5f;

    [Header("Events")]
    [SerializeField] BoolEvent _OnPlayerMove;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _moveDirection;
    [SerializeField, ReadOnly] bool _isMoving = false;
    public bool IsMoving => _isMoving;


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
            SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerFootstepSound, _soundParent.transform);
            yield return new WaitForSeconds(_timeBetweenSteps);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // stop the player when it collides with something that isn't the ground
        if (!other.gameObject.CompareTag("Ground")) Stop();
    }

    [Button]
    public void TurnLeft()
    {
        // rotate the _moveDirection to the left
        _moveDirection = new Vector3(-_moveDirection.z, 0, _moveDirection.x);
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound, _soundParent.transform);
    }

    [Button]
    public void TurnRight()
    {
        // rotate the _moveDirection to the right
        _moveDirection = new Vector3(_moveDirection.z, 0, -_moveDirection.x);
        SoundManager.Instance.PlaySound(transform, SoundAtlas.Instance.PlayerTurnSound, _soundParent.transform);
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
        GamePlayUIController.Instance.AddNotification("Ah shit, I'm slow again...");
        
        _moveSpeed -= speedBoost;
        _timeBetweenSteps *= 2;
    }

    void FixedUpdate()
    {
        if (_isMoving) _rigidbody.velocity = _moveDirection * _moveSpeed;
        else _rigidbody.velocity = Vector3.zero;
    }
}
