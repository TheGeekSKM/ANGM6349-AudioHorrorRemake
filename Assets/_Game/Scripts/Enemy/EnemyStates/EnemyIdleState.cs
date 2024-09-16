using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    float wanderRadius;
    Vector2 timeBetweenRoars;
    float acutalTime;
    float timer;
    public EnemyIdleState(EnemyController controller, NavMeshAgent agent, float wanderRadius, Vector2 timeBetweenRoars) : base(controller, agent)
    {
        this.wanderRadius = wanderRadius;
        this.timeBetweenRoars = timeBetweenRoars;
    }

    public override void OnEnter()
    {
        // Debug.Log("Idle");
        acutalTime = Random.Range(timeBetweenRoars.x, timeBetweenRoars.y);
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
            // Debug.Log($"picked new destination: {finalPosition}");
        }

        timer += Time.deltaTime;
        if (timer >= acutalTime)
        {
            timer = 0;
            acutalTime = Random.Range(timeBetweenRoars.x, timeBetweenRoars.y);
            SoundManager.Instance.PlayEnemySound(Controller.transform, SoundAtlas.Instance.MonsterFootstepSound);
        }
    }

    bool HasReachedDestination()
    {
        return !Agent.pathPending 
            && Agent.remainingDistance <= Agent.stoppingDistance
            && (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f);
    }
}

