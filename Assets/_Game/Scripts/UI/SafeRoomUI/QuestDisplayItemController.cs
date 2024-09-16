using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDisplayItemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _questNameText;
    [SerializeField] TextMeshProUGUI _questDescriptionText;
    [SerializeField] TextMeshProUGUI _questStatusText;

    public void Initialize(QuestData questData)
    {
        _questNameText.text = questData.questName;
        _questDescriptionText.text = questData.questDescription;
        _questStatusText.text = questData.questCompleted ? "Completed" : "In Progress";
        _questStatusText.color = questData.questCompleted ? Color.green : Color.white;
    }

}
