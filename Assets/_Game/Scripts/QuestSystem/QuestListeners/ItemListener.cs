using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemListener : MonoBehaviour
{
    [SerializeField] ItemData _itemData;
    public ItemData ItemData => _itemData;

    public UnityEvent OnPickUpEvent;
    public UnityEvent OnUseEvent;
    public UnityEvent OnBreakEvent;
    public UnityEvent OnDropEvent;

    void OnEnable()
    {
        _itemData.OnPickUp += OnPickUp;
        _itemData.OnUse += OnUse;
        _itemData.OnBreak += OnBreak;
        _itemData.OnDrop += OnDrop;
    }

    void OnDisable()
    {
        _itemData.OnPickUp -= OnPickUp;
        _itemData.OnUse -= OnUse;
        _itemData.OnBreak -= OnBreak;
        _itemData.OnDrop -= OnDrop;
    }

    void OnPickUp()
    {
        Debug.Log($"ItemListener: OnPickUp event fired on {_itemData.ItemName}");
        OnPickUpEvent.Invoke();
    }

    void OnUse()
    {
        Debug.Log($"ItemListener: OnUse event fired on {_itemData.ItemName}");
        OnUseEvent.Invoke();
    }

    void OnBreak()
    {
        Debug.Log($"ItemListener: OnBreak event fired on {_itemData.ItemName}");
        OnBreakEvent.Invoke();
    }

    void OnDrop()
    {
        Debug.Log($"ItemListener: OnDrop event fired on {_itemData.ItemName}");
        OnDropEvent.Invoke();
    }

    
}
