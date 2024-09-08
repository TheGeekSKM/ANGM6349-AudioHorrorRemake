using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;


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

    SceneReference _currentScene;

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

        var gameMainMenuState = new GameMainMenuState(this);
        var gameCutsceneState = new GameCutsceneState(this);
        var gamePlayState = new GamePlayState(this);
        var gameSafeRoomState = new GameSafeRoomState(this);
        var gameEndState = new GameEndState(this);

        GameMainMenuState = gameMainMenuState;
        GameCutsceneState = gameCutsceneState;
        GamePlayState = gamePlayState;
        GameSafeRoomState = gameSafeRoomState;
        GameEndState = gameEndState;

        _gameStateMachine.AddTransition(gameMainMenuState, gameCutsceneState, new BlankPredicate());
        _gameStateMachine.AddTransition(gameCutsceneState, gamePlayState, new BlankPredicate());
        _gameStateMachine.AddTransition(gamePlayState, gameSafeRoomState, new BlankPredicate());
        _gameStateMachine.AddTransition(gameSafeRoomState, gameEndState, new BlankPredicate());

        if (_startWithMainMenu) _gameStateMachine.SetState(gameMainMenuState);
    }



    public void LoadMainMenuScene()
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
        }

        SceneManager.LoadSceneAsync(_mainMenuScene.BuildIndex, LoadSceneMode.Additive);
        _currentScene = _mainMenuScene;
    }

    public void LoadCutsceneScene()
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
        }

        SceneManager.LoadSceneAsync(_cutsceneScene.BuildIndex, LoadSceneMode.Additive);
        _currentScene = _cutsceneScene;
    }

    public void LoadGameScene()
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
        }

        SceneManager.LoadSceneAsync(_gameScene.BuildIndex, LoadSceneMode.Additive);
        _currentScene = _gameScene;
    }

    public void LoadSafeRoomScene()
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
        }

        SceneManager.LoadSceneAsync(_safeRoomScene.BuildIndex, LoadSceneMode.Additive);
        _currentScene = _safeRoomScene;
    }

    public void LoadEndScene()
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
        }

        SceneManager.LoadSceneAsync(_endScene.BuildIndex, LoadSceneMode.Additive);
        _currentScene = _endScene;
    }

}
