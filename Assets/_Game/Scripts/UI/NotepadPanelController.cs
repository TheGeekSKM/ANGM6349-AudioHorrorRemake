using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using SaiUtils.Extensions;
using UnityEngine.UI;

public class NotepadPanelController : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] private float hoverInPosY;
    [SerializeField] private float hoverOutPosY;
    [SerializeField] Button _openButton;
    [SerializeField] Button _closeButton;

    bool isOpen = false;

#region Editor Functions
    [Button]
    void CalculateHoverIn()
    {
        hoverInPosY = rectTransform.anchoredPosition.y;
    }

    [Button]
    void CalculateHoverOut()
    {
        hoverOutPosY = rectTransform.anchoredPosition.y;
    }
#endregion

    private void OnValidate()
    {
        rectTransform = gameObject.GetOrAdd<RectTransform>();
    }

    void OnEnable()
    {
        _openButton.onClick.AddListener(() => isOpen = true);
        _closeButton.onClick.AddListener(() => Invoke("CloseNotepad", 0.3f));
    }

    public void HoverIn()
    {
        if (isOpen) return;
        rectTransform.DOAnchorPosY(hoverInPosY, 0.2f);
    }

    public void HoverOut()
    {
        if (isOpen) return;
        rectTransform.DOAnchorPosY(hoverOutPosY, 0.2f);
    }

    void CloseNotepad() => isOpen = false;



}
