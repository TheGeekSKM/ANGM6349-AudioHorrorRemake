using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Rooms/Room Data")]
public class RoomData : ScriptableObject
{
    string roomName;
    public string RoomName => discovered ? roomName : "Unknown Room";

    [TextArea(15, 20)]
    public string StartingRoomDescription;

    [TextArea(15, 20)]
    public string RoomDescription;

    public List<ItemData> Loot;

    public bool discovered = false;
}
