using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    public string[] ItemUsageDescription;
    public bool UseDurability = false;
    public int Durability = -1;

    [Header("Sounds")]
    public SoundData PickupSound;
    public SoundData UseSound;
    public SoundData BreakSound;
    public SoundData DropSound;

    public virtual void PickUpItem(GameObject user)
    {
        if (PickupSound) SoundManager.Instance.PlaySound(user.transform, PickupSound);
    }

    public virtual void UseItem(GameObject user)
    {
        if (UseSound) SoundManager.Instance.PlaySound(user.transform, UseSound);
    }

    public virtual void DropItem(GameObject user)
    {
        if (DropSound) SoundManager.Instance.PlaySound(user.transform, DropSound);
    }

    public virtual void OnItemBreak(GameObject user)
    {
        if (BreakSound) SoundManager.Instance.PlaySound(user.transform, BreakSound);
    }
}
