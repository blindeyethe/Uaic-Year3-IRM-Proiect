using UnityEngine;

namespace IRM.Utility
{
    internal sealed class LookAtCamera : MonoBehaviour
    {
        private Transform _transform;
        private Transform _cameraTransform;

        private void Awake() =>
            _transform = transform;

        private void Start()
        {
            var mainCamera = Camera.main;
            
            if (mainCamera != null)
                _cameraTransform = mainCamera.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(_cameraTransform.position);
        }
    }
}