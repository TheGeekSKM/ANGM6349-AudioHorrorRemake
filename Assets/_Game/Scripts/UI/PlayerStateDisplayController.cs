using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateDisplayController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _playerStateText;

    [SerializeField] Color _walkingHexColor;
    [SerializeField] string _walkingText = "WALKING";
    [SerializeField] string _standingText = "STANDING";
    public void UpdatePlayerState(bool isPlayerMoving)
    {
        var hexColor = "#" + ColorUtility.ToHtmlStringRGB(_walkingHexColor);
        if (isPlayerMoving) _playerStateText.text = $"<color={hexColor}>{_walkingText}</color>";
        else _playerStateText.text = _standingText;
    }    
}
