using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace IRM.CameraMenuSystems.UI
{
    public class UIHueShiftPanel : MonoBehaviour, UISettingPanel
    {
        private Slider _slider;
        private ColorAdjustments _colorAdjustments;
        
        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            OnDeselect();
        }
        
        private void OnEnable() =>
            _slider.onValueChanged.AddListener(UpdateHue);

        private void UpdateHue(float value) =>
            _colorAdjustments.hueShift.value = value;

        public void Initialize(ColorAdjustments colorAdjustments)
        {
            _colorAdjustments = colorAdjustments;

            _slider.minValue = _colorAdjustments.hueShift.min;
            _slider.maxValue = _colorAdjustments.hueShift.max;

            _slider.value = 0f;
        }
        
        public void OnSelect()
        {
            _slider.value = _colorAdjustments.hueShift.value;
            _slider.gameObject.SetActive(true);
        }

        public void OnDeselect() =>
            _slider.gameObject.SetActive(false);
    }
}