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

    float timer = 0;


    void Start()
    {
        _playerHealth = PlayerController.Instance.PlayerHealth;
    }
    
    
    void Update()
    {
        if (_isPlayerInDamageZone) 
        {
            timer += Time.deltaTime;
            if (timer >= 4f) 
            {
                timer = 0;
                _playerHealth.TakeDamage(_damagePerSecond);
            }
        }
    }

}
