using SaiUtils.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;

    [SerializeField] FloatEvent OnHealthPercentageChanged;

    void Start()
    {
        _currentHealth = _maxHealth;
        OnHealthPercentageChanged.Raise(_currentHealth / _maxHealth);
    }

    [Button]
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();

        OnHealthPercentageChanged.Raise(_currentHealth / _maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player died");
        GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GameEndState, 0.2f);
    }
}
