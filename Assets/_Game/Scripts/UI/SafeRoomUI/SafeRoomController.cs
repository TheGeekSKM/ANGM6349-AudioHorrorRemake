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
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] GameObject _questPanel;
    [SerializeField] GameObject _craftPanel;
    [SerializeField] GameObject _notePadPanel;
    [SerializeField] GameObject _recordsPanel;
    [SerializeField] float _notePadPanelY;

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

    void ConfigureStateMachine()
    {
        _safeRoomStateMachine = new StateMachine();

        SafeRoomUICraftState = new SafeRoomUICraftState(this, _craftPanel);
        SafeRoomUIQuestState = new SafeRoomUIQuestState(this, _questPanel);
        SafeRoomUIDefaultState = new SafeRoomUIDefaultState(this, _dialoguePanel);
        SafeRoomUIRecordsState = new SafeRoomUIRecordsState(this, _recordsPanel);

        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIQuestState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUIQuestState, SafeRoomUICraftState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIDefaultState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUIRecordsState, SafeRoomUIDefaultState, new BlankPredicate());

        _safeRoomStateMachine.SetState(SafeRoomUIDefaultState);
    }

    void OnEnable()
    {
        _leaveButton.onClick.AddListener(() => GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GamePlayState, 0.2f));
        _questButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIQuestState));
        _questCloseButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIDefaultState));
        _craftButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUICraftState));
        _craftCloseButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIDefaultState));
        _notePadButton.onClick.AddListener(() => ShowNotepad());
        _notePadCloseButton.onClick.AddListener(() => HideNotepad());
        _recordsButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIRecordsState));
        _recordsCloseButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIDefaultState));
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
        _notePadPanelY = _notePadPanel.GetComponent<RectTransform>().anchoredPosition.y;
    }

    [Button]
    void ShowNotepad()
    {
        // set the notepad anchor position y to 0
        _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(_notePadPanel.GetComponent<RectTransform>().anchoredPosition.x, 0);
    }

    [Button]
    void HideNotepad()
    {
        _notePadPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(_notePadPanel.GetComponent<RectTransform>().anchoredPosition.x, _notePadPanelY);    
    }
}
