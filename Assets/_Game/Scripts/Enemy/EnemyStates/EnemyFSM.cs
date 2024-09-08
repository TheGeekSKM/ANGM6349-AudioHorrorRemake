using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachineMB
{
    EnemyController _enemyController;

    public EnemyIdleState EnemyIdleState { get; private set; }
    public EnemyTargetState EnemyTargetState { get; private set; }
    public EnemyAttackState EnemyAttackState { get; private set; }

    void OnValidate() 
    {
        if (_enemyController == null) _enemyController = GetComponent<EnemyController>();
    }

    void Awake() 
    {
        EnemyIdleState = new EnemyIdleState(this, _enemyController);
        EnemyTargetState = new EnemyTargetState(this, _enemyController);
        EnemyAttackState = new EnemyAttackState(this, _enemyController);
    }

    void Start() 
    {
        ChangeState(EnemyIdleState);
    }


}
