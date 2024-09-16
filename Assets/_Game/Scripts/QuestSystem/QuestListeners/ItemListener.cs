using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemListener : MonoBehaviour
{
    [SerializeField] ItemData _itemData;
    public ItemData ItemData => _itemData;

    public UnityEvent<ItemData> OnPickUpEvent;
    public UnityEvent<ItemData> OnUseEvent;
    public UnityEvent<ItemData> OnBreakEvent;
    public UnityEvent<ItemData> OnDropEvent;

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
        OnPickUpEvent.Invoke(_itemData);
    }

    void OnUse()
    {
        Debug.Log($"ItemListener: OnUse event fired on {_itemData.ItemName}");
        OnUseEvent.Invoke(_itemData);
    }

    void OnBreak()
    {
        Debug.Log($"ItemListener: OnBreak event fired on {_itemData.ItemName}");
        OnBreakEvent.Invoke(_itemData);
    }

    void OnDrop()
    {
        Debug.Log($"ItemListener: OnDrop event fired on {_itemData.ItemName}");
        OnDropEvent.Invoke(_itemData);
    }

    
}
