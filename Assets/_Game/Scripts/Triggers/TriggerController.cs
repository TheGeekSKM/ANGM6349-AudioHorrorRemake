using UnityEngine;
using UnityEngine.Events;
using SaiUtils.GameEvents;

public class TriggerController : MonoBehaviour
{
    [SerializeField] LayerMask _targetLayer;

    [Header("Unity Events")]
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;

    [Header("Game Events")]
    [SerializeField] private VoidEvent _onTriggerEnterGameEvent;
    [SerializeField] private VoidEvent _onTriggerExitGameEvent;


    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Hit with Layermask");
            _onTriggerEnter?.Invoke();
            _onTriggerEnterGameEvent?.Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Exit with Layermask");
            _onTriggerExit?.Invoke();
            _onTriggerExitGameEvent?.Raise();
        }
    }

}
