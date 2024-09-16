using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomInventoryListener : MonoBehaviour
{
    [SerializeField] RoomData _roomData;
    public List<ItemData> itemDatasToCompare = new();

    public UnityEvent OnLootAddedMatch;
    public UnityEvent OnLootAddedNotMatch;
    public UnityEvent OnLootRemovedMatch;
    public UnityEvent OnLootRemovedNotMatch;


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
        if (!itemDatasToCompare.Contains(item)) OnLootAddedNotMatch.Invoke();
        else OnLootAddedMatch.Invoke();
    }

    void OnLootRemoved(ItemData item)
    {
        if (!itemDatasToCompare.Contains(item)) OnLootRemovedNotMatch.Invoke();
        else OnLootRemovedMatch.Invoke();
    }
}
