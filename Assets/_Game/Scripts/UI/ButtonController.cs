using UnityEngine;
using DG.Tweening;

public class ButtonController : MonoBehaviour {

    [SerializeField] Vector2Int _buttonRotationRange = new Vector2Int(-5, 5);
    [SerializeField] SoundData _onClickSound;
    [SerializeField] SoundData _onHoverSound;

    RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnClick()
    {
        //play sound
        if (_onClickSound) SoundManager.Instance.PlaySound2D(_onClickSound);

        var randRot = Random.Range(0, 2) == 0 ? _buttonRotationRange.x : _buttonRotationRange.y;

        if (!_rectTransform) return;
        _rectTransform.DOLocalRotate(new Vector3(0, 0, randRot), 0.1f).SetEase(Ease.OutCubic).OnComplete(() => {
            _rectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.OutCubic);
        });
    }

    public void OnPointerEnter()
    {
        //tweens to scale up
        _rectTransform.DOScale(1.1f, 0.1f).SetEase(Ease.InExpo);
        //play sound
        // if (_onHoverSound) SoundManager.Instance.PlaySound2D(_onHoverSound);
        //Debug.Log("OnMouseEnter");
    }

    public void OnPointerExit()
    {
        //tweens to scale down
        _rectTransform.DOScale(1f, 0.1f).SetEase(Ease.InExpo);
        //Debug.Log("OnMouseExit");
    }
}
