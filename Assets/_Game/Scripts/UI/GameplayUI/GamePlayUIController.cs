using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using DG.Tweening;

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
    [SerializeField] GameObject _gameHidePanel;
    public GameObject GameHidePanel => _gameHidePanel;

    [Header("UI Panel Settings")]
    [SerializeField] Vector2 _onScreenPanelPosition = new Vector2(960f, -622f);
    [SerializeField] Vector2 _offScreenDefaultPanelPosition;
    [SerializeField] Vector2 _offScreenListenPanelPosition;
    [SerializeField] Vector2 _offScreenPlayerInventoryPanelPosition;
    [SerializeField] Vector2 _offScreenRoomInventoryPanelPosition;
    [SerializeField] Vector2 _offScreenWalkingPanelPosition;
    [SerializeField] bool _enableAnimations = true;


    [Header("Buttons")]
    [SerializeField] Button _walkButton;
    [SerializeField] Button _stopMovingButton;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;
    [SerializeField] Button _listenButton;
    [SerializeField] Button _stopListenButton;
    [SerializeField] Button _playerInventoryButton;
    [SerializeField] Button _closePlayerInventoryButton;
    [SerializeField] Button _roomInventoryButton;
    [SerializeField] Button _closeRoomInventoryButton;
    [SerializeField] Button _notePadActivateButton;
    [SerializeField] Button _notePadHideButton;

    [Header("NotePad")]
    [SerializeField] float _notePadHiddenYPos = 1014f;
    [SerializeField] float _notePadShownYPos = 0f;
    RectTransform _notePadRectTransform;

    [Header("Chatlogs")]
    [SerializeField] List<Transform> _chatLogParents = new();
    [SerializeField] GameObject _chatLogPrefab;

    [Header("Events/Variables")]
    [SerializeField] BoolVariable _isNotepadFound;


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

        // CalculateOffScreenPositions();
        ConfigureStateMachine();
    }

    void Start()
    {
        HideNotePadPanel();

        _notePadRectTransform = _notePadPanel.GetComponent<RectTransform>();
        _notePadPanel.SetActive(_isNotepadFound.Value);

    }


    [Button]
    void CalculateOffScreenPositions()
    {
        _offScreenDefaultPanelPosition = _defaultPanel.GetComponent<RectTransform>().anchoredPosition;
        _offScreenListenPanelPosition = _listenPanel.GetComponent<RectTransform>().anchoredPosition;
        _offScreenPlayerInventoryPanelPosition = _playerInventoryPanel.GetComponent<RectTransform>().anchoredPosition;
        _offScreenRoomInventoryPanelPosition = _roomInventoryPanel.GetComponent<RectTransform>().anchoredPosition;
        _offScreenWalkingPanelPosition = _walkingPanel.GetComponent<RectTransform>().anchoredPosition;
    }

    void ConfigureStateMachine()
    {
        _gamePlayUIStateMachine = new StateMachine();

        GamePlayUIDefaultState = new GamePlayUIDefaultState(
            this, _defaultPanel.GetComponent<RectTransform>(), _onScreenPanelPosition, _offScreenDefaultPanelPosition, _enableAnimations
        );

        GamePlayUIListenState = new GamePlayUIListenState(
            this, _listenPanel.GetComponent<RectTransform>(), _onScreenPanelPosition, _offScreenListenPanelPosition, _enableAnimations
        );

        GamePlayUIWalkingState = new GamePlayUIWalkingState(
            this, _walkingPanel.GetComponent<RectTransform>(), _onScreenPanelPosition, _offScreenWalkingPanelPosition, _enableAnimations
        );

        GamePlayUIPlayerInventoryState = new GamePlayUIPlayerInventoryState(
            this, _playerInventoryPanel.GetComponent<InventoryDisplayController>(), _playerInventoryPanel.GetComponent<RectTransform>(), 
            _onScreenPanelPosition, _offScreenPlayerInventoryPanelPosition, _enableAnimations
        );

        GamePlayUIRoomInventoryState = new GamePlayUIRoomInventoryState(
            this, _roomInventoryPanel.GetComponent<RoomInventoryController>(), _roomInventoryPanel.GetComponent<RectTransform>(), 
            _onScreenPanelPosition, _offScreenRoomInventoryPanelPosition, _enableAnimations
        );
        
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIDefaultState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIListenState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIPlayerInventoryState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIRoomInventoryState, new BlankPredicate());
        _gamePlayUIStateMachine.AddAnyTransition(GamePlayUIWalkingState, new BlankPredicate());

        _gamePlayUIStateMachine.SetState(GamePlayUIDefaultState);
    }

    void OnEnable()
    {
        _walkButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIWalkingState, 0.2f));
        _listenButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIListenState, 0.2f));
        _playerInventoryButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIPlayerInventoryState, 0.2f));
        _roomInventoryButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIRoomInventoryState, 0.2f));
        _notePadActivateButton.onClick.AddListener(ShowNotePadPanel);
        _notePadHideButton.onClick.AddListener(HideNotePadPanel);
        _stopMovingButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIDefaultState, 0.2f));
        _stopListenButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIDefaultState, 0.2f));
        _closePlayerInventoryButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIDefaultState, 0.2f));
        _closeRoomInventoryButton.onClick.AddListener(() => ChangeGamePlayUIStateWithDelay(GamePlayUIDefaultState, 0.2f));
        _leftButton.onClick.AddListener(() => PlayerController.Instance.PlayerMovement.TurnLeft());
        _rightButton.onClick.AddListener(() => PlayerController.Instance.PlayerMovement.TurnRight());

        _isNotepadFound.OnValueChanged += (value) => _notePadPanel.SetActive(value);
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
        _leftButton.onClick.RemoveAllListeners();
        _rightButton.onClick.RemoveAllListeners();

        _isNotepadFound.OnValueChanged -= (value) => _notePadPanel.SetActive(value);
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

    

    [Button]
    public void AddNotification(string message)
    {
        foreach (var chatLogParent in _chatLogParents)
        {
            var chatLog = Instantiate(_chatLogPrefab, chatLogParent).GetComponent<ChatLogController>();
            chatLog.Initialize(message);
        }
    }

    [Button]
    public void ClearChatLog()
    {
        foreach (var chatLogParent in _chatLogParents)
        {
            foreach (Transform child in chatLogParent)
            {
                Destroy(child.gameObject);

                // destroy immediate for unity editor
#if UNITY_EDITOR
                DestroyImmediate(child.gameObject);
#endif
            }
        }
    }

    [Button]
    public void ShowNotePadPanel()
    {
        // _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _notePadShownYPos);
        _notePadRectTransform.DOAnchorPosY(_notePadShownYPos, 0.5f).SetEase(Ease.OutExpo);

    }

    [Button]
    public void HideNotePadPanel()
    {
        // _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _notePadHiddenYPos);
        _notePadRectTransform.DOAnchorPosY(_notePadHiddenYPos, 0.5f).SetEase(Ease.OutExpo);
    }

    void Update()
    {
        _gamePlayUIStateMachine.Update();
    }

    void FixedUpdate()
    {
        _gamePlayUIStateMachine.FixedUpdate();
    }

    public void ChangeGamePlayUIStateWithDelay(GamePlayUIBaseState state, float delay)
    {
        StartCoroutine(ChangeGamePlayUIStateWithDelayCoroutine(state, delay));
    }

    IEnumerator ChangeGamePlayUIStateWithDelayCoroutine(GamePlayUIBaseState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        _gamePlayUIStateMachine.ChangeState(state);
    }
}

