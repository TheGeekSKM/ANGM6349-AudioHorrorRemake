using System.Collections;
using TMPro;
using UnityEngine;
using SaiUtils.Extensions;
using DG.Tweening;
using Sirenix.OdinInspector;

public class LocalCutsceneManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI _characterNameText;
    [SerializeField] TextMeshProUGUI _dialogueText;

    [Header("Audio")]
    [SerializeField] AudioSource _voiceAudioSource;
    [SerializeField] AudioSource _backgroundMusicAudioSource;

    [Header("Skip Button")]
    [SerializeField] GameObject _skipButton;
    [SerializeField, ReadOnly] float _skipButtonYPosition;
    RectTransform _skipButtonRectTransform;
    bool _isSkipButtonEnabled = false;

    Coroutine _dialogueCoroutine;

    private void OnValidate()
    {
        if (_voiceAudioSource == null) _voiceAudioSource = gameObject.GetOrAdd<AudioSource>();
    }

    private void Start()
    {
        _characterNameText.text = string.Empty;
        _dialogueText.text = string.Empty;

        _skipButtonRectTransform = _skipButton.GetComponent<RectTransform>();
        _skipButtonYPosition = _skipButtonRectTransform.anchoredPosition.y;

        DialogueStart(CutsceneManager.Instance.CurrentDialogueScene);
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0)) ToggleEnableSkipButton();
    // }

    public void ToggleEnableSkipButton()
    {
        // Debug.Log("ToggleEnableSkipButton");

        if (_isSkipButtonEnabled)
        {
            //_skipButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _skipButtonYPosition);
            _skipButtonRectTransform.DOAnchorPosY(_skipButtonYPosition, 0.5f).SetEase(Ease.OutExpo);
            _isSkipButtonEnabled = false;
            
        }
        else
        {
            // _skipButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            _skipButtonRectTransform.DOAnchorPosY(50f, 0.5f).SetEase(Ease.OutExpo);
            _isSkipButtonEnabled = true;
        }
    }

    public void DialogueStart(DialogueSceneSO dialogueSceneSO)
    {
        // Debug.Log("DialogueStarted");

        if (_dialogueCoroutine != null) StopCoroutine(_dialogueCoroutine);

        _dialogueCoroutine = StartCoroutine(DialogueRoutine(dialogueSceneSO));
    }

    IEnumerator DialogueRoutine(DialogueSceneSO dialogueSceneSO)
    {
        _characterNameText.text = string.Empty;
        _dialogueText.text = string.Empty;

        foreach (var dialogue in dialogueSceneSO.DialogueLines)
        {
            _dialogueText.text = dialogue.Line;
            if (dialogue.VoiceClip) _voiceAudioSource.PlayOneShot(dialogue.VoiceClip);

            yield return new WaitForSeconds(dialogue.Duration);
        }

        OnDialogueEnd();
    }

    public void SkipDialogue()
    {
        if (_dialogueCoroutine != null) StopCoroutine(_dialogueCoroutine);
        _dialogueText.text = string.Empty;

        OnDialogueEnd();
    }

    public void OnDialogueEnd()
    {
        _characterNameText.text = string.Empty;
        _dialogueText.text = string.Empty;
        CutsceneManager.Instance.ClearDialogue();
        GameManager.Instance.ChangeGameStateWithDelay(GameManager.Instance.GamePlayState, 0.2f);
    }
}
