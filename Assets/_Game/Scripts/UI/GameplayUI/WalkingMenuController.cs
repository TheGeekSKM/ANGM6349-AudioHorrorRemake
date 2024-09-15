using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class WalkingMenuController : MonoBehaviour
{
    [SerializeField] Button _walkingButton;
    [SerializeField] TextMeshProUGUI _walkingButtonText;

    PlayerMovement _playerMovement;

    void Start()
    {
        _playerMovement = PlayerController.Instance.PlayerMovement;
    }

    void OnEnable()
    {
        _walkingButton.onClick.AddListener(OnWalkingButtonClicked);
    }

    void OnDisable()
    {
        _walkingButton.onClick.RemoveListener(OnWalkingButtonClicked);
    }

    void OnWalkingButtonClicked()
    {
        if (_playerMovement.IsMoving)
        {
            _playerMovement.Stop();
            _walkingButtonText.text = "Walk Forward";
        }
        else
        {
            _playerMovement.Move();
            _walkingButtonText.text = "Stop Walking";
        }
    }
}
