using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] List<RoomTrigger> _rooms;

    public RoomTrigger FindRoomTrigger(RoomData roomData)
    {
        foreach (var room in _rooms)
        {
            // Debug.Log($"Does {room.RoomData.name} == {roomData.name}?");
            if (room.RoomData.name == roomData.name) 
            {
                Debug.Log("Room found");
                return room;
            }
        }
        Debug.LogError("Room not found");
        return null;
    }

}
