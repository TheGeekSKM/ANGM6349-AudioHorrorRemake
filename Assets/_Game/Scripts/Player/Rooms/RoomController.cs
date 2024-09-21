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

        if (_currentRoom.discovered) GamePlayUIController.Instance.AddNotification($"<b>You:</b> I think I just wandered into the {room.RoomName}...");
        GamePlayUIController.Instance.AddNotification($"<b>You:</b> {room.RoomDescription}");
        _currentRoom.discovered = true;
    }

    public void RoomExit()
    {
        _currentRoom = null;
    }

}
