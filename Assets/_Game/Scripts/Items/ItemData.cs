using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    [TextArea(3, 10)] public string ItemDescription;
    public string[] ItemUsageDescription;
    public bool UseDurability = false;
    public int Durability = -1;

    [Header("Sounds")]
    public SoundData PickupSound;
    public SoundData UseSound;
    public SoundData BreakSound;
    public SoundData DropSound;

    public Action OnPickUp;
    public Action OnUse;
    public Action OnBreak;
    public Action OnDrop;

    public virtual void PickUpItem(GameObject user)
    {
        if (PickupSound) SoundManager.Instance.PlaySound(user.transform, PickupSound);
        OnPickUp?.Invoke();

        GamePlayUIController.Instance.AddNotification($"<b>You:</b> I think I found a {ItemName}...");
    }

    public virtual void UseItem(GameObject user)
    {

        if (UseSound) SoundManager.Instance.PlaySound(user.transform, UseSound);
        OnUse?.Invoke();

        GamePlayUIController.Instance.AddNotification($"<b>You:</b> {ItemUsageDescription[UnityEngine.Random.Range(0, ItemUsageDescription.Length)]}");

        if (UseDurability) 
        {
            Durability--;
            if (Durability == 0) OnItemBreak(user);
        }
    }

    public virtual void DropItem(GameObject user, RoomData room)
    {
        if (DropSound) SoundManager.Instance.PlaySound(user.transform, DropSound);
        OnDrop?.Invoke();

        GamePlayUIController.Instance.AddNotification($"<b>You:</b> I dropped the {ItemName} in the {room.RoomName}.");
    }

    public virtual void OnItemBreak(GameObject user)
    {
        if (BreakSound) SoundManager.Instance.PlaySound(user.transform, BreakSound);
        OnBreak?.Invoke();

        GamePlayUIController.Instance.AddNotification($"<b>You:</b> The {ItemName} broke...shit...");
    }
}
