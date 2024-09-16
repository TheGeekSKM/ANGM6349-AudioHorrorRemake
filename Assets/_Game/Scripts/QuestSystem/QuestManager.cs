using System.Collections.Generic;
using SaiUtils.GameEvents;
using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    List<QuestData> _quests = new List<QuestData>();
    public List<QuestData> Quests => _quests;

    public QuestData currentQuest;

    public StringEvent OnQuestTextUpdate;

    [Header("Quests")]
    [SerializeField] GameObject FindBandagesQuestListener;
    [SerializeField] GameObject BringBandagesQuestListener;
    [SerializeField] GameObject FindMonsterRoomQuestListener;
    [SerializeField] GameObject ReportMonsterRoomQuestListener;
    [SerializeField] GameObject FindSuppliesQuestListener;
    [SerializeField] GameObject BringSuppliesBackQuestListener;
    [SerializeField] GameObject FindExitRoomQuestListener;
    [SerializeField] GameObject ReportExitRoomQuestListener;
    [SerializeField] GameObject KillMonsterListener;
    [SerializeField] GameObject ReportKillListener;
    [SerializeField] GameObject ExplodeExitRoomListener;

    [Header("Cutscenes")]
    [SerializeField] DialogueSceneSO _bringBandagesCutscene;
    [SerializeField] DialogueSceneSO _reportMonsterRoomCutscene;
    [SerializeField] DialogueSceneSO _bringSuppliesBackCutscene;
    [SerializeField] DialogueSceneSO _reportExitRoomCutscene;
    [SerializeField] DialogueSceneSO _reportKillCutscene;

    [Header("Required Items")]
    [SerializeField] ItemData _bandages;
    [SerializeField] ItemData _explosiveChemicals;
    [SerializeField] ItemData _beaker;
    bool _foundExplosiveChemicals = false;
    bool _foundBeaker = false;

    bool _reportedKill = false;
    bool _explodedExitRoom = false;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        ConfigureStateMachine();
    }


    StateMachine _questStateMachine;

#region All The Quests
    public QuestFindBandagesState QuestFindBandagesState { get; private set; }
    public QuestBringBandagesState QuestBringBandagesState { get; private set; }
    public QuestFindMonsterNestState QuestFindMonsterNestState { get; private set; }
    public QuestReportMonsterRoomState QuestReportMonsterRoomState { get; private set; }
    public QuestFindSuppliesState QuestFindSuppliesState { get; private set; }
    public QuestBringSuppliesBackState QuestBringSuppliesBackState { get; private set; }
    public QuestFindExitRoomState QuestFindExitRoomState { get; private set; }
    public QuestReportExitRoomState QuestReportExitRoomState { get; private set; }
    public QuestKillMonsterState QuestKillMonsterState { get; private set; }
    public QuestReportKillOrLeave QuestReportKillOrLeaveState { get; private set; }
