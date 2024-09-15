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

}
