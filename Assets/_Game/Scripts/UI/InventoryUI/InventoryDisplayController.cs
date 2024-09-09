using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayController : MonoBehaviour
{
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private GameObject _inventoryItemDisplayPrefab;
    [SerializeField] Transform _inventoryDisplayParent;

    List<GameObject> _inventoryItemDisplays = new List<GameObject>();

    public void OpenPlayerInventory()
    {
        // Open the inventory UI
        _inventoryController = PlayerController.Instance.InventoryController;

        foreach (var item in _inventoryController.Inventory.Inventory)
        {
            var itemDisplay = Instantiate(_inventoryItemDisplayPrefab, _inventoryDisplayParent);
            itemDisplay.GetComponent<InventoryItemDisplayController>().Initialize(item);

            _inventoryItemDisplays.Add(itemDisplay);
        }
    }

    public void ClosePlayerInventory()
    {
        // Close the inventory UI
        foreach (var display in _inventoryItemDisplays)
        {
            Destroy(display);
        }

        _inventoryItemDisplays.Clear();
    }
}
