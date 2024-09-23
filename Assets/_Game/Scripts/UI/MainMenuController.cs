using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] RectTransform _titleText;
    [SerializeField] RectTransform _buttonsPanel;
    [SerializeField] Image _blackScreen;
    [SerializeField] Button _creditsButton;
    [SerializeField] Button _creditsCloseButton;
    [SerializeField] RectTransform _creditsPanel;

    bool _creditsOpen = false;

    void OnEnable()
    {
        _creditsButton.onClick.AddListener(ShowCredits);
        _creditsCloseButton.onClick.AddListener(HideCredits);
    }

    void OnDisable()
    {
        _creditsButton.onClick.RemoveListener(ShowCredits);
        _creditsCloseButton.onClick.RemoveListener(HideCredits);
    }

    void Start()
    {
        _blackScreen.DOFade(0f, 2f).OnComplete(() =>
        {
            _titleText.DOAnchorPosY(0f, 0.5f).SetEase(Ease.OutBack);
            _buttonsPanel.DOAnchorPosY(0f, 0.5f).SetEase(Ease.OutBack);
        });
    }

    void ShowCredits()
    {
        _blackScreen.DOFade(1f, 0.5f);
        if (_creditsOpen)
            return;

        _creditsOpen = true;
        _creditsPanel.DOAnchorPosY(0f, 0.5f).SetEase(Ease.OutBack);
    }

    void HideCredits()
    {
        if (!_creditsOpen)
            return;

        _creditsOpen = false;
        _creditsPanel.DOAnchorPosY(-_creditsPanel.rect.height - 180f, 0.5f).SetEase(Ease.InBack);
        _blackScreen.DOFade(0f, 0.5f);
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GameCutsceneState, 0.2f);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
