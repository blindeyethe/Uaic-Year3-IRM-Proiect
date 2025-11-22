using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.CameraMenuSystems.UI
{
    internal sealed class SettingMenuInteractable : XRBaseInteractable
    {
        [SerializeField] private Volume volume;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float rotationSpeed = 200f;

        private Transform _transform;
        
        private Transform interactorTransform;
        private Vector3 _lastInteractorPosition;

        private CameraSetting _currentSetting;
        private CameraSetting[] _settings;
        
        protected override void Awake()
        {
            base.Awake();
            _settings = GetComponentsInChildren<CameraSetting>();
            _transform = transform;
        }

        private void Start() =>
            InitializeSettings();

        protected override void OnSelectEntered(SelectEnterEventArgs eventInfo)
        {
            if (eventInfo.interactorObject == null)
                return;
            
            interactorTransform = eventInfo.interactorObject.transform;
            _lastInteractorPosition = interactorTransform.position;
        }

        protected override void OnSelectExited(SelectExitEventArgs eventInfo)
        {
            interactorTransform = null;
            SnapRotation();
            UpdateCurrentSetting();
        }

        private void Update()
        {
            if (interactorTransform == null)
                return;

            var delta = interactorTransform.position - _lastInteractorPosition;
            float rotationDelta = delta.x * rotationSpeed;

            _transform.Rotate(Vector3.up, -rotationDelta, Space.World);
            _lastInteractorPosition = interactorTransform.position;
        }

        private void SnapRotation()
        {
            int count = _settings.Length;
            float anglePerSlot = 360f / count;

            var eulerAngles = _transform.eulerAngles;

            float snappedY = Mathf.Round(eulerAngles.y / anglePerSlot) * anglePerSlot;
            _transform.rotation = Quaternion.Euler(eulerAngles.x, snappedY, eulerAngles.z);
        }
        
        private void InitializeSettings()
        {
            int count = _settings.Length;
            
            for (int i = 0; i < count; i++)
            {
                float angle = i * Mathf.PI * 2 / count;
                var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                
                _settings[i].transform.localPosition = position;
                _settings[i].Initialize(volume);
            }
        }
        
        private int GetCurrentIndex()
        {
            int count = _settings.Length;
            float anglePerSlot = 360f / count;

            float y = _transform.eulerAngles.y % 360f;
            y = y < 0 ? y + 360f : y;

            int index = Mathf.RoundToInt(y / anglePerSlot) % count;
            return index;
        }
        
        private void UpdateCurrentSetting()
        {
            int index = GetCurrentIndex();
            if (_currentSetting == _settings[index])
                return;
            
            if(_currentSetting != null)
                _currentSetting.OnExited();
            
            _currentSetting = _settings[index];
            
            _currentSetting.OnSelected();
        }
    }
}