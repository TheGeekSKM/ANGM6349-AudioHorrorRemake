using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordTriggerController : MonoBehaviour
{
    [SerializeField] RecordsData _recordsData;
    [SerializeField] Button _recordPlayButton;
    [SerializeField] TextMeshProUGUI _recordNameText;

    void OnEnable()
    {
        _recordPlayButton.onClick.AddListener(PlayRecord);
    }

    void OnDisable()
    {
        _recordPlayButton.onClick.RemoveListener(PlayRecord);
    }

    void Start()
    {
        _recordNameText.text = _recordsData.RecordsName;
    }

    void PlayRecord()
    {
        SafeRoomController.Instance.PlayRecord(_recordsData);
    }
}
