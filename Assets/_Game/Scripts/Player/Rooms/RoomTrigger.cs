using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomData roomData;
    public RoomData RoomData => roomData;


    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<RoomController>();
        if (!player) return;

        player.RoomEnter(roomData);
    }
}
