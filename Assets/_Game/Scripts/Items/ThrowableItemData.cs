using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throwable Item", menuName = "Items/Throwable Item")]
public class ThrowableItemData : ItemData
{
    [Header("Throwable Item Data")]
    [SerializeField] float _timeToDestroy = 5f;
    [SerializeField] float _speedFactor = 0.5f;
    [SerializeField] float _damageAtEnd = 10f;
    [SerializeField] float _damageOverTime = 5f;
    [SerializeField] float _timeBetweenDamage = 1f;
    [SerializeField] ThrowableItemSpawnController _afterThrownPrefab;

    
    public float TimeToDestroy => _timeToDestroy;
    public float SpeedFactor => _speedFactor;
    public float DamageAtEnd => _damageAtEnd;
    public float DamageOverTime => _damageOverTime;
    public float TimeBetweenDamage => _timeBetweenDamage;
    
    public ThrowableItemSpawnController AfterThrownPrefab => _afterThrownPrefab;

    public override void DropItem(GameObject user, RoomData room)
    {
        base.DropItem(user, room);
        var roomTrigger = RoomManager.Instance.FindRoomTrigger(room);
        var roomTransform = roomTrigger.gameObject.transform;
        Debug.Log("Room Trigger: " + roomTrigger);
        if (AfterThrownPrefab) 
        {
            var prefab = Instantiate(AfterThrownPrefab, roomTransform.position, roomTransform.rotation);
            prefab.Initialize(this, roomTrigger);
        }
    }
}
