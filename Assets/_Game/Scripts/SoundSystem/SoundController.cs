using Sirenix.OdinInspector;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField, ReadOnly] SoundData _soundData;
    [SerializeField] float _additionalTime = 0f;

    [Header("References")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] AudioSource _audioSource;

    [Header("Debug")]
    [SerializeField, ReadOnly] float _clipLength;
    [SerializeField, ReadOnly] float _fadeSpeed;


    void OnValidate()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitSound(SoundData soundData)
    {
        // set the sound data
        _soundData = soundData;
        _audioSource.clip = _soundData.GetRandomSound();

        // set the clip length
        if (_audioSource.clip) _clipLength = _audioSource.clip.length * 5f + _additionalTime;
        else _clipLength = 10f + _additionalTime;

        // set the volume
        _audioSource.volume = _soundData.Volume;

        // // set the sprite size
        // transform.localScale = Vector3.one * _soundData.Volume;
        
        // play the sound
        _audioSource.Play();

        // rotate the sound controller around the y axis
        transform.Rotate(Vector3.up, Random.Range(0, 360));

        // set the fade speed based on the clip length and the sprite renderer alpha so that the sound and the sprite renderer fade out at the same time
        float y1 = _spriteRenderer.color.a;
        float x2 = _clipLength;
        float negativeSlope = y1 / x2;
        _fadeSpeed = negativeSlope;

        // destroy the sound controller after the clip length
        Destroy(gameObject, _clipLength);
    }

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyController>();
        if (!enemy) return;

        // trigger the target state
        enemy.TriggerTargetState(transform);

    }

    void Update()
    {

        if (!_soundData) return;

        transform.localScale += Vector3.one * Time.deltaTime * _soundData.Volume * 15;
        
        transform.rotation = Quaternion.Euler(0, 0, 0);

        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - _fadeSpeed * Time.deltaTime);


        if (_audioSource.time >= _clipLength - (_clipLength / 2f)) // if the sound is halfway through
        {
            // fade out the sound and the sprite renderer
            _audioSource.volume -= Time.deltaTime;
        }
    }
}
