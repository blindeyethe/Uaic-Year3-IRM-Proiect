using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace IRM.CameraMenuSystems.UI
{
    public class UIDepthOfFieldPanel : MonoBehaviour, UISettingPanel
    {
        [SerializeField] private float _maxFocusDistance = 50;
        
        private Slider _slider;
        private DepthOfField _depthOfField;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            OnDeselect();
        }
        
        private void OnEnable() =>
            _slider.onValueChanged.AddListener(UpdateFocusDistance);

        private void UpdateFocusDistance(float value) =>
            _depthOfField.focusDistance.value = value;

        public void Initialize(DepthOfField depthOfField)
        {
            _depthOfField = depthOfField;

            _slider.minValue = _depthOfField.focusDistance.min;
            _slider.maxValue = _maxFocusDistance;
        }

        public void OnSelect()
        {
            _slider.value = _depthOfField.focusDistance.value;
            _slider.gameObject.SetActive(true);
        }
        
        public void OnDeselect() =>
            _slider.gameObject.SetActive(false);
    }
}