using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    float wanderRadius;
    public EnemyIdleState(EnemyController controller, NavMeshAgent agent, float wanderRadius) : base(controller, agent)
    {
        this.wanderRadius = wanderRadius;
    }

    public override void OnEnter()
    {
        Debug.Log("Idle");
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        if (HasReachedDestination())
        {
            var randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += Controller.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            var finalPosition = hit.position;
            Agent.SetDestination(finalPosition);
            Controller.Direction = finalPosition - Controller.transform.position;
            Debug.Log($"picked new destination: {finalPosition}");
        }
    }

    bool HasReachedDestination()
    {
        return !Agent.pathPending 
            && Agent.remainingDistance <= Agent.stoppingDistance
            && (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f);
    }
}

