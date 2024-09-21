using TMPro;
using UnityEngine;
using DG.Tweening;

public class ChatLogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _chatLogText;
    int _chatLogCount = 0;
    float positionX;

    public void Initialize(string message, int chatLogCount)
    {
        positionX = GetComponent<RectTransform>().anchoredPosition.x;

        // Initialize the chat log UI
        _chatLogText.text = message;
        _chatLogCount = chatLogCount;

        GetComponent<RectTransform>().DOAnchorPosX(positionX - 100f, 0f);

        // SoundManager.Instance.PlaySound2D(SoundAtlas.Instance.PencilSound);
        // GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, strength:new Vector3(10, 0, 0), vibrato:10, randomness:10, false, true);
        GetComponent<RectTransform>().DOAnchorPosX(positionX, 0.2f).SetEase(Ease.OutBounce);

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
