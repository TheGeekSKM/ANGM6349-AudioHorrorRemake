using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    protected EnemyController Controller;
    protected NavMeshAgent Agent;

    public EnemyBaseState(EnemyController controller, NavMeshAgent agent)
    {
        Controller = controller;
        Agent = agent;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}
