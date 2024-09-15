using System.Collections.Generic;
using SaiUtils.GameEvents;
using SaiUtils.StateMachine;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    List<QuestData> _quests = new List<QuestData>();
    public List<QuestData> Quests => _quests;

    public StringEvent OnQuestTextUpdate;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    StateMachine _questStateMachine;
    

    public void UpdateQuestText(string description)
    {
        // noop
        OnQuestTextUpdate.Raise(description);
    }
}
