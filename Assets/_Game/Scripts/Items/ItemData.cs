using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    public string ItemDescription;
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
    }

    public virtual void UseItem(GameObject user)
    {

        if (UseSound) SoundManager.Instance.PlaySound(user.transform, UseSound);
        OnUse?.Invoke();

        if (UseDurability) 
        {
            Durability--;
            if (Durability == 0) OnItemBreak(user);
        }
    }

    public virtual void DropItem(GameObject user)
    {
        if (DropSound) SoundManager.Instance.PlaySound(user.transform, DropSound);
        OnDrop?.Invoke();
    }

    public virtual void OnItemBreak(GameObject user)
    {
        if (BreakSound) SoundManager.Instance.PlaySound(user.transform, BreakSound);
        OnBreak?.Invoke();
    }
}
