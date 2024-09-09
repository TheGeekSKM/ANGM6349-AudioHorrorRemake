using System;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private RoomData _currentRoom;
    public RoomData CurrentRoom => _currentRoom;
    public Action<RoomData> OnRoomEnter;

    public void RoomEnter(RoomData room)
    {
        _currentRoom = room;
        OnRoomEnter?.Invoke(_currentRoom);
    }

    public void RoomExit()
    {
        _currentRoom = null;
    }

}
