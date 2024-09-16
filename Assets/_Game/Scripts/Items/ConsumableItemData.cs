using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item Data")]
public class ConsumableItemData : ItemData
{
    [Header("Consumable Item Data")]
    public int HealthRestore;
    public int SpeedBoost;
    public int SpeedBoostDuration;

    public override void UseItem(GameObject user)
    {
        base.UseItem(user);

        if (user.CompareTag("Player")) // this might not be necessary if we're only using this script for the player but just in case idk...
        {
            if (HealthRestore > 0) PlayerController.Instance.PlayerHealth.TakeDamage(-HealthRestore);
            if (SpeedBoost > 0) PlayerController.Instance.PlayerMovement.BoostSpeed(SpeedBoost, SpeedBoostDuration > 0 ? SpeedBoostDuration : 0.1f);
        }
    }
}
