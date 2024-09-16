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

    // [Header("Events")]
    // [SerializeField] VoidEvent _onPlayerEnterSafeRoom;


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

}
