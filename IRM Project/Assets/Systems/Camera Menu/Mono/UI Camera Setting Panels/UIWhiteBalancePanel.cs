using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace IRM.CameraMenuSystems.UI
{
    public class UIWhiteBalancePanel : MonoBehaviour, UISettingPanel
    {
        private Slider _slider;
        private WhiteBalance _whiteBalance;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            OnDeselect();
        }

        private void OnEnable() =>
            _slider.onValueChanged.AddListener(UpdateTemperature);

        private void UpdateTemperature(float value) =>
            _whiteBalance.temperature.value = value;

        public void Initialize(WhiteBalance whiteBalance)
        {
            _whiteBalance = whiteBalance;
            
            _slider.minValue = _whiteBalance.temperature.min;
            _slider.maxValue = _whiteBalance.temperature.max;
        }

        public void OnSelect()
        {
            _slider.value = _whiteBalance.temperature.value;
            _slider.gameObject.SetActive(true);
        }

        public void OnDeselect() =>
            _slider.gameObject.SetActive(false);
    }
}