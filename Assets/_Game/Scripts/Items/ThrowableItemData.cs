using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItemData : ItemData
{
    [SerializeField] GameObject _afterThrownPrefab;
    public GameObject AfterThrownPrefab => _afterThrownPrefab;

    public override void DropItem(GameObject user)
    {
        base.DropItem(user);
        if (AfterThrownPrefab) Instantiate(AfterThrownPrefab, user.transform.position, user.transform.rotation);
    }
}
