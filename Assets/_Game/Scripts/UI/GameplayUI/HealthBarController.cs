using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider _healthBarSlider;

    public void UpdateHealthBar(float healthPercentage) 
    {
        _healthBarSlider.value = healthPercentage;
        if (healthPercentage > 0f) GetComponent<RectTransform>().DOShakeAnchorPos(
                                        0.2f, strength: new Vector3(10, 0, 0), vibrato: 10, randomness: 10, false, true
                                    );
    }
}
