using System;
using SaiUtils.GameEvents;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private RoomData _currentRoom;
    [SerializeField] StringEvent _onRoomEnterString;
    [SerializeField] RoomEvent _onRoomEnterRoomData;

    public RoomData CurrentRoom => _currentRoom;

    

    public void RoomEnter(RoomData room)
    {
        _currentRoom = room;
        _onRoomEnterRoomData?.Raise(_currentRoom);
        _onRoomEnterString?.Raise(_currentRoom.RoomName);
    }

    public void RoomExit()
    {
        _currentRoom = null;
    }

}
