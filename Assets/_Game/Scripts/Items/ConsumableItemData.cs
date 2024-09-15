using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item Data")]
public class ConsumableItemData : ItemData
{
    [Header("Consumable Item Data")]
    public int HealthRestore;

    public override void UseItem(GameObject user)
    {
        base.UseItem(user);

        if (HealthRestore > 0) PlayerController.Instance.PlayerHealth.TakeDamage(-HealthRestore);
    }
}