#endregion

    void ConfigureStateMachine()
    {
        _questStateMachine = new StateMachine();

        QuestFindBandagesState = new QuestFindBandagesState(this, new QuestData("Find Bandages", "Find bandages in the <b>Infirmary</b>..."));
        _questStateMachine.AddAnyTransition(QuestFindBandagesState, new BlankPredicate());
        _quests.Add(QuestFindBandagesState.QuestData);

        QuestBringBandagesState = new QuestBringBandagesState(this, new QuestData("Bring Bandages", "Bring bandages to the man in the <b>Safe Room</b>..."));
        _questStateMachine.AddTransition(QuestFindBandagesState, QuestBringBandagesState, new BlankPredicate());
        _quests.Add(QuestBringBandagesState.QuestData);


        QuestFindMonsterNestState = new QuestFindMonsterNestState(this, new QuestData("Find Monster Nest", "Find the monster nest..."));
        _questStateMachine.AddTransition(QuestFindSuppliesState, QuestFindMonsterNestState, new BlankPredicate());
        _quests.Add(QuestFindMonsterNestState.QuestData);

        QuestReportMonsterRoomState = new QuestReportMonsterRoomState(this, new QuestData("Report Monster Room", "Report the Monster Nest's location to the man in the <b>Safe Room</b>..."));
        _questStateMachine.AddTransition(QuestFindMonsterNestState, QuestReportMonsterRoomState, new BlankPredicate());
        _quests.Add(QuestReportMonsterRoomState.QuestData);

        QuestFindSuppliesState = new QuestFindSuppliesState(this, new QuestData("Find Supplies", 
            "Find explosive chemicals in the <b>Chemical Storage Room</b> and beakers in the <b>Supply Storage Room</b>..."));
        _questStateMachine.AddTransition(QuestBringBandagesState, QuestFindSuppliesState, new BlankPredicate());
        _quests.Add(QuestFindSuppliesState.QuestData);

        QuestBringSuppliesBackState = new QuestBringSuppliesBackState(this, new QuestData("Bring Supplies Back", "Bring the chemicals and beaker back to the man in the <b>Safe Room</b>..."));
        _questStateMachine.AddTransition(QuestFindSuppliesState, QuestBringSuppliesBackState, new BlankPredicate());
        _quests.Add(QuestBringSuppliesBackState.QuestData);
       
        QuestFindExitRoomState = new QuestFindExitRoomState(this, new QuestData("Find Exit Room", "Find the exit room..."));
        _questStateMachine.AddTransition(QuestBringSuppliesBackState, QuestFindExitRoomState, new BlankPredicate());
        _quests.Add(QuestFindExitRoomState.QuestData);

        QuestReportExitRoomState = new QuestReportExitRoomState(this, new QuestData("Report Exit Room", "Report the exit room to the man in the <b>Safe Room</b>..."));
        _questStateMachine.AddTransition(QuestFindExitRoomState, QuestReportExitRoomState, new BlankPredicate());
        _quests.Add(QuestReportExitRoomState.QuestData);

        QuestKillMonsterState = new QuestKillMonsterState(this, new QuestData("Kill Monster", "Kill the monster..."));
        _questStateMachine.AddTransition(QuestReportMonsterRoomState, QuestKillMonsterState, new BlankPredicate());
        _quests.Add(QuestKillMonsterState.QuestData);

        QuestReportKillOrLeaveState = new QuestReportKillOrLeave(this, new QuestData("Report Kill", "Report the monster's death to the man, or explode the <b>Exit Room</b> and leave..."));
        _questStateMachine.AddTransition(QuestKillMonsterState, QuestReportKillOrLeaveState, new BlankPredicate());
        _quests.Add(QuestReportKillOrLeaveState.QuestData);

        _questStateMachine.SetState(QuestFindBandagesState);
    }

    void Start()
    {
        QuestListenerSetup();
    }

    void QuestListenerSetup()
    {
        QuestFindBandagesState.QuestData.questActivated = true;

        FindBandagesQuestListener.SetActive(true);
        BringBandagesQuestListener.SetActive(false);
        FindMonsterRoomQuestListener.SetActive(false);
        ReportMonsterRoomQuestListener.SetActive(false);
        FindSuppliesQuestListener.SetActive(false);
        BringSuppliesBackQuestListener.SetActive(false);
        FindExitRoomQuestListener.SetActive(false);
        ReportExitRoomQuestListener.SetActive(false);
        KillMonsterListener.SetActive(false);
        ReportKillListener.SetActive(false);
        ExplodeExitRoomListener.SetActive(false);
    }
    

    public void ChangeQuestState(QuestBaseState state)
    {
        _questStateMachine.ChangeState(state);
    }

    public void UpdateQuestText(string description)
    {
        // noop
        OnQuestTextUpdate.Raise(description);
    }

    public void FoundBandages()
    {
        if (QuestFindBandagesState.QuestData.questCompleted) return;
        QuestFindBandagesState.QuestData.questCompleted = true;

        BringBandagesQuestListener.SetActive(true);
        QuestBringBandagesState.QuestData.questActivated = true;

        ChangeQuestState(QuestBringBandagesState);
        FindBandagesQuestListener.SetActive(false);
    }

    public void BroughtBandages()
    {
        if (QuestBringBandagesState.QuestData.questCompleted) return;
        QuestBringBandagesState.QuestData.questCompleted = true;

        PlayerController.Instance.InventoryController.RemoveItem(_bandages);
        GameManager.Instance.PlayCutscene(_bringBandagesCutscene);
        FindMonsterRoomQuestListener.SetActive(true);
        QuestFindMonsterNestState.QuestData.questActivated = true;

        ChangeQuestState(QuestFindSuppliesState);
        BringBandagesQuestListener.SetActive(false);
    }

    public void FoundMonsterRoom()
    {
        if (QuestFindMonsterNestState.QuestData.questCompleted) return;
        QuestFindMonsterNestState.QuestData.questCompleted = true;

        ReportMonsterRoomQuestListener.SetActive(true);
        QuestReportMonsterRoomState.QuestData.questActivated = true;

        ChangeQuestState(QuestReportMonsterRoomState);
        FindMonsterRoomQuestListener.SetActive(false);
    }

    public void ReportedMonsterRoom()
    {
        if (QuestReportMonsterRoomState.QuestData.questCompleted) return;
        QuestReportMonsterRoomState.QuestData.questCompleted = true;

        GameManager.Instance.PlayCutscene(_reportMonsterRoomCutscene);
        QuestFindSuppliesState.QuestData.questActivated = true;
        FindSuppliesQuestListener.SetActive(true);

        ChangeQuestState(QuestFindSuppliesState);
        ReportMonsterRoomQuestListener.SetActive(false);
    }

    public void FoundChemicalsAndBeaker(ItemData item)
    {
        if (item.ItemName == _explosiveChemicals.ItemName) _foundExplosiveChemicals = true;
        if (item.ItemName == _beaker.ItemName) _foundBeaker = true;

        if (_foundExplosiveChemicals && _foundBeaker) 
        {
            if (QuestFindSuppliesState.QuestData.questCompleted) return;
            QuestFindSuppliesState.QuestData.questCompleted = true;

            BringSuppliesBackQuestListener.SetActive(true);
            QuestBringSuppliesBackState.QuestData.questActivated = true;

            ChangeQuestState(QuestBringSuppliesBackState);
            FindSuppliesQuestListener.SetActive(false);
        }
    }

    public void LostChemicalsOrBeaker(ItemData itemData)
    {
        if (itemData.ItemName == _explosiveChemicals.ItemName) _foundExplosiveChemicals = false;
        if (itemData.ItemName == _beaker.ItemName) _foundBeaker = false;
    }

    public void BroughtChemicalsAndBeaker()
    {
        if (QuestBringSuppliesBackState.QuestData.questCompleted) return;
        QuestBringSuppliesBackState.QuestData.questCompleted = true;

        PlayerController.Instance.InventoryController.RemoveItem(_explosiveChemicals);
        PlayerController.Instance.InventoryController.RemoveItem(_beaker);
        GameManager.Instance.PlayCutscene(_bringSuppliesBackCutscene);
        FindExitRoomQuestListener.SetActive(true);
        QuestFindExitRoomState.QuestData.questActivated = true;

        ChangeQuestState(QuestFindExitRoomState);
        BringSuppliesBackQuestListener.SetActive(false);
    }

    public void FoundExitRoom()
    {
        if (QuestFindExitRoomState.QuestData.questCompleted) return;
        QuestFindExitRoomState.QuestData.questCompleted = true;

        ReportExitRoomQuestListener.SetActive(true);
        QuestReportExitRoomState.QuestData.questActivated = true;

        ChangeQuestState(QuestReportExitRoomState);
        FindExitRoomQuestListener.SetActive(false);
    }

    public void ReportedExitRoom()
    {
        if (QuestReportExitRoomState.QuestData.questCompleted) return;
        QuestReportExitRoomState.QuestData.questCompleted = true;

        GameManager.Instance.PlayCutscene(_reportExitRoomCutscene);
        QuestKillMonsterState.QuestData.questActivated = true;
        KillMonsterListener.SetActive(true);
  
        ReportExitRoomQuestListener.SetActive(false);
        ChangeQuestState(QuestKillMonsterState);
    }

    public void KilledMonster()
    {
        if (QuestKillMonsterState.QuestData.questCompleted) return;
        QuestKillMonsterState.QuestData.questCompleted = true;

        ReportKillListener.SetActive(true);
        ExplodeExitRoomListener.SetActive(true);
        QuestReportKillOrLeaveState.QuestData.questActivated = true;


        ChangeQuestState(QuestReportKillOrLeaveState);
        KillMonsterListener.SetActive(false);
    }

    public void ReportedKill()
    {
        if (QuestReportKillOrLeaveState.QuestData.questCompleted) return;
        QuestReportKillOrLeaveState.QuestData.questCompleted = true;
        _reportedKill = true;

        GameManager.Instance.PlayCutscene(_reportKillCutscene);
        ReportKillListener.SetActive(false);

        if (!_explodedExitRoom) UpdateQuestText("Explode the exit room and leave...");
        else UpdateQuestText("Leave through the exit room...");
    }

    public void ExplodedExitRoom()
    {
        if (QuestReportKillOrLeaveState.QuestData.questCompleted) return;
        QuestReportKillOrLeaveState.QuestData.questCompleted = true;
        _explodedExitRoom = true;

        ExplodeExitRoomListener.SetActive(false);

        if (!_reportedKill) UpdateQuestText("Leave the man behind...");
        else UpdateQuestText("Leave through the exit room...");
    }

}
