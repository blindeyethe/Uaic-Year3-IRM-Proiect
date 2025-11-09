using UnityEngine;
using UnityEngine.InputSystem;
using IRM.Utility;
using IRM.PhotoSystem;

namespace IRM.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera cameraView;
        [SerializeField] private LayerMask detectionLayerMask;
        
        private PhotoValidator _photoValidator;
        
        private readonly RaycastHit[] _hits = new RaycastHit[10];

        private void Awake() =>
            _photoValidator = GetComponent<PhotoValidator>();

        private void Update()
        {
            if (!Keyboard.current.pKey.wasPressedThisFrame)
                return;
    
            var cameraTransform = cameraView.transform;
            cameraView.CalculateFrustumBounds(out var boundCenter, out var halfExtents);
            
            int count = Physics.BoxCastNonAlloc(boundCenter, halfExtents, 
                cameraTransform.forward, _hits, cameraTransform.rotation, 
                0f, detectionLayerMask);

            if (count == 0)
                return;

            var detectedObjects = PhotoObject.None;
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cameraView);
            
            for (int i = 0; i < count; i++)
            {
                var hitCollider = _hits[i].collider;
                if (!GeometryUtility.TestPlanesAABB(frustumPlanes, hitCollider.bounds))
                    continue;

                if (hitCollider.TryGetComponent<DetectionObject>(out var detectionObject))
                    detectedObjects |= detectionObject.PhotoObject;
            }

            _photoValidator.Validate(detectedObjects);
        }

        private void OnDrawGizmos()
        {
            Camera.main.CalculateFrustumBounds(out var boundCenter, out var halfExtents);
            Gizmos.DrawWireCube(boundCenter, halfExtents * 2f);
        }
    }
}