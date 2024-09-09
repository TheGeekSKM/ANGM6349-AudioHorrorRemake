using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "InventorySO")]
public class InventorySO : ScriptableObject 
{
    public List<ItemData> Inventory = new List<ItemData>();    
}
