using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChatLogContainer : MonoBehaviour
{
    [SerializeField] List<string> _walkChatLog;
    [SerializeField] List<string> _stopChatLog;
    [SerializeField] List<string> _wallStopChatLog;
    [SerializeField] List<string> _inventoryCheckChatLog;
    [SerializeField] List<string> _roomInventoryCheckChatLog;

    public string WalkChatLog => _walkChatLog[Random.Range(0, _walkChatLog.Count)];
    public string StopChatLog => _stopChatLog[Random.Range(0, _stopChatLog.Count)];
    public string WallStopChatLog => _wallStopChatLog[Random.Range(0, _wallStopChatLog.Count)];
    public string InventoryCheckChatLog => _inventoryCheckChatLog[Random.Range(0, _inventoryCheckChatLog.Count)];
    public string RoomInventoryCheckChatLog => _roomInventoryCheckChatLog[Random.Range(0, _roomInventoryCheckChatLog.Count)];
}
