using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    EnemyController _enemyController;
    EnemyFSM _enemyFSM;

    public EnemyAttackState(EnemyFSM enemyFSM, EnemyController enemyController)
    {
        _enemyFSM = enemyFSM;
        _enemyController = enemyController;
    }
}
