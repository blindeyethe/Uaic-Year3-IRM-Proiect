using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace IRM.CameraMenuSystems.UI
{
    public class UIContrastPanel : MonoBehaviour, UISettingPanel
    {
        [SerializeField] private float minContrast = -50;
        
        private Slider _slider;
        private ColorAdjustments _colorAdjustments;
        
        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            OnDeselect();
        }
        
        private void OnEnable() =>
            _slider.onValueChanged.AddListener(UpdateContrast);

        private void UpdateContrast(float value) =>
            _colorAdjustments.contrast.value = value;
        
        public void Initialize(ColorAdjustments colorAdjustments)
        {
            _colorAdjustments = colorAdjustments;

            _slider.minValue = minContrast;
            _slider.maxValue = _colorAdjustments.contrast.max;

            _slider.value = 0f;
        }
        
        public void OnSelect()
        {
            _slider.value = _colorAdjustments.contrast.value;
            _slider.gameObject.SetActive(true);
        }

        public void OnDeselect() =>
            _slider.gameObject.SetActive(false);
    }
}