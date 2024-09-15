using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using TMPro;
using UnityEngine;

public class QuestTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _questText;
    void OnValidate() {
        if (_questText == null) _questText = gameObject.GetOrAdd<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (QuestManager.Instance.currentQuest != null)
        {
            UpdateQuestText(QuestManager.Instance.currentQuest.questDescription);
        }
    }


    public void UpdateQuestText(string description)
    {
        Debug.Log($"Quest: {description}");
        _questText.text = $"Quest: {description}";
    }
}
