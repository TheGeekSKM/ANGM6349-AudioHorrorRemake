using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public enum GamePlayUIState
{
    Default,
    Listen,
    PlayerInventory,
    RoomInventory,
    Walking
}

public class GamePlayUIController : MonoBehaviour
{
    public static GamePlayUIController Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] GameObject _defaultPanel;
    [SerializeField] GameObject _listenPanel;
    [SerializeField] GameObject _playerInventoryPanel;
    [SerializeField] GameObject _roomInventoryPanel;
    [SerializeField] GameObject _walkingPanel;
    [SerializeField] GameObject _notePadPanel;

    [Header("Buttons")]
    [SerializeField] Button _walkButton;
    [SerializeField] Button _stopMovingButton;
    [SerializeField] Button _listenButton;
    [SerializeField] Button _stopListenButton;
    [SerializeField] Button _playerInventoryButton;
    [SerializeField] Button _closePlayerInventoryButton;
    [SerializeField] Button _roomInventoryButton;
    [SerializeField] Button _closeRoomInventoryButton;
    [SerializeField] Button _notePadActivateButton;
    [SerializeField] Button _notePadHideButton;

    [SerializeField] float _notePadHiddenYPos = 1014f;
    [SerializeField] float _notePadShownYPos = 0f;


    StateMachine _gamePlayUIStateMachine;
    public StateMachine GamePlayUIStateMachine => _gamePlayUIStateMachine;

    public GamePlayUIDefaultState GamePlayUIDefaultState {get; private set;}
    public GamePlayUIListenState GamePlayUIListenState {get; private set;}
    public GamePlayUIPlayerInventoryState GamePlayUIPlayerInventoryState {get; private set;}
    public GamePlayUIRoomInventoryState GamePlayUIRoomInventoryState {get; private set;}
    public GamePlayUIWalkingState GamePlayUIWalkingState {get; private set;}


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ConfigureStateMachine();
    }

    void Start()
    {
        ShowDefaultPanel();
        HideNotePadPanel();
    }

    void ConfigureStateMachine()
    {
        _gamePlayUIStateMachine = new StateMachine();

        GamePlayUIDefaultState = new GamePlayUIDefaultState(this);
        GamePlayUIListenState = new GamePlayUIListenState(this);
        GamePlayUIPlayerInventoryState = new GamePlayUIPlayerInventoryState(this);
        GamePlayUIRoomInventoryState = new GamePlayUIRoomInventoryState(this);
        GamePlayUIWalkingState = new GamePlayUIWalkingState(this);

        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIDefaultState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIListenState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIPlayerInventoryState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIRoomInventoryState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIWalkingState, new BlankPredicate());

        _gamePlayUIStateMachine.SetState(GamePlayUIDefaultState);
    }

    void OnEnable()
    {
        _walkButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Walking));
        _listenButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Listen));
        _playerInventoryButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.PlayerInventory));
        _roomInventoryButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.RoomInventory));
        _notePadActivateButton.onClick.AddListener(ShowNotePadPanel);
        _notePadHideButton.onClick.AddListener(HideNotePadPanel);
        _stopMovingButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Default));
        _stopListenButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Default));
        _closePlayerInventoryButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Default));
        _closeRoomInventoryButton.onClick.AddListener(() => ChangeUIState(GamePlayUIState.Default));
    }

    void OnDisable()
    {
        _walkButton.onClick.RemoveAllListeners();
        _listenButton.onClick.RemoveAllListeners();
        _playerInventoryButton.onClick.RemoveAllListeners();
        _roomInventoryButton.onClick.RemoveAllListeners();
        _notePadActivateButton.onClick.RemoveAllListeners();
        _notePadHideButton.onClick.RemoveAllListeners();
        _stopMovingButton.onClick.RemoveAllListeners();
        _stopListenButton.onClick.RemoveAllListeners();
        _closePlayerInventoryButton.onClick.RemoveAllListeners();
        _closeRoomInventoryButton.onClick.RemoveAllListeners();
    }

    public void ChangeUIState(GamePlayUIState state)
    {
        switch (state)
        {
            case GamePlayUIState.Default:
                _gamePlayUIStateMachine.ChangeState(GamePlayUIDefaultState);
                break;
            case GamePlayUIState.Listen:
                _gamePlayUIStateMachine.ChangeState(GamePlayUIListenState);
                break;
            case GamePlayUIState.PlayerInventory:
                _gamePlayUIStateMachine.ChangeState(GamePlayUIPlayerInventoryState);
                break;
            case GamePlayUIState.RoomInventory:
                _gamePlayUIStateMachine.ChangeState(GamePlayUIRoomInventoryState);
                break;
            case GamePlayUIState.Walking:
                _gamePlayUIStateMachine.ChangeState(GamePlayUIWalkingState);
                break;
        }
    }

    public void ShowDefaultPanel()
    {
        _defaultPanel.SetActive(true);
        _listenPanel.SetActive(false);
        _playerInventoryPanel.SetActive(false);
        _roomInventoryPanel.SetActive(false);
        _walkingPanel.SetActive(false);
    }

    public void ShowListenPanel()
    {
        _defaultPanel.SetActive(false);
        _listenPanel.SetActive(true);
        _playerInventoryPanel.SetActive(false);
        _roomInventoryPanel.SetActive(false);
        _walkingPanel.SetActive(false);
    }

    public void ShowPlayerInventoryPanel()
    {
        _defaultPanel.SetActive(false);
        _listenPanel.SetActive(false);
        _playerInventoryPanel.SetActive(true);
        _roomInventoryPanel.SetActive(false);
        _walkingPanel.SetActive(false);
    }

    public void ShowRoomInventoryPanel()
    {
        _defaultPanel.SetActive(false);
        _listenPanel.SetActive(false);
        _playerInventoryPanel.SetActive(false);
        _roomInventoryPanel.SetActive(true);
        _walkingPanel.SetActive(false);
    }

    public void ShowWalkingPanel()
    {
        _defaultPanel.SetActive(false);
        _listenPanel.SetActive(false);
        _playerInventoryPanel.SetActive(false);
        _roomInventoryPanel.SetActive(false);
        _walkingPanel.SetActive(true);

        
    }

    [Button]
    public void ShowNotePadPanel()
    {
        _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _notePadShownYPos);
    }

    [Button]
    public void HideNotePadPanel()
    {
        _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _notePadHiddenYPos);
    }

    void Update()
    {
        _gamePlayUIStateMachine.Update();
    }

    void FixedUpdate()
    {
        _gamePlayUIStateMachine.FixedUpdate();
    }
}

