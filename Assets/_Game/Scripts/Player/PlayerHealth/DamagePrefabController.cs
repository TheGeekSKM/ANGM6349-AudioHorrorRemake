using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePrefabController : MonoBehaviour
{

    [SerializeField] float _destroyTime = 0.5f;
    [SerializeField] int _attackDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (!player) return;

        player.TakeDamage(_attackDamage);
    }
}
