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


    public void UpdateQuestText(string description)
    {
        _questText.text = $"Quest: {description}";
    }
}
