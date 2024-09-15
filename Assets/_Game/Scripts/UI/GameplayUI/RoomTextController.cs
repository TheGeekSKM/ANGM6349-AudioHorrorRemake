using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SaiUtils.Extensions;

public class RoomTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _roomText;

    void OnValidate()
    {
        if (_roomText == null) _roomText = gameObject.GetOrAdd<TextMeshProUGUI>();
    }

    public void UpdateRoomText(RoomData room)
    {
        _roomText.text = room.RoomName;
    }
}
