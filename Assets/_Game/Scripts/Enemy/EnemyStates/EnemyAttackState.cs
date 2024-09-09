using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    float _attackTimer = 0f;
    float _timeBetweenAttacks = 1f;
    GameObject _damagePrefab;

    float _agentSpeed;
    public EnemyAttackState(EnemyController controller, NavMeshAgent agent, GameObject damagePrefab, float timeBetweenAttacks) : base(controller, agent)
    {
        _damagePrefab = damagePrefab;
        _timeBetweenAttacks = timeBetweenAttacks;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _attackTimer = 0f;

        _agentSpeed = Agent.speed;
        Agent.speed = 0f;

        Debug.Log("Attack");
    }

    public override void OnExit()
    {
        base.OnExit();

        Agent.speed = _agentSpeed;
    }

    public override void Update()
    {
        base.Update();

        if (!_damagePrefab) return; 
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _timeBetweenAttacks)
        {
            _attackTimer = 0f;

            var damage = GameObject.Instantiate(_damagePrefab, Controller.transform.position, Quaternion.identity);
        }
    }
}
