using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomInventoryListener : MonoBehaviour
{
    [SerializeField] RoomData _roomData;

    public UnityEvent OnLootAddedEvent;
    public UnityEvent OnLootRemovedEvent;

    public List<ItemData> itemDatasToCompare = new();

    void OnEnable()
    {
        _roomData.OnLootAdded += OnLootAdded;
        _roomData.OnLootRemoved += OnLootRemoved;
    }

    void OnDisable()
    {
        _roomData.OnLootAdded -= OnLootAdded;
        _roomData.OnLootRemoved -= OnLootRemoved;
    }

    void OnLootAdded(ItemData item)
    {
        if (!itemDatasToCompare.Contains(item)) return;
        OnLootAddedEvent.Invoke();
    }

    void OnLootRemoved(ItemData item)
    {
        if (!itemDatasToCompare.Contains(item)) return;
        OnLootRemovedEvent.Invoke();
    }
}
