using System;
using UnityEngine;
using UnityEngine.InputSystem;
using IRM.Utility;
using IRM.PhotoSystem;

namespace IRM.CameraSystem
{
    public class PhotoDetection : MonoBehaviour
    {
        public static event Action<Bounds> OnObjectDetected;
        public static event Action OnStartObjectDetection;
        
        [SerializeField] private Camera cameraView;
        [SerializeField] private LayerMask detectionLayerMask;
        
        private Transform _cameraViewTransform;
        private readonly RaycastHit[] _hits = new RaycastHit[10];

        private void Start() =>
            _cameraViewTransform = cameraView.transform;

        private void Update() =>
            Detect();

        public PhotoObject Detect()
        {
            cameraView.CalculateFrustumBounds(out var boundCenter, out var halfExtents);
            
            int count = Physics.BoxCastNonAlloc(boundCenter, halfExtents, 
                _cameraViewTransform.forward, _hits, _cameraViewTransform.rotation, 
                0f, detectionLayerMask);

            OnStartObjectDetection?.Invoke();
            if (count == 0)
                return 0;

            var detectedObjects = PhotoObject.None;
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cameraView);
            
            for (int i = 0; i < count; i++)
            {
                var hitCollider = _hits[i].collider;
                if (!GeometryUtility.TestPlanesAABB(frustumPlanes, hitCollider.bounds))
                    continue;

                if (!hitCollider.TryGetComponent<DetectionObject>(out var detectionObject)) 
                    continue;
                
                OnObjectDetected?.Invoke(hitCollider.bounds);
                detectedObjects |= detectionObject.PhotoObject;
            }

            return detectedObjects;
        }
        
        private void OnDrawGizmos()
        {
            Camera.main.CalculateFrustumBounds(out var boundCenter, out var halfExtents);
            Gizmos.DrawWireCube(boundCenter, halfExtents * 2f);
        }
    }
}