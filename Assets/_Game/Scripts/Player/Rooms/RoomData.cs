using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Rooms/Room Data")]
public class RoomData : ScriptableObject
{
    [SerializeField] string roomName;
    public string RoomName => roomName;

    [TextArea(4, 20)]
    [SerializeField] string startingRoomDescription;

    [SerializeField] List<string> roomDescriptions;

    public string RoomDescription => discovered ? roomDescriptions[UnityEngine.Random.Range(0, roomDescriptions.Count - 1)] : startingRoomDescription;

    public List<ItemData> Loot;
    public Action<ItemData> OnLootAdded;
    public Action<ItemData> OnLootRemoved; 

    public void AddItem(ItemData item)
    {
        OnLootAdded?.Invoke(item);
        Loot.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        OnLootRemoved?.Invoke(item);
        Loot.Remove(item);
    }

    public bool discovered = false;
}
