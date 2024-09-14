using UnityEngine;
using UnityEngine.Events;
using SaiUtils.GameEvents;

public class TriggerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] bool _ignoreFirstTime = false;

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

            if (_ignoreFirstTime) {
                _ignoreFirstTime = false;
                return;
            }
            
            _onTriggerEnter?.Invoke();
            _onTriggerEnterGameEvent?.Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            _onTriggerExit?.Invoke();
            _onTriggerExitGameEvent?.Raise();
        }
    }

}
