using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemDisplayController : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] Button _useButton;
    [SerializeField] Button _discardButton;

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        _itemName.text = _itemData.ItemName;
        _itemDescription.text = _itemData.ItemDescription;

        _useButton.onClick.AddListener(UseItem);
        _discardButton.onClick.AddListener(DiscardItem);

    }

    private void UseItem()
    {
        _itemData.UseItem(PlayerController.Instance.gameObject);
        HandleDurability();        
    }

    void HandleDurability()
    {
        if (_itemData.UseDurability)
        {
            _itemData.Durability--;
            if (_itemData.Durability <= 0)
            {
                PlayerController.Instance.InventoryController.RemoveItem(_itemData);
                
                _useButton.onClick.RemoveAllListeners();
                _discardButton.onClick.RemoveAllListeners();
                Destroy(gameObject);
            }
        }
    }

    private void DiscardItem()
    {
        PlayerController.Instance.InventoryController.RemoveItem(_itemData);
        PlayerController.Instance.RoomController.CurrentRoom.Loot.Add(_itemData);
    }
}
