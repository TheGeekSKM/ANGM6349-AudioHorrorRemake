using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatLogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _chatLogText;
    public void Initialize(string message)
    {
        // Initialize the chat log UI
        _chatLogText.text = message;
    }
}
