using System.Collections.Generic;
using UnityEngine;

public class RoomInventoryController : MonoBehaviour
{
    [SerializeField] private RoomData _currentRoomData;
    [SerializeField] GameObject _roomInventoryDisplayPrefab;
    [SerializeField] Transform _roomInventoryDisplayParent;

    List<GameObject> _roomInventoryDisplays = new();

    // OpenRoomInventory uses the PlayerController's CurrentRoom and instantiates a RoomInventoryDisplay prefab for each item in the room's Loot list.
    public void OpenRoomInventory()
    {
        _currentRoomData = PlayerController.Instance.RoomController.CurrentRoom;
        if (_currentRoomData == null) return;

        foreach (var item in _currentRoomData.Loot)
        {
            var roomInventoryDisplay = Instantiate(_roomInventoryDisplayPrefab, _roomInventoryDisplayParent);
            roomInventoryDisplay.GetComponent<RoomInventoryDisplayController>().Initialize(item);

            _roomInventoryDisplays.Add(roomInventoryDisplay);
        }
    }

    // CloseRoomInventory destroys all RoomInventoryDisplay prefabs.
    public void CloseRoomInventory()
    {
        foreach (var display in _roomInventoryDisplays)
        {
            Destroy(display);
        }

        _roomInventoryDisplays.Clear();
    }
}
