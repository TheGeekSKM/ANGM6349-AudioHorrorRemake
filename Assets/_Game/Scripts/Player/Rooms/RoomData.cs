using System.Collections;
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

    public string RoomDescription => discovered ? roomDescriptions[Random.Range(0, roomDescriptions.Count - 1)] : startingRoomDescription;

    public List<ItemData> Loot;

    public bool discovered = false;
}
