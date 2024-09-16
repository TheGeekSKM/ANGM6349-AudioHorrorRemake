using System.Collections.Generic;
using SaiUtils.GameEvents;
using SaiUtils.StateMachine;
using UnityEngine;

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

    [Header("Cutscenes")]
    [SerializeField] DialogueSceneSO _bringBandagesCutscene;

    [Header("Required Items")]
    [SerializeField] ItemData _bandages;

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
    public QuestFindSuppliesState QuestFindSuppliesState { get; private set; }
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

        QuestFindSuppliesState = new QuestFindSuppliesState(this, new QuestData("Find Supplies", "Find explosive chemicals in the <b>Chemical Storage Room</b>..."));
        _questStateMachine.AddTransition(QuestBringBandagesState, QuestFindSuppliesState, new BlankPredicate());
        _quests.Add(QuestFindSuppliesState.QuestData);

        _questStateMachine.SetState(QuestFindBandagesState);
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

        ChangeQuestState(QuestBringBandagesState);
        FindBandagesQuestListener.SetActive(false);
    }

    public void BroughtBandages()
    {
        if (QuestBringBandagesState.QuestData.questCompleted) return;
        QuestBringBandagesState.QuestData.questCompleted = true;

        PlayerController.Instance.InventoryController.RemoveItem(_bandages);
        GameManager.Instance.PlayCutscene(_bringBandagesCutscene);

        ChangeQuestState(QuestFindSuppliesState);
        BringBandagesQuestListener.SetActive(false);
    }

}
