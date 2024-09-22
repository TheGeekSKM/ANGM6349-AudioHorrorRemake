using System.Collections;
using UnityEngine;

public class ThrowableItemSpawnController : MonoBehaviour
{
    [SerializeField] private ThrowableItemData _throwableItemData;
    [SerializeField] float _timeToDestroy = 5f;
    [SerializeField] float _speedFactor = 0.5f;
    [SerializeField] float _damageAtEnd = 10f;
    [SerializeField] float _damageOverTime = 5f;
    [SerializeField] float _timeBetweenDamage = 1f;
    [SerializeField] RoomTrigger _roomTrigger;

    float _timeBetweenAttacks;
    Coroutine _destroyCoroutine;
    EnemyHealth _enemyHealth;
    EnemyController _enemyController;

    PlayerHealth _playerHealth;
    PlayerMovement _playerMovement;


    public void Initialize(ThrowableItemData throwableItemData, RoomTrigger roomTrigger)
    {
        _throwableItemData = throwableItemData;
        _timeToDestroy = _throwableItemData.TimeToDestroy;
        _speedFactor = _throwableItemData.SpeedFactor;
        _damageAtEnd = _throwableItemData.DamageAtEnd;
        _damageOverTime = _throwableItemData.DamageOverTime;
        _timeBetweenDamage = _throwableItemData.TimeBetweenDamage;

        //this object should be the same size as the RoomTrigger's collider
        _roomTrigger = roomTrigger;
        transform.localScale = roomTrigger.GetComponent<BoxCollider>().size;

        if (_destroyCoroutine != null) StopCoroutine(_destroyCoroutine);
        _destroyCoroutine = StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {

            _enemyHealth = enemyHealth;
            _enemyController = other.GetComponent<EnemyController>();
            _enemyController.NavMeshAgent.speed *= _speedFactor;
        }
        else
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                _playerHealth = playerHealth;
                _playerMovement = other.GetComponent<PlayerMovement>();
                _playerMovement.MoveSpeed *= _speedFactor;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            _enemyController.NavMeshAgent.speed /= _speedFactor;
            _enemyHealth = null;
            _enemyController = null;
        }
        else
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                _playerMovement.MoveSpeed /= _speedFactor;
                _playerHealth = null;
                _playerMovement = null;
            }
        }
    }

    void Update()
    {
        if (_enemyHealth)
        {
            _timeBetweenAttacks += Time.deltaTime;

            if (_timeBetweenAttacks >= _timeBetweenDamage)
            {
                _enemyHealth.TakeDamage(Mathf.RoundToInt(_damageOverTime));
                _timeBetweenAttacks = 0;
            }
        }

        if (_playerHealth)
        {
            _timeBetweenAttacks += Time.deltaTime;

            if (_timeBetweenAttacks >= _timeBetweenDamage)
            {
                _playerHealth.TakeDamage(Mathf.RoundToInt(_damageOverTime));
                _timeBetweenAttacks = 0;
            }
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        if (_enemyController) _enemyController.NavMeshAgent.speed /= _speedFactor;
        if (_enemyHealth) _enemyHealth.TakeDamage(Mathf.RoundToInt(_damageAtEnd));
        if (_playerMovement) _playerMovement.MoveSpeed /= _speedFactor;
        if (_playerHealth) _playerHealth.TakeDamage(Mathf.RoundToInt(_damageAtEnd));
        _roomTrigger.RoomData.RemoveItem(_throwableItemData);
        Destroy(gameObject);
    }



}
