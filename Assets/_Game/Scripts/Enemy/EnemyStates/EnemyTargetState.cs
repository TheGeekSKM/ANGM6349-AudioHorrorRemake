using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetState : State
{
    EnemyController _enemyController;
    EnemyFSM _enemyFSM;

    public EnemyTargetState(EnemyFSM enemyFSM, EnemyController enemyController)
    {
        _enemyFSM = enemyFSM;
        _enemyController = enemyController;
    }

    public override void Tick()
    {
        if (_enemyController.CurrentTarget == null)
        {
            _enemyController.TriggerIdleState();
        }
    }

}
