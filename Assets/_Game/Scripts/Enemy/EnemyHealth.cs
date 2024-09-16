using System.Collections;
using System.Collections.Generic;
using SaiUtils.GameEvents;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _health;
    [SerializeField] VoidEvent _onDeath;

    public void TakeDamage(int damage)
    {
        _health.Value -= damage;
        if (_health.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _onDeath.Raise();
        Destroy(gameObject);
    }
}
