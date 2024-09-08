using SaiUtils.StateMachine;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    StateMachine _gameStateMachine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void ConfigureStateMachine()
    {
        _gameStateMachine = new StateMachine();

        var gameMainMenuState = new GameMainMenuState(this);
        var gamePlayState = new GamePlayState(this);
        var gameSafeRoomState = new GameSafeRoomState(this);
        var gameEndState = new GameEndState(this);

        


    }

}
