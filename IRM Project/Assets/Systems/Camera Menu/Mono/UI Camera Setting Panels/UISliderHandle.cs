using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.CameraMenuSystems.UI
{
    public class UISliderHandle : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private XRGrabInteractable _grabInteractable;
        private Transform _transform;
        
        private float _sliderLength;
        private Vector3 _sliderDirection;

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _transform = transform;
            
            ComputeSliderData();
        }
        
        private void ComputeSliderData()
        {
            var start = startPoint.position;
            var end = endPoint.position;

            _sliderDirection = end - start;
            _sliderLength = _sliderDirection.magnitude;
            _sliderDirection.Normalize();
        }

        private void Update()
        {
            if (!_grabInteractable.isSelected)
            {
                UpdateHandleFromSlider();
                return;
            }
            
            UpdateSliderFromHandle();
        }

        private void UpdateSliderFromHandle()
        {
            var start = startPoint.position;
            var startToHandle = _transform.position - start;
            
            float dot = Vector3.Dot(startToHandle, _sliderDirection);
            dot = Mathf.Clamp(dot, 0, _sliderLength);
            
            _transform.position = start + _sliderDirection * dot;

            float value = dot / _sliderLength;
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, value);
        }

        private void UpdateHandleFromSlider()
        {
            float value = Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value);
            float dot = value * _sliderLength;
            
            _transform.position = startPoint.position + _sliderDirection * dot;
            _transform.rotation = slider.transform.rotation;
        }
    }
}