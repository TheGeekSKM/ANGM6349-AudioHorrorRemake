using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuController : MonoBehaviour
{
    [SerializeField] QuestDisplayItemController _questDisplayItemControllerPrefab;
    [SerializeField] Transform _questDisplayItemParent;


    void Start()
    {
        DisplayQuests(QuestManager.Instance.Quests);
    }

    public void DisplayQuests(List<QuestData> quests)
    {
        foreach (Transform child in _questDisplayItemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in quests)
        {
            if (quest.questActivated)
            {
                var questDisplayItem = Instantiate(_questDisplayItemControllerPrefab, _questDisplayItemParent);
                questDisplayItem.Initialize(quest);
            }
        }
    }
}
