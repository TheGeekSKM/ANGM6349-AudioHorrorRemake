using TMPro;
using UnityEngine;
using DG.Tweening;

public class ChatLogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _chatLogText;
    int _chatLogCount = 0;

    public void Initialize(string message, int chatLogCount)
    {
        // Initialize the chat log UI
        _chatLogText.text = message;
        _chatLogCount = chatLogCount;

        // SoundManager.Instance.PlaySound2D(SoundAtlas.Instance.PencilSound);
        _chatLogText.GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, strength:new Vector3(10, 0, 0), vibrato:10, randomness:10, false, true);

    }

    public void ChatLogCountUpdated(int chatLogCount)
    {
        // if the chat log count is greater than the current chat log count, make this chat log slightly more transparent each time
        if (chatLogCount > _chatLogCount && _chatLogText.color.a > 0.3f)
        {
            _chatLogText.color = new Color(
                _chatLogText.color.r, _chatLogText.color.g, 
                _chatLogText.color.b, _chatLogText.color.a - 0.15f
            );
        }
    }
}
