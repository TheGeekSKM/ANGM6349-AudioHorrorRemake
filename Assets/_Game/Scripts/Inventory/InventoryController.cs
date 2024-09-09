using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventorySO _inventorySO;
    public InventorySO Inventory => _inventorySO;

    public void AddItem(ItemData item)
    {
        _inventorySO.Inventory.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        _inventorySO.Inventory.Remove(item);
    }
}
