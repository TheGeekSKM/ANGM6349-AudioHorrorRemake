using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{
    [SerializeField] GameStateEnum _gameState;
    public UnityEvent OnGameStateChangeEvent;

    void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange;
    }

    void OnGameStateChange(GameStateEnum gameState)
    {
        if (gameState == _gameState) Effect();
    }

    void Effect()
    {
        OnGameStateChangeEvent?.Invoke();
        Debug.Log("GameStateListener: Effect");
    }
}
