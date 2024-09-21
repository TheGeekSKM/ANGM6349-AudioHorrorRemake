using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
        _questText.text = $"<b>Quest:</b> {description}";
        GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, strength: new Vector3(10, 0, 0), vibrato: 10, randomness: 10, false, true);
    }
}
