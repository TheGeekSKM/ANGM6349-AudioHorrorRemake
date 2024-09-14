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

        ConfigureStateMachine();
    }

    void Start()
    {
        ShowDefaultPanel();
        HideNotePadPanel();

        _notePadRectTransform = _notePadPanel.GetComponent<RectTransform>();
        _notePadPanel.SetActive(_isNotepadFound.Value);
    }

    void ConfigureStateMachine()
    {
        _gamePlayUIStateMachine = new StateMachine();

        GamePlayUIDefaultState = new GamePlayUIDefaultState(this);
        GamePlayUIListenState = new GamePlayUIListenState(this);
        GamePlayUIPlayerInventoryState = new GamePlayUIPlayerInventoryState(this, _playerInventoryPanel.GetComponent<InventoryDisplayController>());
        GamePlayUIRoomInventoryState = new GamePlayUIRoomInventoryState(this, _roomInventoryPanel.GetComponent<RoomInventoryController>());
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

