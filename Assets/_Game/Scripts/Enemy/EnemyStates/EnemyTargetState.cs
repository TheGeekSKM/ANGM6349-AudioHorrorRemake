using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetState : EnemyBaseState
{
    Transform target;
    public EnemyTargetState(EnemyController controller, NavMeshAgent agent, Transform target) : base(controller, agent)
    {
        this.target = target;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Controller.Direction = target.position - Controller.transform.position;
        Agent.SetDestination(target.position);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        base.Update();
    }
}

