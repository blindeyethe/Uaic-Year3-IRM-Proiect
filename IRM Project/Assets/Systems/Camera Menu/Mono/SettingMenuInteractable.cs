using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.CameraMenuSystems.UI
{
    internal sealed class SettingMenuInteractable : XRBaseInteractable
    {
        [SerializeField] private float radius = 1f;
        [SerializeField] private float rotationSpeed = 200f;

        private Transform _transform;
        
        private Transform interactorTransform;
        private Vector3 _lastInteractorPosition;

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        private void Start() =>
            ArrangeChildren();

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
            int count = transform.childCount;
            float anglePerSlot = 360f / count;

            var eulerAngles = _transform.eulerAngles;

            float snappedY = Mathf.Round(eulerAngles.y / anglePerSlot) * anglePerSlot;
            _transform.rotation = Quaternion.Euler(eulerAngles.x, snappedY, eulerAngles.z);
        }
        
        private void ArrangeChildren()
        {
            int count = transform.childCount;
            
            for (int i = 0; i < count; i++)
            {
                float angle = i * Mathf.PI * 2 / count;
                var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                
                var child = transform.GetChild(i);
                child.localPosition = position;
            }
        }
    }
}