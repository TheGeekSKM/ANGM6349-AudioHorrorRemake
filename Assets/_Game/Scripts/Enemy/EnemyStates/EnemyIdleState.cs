using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    EnemyController _enemyController;
    EnemyFSM _enemyFSM;

    public EnemyIdleState(EnemyFSM enemyFSM, EnemyController enemyController)
    {
        _enemyFSM = enemyFSM;
        _enemyController = enemyController;
    }
}
