using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Rooms/Room Data")]
public class RoomData : ScriptableObject
{
    public string RoomName;

    [TextArea(15, 20)]
    public string StartingRoomDescription;

    [TextArea(15, 20)]
    public string RoomDescription;

    public List<ItemData> Loot;

    public bool discovered = false;
}
