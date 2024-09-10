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


    [Header("Panels")]
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] GameObject _questPanel;
    [SerializeField] GameObject _craftPanel;
    [SerializeField] GameObject _notePadPanel;
    [SerializeField] float _notePadPanelY;

    StateMachine _safeRoomStateMachine;
    public StateMachine SafeRoomStateMachine => _safeRoomStateMachine;

    public SafeRoomUICraftState SafeRoomUICraftState { get; private set; }
    public SafeRoomUIQuestState SafeRoomUIQuestState { get; private set; }
    public SafeRoomUIDefaulState SafeRoomUIDefaultState { get; private set; }

    private void Awake()
    {
        ConfigureStateMachine();

        _leaveButton.onClick.AddListener(() => GameManager.Instance.GameStateMachine.ChangeState(GameManager.Instance.GamePlayState));
        _questButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIQuestState));
        _questCloseButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIDefaultState));
        _craftButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUICraftState));
        _craftCloseButton.onClick.AddListener(() => _safeRoomStateMachine.ChangeState(SafeRoomUIDefaultState));
        _notePadButton.onClick.AddListener(() => ShowNotepad());
        _notePadCloseButton.onClick.AddListener(() => HideNotepad());
    }

    void ConfigureStateMachine()
    {
        _safeRoomStateMachine = new StateMachine();

        SafeRoomUICraftState = new SafeRoomUICraftState(this, _craftPanel);
        SafeRoomUIQuestState = new SafeRoomUIQuestState(this, _questPanel);
        SafeRoomUIDefaultState = new SafeRoomUIDefaulState(this, _dialoguePanel);

        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIQuestState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUIQuestState, SafeRoomUICraftState, new BlankPredicate());
        _safeRoomStateMachine.AddTransition(SafeRoomUICraftState, SafeRoomUIDefaultState, new BlankPredicate());

        _safeRoomStateMachine.SetState(SafeRoomUIDefaultState);
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
