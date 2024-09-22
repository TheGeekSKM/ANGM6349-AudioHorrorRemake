using SaiUtils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordsDisplayController : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] Button _closeButton;
    
    [Header("Slider")]
    [SerializeField] Slider _recordsPlayBarSlider;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI _recordsNameText;
    [SerializeField] TextMeshProUGUI _recordsTranscriptText;

    [Header("Audio Source")]
    [SerializeField] AudioSource _recordsAudioSource;
    RecordsData _recordsData;

    void OnValidate()
    {
        _recordsAudioSource = gameObject.GetOrAdd<AudioSource>();
    }

    void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseRecords);
    }

    void OnDisable()
    {
        _closeButton.onClick.RemoveListener(CloseRecords);
    }

    public void Initialize(RecordsData recordsData)
    {
        _recordsData = recordsData;
        _recordsNameText.text = recordsData.RecordsName;
        _recordsTranscriptText.text = recordsData.RecordsTranscript;
        _recordsAudioSource.clip = recordsData.RecordsAudioClip;
        _recordsPlayBarSlider.maxValue = recordsData.RecordsAudioClipLength;
        _recordsPlayBarSlider.value = 0;
        _recordsAudioSource.Play();
    }

    void Update()
    {
        if (_recordsAudioSource.isPlaying) _recordsPlayBarSlider.value = _recordsAudioSource.time;
    }

    void CloseRecords()
    {
        _recordsAudioSource.Stop();
        // gameObject.SetActive(false);
    }

}
