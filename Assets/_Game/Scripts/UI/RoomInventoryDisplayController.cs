using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomInventoryDisplayController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] TextMeshProUGUI _roomName;
    [SerializeField] TextMeshProUGUI _roomDescription;
    [SerializeField] Button _pickUpButton;

    [Header("Item Data")]
    [SerializeField] ItemData _itemData;

    public void Initialize(ItemData item)
    {
        _itemData = item;
        _roomName.text = _itemData.ItemName;
        _roomDescription.text = _itemData.ItemDescription;
        _pickUpButton.onClick.AddListener(PickUpItem);
    }

    private void PickUpItem()
    {
        _itemData.PickUpItem(gameObject);

        PlayerController.Instance.RoomController.CurrentRoom.Loot.Remove(_itemData);
        Destroy(gameObject);
    }
}
