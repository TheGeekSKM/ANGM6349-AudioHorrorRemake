using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeRoomController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _leaveButton;
    [SerializeField] Button _questButton;
    [SerializeField] Button _questCloseButton;
    [SerializeField] Button _craftButton;
    [SerializeField] Button _craftCloseButton;
    [SerializeField] Button _notePadButton;
    [SerializeField] Button _notePadCloseButton;
    

    [Header("Panels")]
    [SerializeField] GameObject _questPanel;
    [SerializeField] GameObject _craftPanel;
    [SerializeField] GameObject _notePadPanel;
}
