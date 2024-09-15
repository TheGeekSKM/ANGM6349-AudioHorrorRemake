using System.Collections;
using DG.Tweening;
using SaiUtils.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SafeRoomController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _leaveButton;
    [SerializeField] Button _questButton;
    [SerializeField] Button _questCloseButton;
    [SerializeField] Button _craftButton;
    [SerializeField] Button _craftCloseButton;
    [SerializeField] Button _notePadButton;
    [SerializeField] Button _notePadCloseButton;
    [SerializeField] Button _recordsButton;
    [SerializeField] Button _recordsCloseButton;


    [Header("Panels")]
    [SerializeField] RectTransform _dialoguePanel;
    [SerializeField] RectTransform _questPanel;
    [SerializeField] RectTransform _craftPanel;
    [SerializeField] RectTransform _notePadPanel;
    [SerializeField] RectTransform _recordsPanel;
    [SerializeField] float _notePadPanelY;

    [Header("Panel Settings")]
    [SerializeField] Vector2 _onScreenPos = new Vector2(0, 50f);
    [SerializeField] Vector2 _offScreenDialoguePanelPos;
    [SerializeField] Vector2 _offScreenQuestPanelPos;
    [SerializeField] Vector2 _offScreenCraftPanelPos;
    [SerializeField] Vector2 _offScreenRecordsPanelPos;
    [SerializeField] bool _enableAnimations = true;
    StateMachine _safeRoomStateMachine;
    public StateMachine SafeRoomStateMachine => _safeRoomStateMachine;

    public SafeRoomUICraftState SafeRoomUICraftState { get; private set; }
    public SafeRoomUIQuestState SafeRoomUIQuestState { get; private set; }
    public SafeRoomUIDefaultState SafeRoomUIDefaultState { get; private set; }
    public SafeRoomUIRecordsState SafeRoomUIRecordsState { get; private set; }

    private void Awake()
    {
        ConfigureStateMachine();
    }

    [Button]
    void CalculateOffScreenPositions()
    {
        _offScreenDialoguePanelPos = _dialoguePanel.anchoredPosition;
        _offScreenQuestPanelPos = _questPanel.anchoredPosition;
        _offScreenCraftPanelPos = _craftPanel.anchoredPosition;
        _offScreenRecordsPanelPos = _recordsPanel.anchoredPosition;
    }

    void ConfigureStateMachine()
    {
        _safeRoomStateMachine = new StateMachine();

        SafeRoomUICraftState = new SafeRoomUICraftState(this, _craftPanel, _onScreenPos, _offScreenCraftPanelPos, _enableAnimations);
        SafeRoomUIQuestState = new SafeRoomUIQuestState(this, _questPanel, _onScreenPos, _offScreenQuestPanelPos, _enableAnimations);
        SafeRoomUIDefaultState = new SafeRoomUIDefaultState(this, _dialoguePanel, _onScreenPos, _offScreenDialoguePanelPos, _enableAnimations);
        SafeRoomUIRecordsState = new SafeRoomUIRecordsState(this, _recordsPanel, _onScreenPos, _offScreenRecordsPanelPos, _enableAnimations);

        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIQuestState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUIQuestState, SafeRoomUICraftState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIDefaultState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUIRecordsState, SafeRoomUIDefaultState, new BlankPredicate());

        _safeRoomStateMachine.SetState(SafeRoomUIDefaultState);
    }

    void OnEnable()
    {
        _leaveButton.onClick.AddListener(() => GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GamePlayState, 0.2f));
        _questButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUIQuestState, 0.2f));
        _questCloseButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUIDefaultState, 0.2f));
        _craftButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUICraftState, 0.2f));
        _craftCloseButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUIDefaultState, 0.2f));
        _notePadButton.onClick.AddListener(() => ShowNotepad());
        _notePadCloseButton.onClick.AddListener(() => HideNotepad());
        _recordsButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUIRecordsState, 0.2f));
        _recordsCloseButton.onClick.AddListener(() => ChangeSafeRoomStateWithDelay(SafeRoomUIDefaultState, 0.2f));
    }

    void OnDisable()
    {
        _leaveButton.onClick.RemoveAllListeners();
        _questButton.onClick.RemoveAllListeners();
        _questCloseButton.onClick.RemoveAllListeners();
        _craftButton.onClick.RemoveAllListeners();
        _craftCloseButton.onClick.RemoveAllListeners();
        _notePadButton.onClick.RemoveAllListeners();
        _notePadCloseButton.onClick.RemoveAllListeners();
        _recordsButton.onClick.RemoveAllListeners();
        _recordsCloseButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _notePadPanelY = _notePadPanel.anchoredPosition.y;
    }

    [Button]
    void ShowNotepad()
    {
        // set the notepad anchor position y to 0
        _notePadPanel.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutExpo);
    }

    [Button]
    void HideNotepad()
    {
        // set the notepad anchor position y to the original value
        _notePadPanel.DOAnchorPosY(_notePadPanelY, 0.3f).SetEase(Ease.OutExpo);    
    }

    public void ChangeSafeRoomStateWithDelay(SafeRoomUIBaseState state, float delay)
    {
        StartCoroutine(ChangeSafeRoomStateWithDelayCoroutine(state, delay));
    }

    IEnumerator ChangeSafeRoomStateWithDelayCoroutine(SafeRoomUIBaseState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        _safeRoomStateMachine.ChangeState(state);
    }
    
}
