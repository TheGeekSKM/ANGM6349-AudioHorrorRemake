using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetState : EnemyBaseState
{
    Transform target;
    Vector2 targetTimeRange;
    float targetTime;
    float timer;
    public EnemyTargetState(EnemyController controller, NavMeshAgent agent, Transform target, Vector2 targetTimeRange) : base(controller, agent)
    {
        this.target = target;
        this.targetTimeRange = targetTimeRange;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Controller.Direction = target.position - Controller.transform.position;
        Agent.SetDestination(target.position);

        targetTime = Random.Range(targetTimeRange.x, targetTimeRange.y);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= targetTime) Controller.TriggerIdleState();

        base.Update();
    }
}

