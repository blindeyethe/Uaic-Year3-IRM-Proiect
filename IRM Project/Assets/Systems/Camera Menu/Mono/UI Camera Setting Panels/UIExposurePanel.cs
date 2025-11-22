using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace IRM.CameraMenuSystems.UI
{
    public class UIExposurePanel : MonoBehaviour, UISettingPanel
    {
        [SerializeField] private float maxExposure;
        [SerializeField] private float minExposure;
        
        private Slider _slider;
        private ColorAdjustments _colorAdjustments;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            OnDeselect();
        }
        
        private void OnEnable() =>
            _slider.onValueChanged.AddListener(UpdateExposure);

        private void UpdateExposure(float value) =>
            _colorAdjustments.postExposure.value = value;

        public void Initialize(ColorAdjustments colorAdjustments)
        {
            _colorAdjustments = colorAdjustments;

            _slider.minValue = minExposure;
            _slider.maxValue = maxExposure;
            
            _slider.value = 0f;
        }

        public void OnSelect()
        {
            _slider.value = _colorAdjustments.postExposure.value;
            _slider.gameObject.SetActive(true);
        }

        public void OnDeselect() =>
            _slider.gameObject.SetActive(false);
    }
}