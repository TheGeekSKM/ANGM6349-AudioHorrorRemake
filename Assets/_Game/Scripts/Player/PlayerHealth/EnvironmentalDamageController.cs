using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalDamageController : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] float _damagePerSecond = 2f;

    bool _isPlayerInDamageZone = false;

    public void PlayerInDamageZone() => _isPlayerInDamageZone = true;

    public void PlayerNotInDamageZone() => _isPlayerInDamageZone = false;
    
    
    void Update()
    {
        if (_isPlayerInDamageZone)
            _playerHealth.TakeDamage(_damagePerSecond * Time.deltaTime);
    }

}
