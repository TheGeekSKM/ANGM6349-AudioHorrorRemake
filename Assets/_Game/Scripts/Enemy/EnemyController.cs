using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] NavMeshAgent _navMeshAgent;

    [Header("Debug")]
    [SerializeField, ReadOnly] Vector3 _direction;
    public Vector3 Direction => _direction;
    [SerializeField, ReadOnly] Transform _currentTarget;
    public Transform CurrentTarget => _currentTarget; 
    [SerializeField, ReadOnly] bool _idling;
    public bool Idling => _idling;
    
    public UnityAction<bool> OnTargetState;




#region Idle Logic
    
    public void TriggerIdleState()
    {
        _idling = true;
    }
#endregion


#region Target Logic

    public void TriggerTargetState(Transform target)
    {
        Debug.Log($"EnemyController: TriggerTargetState: {target.name} at {target.position}");
        _navMeshAgent.SetDestination(target.position);
        _direction = (target.position - transform.position).normalized;
    }

   
#endregion

}

