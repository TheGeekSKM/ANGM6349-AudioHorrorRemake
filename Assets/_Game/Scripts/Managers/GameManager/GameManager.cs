using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;
using System.Collections;
using System;


public enum GameStateEnum
{
    MainMenu,
    Cutscene,
    Play,
    SafeRoom,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Scenes")]
    [SerializeField] private SceneReference _mainMenuScene;
    [SerializeField] private SceneReference _cutsceneScene;
    [SerializeField] private SceneReference _gameScene;
    [SerializeField] private SceneReference _safeRoomScene;
    [SerializeField] private SceneReference _endScene;

    [Header("Game Settings")]   
    [SerializeField] bool _startWithMainMenu = true;
    public Action<GameStateEnum> OnGameStateChange;

    [SerializeField] RoomData _monsterNest;

    // [Header("Events")]
    // [SerializeField] VoidEvent _onPlayerEnterSafeRoom;

    bool _playerInSafeRoom = false;
    float _safeRoomTimer = 0;


    StateMachine _gameStateMachine;

    public StateMachine GameStateMachine => _gameStateMachine;

    public GameMainMenuState GameMainMenuState {get; private set;}
    public GameCutsceneState GameCutsceneState {get; private set;}
    public GamePlayState GamePlayState {get; private set;}
    public GameSafeRoomState GameSafeRoomState {get; private set;}
    public GameEndState GameEndState {get; private set;}

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ConfigureStateMachine();
    }

    void ConfigureStateMachine()
    {
        _gameStateMachine = new StateMachine();

        var gameMainMenuState = new GameMainMenuState(this, _mainMenuScene);
        var gameCutsceneState = new GameCutsceneState(this, _cutsceneScene);
        var gamePlayState = new GamePlayState(this);
        var gameSafeRoomState = new GameSafeRoomState(this, _safeRoomScene);
        var gameEndState = new GameEndState(this, _endScene, _gameScene);

        GameMainMenuState = gameMainMenuState;
        GameCutsceneState = gameCutsceneState;
        GamePlayState = gamePlayState;
        GameSafeRoomState = gameSafeRoomState;
        GameEndState = gameEndState;

        _gameStateMachine.AddTransition(gameMainMenuState, gameCutsceneState, new BlankPredicate());
        _gameStateMachine.AddTransition(gameCutsceneState, gamePlayState, new BlankPredicate());
        _gameStateMachine.AddTransition(gamePlayState, gameSafeRoomState, new BlankPredicate());
        _gameStateMachine.AddTransition(gameSafeRoomState, gameEndState, new BlankPredicate());

    }

    void Start()
    {
        SceneManager.LoadSceneAsync(_gameScene.BuildIndex, LoadSceneMode.Additive);
        if (_startWithMainMenu) _gameStateMachine.SetState(GameMainMenuState);

    }

    void Update()
    {
        _gameStateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        if (_playerInSafeRoom)
        {
            _safeRoomTimer += Time.deltaTime;
            if (_safeRoomTimer == 1f) {
                PlayerController.Instance.PlayerHealth.TakeDamage(-2);
                _safeRoomTimer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        _gameStateMachine.FixedUpdate();
    }

    public void GameEnd() => ChangeGameStateWithDelay(GameEndState, 0.1f);

    public void ChangeGameStateWithDelay(GameBaseState state, float delay)
    {
        StartCoroutine(ChangeGameStateWithDelayCoroutine(state, delay));
    }

    IEnumerator ChangeGameStateWithDelayCoroutine(GameBaseState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        _gameStateMachine.ChangeState(state);
    }

    public void PlayCutscene(DialogueSceneSO dialogueSceneSO)
    {
        CutsceneManager.Instance.SetDialogue(dialogueSceneSO);
        var gameCutsceneState = new GameCutsceneState(this, _cutsceneScene, dialogueSceneSO);
        ChangeGameStateWithDelay(gameCutsceneState, 0.1f);
    }

    public void SafeRoom() => _gameStateMachine.ChangeState(GameSafeRoomState);

    public void AlertMonsterToMonsterNest()
    {
        Debug.Log("Alerting monster to monster nest");
        var monsterNestRT = RoomManager.Instance.FindRoomTrigger(_monsterNest);
        EnemyController.Instance.NavMeshAgent.Warp(monsterNestRT.transform.position);
        EnemyController.Instance.NavMeshAgent.speed = 0;
        StartCoroutine(StayInRoom());
        // EnemyController.Instance.TriggerTargetState(monsterNestRT.transform);
    }

    IEnumerator StayInRoom()
    {
        yield return new WaitForSeconds(10f);
        EnemyController.Instance.NavMeshAgent.speed = 2;
    }

    public void PlayerInSafeRoom()
    {
        Debug.Log("Player in safe room");
        EnemyController.Instance.NavMeshAgent.speed = 0;
        _playerInSafeRoom = true;
    }

    public void PlayerOutOfSafeRoom()
    {
        Debug.Log("Player out of safe room");
        EnemyController.Instance.NavMeshAgent.speed = 2;
        _playerInSafeRoom = false;
    }

}
