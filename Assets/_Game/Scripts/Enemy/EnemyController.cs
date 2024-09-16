using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using SaiUtils.StateMachine;

public class EnemyController : MonoBehaviour
{

    public static EnemyController Instance { get; private set; }

    [Header("References")]
    [SerializeField] NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    [SerializeField] GameObject _damagePrefab;

    [Header("Settings")]
    [SerializeField] float _attackRange = 1f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] float _wanderRadius = 10f;
    [SerializeField] float _damage = 10f;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _direction;
    public Vector3 Direction 
    {
        get => _direction;
        set => _direction = value;
    }

    [SerializeField] Transform _currentTarget;
    public Transform CurrentTarget => _currentTarget;

    StateMachine _enemyStateMachine;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyTargetState TargetState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ConfigureStateMachine();
    }

    void ConfigureStateMachine()
    {
        _enemyStateMachine = new StateMachine();

        IdleState = new EnemyIdleState(this, _navMeshAgent, _wanderRadius, new Vector2(10, 15));
        TargetState = new EnemyTargetState(this, _navMeshAgent, PlayerController.Instance.PlayerTransform);
        AttackState = new EnemyAttackState(this, _navMeshAgent, _damagePrefab, _attackCooldown);

        // switch to idle if player is out of range, or if the player is not the current target, or if there is no current target
        _enemyStateMachine.AddAnyTransition(IdleState, new FuncPredicate(() => 
            Vector3.Distance(transform.position, PlayerController.Instance.PlayerTransform.position) > _attackRange &&
            (CurrentTarget != PlayerController.Instance.PlayerTransform || CurrentTarget == null)
        ));

        // switch to target if sound is heard, this must be done manually
        _enemyStateMachine.AddTransition(IdleState, TargetState, new BlankPredicate());

        // switch to attack if player is in range
        _enemyStateMachine.AddTransition(TargetState, AttackState, new FuncPredicate(() => 
            Vector3.Distance(transform.position, PlayerController.Instance.PlayerTransform.position) <= _attackRange)
        );

        // switch to idle if player is out of range (mimics confusion)
        _enemyStateMachine.AddTransition(AttackState, IdleState, new FuncPredicate(() => 
            Vector3.Distance(transform.position, PlayerController.Instance.PlayerTransform.position) > _attackRange)
        );

        _enemyStateMachine.SetState(IdleState);
    }

    public void TriggerTargetState(Transform target)
    {
        _currentTarget = target;
        _enemyStateMachine.ChangeState(TargetState);
    }

    void Update()
    {
        _enemyStateMachine.Update();
    }

    void FixedUpdate()
    {
        _enemyStateMachine.FixedUpdate();        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _wanderRadius);
    }

    void OnCollisionEnter(Collision other)
    {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damage);
            SoundManager.Instance.PlayEnemySound(transform, SoundAtlas.Instance.MonsterGrowlSound);
        }
    }
    

}

