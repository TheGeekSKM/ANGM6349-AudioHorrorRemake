using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaiUtils.StateMachine;

public class QuestBaseState : IState
{
    protected QuestManager questManager;
    protected QuestData questData;
    public QuestData QuestData => questData;

    public QuestBaseState(QuestManager questManager, QuestData questData)
    {
        this.questManager = questManager;
        this.questData = questData;
    }


    public void OnEnter() 
    {
        questManager.UpdateQuestText(questData.questDescription);
        questManager.Quests.Add(questData);
        questManager.currentQuest = questData;
    }


    public void FixedUpdate() { }
    public void OnExit() { }
    public void Update() { }
}

public class QuestData
{
    public string questName;
    public string questDescription;
    public bool questCompleted;

    public QuestData(string questName, string questDescription)
    {
        this.questName = questName;
        this.questDescription = questDescription;
        questCompleted = false;
    }
}
